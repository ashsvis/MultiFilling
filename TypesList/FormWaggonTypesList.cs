using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MultiFilling.TypesList
{
    public partial class FormWaggonTypesList : Form
    {
        private int _rowIndex = -1;

        public int DisplayIndex { get; set; }

        public FormWaggonTypesList()
        {
            InitializeComponent();
        }

        private void FormWaggonTypesList_Load(object sender, EventArgs e)
        {
            UpdateWaggonTypesList();
        }

        private void UpdateWaggonTypesList(int rowindex = -1)
        {
            var table = new DataTable();
            table.Columns.Add(new DataColumn("Тип"));
            table.Columns.Add(new DataColumn("Диаметр"));
            table.Columns.Add(new DataColumn("Высота горловины"));
            table.Columns.Add(new DataColumn("Взлив по умолчанию"));
            foreach (var wagtype in TypeDataKeeper.GetWaggonTypeItems().OrderBy(item => int.Parse(item.NType)))
            {
                table.Rows.Add(wagtype.NType, wagtype.Diameter, wagtype.Throat, wagtype.Deflevel);
            }
            dataGridView1.DataSource = table;

            if (rowindex >= 0 && dataGridView1.Rows.Count > 0 && rowindex < dataGridView1.Rows.Count)
                dataGridView1.CurrentCell = dataGridView1[0, rowindex];
            btnChangeType.Enabled = dataGridView1.Rows.Count > 0 && _rowIndex >= 0;
            btnDeleteType.Enabled = dataGridView1.Rows.Count > 0 && _rowIndex >= 0;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowWaggonTypeEditDialog(e.RowIndex);
        }

        private void ShowWaggonTypeInsertDialog()
        {
            using (var frm = new FormWaggonTypeDataEditor(false,
                                                          "", 0, 0, 0))
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                var resultwagtype = frm.GetValue;
                if (resultwagtype == null) return;
                TypeDataKeeper.Add(resultwagtype.NType, resultwagtype.Diameter, resultwagtype.Throat,
                                    resultwagtype.Deflevel);
                var index = TypeDataKeeper.FindIndex(resultwagtype.NType);
                UpdateWaggonTypesList(index);
            }
        }

        private void ShowWaggonTypeEditDialog(int rowIndex)
        {
            if (rowIndex < 0) return;
            var n = 0;
            foreach (var wagtype in TypeDataKeeper.GetWaggonTypeItems().OrderBy(item => int.Parse(item.NType)))
            {
                if (n == rowIndex)
                {
                    using (var frm = new FormWaggonTypeDataEditor(true,
                                                                  wagtype.NType, wagtype.Diameter, wagtype.Throat,
                                                                  wagtype.Deflevel))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            var resultwagtype = frm.GetValue;
                            if (resultwagtype != null)
                            {
                                TypeDataKeeper.Edit(resultwagtype.NType, resultwagtype.Diameter, resultwagtype.Throat,
                                    resultwagtype.Deflevel);
                                var index = TypeDataKeeper.FindIndex(resultwagtype.NType);
                                UpdateWaggonTypesList(index);
                            }
                        }
                    }
                    break;
                }
                n++;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    ShowWaggonTypeInsertDialog();
                    break;
                case Keys.Delete:
                    ShowWaggonTypeDeleteDialog();
                    break;
                case Keys.Return:
                    ShowWaggonTypeEditDialog(_rowIndex);
                    break;
            }
        }

        private void ShowWaggonTypeDeleteDialog()
        {
            if (_rowIndex < 0) return;
            var ntype = TypeDataKeeper.GetWaggonType(_rowIndex);
            if (ntype == null || MessageBox.Show(@"Удалить запись о типе цистерны " + ntype + @" ?",
                                                 @"Подтверждение удаления", MessageBoxButtons.OKCancel,
                                                 MessageBoxIcon.Warning,
                                                 MessageBoxDefaultButton.Button2) != DialogResult.OK) return;
            TypeDataKeeper.Delete(ntype);
            UpdateWaggonTypesList();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _rowIndex = e.RowIndex;
        }

        private void miAddWaggonType_Click(object sender, EventArgs e)
        {
            ShowWaggonTypeInsertDialog();
        }

        private void miEditWaggonType_Click(object sender, EventArgs e)
        {
            ShowWaggonTypeEditDialog(_rowIndex);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            miEditWaggonType.Enabled = _rowIndex >= 0;
            miDeleteWaggonType.Enabled = _rowIndex >= 0;
        }

        private void miDeleteWaggonType_Click(object sender, EventArgs e)
        {
            ShowWaggonTypeDeleteDialog();
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


    }
}
