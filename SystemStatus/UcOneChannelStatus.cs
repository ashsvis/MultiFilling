using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace MultiFilling.SystemStatus
{
    public partial class UcOneChannelStatus : UserControl, IUserControlMisc
    {
        public int ChannelIndex
        {
            get { return cbChannelByName.SelectedIndex; }
            set
            {
                if (value >= 0 && value < cbChannelByName.Items.Count)
                {
                    cbChannelByName.SelectedIndex = value;
                }
                else if (cbChannelByName.Items.Count > 0)
                    cbChannelByName.SelectedIndex = 0;
            }
        }

        public int DisplayIndex { get; set; }


        public UcOneChannelStatus()
        {
            InitializeComponent();
        }

        public void Loaded()
        {
            ucTreeNavigator1.SelectNode("ndChannels");
            lock (Data.ChannelNodes)
            {
                nudChannelByIndex.Maximum = Data.ChannelNodes.Count;
                var list = new List<string>();
                foreach (var channel in Data.ChannelNodes.OrderBy(item => item.Name)
                    .Where(channel => !list.Contains(channel.Name)))
                {
                    list.Add(channel.Name);
                    cbChannelByName.Items.Add(channel);
                }
            }
            ChannelIndex = Data.Session.ReadInteger("SystemStatus" + DisplayIndex, "ChannelIndex", -1);
            cbChannelByName.SelectedIndex = ChannelIndex;
            nudChannelByIndex.Value = ChannelIndex + 1;
            nudChannelByIndex.ValueChanged += nudChannelByIndex_ValueChanged;
            cbChannelByName.SelectedIndexChanged += cbChannelByName_SelectedIndexChanged;
            checkBoxActive.CheckedChanged += checkBoxActive_CheckedChanged;
            timerUpdate_Tick(null, null);
        }

        private void checkBoxActive_CheckedChanged(object sender, EventArgs e)
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Включение в работу канала связи",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Eng)
            {
                var checkbox = (CheckBox) sender;
                var index = ChannelIndex;
                lock (Data.ChannelNodes)
                {
                    if (index >= Data.ChannelNodes.Count) return;
                    var channel = Data.ChannelNodes[index];
                    if (checkbox.Checked)
                    {
                        channel.BarometerValue = 0;
                        channel.TotalRequests = 0;
                        channel.TotalErrors = 0;
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
                                @"Включение в работу канала связи",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void cbChannelByName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                nudChannelByIndex.ValueChanged -= nudChannelByIndex_ValueChanged;
                var channel = (ChannelNode) cbChannelByName.SelectedItem;
                nudChannelByIndex.Value = channel.Index + 1;
                Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "ChannelIndex", channel.Index);
            }
            finally
            {
                nudChannelByIndex.ValueChanged += nudChannelByIndex_ValueChanged;                
            }
        }

        void nudChannelByIndex_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                cbChannelByName.SelectedIndexChanged -= cbChannelByName_SelectedIndexChanged;
                cbChannelByName.SelectedIndex = Convert.ToInt32(nudChannelByIndex.Value) - 1;
                Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "ChannelIndex", cbChannelByName.SelectedIndex);
            }
            finally
            {
                cbChannelByName.SelectedIndexChanged += cbChannelByName_SelectedIndexChanged;                
            }
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (ChannelIndex < 0) return;
            lock (Data.ChannelNodes)
            {
                var channel = Data.ChannelNodes[ChannelIndex];
                try
                {
                    checkBoxActive.CheckedChanged -= checkBoxActive_CheckedChanged;
                    checkBoxActive.Checked = channel.Active;
                    checkBoxActive.Enabled = Data.UserLevel >= UserLevel.Eng;
                    buttonFetch.Enabled = channel.Active;
                }
                finally
                {
                    checkBoxActive.CheckedChanged += checkBoxActive_CheckedChanged;
                }
                var lampBox = riserOneStateControl1;
                if (channel.Active)
                {
                    if (channel.BarometerValue >= channel.FailLimit)
                    {
                        lampBox.State = true;
                        lampBox.LampColorOn = Color.Red;
                        lampBox.Caption = "Отказ";
                        lblFetchTime.Text = channel.TimeFail.TotalSeconds.ToString("0");
                    }
                    else if (channel.BarometerValue >= channel.MarginalLimit)
                    {
                        lampBox.State = true;
                        lampBox.LampColorOn = Color.Yellow;
                        lampBox.Caption = "Сбой";
                        lblFetchTime.Text = channel.TimeMarginal.TotalSeconds.ToString("0");
                    }
                    else
                    {
                        lampBox.State = true;
                        lampBox.LampColorOn = Color.Lime;
                        lampBox.Caption = "Норма";
                        lblFetchTime.Text = channel.FetchTime.ToString("0");
                    }
                }
                else
                {
                    lampBox.State = false;
                    lampBox.Caption = "Не активен";
                }
                lblTotalRequests.Text = channel.TotalRequests.ToString("0");
                lblTotalErrors.Text = channel.TotalErrors.ToString("0");
                if (channel.TotalRequests > 0)
                {
                    var perc = Math.Round(channel.TotalErrors*100.0/channel.TotalRequests, 3);
                    var value = perc.ToString("G", CultureInfo.GetCultureInfo("en-US"));
                    lblErrorPercent.Text = value.Substring(0, Math.Min(6, value.Length));
                }
                else
                    lblErrorPercent.Text = @".00000";
                lblBarometerValue.Text = channel.BarometerValue.ToString("0");
                lblMarginalLimit.Text = channel.MarginalLimit.ToString("0");
                lblFailLimit.Text = channel.FailLimit.ToString("0");
                lblMoxaIp.Text = channel.IpAddr;
                lblDescriptor.Text = channel.Descriptor;
                lblSentTimeout.Text = channel.SendTimeout.ToString("0");
                lblReceiveTimeout.Text = channel.ReceiveTimeout.ToString("0");
            }
        }

        public void Unload()
        {
            timerUpdate.Tick -= timerUpdate_Tick;
            nudChannelByIndex.ValueChanged -= nudChannelByIndex_ValueChanged;
            cbChannelByName.SelectedIndexChanged -= cbChannelByName_SelectedIndexChanged;
            checkBoxActive.CheckedChanged -= checkBoxActive_CheckedChanged;
        }
    }
}
