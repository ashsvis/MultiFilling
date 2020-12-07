using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MultiFilling.RiserTuning
{
    public partial class FormRiserTuning : Form
    {
        private readonly List<IFetchUpdate> _updateList = new List<IFetchUpdate>();
        private int _pageIndex;

        public RiserAddress? RiserAddress { get; set; }

        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = value;
                switch (_pageIndex)
                {
                    case 0:
                        tabControl1.SelectedTab = tabPage1;
                        SelectTabControl(tabPage1);
                        break;
                    case 1:
                        tabControl1.SelectedTab = tabPage2;
                        SelectTabControl(tabPage2);
                        break;
                    case 2:
                        tabControl1.SelectedTab = tabPage3;
                        SelectTabControl(tabPage3);
                        break;
                    case 3:
                        tabControl1.SelectedTab = tabPage4;
                        SelectTabControl(tabPage4);
                        break;
                    case 4:
                        tabControl1.SelectedTab = tabPage5;
                        SelectTabControl(tabPage5);
                        break;
                }
            }
        }

        public FormRiserTuning()
        {
            InitializeComponent();
            tabControl1.SelectedTab = null;
        }

        private void FormRiserTuning_Load(object sender, EventArgs e)
        {
            SelectTabControl(tabPage1);
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.Action == TabControlAction.Selecting)
            {
                SelectTabControl(e.TabPage);
            }
        }

        private void SelectTabControl(Control page)
        {
            Control item;
            switch (page.Text)
            {
                case "Параметры связи":
                    if (page.Controls.Count == 0)
                    {
                        item = new RiserTuningLinkControl {Dock = DockStyle.Fill};
                        (item as RiserTuningLinkControl).OnWrite += UcOneController_OnWrite;
                        page.Controls.Add(item);
                        page.Tag = item.Size;
                        _updateList.Add((IFetchUpdate) item);
                    }
                    Size = new Size(680, 491);
                    break;
                case "PLC":
                    if (page.Controls.Count == 0)
                    {
                        item = new RiserTuningPlcControl {Dock = DockStyle.Fill};
                        (item as RiserTuningPlcControl).OnWrite += UcOneController_OnWrite;
                        page.Controls.Add(item);
                        page.Tag = item.Size;
                        _updateList.Add((IFetchUpdate) item);
                    }
                    Size = new Size(680, 480);
                    break;
                case "ADC":
                    if (page.Controls.Count == 0)
                    {
                        item = new RiserTuningAdcControl {Dock = DockStyle.Fill};
                        (item as RiserTuningAdcControl).OnWrite += UcOneController_OnWrite;
                        page.Controls.Add(item);
                        page.Tag = item.Size;
                        _updateList.Add((IFetchUpdate) item);
                    }
                    Size = new Size(680, 685);
                    break;
                case "Сигнализатор аварийный":
                    if (page.Controls.Count == 0)
                    {
                        item = new RiserTuningAlarmLevelControl {Dock = DockStyle.Fill};
                        (item as RiserTuningAlarmLevelControl).OnWrite += UcOneController_OnWrite;
                        page.Controls.Add(item);
                        page.Tag = item.Size;
                        _updateList.Add((IFetchUpdate) item);
                    }
                    Size = new Size(680, 462);
                    break;
                case "Сигнализатор уровня":
                    if (page.Controls.Count == 0)
                    {
                        item = new RiserTuningAnalogLevelControl {Dock = DockStyle.Fill};
                        (item as RiserTuningAnalogLevelControl).OnWrite += UcOneController_OnWrite;
                        page.Controls.Add(item);
                        page.Tag = item.Size;
                        _updateList.Add((IFetchUpdate) item);
                    }
                    Size = new Size(680, 546);
                    break;
            }
        }

        private void UcOneController_OnWrite(int address, int regcount, ushort[] hregs, string[] changelogdata = null)
        {
            if (RiserAddress == null) return;
            string name;
            RiserAddress addr;
            lock (Data.RiserNodes)
            {
                var riser = Data.RiserNodes[(RiserAddress)RiserAddress];
                name = riser.Name;
                addr = riser.Address;
                riser.WriteAddress = address;
                riser.WriteData = hregs;
            }
            if (changelogdata == null) return;
            foreach (var data in changelogdata)
            {
                var vals = data.Split(new[] { '\t' });
                if (vals.Length != 4) continue;
                var param = vals[0];
                var oldval = vals[1];
                var newval = vals[2];
                var desc = vals[3];
                Data.SendToChangeLog(name, addr, param, oldval, newval, Data.UserName, desc);
            }
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;
            if (RiserAddress == null)
            {
                foreach (var item in _updateList)
                    item.UpdateTimeout();
                return;
            }
            var addr = (RiserAddress) RiserAddress;
            bool active;
            int riser, channel, barometerValue;
            long marginalLimit;
            ushort[] hregs;
            lock (Data.RiserNodes)
            {
                if (!Data.RiserNodes.ContainsKey(addr)) return;
                var riserNode = Data.RiserNodes[addr];
                channel = riserNode.Channel;
                riser = riserNode.Riser;
                active = riserNode.Active;
                barometerValue = riserNode.BarometerValue;
                marginalLimit = riserNode.MarginalLimit;
                hregs = riserNode.Hregs;
            }
            var remoted = true;
            lock (Data.ChannelNodes)
            {
                if (channel >= 0 && channel < Data.ChannelNodes.Count)
                    remoted = !Data.ChannelNodes[channel].Active;
            }
            Text = string.Format("Настройка [ Стояк {0} ]", riser);
            foreach (var item in _updateList)
            {
                if (active && barometerValue < marginalLimit)
                    item.UpdateData(hregs, remoted);
                else
                    item.UpdateTimeout();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
