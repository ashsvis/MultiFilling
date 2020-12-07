namespace MultiFilling.SystemStatus
{
    partial class UcOneControllerStatus
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatusName = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblControllerName = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblChannelName = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblControlerId = new System.Windows.Forms.Label();
            this.lblChannelDesc = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTotalRequests = new System.Windows.Forms.Label();
            this.lblTotalErrors = new System.Windows.Forms.Label();
            this.lblErrorPercent = new System.Windows.Forms.Label();
            this.lblBarometerValue = new System.Windows.Forms.Label();
            this.lblMarginalLimit = new System.Windows.Forms.Label();
            this.lblFailLimit = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRiser = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblOverpass = new System.Windows.Forms.Label();
            this.lblWay = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label17 = new System.Windows.Forms.Label();
            this.cbNodeType = new System.Windows.Forms.ComboBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.ucTreeNavigator1 = new MultiFilling.SystemStatus.UcTreeNavigator();
            this.riserOneStateControl1 = new MultiFilling.SystemStatus.UcRiserOneState();
            this.lvLogView = new MultiFilling.ListViewEx();
            this.cbSnapTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chParameter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chOld = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNew = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbDescriptor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 254F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblStatusName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ucTreeNavigator1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.vScrollBar1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lvLogView, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 610F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1162, 707);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblStatusName
            // 
            this.lblStatusName.AutoSize = true;
            this.lblStatusName.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblStatusName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatusName.ForeColor = System.Drawing.Color.White;
            this.lblStatusName.Location = new System.Drawing.Point(0, 0);
            this.lblStatusName.Margin = new System.Windows.Forms.Padding(0);
            this.lblStatusName.Name = "lblStatusName";
            this.lblStatusName.Size = new System.Drawing.Size(254, 50);
            this.lblStatusName.TabIndex = 1;
            this.lblStatusName.Text = "Статус системы";
            this.lblStatusName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.lblControllerName);
            this.flowLayoutPanel1.Controls.Add(this.label11);
            this.flowLayoutPanel1.Controls.Add(this.lblChannelName);
            this.flowLayoutPanel1.Controls.Add(this.label10);
            this.flowLayoutPanel1.Controls.Add(this.lblControlerId);
            this.flowLayoutPanel1.Controls.Add(this.lblChannelDesc);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(254, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(908, 50);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Контроллер:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControllerName
            // 
            this.lblControllerName.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblControllerName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblControllerName.ForeColor = System.Drawing.Color.White;
            this.lblControllerName.Location = new System.Drawing.Point(131, 6);
            this.lblControllerName.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblControllerName.Name = "lblControllerName";
            this.lblControllerName.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblControllerName.Size = new System.Drawing.Size(147, 38);
            this.lblControllerName.TabIndex = 1;
            this.lblControllerName.Text = "------";
            this.lblControllerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(278, 6);
            this.label11.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 38);
            this.label11.TabIndex = 1;
            this.label11.Text = "Канал связи:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChannelName
            // 
            this.lblChannelName.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblChannelName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblChannelName.ForeColor = System.Drawing.Color.White;
            this.lblChannelName.Location = new System.Drawing.Point(409, 6);
            this.lblChannelName.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblChannelName.Name = "lblChannelName";
            this.lblChannelName.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblChannelName.Size = new System.Drawing.Size(174, 38);
            this.lblChannelName.TabIndex = 1;
            this.lblChannelName.Text = "------";
            this.lblChannelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(583, 6);
            this.label10.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 38);
            this.label10.TabIndex = 1;
            this.label10.Text = "Modbus ID:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControlerId
            // 
            this.lblControlerId.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblControlerId.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblControlerId.ForeColor = System.Drawing.Color.White;
            this.lblControlerId.Location = new System.Drawing.Point(704, 6);
            this.lblControlerId.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblControlerId.Name = "lblControlerId";
            this.lblControlerId.Size = new System.Drawing.Size(48, 38);
            this.lblControlerId.TabIndex = 1;
            this.lblControlerId.Text = "---";
            this.lblControlerId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChannelDesc
            // 
            this.lblChannelDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChannelDesc.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblChannelDesc.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblChannelDesc.ForeColor = System.Drawing.Color.White;
            this.lblChannelDesc.Location = new System.Drawing.Point(0, 50);
            this.lblChannelDesc.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblChannelDesc.Name = "lblChannelDesc";
            this.lblChannelDesc.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblChannelDesc.Size = new System.Drawing.Size(641, 38);
            this.lblChannelDesc.TabIndex = 1;
            this.lblChannelDesc.Text = "------";
            this.lblChannelDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControl1, 2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(257, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(902, 604);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(894, 570);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Статус опроса";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 221F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 705F));
            this.tableLayoutPanel2.Controls.Add(this.checkBoxActive, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label13, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label14, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.label3, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.label4, 1, 11);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 12);
            this.tableLayoutPanel2.Controls.Add(this.label6, 1, 14);
            this.tableLayoutPanel2.Controls.Add(this.label7, 1, 15);
            this.tableLayoutPanel2.Controls.Add(this.label8, 1, 16);
            this.tableLayoutPanel2.Controls.Add(this.label9, 1, 17);
            this.tableLayoutPanel2.Controls.Add(this.riserOneStateControl1, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 13);
            this.tableLayoutPanel2.Controls.Add(this.panel4, 1, 18);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalRequests, 2, 10);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalErrors, 2, 11);
            this.tableLayoutPanel2.Controls.Add(this.lblErrorPercent, 2, 12);
            this.tableLayoutPanel2.Controls.Add(this.lblBarometerValue, 2, 15);
            this.tableLayoutPanel2.Controls.Add(this.lblMarginalLimit, 2, 16);
            this.tableLayoutPanel2.Controls.Add(this.lblFailLimit, 2, 17);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblRiser, 2, 7);
            this.tableLayoutPanel2.Controls.Add(this.label12, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label15, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label16, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.lblOverpass, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblWay, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblProduct, 2, 6);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 3, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 20;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(888, 564);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Enabled = false;
            this.checkBoxActive.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxActive.Location = new System.Drawing.Point(23, 76);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(107, 25);
            this.checkBoxActive.TabIndex = 1;
            this.checkBoxActive.Text = "Разрешить";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            this.checkBoxActive.CheckedChanged += new System.EventHandler(this.checkBoxActive_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(23, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 21);
            this.label13.TabIndex = 0;
            this.label13.Text = "Статус";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(23, 232);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(163, 21);
            this.label14.TabIndex = 0;
            this.label14.Text = "Статистика ошибок";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(23, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "Всего запросов:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(23, 307);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "Всего ошибок:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(23, 337);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 30);
            this.label5.TabIndex = 0;
            this.label5.Text = "% Ошибок:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(23, 377);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Барометр";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(23, 422);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "Текущее значение:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(23, 452);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 30);
            this.label8.TabIndex = 0;
            this.label8.Text = "Предел сбоев:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(23, 482);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(150, 30);
            this.label9.TabIndex = 0;
            this.label9.Text = "Предел отказов:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel2.SetColumnSpan(this.panel1, 3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(25, 45);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 2);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel2.SetColumnSpan(this.panel2, 3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(25, 229);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1034, 1);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel2.SetColumnSpan(this.panel3, 3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(25, 372);
            this.panel3.Margin = new System.Windows.Forms.Padding(5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1034, 1);
            this.panel3.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel2.SetColumnSpan(this.panel4, 3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(25, 517);
            this.panel4.Margin = new System.Windows.Forms.Padding(5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1034, 1);
            this.panel4.TabIndex = 3;
            // 
            // lblTotalRequests
            // 
            this.lblTotalRequests.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotalRequests.Location = new System.Drawing.Point(244, 277);
            this.lblTotalRequests.Name = "lblTotalRequests";
            this.lblTotalRequests.Size = new System.Drawing.Size(112, 30);
            this.lblTotalRequests.TabIndex = 0;
            this.lblTotalRequests.Text = "------";
            this.lblTotalRequests.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalErrors
            // 
            this.lblTotalErrors.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotalErrors.Location = new System.Drawing.Point(244, 307);
            this.lblTotalErrors.Name = "lblTotalErrors";
            this.lblTotalErrors.Size = new System.Drawing.Size(112, 30);
            this.lblTotalErrors.TabIndex = 0;
            this.lblTotalErrors.Text = "------";
            this.lblTotalErrors.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblErrorPercent
            // 
            this.lblErrorPercent.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblErrorPercent.Location = new System.Drawing.Point(244, 337);
            this.lblErrorPercent.Name = "lblErrorPercent";
            this.lblErrorPercent.Size = new System.Drawing.Size(112, 30);
            this.lblErrorPercent.TabIndex = 0;
            this.lblErrorPercent.Text = "------";
            this.lblErrorPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBarometerValue
            // 
            this.lblBarometerValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBarometerValue.Location = new System.Drawing.Point(244, 422);
            this.lblBarometerValue.Name = "lblBarometerValue";
            this.lblBarometerValue.Size = new System.Drawing.Size(112, 30);
            this.lblBarometerValue.TabIndex = 0;
            this.lblBarometerValue.Text = "------";
            this.lblBarometerValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMarginalLimit
            // 
            this.lblMarginalLimit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMarginalLimit.Location = new System.Drawing.Point(244, 452);
            this.lblMarginalLimit.Name = "lblMarginalLimit";
            this.lblMarginalLimit.Size = new System.Drawing.Size(112, 30);
            this.lblMarginalLimit.TabIndex = 0;
            this.lblMarginalLimit.Text = "------";
            this.lblMarginalLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFailLimit
            // 
            this.lblFailLimit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFailLimit.Location = new System.Drawing.Point(244, 482);
            this.lblFailLimit.Name = "lblFailLimit";
            this.lblFailLimit.Size = new System.Drawing.Size(112, 30);
            this.lblFailLimit.TabIndex = 0;
            this.lblFailLimit.Text = "------";
            this.lblFailLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(23, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "№ стояка:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRiser
            // 
            this.lblRiser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRiser.Location = new System.Drawing.Point(244, 194);
            this.lblRiser.Name = "lblRiser";
            this.lblRiser.Size = new System.Drawing.Size(112, 30);
            this.lblRiser.TabIndex = 0;
            this.lblRiser.Text = "------";
            this.lblRiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(23, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 30);
            this.label12.TabIndex = 0;
            this.label12.Text = "Эстакада:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(23, 134);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(150, 30);
            this.label15.TabIndex = 0;
            this.label15.Text = "Путь:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(23, 164);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(150, 30);
            this.label16.TabIndex = 0;
            this.label16.Text = "Продукт:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOverpass
            // 
            this.lblOverpass.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblOverpass.Location = new System.Drawing.Point(244, 104);
            this.lblOverpass.Name = "lblOverpass";
            this.lblOverpass.Size = new System.Drawing.Size(112, 30);
            this.lblOverpass.TabIndex = 0;
            this.lblOverpass.Text = "------";
            this.lblOverpass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWay
            // 
            this.lblWay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblWay.Location = new System.Drawing.Point(244, 134);
            this.lblWay.Name = "lblWay";
            this.lblWay.Size = new System.Drawing.Size(112, 30);
            this.lblWay.TabIndex = 0;
            this.lblWay.Text = "------";
            this.lblWay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProduct.Location = new System.Drawing.Point(244, 164);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(112, 30);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "------";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.label17);
            this.flowLayoutPanel2.Controls.Add(this.cbNodeType);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(359, 73);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(301, 29);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 29);
            this.label17.TabIndex = 0;
            this.label17.Text = "Тип:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbNodeType
            // 
            this.cbNodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNodeType.Enabled = false;
            this.cbNodeType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbNodeType.FormattingEnabled = true;
            this.cbNodeType.Items.AddRange(new object[] {
            "Камышинский контроллер",
            "CPM 712 (Fastwel - IO)"});
            this.cbNodeType.Location = new System.Drawing.Point(45, 0);
            this.cbNodeType.Margin = new System.Windows.Forms.Padding(0);
            this.cbNodeType.Name = "cbNodeType";
            this.cbNodeType.Size = new System.Drawing.Size(256, 29);
            this.cbNodeType.TabIndex = 1;
            this.cbNodeType.SelectionChangeCommitted += new System.EventHandler(this.cbNodeType_SelectionChangeCommitted);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar1.LargeChange = 1;
            this.vScrollBar1.Location = new System.Drawing.Point(1142, 660);
            this.vScrollBar1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.vScrollBar1.Maximum = 500;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 47);
            this.vScrollBar1.TabIndex = 6;
            this.vScrollBar1.Value = 500;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 500;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // ucTreeNavigator1
            // 
            this.ucTreeNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTreeNavigator1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ucTreeNavigator1.Location = new System.Drawing.Point(0, 50);
            this.ucTreeNavigator1.Margin = new System.Windows.Forms.Padding(0);
            this.ucTreeNavigator1.Name = "ucTreeNavigator1";
            this.tableLayoutPanel1.SetRowSpan(this.ucTreeNavigator1, 2);
            this.ucTreeNavigator1.Size = new System.Drawing.Size(254, 657);
            this.ucTreeNavigator1.TabIndex = 4;
            // 
            // riserOneStateControl1
            // 
            this.riserOneStateControl1.Caption = "------";
            this.riserOneStateControl1.CaptionAtRight = false;
            this.riserOneStateControl1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.riserOneStateControl1.LampColorNone = System.Drawing.Color.Gray;
            this.riserOneStateControl1.LampColorOff = System.Drawing.Color.Black;
            this.riserOneStateControl1.LampColorOn = System.Drawing.Color.Lime;
            this.riserOneStateControl1.Location = new System.Drawing.Point(241, 76);
            this.riserOneStateControl1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.riserOneStateControl1.Name = "riserOneStateControl1";
            this.riserOneStateControl1.Size = new System.Drawing.Size(118, 23);
            this.riserOneStateControl1.State = null;
            this.riserOneStateControl1.TabIndex = 2;
            // 
            // lvLogView
            // 
            this.lvLogView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cbSnapTime,
            this.chParameter,
            this.chOld,
            this.chNew,
            this.cbDescriptor});
            this.lvLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLogView.FullRowSelect = true;
            this.lvLogView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvLogView.HideSelection = false;
            this.lvLogView.Location = new System.Drawing.Point(257, 663);
            this.lvLogView.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lvLogView.Name = "lvLogView";
            this.lvLogView.ShowItemToolTips = true;
            this.lvLogView.Size = new System.Drawing.Size(885, 41);
            this.lvLogView.TabIndex = 7;
            this.lvLogView.UseCompatibleStateImageBehavior = false;
            this.lvLogView.View = System.Windows.Forms.View.Details;
            // 
            // cbSnapTime
            // 
            this.cbSnapTime.Text = "Дата и время";
            this.cbSnapTime.Width = 170;
            // 
            // chParameter
            // 
            this.chParameter.Text = "Параметр";
            this.chParameter.Width = 100;
            // 
            // chOld
            // 
            this.chOld.Text = "Было";
            this.chOld.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chOld.Width = 80;
            // 
            // chNew
            // 
            this.chNew.Text = "Стало";
            this.chNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chNew.Width = 80;
            // 
            // cbDescriptor
            // 
            this.cbDescriptor.Text = "Дескриптор";
            // 
            // UcOneControllerStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UcOneControllerStatus";
            this.Size = new System.Drawing.Size(1162, 707);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblStatusName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private UcRiserOneState riserOneStateControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTotalRequests;
        private System.Windows.Forms.Label lblTotalErrors;
        private System.Windows.Forms.Label lblErrorPercent;
        private System.Windows.Forms.Label lblBarometerValue;
        private System.Windows.Forms.Label lblMarginalLimit;
        private System.Windows.Forms.Label lblFailLimit;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblControllerName;
        private UcTreeNavigator ucTreeNavigator1;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Label lblChannelDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRiser;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblControlerId;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblChannelName;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private ListViewEx lvLogView;
        private System.Windows.Forms.ColumnHeader cbSnapTime;
        private System.Windows.Forms.ColumnHeader chParameter;
        private System.Windows.Forms.ColumnHeader chOld;
        private System.Windows.Forms.ColumnHeader chNew;
        private System.Windows.Forms.ColumnHeader cbDescriptor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblOverpass;
        private System.Windows.Forms.Label lblWay;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cbNodeType;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label17;
    }
}
