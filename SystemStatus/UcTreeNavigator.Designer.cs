namespace MultiFilling.SystemStatus
{
    partial class UcTreeNavigator
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Каналы");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Контроллеры");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Интерфейсы контроллеров", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Станции");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Принтеры");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Системное оборудование", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            this.tvNavigator = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvNavigator
            // 
            this.tvNavigator.BackColor = System.Drawing.Color.Gainsboro;
            this.tvNavigator.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvNavigator.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tvNavigator.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tvNavigator.HideSelection = false;
            this.tvNavigator.Location = new System.Drawing.Point(0, 0);
            this.tvNavigator.Margin = new System.Windows.Forms.Padding(0);
            this.tvNavigator.Name = "tvNavigator";
            treeNode1.Name = "ndChannels";
            treeNode1.Text = "Каналы";
            treeNode2.Name = "ndControllers";
            treeNode2.Text = "Контроллеры";
            treeNode3.Name = "ndControllerInterfaces";
            treeNode3.Text = "Интерфейсы контроллеров";
            treeNode4.Name = "ndStations";
            treeNode4.Text = "Станции";
            treeNode5.Name = "ndPrinters";
            treeNode5.Text = "Принтеры";
            treeNode6.Name = "ndSystemHardware";
            treeNode6.Text = "Системное оборудование";
            this.tvNavigator.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6});
            this.tvNavigator.Size = new System.Drawing.Size(254, 604);
            this.tvNavigator.TabIndex = 1;
            this.tvNavigator.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvNavigator_MouseUp);
            // 
            // UcTreeNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvNavigator);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UcTreeNavigator";
            this.Size = new System.Drawing.Size(254, 604);
            this.Load += new System.EventHandler(this.TreeNavigatorUc_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvNavigator;
    }
}
