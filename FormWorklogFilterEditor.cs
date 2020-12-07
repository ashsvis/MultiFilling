using System;
using System.Linq;
using System.Windows.Forms;

namespace MultiFilling
{
    public partial class FormWorklogFilterEditor : Form
    {
        public FormWorklogFilterEditor()
        {
            InitializeComponent();
            foreach (var message in _messages)
            {
                var lvi = lvEvents.Items.Add(message);
                lvi.Checked = Data.FilterEventsList.Contains(message);
            }
            var dt1 = Data.FilterDateTimeRange.Item1;
            var dt2 = Data.FilterDateTimeRange.Item2;
            cbStartedTime.Checked = dt1 != Data.MinRangeDate();
            try
            {
                dtpStartDate.Value = cbStartedTime.Checked
                                         ? new DateTime(dt1.Year, dt1.Month, dt1.Day)
                                         : Data.MinRangeDate();
                dtpStartTime.Value = cbStartedTime.Checked
                                         ? new DateTime(dt1.Year, dt1.Month, dt1.Day, dt1.Hour, dt1.Minute, dt1.Second)
                                         : Data.MinRangeDate();
            }
            catch (Exception)
            {
                dtpStartDate.Value = Data.MinRangeDate();
                dtpStartTime.Value = Data.MinRangeDate();
            }
            cbEndedTime.Checked = dt2 != Data.MaxRangeDate();
            try
            {
                dtpEndDate.Value = cbEndedTime.Checked
                                       ? new DateTime(dt2.Year, dt2.Month, dt2.Day)
                                       : Data.MaxRangeDate();
                dtpEndTime.Value = cbEndedTime.Checked
                                       ? new DateTime(dt2.Year, dt2.Month, dt2.Day, dt2.Hour, dt2.Minute, dt2.Second)
                                       : Data.MaxRangeDate();

            }
            catch (Exception)
            {
                dtpEndDate.Value = Data.MinRangeDate();
                dtpEndTime.Value = Data.MinRangeDate();
            }
            var overpass = Data.FilterOverpassList;
            foreach (var item in lvOverpass.Items.Cast<ListViewItem>())
                item.Checked = overpass.Contains(item.Tag.ToString());
            var way = Data.FilterWayList;
            foreach (var item in lvWay.Items.Cast<ListViewItem>())
                item.Checked = way.Contains(item.Tag.ToString());
            var product = Data.FilterProductList;
            foreach (var item in lvProduct.Items.Cast<ListViewItem>())
                item.Checked = product.Contains(item.Tag.ToString());
            for (var i = 1; i <= 247; i++)
            {
                cbStartRisers.Items.Add(i.ToString("0"));
                cbEndRisers.Items.Add(i.ToString("0"));
            }
            var risers = Data.FilterRiserRange;
            cbStartRisers.Text = risers.Item1.ToString("0");
            cbEndRisers.Text = risers.Item2.ToString("0");

        }

        public DateTime GetStartDateTime()
        {
            if (cbStartedTime.Checked)
                return new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day,
                    dtpStartTime.Value.Hour, dtpStartTime.Value.Minute, dtpStartTime.Value.Second);
            return Data.MinRangeDate();
        }

        public DateTime GetEndDateTime()
        {
            if (cbEndedTime.Checked)
                return new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day,
                    dtpEndTime.Value.Hour, dtpEndTime.Value.Minute, dtpEndTime.Value.Second);
            return Data.MaxRangeDate();
        }

        public string[] GetSelectedEvents()
        {
            var list = (from lvi in lvEvents.Items.Cast<ListViewItem>() where lvi.Checked select lvi.Text).ToList();
            return list.ToArray();
        }

        public string[] GetSelectedOverpasses()
        {
            var list = (from lvi in lvOverpass.Items.Cast<ListViewItem>() where lvi.Checked select lvi.Tag.ToString()).ToArray();
            return list;
        }

        public string[] GetSelectedWays()
        {
            var list = (from lvi in lvWay.Items.Cast<ListViewItem>() where lvi.Checked select lvi.Tag.ToString()).ToArray();
            return list;
        }

        public string[] GetSelectedProducts()
        {
            var list = (from lvi in lvProduct.Items.Cast<ListViewItem>() where lvi.Checked select lvi.Tag.ToString()).ToArray();
            return list;
        }

        public int GetFirstRiser()
        {
            return int.Parse(cbStartRisers.Text);
        }

        public int GetLastRiser()
        {
            return int.Parse(cbEndRisers.Text);
        }

        private readonly string[] _messages = new[]
            {
                "Налив завершен аварийно. Сработал сигнализатор аварийный",
                "Налив завершен аварийно. Неисправность цепи готовности",
                "Налив завершен аварийно. Неисправность сигнализатора уровня",
                "Налив завершен аварийно. Истекло время работы без связи",
                "Налив завершен аварийно. Заземление отсутствует",
                "Налив завершен аварийно. Ошибка клапана большого прохода",
                "Налив завершен аварийно. Ошибка клапана малого прохода",
                "Налив завершен аварийно. Ток сигнализатора уровня меньше минимального",
                "Налив завершен аварийно. Ток сигнализатора уровня больше максимального",
                "Налив завершен аварийно. Ток сигнализатора аварийного меньше минимального",
                "Налив завершен аварийно. Ток сигнализатора аварийного больше максимального",
                "Налив завершен аварийно. Нет рабочего положения",
                "Налив завершен аварийно. Неверные данные налива",
                "Запуск налива",
                "Налив завершен автоматически",
                "Налив завершен оператором АРМ",
                "Налив завершен кнопкой \"СТОП\" пульта управления",
                "Запуск системы",
                "Останов системы",
                "Вход в систему",
                "Выход из системы",
                "Установка соединения",
                "Обрыв соединения"
            };

        private void btnSelectAllEvents_Click(object sender, EventArgs e)
        {
            foreach (var lvi in lvEvents.Items.Cast<ListViewItem>())
                lvi.Checked = true;
            DataChanged(null, null);
        }

        private void btnClearAllEvents_Click(object sender, EventArgs e)
        {
            foreach (var lvi in lvEvents.Items.Cast<ListViewItem>())
                lvi.Checked = false;
            DataChanged(null, null);
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            var dt1 = DateTime.Now;
            dtpStartDate.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            dtpStartTime.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            dtpEndDate.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 23, 59, 59);
            dtpEndTime.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 23, 59, 59);
            cbStartedTime.Checked = true;
            cbEndedTime.Checked = true;
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            var dt1 = DateTime.Now.AddDays(-1);
            dtpStartDate.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            dtpStartTime.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            dtpEndDate.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 23, 59, 59);
            dtpEndTime.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 23, 59, 59);
            cbStartedTime.Checked = true;
            cbEndedTime.Checked = true;
        }

        private void btnResetRisers_Click(object sender, EventArgs e)
        {
            cbStartRisers.Text = @"1";
            cbEndRisers.Text = @"247";
            DataChanged(null, null);
        }

        private void FormWorklogFilterEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void DataChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Data.UpdateFilterEventsList(GetSelectedEvents());
            Data.UpdateFilterDateTimeRange(GetStartDateTime(), GetEndDateTime());
            Data.UpdateFilterAddress(GetSelectedOverpasses(),
                                     GetSelectedWays(), GetSelectedProducts(), GetFirstRiser(), GetLastRiser());
            Data.WorkLogFilterChanged();
            btnApply.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbStartRisers_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbEndRisers.Text = cbStartRisers.Text;
            btnResetRisers.Enabled = true;
            DataChanged(null, null);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cbStartedTime.Checked = false;
            cbEndedTime.Checked = false;
            dtpStartDate.Value = Data.MinRangeDate();
            dtpStartTime.Value = Data.MinRangeDate();
            dtpEndDate.Value = Data.MaxRangeDate();
            dtpEndTime.Value = Data.MaxRangeDate();
        }

        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            if (dtpStartDate.Value != Data.MinRangeDate() && dtpStartTime.Value != Data.MinRangeDate())
            {
                dtpStartDate.Value -= new TimeSpan(1, 0, 0, 0);
                dtpStartTime.Value -= new TimeSpan(1, 0, 0, 0);
            }
            if (dtpEndDate.Value != Data.MinRangeDate() && dtpEndTime.Value != Data.MinRangeDate())
            {
                dtpEndDate.Value -= new TimeSpan(1, 0, 0, 0);
                dtpEndTime.Value -= new TimeSpan(1, 0, 0, 0);
            }
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            if (dtpStartDate.Value != Data.MaxRangeDate() && dtpStartTime.Value != Data.MaxRangeDate())
            {
                dtpStartDate.Value += new TimeSpan(1, 0, 0, 0);
                dtpStartTime.Value += new TimeSpan(1, 0, 0, 0);
            }
            if (dtpEndDate.Value != Data.MaxRangeDate() && dtpEndTime.Value != Data.MaxRangeDate())
            {
                dtpEndDate.Value += new TimeSpan(1, 0, 0, 0);
                dtpEndTime.Value += new TimeSpan(1, 0, 0, 0);
            }
        }

        private void cbEndRisers_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnResetRisers.Enabled = true;
            DataChanged(null, null);
        }

        private void lvEvents_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            DataChanged(null, null);
        }

        private void FormWorklogFilterEditor_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            btnApply.Enabled = false;
        }
    }
}
