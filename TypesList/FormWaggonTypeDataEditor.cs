using System;
using System.Windows.Forms;

namespace MultiFilling.TypesList
{
    public partial class FormWaggonTypeDataEditor : Form
    {
        public FormWaggonTypeDataEditor(bool edit, string ntype, int diameter, int throat, int deflevel)
        {
            InitializeComponent();
            Text = edit ? "Редактировать тип цистерны" : "Новый тип цистерны";
            tbNtype.Text = ntype;
            tbNtype.Enabled = !edit;
            lbNtype.Enabled = !edit;
            tbDiameter.Text = diameter > 0 ? diameter.ToString("0") : "";
            tbThroat.Text = throat > 0 ? throat.ToString("0") : "";
            tbDefLevel.Text = deflevel > 0 ? deflevel.ToString("0") : "";
        }

        private void tbNtype_TextChanged(object sender, EventArgs e)
        {
            CheckData();
        }

        private void tbDefLevel_TextChanged(object sender, EventArgs e)
        {
            CheckData();
            tbDefLevel_Validated(null, null);
        }

        private void CheckData()
        {
            int ntype, diameter, throat, deflevel;
            if (int.TryParse(tbNtype.Text, out ntype) &&
                int.TryParse(tbDiameter.Text, out diameter) &&
                int.TryParse(tbThroat.Text, out throat) &&
                int.TryParse(tbDefLevel.Text, out deflevel) &&
                ntype > 10 && deflevel >= 0 && diameter > 2500 && throat > 0 &&
                deflevel < diameter)
            {
                btnOk.Enabled = true;
            }
            else
            {
                btnOk.Enabled = false;
            }
        }

        public TypeData GetValue
        {
            get
            {
                int ntype, diameter, throat, deflevel;
                if (int.TryParse(tbNtype.Text, out ntype) &&
                    int.TryParse(tbDiameter.Text, out diameter) &&
                    int.TryParse(tbThroat.Text, out throat) &&
                    int.TryParse(tbDefLevel.Text, out deflevel) &&
                    ntype >= 10 && ntype <= 999 && deflevel >= 0 &&
                    diameter >= 2800 && diameter <= 3400 &&
                    throat >= 20 && throat <= 300 &&
                    deflevel < diameter)
                {
                    return new TypeData(ntype.ToString("0"), diameter, throat, deflevel);
                }
                return null;
            }
        }

        private void tbNtype_Validated(object sender, EventArgs e)
        {
            int ntype;
            if (int.TryParse(tbNtype.Text, out ntype) &&
                ntype >= 10 && ntype <= 999)
                errorProvider1.SetError(tbNtype, string.Empty);
            else
                errorProvider1.SetError(tbNtype, "Ожидалось двузначное или трехзначное число типа");
        }

        private void tbDiameter_Validated(object sender, EventArgs e)
        {
            int diameter;
            if (int.TryParse(tbDiameter.Text, out diameter) &&
                diameter >= 2800 && diameter <= 3400)
                errorProvider1.SetError(tbDiameter, string.Empty);
            else
                errorProvider1.SetError(tbDiameter, "Ожидалось значение в диапазоне [2800..3400] мм");
        }

        private void tbThroat_Validated(object sender, EventArgs e)
        {
            int throat;
            if (int.TryParse(tbThroat.Text, out throat) &&
                throat >= 20 && throat <= 300)
                errorProvider1.SetError(tbThroat, string.Empty);
            else
                errorProvider1.SetError(tbThroat, "Ожидалось значение в диапазоне [20..300] мм");
        }

        private void tbDefLevel_Validated(object sender, EventArgs e)
        {
            int deflevel;
            if (int.TryParse(tbDefLevel.Text, out deflevel) &&
                deflevel >= 0)
                errorProvider1.SetError(tbDefLevel, string.Empty);
            else
                errorProvider1.SetError(tbDefLevel, "Ожидалось целое положительное число");
        }
    }
}
