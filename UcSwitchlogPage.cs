using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MultiFilling.SystemStatus;

namespace MultiFilling
{
    public partial class UcSwitchlogPage : UserControl, IUserControlMisc
    {
        private int _viewPos, _viewCount = 34;

        public int DisplayIndex { get; set; }

        public UcSwitchlogPage()
        {
            InitializeComponent();
            lvLogView.SetDoubleBuffered(true);
        }

        public void Loaded()
        {
            CalcRowsCount(lvLogView);
            Update();
            LoadLog(_viewPos, _viewCount);
            tsbBackward.Enabled = lvLogView.Items.Count >= _viewCount;
            Data.OnUpdateWorkLogFilter += Data_OnUpdateSwitchlogFilter;
            Data.OnUpdateSwitchLog += Data_OnUpdateSwitchLog;
        }

        void Data_OnUpdateSwitchLog(object sender, EventArgs e)
        {
            if (!tsbEnd.Enabled)
                SelectEndRecords();
        }

        public void Unload()
        {
            Data.OnUpdateSwitchLog -= Data_OnUpdateSwitchLog;
            Data.OnUpdateWorkLogFilter -= Data_OnUpdateSwitchlogFilter;
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
            var results = Data.GetSwitchLogRecords(pos, count, dateBefore, out linescount, true);
            if (count <= 0) return;
            _reportrows.Clear();
            var row = 0;
            foreach (string[] rec in results)
            {
                if (rec.Length != 11) continue;
                var item = new ListViewItem(rec[0]);
                if (row % 2 != 0)
                    item.BackColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
                var overpass = rec[2];
                var way = rec[3];
                var product = Data.GetFineProductName(rec[4]);
                var riser = rec[5];
                var addr = string.Join("", rec, 1, 5).Trim().Length > 0
                               ? string.Format("Эстакада {0}. Путь {1}. {2}. Стояк {3}",
                                               overpass, way, product, riser) : "";
                item.SubItems.Add(addr);
                var param = rec[7];
                item.SubItems.Add(param);
                var oldstate = rec[8];
                item.SubItems.Add(oldstate);
                var newstate = rec[9];
                item.SubItems.Add(newstate);
                var desc = rec[10];
                item.SubItems.Add(desc);
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

        void Data_OnUpdateSwitchlogFilter(object sender, EventArgs e)
        {
            SelectEndRecords();
        }

    }
}
