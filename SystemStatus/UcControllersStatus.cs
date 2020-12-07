using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace MultiFilling.SystemStatus
{
    public partial class UcControllersStatus : UserControl, IUserControlMisc
    {
        private readonly List<LinkLabel> _linkLabels = new List<LinkLabel>();
        private readonly Dictionary<string, UcRiserOneState> _lamps = 
            new Dictionary<string, UcRiserOneState>();
        private readonly Dictionary<string, CheckBox> _cboxs = 
            new Dictionary<string, CheckBox>();

        public int DisplayIndex { get; set; }

        public int ChannelIndex
        {
            get { return cbChannelByName.SelectedIndex; }
            set
            {
                if (value >= 0 && value < cbChannelByName.Items.Count)
                    cbChannelByName.SelectedIndex = value;
                else if (cbChannelByName.Items.Count > 0)
                    cbChannelByName.SelectedIndex = 0;
            }
        }

        public UcControllersStatus()
        {
            InitializeComponent();
        }

        public void Loaded()
        {
            ucTreeNavigator1.SelectNode("ndControllers");
            try
            {
                cbChannelByName.SelectedIndexChanged -= cbChannelByName_SelectedIndexChanged;
                lock (Data.ChannelNodes)
                {
                    var list = new List<string>();
                    foreach (var channel in Data.ChannelNodes.OrderBy(item => item.Name)
                        .Where(channel => !list.Contains(channel.Name)))
                    {
                        list.Add(channel.Name);
                        cbChannelByName.Items.Add(new ChannelComboItem { Index = channel.Index, Name = channel.Name });
                    }
                    //cbChannelByName.SelectedIndex = Data.Session.ReadInteger("SystemStatus" + DisplayIndex, "ChannelIndex", -1);
                }
                ChannelIndex = Data.Session.ReadInteger("SystemStatus" + DisplayIndex, "ChannelIndex", -1);
                FillControllerslGrid();
            }
            finally
            {
                cbChannelByName.SelectedIndexChanged += cbChannelByName_SelectedIndexChanged;
            }
            timerUpdate_Tick(null, null);
        }

        private void FillControllerslGrid()
        {
            timerUpdate.Enabled = false;
            _lamps.Clear();
            foreach (var linkLabel in _linkLabels)
                linkLabel.LinkClicked -= linkToControllerPage_LinkClicked;
            _linkLabels.Clear();

            foreach (var checkBox in _cboxs.Values)
                checkBox.CheckedChanged -= checkBox_CheckedChanged;
            _cboxs.Clear();

            var channelComboItem = (ChannelComboItem)cbChannelByName.SelectedItem;

            if (channelComboItem == null) return;

            ChannelNode channel;
            lock (Data.ChannelNodes)
            {
                channel = Data.ChannelNodes[channelComboItem.Index];
                labDesc.Text = channel.Descriptor;
            }

            var nrows = channel.Risers.Count;

            flowLayoutPanel1.Controls.Clear();

            for (var i = 0; i < nrows; i++)
            {
                var flowLayoutPanelOneRow = new FlowLayoutPanel
                    {
                        Margin = new Padding(0, 0, 0, 0),
                        Padding = new Padding(10, 0, 0, 0),
                        FlowDirection = FlowDirection.LeftToRight,
                        //BackColor = Color.MediumPurple,
                        AutoSize = true
                    };

                var checkBox = new CheckBox
                    {
                        AutoSize = false,
                        Enabled = false,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Margin = new Padding(3, 3, 3, 3),
                        Name = "checkBox" + channel.Risers[i].Index,
                        Text = channel.Risers[i].Riser.ToString("0"),
                        Size = new Size(60, 26),
                        //BackColor = Color.MediumAquamarine,
                        TabIndex = 1,
                        UseVisualStyleBackColor = true,
                        Checked = channel.Risers[i].Active
                    };

                checkBox.CheckedChanged += checkBox_CheckedChanged;

                _cboxs.Add(checkBox.Name, checkBox);

                flowLayoutPanelOneRow.Controls.Add(checkBox);

                var addr = new RiserAddress
                    {
                        Channel = channel.Index,
                        Overpass = channel.Overpass, 
                        Way = channel.Way, 
                        Product = channel.Product,
                        Riser = channel.Risers[i].Riser
                    };
                var linkToControllerPage = new LinkLabel
                    {
                        AutoSize = false,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Name = channel.Risers[i].Name,
                        Margin = new Padding(20, 0, 0, 0),
                        Size = new Size(106, 28),
                        //BackColor = Color.Aquamarine,
                        TabIndex = 2,
                        TabStop = true,
                        Text = channel.Risers[i].Name,
                        Tag = addr,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                linkToControllerPage.LinkClicked += linkToControllerPage_LinkClicked;

                flowLayoutPanelOneRow.Controls.Add(linkToControllerPage);

                var labelId = new Label
                    {
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Name = "labelId",
                        Size = new Size(93, 28),
                        //BackColor = Color.MediumAquamarine,
                        TabIndex = 0,
                        Text = channel.Risers[i].Node.ToString("0"),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                flowLayoutPanelOneRow.Controls.Add(labelId);

                var linkToChannelPage = new LinkLabel
                    {
                        AutoSize = false,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Name = "linxLabel" + channel.Index,
                        Size = new Size(116, 28),
                        //BackColor = Color.Aquamarine,
                        TabIndex = 2,
                        TabStop = true,
                        Text = channel.Name,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                linkToChannelPage.LinkClicked += linkToChannelPage_LinkClicked;
                _linkLabels.Add(linkToChannelPage);

                flowLayoutPanelOneRow.Controls.Add(linkToChannelPage);

                var lampBox = new UcRiserOneState
                    {
                        Caption = "------",
                        CaptionAtRight = false,
                        Font = new Font("Courier New", 9.75F, FontStyle.Bold),
                        LampColorNone = Color.Gray,
                        LampColorOff = Color.Black,
                        LampColorOn = Color.Lime,
                        Margin = new Padding(0, 0, 0, 0),
                        Name = channel.Risers[i].Name,
                        Size = new Size(129, 28),
                        //BackColor = Color.Aquamarine,
                        State = null,
                        TabIndex = 3
                    };
                _lamps.Add(lampBox.Name, lampBox);

                if (channel.Risers[i].Active)
                {
                    if (channel.Risers[i].BarometerValue >= channel.Risers[i].FailLimit)
                    {
                        lampBox.State = true;
                        lampBox.LampColorOn = Color.Red;
                        lampBox.Caption = "Отказ";
                    }
                    else if (channel.Risers[i].BarometerValue >= channel.Risers[i].MarginalLimit)
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

                flowLayoutPanelOneRow.Controls.Add(lampBox);
                flowLayoutPanelOneRow.SetFlowBreak(lampBox, true);

                flowLayoutPanel1.Controls.Add(flowLayoutPanelOneRow);
            }

            timerUpdate.Enabled = true;

        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Включение опроса контроллера",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Eng)
            {
                var cbox = (CheckBox) sender;
                int index;
                if (!int.TryParse(cbox.Name.Substring(8), out index)) return;
                var channelComboItem = (ChannelComboItem) cbChannelByName.SelectedItem;
                lock (Data.ChannelNodes)
                {
                    var riserNode = Data.ChannelNodes[channelComboItem.Index].Risers[index];
                    riserNode.Active = cbox.Checked;
                    Data.SendToSystemLog(cbox.Checked ? "Установка соединения" : "Обрыв соединения", riserNode.Address);
                    var channelName = string.Format("Channel{0}", riserNode.Channel + 1);
                    var name = riserNode.Name;
                    Data.Config.WriteBool(channelName, name, cbox.Checked);
                    Data.Config.UpdateFile();
                    var val = cbox.Checked;
                    Data.SendToChangeLog(name, riserNode.Address,
                                         "Active", !val ? "1" : "0", val ? "1" : "0", Data.UserName, "Опрос контроллера");
                }
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Включение опроса контроллера",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void linkToControllerPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var link = (LinkLabel)sender;
            if (link.Tag == null) return;
            var addr = (RiserAddress)link.Tag;
            var parent = Parent;
            while (parent != null)
            {
                if (parent is Form)
                {
                    Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "Channel", addr.Channel);
                    Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "Overpass", addr.Overpass);
                    Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "Way", addr.Way);
                    Data.Session.WriteString("SystemStatus" + DisplayIndex, "Product", addr.Product);
                    Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "Riser", addr.Riser);
                    Data.Navigate(GetType(), new ControllersStatusArgs
                        {
                            Panel = parent,
                            WhatShow = ControllerShowAs.OneControllerPage
                        });
                    break;
                }
                parent = parent.Parent;
            }
        }

        private void linkToChannelPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var link = (LinkLabel)sender;
            int index;
            if (!int.TryParse(link.Name.Substring(9), out index)) return;
            var parent = Parent;
            while (parent != null)
            {
                if (parent is Form)
                {
                    Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "ChannelIndex", index);
                    Data.Navigate(GetType(), new ControllersStatusArgs
                        {
                            Panel  = parent, 
                            WhatShow = ControllerShowAs.OneChannelPage
                        });
                    break;
                }
                parent = parent.Parent;
            }
        }

        private void cbChannelByName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "ChannelIndex", cbChannelByName.SelectedIndex);
            FillControllerslGrid();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            var channel = (ChannelComboItem)cbChannelByName.SelectedItem;
            if (channel == null) return;
            lock (Data.RiserNodes)
            {
                foreach (var riserNode in Data.RiserNodes.Values
                    .Where(item => item.Channel == channel.Index))
                {
                    if (!_lamps.ContainsKey(riserNode.Name)) continue;
                    var lampBox = _lamps[riserNode.Name];
                    if (riserNode.Active)
                    {
                        var perc = Math.Round(riserNode.TotalErrors * 100.0 / riserNode.TotalRequests, 0);
                        var errors = perc.ToString("G", CultureInfo.GetCultureInfo("en-US"));

                        if (riserNode.BarometerValue >= riserNode.FailLimit)
                        {
                            lampBox.State = true;
                            lampBox.LampColorOn = Color.Red;
                            lampBox.Caption = "Отказ";
                        }
                        else if (riserNode.BarometerValue >= riserNode.MarginalLimit)
                        {
                            lampBox.State = true;
                            lampBox.LampColorOn = Color.Yellow;
                            lampBox.Caption = "Сбой (" + errors + "%)";
                        }
                        else
                        {
                            lampBox.State = true;
                            lampBox.LampColorOn = Color.Lime;
                            lampBox.Caption = Math.Abs(perc) < double.Epsilon ? "Норма" : "Норма (" + errors + "%)";
                        }
                    }
                    else
                    {
                        lampBox.State = false;
                        lampBox.Caption = "Не активен";
                    }

                    var cboxName = "checkBox" + riserNode.Index;
                    if (!_cboxs.ContainsKey(cboxName)) continue;
                    var cbox = _cboxs[cboxName];
                    try
                    {
                        cbox.CheckedChanged -= checkBox_CheckedChanged;
                        cbox.Checked = riserNode.Active;
                        cbox.Enabled = Data.UserLevel >= UserLevel.Eng;
                    }
                    finally
                    {
                        cbox.CheckedChanged += checkBox_CheckedChanged;
                    }
                }
            }

        }

        public void Unload()
        {
            timerUpdate.Tick -= timerUpdate_Tick;
        }
    }
}
