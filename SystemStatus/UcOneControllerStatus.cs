using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using MultiFilling.RiserTuning;

namespace MultiFilling.SystemStatus
{
    public partial class UcOneControllerStatus : UserControl, IUserControlMisc
    {
        private readonly List<IFetchUpdate> _updateList = new List<IFetchUpdate>();

        private RiserAddress _riserAddress;
        private int _nodeType;
        
        public int DisplayIndex { get; set; }

        public UcOneControllerStatus()
        {
            InitializeComponent();
            lvLogView.SetDoubleBuffered(true);
            tabControl1.TabPages.Add(new TabPage("Состояние") { BackColor = SystemColors.Window });
            tabControl1.TabPages.Add(new TabPage("Параметры связи") { BackColor = SystemColors.Window });
            tabControl1.TabPages.Add(new TabPage("Логика PLC") { BackColor = SystemColors.Window });
            tabControl1.TabPages.Add(new TabPage("Параметры ADC") { BackColor = SystemColors.Window });
            tabControl1.TabPages.Add(new TabPage("Сигнализатор аварийный") { BackColor = SystemColors.Window });
            tabControl1.TabPages.Add(new TabPage("Сигнализатор уровня") { BackColor = SystemColors.Window });
            tabControl1.TabPages.Add(new TabPage("Диагностика работы контроллера") { BackColor = SystemColors.Window });
        }

        public void Loaded()
        {
            ucTreeNavigator1.SelectNode("ndControllers");
            _riserAddress.Channel = Data.Session.ReadInteger("SystemStatus" + DisplayIndex, "Channel", 0);
            _riserAddress.Overpass = Data.Session.ReadInteger("SystemStatus" + DisplayIndex, "Overpass", 0);
            _riserAddress.Way = Data.Session.ReadInteger("SystemStatus" + DisplayIndex, "Way", 0);
            _riserAddress.Product = Data.Session.ReadString("SystemStatus" + DisplayIndex, "Product", "");
            _riserAddress.Riser = Data.Session.ReadInteger("SystemStatus" + DisplayIndex, "Riser", 0);
            int channelIndex;
            lock (Data.RiserNodes)
            {
                var riserNode = Data.RiserNodes[_riserAddress];
                _nodeType = riserNode.NodeType;
                channelIndex = riserNode.Channel;
                lblControlerId.Text = riserNode.Node.ToString("0");
                lblControllerName.Text = riserNode.Name;
                lblOverpass.Text = riserNode.Overpass.ToString("0");
                lblWay.Text = riserNode.Way.ToString("0");
                lblProduct.Text = Data.GetFineProductName(riserNode.Product);
                lblRiser.Text = riserNode.Riser.ToString("0");
                if (riserNode.NodeType < cbNodeType.Items.Count)
                    cbNodeType.SelectedIndex = riserNode.NodeType;
            }
            lock (Data.ChannelNodes)
            {
                var channel = Data.ChannelNodes[channelIndex];
                lblChannelName.Text = channel.Name;
                lblChannelDesc.Text = channel.Descriptor;                    
            }
            Data.OnUpdateSwitchLog += Data_OnUpdateSwitchLog;

            vScrollBar1.Maximum = Data.Config.ReadInteger("General", "LogMessagesCount", 500);
            vScrollBar1.Value = vScrollBar1.Maximum; 

            UpdateFirst();
        }

        private void UpdateFirst()
        {
            UpdateListView();
            timerUpdate_Tick(null, null);            
        }

        private void Data_OnUpdateSwitchLog(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void cbNodeType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Включение опроса контроллера",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Eng)
            {
                var cbox = (ComboBox) sender;
                lock (Data.RiserNodes)
                {
                    var riserNode = Data.RiserNodes[_riserAddress];
                    var lastval = riserNode.NodeType;
                    riserNode.NodeType = cbox.SelectedIndex;
                    _nodeType = riserNode.NodeType;

                    var channelName = string.Format("Channel{0}", riserNode.Channel + 1);
                    var name = riserNode.Name + "_NodeType";
                    Data.Config.WriteInteger(channelName, name, riserNode.NodeType);
                    Data.Config.UpdateFile();

                    var val = cbox.SelectedIndex;
                    Data.SendToChangeLog(name, _riserAddress,
                                         "NodeType", lastval.ToString("0"), val.ToString("0") , Data.UserName, "Тип контроллера");
                }
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Включение опроса контроллера",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void checkBoxActive_CheckedChanged(object sender, EventArgs e)
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Включение опроса контроллера",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Eng)
            {
                var checkbox = (CheckBox) sender;
                lock (Data.RiserNodes)
                {
                    var riserNode = Data.RiserNodes[_riserAddress];
                    if (checkbox.Checked)
                    {
                        riserNode.BarometerValue = 0;
                        riserNode.TotalRequests = 0;
                        riserNode.TotalErrors = 0;
                        riserNode.Active = true;
                        Data.SendToSystemLog("Установка соединения", riserNode.Address);
                    }
                    else
                    {
                        riserNode.Active = false;
                        Data.SendToSystemLog("Обрыв соединения", riserNode.Address);
                    }

                    var channelName = string.Format("Channel{0}", riserNode.Channel + 1);
                    var name = riserNode.Name;
                    Data.Config.WriteBool(channelName, name, riserNode.Active);
                    Data.Config.UpdateFile();

                    var val = checkbox.Checked;
                    Data.SendToChangeLog(name, _riserAddress,
                                         "Active", !val ? "1" : "0", val ? "1" : "0", Data.UserName, "Опрос контроллера");

                    var lampBox = riserOneStateControl1;
                    if (lampBox == null) return;
                    if (checkbox.Checked)
                    {
                        lampBox.State = true;
                        lampBox.LampColorOn = Color.Lime;
                        lampBox.Caption = "Норма";
                    }
                    else
                    {
                        lampBox.State = false;
                        lampBox.Caption = "Не активен";
                    }
                }
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Включение опроса контроллера",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            bool active;
            int channel, marginalLimit, failLimit, barometerValue;
            long totalRequests, totalErrors;
            ushort[] hregs;
            lock (Data.RiserNodes)
            {
                if (!Data.RiserNodes.ContainsKey(_riserAddress)) return;
                var riserNode = Data.RiserNodes[_riserAddress];
                channel = riserNode.Channel;
                active = riserNode.Active;
                marginalLimit = riserNode.MarginalLimit;
                failLimit = riserNode.FailLimit;
                totalRequests = riserNode.TotalRequests;
                totalErrors = riserNode.TotalErrors;
                barometerValue = riserNode.BarometerValue;
                hregs = riserNode.Hregs;
            }
            var remoted = true;
            lock (Data.ChannelNodes)
            {
                if (channel >= 0 && channel < Data.ChannelNodes.Count)
                    remoted = !Data.ChannelNodes[channel].Active;
            }
            try
            {
                checkBoxActive.CheckedChanged -= checkBoxActive_CheckedChanged;
                checkBoxActive.Checked = active;
                checkBoxActive.Enabled = Data.UserLevel >= UserLevel.Eng;
                cbNodeType.Enabled = Data.UserLevel >= UserLevel.Eng;
            }
            finally
            {
                checkBoxActive.CheckedChanged += checkBoxActive_CheckedChanged;
            }
            var lampBox = riserOneStateControl1;
            if (active)
            {
                if (barometerValue >= failLimit)
                {
                    lampBox.State = true;
                    lampBox.LampColorOn = Color.Red;
                    lampBox.Caption = "Отказ";
                }
                else if (barometerValue >= marginalLimit)
                {
                    lampBox.State = true;
                    lampBox.LampColorOn = Color.Yellow;
                    lampBox.Caption = "Сбой";
                }
                else
                {
                    lampBox.State = true;
                    lampBox.LampColorOn = Color.Lime;
                    lampBox.Caption = "Норма";
                }
            }
            else
            {
                lampBox.State = false;
                lampBox.Caption = "Не активен";
            }
            lblTotalRequests.Text = totalRequests.ToString("0");
            lblTotalErrors.Text = totalErrors.ToString("0");
            if (totalRequests > 0)
            {
                var perc = Math.Round(totalErrors*100.0/totalRequests, 3);
                var value = perc.ToString("G", CultureInfo.GetCultureInfo("en-US"));
                lblErrorPercent.Text = value.Substring(0, Math.Min(6, value.Length));
            }
            else
                lblErrorPercent.Text = @".00000";
            lblBarometerValue.Text = barometerValue.ToString("0");
            lblMarginalLimit.Text = marginalLimit.ToString("0");
            lblFailLimit.Text = failLimit.ToString("0");
            //----------------------------------------------------------
            foreach (var item in _updateList)
            {
                if (active && barometerValue < marginalLimit)
                    item.UpdateData(hregs, remoted);
                else
                    item.UpdateTimeout();
            }

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.Action != TabControlAction.Selecting) return;
            Control item;
            switch (e.TabPage.Text)
            {
                case "Состояние":
                    if (e.TabPage.Controls.Count == 0)
                    {
                        item = new RiserStatusControl { Dock = DockStyle.Fill, NodeType = _nodeType };
                        e.TabPage.Controls.Add(item);
                        _updateList.Add((IFetchUpdate) item);
                    }
                    break;
                case "Параметры связи":
                    if (e.TabPage.Controls.Count == 0)
                    {
                        item = new RiserTuningLinkControl { Dock = DockStyle.Fill, NodeType = _nodeType };
                        (item as IFetchUpdate).OnWrite += UcOneController_OnWrite;
                        e.TabPage.Controls.Add(item);
                        _updateList.Add((IFetchUpdate) item);
                    }
                    break;
                case "Логика PLC":
                    if (e.TabPage.Controls.Count == 0)
                    {
                        item = new RiserTuningPlcControl { Dock = DockStyle.Fill, NodeType = _nodeType };
                        (item as IFetchUpdate).OnWrite += UcOneController_OnWrite;
                        e.TabPage.Controls.Add(item);
                        _updateList.Add((IFetchUpdate) item);
                    }
                    break;
                case "Параметры ADC":
                    if (e.TabPage.Controls.Count == 0)
                    {
                        item = new RiserTuningAdcControl { Dock = DockStyle.Fill, NodeType = _nodeType };
                        (item as IFetchUpdate).OnWrite += UcOneController_OnWrite;
                        e.TabPage.Controls.Add(item);
                        _updateList.Add((IFetchUpdate) item);
                    }
                    break;
                case "Сигнализатор аварийный":
                    if (e.TabPage.Controls.Count == 0)
                    {
                        item = new RiserTuningAlarmLevelControl { Dock = DockStyle.Fill, NodeType = _nodeType };
                        (item as IFetchUpdate).OnWrite += UcOneController_OnWrite;
                        e.TabPage.Controls.Add(item);
                        _updateList.Add((IFetchUpdate) item);
                    }
                    break;
                case "Сигнализатор уровня":
                    if (e.TabPage.Controls.Count == 0)
                    {
                        item = new RiserTuningAnalogLevelControl { Dock = DockStyle.Fill, NodeType = _nodeType };
                        (item as IFetchUpdate).OnWrite += UcOneController_OnWrite;
                        e.TabPage.Controls.Add(item);
                        _updateList.Add((IFetchUpdate) item);
                    }
                    break;
                case "Диагностика работы контроллера":
                    if (e.TabPage.Controls.Count == 0)
                    {
                        item = new RiserWorkDiagramControl { Dock = DockStyle.Fill, NodeType = _nodeType };
                        (item as IFetchUpdate).OnWrite += UcOneController_OnWrite;
                        e.TabPage.Controls.Add(item);
                        _updateList.Add((IFetchUpdate)item);
                    }
                    break;
            }
        }

        private void UcOneController_OnWrite(int address, int regcount, ushort[] hregs, string[] changelogdata = null)
        {
            string name;
            RiserAddress addr;
            lock (Data.RiserNodes)
            {
                var riser = Data.RiserNodes[_riserAddress];
                name = riser.Name;
                addr = riser.Address;
                riser.WriteAddress = address;
                riser.WriteData = hregs;
            }
            if (changelogdata == null) return;
            foreach (var data in changelogdata)
            {
                var vals = data.Split(new[] {'\t'});
                if (vals.Length != 4) continue;
                var param = vals[0];
                var oldval = vals[1];
                var newval = vals[2];
                var desc = vals[3];
                Data.SendToChangeLog(name, addr, param, oldval, newval, Data.UserName, desc);
            }
        }

        private void UpdateListView()
        {
            LoadLog(vScrollBar1.Maximum - vScrollBar1.Value);
        }

        readonly List<ListViewItem> _reportrows = new List<ListViewItem>();

        private int _viewCount = 34;

        private void LoadLog(int pos, bool print = false)
        {
            if (pos < 0) return;
            var dateBefore = DateTime.Now.AddDays(-1);
            CalcRowsCount(lvLogView);
            var count = _viewCount;
            int linescount;
            var results = Data.GetSwitchLogRecords(pos, count, dateBefore, out linescount, _riserAddress);
            if (count <= 0) return;
            _reportrows.Clear();
            var row = 0;
            foreach (string[] rec in results)
            {
                if (rec.Length != 11) continue;
                var item = new ListViewItem(rec[0]);
                if (row % 2 != 0)
                    item.BackColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
                var name = rec[7];
                var oldvalue = rec[8];
                var newvalue = rec[9];
                var desc = rec[10];
                item.SubItems.AddRange(new[] {name, oldvalue, newvalue, desc});
                _reportrows.Add(item);
                row++;
            }
            UpdateColumnWidths(lvLogView);
            if (print) return;
            lvLogView.BeginUpdate();
            try
            {
                lvLogView.Items.Clear();
                lvLogView.Items.AddRange(_reportrows.ToArray());
            }
            finally
            {
                lvLogView.EndUpdate();
            }
        }

        private static void UpdateColumnWidths(ListView lv)
        {
            var panelwidth = lv.ClientSize.Width;
            var sum = 0;
            for (var i = 0; i < lv.Columns.Count - 1; i++)
                sum += lv.Columns[i].Width;
            lv.Columns[lv.Columns.Count - 1].Width = panelwidth - sum;
        }

        private void CalcRowsCount(ListView lv)
        {   // Автоматический подсчет количества строк, которые умещаются в ListView без прокрутки
            var hasrows = lv.Items.Count > 0;
            if (!hasrows) lv.Items.Add("0");
            var itemHeight = lv.GetItemRect(0).Height * 1.0;
            _viewCount = Convert.ToInt32(Math.Truncate(lv.ClientSize.Height / itemHeight) - 1);
            if (!hasrows) lv.Items.Clear();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateListView();
        }


        public void Unload()
        {
            Data.OnUpdateSwitchLog -= Data_OnUpdateSwitchLog;
            timerUpdate.Tick -= timerUpdate_Tick;
        }
    }
}
