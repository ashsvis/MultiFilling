using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MultiFilling.SystemStatus
{
    public partial class UcChannelsStatus : UserControl, IUserControlMisc
    {
        public UcChannelsStatus()
        {
            InitializeComponent();
        }

        public int DisplayIndex { get; set; }

        public void Loaded()
        {
            ucTreeNavigator1.SelectNode("ndChannels");
            lock (Data.ChannelNodes)
            {
                foreach (var channel in Data.ChannelNodes)
                {
                    var flowLayoutPanelOneRow = new FlowLayoutPanel
                    {
                        //BackColor = Color.Aquamarine,
                        Margin = new Padding(0, 3, 0, 3),
                        FlowDirection = FlowDirection.LeftToRight,
                        AutoSize = true
                    };
                    var channelIndex = channel.Index + 1;

                    var checkBoxEnableChannel = new CheckBox
                    {
                        //BackColor = Color.Aqua,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Enabled = false,
                        Margin = new Padding(10, 3, 0, 0),
                        Name = "checkBox" + channel.Index,
                        Size = new Size(50, 28),
                        TabIndex = 1,
                        Text = channelIndex.ToString("0"),
                        Checked = channel.Active,
                        UseVisualStyleBackColor = true
                    };
                    checkBoxEnableChannel.CheckedChanged += checkBox1_CheckedChanged;
                    flowLayoutPanelOneRow.Controls.Add(checkBoxEnableChannel);

                    var linkLabel = new LinkLabel
                    {
                        //BackColor = Color.Azure,
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Name = "linkLabel" + channel.Index,
                        Size = new Size(120, 28),
                        TabIndex = 2,
                        TabStop = true,
                        Text = channel.Name,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    linkLabel.LinkClicked += linkLabel0_LinkClicked;
                    flowLayoutPanelOneRow.Controls.Add(linkLabel);

                    var labelType = new Label
                    {
                        //BackColor = Color.Aqua,
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Location = new Point(286, 40),
                        Name = "labelType" + channel.Index,
                        Size = new Size(120, 28),
                        TabIndex = 0,
                        Text = @"Modbus RTU",
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    flowLayoutPanelOneRow.Controls.Add(labelType);

                    var riserOneStateControl = new UcRiserOneState
                    {
                        //BackColor = Color.Azure,
                        Caption = "------",
                        CaptionAtRight = false,
                        Font = new Font("Courier New", 9.75F, FontStyle.Bold),
                        LampColorNone = Color.Gray,
                        LampColorOff = Color.Black,
                        LampColorOn = Color.Lime,
                        Margin = new Padding(20, 1, 0, 1),
                        Name = "riserOneStateControl" + channel.Index,
                        Size = new Size(130, 26),
                        State = null,
                        TabIndex = 3
                    };
                    flowLayoutPanelOneRow.Controls.Add(riserOneStateControl);

                    var labIpAddr = new Label
                    {
                        //BackColor = Color.Aqua,
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        Font = new Font("Lucida Console", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Name = "labIpAddr" + channel.Index,
                        Size = new Size(116, 28),
                        TabIndex = 5,
                        Text = channel.LinkType == 0 ? channel.IpAddr : "",
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    flowLayoutPanelOneRow.Controls.Add(labIpAddr);

                    var labComportTuned = new Label
                    {
                        //BackColor = Color.Aqua,
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        Font = new Font("Lucida Console", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Name = "labComportTuned" + channel.Index,
                        Size = new Size(116, 28),
                        TabIndex = 5,
                        Text = channel.LinkType == 1 ? 
                            string.Format("COM{0}:{1},{2}", channel.Comport, channel.Baudrate, channel.Parity) : "",
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    flowLayoutPanelOneRow.Controls.Add(labComportTuned);

                    var button = new Button
                    {
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Margin = new Padding(3, 1, 14, 1),
                        Name = "button" + channel.Index,
                        Size = new Size(141, 28),
                        TabIndex = 4,
                        Text = @"Контроллеры...",
                        FlatStyle = FlatStyle.System,
                        UseVisualStyleBackColor = true
                    };
                    button.Click += button0_Click;
                    flowLayoutPanelOneRow.Controls.Add(button);

                    var labDesc = new Label
                    {
                        //BackColor = Color.Azure,
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Margin = new Padding(3, 3, 3, 3),
                        Name = "labDesc" + channel.Index,
                        Size = new Size(353, 28),
                        TabIndex = 0,
                        Text = channel.Descriptor,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    flowLayoutPanelOneRow.Controls.Add(labDesc);
                    flowLayoutPanelOneRow.SetFlowBreak(labDesc, true);

                    flowLayoutPanel1.Controls.Add(flowLayoutPanelOneRow);
                }

            }
            timerUpdate_Tick(null, null);
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            lock (Data.ChannelNodes)
            {
                foreach (var channel in Data.ChannelNodes)
                {
                    var cntrls = Controls.Find("checkBox" + channel.Index, true);
                    if (cntrls.Length <= 0) continue;
                    var checkBox = cntrls[0] as CheckBox;
                    if (checkBox == null) continue;
                    try
                    {
                        checkBox.CheckedChanged -= checkBox1_CheckedChanged;
                        checkBox.Checked = channel.Active;
                        checkBox.Enabled = Data.UserLevel >= UserLevel.Eng;
                    }
                    finally
                    {
                        checkBox.CheckedChanged += checkBox1_CheckedChanged;
                    }                  
                    cntrls = Controls.Find("riserOneStateControl" + channel.Index, true);
                    if (cntrls.Length <= 0) continue;
                    var lampBox = cntrls[0] as UcRiserOneState;
                    if (lampBox == null) continue;
                    if (channel.Active)
                    {
                        if (channel.BarometerValue >= channel.FailLimit)
                        {
                            lampBox.State = true;
                            lampBox.LampColorOn = Color.Red;
                            lampBox.Caption = "Отказ";
                        }
                        else if (channel.BarometerValue >= channel.MarginalLimit)
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
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Включение в работу канала связи",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Eng)
            {
                var checkbox = (CheckBox) sender;
                int index;
                if (!int.TryParse(checkbox.Name.Substring(8), out index)) return;
                ChannelNode channel;
                lock (Data.ChannelNodes)
                {
                    if (index >= Data.ChannelNodes.Count) return;
                    channel = Data.ChannelNodes[index];
                    if (checkbox.Checked)
                    {
                        channel.BarometerValue = 0;
                        channel.TotalRequests = 0;
                        channel.TotalErrors = 0;
                        channel.NextFetching = DateTime.Now + new TimeSpan(channel.FetchTime*TimeSpan.TicksPerSecond);
                        channel.Active = true;
                    }
                    else
                    {
                        channel.Active = false;
                    }

                    var channelName = string.Format("Channel{0}", channel.Index + 1);
                    Data.Config.WriteBool("FetchChannels", channelName, checkbox.Checked);
                    Data.Config.UpdateFile();
                    var val = checkbox.Checked;
                    var idx = channel.Descriptor.IndexOf("Стояки", StringComparison.Ordinal);
                    if (idx < 0) idx = 0;
                    Data.SendToChangeLog(channelName,
                                         new RiserAddress
                                             {
                                                 Channel = channel.Index + 1,
                                                 Overpass = channel.Overpass,
                                                 Way = channel.Way,
                                                 Product = channel.Product
                                             },
                                         "Active", !val ? "1" : "0", val ? "1" : "0", Data.UserName,
                                         "Опрос канала (" + channel.Descriptor.Substring(idx) + ")");
                    var cntrls = Controls.Find("riserOneStateControl" + channel.Index, true);
                    if (cntrls.Length <= 0) return;
                    var lampBox = cntrls[0] as UcRiserOneState;
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
                lock (Data.RiserNodes)
                {
                    foreach (var riserNode in Data.RiserNodes.Values
                                                  .Where(item => item.Channel == channel.Index)
                                                  .Where(riserNode => checkbox.Checked && riserNode.Active))
                    {
                        riserNode.BarometerValue = 0;
                        riserNode.TotalRequests = 0;
                        riserNode.TotalErrors = 0;
                    }
                }
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Включение в работу канала связи",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void linkLabel0_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var link = (LinkLabel) sender;
            int index;
            if (!int.TryParse(link.Name.Substring(9), out index)) return;
            var parent = Parent;
            while (parent != null)
            {
                if (parent is Form)
                {
                    Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "ChannelIndex", index);
                    Data.Navigate(GetType(), new ChannelsStatusArgs
                        {
                            Panel = parent,
                            WhatShow = ChannelShowAs.OneChannelPage
                        });
                    break;
                }
                parent = parent.Parent;
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            var button = (Button) sender;
            int index;
            if (!int.TryParse(button.Name.Substring(6), out index)) return;
            var parent = Parent;
            while (parent != null)
            {
                if (parent is Form)
                {
                    Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "ChannelIndex", index); 
                    Data.Navigate(GetType(), new ChannelsStatusArgs
                    {
                        Panel = parent,
                        WhatShow = ChannelShowAs.ChannelControllersPage
                    });
                    break;
                }
                parent = parent.Parent;
            }
        }

        public void Unload()
        {
            timerUpdate.Tick -= timerUpdate_Tick;
        }
    }
}
