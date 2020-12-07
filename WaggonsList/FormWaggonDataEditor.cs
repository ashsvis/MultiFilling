using System;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;
using MultiFilling.TypesList;

namespace MultiFilling.WaggonsList
{
    public partial class FormWaggonDataEditor : Form
    {
        public FormWaggonDataEditor(bool edit, string number, string ntype, int factlevel)
        {
            InitializeComponent();
            Text = edit ? "Редактировать цистерну" : "Новая цистерна";
            tbNumber.Text = number;
            tbNumber.Enabled = !edit;
            lbNumber.Enabled = !edit;
            foreach (var item in TypeDataKeeper.GetWaggonTypeItems()
                .OrderBy(item => int.Parse(item.NType)))
            {
                cbNtype.Items.Add(item);
            }
            cbNtype.Text = ntype;
            tbFactHeight.Text = factlevel > 0 ? factlevel.ToString("0") : "";
        }


        public WaggonData GetValue
        {
            get
            {
                int number, ntype, factlevel;
                if (int.TryParse(tbNumber.Text, out number) &&
                    int.TryParse(cbNtype.Text, out ntype) &&
                    int.TryParse(tbFactHeight.Text, out factlevel) &&
                    ntype >= 10 && ntype <= 999)
                {
                    return new WaggonData(number.ToString("0"), ntype.ToString("0"), factlevel);
                }
                return null;
            }
        }

        private void FormWaggonDataEditor_Load(object sender, EventArgs e)
        {
            if (tbNumber.Text.Trim().Length > 0)
                errorProvider1.SetError(tbNumber, CheckWaggonNumber(tbNumber.Text));
            if (tbFactHeight.Text.Trim().Length > 0)
                errorProvider1.SetError(tbFactHeight, CheckFactHeight(tbFactHeight.Text));
        }

        private static string CheckWaggonNumber(string number)
        {
            if (number.Trim().Length != 8) return "Ожидался 8-значный номер";
            if (number.Any(ch => !char.IsDigit(ch))) return "Номер содержит нечисловые символы";
            var fp = CultureInfo.GetCultureInfo("en-US");
            var sum = 0;
            var kf = new[] {2, 1, 2, 1, 2, 1, 2};
            for (var i = 1; i <= 7; i++)
            {
                var val = kf[i - 1]*int.Parse(number[i - 1].ToString(fp));
                if (val > 9) val = 1 + (val - 10);
                sum += val;
            }
            var lastDigit = int.Parse(number[7].ToString(fp));
            var text = (sum + lastDigit).ToString("0");
            return text.Length > 0 && text[text.Length - 1] == '0' 
                ? string.Empty
                : "Некорректный номер (не сходится контрольная сумма)";
        }

        private void tbNumber_TextChanged(object sender, System.EventArgs e)
        {
            CheckData();
        }

        private void tbFactHeight_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(tbFactHeight, CheckFactHeight(tbFactHeight.Text));
            CheckData();
        }

        private void CheckData()
        {
            int ntype, number, factlevel;
            if (tbNumber.Text.Trim().Length == 8 &&
                int.TryParse(tbNumber.Text, out number) &&
                int.TryParse(cbNtype.Text, out ntype) &&
                int.TryParse(tbFactHeight.Text, out factlevel) &&
                factlevel > 0)
            {
                btnOk.Enabled = true;
            }
            else
            {
                btnOk.Enabled = false;
            }
        }

        private void tbNumber_Validated(object sender, System.EventArgs e)
        {
            errorProvider1.SetError(tbNumber, CheckWaggonNumber(tbNumber.Text));
        }

        private void cbNtype_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            var typeData = (TypeData) cbNtype.SelectedItem;
            if (typeData == null) return;
            if (tbFactHeight.Text.Trim().Length != 0) return;
            tbFactHeight.Text = (typeData.Diameter + typeData.Throat).ToString("0");
            errorProvider1.SetError(tbFactHeight, CheckFactHeight(tbFactHeight.Text));
        }

        private void tbFactHeight_Enter(object sender, System.EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private string CheckFactHeight(string number)
        {
            int level;
            if (number.Trim().Length != 4 || !int.TryParse(number, out level) || level <= 0)
                return "Ошибка при вводе значения фактической высоты";
            var typeData = (TypeData)cbNtype.SelectedItem;
            if (typeData != null)
            {
                var val = typeData.Diameter + typeData.Throat;
                var minval = Convert.ToInt32(Math.Round(val * 0.9));
                var maxval = Convert.ToInt32(Math.Round(val * 1.1));
                if (level >= minval && level <= maxval)
                    return string.Empty;
                return "Высота вагона отличается на 10% от заявленной в типе вагона";
            }
            return string.Empty;
        }

        private void tbFactHeight_Validated(object sender, System.EventArgs e)
        {
            errorProvider1.SetError(tbFactHeight, CheckFactHeight(tbFactHeight.Text));
        }
    }
}
