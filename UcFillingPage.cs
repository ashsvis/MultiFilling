using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MultiFilling.SystemStatus;
using MultiFilling.WaggonsList;

namespace MultiFilling
{
    public delegate void RiserSelected(RiserAddress? addr);


    public struct SelectedRectItem
    {
        public RiserAddress Addr;
        public Rectangle Rect;
    }

    public partial class UcFillingPage : UserControl, IUserControlMisc
    {
        private readonly Color _backDrawingColor;
        private readonly List<SelectedRectItem> _risers = new List<SelectedRectItem>();
        private readonly List<SelectedRectItem> _starts = new List<SelectedRectItem>();
        private readonly List<SelectedRectItem> _stops = new List<SelectedRectItem>();
        private readonly List<SelectedRectItem> _checks = new List<SelectedRectItem>();
        
        private int _viewCount = 34;

        public int DisplayIndex { get; set; }

        public event RiserSelected OnRiserSelected;

        private bool _splittersLinked;

        public UcFillingPage()
        {
            InitializeComponent();
            _backDrawingColor = Color.FromArgb(216, 228, 248);
        }

        public void Loaded()
        {
            vScrollBar1.Maximum = Data.Config.ReadInteger("General", "LogMessagesCount", 500);
            vScrollBar1.Value = vScrollBar1.Maximum; 
            RestoreSettings();
        }

        private void UcFillingPage_Load(object sender, EventArgs e)
        {
            Data.UpdateProductTree(tvNavigator, DisplayIndex);
            lvLogView.SetDoubleBuffered(true);
            var lv = lvLogView;
            var sum = 0;
            for (var i = 0; i < 1; i++) sum += lv.Columns[i].Width;
            lv.Columns[1].Width = lv.ClientSize.Width - sum;
            Data.OnUpdateSystemLog += Data_OnUpdateSystemLog;
        }

        void Data_OnUpdateSystemLog(object sender, EventArgs e)
        {
            UpdateListView();
        }

        readonly List<ListViewItem> _reportrows = new List<ListViewItem>();

        private void LoadLog(int pos, bool print = false)
        {
            if (pos < 0) return;
            var dateBefore = DateTime.Now.AddDays(-1);
            CalcRowsCount(lvLogView);
            var count = _viewCount;
            int linescount;
            var results = Data.GetSysLogRecords(pos, count, dateBefore, out linescount, false);
            if (count <= 0) return;
            _reportrows.Clear();
            var row = 0;
            foreach (string[] rec in results)
            {
                if (rec.Length != 12) continue;
                var item = new ListViewItem(rec[0]);
                if (row%2 != 0)
                    item.BackColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
                var addr = string.Join("", rec, 1, 5).Trim().Length > 0
                               ? string.Format("[Эстакада {0}. Путь {1}. {2}. Стояк {3}] ",
                                               rec[2], rec[3],  Data.GetFineProductName(rec[4]), rec[5])
                               : "";
                var mess = rec[6];
                if (mess.IndexOf("Налив завершен аварийно", StringComparison.Ordinal) >= 0 ||
                    mess.IndexOf("Налив завершен кнопкой", StringComparison.Ordinal) >= 0 ||
                    mess.IndexOf("Налив завершен оператором", StringComparison.Ordinal) >= 0)
                    item.ForeColor = Color.Red;
                else if (mess.IndexOf("Обрыв соединения", StringComparison.Ordinal) >= 0)
                    item.ForeColor = Color.DarkRed;
                else if (mess.IndexOf("Установка соединения", StringComparison.Ordinal) >= 0)
                    item.ForeColor = Color.DarkGreen;
                else if (mess.IndexOf("Налив завершен", StringComparison.Ordinal) >= 0)
                    item.ForeColor = Color.Green;
                else if (mess.IndexOf("Запуск системы", StringComparison.Ordinal) >= 0 ||
                         mess.IndexOf("Останов системы", StringComparison.Ordinal) >= 0)
                    item.ForeColor = Color.Blue;
                else if (mess.IndexOf("Вход в систему", StringComparison.Ordinal) >= 0 ||
                         mess.IndexOf("Выход из системы", StringComparison.Ordinal) >= 0)
                    item.ForeColor = Color.Purple;
                var waggon = string.Join("", rec, 7, 4).Trim().Length > 0
                                 ? string.Format(". Цистерна №{0} типа {1}, задание {2}/{3}",
                                                 rec[7], rec[8], rec[9], rec[10])
                                 : "";
                var filled = rec[11].Trim().Length > 0
                                 ? string.Format(", налито {0}", rec[11])
                                 : "";
                item.SubItems.Add(string.Concat(addr, mess, waggon, filled));
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
            _viewCount = itemHeight > 0 ? Convert.ToInt32(Math.Truncate(lv.ClientSize.Height / itemHeight) - 1) : 0;
            if (!hasrows) lv.Items.Clear();
        }

        private void tvNavigator_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;
            if (node == null) return;
            if (node.Tag == null) return;
            Cursor = Cursors.WaitCursor;
            try
            {
                lblFillinCaption.Text = node.FullPath.Replace(tvNavigator.PathSeparator, ". ");
                var channelIndexes = node.Tag.ToString().Split(new[] {';'});
                var riserList = new List<RiserAddress>();
                var channelActive = false;
                foreach (var channelIndex in channelIndexes)
                {
                    int index;
                    if (!int.TryParse(channelIndex, out index)) continue;
                    lock (Data.ChannelNodes)
                    {
                        if (index < 0 || index >= Data.ChannelNodes.Count) continue;
                        var channel = Data.ChannelNodes[index];
                        if (!channelActive)
                            channelActive = channel.Active;
                        riserList.AddRange(channel.RisersRange.Select(riserNo => new RiserAddress
                            {
                                Channel = channel.Index,
                                Overpass = channel.Overpass,
                                Way = channel.Way,
                                Product = channel.Product,
                                Riser = riserNo
                            }));
                    }
                }
                btnTask.Visible = btnClear.Visible = btnClearAll.Visible = btnAllTasks.Visible = 
                    btnStartAllSelected.Visible = btnStopAllRuns.Visible = toolStripSeparator1.Visible =
                    toolStripSeparator3.Visible = channelActive;
                cbRisers.Items.Clear();
                foreach (var addr in riserList.OrderBy(item => item.Riser))
                    cbRisers.Items.Add(addr);
                DrawFillingPanel(null, true); // коллекционирование прямоугольников выбора
                Data.Config.WriteString("FillingPage" + DisplayIndex, "Navigate", node.Name);
                Data.Config.UpdateFile();
                if (OnRiserSelected != null) OnRiserSelected(null);
                pictBox.Invalidate();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void pictBox_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            DrawFillingPanel(graphics);
        }

        private void DrawFillingPanel(Graphics graphics, bool calcBoxes = false)
        {
            if (calcBoxes)
            {
                _risers.Clear();
                _starts.Clear();
                _stops.Clear();
                _checks.Clear();
            }
            var selIndex = cbRisers.SelectedIndex;
            var clientWidth = pictBox.ClientRectangle.Width;
            var clientHeight = pictBox.ClientRectangle.Height;
            var riserRect = new Rectangle(0, 0, 146, 98);
            var n = 0;
            while (riserRect.Top + riserRect.Height < clientHeight)
            {
                while (riserRect.Left + riserRect.Width < clientWidth)
                {
                    if (n >= cbRisers.Items.Count) return;
                    var addr = (RiserAddress)cbRisers.Items[n];
                    var rect = new Rectangle(riserRect.Location, riserRect.Size);
                    bool linked, channelActive, fillingInProgress = false;
                    bool startPressed, stopPressed;
                    //bool? filledKind;
                    int channel;
                    lock (Data.RiserNodes)
                    {
                        var riserNode = Data.RiserNodes[addr];
                        channel = riserNode.Channel;
                        linked = riserNode.Active && riserNode.BarometerValue < riserNode.MarginalLimit;
                        //filledKind = riserNode.FilledKind;
                        startPressed = riserNode.StartPressed;
                        stopPressed = riserNode.StopPressed;
                    }
                    lock (Data.ChannelNodes)
                    {
                        channelActive = Data.ChannelNodes[channel].Active;
                    }

                    var selected = false;

                    var ntype = "";
                    int setpoint = 0, currentLevel = 0, filledLevel = 0;
                    if (calcBoxes)
                    {
                        _risers.Add(new SelectedRectItem {Addr = addr, Rect = rect});
                    }
                    else
                    {
                        rect.Inflate(-1, -1);
                        using (var brush = new SolidBrush(selIndex == n ? Color.White : _backDrawingColor))
                            graphics.FillRectangle(brush, rect);
                        DrawBorder(graphics, rect,
                                   SystemColors.ControlLightLight, SystemColors.ControlDarkDark, 1);
                        bool? hasGround;
                        bool alarmLevel, hasHandMode, workMode, smallValve, bigValve, ready;
                        lock (Data.RiserNodes)
                        {
                            var riserNode = Data.RiserNodes[addr];
                            linked = riserNode.Active && riserNode.BarometerValue < riserNode.MarginalLimit;
                            ready = riserNode.Ready;
                            alarmLevel = riserNode.HasAlarmLevel;
                            hasGround = !riserNode.HasNoGround;
                            hasHandMode = riserNode.HasHandMode;
                            setpoint = riserNode.Setpoint;
                            ntype = riserNode.Ntype;
                            currentLevel = riserNode.ShowedLevel;
                            filledLevel = riserNode.FilledLevel;
                            workMode = riserNode.WorkMode;
                            smallValve = riserNode.SmallValve;
                            bigValve = riserNode.BigValve;
                            selected = riserNode.Selected;
                            fillingInProgress = riserNode.FreshedValue;
                        }
                        if (linked)
                        {
                            rect = new Rectangle(new Point(riserRect.Left + 29, riserRect.Top + 22),
                                                 Properties.Resources.waggon.Size);
                            graphics.DrawImage(Properties.Resources.waggon, rect);
                            if (workMode) // стояк в положении налива
                            {
                                rect = new Rectangle(new Point(riserRect.Left + 67, riserRect.Top + 4),
                                                     Properties.Resources.throat.Size);
                                graphics.DrawImage(Properties.Resources.throat, rect);
                                if (smallValve) // малый клапан включен
                                {
                                    rect = new Rectangle(new Point(riserRect.Left + 67, riserRect.Top + 4),
                                                         Properties.Resources.flow_small.Size);
                                    graphics.DrawImage(Properties.Resources.flow_small, rect);
                                }
                                if (bigValve) // большой клапан включен
                                {
                                    rect = new Rectangle(new Point(riserRect.Left + 67, riserRect.Top + 4),
                                                         Properties.Resources.flow_big.Size);
                                    graphics.DrawImage(Properties.Resources.flow_big, rect);
                                }
                            }
                            if (Data.ShowReadyAndAlarm)
                            {
                                DrawLamp(graphics, new Rectangle(riserRect.Left + 47, riserRect.Top + 7, 14, 14), ready,
                                         Color.Lime, Color.Red, Color.Silver);
                                if (alarmLevel)
                                    DrawLamp(graphics, new Rectangle(riserRect.Left + 77, riserRect.Top + 22, 14, 14), true,
                                             Color.Red, Color.Lime, Color.Silver);
                            }
                            else
                                DrawLamp(graphics, new Rectangle(riserRect.Left + 47, riserRect.Top + 7, 14, 14), alarmLevel,
                                            Color.Red, Color.Lime, Color.Silver);
                            if (hasGround != null) // нет запрета контроля заземления
                            {
                                rect = new Rectangle(new Point(riserRect.Left + 76, riserRect.Top + 65),
                                                     (bool) hasGround
                                                         ? Properties.Resources.ground_green.Size
                                                         : Properties.Resources.ground_red.Size);
                                graphics.DrawImage((bool) hasGround
                                                       ? Properties.Resources.ground_green
                                                       : Properties.Resources.ground_red, rect);
                            }
                            if (hasHandMode) //режим АВТОНОМНО
                            {
                                rect = new Rectangle(new Point(riserRect.Left + 122, riserRect.Top + 4),
                                                     Properties.Resources.hand_red.Size);
                                graphics.DrawImage(Properties.Resources.hand_red, rect);
                            }
                        }
                    }
                    using (var format = new StringFormat())
                    {
                        if (!calcBoxes)
                        {
                            format.LineAlignment = StringAlignment.Center;
                            format.Alignment = StringAlignment.Center;
                            // номер стояка
                            using (var font = new Font("Courier New", 10, FontStyle.Bold))
                                graphics.DrawString(addr.Riser.ToString("0"), font, linked ? Brushes.Blue : Brushes.Red,
                                                    new Rectangle(riserRect.Left + 58, riserRect.Top + 80, 34, 16),
                                                    format);
                            // задание взлива
                            using (var font = new Font("Courier New", 9, FontStyle.Bold))
                                graphics.DrawString(setpoint.ToString("0"), font, Brushes.Black,
                                                    new Rectangle(riserRect.Left + 2, riserRect.Top + 6, 40, 16), format);
                            // номер типа вагона
                            using (var font = new Font("Courier New", 9, FontStyle.Bold))
                                graphics.DrawString(ntype, font, Brushes.Green,
                                                    new Rectangle(riserRect.Left + 2, riserRect.Top + 39, 33, 16),
                                                    format);
                        }

                        ////if (!calcBoxes)
                        ////using (var font = new Font("Courier New", 9, FontStyle.Bold))
                        ////    graphics.DrawString(channel.ToString("0"), font, Brushes.Green,
                        ////                        new Rectangle(riserRect.Left + 2, riserRect.Top + 18, 40,
                        ////                                        16), format);

                        // текущий измеренный уровень взлива
                        if (linked)
                        {
                            if (!calcBoxes)
                            {
                                var valueForLevel = currentLevel.ToString("0");
                                
                                
                                //if (filledKind != null)
                                //{
                                //    if ((bool)filledKind)
                                //    {
                                //        using (var font = new Font("Courier New", 9, FontStyle.Bold))
                                //            graphics.DrawString(filledLevel.ToString("0"), font, Brushes.Green,
                                //                                new Rectangle(riserRect.Left + 2, riserRect.Top + 18, 40,
                                //                                              16),
                                //                                format);
                                //    }
                                //    else
                                //    {
                                //        using (var font = new Font("Courier New", 9, FontStyle.Bold))
                                //            graphics.DrawString(filledLevel.ToString("0"), font, Brushes.Red,
                                //                                new Rectangle(riserRect.Left + 2, riserRect.Top + 18, 40,
                                //                                              16),
                                //                                format);

                                //    }
                                //}
                                if (currentLevel < 0) valueForLevel = "0";
                                using (var font = new Font("Segoe UI", 20, FontStyle.Bold))
                                {
                                    graphics.DrawString(valueForLevel, font, Brushes.Black,
                                                        new Rectangle(riserRect.Left + 30, riserRect.Top + 18, 112, 56),
                                                        format);
                                    graphics.DrawString(valueForLevel, font, fillingInProgress
                                                                                 ? Brushes.Yellow
                                                                                 : Brushes.Khaki,
                                                        new Rectangle(riserRect.Left + 29, riserRect.Top + 17, 112, 56),
                                                        format);
                                }
                            }
                            if (channelActive)
                            {
                                if (calcBoxes)
                                {
                                    var rstart = new Rectangle(riserRect.Left + 2, riserRect.Top + 77, 56, 20);
                                    _starts.Add(new SelectedRectItem {Addr = addr, Rect = rstart});
                                }
                                else
                                {
                                    var startEnabled = _enabled;
                                    DrawButton(graphics, "Старт", riserRect.Left + 2, riserRect.Top + 77, 56, 20,
                                               startEnabled, startPressed);
                                }
                                if (calcBoxes)
                                {
                                    var rstop = new Rectangle(riserRect.Left + riserRect.Width - 56, riserRect.Top + 77,
                                                              56, 20);
                                    _stops.Add(new SelectedRectItem {Addr = addr, Rect = rstop});
                                }
                                else
                                {
                                    var stopEnabled = _enabled;
                                    DrawButton(graphics, "Стоп", riserRect.Left + riserRect.Width - 56,
                                               riserRect.Top + 77,
                                               56, 20,
                                               stopEnabled, stopPressed);
                                }
                                if (calcBoxes)
                                {
                                    var rcheck = new Rectangle(riserRect.Left + 12, riserRect.Top + 60, 14, 14);
                                    _checks.Add(new SelectedRectItem {Addr = addr, Rect = rcheck});
                                }
                                else
                                {
                                    ControlPaint.DrawCheckBox(graphics,
                                                              new Rectangle(riserRect.Left + 12, riserRect.Top + 60, 14,
                                                                            14),
                                                              selected ? ButtonState.Checked : ButtonState.Normal);
                                }
                            }
                        }
                        else if (!calcBoxes)
                        {
                            using (var font = new Font("Courier New", 10, FontStyle.Bold))
                                graphics.DrawString("НЕТ СВЯЗИ", font, Brushes.Red,
                                                    new RectangleF(riserRect.Left + 29, riserRect.Top + 22, 112, 56),
                                                    format);
                        }
                    }
                    //-----------------------------------
                    riserRect.Offset(riserRect.Width + 3, 0);
                    n++;
                }
                riserRect.X = 0;
                riserRect.Offset(0, riserRect.Height + 3);
            }
        }

        private static void DrawButton(Graphics graphics, string buttonText, int left, int top, 
            int width, int height, bool enabled, bool pushed)
        {
            ControlPaint.DrawButton(graphics, left, top, width, height, pushed ? ButtonState.Pushed : ButtonState.Normal);
            var dx = pushed ? 1 : 0;
            var dy = pushed ? 1 : 0;
            using (var format = new StringFormat())
            {
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                using (var font = new Font("Courier New", 9, FontStyle.Bold))
                    graphics.DrawString(buttonText, font,
                                        enabled ? SystemBrushes.ControlText : Brushes.Gray,
                                        new Rectangle(left + dx, top - 1 + dy, width, height),
                                        format);
            }
        }

        private static void DrawLamp(Graphics graphics, Rectangle rect, bool? state,
                              Color colorOn, Color colorOff, Color colorNone)
        {
            using (var brush = new SolidBrush(state != null ?
                ((bool)state ? colorOn : colorOff) : colorNone))
            {
                graphics.FillEllipse(brush, rect);
                graphics.DrawEllipse(Pens.Black, rect);
                rect.Inflate(-1, -1);
                graphics.DrawEllipse(Pens.Gray, rect);
            }
            var blick = new Rectangle(rect.X + 2, rect.Y + 2, 4, 4);
            graphics.FillEllipse(Brushes.White, blick);
        }

        private static void DrawBorder(Graphics g, RectangleF rect, Color lefttop, Color rightdown,
                                 float width)
        {
            using (var pen = new Pen(lefttop, width))
                g.DrawLines(pen, new[]
                    {
                        new PointF(rect.X, rect.Y + rect.Height),
                        new PointF(rect.X, rect.Y),
                        new PointF(rect.X + rect.Width, rect.Y)
                    });
            using (var pen = new Pen(rightdown, width))
                g.DrawLines(pen, new[]
                    {
                        new PointF(rect.X + rect.Width, rect.Y),
                        new PointF(rect.X + rect.Width, rect.Y + rect.Height),
                        new PointF(rect.X, rect.Y + rect.Height)
                    });
        }

        private void cbRisers_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictBox.Invalidate();
            if (cbRisers.SelectedItem != null)
            {
                var addr = (RiserAddress) cbRisers.SelectedItem;
                Data.Session.WriteInteger("TrendShowed" + DisplayIndex, "Riser", addr.Riser);

                Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "Channel", addr.Channel);
                Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "Overpass", addr.Overpass);
                Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "Way", addr.Way);
                Data.Session.WriteString("SystemStatus" + DisplayIndex, "Product", addr.Product);
                Data.Session.WriteInteger("SystemStatus" + DisplayIndex, "Riser", addr.Riser);
            }
            
            if (OnRiserSelected != null) 
                OnRiserSelected(cbRisers.SelectedItem != null ? (RiserAddress?)cbRisers.SelectedItem : null);
        }

        private void UpdateListView()
        {
            LoadLog(vScrollBar1.Maximum - vScrollBar1.Value);            
        }

        private bool _enabled;

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            _enabled = Data.UserLevel > UserLevel.None;
            DrawFillingPanel(null, true); // коллекционирование прямоугольников выбора
            pictBox.Invalidate();
            
            if (!_splittersLinked)
            {
                splitLeftRight.SplitterMoved += splitLeftRightContainer_SplitterMoved;
                splitTopBottom.SplitterMoved += splitTopBottomContainer_SplitterMoved;
                _splittersLinked = true;
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateListView();
        }

        private void splitLeftRightContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            DrawFillingPanel(null, true); // коллекционирование прямоугольников выбора
        }

        private void splitTopBottomContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            DrawFillingPanel(null, true); // коллекционирование прямоугольников выбора
            UpdateListView();
        }

/*
        private void pictBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_enabled) return;
            var found = false;
            if (_starts.Any(selectedRectItem => selectedRectItem.Rect.Contains(e.Location)))
            {
                Cursor = Cursors.Hand;
                found = true;
            }
            if (_stops.Any(selectedRectItem => selectedRectItem.Rect.Contains(e.Location)))
            {
                Cursor = Cursors.Hand;
                found = true;
            }
            if (!found) Cursor = Cursors.Default;
        }
*/

        private void pictBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1) return;
            if (e.Button == MouseButtons.Left)
            {
                foreach (var selectedRectItem in
                        _checks.Where(selectedRectItem => selectedRectItem.Rect.Contains(e.Location)))
                {
                    lock (Data.RiserNodes)
                    {
                        var riser = Data.RiserNodes[selectedRectItem.Addr];
                        riser.Selected = !riser.Selected;
                    }
                    pictBox.Invalidate();
                    break;
                }
                if (_enabled)
                {
                    foreach (var selectedRectItem in
                            _stops.Where(selectedRectItem => selectedRectItem.Rect.Contains(e.Location)))
                    {
                        if (Data.UserLevel == UserLevel.None)
                            MessageBox.Show(this, @"Вход в систему не выполнен!", @"Останов налива",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else if (Data.UserLevel >= UserLevel.Oper)
                        {
                            lock (Data.RiserNodes)
                            {
                                var riser = Data.RiserNodes[selectedRectItem.Addr];
                                if (riser.RiserMode > RiserState.Waiting)
                                {
                                    riser.WriteAddress = 0x06;
                                    riser.WriteData = new ushort[]
                                        {
                                            0x02
                                        };
                                    riser.StopPressed = true;
                                }
                            }
                        }
                        else
                            MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                            @"Останов налива", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    foreach (var selectedRectItem in
                            _starts.Where(selectedRectItem => selectedRectItem.Rect.Contains(e.Location)))
                    {
                        if (Data.UserLevel == UserLevel.None)
                            MessageBox.Show(this, @"Вход в систему не выполнен!", @"Пуск налива",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else if (Data.UserLevel >= UserLevel.Oper)
                        {
                            string riserName;
                            var resetFilledKind = false;
                            lock (Data.RiserNodes)
                            {
                                var riser = Data.RiserNodes[selectedRectItem.Addr];
                                if (riser.Ready && !riser.HasHandMode &&
                                    riser.RiserMode == RiserState.Waiting &&
                                    riser.Setpoint > 0)
                                {
                                    riser.FilledLevel = 0;
                                    riser.WriteAddress = 0x06;
                                    riser.WriteData = new ushort[]
                                        {
                                            0x01,
                                            Convert.ToUInt16(riser.FactHeight),
                                            Convert.ToUInt16(riser.Setpoint)
                                        };
                                    riser.StartPressed = true;
                                    riser.FilledKind = null;
                                    resetFilledKind = true;
                                    riser.FillingStarted = DateTime.Now;
                                    riser.FillingUser = Data.UserName ?? "";
                                    Data.SendToSystemLog("Запуск налива", riser.Address, riser.WaggonData);
                                }
                                riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                                                          riser.Channel, riser.Overpass, riser.Way,
                                                          riser.Product, riser.Riser);
                            }
                            if (resetFilledKind)
                            {
                                Data.UpdateRiserProperty(riserName, "FilledLevel", "0");
                                Data.UpdateRiserProperty(riserName, "FilledKind", "");
                            }
                        }
                        else
                            MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                            @"Пуск налива", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }
            }
            foreach (var selectedRectItem in
                    _risers.Where(selectedRectItem => selectedRectItem.Rect.Contains(e.Location)))
            {
                cbRisers.SelectedItem = selectedRectItem.Addr;
                pictBox.Invalidate();
                break;
            }
        }

        private void pictBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_starts.Any(item => item.Rect.Contains(e.Location))) return;
            if (_stops.Any(item => item.Rect.Contains(e.Location))) return;
            if (_checks.Any(item => item.Rect.Contains(e.Location))) return;
            DefineTaskData();
        }

        private void DefineTaskData()
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Ввод задания налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Oper)
            {
                var selectedItemAddr = (RiserAddress) cbRisers.SelectedItem;
                string number, ntype;
                int riser, factheight, setpoint, channel;
                int? deeplevel, worklevel;
                bool channelActive;
                lock (Data.RiserNodes)
                {
                    var riserNode = Data.RiserNodes[selectedItemAddr];
                    channel = riserNode.Channel;
                    riser = riserNode.Riser;
                    number = riserNode.Number;
                    ntype = riserNode.Ntype;
                    factheight = riserNode.FactHeight;
                    setpoint = riserNode.Setpoint;
                    deeplevel = riserNode.DeepLevel;
                    worklevel = riserNode.WorkLevel;
                }
                lock (Data.ChannelNodes)
                {
                    channelActive = Data.ChannelNodes[channel].Active;
                }
                if (!channelActive) return;
                using (var frm = new FormTaskDataEditor(number, ntype, factheight, setpoint, selectedItemAddr)
                    {
                        Text = string.Format("Задание налива [ Стояк {0} ]", riser),
                        DeepLevel = deeplevel,
                        WorkLevel = worklevel
                    })
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        var waggon = frm.GetData;
                        string riserName;
                        lock (Data.RiserNodes)
                        {
                            var riserNode = Data.RiserNodes[selectedItemAddr];
                            riserNode.Number = waggon.Number;
                            riserNode.Ntype = waggon.Ntype;
                            riserNode.FactHeight = waggon.FactHeight;
                            riserNode.Setpoint = waggon.Setpoint;
                            riserNode.FilledKind = null;
                            riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                                                      riserNode.Channel, riserNode.Overpass, riserNode.Way,
                                                      riserNode.Product, riserNode.Riser);
                        }
                        Data.UpdateRiserProperty(riserName, "Number", waggon.Number);
                        Data.UpdateRiserProperty(riserName, "Ntype", waggon.Ntype);
                        Data.UpdateRiserProperty(riserName, "FactHeight", waggon.FactHeight.ToString("0"));
                        Data.UpdateRiserProperty(riserName, "Setpoint", waggon.Setpoint.ToString("0"));
                        Data.UpdateRiserProperty(riserName, "FilledKind", "");

                        pictBox.Invalidate();
                        WaggonDataKeeper.Add(waggon.Number, waggon.Ntype, waggon.FactHeight);
                        var riserAddr = string.Format("N{0}{1}{2}{3}",
                                                     selectedItemAddr.Overpass, selectedItemAddr.Way,
                                                     selectedItemAddr.Product, selectedItemAddr.Riser.ToString("000"));
                        Data.Tasks.WriteInteger(riserAddr, "Overpass", selectedItemAddr.Overpass);
                        Data.Tasks.WriteInteger(riserAddr, "Way", selectedItemAddr.Way);
                        Data.Tasks.WriteString(riserAddr, "Product", selectedItemAddr.Product);
                        Data.Tasks.WriteInteger(riserAddr, "Riser", selectedItemAddr.Riser);
                        Data.Tasks.WriteString(riserAddr, "Number", waggon.Number);
                        Data.Tasks.WriteString(riserAddr, "NType", waggon.Ntype);
                        Data.Tasks.WriteInteger(riserAddr, "FactHeight", waggon.FactHeight);
                        Data.Tasks.WriteInteger(riserAddr, "Setpoint", waggon.Setpoint);
                        Data.Tasks.UpdateFile();
                    }
                }
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Ввод задания налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ClearTaskData()
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Очистка задания налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Oper)
            {
                var selectedItemAddr = (RiserAddress)cbRisers.SelectedItem;
                string riserName;
                lock (Data.RiserNodes)
                {
                    var riserNode = Data.RiserNodes[selectedItemAddr];
                    riserNode.Number = "";
                    riserNode.Ntype = "";
                    riserNode.FactHeight = 0;
                    riserNode.Setpoint = 0;
                    riserNode.FilledKind = null;
                    riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                                              riserNode.Channel, riserNode.Overpass, riserNode.Way,
                                              riserNode.Product, riserNode.Riser);
                }
                Data.UpdateRiserProperty(riserName, "Number", "");
                Data.UpdateRiserProperty(riserName, "Ntype", "");
                Data.UpdateRiserProperty(riserName, "FactHeight", "0");
                Data.UpdateRiserProperty(riserName, "Setpoint", "0");
                Data.UpdateRiserProperty(riserName, "FilledKind", "");

                pictBox.Invalidate();
                var riserAddr = string.Format("N{0}{1}{2}{3}",
                                             selectedItemAddr.Overpass, selectedItemAddr.Way,
                                             selectedItemAddr.Product, selectedItemAddr.Riser.ToString("000"));
                Data.Tasks.EraseSection(riserAddr);
                Data.Tasks.UpdateFile();
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Очистка задания налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ClearAllTaskData()
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Очистка всех заданий налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Oper)
            {
                if (MessageBox.Show(this, @"Очистить все задания группы?", @"Очистка всех заданий налива",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    foreach (var selectedItemAddr in cbRisers.Items.Cast<RiserAddress>())
                    {
                        string riserName;
                        lock (Data.RiserNodes)
                        {
                            var riserNode = Data.RiserNodes[selectedItemAddr];
                            riserNode.Number = "";
                            riserNode.Ntype = "";
                            riserNode.FactHeight = 0;
                            riserNode.Setpoint = 0;
                            riserNode.FilledKind = null;
                            riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                                                      riserNode.Channel, riserNode.Overpass, riserNode.Way,
                                                      riserNode.Product, riserNode.Riser);
                        }
                        Data.UpdateRiserProperty(riserName, "Number", "");
                        Data.UpdateRiserProperty(riserName, "Ntype", "");
                        Data.UpdateRiserProperty(riserName, "FactHeight", "0");
                        Data.UpdateRiserProperty(riserName, "Setpoint", "0");
                        Data.UpdateRiserProperty(riserName, "FilledKind", "");

                        var riserAddr = string.Format("N{0}{1}{2}{3}",
                                                     selectedItemAddr.Overpass, selectedItemAddr.Way,
                                                     selectedItemAddr.Product, selectedItemAddr.Riser.ToString("000"));
                        Data.Tasks.EraseSection(riserAddr);
                    }
                    pictBox.Invalidate();
                    Data.Tasks.UpdateFile();
                }
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Очистка всех заданий налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void StartAllSelected()
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Групповой запуск налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Oper)
            {
                if (MessageBox.Show(this, @"Выполнить групповой запуск налива?", @"Групповой запуск налива",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    var list = new List<RiserAddress>();
                    foreach (var selectedItemAddr in cbRisers.Items.Cast<RiserAddress>())
                    {
                        lock (Data.RiserNodes)
                        {
                            var riser = Data.RiserNodes[selectedItemAddr];
                            if (riser.Selected)
                                list.Add(selectedItemAddr);
                        }
                    }
                    btnStartAllSelected.Enabled = false;
                    var period = Data.StartTaskPeriod;
                    ThreadPool.QueueUserWorkItem(arg =>
                    {
                        foreach (var riserAddress in list)
                        {
                            string riserName;
                            lock (Data.RiserNodes)
                            {
                                var riser = Data.RiserNodes[riserAddress];
                                if (!riser.Ready || riser.HasHandMode || riser.RiserMode != RiserState.Waiting ||
                                    riser.Setpoint <= 0) continue;
                                riser.FilledLevel = 0;
                                riser.WriteAddress = 0x06;
                                riser.WriteData = new ushort[]
                                    {
                                        0x01, 
                                        Convert.ToUInt16(riser.FactHeight), 
                                        Convert.ToUInt16(riser.Setpoint)
                                    };
                                riser.FillingStarted = DateTime.Now;
                                riser.FillingUser = Data.UserName ?? "";
                                riser.FilledKind = null;
                                Data.SendToSystemLog("Запуск налива", riser.Address, riser.WaggonData);
                                riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                                                         riser.Channel, riser.Overpass, riser.Way,
                                                         riser.Product, riser.Riser);
                            }
                            Data.UpdateRiserProperty(riserName, "FilledLevel", "0");
                            Data.UpdateRiserProperty(riserName, "FilledKind", "");
                            Thread.Sleep(period);
                        }
                        var method = new MethodInvoker(() =>
                        {
                            btnStartAllSelected.Enabled = true;
                        }
                            );
                        if (InvokeRequired)
                            BeginInvoke(method);
                        else
                            method();

                    });
                }
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Групповой запуск налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void StopAllRuns()
        {
            if (Data.UserLevel == UserLevel.None)
                MessageBox.Show(this, @"Вход в систему не выполнен!", @"Групповой останов налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Data.UserLevel >= UserLevel.Oper)
            {
                if (MessageBox.Show(this, @"Выполнить групповой останов налива?", @"Групповой останов налива",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) ==
                    DialogResult.Yes)
                {
                    var list = new List<RiserAddress>();
                    foreach (var selectedItemAddr in cbRisers.Items.Cast<RiserAddress>())
                    {
                        lock (Data.RiserNodes)
                        {
                            var riser = Data.RiserNodes[selectedItemAddr];
                            if (riser.RiserMode > RiserState.Waiting)
                                list.Add(selectedItemAddr);
                        }
                    }
                    btnStopAllRuns.Enabled = false;
                    var period = Data.StopTaskPeriod;
                    ThreadPool.QueueUserWorkItem(arg =>
                        {
                            foreach (var riserAddress in list)
                            {
                                lock (Data.RiserNodes)
                                {
                                    var riser = Data.RiserNodes[riserAddress];
                                    if (riser.RiserMode <= RiserState.Waiting) continue;
                                    riser.WriteAddress = 0x06;
                                    riser.WriteData = new ushort[]
                                        {
                                            0x02
                                        };
                                    //riser.TaskStatus = RiserTaskStatus.OperEnding;
                                }
                                Thread.Sleep(period);
                            }
                            var method = new MethodInvoker(() =>
                                {
                                    btnStopAllRuns.Enabled = true;
                                }
                                );
                            if (InvokeRequired)
                                BeginInvoke(method);
                            else 
                                method();
                        });
                }
            }
            else
                MessageBox.Show(this, @"Запрашиваемое действие не разрешено для текущего пользователя!",
                                @"Групповой останов налива",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            if (cbRisers.SelectedItem == null)
            {
                MessageBox.Show(this, @"Не выбран стояк для ввода данных налива!", @"Ввод данных налива",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DefineTaskData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (cbRisers.SelectedItem == null)
            {
                MessageBox.Show(this, @"Не выбран стояк для очистки данных налива!", @"Очистка данных налива",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ClearTaskData();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAllTaskData();
        }

        private void btnAllTasks_Click(object sender, EventArgs e)
        {
            //
        }

        private void btnStartAllSelected_Click(object sender, EventArgs e)
        {
            StartAllSelected();
        }

        private void btnStopAllRuns_Click(object sender, EventArgs e)
        {
            StopAllRuns();
        }

        public void Unload()
        {
            Data.OnUpdateSystemLog -= Data_OnUpdateSystemLog;
            timerUpdate.Tick -= timerUpdate_Tick;
            Data.Config.WriteInteger("FillingPage" + DisplayIndex, "LeftRightSplitter",
                splitLeftRight.SplitterDistance);
            Data.Config.WriteInteger("FillingPage" + DisplayIndex, "TopBottomSplitter",
                splitTopBottom.SplitterDistance);
            Data.Config.UpdateFile();
        }

        private void RestoreSettings()
        {
            splitTopBottom.SplitterDistance = Data.Config.ReadInteger("FillingPage" + DisplayIndex,
                                                                      "TopBottomSplitter", 514);
            splitLeftRight.SplitterDistance = Data.Config.ReadInteger("FillingPage" + DisplayIndex,
                                                                      "LeftRightSplitter", 183);
            UpdateListView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

            Data.SubscribeValues();

        }
    }
}
