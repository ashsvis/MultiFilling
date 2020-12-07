using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MultiFilling.SystemStatus;

namespace MultiFilling
{
    public partial class UcFillinglogPage : UserControl, IUserControlMisc
    {
        private int _viewPos, _viewCount = 34;

        public UcFillinglogPage()
        {
            InitializeComponent();
            lvLogView.SetDoubleBuffered(true);
        }

        public int DisplayIndex { get; set; }

        public void Loaded()
        {
            CalcRowsCount(lvLogView);
            Update();
            LoadLog(_viewPos, _viewCount);
            tsbBackward.Enabled = lvLogView.Items.Count >= _viewCount;
            Data.OnUpdateWorkLogFilter += Data_OnUpdateWorkLogFilter;
            Data.OnUpdateFillingLog += Data_OnUpdateFillingLog;
        }

        void Data_OnUpdateFillingLog(object sender, EventArgs e)
        {
            if (!tsbEnd.Enabled)
                SelectEndRecords();
        }

        public void Unload()
        {
            Data.OnUpdateFillingLog -= Data_OnUpdateFillingLog;
            Data.OnUpdateWorkLogFilter -= Data_OnUpdateWorkLogFilter;
        }
        readonly List<ListViewItem> _reportrows = new List<ListViewItem>();

        enum GoalRecord
        {
            FirstRecord,
            LastRecord
        }

        private void GotoRecord(GoalRecord goal)
        {
            if (lvLogView.Items.Count <= 0) return;
            var item = goal == GoalRecord.FirstRecord ? lvLogView.Items[0] : lvLogView.Items[lvLogView.Items.Count - 1];
            item.Selected = true;
            lvLogView.FocusedItem = item;
            item.EnsureVisible();
        }

        private void SelectEndRecords()
        {
            _viewPos = 0;
            LoadLog(_viewPos, _viewCount);
            GotoRecord(GoalRecord.LastRecord);
            tsbForward.Enabled = false;
            tsbEnd.Enabled = false;
            tsbBackward.Enabled = lvLogView.Items.Count >= _viewCount;
        }

        private void LoadLog(int pos, int count, bool print = false)
        {
            if (pos < 0) return;
            var dateBefore = DateTime.Now.AddYears(-1);
            CalcRowsCount(lvLogView);
            int linescount;
            var results = Data.GetFillLogRecords(pos, count, dateBefore, out linescount, true);
            if (count <= 0) return;
            _reportrows.Clear();
            var row = 0;
            foreach (string[] rec in results)
            {
                if (rec.Length != 13) continue;
                var item = new ListViewItem();
                if (row % 2 != 0)
                    item.BackColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
                var startdatetime = rec[0];
                var enddatetime = rec[1];
                item.Text = GetDateStr(enddatetime);
                var overpass = rec[3];
                var way = rec[4];
                var product = Data.GetFineProductName(rec[5]);
                var riser = rec[6];
                var addr = string.Join("", rec, 1, 5).Trim().Length > 0
                               ? string.Format("Эстакада {0}. Путь {1}. {2}. Стояк {3}",
                                               overpass, way, product, riser) : "";
                item.SubItems.Add(addr);
                var waggon = rec[7];
                item.SubItems.Add(waggon);
                var type = rec[8];
                item.SubItems.Add(type);
                var setpoint = rec[9];
                item.SubItems.Add(setpoint);
                item.SubItems.Add(GetTimeStr(startdatetime));
                item.SubItems.Add(GetTimeStr(enddatetime));
                var filled = rec[10];
                item.SubItems.Add(filled);
                var user = rec[11];
                item.SubItems.Add(user);
                var mess = rec[12];
                item.SubItems.Add(mess.EndsWith("автоматически") ? "" : mess);
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

        private static string GetDateStr(string date)
        {
            var adate = date.Split(new[] {' '})[0].Split(new [] { '-' });
            return adate.Length == 3 ? string.Format("{2}.{1}.{0}", adate[0], adate[1], adate[2]) : "";
        }

        private static string GetTimeStr(string time)
        {
            var atime = time.Split(new[] { ' ', '.' });
            return atime.Length == 3 ? atime[1] : "";
        }

        private void CalcRowsCount(ListView lv)
        {   // Автоматический подсчет количества строк, которые умещаются в ListView без прокрутки
            var hasrows = lv.Items.Count > 0;
            if (!hasrows) lv.Items.Add("0");
            var itemHeight = lv.GetItemRect(0).Height * 1.0;
            _viewCount = Convert.ToInt32(Math.Truncate(lv.ClientSize.Height / itemHeight) - 1);
            if (!hasrows) lv.Items.Clear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SelectEndRecords();
        }

        private void tsbBackward_Click(object sender, EventArgs e)
        {
            _viewPos += _viewCount;
            LoadLog(_viewPos, _viewCount);
            GotoRecord(GoalRecord.FirstRecord);
            tsbBackward.Enabled = lvLogView.Items.Count >= _viewCount;
            tsbForward.Enabled = true;
            tsbEnd.Enabled = true;
        }

        private void tsbForward_Click(object sender, EventArgs e)
        {
            if (_viewPos - _viewCount >= 0)
            {
                _viewPos -= _viewCount;
                LoadLog(_viewPos < 0 ? 0 : _viewPos, _viewCount);
                GotoRecord(GoalRecord.LastRecord);
                tsbEnd.Enabled = tsbForward.Enabled;
                tsbBackward.Enabled = lvLogView.Items.Count >= _viewCount;
            }
            else
            {
                tsbForward.Enabled = false;
                tsbEnd.Enabled = false;
            }
        }

        private void tsbEnd_Click(object sender, EventArgs e)
        {
            SelectEndRecords();
        }

        private void tsbFilterEdit_Click(object sender, EventArgs e)
        {
            var parent = Parent;
            while (parent != null)
            {
                if (parent is Form)
                {
                    Data.ShowWorklogFilterEditor(parent);
                    break;
                }
                parent = parent.Parent;
            }

        }

        void Data_OnUpdateWorkLogFilter(object sender, EventArgs e)
        {
            SelectEndRecords();
        }
    }
}
