using System;
using System.Windows.Forms;

namespace MultiFilling.UserList
{
    internal partial class FrmPassword : Form
    {
        public string Password { get; private set; }
        internal FrmPassword()
        {
            InitializeComponent();
            Password = String.Empty;
        }

        private void TbPasswordTextChanged(object sender, EventArgs e)
        {
            Password = tbPassword.Text;
        }

    }
}
