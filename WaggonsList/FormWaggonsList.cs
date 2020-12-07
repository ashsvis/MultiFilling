using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MultiFilling.WaggonsList
{
    public partial class FormWaggonsList : Form
    {
        public int DisplayIndex { get; set; }
        private readonly int _recordCount;
        private int _rowIndex = -1;

        public FormWaggonsList()
        {
            InitializeComponent();
            _recordCount = 15;
        }

        private void FormWaggonsList_Load(object sender, EventArgs e)
        {
            var count = WaggonDataKeeper.Count();
            vScrollBar1.Maximum = count > 0 ? count - 1 : 0;
            vScrollBar1.LargeChange = _recordCount;
            UpdateWaggonsList();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateWaggonsList();
        }

        private void UpdateWaggonsList(int rowindex = -1)
        {
            var table = new DataTable();
            table.Columns.Add(new DataColumn("Номер вагона-цистерны"));
            table.Columns.Add(new DataColumn("Тип"));
            table.Columns.Add(new DataColumn("Фактическая высота"));
            foreach (var vals in WaggonDataKeeper.GetLineItems(vScrollBar1.Value, _recordCount)
                .Select(line => line.Split(new[] { '\t' })).Where(vals => vals.Length == 3))
            {
                table.Rows.Add(vals[0], vals[1], vals[2]);
            }
            dataGridView1.DataSource = table;
            if (rowindex >= 0 && dataGridView1.Rows.Count > 0 && rowindex < dataGridView1.Rows.Count)
                dataGridView1.CurrentCell = dataGridView1[0, rowindex];
            btnChangeType.Enabled = dataGridView1.Rows.Count > 0 && _rowIndex >= 0;
            btnDeleteType.Enabled = dataGridView1.Rows.Count > 0 && _rowIndex >= 0;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            miEditWaggon.Enabled = _rowIndex >= 0;
            miDeleteWaggon.Enabled = _rowIndex >= 0;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var rowindex = _rowIndex = e.RowIndex;
                if (rowindex >= 0 && dataGridView1.Rows.Count > 0 && rowindex < dataGridView1.Rows.Count)
                    dataGridView1.CurrentCell = dataGridView1[0, rowindex];
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    ShowWaggonInsertDialog();
                    break;
                case Keys.Delete:
                    ShowWaggonDeleteDialog();
                    break;
                case Keys.Return:
                    ShowWaggonEditDialog(_rowIndex);
                    break;
                case  Keys.PageUp:
                    if (_rowIndex == 0 && vScrollBar1.Value > 0)
                    {
                        if (vScrollBar1.Value - _recordCount < 0)
                            vScrollBar1.Value = 0;
                        else
                            vScrollBar1.Value -= _recordCount;
                        UpdateWaggonsList();
                    }
                    break;
                case Keys.Home:
                        vScrollBar1.Value = 0;
                        UpdateWaggonsList();
                    break;
                case Keys.Up:
                    if (_rowIndex == 0 && vScrollBar1.Value > 0)
                    {
                        vScrollBar1.Value--;
                        UpdateWaggonsList();
                    }
                    break;
                case Keys.Down:
                    if (_rowIndex == dataGridView1.Rows.Count - 1 &&
                        vScrollBar1.Value < vScrollBar1.Maximum - _recordCount + 1)
                    {
                        vScrollBar1.Value++;
                        UpdateWaggonsList(dataGridView1.Rows.Count-1);
                    }
                    break;
                case Keys.PageDown:
                    if (_rowIndex == dataGridView1.Rows.Count - 1 &&
                        vScrollBar1.Value < vScrollBar1.Maximum - _recordCount + 1)
                    {
                        vScrollBar1.Value += _recordCount;
                        UpdateWaggonsList(dataGridView1.Rows.Count-1);
                    }
                    break;
                case Keys.End:
                    vScrollBar1.Value = vScrollBar1.Maximum - _recordCount + 1;
                    UpdateWaggonsList(dataGridView1.Rows.Count-1);
                    break;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowWaggonEditDialog(e.RowIndex);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _rowIndex = e.RowIndex;
        }

        private void ShowWaggonInsertDialog()
        {
            using (var frm = new FormWaggonDataEditor(false, "", "", 0))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                var resultwag = frm.GetValue;
                if (resultwag == null) return;
                var index = WaggonDataKeeper.Find(resultwag.Number);
                if (index >= 0)
                {
                    MessageBox.Show(this, @"Вагон с номером " + resultwag.Number + @" уже существует!",
                                    @"Новый вагон", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    vScrollBar1.Value = index;
                    UpdateWaggonsList(0);
                    return;   
                }
                if (!WaggonDataKeeper.Add(resultwag.Number, resultwag.Ntype, resultwag.FactHeight)) return;
                var count = WaggonDataKeeper.Count();
                vScrollBar1.Maximum = count > 0 ? count - 1 : 0;
                index = WaggonDataKeeper.Find(resultwag.Number);
                if (index < 0) return;
                vScrollBar1.Value = index;
                UpdateWaggonsList(0);
            }
        }

        private void ShowWaggonEditDialog(int rowIndex)
        {
            if (rowIndex < 0) return;
            int factlevel;
            var number = dataGridView1[0, _rowIndex].Value.ToString();
            var ntype = dataGridView1[1, _rowIndex].Value.ToString();
            int.TryParse(dataGridView1[2, _rowIndex].Value.ToString(), out factlevel);
            using (var frm = new FormWaggonDataEditor(true, number, ntype, factlevel))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                var resultwag = frm.GetValue;
                if (resultwag == null) return;
                if (!WaggonDataKeeper.Edit(resultwag.Number, resultwag.Ntype, resultwag.FactHeight)) return;
                UpdateWaggonsList(_rowIndex);
            }
        }

        private void ShowWaggonDeleteDialog()
        {
            if (_rowIndex < 0) return;
            var number = dataGridView1[0, _rowIndex].Value.ToString();
            if (MessageBox.Show(@"Удалить запись о цистерне " + number + @"-" + dataGridView1[1, _rowIndex].Value + @" ?",
                                                 @"Подтверждение удаления", MessageBoxButtons.OKCancel,
                                                 MessageBoxIcon.Warning,
                                                 MessageBoxDefaultButton.Button2) != DialogResult.OK) return;
            WaggonDataKeeper.Delete(number);
            UpdateWaggonsList();
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            ShowWaggonInsertDialog();
        }

        private void btnChangeType_Click(object sender, EventArgs e)
        {
            ShowWaggonEditDialog(_rowIndex);
        }

        private void btnDeleteType_Click(object sender, EventArgs e)
        {
            ShowWaggonDeleteDialog();
        }

    }
}
