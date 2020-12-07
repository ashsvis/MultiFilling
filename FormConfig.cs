using System;
using System.Net;
using System.Windows.Forms;

namespace MultiFilling
{
    public partial class FormConfig : Form
    {
        private bool _lastDisableTaskManagerState;
        private int _lastWorklogMessagesCount;
        private int _lastMonitors;

        public FormConfig()
        {
            InitializeComponent();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            try
            {
                cbSystemShell.Checked = Data.Config.ReadBool("GeneralFor" + Environment.UserName, "SystemShell", false);
                cbDisableTaskManager.Checked =
                    _lastDisableTaskManagerState =
                    Data.Config.ReadBool("GeneralFor" + Environment.UserName, "DisableTaskMgr", false);
                nudStartTaskPeriod.Value = Data.Config.ReadInteger("General", "StartTaskPeriod", 3000);
                nudStopTaskPeriod.Value = Data.Config.ReadInteger("General", "StopTaskPeriod", 3000);
                nudLogViewMessagesCount.Value =
                    _lastWorklogMessagesCount = Data.Config.ReadInteger("General", "LogMessagesCount", 500);
                сbShowReadyAndAlarm.Checked = Data.Config.ReadBool("FillingPageCommon", "ShowReadyAndAlarm", false);
                cbUseSmartLevel.Checked = Data.Config.ReadBool("FillingPageCommon", "UseSmartLevel", true);
                nudSmartLevelDifferent.Value =Data.Config.ReadInteger("FillingPageCommon", "SmartLevelDifferent", 3);
                nudSmartLevelQueueSize.Value = Data.Config.ReadInteger("FillingPageCommon", "SmartLevelQueueSize", 15);
                nudDeleteLogsAfter.Value = Data.Config.ReadInteger("General", "DeleteLogsAfter", 90);
                nudDeleteTrendsAfter.Value = Data.Config.ReadInteger("General", "DeleteTrendsAfter", 14);
                nudMonitors.Value = _lastMonitors = Data.Config.ReadInteger("Station", "Monitors", 1);

                cbEnableLocalEventServer.Checked = Data.Config.ReadBool("DataEventServers", "EnableLocalEventServer", false);

                cbEventServer1.Checked = Data.Config.ReadBool("DataEventServers", "EnableEventServer1", false);
                tbEventServer1.Text = Data.Config.ReadString("DataEventServers", "AddressEventServer1", "");
                cbEventServer2.Checked = Data.Config.ReadBool("DataEventServers", "EnableEventServer2", false);
                tbEventServer2.Text = Data.Config.ReadString("DataEventServers", "AddressEventServer2", "");
                cbEventServer3.Checked = Data.Config.ReadBool("DataEventServers", "EnableEventServer3", false);
                tbEventServer3.Text = Data.Config.ReadString("DataEventServers", "AddressEventServer3", "");
                cbEventServer4.Checked = Data.Config.ReadBool("DataEventServers", "EnableEventServer4", false);
                tbEventServer4.Text = Data.Config.ReadString("DataEventServers", "AddressEventServer4", "");
                
                tbSelfIpAddress.Text = Data.Config.ReadString("DataEventServers", "SelfIpAddress", "");

            }
            catch (Exception ex)
            {
                Data.SendToErrorsLog("Ошибка при загрузке конфигурации в окно редактирования: " + ex.FullMessage());
            }
            btnApply.Enabled = false;
        }

        private void DataChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string errormsg;
            btnApply.Enabled = false;
            if (nudMonitors.Value != _lastMonitors)
            {
                Data.Config.WriteInteger("Station", "Monitors", Convert.ToInt32(nudMonitors.Value));
                Data.Config.UpdateFile();
                Data.SendToChangeLog("Station", new RiserAddress(), "Monitors",
                    _lastMonitors.ToString("0"), nudMonitors.Value.ToString("0"),
                    Data.UserName, "Количество мониторов станции");
                _lastMonitors = Convert.ToInt32(nudMonitors.Value);
            }
            if (cbUseSmartLevel.Checked != Data.UseSmartLevel)
            {
                Data.Config.WriteBool("FillingPageCommon", "UseSmartLevel", cbUseSmartLevel.Checked);
                Data.Config.UpdateFile();
                Data.SendToChangeLog("FillingPage", new RiserAddress(), "UseSmartLevel",
                    !cbUseSmartLevel.Checked ? "1" : "0", cbUseSmartLevel.Checked ? "1" : "0",
                    Data.UserName, "Обнулять показания уровня, если долго нет изменений значения");
                Data.UseSmartLevel = cbUseSmartLevel.Checked;
            }
            if (nudSmartLevelDifferent.Value != Data.SmartLevelDifferent)
            {
                Data.Config.WriteInteger("FillingPageCommon", "SmartLevelDifferent", Convert.ToInt32(nudSmartLevelDifferent.Value));
                Data.Config.UpdateFile();
                Data.SendToChangeLog("FillingPage", new RiserAddress(), "SmartLevelDifferent",
                    Data.SmartLevelDifferent.ToString("0"), nudSmartLevelDifferent.Value.ToString("0"),
                    Data.UserName, "Изменение показаний более чем на (мм)");
                Data.SmartLevelDifferent = Convert.ToInt32(nudSmartLevelDifferent.Value);
            }
            if (nudSmartLevelQueueSize.Value != Data.SmartLevelQueueSize)
            {
                Data.Config.WriteInteger("FillingPageCommon", "SmartLevelQueueSize", Convert.ToInt32(nudSmartLevelQueueSize.Value));
                Data.Config.UpdateFile();
                Data.SendToChangeLog("FillingPage", new RiserAddress(), "SmartLevelQueueSize",
                    Data.SmartLevelQueueSize.ToString("0"), nudSmartLevelQueueSize.Value.ToString("0"),
                    Data.UserName, "Использовать усреднение для количества отсчетов");
                Data.SmartLevelQueueSize = Convert.ToInt32(nudSmartLevelQueueSize.Value);
            }
            if (сbShowReadyAndAlarm.Checked != Data.ShowReadyAndAlarm)
            {
                Data.Config.WriteBool("FillingPageCommon", "ShowReadyAndAlarm", сbShowReadyAndAlarm.Checked);
                Data.Config.UpdateFile();
                Data.SendToChangeLog("FillingPage", new RiserAddress(), "ShowReadyAndAlarm",
                    !сbShowReadyAndAlarm.Checked ? "1" : "0", сbShowReadyAndAlarm.Checked ? "1" : "0",
                    Data.UserName, "Показывать вместе \"Готовность\" и \"Аварийный уровень\" на диаграмме стояка");
                Data.ShowReadyAndAlarm = сbShowReadyAndAlarm.Checked;
            }
            if (nudLogViewMessagesCount.Value != _lastWorklogMessagesCount)
            {
                Data.Config.WriteInteger("General", "LogMessagesCount", Convert.ToInt32(nudLogViewMessagesCount.Value));
                Data.Config.UpdateFile();
                Data.SendToChangeLog("General", new RiserAddress(), "LogMessagesCount",
                    _lastWorklogMessagesCount.ToString("0"), nudLogViewMessagesCount.Value.ToString("0"),
                    Data.UserName, "Количество строк в окне лога");
                _lastWorklogMessagesCount = Convert.ToInt32(nudLogViewMessagesCount.Value);
            }
            if (nudStartTaskPeriod.Value != Data.StartTaskPeriod)
            {
                Data.Config.WriteInteger("General", "StartTaskPeriod", Convert.ToInt32(nudStartTaskPeriod.Value));
                Data.Config.UpdateFile();
                Data.SendToChangeLog("General", new RiserAddress(), "StartTaskPeriod",
                    Data.StartTaskPeriod.ToString("0"), nudStartTaskPeriod.Value.ToString("0"), 
                    Data.UserName, "Период запуска налива стояков при групповом старте");
                Data.StartTaskPeriod = Convert.ToInt32(nudStartTaskPeriod.Value);
            }
            if (nudStopTaskPeriod.Value != Data.StopTaskPeriod)
            {
                Data.Config.WriteInteger("General", "StopTaskPeriod", Convert.ToInt32(nudStopTaskPeriod.Value));
                Data.Config.UpdateFile();
                Data.SendToChangeLog("General", new RiserAddress(), "StopTaskPeriod",
                    Data.StopTaskPeriod.ToString("0"), nudStopTaskPeriod.Value.ToString("0"),
                    Data.UserName, "Период останова налива стояков при групповом останове");
                Data.StopTaskPeriod = Convert.ToInt32(nudStopTaskPeriod.Value);
            }
            if (nudDeleteLogsAfter.Value != Data.DeleteLogsAfter)
            {
                Data.Config.WriteInteger("General", "DeleteLogsAfter", Convert.ToInt32(nudDeleteLogsAfter.Value));
                Data.Config.UpdateFile();
                Data.SendToChangeLog("General", new RiserAddress(), "DeleteLogsAfter",
                    Data.DeleteLogsAfter.ToString("0"), nudDeleteLogsAfter.Value.ToString("0"),
                    Data.UserName, "Количество дней перед удалением старых записей журналов");
                Data.DeleteLogsAfter = Convert.ToInt32(nudDeleteLogsAfter.Value);
            }
            if (nudDeleteTrendsAfter.Value != Data.DeleteTrendsAfter)
            {
                Data.Config.WriteInteger("General", "DeleteTrendsAfter", Convert.ToInt32(nudDeleteTrendsAfter.Value));
                Data.Config.UpdateFile();
                Data.SendToChangeLog("General", new RiserAddress(), "DeleteTrendsAfter",
                    Data.DeleteTrendsAfter.ToString("0"), nudDeleteTrendsAfter.Value.ToString("0"),
                    Data.UserName, "Количество дней перед удалением старых записей трендов");
                Data.DeleteTrendsAfter = Convert.ToInt32(nudDeleteTrendsAfter.Value);
            }

            if (cbEnableLocalEventServer.Checked != Data.EnableLocalEventServer)
            {
                Data.Config.WriteBool("DataEventServers", "EnableLocalEventServer", cbEnableLocalEventServer.Checked);
                Data.Config.UpdateFile();
                Data.SendToChangeLog("DataEventServers", new RiserAddress(), "EnableLocalEventServer",
                    !cbEnableLocalEventServer.Checked ? "1" : "0", cbEnableLocalEventServer.Checked ? "1" : "0",
                    Data.UserName, "Отдавать данные другим подписчикам");
                Data.EnableLocalEventServer = cbEnableLocalEventServer.Checked;
            }
            
            if (tbSelfIpAddress.Text != Data.SelfIpAddress)
            {
                IPAddress address;
                if (IPAddress.TryParse(tbSelfIpAddress.Text, out address))
                {
                    Data.Config.WriteString("DataEventServers", "SelfIpAddress", tbSelfIpAddress.Text);
                    Data.Config.UpdateFile();
                    Data.SendToChangeLog("DataEventServers", new RiserAddress(), "SelfIpAddress",
                                         Data.SelfIpAddress, tbSelfIpAddress.Text,
                                         Data.UserName, "IP-адрес сервера локальной машины");
                    Data.SelfIpAddress = tbSelfIpAddress.Text;
                }
                else
                {
                    MessageBox.Show(this, @"Ошибка при вводе IP-адреса для сервера локальной машины",
                                    @"Настройка серверов подписки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbSelfIpAddress.Text = Data.SelfIpAddress;
                }
            }

            if (cbEventServer1.Checked != Data.StationNodes[0].Enable)
            {
                Data.Config.WriteBool("DataEventServers", "EnableEventServer1", cbEventServer1.Checked);
                Data.Config.UpdateFile();
                Data.SendToChangeLog("DataEventServers", new RiserAddress(), "EnableEventServer1",
                    !cbEventServer1.Checked ? "1" : "0", cbEventServer1.Checked ? "1" : "0",
                    Data.UserName, "Разрешение подписки на сервер событий 1");
                Data.StationNodes[0].Enable = cbEventServer1.Checked;
            }
            
            var ipaddr = Data.StationNodes[0].Address != null ? Data.StationNodes[0].Address.ToString() : "";
            if (cbEventServer1.Checked && tbEventServer1.Text != ipaddr)
            {
                IPAddress address;
                if (IPAddress.TryParse(tbEventServer1.Text, out address))
                {
                    Data.Config.WriteString("DataEventServers", "AddressEventServer1", tbEventServer1.Text);
                    Data.Config.UpdateFile();
                    Data.SendToChangeLog("DataEventServers", new RiserAddress(), "AddressEventServer1",
                                         ipaddr, tbEventServer1.Text,
                                         Data.UserName, "IP-адрес сервера событий 1");
                    Data.StationNodes[0].Address = address;
                }
                else
                {
                    MessageBox.Show(this, @"Ошибка при вводе IP-адреса для сервера подписки 1",
                                    @"Настройка серверов подписки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbEventServer1.Text = ipaddr;
                }
            }

            if (cbEventServer2.Checked != Data.StationNodes[1].Enable)
            {
                Data.Config.WriteBool("DataEventServers", "EnableEventServer2", cbEventServer2.Checked);
                Data.Config.UpdateFile();
                Data.SendToChangeLog("DataEventServers", new RiserAddress(), "EnableEventServer2",
                    !cbEventServer2.Checked ? "1" : "0", cbEventServer2.Checked ? "1" : "0",
                    Data.UserName, "Разрешение подписки на сервер событий 2");
                Data.StationNodes[1].Enable = cbEventServer2.Checked;
            }
            ipaddr = Data.StationNodes[1].Address != null ? Data.StationNodes[1].Address.ToString() : "";
            if (cbEventServer2.Checked && tbEventServer2.Text != ipaddr)
            {
                IPAddress address;
                if (IPAddress.TryParse(tbEventServer2.Text, out address))
                {
                    Data.Config.WriteString("DataEventServers", "AddressEventServer2", tbEventServer2.Text);
                    Data.Config.UpdateFile();
                    Data.SendToChangeLog("DataEventServers", new RiserAddress(), "AddressEventServer2",
                                         ipaddr, tbEventServer2.Text,
                                         Data.UserName, "IP-адрес сервера событий 2");
                    Data.StationNodes[1].Address = address;
                }
                else
                {
                    MessageBox.Show(this, @"Ошибка при вводе IP-адреса для сервера подписки 2",
                                    @"Настройка серверов подписки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbEventServer2.Text = ipaddr;
                }
            }

            if (cbEventServer3.Checked != Data.StationNodes[2].Enable)
            {
                Data.Config.WriteBool("DataEventServers", "EnableEventServer3", cbEventServer3.Checked);
                Data.Config.UpdateFile();
                Data.SendToChangeLog("DataEventServers", new RiserAddress(), "EnableEventServer3",
                    !cbEventServer3.Checked ? "1" : "0", cbEventServer3.Checked ? "1" : "0",
                    Data.UserName, "Разрешение подписки на сервер событий 3");
                Data.StationNodes[2].Enable = cbEventServer3.Checked;
            }
            ipaddr = Data.StationNodes[2].Address != null ? Data.StationNodes[2].Address.ToString() : "";
            if (cbEventServer3.Checked && tbEventServer3.Text != ipaddr)
            {
                IPAddress address;
                if (IPAddress.TryParse(tbEventServer3.Text, out address))
                {
                    Data.Config.WriteString("DataEventServers", "AddressEventServer3", tbEventServer3.Text);
                    Data.Config.UpdateFile();
                    Data.SendToChangeLog("DataEventServers", new RiserAddress(), "AddressEventServer3",
                                         ipaddr, tbEventServer3.Text,
                                         Data.UserName, "IP-адрес сервера событий 3");
                    Data.StationNodes[2].Address = address;
                }
                else
                {
                    MessageBox.Show(this, @"Ошибка при вводе IP-адреса для сервера подписки 3",
                        @"Настройка серверов подписки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbEventServer3.Text = ipaddr;
                }
            }
            if (cbEventServer4.Checked != Data.StationNodes[3].Enable)
            {
                Data.Config.WriteBool("DataEventServers", "EnableEventServer4", cbEventServer4.Checked);
                Data.Config.UpdateFile();
                Data.SendToChangeLog("DataEventServers", new RiserAddress(), "EnableEventServer4",
                    !cbEventServer4.Checked ? "1" : "0", cbEventServer4.Checked ? "1" : "0",
                    Data.UserName, "Разрешение подписки на сервер событий 4");
                Data.StationNodes[3].Enable = cbEventServer4.Checked;
            }
            ipaddr = Data.StationNodes[3].Address != null ? Data.StationNodes[3].Address.ToString() : "";
            if (cbEventServer4.Checked && tbEventServer4.Text != ipaddr)
            {
                IPAddress address;
                if (IPAddress.TryParse(tbEventServer4.Text, out address))
                {
                    Data.Config.WriteString("DataEventServers", "AddressEventServer4", tbEventServer4.Text);
                    Data.Config.UpdateFile();
                    Data.SendToChangeLog("DataEventServers", new RiserAddress(), "AddressEventServer4",
                                         ipaddr, tbEventServer4.Text,
                                         Data.UserName, "IP-адрес сервера событий 4");
                    Data.StationNodes[3].Address = address;
                }
                else
                {
                    MessageBox.Show(this, @"Ошибка при вводе IP-адреса для сервера подписки 4",
                                    @"Настройка серверов подписки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbEventServer4.Text = ipaddr;
                }
            }

            if (cbDisableTaskManager.Checked != _lastDisableTaskManagerState)
            {
                if (!Data.DisableTaskManager(cbDisableTaskManager.Checked, out errormsg))
                {
                    cbDisableTaskManager.Checked = _lastDisableTaskManagerState;
                    MessageBox.Show(this, string.Concat(errormsg, Environment.NewLine,
                                                        @"Попробуйте запустить программу от имени администратора."),
                                    @"Настройка запрета вызова диспетчера задач",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Data.Config.WriteBool("GeneralFor" + Environment.UserName, "DisableTaskMgr", cbDisableTaskManager.Checked);
                    Data.Config.UpdateFile();
                    _lastDisableTaskManagerState = cbDisableTaskManager.Checked;
                }
            }
            if (Data.SystemShell != cbSystemShell.Checked)
            {
                if (!Data.SetShellMode(cbSystemShell.Checked, out errormsg))
                {
                    cbSystemShell.Checked = Data.SystemShell;
                    MessageBox.Show(this, string.Concat(errormsg, Environment.NewLine,
                                                        @"Попробуйте запустить программу от имени администратора."),
                                    @"Настройка режима оболочки системы",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Data.SystemShell = cbSystemShell.Checked;
                    Data.MustWinLogOff = !Data.SystemShell;
                    Data.Config.WriteBool("GeneralFor" + Environment.UserName, "SystemShell", cbSystemShell.Checked);
                    Data.Config.UpdateFile();
                }
            }
        }

        private void cbEventServer1_CheckedChanged(object sender, EventArgs e)
        {
            tbEventServer1.Enabled = cbEventServer1.Checked;
        }

        private void cbEventServer2_CheckedChanged(object sender, EventArgs e)
        {
            tbEventServer2.Enabled = cbEventServer2.Checked;
        }

        private void cbEventServer3_CheckedChanged(object sender, EventArgs e)
        {
            tbEventServer3.Enabled = cbEventServer3.Checked;
        }

        private void cbEventServer4_CheckedChanged(object sender, EventArgs e)
        {
            tbEventServer4.Enabled = cbEventServer4.Checked;
        }
    }
}
