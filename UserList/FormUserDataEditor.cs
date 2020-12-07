using System;
using System.Windows.Forms;

namespace MultiFilling.UserList
{
    internal partial class FrmUserDataEditor : Form
    {
        bool _female;
        bool _passmodifyed;
        string _passhash = String.Empty;
        internal FrmUserDataEditor(int category)
        {
            InitializeComponent();
            cbCategory.Items.Clear();
            foreach (var cat in UserListKeeper.Category) cbCategory.Items.Add(cat);
            cbCategory.SelectedIndex = category - 1;
        }

        internal UserData UserData
        {
            get
            {
                var user = new UserData(tbLastName.Text, tbFirstName.Text,
                    tbMiddleName.Text, cbCategory.SelectedIndex) {Female = _female, PasswordHash = _passhash};
                return user;
            }
            set
            {
                tbLastName.Text = value.LastName;
                tbFirstName.Text = value.FirstName;
                tbMiddleName.Text = value.MiddleName;
                cbCategory.SelectedIndex = value.Category;
                _female = value.Female;
                _passhash = value.PasswordHash;
                pbMale.Visible = !_female;
                pbFemale.Visible = _female;
                _passmodifyed = false;
            }
        }

        private void TbLastNameTextChanged(object sender, EventArgs e)
        {
            CheckValid();
        }

        private void CheckValid()
        {
            // проверка валидности значения полей без смены пароля или со сменой пароля
            btnOk.Enabled = !String.IsNullOrWhiteSpace(tbLastName.Text) &&
                !String.IsNullOrWhiteSpace(tbFirstName.Text) &&
                !String.IsNullOrWhiteSpace(tbMiddleName.Text) &&
                (!_passmodifyed && !String.IsNullOrWhiteSpace(_passhash) || _passmodifyed &&
                !String.IsNullOrWhiteSpace(tbPassword.Text) &&
                tbPassword.Text.Equals(tbRepeatPassword.Text));
        }

        private void PbMaleClick(object sender, EventArgs e)
        {
            _female = true;
            pbFemale.Visible = true;
            pbMale.Visible = false;
            CheckValid();
        }

        private void PbFemaleClick(object sender, EventArgs e)
        {
            _female = false;
            pbMale.Visible = true;
            pbFemale.Visible = false;
            CheckValid();
        }

        private void TbPasswordTextChanged(object sender, EventArgs e)
        {
            _passhash = UserListKeeper.GetMd5Hash(tbPassword.Text);
            // проверка валидности значения полей при смене пароля
            btnOk.Enabled = !String.IsNullOrWhiteSpace(tbLastName.Text) &&
                !String.IsNullOrWhiteSpace(tbFirstName.Text) &&
                !String.IsNullOrWhiteSpace(tbMiddleName.Text) &&
                !String.IsNullOrWhiteSpace(tbPassword.Text) &&
                tbPassword.Text.Equals(tbRepeatPassword.Text);
        }

        private void CbCategorySelectedIndexChanged(object sender, EventArgs e)
        {
            CheckValid();
        }
    }
}
