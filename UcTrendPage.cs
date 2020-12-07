using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MultiFilling.SystemStatus;

namespace MultiFilling
{
    public partial class UcTrendPage : UserControl, IUserControlMisc
    {
        private const int ScaleHigh = 3300;
        private const int ScaleLow = 1900;

        private int _minutes = 20;
        private int _zoomminutes = 20;
        private string _riserName = "";
        private List<object> _seriesNames;
        private string _lastSelected = "";
        private int _offset;
        private DateTime _cursorPosition = DateTime.MinValue;

        public int DisplayIndex { get; set; }

        private DateTime CursorPosition
        {
            get { return _cursorPosition; }
            set
            {
                _cursorPosition = value;
                if (_cursorPosition == DateTime.MinValue)
                    Data.Session.DeleteKey("TrendShowed" + DisplayIndex, "TimePosition");
                else
                    Data.Session.WriteDateTime("TrendShowed" + DisplayIndex, "TimePosition", _cursorPosition);
            }
        }

        private int Offset
        {
            get { return _offset; }
            set
            {
                _offset = value;
                Data.Session.WriteInteger("TrendShowed" + DisplayIndex, "TimeOffset", _offset);
            }
        }

        private int Zoomminutes
        {
            get { return _zoomminutes; }
            set
            {
                _zoomminutes = value;
                Data.Session.WriteInteger("TrendShowed" + DisplayIndex, "TimeZoom", _zoomminutes);
            }
        }

        public UcTrendPage()
        {
            InitializeComponent();
            lvTrends.SetDoubleBuffered(true);
            InitChart();
        }

        public void Loaded()
        {
            Data.UpdateProductTree(tvNavigator, DisplayIndex);
            UpdateChart();
            InitTrendsList();
            fileSystemWatcher1.Path = Data.HistoryFolder;
            _minutes = Data.Session.ReadInteger("TrendShowed" + DisplayIndex, "TimeRange", 20);
            tsbNextTime.Enabled = false;
            foreach (var dropitem in tsbTimeSelect.DropDownItems.Cast<ToolStripDropDownItem>()
                .Where(dropitem => dropitem.Tag != null && 
                    (string) dropitem.Tag == _minutes.ToString("0")))
            {
                tsbTimeSelect.Text = dropitem.Text;
                break;
            }
            _offset = Data.Session.ReadInteger("TrendShowed" + DisplayIndex, "TimeOffset", 0);
            if (_offset > 0)
                tsbNextTime.Enabled = true;
            _cursorPosition = Data.Session.ReadDateTime("TrendShowed" + DisplayIndex, "TimePosition", DateTime.MinValue);
            if (_cursorPosition == DateTime.MinValue)
                ClearDateCursor();
            else
                tslCursorPositionDate.Text = @"Позиция курсора: " + CursorPosition.ToString("dd.MM.yy HH:mm.ss.fff");
            _zoomminutes = Data.Session.ReadInteger("TrendShowed" + DisplayIndex, "TimeZoom", _minutes);
            if (_zoomminutes < _minutes && _cursorPosition > DateTime.MinValue)
            {
                tsbNoZoom.Enabled = true;
                var dateHighRange = CursorPosition.AddMinutes(_zoomminutes);
                var dateLowRange = CursorPosition.AddMinutes(-_zoomminutes);
                UpdateChart(dateLowRange, dateHighRange);
                FillCursorsData(CursorPosition);
                tsbZoomIn.Enabled = true;
                tsbZoomOut.Enabled = true;
            }
            else 
                UpdateChart();
        }

        public void Unload()
        {
            fileSystemWatcher1.Changed -= fileSystemWatcher1_Changed;
        }

        private void InitChart()
        {
            // строки местами не переставлять!, их положение определяется структурой файла .trd
            _seriesNames = new List<object>
                {
                    new object[] {Color.Green, 3, SeriesChartType.FastLine, ChartDashStyle.Solid, "Текщий уровень", "Текущий уровень взлива"},
                    new object[] {Color.Red, 2, SeriesChartType.FastLine, ChartDashStyle.Dash, "Минимум", "Минимум"},
                    new object[] {Color.Red, 2, SeriesChartType.FastLine, ChartDashStyle.Dash, "Максимальная высота", "Максимальная высота (диаметр цистерны с горловиной)"},
                    new object[] {Color.Blue, 2, SeriesChartType.FastLine, ChartDashStyle.Dash, "Глубина погружения", "Глубина погружения (минимум шкалы уровнемера)"},
                    new object[] {Color.Blue, 2, SeriesChartType.FastLine, ChartDashStyle.Dash, "Рабочий диапазон", "Рабочий диапазон (максимум шкалы уровнемера)"},
                    new object[] {Color.FromArgb(192, 192, 0), 2, SeriesChartType.FastLine, ChartDashStyle.Dash, "Отключение большого клапана", "Уровень для отключения большого клапана"},
                    new object[] {Color.FromArgb(0, 192, 192), 2, SeriesChartType.FastLine, ChartDashStyle.Dash, "Заданный уровень", "Задание налива"},
                    new object[] {Color.Aqua, 3, SeriesChartType.FastLine, ChartDashStyle.Solid, "Ток сигнализатора уровня", "Ток сигнализатора уровня"},
                    new object[] {Color.Magenta, 3, SeriesChartType.FastLine, ChartDashStyle.Solid, "Ток сигнализатора аварийного", "Ток сигнализатора аварийного"},
                    // сигнал готовности
                    new object[] {Color.Green, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR03.3", "HR03.3 - Цепь готовности"}, 
                    // датчик положения стояка
                    new object[] {Color.Brown, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR03.4", "HR03.4 - Конечник рабочего положения"}, 
                    new object[] {Color.Brown, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR01.14", "HR01.14 - Состояние конечника рабочего положения"}, 
                    // команда и состояния клапана малого прохода
                    new object[] {Color.DarkCyan, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR04.9", "HR04.9 - Команда включения клапана малого прохода"}, 
                    new object[] {Color.Olive, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR03.2", "HR03.2 - Конечник клапана малого прохода"}, 
                    new object[] {Color.Olive, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR01.13", "HR01.13 - Состояние клапана малого прохода"}, 
                    new object[] {Color.Olive, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR04.13", "HR04.13 - Состояние клапана малого прохода"}, 
                    // команда и состояния клапана большого прохода
                    new object[] {Color.DarkCyan, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR04.8", "HR04.8 - Команда включения клапана большого прохода"}, 
                    new object[] {Color.BlueViolet, 2, SeriesChartType.StepLine, ChartDashStyle.Solid,"HR03.1", "HR03.1 - Конечник клапана большого прохода"}, 
                    new object[] {Color.BlueViolet, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR01.12", "HR01.12 - Состояние клапана большого прохода"}, 
                    new object[] {Color.BlueViolet, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR04.12", "HR04.12 - Состояние клапана большого прохода"}, 
                    // состояния сигнализатора аварийного
                    new object[] {Color.Red, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR01.8", "HR01.8 - Состояние сигнализатора аварийного (1 - мокрый)"}, 
                    new object[] {Color.DarkRed, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR03.14", "HR03.14 - Ток сигнализатора аварийного меньше минимального"}, 
                    new object[] {Color.DarkRed, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR03.15", "HR03.15 - Ток сигнализатора аварийного больше максимального"}, 
                    // состояния сигнализатора уровня
                    new object[] {Color.Red, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR03.6", "HR03.6 - Контроль сигнализатора уровня"}, 
                    new object[] {Color.DarkRed, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR03.10", "HR03.10 - Ток сигнализатора уровня меньше минимального"}, 
                    new object[] {Color.DarkRed, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, "HR03.11", "HR03.11 - Ток сигнализатора уровня больше максимального"}
                };
            var legend1 = new Legend
                {
                    BackColor = SystemColors.Control,
                    Docking = Docking.Bottom,
                    Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204),
                    IsTextAutoFit = false,
                    Name = "Legend1"
                };
            var legend2 = new Legend
            {
                BackColor = SystemColors.Control,
                Docking = Docking.Right,
                Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204),
                IsTextAutoFit = false,
                Name = "Legend2"
            };
            chartTrends.Legends.Add(legend1);
            chartTrends.Legends.Add(legend2);
            chartTrends.ChartAreas[0].AxisY2.Minimum = ScaleLow;
            chartTrends.ChartAreas[0].AxisY2.Maximum = ScaleHigh;
            chartTrends.ChartAreas[0].AxisY2.LabelStyle.Format = "0 мм";
            vScrollBar1.Maximum = ScaleLow + 99;
            vScrollBar1.Value = 100;

            chartTrends.ChartAreas[0].AxisY2.Minimum = ScaleLow - 100 - vScrollBar1.Value;
            chartTrends.ChartAreas[0].AxisY2.Maximum = ScaleHigh - vScrollBar1.Value;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            chartTrends.ChartAreas[0].AxisY2.Minimum = ScaleLow - 100 - vScrollBar1.Value;
            chartTrends.ChartAreas[0].AxisY2.Maximum = ScaleHigh - vScrollBar1.Value; 
        }

        private void UpdateChart(DateTime? dateLowRange = null, DateTime? dateHighRange = null)
        {
            chartTrends.Series.Clear();
            foreach (var info in _seriesNames.Cast<object[]>())
            {
                var color = (Color) info[0];
                var lineWidth = (int) info[1];
                var chartType = (SeriesChartType) info[2];
                var dashStyle = (ChartDashStyle) info[3];
                var legendText = (string) info[4];
                var toolTip = (string) info[5];
                AddToSeries(legendText.StartsWith("HR0") ? "Legend2" : "Legend1", 
                    color, lineWidth, chartType, dashStyle, legendText, toolTip);
            }
            dateHighRange =  dateHighRange ?? DateTime.Now - TimeSpan.FromMinutes(Offset);
            dateLowRange = dateLowRange ?? dateHighRange - new TimeSpan(0, _minutes, 0);
            Cursor = Cursors.WaitCursor;
            try
            {
                var trenddata = Data.LoadTrend(_riserName, (DateTime) dateLowRange, (DateTime) dateHighRange);

                var countSeries = _seriesNames.Count();
                for (var i = 0; i < countSeries; i++) chartTrends.Series[i].Points.Clear();
                foreach (var item in trenddata)
                {
                    var snap = item.Key;
                    var values = item.Value;
                    const int step = 40;
                    const int heigh = 20;
                    var pos = 2600;
                    var levlow = ScaleLow;
                    var levhigh = ScaleHigh;
                    for (var i = 0; i < countSeries; i++)
                    {
                        if (i >= values.Length) continue;
                        var value = values[i];
                        var customvalue = value.ToString("0");
                        if (i == 3) levlow = value;
                        if (i == 4) levhigh = value;
                        if (i >= 7 && i <= 8)
                        {
                            customvalue = (value/1000.0).ToString("F3");
                            value = Convert.ToInt32(ConvertValue(value, 20000, 4000, levhigh, levlow));
                        }
                        if (i >= 9)
                        {
                            var visualLevel = DecPos(ref pos, step);
                            var low = visualLevel;
                            var high = visualLevel + heigh;
                            value = value == 1 ? high : low;
                        }
                        var name = chartTrends.Series[i].Name;
                        var visible = Data.Session.ReadBool("TrendShowed" + DisplayIndex, name, true);
                        if (visible)
                        {
                            var pt = new DataPoint(chartTrends.Series[i]);
                            pt.SetCustomProperty("Value", customvalue);
                            pt.SetValueXY(snap, value);
                            chartTrends.Series[i].Points.Add(pt);
                        }
                        else
                            chartTrends.Series[i].IsVisibleInLegend = false;
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            // добавляем точку справа, чтобы линия тренда заканчивалась на границе
            //if (_offset <= 0)
            //{
            //    x.Add(DateTime.Now.AddSeconds(-1));
            //    for (var i = 0; i < count; i++) y[i].Add(values[i]);
            //}

            chartTrends.ChartAreas[0].AxisX.Minimum = ((DateTime)dateLowRange).ToOADate();
            chartTrends.ChartAreas[0].AxisX.Maximum = ((DateTime)dateHighRange).ToOADate();

            AddCursorLine(CursorPosition < dateLowRange ? DateTime.Now.AddSeconds(-1) : CursorPosition);
            //InitTrendsList();
            FillCursorsData(CursorPosition < dateLowRange ? DateTime.Now.AddSeconds(-1) : CursorPosition);
            // восстановление строки в таблице описания серий
            if (string.IsNullOrWhiteSpace(_lastSelected)) return;
            var lvi = lvTrends.FindItemWithText(_lastSelected);
            if (lvi == null) return;
            lvTrends.FocusedItem = lvi;
            lvi.Selected = true;
            lvTrends.EnsureVisible(lvi.Index);
        }

        private static int DecPos(ref int pos, int step)
        {
            var last = pos;
            pos -= step;
            return last;
        }

        private static double ConvertValue(double value, double from1, double from2, double to1, double to2)
        {
            return Math.Abs((from2 - from1) * (to2 - to1) + to1) < double.Epsilon 
                ? 0.0
                : (value - from1) / (from2 - from1) * (to2 - to1) + to1;
        }

        private static void DrawIconImage(Graphics g, Color color, string text)
        {
            using (var brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, new Rectangle(0, 0, 16, 16));
                g.DrawLines(Pens.DarkGray, new[] { new Point(1, 15), new Point(15, 15), new Point(15, 1) });
                using (var font = new Font("Courier New", 8))
                {
                    g.DrawString(text, font, Brushes.Black, new RectangleF(-1, 0, 20, 20));
                }
            }
        }

        private void InitTrendsList()
        {
            // заполнение списков цветов
            ilColors.Images.Clear();
            foreach (var color in chartTrends.Series.Select(seria => seria.Color))
            {
                using (var bmp = new Bitmap(16, 16))
                {
                    DrawIconImage(Graphics.FromImage(bmp), color, "");
                    ilColors.Images.Add(bmp);
                }
            }
            lvTrends.Items.Clear();
            lvTrends.ItemChecked -= lvTrends_ItemChecked;
            try
            {
                for (var i = 0; i < chartTrends.Series.Count - 1; i++)
                {
                    var name = chartTrends.Series[i].Name;
                    var item = new ListViewItem(String.Format("{0}.", i + 1))
                        {
                            Name = name,
                            ImageIndex = i,
                            Checked = Data.Session.ReadBool("TrendShowed" + DisplayIndex, name, true)
                        };
                    lvTrends.Items.Add(item);
                    var param = chartTrends.Series[i].LegendText;
                    var points = param.StartsWith("HR") ? "" : param.StartsWith("Ток") ? "мА" : "мм";
                    var scale = param.StartsWith("HR") ? "булев" : param.StartsWith("Ток") ? "4..20" : "0.." + ScaleHigh;
                    var desc = chartTrends.Series[i].ToolTip;
                    if (desc.StartsWith(string.Concat(param, " - "))) desc = desc.Substring(param.Length + 3);
                    item.SubItems.Add(param); // имя тренда
                    item.SubItems.Add(String.Empty); // текущее значение
                    item.SubItems.Add(points); // ед.изм
                    item.SubItems.Add(scale); // шкала
                    item.SubItems.Add(desc); // дескриптор
                }
            }
            finally
            {
                lvTrends.ItemChecked += lvTrends_ItemChecked;
            }
        }

/*
        private void AddSwitchToTrend(IDictionary<string, Tuple<string, SortedList<DateTime, bool>>> switchdata,
                                      string bitname, Color colorGroup, int visualLevel, int visualHeight)
        {
            if (!switchdata.ContainsKey(bitname)) return;
            var swd = switchdata[bitname];
            var descriptor = swd.Item1;
            var n = AddToSeries("Legend2", colorGroup, 2, SeriesChartType.StepLine, ChartDashStyle.Solid, bitname,
                                bitname + " - " + descriptor);
            var list = swd.Item2;
            var state = false;
            foreach (var value in list)
            {
                state = value.Value;
                AddDataPoint(n, value, visualLevel, visualLevel + visualHeight);
            }
            // добавляем точку справа, чтобы линия тренда заканчивалась на границе
            if (_offset <= 0)
                AddDataPoint(n, new KeyValuePair<DateTime, bool>(DateTime.Now.AddSeconds(-1), state), visualLevel,
                             visualLevel + visualHeight);
        }
*/

/*
        private void AddDataPoint(int seriesIndex, KeyValuePair<DateTime, bool> value, int low, int high)
        {
            var pt = new DataPoint(chartTrends.Series[seriesIndex]);
            pt.SetCustomProperty("Value", (value.Value ? "1" : "0"));
            pt.SetValueXY(value.Key, value.Value ? high : low);
            chartTrends.Series[seriesIndex].Points.Add(pt);
        }
*/

        private void AddToSeries(string legend, Color color, int lineWidth, SeriesChartType chartType, 
            ChartDashStyle dashStyle, string legendText, string toolTip)
        {
            var series = new Series
                {
                    BorderWidth = lineWidth,
                    ChartArea = "ChartArea1",
                    Color = color,
                    ChartType = chartType,
                    BorderDashStyle = dashStyle,
                    Legend = legend,
                    LegendText = legendText,
                    LegendToolTip = toolTip,
                    Name = legendText, 
                    ToolTip = toolTip,
                    XValueType = ChartValueType.DateTime,
                    YAxisType = AxisType.Secondary,
                    YValueType = ChartValueType.Int32,                    
                };
            chartTrends.Series.Add(series);
        }

        private void AddCursorLine(DateTime dateTime)
        {
            var series = new Series
                {
                    BorderWidth = 1,
                    ChartArea = "ChartArea1",
                    Color = Color.Red,
                    ChartType = SeriesChartType.FastLine,
                    BorderDashStyle = ChartDashStyle.Solid,
                    Name = "CursorLine",
                    IsVisibleInLegend = false,
                    XValueType = ChartValueType.DateTime,
                    YAxisType = AxisType.Secondary,
                    YValueType = ChartValueType.Int32
                };
            chartTrends.Series.Add(series);
            AddCursorLinePoints(dateTime);
        }

        private void AddCursorLinePoints(DateTime dateTime)
        {
            chartTrends.Series["CursorLine"].Points.Clear();
            var pt = new DataPoint(chartTrends.Series["CursorLine"]);
            pt.SetValueXY(dateTime, 0);
            chartTrends.Series["CursorLine"].Points.Add(pt);
            pt = new DataPoint(chartTrends.Series["CursorLine"]);
            pt.SetValueXY(dateTime, 3300);
            chartTrends.Series["CursorLine"].Points.Add(pt);
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            Offset = 0;
            ClearDateCursor();
            tsbNextTime.Enabled = false;
            UpdateChart();
        }

        private void MiTimeSelectClick(object sender, EventArgs e)
        {
            tsbNextTime.Enabled = false;
            var item = (ToolStripDropDownItem)sender;
            tsbTimeSelect.Text = item.Text;
            _minutes = int.Parse((string)item.Tag);
            Data.Session.WriteInteger("TrendShowed" + DisplayIndex, "TimeRange", _minutes);
            Zoomminutes = _minutes;
            Offset = 0;
            ClearDateCursor();
            UpdateChart();
        }

        private void tsbPrevTime_Click(object sender, EventArgs e)
        {
            tsbNextTime.Enabled = true;
            Offset += _minutes / 8;
            ClearDateCursor();
            UpdateChart();
        }

        private void TsbNextTimeClick(object sender, EventArgs e)
        {
            if (Offset > 0)
            {
                Offset -= _minutes / 8;
                if (Offset <= 0)
                {
                    Offset = 0;
                    tsbNextTime.Enabled = false;
                }
            }
            ClearDateCursor();
            UpdateChart();
        }

        private void ClearDateCursor()
        {
            CursorPosition = DateTime.MinValue;
            tslCursorPositionDate.Text = @"Курсор не установлен";
            tsbZoomIn.Enabled = false;
            tsbZoomOut.Enabled = false;
            tsbNoZoom.Enabled = false;
        }

        private void tvNavigator_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;
            if (node == null) return;
            if (node.Tag == null) return;
            var channelIndexes = node.Tag.ToString().Split(new[] { ';' });
            var riserList = new List<RiserAddress>();
            foreach (var channelIndex in channelIndexes)
            {
                int index;
                if (!int.TryParse(channelIndex, out index)) continue;
                lock (Data.ChannelNodes)
                {
                    if (index < 0 || index >= Data.ChannelNodes.Count) continue;
                    var channel = Data.ChannelNodes[index];
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
            var riser = Data.Session.ReadInteger("TrendShowed" + DisplayIndex, "Riser", 0);
            var addrIndex = -1;
            var n = 0;
            cbRisers.Items.Clear();
            foreach (var addr in riserList.OrderBy(item => item.Riser))
            {
                cbRisers.Items.Add(addr);
                if (addr.Riser == riser) addrIndex = n;
                n++;
            }
            if (cbRisers.Items.Count > 0 && cbRisers.SelectedItem == null)
                cbRisers.SelectedItem = addrIndex < 0 ? cbRisers.Items[0] : cbRisers.Items[addrIndex];
        }

        private void cbRisers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRisers.SelectedItem == null) return;
            var addr = (RiserAddress) cbRisers.SelectedItem;
            Data.Session.WriteInteger("TrendShowed" + DisplayIndex, "Riser", addr.Riser);
            int channel;
            lock (Data.RiserNodes)
            {
                channel = Data.RiserNodes[addr].Channel;
            }
            string descriptor;
            lock (Data.ChannelNodes)
            {
                descriptor = Data.ChannelNodes[channel].Descriptor;
            }
            lbGroupCaption.Text = descriptor;
            //"N235D053"
            _riserName = string.Concat("N", addr.Overpass, addr.Way, addr.Product, addr.Riser.ToString("000"));
            fileSystemWatcher1.Filter = _riserName + "*.trd";
            UpdateChart();
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            if (Offset <= 0 && Zoomminutes == _minutes) UpdateChart();
        }

        private void chartTrends_MouseDown(object sender, MouseEventArgs e)
        {
            var result = chartTrends.HitTest(e.X, e.Y);
            if (result.ChartElementType != ChartElementType.DataPoint) return;
            var index = result.PointIndex;
            var pt = result.Series.Points[index];
            CursorPosition = DateTime.FromOADate(pt.XValue);
            var lvi = lvTrends.FindItemWithText(result.Series.Name);
            if (lvi != null)
            {
                lvTrends.FocusedItem = lvi;
                lvi.Selected = true;
                lvTrends.EnsureVisible(lvi.Index);
                _lastSelected = result.Series.Name;
            }
            // заполнение остальных значений
            FillCursorsData(CursorPosition);
            tslCursorPositionDate.Text = @"Позиция курсора: " + CursorPosition.ToString("dd.MM.yy HH:mm.ss.fff");
            AddCursorLinePoints(CursorPosition);
            tsbZoomIn.Enabled = true;
            tsbZoomOut.Enabled = true;
        }

        private void FillCursorsData(DateTime snaptime)
        {
            foreach (var seria in chartTrends.Series)
            {
                if (!seria.IsVisibleInLegend) continue;
                var diff = double.MaxValue;
                var res = 0.0;
                var found = false;
                string customValue = null;
                var xvalue = snaptime.ToOADate();
                foreach (var dpt in seria.Points)
                {
                    var curr = Math.Abs(dpt.XValue - xvalue);
                    if (!(curr < diff)) continue;
                    diff = curr;
                    res = dpt.YValues[0];
                    customValue = dpt.GetCustomProperty("Value");
                    found = true;
                }
                var lvi = lvTrends.FindItemWithText(seria.Name);
                if (lvi == null) continue;
                if (found)
                    lvi.SubItems[2].Text = customValue ?? res.ToString("0");
                else
                    lvi.SubItems[2].Text = "";
            }
        }

        private void tsbZoomIn_Click(object sender, EventArgs e)
        {
            var dateLowRange = DateTime.FromOADate(chartTrends.ChartAreas[0].AxisX.Minimum);
            var dateHighRange = DateTime.FromOADate(chartTrends.ChartAreas[0].AxisX.Maximum);
            if (CursorPosition <= dateLowRange || CursorPosition >= dateHighRange) return;
            if (Zoomminutes <= 1) return;
            Zoomminutes -= Zoomminutes / 2;
            dateHighRange = CursorPosition.AddMinutes(Zoomminutes);
            dateLowRange = CursorPosition.AddMinutes(-Zoomminutes);
            UpdateChart(dateLowRange, dateHighRange);
            tsbNoZoom.Enabled = true;
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            var dateLowRange = DateTime.FromOADate(chartTrends.ChartAreas[0].AxisX.Minimum);
            var dateHighRange = DateTime.FromOADate(chartTrends.ChartAreas[0].AxisX.Maximum);
            if (CursorPosition <= dateLowRange || CursorPosition >= dateHighRange) return;
            if (Zoomminutes >= _minutes) return;
            Zoomminutes += Zoomminutes * 2;
            if (Zoomminutes > _minutes) Zoomminutes = _minutes;
            dateHighRange = CursorPosition.AddMinutes(Zoomminutes);
            dateLowRange = CursorPosition.AddMinutes(-Zoomminutes);
            UpdateChart(dateLowRange, dateHighRange);
            tsbNoZoom.Enabled = true;
        }

        private void tsbNoZoom_Click(object sender, EventArgs e)
        {
            var dateLowRange = DateTime.FromOADate(chartTrends.ChartAreas[0].AxisX.Minimum);
            var dateHighRange = DateTime.FromOADate(chartTrends.ChartAreas[0].AxisX.Maximum);
            if (CursorPosition <= dateLowRange || CursorPosition >= dateHighRange) return;
            Zoomminutes = _minutes;
            UpdateChart();
            tsbNoZoom.Enabled = false;
        }

        private void lvTrends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvTrends.SelectedItems.Count == 0) return;
            _lastSelected = lvTrends.SelectedItems[0].Name;
        }

        private void lvTrends_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            e.Item.Selected = true;
            Data.Session.WriteBool("TrendShowed" + DisplayIndex, e.Item.Name, e.Item.Checked);
            UpdateChart();
        }

    }
}
