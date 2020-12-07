namespace MultiFilling.UserList
{
    partial class FrmUserList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Операторы");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Прибористы");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Инженеры-технологи");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Инженеры АСУ ТП");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Администраторы");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserList));
            this.tsToolButtons = new System.Windows.Forms.ToolStrip();
            this.btnAddUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnChangeUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteUser = new System.Windows.Forms.ToolStripButton();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnCancelRegistry = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.tvUsers = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsToolButtons.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsToolButtons
            // 
            this.tsToolButtons.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsToolButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddUser,
            this.toolStripSeparator1,
            this.btnChangeUser,
            this.toolStripSeparator2,
            this.btnDeleteUser});
            this.tsToolButtons.Location = new System.Drawing.Point(0, 0);
            this.tsToolButtons.Name = "tsToolButtons";
            this.tsToolButtons.Size = new System.Drawing.Size(374, 27);
            this.tsToolButtons.TabIndex = 0;
            this.tsToolButtons.Text = "toolStrip1";
            // 
            // btnAddUser
            // 
            this.btnAddUser.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAddUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(80, 24);
            this.btnAddUser.Text = "Добавить";
            this.btnAddUser.ToolTipText = "Добавить нового пользователя";
            this.btnAddUser.Click += new System.EventHandler(this.BtnAddUserClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnChangeUser
            // 
            this.btnChangeUser.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnChangeUser.Enabled = false;
            this.btnChangeUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChangeUser.Name = "btnChangeUser";
            this.btnChangeUser.Size = new System.Drawing.Size(82, 24);
            this.btnChangeUser.Text = "Изменить";
            this.btnChangeUser.ToolTipText = "Изменить данные пользователя";
            this.btnChangeUser.Click += new System.EventHandler(this.BtnChangeUserClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDeleteUser.Enabled = false;
            this.btnDeleteUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(69, 24);
            this.btnDeleteUser.Text = "Удалить";
            this.btnDeleteUser.ToolTipText = "Удалить данные пользователя";
            this.btnDeleteUser.Click += new System.EventHandler(this.BtnDeleteUserClick);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancelRegistry);
            this.panelBottom.Controls.Add(this.buttonClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelBottom.Location = new System.Drawing.Point(0, 384);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(374, 52);
            this.panelBottom.TabIndex = 1;
            // 
            // btnCancelRegistry
            // 
            this.btnCancelRegistry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelRegistry.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancelRegistry.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnCancelRegistry.Enabled = false;
            this.btnCancelRegistry.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancelRegistry.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancelRegistry.Location = new System.Drawing.Point(14, 8);
            this.btnCancelRegistry.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancelRegistry.Name = "btnCancelRegistry";
            this.btnCancelRegistry.Size = new System.Drawing.Size(202, 33);
            this.btnCancelRegistry.TabIndex = 2;
            this.btnCancelRegistry.Text = "Отмена регистрации";
            this.btnCancelRegistry.UseVisualStyleBackColor = false;
            this.btnCancelRegistry.Visible = false;
            this.btnCancelRegistry.Click += new System.EventHandler(this.BtnCancelRegistryClick);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonClose.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClose.Location = new System.Drawing.Point(265, 8);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(98, 33);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // tvUsers
            // 
            this.tvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUsers.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tvUsers.FullRowSelect = true;
            this.tvUsers.HideSelection = false;
            this.tvUsers.ImageIndex = 0;
            this.tvUsers.ImageList = this.imageList1;
            this.tvUsers.Location = new System.Drawing.Point(0, 27);
            this.tvUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvUsers.Name = "tvUsers";
            treeNode1.Name = "Узел1";
            treeNode1.Tag = "1";
            treeNode1.Text = "Операторы";
            treeNode2.Name = "Узел2";
            treeNode2.Tag = "2";
            treeNode2.Text = "Прибористы";
            treeNode3.Name = "Узел3";
            treeNode3.Tag = "3";
            treeNode3.Text = "Инженеры-технологи";
            treeNode4.Name = "Узел4";
            treeNode4.Tag = "4";
            treeNode4.Text = "Инженеры АСУ ТП";
            treeNode5.Name = "Узел5";
            treeNode5.Tag = "5";
            treeNode5.Text = "Администраторы";
            this.tvUsers.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.tvUsers.SelectedImageIndex = 0;
            this.tvUsers.Size = new System.Drawing.Size(374, 357);
            this.tvUsers.TabIndex = 0;
            this.tvUsers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvUsersAfterSelect);
            this.tvUsers.DoubleClick += new System.EventHandler(this.TvUsersDoubleClick);
            this.tvUsers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TvUsersKeyDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "edit_group_7135.png");
            this.imageList1.Images.SetKeyName(1, "user_male_3028.png");
            this.imageList1.Images.SetKeyName(2, "user_female_4866.png");
            // 
            // FrmUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(374, 436);
            this.Controls.Add(this.tvUsers);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.tsToolButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUserList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Список пользователей";
            this.Load += new System.EventHandler(this.FrmUserListLoad);
            this.tsToolButtons.ResumeLayout(false);
            this.tsToolButtons.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsToolButtons;
        private System.Windows.Forms.ToolStripButton btnAddUser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnChangeUser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDeleteUser;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TreeView tvUsers;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnCancelRegistry;
    }
}