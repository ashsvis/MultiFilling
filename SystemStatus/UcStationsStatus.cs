using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MultiFilling.EventClient;

namespace MultiFilling.SystemStatus
{
    public partial class UcStationsStatus : UserControl, IUserControlMisc
    {
        public UcStationsStatus()
        {
            InitializeComponent();
        }

        public int DisplayIndex { get; set; }

        public void Loaded()
        {
            ucTreeNavigator1.SelectNode("ndStations");
            lock (Data.StationNodes)
            {
                foreach (var station in Data.StationNodes)
                {
                    var flowLayoutPanelOneRow = new FlowLayoutPanel
                    {
                        //BackColor = Color.Aquamarine,
                        Margin = new Padding(0, 3, 0, 3),
                        FlowDirection = FlowDirection.LeftToRight,
                        AutoSize = true
                    };
                    var stationIndex = station.Index;

                    var checkBoxEnableChannel = new CheckBox
                    {
                        //BackColor = Color.Aqua,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Enabled = false,
                        Margin = new Padding(10, 3, 0, 0),
                        Name = "checkBox" + station.Index,
                        Size = new Size(50, 28),
                        TabIndex = 1,
                        Text = stationIndex.ToString("0"),
                        Checked = station.Enable,
                        UseVisualStyleBackColor = true
                    };
                    flowLayoutPanelOneRow.Controls.Add(checkBoxEnableChannel);

                    var linkLabel = new LinkLabel
                    {
                        //BackColor = Color.Azure,
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Name = "linkLabel" + station.Index,
                        Size = new Size(120, 28),
                        TabIndex = 2,
                        TabStop = true,
                        Text = station.Name,
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
                        Name = "labelType" + station.Index,
                        Size = new Size(120, 28),
                        TabIndex = 0,
                        Text = station.ItThisStation ? "Публикация" : "Подписка",
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    flowLayoutPanelOneRow.Controls.Add(labelType);

                    var riserOneStateControl = new UcRiserOneState
                    {
                        //BackColor = Color.Azure,
                        Caption = "------",
                        CaptionAtRight = false,
                        Font = new Font("Courier New", 9.75F, FontStyle.Bold),
                        LampColorNone = Color.Gray,
                        LampColorOff = Color.Silver,
                        LampColorOn = Color.Lime,
                        Margin = new Padding(20, 1, 0, 1),
                        Name = "riserOneStateControl" + station.Index,
                        Size = new Size(145, 26),
                        State = null,
                        TabIndex = 3
                    };
                    flowLayoutPanelOneRow.Controls.Add(riserOneStateControl);

                    var stationIpAddr = station.Address != null ? station.Address.ToString() : "";

                    var labIpAddr = new Label
                    {
                        //BackColor = Color.Aqua,
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        Font = new Font("Lucida Console", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Name = "labIpAddr" + station.Index,
                        Size = new Size(126, 28),
                        TabIndex = 5,
                        Text = stationIpAddr,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    flowLayoutPanelOneRow.Controls.Add(labIpAddr);

                    var labDesc = new Label
                    {
                        //BackColor = Color.Azure,
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        Margin = new Padding(20, 3, 3, 3),
                        Name = "labDesc" + station.Index,
                        Size = new Size(353, 28),
                        TabIndex = 0,
                        Text = station.Descriptor,
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
            lock (Data.StationNodes)
            {
                foreach (var station in Data.StationNodes)
                {
                    var cntrls = Controls.Find("checkBox" + station.Index, true);
                    if (cntrls.Length <= 0) continue;
                    var checkBox = cntrls[0] as CheckBox;
                    if (checkBox == null) continue;
                    try
                    {
                        //checkBox.CheckedChanged -= checkBox1_CheckedChanged;
                        checkBox.Checked = station.Enable;
                        checkBox.Enabled = Data.UserLevel >= UserLevel.Eng;
                    }
                    finally
                    {
                        //checkBox.CheckedChanged += checkBox1_CheckedChanged;
                    }                  
                    cntrls = Controls.Find("riserOneStateControl" + station.Index, true);
                    if (cntrls.Length <= 0) continue;
                    var lampBox = cntrls[0] as UcRiserOneState;
                    if (lampBox == null) continue;
                    if (station.Enable)
                    {
                        switch (station.ConnectionStatus)
                        {
                            case ClientConnectionStatus.Faulted:
                                    lampBox.State = true;
                                    lampBox.LampColorOn = Color.Red;
                                    lampBox.Caption = "Ошибка";
                                break;
                            case ClientConnectionStatus.Opening:
                                    lampBox.State = true;
                                    lampBox.LampColorOn = Color.Gold;
                                    lampBox.Caption = "Подключение...";
                                break;
                            case ClientConnectionStatus.Opened:
                                    lampBox.State = true;
                                    lampBox.LampColorOn = Color.Lime;
                                    lampBox.Caption = "Норма";
                                break;
                            case ClientConnectionStatus.Closed:
                                lampBox.State = true;
                                lampBox.LampColorOn = Color.Black;
                                lampBox.Caption = "Отключено";
                                break;
                        }
                    }
                    else
                    {
                        lampBox.State = false;
                        lampBox.Caption = "Отключено";
                    }
                }
            }
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
                    Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "StationIndex", index);
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

        public void Unload()
        {
            timerUpdate.Tick -= timerUpdate_Tick;
        }
    }
}
