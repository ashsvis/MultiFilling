using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MultiFilling.UserList
{
    internal partial class FrmUserList : Form
    {
        readonly bool _editorMode;
        internal FrmUserList(bool editorMode = false)
        {
            InitializeComponent();
            _editorMode = editorMode;
            btnCancelRegistry.Visible = !editorMode;
            btnCancelRegistry.Enabled = UserListKeeper.CurrentUser != null;
            tsToolButtons.Visible = editorMode;
            FillTree();
        }

        private void FillTree(string fullname = "")
        {
            tvUsers.Nodes.Clear();
            for (var i = 0; i < UserListKeeper.Categories.Count; i++)
            {
                var catNode = new TreeNode(UserListKeeper.Categories[i])
                                  {
                                      Tag = i + 1,
                                      ImageIndex = 0,
                                      SelectedImageIndex = 0
                                  };
                tvUsers.Nodes.Add(catNode);
                var j = i;
                IEnumerable<UserData> query = from user in UserListKeeper.Users
                                            where user.Category == j
                                            orderby user.GetFullName()
                                            select user;
                foreach (var user in query)
                {
                    var userNode = new TreeNode(user.GetFullName()) {Tag = user, ImageIndex = user.Female ? 2 : 1};
                    userNode.SelectedImageIndex = userNode.ImageIndex;
                    catNode.Nodes.Add(userNode);
                    if ((!_editorMode || String.IsNullOrWhiteSpace(fullname) || !fullname.Equals(userNode.Text)) &&
                        (_editorMode || UserListKeeper.CurrentUser == null ||
                         !userNode.Text.Equals(UserListKeeper.CurrentUser.GetFullName()))) continue;
                    tvUsers.SelectedNode = userNode;
                    userNode.Parent.Expand();
                }
            }
            if (_editorMode || tvUsers.SelectedNode != null) return;
            foreach (var node in tvUsers.Nodes.Cast<TreeNode>().Where(node => node.Text.Equals("Операторы")))
            {
                tvUsers.SelectedNode = node;
                node.Expand();
                break;
            }
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAddUserClick(object sender, EventArgs e)
        {
            InsertUserData();
        }

        private void InsertUserData()
        {
            if (!_editorMode) return;
            var node = tvUsers.SelectedNode;
            // первоначально категория выбирается как выбрано в tvUsers
            // если не выбрано, то назначается 1 - оператор
            int category;
            if (node != null)
            {
                var user = node.Tag as UserData;
                category = user != null ? user.Category : int.Parse(node.Tag.ToString());
            }
            else
                category = 1;
            using (var f = new FrmUserDataEditor(category))
            {
                if (f.ShowDialog() != DialogResult.OK) return;
                UserListKeeper.Users.Add(f.UserData);
                FillTree(f.UserData.GetFullName());
            }
        }

        private void TvUsersAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                btnChangeUser.Enabled = btnDeleteUser.Enabled = (e.Node.Level == 1);
            }
        }

        private void BtnChangeUserClick(object sender, EventArgs e)
        {
            ChangeUserData();
        }

        private void ChangeUserData()
        {
            if (!_editorMode) return;
            var user = tvUsers.SelectedNode.Tag as UserData;
            if (user == null) return;
            using (var f = new FrmUserDataEditor(0))
            {
                f.UserData = user;
                if (f.ShowDialog() != DialogResult.OK) return;
                tvUsers.SelectedNode.Tag = f.UserData;
                UserListKeeper.Users.Remove(user);
                UserListKeeper.Users.Add(f.UserData);
                FillTree(f.UserData.GetFullName());
            }
        }

        private void TvUsersDoubleClick(object sender, EventArgs e)
        {
            if (_editorMode)
                ChangeUserData();
            else
                SelectUserData();
        }

        private void SelectUserData()
        {
            var user = tvUsers.SelectedNode.Tag as UserData;
            if (user == null) return;
            using (var f = new FrmPassword())
            {
                if (f.ShowDialog() != DialogResult.OK) return;
                if (UserListKeeper.VerifyMd5Hash(f.Password, user.PasswordHash))
                {
                    UserListKeeper.CurrentUser = user;
                    btnCancelRegistry.Enabled = true;
                    DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(this, @"Ошибка пароля",
                                    @"Регистрация пользователя",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TvUsersKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    if (_editorMode)
                        ChangeUserData();
                    else
                        SelectUserData();
                    break;
                case Keys.Insert:
                    InsertUserData();
                    break;
                case Keys.Delete:
                    DeleteUserData();
                    break;
            }
        }

        private void FrmUserListLoad(object sender, EventArgs e)
        {
            tvUsers.Focus();
        }

        private void BtnDeleteUserClick(object sender, EventArgs e)
        {
            DeleteUserData();
        }

        private void DeleteUserData()
        {
            if (!_editorMode) return;
            var user = tvUsers.SelectedNode.Tag as UserData;
            if (user == null) return;
            if (MessageBox.Show(this,
                                String.Format("Удалить пользователя \"{0}\"?", user.GetShortName()),
                                @"Редактор данных пользователя",
                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            UserListKeeper.Users.Remove(user);
            FillTree();
        }

        private void BtnCancelRegistryClick(object sender, EventArgs e)
        {
            btnCancelRegistry.Enabled = false;
            UserListKeeper.CurrentUser = null;
        }
    }
}
