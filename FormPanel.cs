using System;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;
using MultiFilling.RiserTuning;
using MultiFilling.SystemStatus;
using MultiFilling.TypesList;
using MultiFilling.WaggonsList;
using MultiFilling.UserList;

namespace MultiFilling
{
    public partial class FormPanel : Form
    {
        private Rectangle _workArea;
        private FormMain Host { get; set; }
        public bool Primary { get; private set; }

        public int DisplayIndex { get; set; }

        public string ErrorMessage {
            set
            {
                var method = new MethodInvoker(() =>
                {
                    lbErrorMessage.Text = value;
                    statusStripAlarm.Invalidate();
                });
                if (InvokeRequired)
                    BeginInvoke(method);
                else
                    method();
            }
        }

        public string StatusMessage
        {
            set
            {
                var method = new MethodInvoker(() =>
                {
                    lbStatusMessage.Text = value;
                    statusStripMessage.Invalidate();
                });
                if (InvokeRequired)
                    BeginInvoke(method);
                else
                    method();
            }
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public FormPanel(FormMain form, bool primary, Rectangle rect)
        {
            InitializeComponent();
            Host = form;
            _workArea = rect;
            Primary = primary;
        }

        private void FormPanel_Load(object sender, EventArgs e)
        {
            Left = _workArea.X;
            Top = _workArea.Y;
            Width = _workArea.Width;
            Height = _workArea.Height;

            Data.OnNavigate += Data_OnNavigate;

            CallFillingPage();
        }

        private void CallFillingPage()
        {
            Data.Navigate(typeof(UcFillingPage), new FillingPageArgs { Panel = this });
        }

        private RiserAddress? _riserAddressSelected;

        private void panelPlace_ControlRemoved(object sender, ControlEventArgs e)
        {
            var fp = e.Control as UcFillingPage;
            if (fp != null) fp.OnRiserSelected -= fillingPage_OnRiserSelected;
            _riserAddressSelected = null;
            if (_formRiserStatus != null)
                _formRiserStatus.Close();
            if (_formRiserTuning != null)
                _formRiserTuning.Close();
        }

        void fillingPage_OnRiserSelected(RiserAddress? addr)
        {
            _riserAddressSelected = addr;
            if (_formRiserStatus != null)
            {
                if (addr != null)
                    _formRiserStatus.RiserAddress = addr;
                else
                    _formRiserStatus.Close();
            }
            if (_formRiserTuning != null)
            {
                if (addr != null)
                    _formRiserTuning.RiserAddress = addr;
                else
                    _formRiserTuning.Close();
            }
        }

        private void Data_OnNavigate(Type senderType, NavigateArgs args)
        {
            if (this != args.Panel) return;
            if (senderType == typeof(UcChannelsStatus))
            {
                var channelArgs = (ChannelsStatusArgs) args;
                switch (channelArgs.WhatShow)
                {
                    case ChannelShowAs.OneChannelPage:
                        CreateAndPlaceToPage(typeof(UcOneChannelStatus));
                        break;
                    case ChannelShowAs.ChannelControllersPage:
                        CreateAndPlaceToPage(typeof(UcControllersStatus));
                        break;
                }
            }
            else if (senderType == typeof(UcControllersStatus))
            {
                var controllerArgs = (ControllersStatusArgs)args;
                switch (controllerArgs.WhatShow)
                {
                    case ControllerShowAs.OneChannelPage:
                        CreateAndPlaceToPage(typeof(UcOneChannelStatus));
                        break;
                    case ControllerShowAs.OneControllerPage:
                        CreateAndPlaceToPage(typeof(UcOneControllerStatus));
                        break;
                }
            }
            else if (senderType == typeof(UcTreeNavigator))
            {
                var navigatorArgs = (NavigateTreeArgs) args;
                switch (navigatorArgs.NodeName)
                {
                    case "ndStations":
                        CreateAndPlaceToPage(typeof(UcStationsStatus));
                        break;
                    case "ndChannels":
                        CreateAndPlaceToPage(typeof (UcChannelsStatus));
                        break;
                    case "ndControllers":
                        CreateAndPlaceToPage(typeof(UcControllersStatus));
                        break;
                }
            }
            else if (senderType == typeof (UcFillingPage))
            {
                var fillingPage = (UcFillingPage)CreateAndPlaceToPage(typeof(UcFillingPage));
                fillingPage.OnRiserSelected += fillingPage_OnRiserSelected;
            }
            else if (senderType == typeof(UcWorklogPage))
            {
                CreateAndPlaceToPage(typeof(UcWorklogPage));
            }
            else if (senderType == typeof(UcSwitchlogPage))
            {
                CreateAndPlaceToPage(typeof(UcSwitchlogPage));
            }
            else if (senderType == typeof(UcChangelogPage))
            {
                CreateAndPlaceToPage(typeof(UcChangelogPage));
            }
            else if (senderType == typeof(UcTrendPage))
            {
                CreateAndPlaceToPage(typeof (UcTrendPage));
            }
            else if (senderType == typeof(UcFillinglogPage))
            {
                CreateAndPlaceToPage(typeof(UcFillinglogPage));
            }
        }

        private Control CreateAndPlaceToPage(Type pageType)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (panelPlace.Controls.Count > 0 &&
                panelPlace.Controls[0].GetType() == pageType) return panelPlace.Controls[0];
                var userControl = (UserControl) Activator.CreateInstance(pageType);
                userControl.Dock = DockStyle.Fill;
                userControl.Font = new Font("Segoe UI", 9.75F);
                userControl.Margin = new Padding(0, 0, 0, 0);
                var controlFunctions = userControl as IUserControlMisc;
                if (controlFunctions != null)
                    controlFunctions.DisplayIndex = DisplayIndex;
                panelPlace.Controls.Add(userControl);
                if (panelPlace.Controls.Count > 1)
                {
                    var lastControl = panelPlace.Controls[0] as IUserControlMisc;
                    if (lastControl != null) lastControl.Unload();
                    panelPlace.Controls.RemoveAt(0);
                }
                if (controlFunctions != null)
                    controlFunctions.Loaded();
                return userControl;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void FormPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Пользователь не может при помощи Alt+F4 завершить приложение
            if (e.CloseReason == CloseReason.UserClosing) e.Cancel = true;
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, @"Приложение налива будет завершено!", @"Завершение работы " + Host.Text,
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) !=
                DialogResult.OK) return;
            var frm = new FormSplashClose();
            frm.Location = new Point((Width - frm.Width) / 2, (Height - frm.Height) / 2);
            frm.Show();
            Host.Close();
        }

        private void tmrClock_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            lblDate.Text = now.ToString("dd-MMM-yy");
            lblTime.Text = now.ToString("HH:mm:ss");
            lblOperator.Text = Data.UserName ?? "(вход не выполнен)";
            lblCaption.Text = @"АРМ налива ЦОиХТП. Пользователь: ";
            if (Data.UserFullname == null)
                lblCaption.Text += @"(вход не выполнен)";
            else
                lblCaption.Text += Data.UserFullname + @" [" + Data.UserLevel.GetEnumDescription() + @"]";
            lblStationName.Text = Environment.MachineName;
        }

        private void miProductFillingPage_Click(object sender, EventArgs e)
        {
            CallFillingPage();
        }

        private void miStations_Click(object sender, EventArgs e)
        {
            Data.Navigate(typeof(UcTreeNavigator), new NavigateTreeArgs { Panel = this, NodeName = "ndStations" });
        }

        private void miChannels_Click(object sender, EventArgs e)
        {
            Data.Navigate(typeof(UcTreeNavigator), new NavigateTreeArgs { Panel = this, NodeName = "ndChannels" });
        }

        private void miControllers_Click(object sender, EventArgs e)
        {
            Data.Navigate(typeof(UcTreeNavigator), new NavigateTreeArgs { Panel = this, NodeName = "ndControllers" });
        }

        private void miUsersList_Click(object sender, EventArgs e)
        {
            var filename = Path.Combine(Data.ConfigFolder, "users.xml");
            UserListKeeper.ShowEditor(this, filename);
        }

        private void ShowRegisterDialog()
        {
            var localfilename = Path.Combine(Data.ConfigFolder, "users.xml");
            if (!UserListKeeper.ShowSelector(this, localfilename)) return;
            // Проверка уровня доступа при смене пользователя
            var level = UserListKeeper.GetCurrentUserLevel();
            var ui = Host as IUserInfo;
            if (ui == null) return;
            if (level > 0)
                ui.LoginUser(UserListKeeper.GetCurrentUserFullName(),
                             UserListKeeper.GetCurrentUserName(),
                             UserInfo.IntToUserLevel(level));
            else
                ui.ResetLogin();
        }

        private void miLogin_Click(object sender, EventArgs e)
        {
            ShowRegisterDialog();
        }

        private void miLogout_Click(object sender, EventArgs e)
        {
            var ui = Host as IUserInfo;
            if (ui == null) return;
            ui.ResetLogin();
        }

        private void lblAlarm_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void lblAlarm_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void miUsers_DropDownOpening(object sender, EventArgs e)
        {
            miUsersList.Enabled = Data.UserLevel >= UserLevel.Eng;
            miLogin.Enabled = Data.UserLevel == UserLevel.None;
            miLogout.Enabled = Data.UserLevel != UserLevel.None;
        }

        private void miWaggons_DropDownOpening(object sender, EventArgs e)
        {
            miWaggonTypes.Enabled = Data.UserLevel >= UserLevel.Oper;
            miWaggonList.Enabled = Data.UserLevel >= UserLevel.Oper;
        }

        private void CallWaggonTypesPage()
        {
            (new FormWaggonTypesList { DisplayIndex = DisplayIndex}).ShowDialog(this);
        }

        private void miWaggonTypes_Click(object sender, EventArgs e)
        {
            CallWaggonTypesPage();
        }

        private void miWaggonList_Click(object sender, EventArgs e)
        {
            CallWaggonsPage();
        }

        private void CallWaggonsPage()
        {
            (new FormWaggonsList { DisplayIndex = DisplayIndex }).ShowDialog(this);
        }

        private FormRiserStatus _formRiserStatus;

        private void miRiserState_Click(object sender, EventArgs e)
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Состояние стояка налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Oper)
            {
                if (_riserAddressSelected != null)
                {
                    if (_formRiserStatus == null)
                    {
                        _formRiserStatus = new FormRiserStatus();
                        _formRiserStatus.FormClosing += (o, args) =>
                            {
                                args.Cancel = true;
                                _formRiserStatus.Hide();
                            };
                        _formRiserStatus.RiserAddress = _riserAddressSelected;
                        _formRiserStatus.Show(this);
                    }
                    else
                    {
                        _formRiserStatus.RiserAddress = _riserAddressSelected;
                        _formRiserStatus.Visible = true;
                        _formRiserStatus.BringToFront();
                    }
                }
                else
                    MessageBox.Show(this, @"Не выбран стояк для просмотра состояния!", @"Состояние стояка налива",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Состояние стояка налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private FormRiserTuning _formRiserTuning;

        private void ShowRiserTuningForm(int pageIndex)
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Настройка стояка налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Oper)
            {
                if (_riserAddressSelected != null)
                {
                    if (_formRiserTuning == null)
                    {
                        _formRiserTuning = new FormRiserTuning();
                        _formRiserTuning.FormClosing += (o, args) =>
                            {
                                args.Cancel = true;
                                _formRiserTuning.Hide();
                            };
                        _formRiserTuning.RiserAddress = _riserAddressSelected;
                        _formRiserTuning.Show(this);
                        _formRiserTuning.PageIndex = pageIndex;
                    }
                    else
                    {
                        _formRiserTuning.RiserAddress = _riserAddressSelected;
                        _formRiserTuning.PageIndex = pageIndex;
                        _formRiserTuning.Visible = true;
                        _formRiserTuning.BringToFront();
                    }
                }
                else
                    MessageBox.Show(this, @"Не выбран стояк для просмотра состояния!", @"Настройка стояка налива",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Настройка стояка налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void miRiserLink_Click(object sender, EventArgs e)
        {
            ShowRiserTuningForm(0);
        }

        private void miRiserPLC_Click(object sender, EventArgs e)
        {
            ShowRiserTuningForm(1);
        }

        private void miRiserADC_Click(object sender, EventArgs e)
        {
            ShowRiserTuningForm(2);
        }

        private void miRiserAlarmLevel_Click(object sender, EventArgs e)
        {
            ShowRiserTuningForm(3);
        }

        private void miRiserAnalogLevel_Click(object sender, EventArgs e)
        {
            ShowRiserTuningForm(4);
        }

        private void CallWorklogPage()
        {
            Data.Navigate(typeof(UcWorklogPage), new WorklogPageArgs { Panel = this });
        }

        private void miWorklog_Click(object sender, EventArgs e)
        {
            CallWorklogPage();
        }

        private void miWorklogFilter_Click(object sender, EventArgs e)
        {
            Data.ShowWorklogFilterEditor(this);
        }

        private void CallTrendsPage()
        {
            Data.Navigate(typeof(UcTrendPage), new TrendPageArgs { Panel = this });
        }

        private void miTrends_Click(object sender, EventArgs e)
        {
            CallTrendsPage();
        }

        private void miTuning_DropDownOpening(object sender, EventArgs e)
        {
            miRisersTuning.Enabled = _riserAddressSelected != null;
            if (_riserAddressSelected == null) return;
            var riserAddr = (RiserAddress) _riserAddressSelected;
            int channel;
            lock (Data.RiserNodes)
            {
                if (!Data.RiserNodes.ContainsKey(riserAddr)) return;
                channel = Data.RiserNodes[riserAddr].Channel;
            }
            var remoted = true;
            lock (Data.ChannelNodes)
            {
                if (channel >= 0 && channel < Data.ChannelNodes.Count)
                    remoted = !Data.ChannelNodes[channel].Active;
            }
            miRiserLink.Enabled = miRiserPLC.Enabled = miRiserADC.Enabled = 
                miRiserAnalogLevel.Enabled = miRiserAlarmLevel.Enabled = !remoted;
        }

        private void miFillinglog_Click(object sender, EventArgs e)
        {
            CallFillinglogPage();
        }

        private void CallFillinglogPage()
        {
            Data.Navigate(typeof(UcFillinglogPage), new FillinglogPageArgs { Panel = this });
        }

        private void miSwitchlog_Click(object sender, EventArgs e)
        {
            CallSwitchlogPage();
        }

        private void CallSwitchlogPage()
        {
            Data.Navigate(typeof(UcSwitchlogPage), new SwitchlogPageArgs { Panel = this });
        }

        private void miUserlog_Click(object sender, EventArgs e)
        {
            CallChangelogPage();
        }

        private void CallChangelogPage()
        {
            Data.Navigate(typeof(UcChangelogPage), new ChangelogPageArgs { Panel = this });
        }

        private void miGeneralTuning_Click(object sender, EventArgs e)
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Настройка параметров системы",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Admin)
            {
                using (var frm = new FormConfig())
                {
                    frm.ShowDialog(this);
                }
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Настройка параметров системы",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void miStation_DropDownOpened(object sender, EventArgs e)
        {
            miExit.Enabled = Data.SystemShell && Data.UserLevel >= UserLevel.Admin || !Data.SystemShell;
        }
    }
}
