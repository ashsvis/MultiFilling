namespace MultiFilling.SystemStatus
{
    partial class UcOneChannelStatus
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.riserOneStateControl1 = new MultiFilling.SystemStatus.UcRiserOneState();
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
            this.label12 = new System.Windows.Forms.Label();
            this.lblMoxaIp = new System.Windows.Forms.Label();
            this.buttonFetch = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.lblDescriptor = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblFetchTime = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblSentTimeout = new System.Windows.Forms.Label();
            this.lblReceiveTimeout = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.nudChannelByIndex = new System.Windows.Forms.NumericUpDown();
            this.cbChannelByName = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ucTreeNavigator1 = new MultiFilling.SystemStatus.UcTreeNavigator();
            this.label28 = new System.Windows.Forms.Label();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.riserOneStateControl0 = new MultiFilling.SystemStatus.UcRiserOneState();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannelByIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 254F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblStatusName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ucTreeNavigator1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1189, 578);
            this.tableLayoutPanel1.TabIndex = 1;
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 221F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 576F));
            this.tableLayoutPanel2.Controls.Add(this.checkBoxActive, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label13, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 6);
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
            this.tableLayoutPanel2.Controls.Add(this.label12, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblMoxaIp, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.buttonFetch, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.label15, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblDescriptor, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label16, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblFetchTime, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.label18, 3, 5);
            this.tableLayoutPanel2.Controls.Add(this.label17, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblSentTimeout, 2, 6);
            this.tableLayoutPanel2.Controls.Add(this.lblReceiveTimeout, 2, 7);
            this.tableLayoutPanel2.Controls.Add(this.label20, 3, 6);
            this.tableLayoutPanel2.Controls.Add(this.label21, 3, 7);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(254, 50);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 20;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(935, 528);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Enabled = false;
            this.checkBoxActive.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxActive.Location = new System.Drawing.Point(23, 74);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(107, 25);
            this.checkBoxActive.TabIndex = 1;
            this.checkBoxActive.Text = "Разрешить";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(23, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 21);
            this.label13.TabIndex = 0;
            this.label13.Text = "Статус";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(23, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "Время на запрос:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // riserOneStateControl1
            // 
            this.riserOneStateControl1.Caption = "------";
            this.riserOneStateControl1.CaptionAtRight = false;
            this.riserOneStateControl1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.riserOneStateControl1.LampColorNone = System.Drawing.Color.Gray;
            this.riserOneStateControl1.LampColorOff = System.Drawing.Color.Black;
            this.riserOneStateControl1.LampColorOn = System.Drawing.Color.Lime;
            this.riserOneStateControl1.Location = new System.Drawing.Point(241, 74);
            this.riserOneStateControl1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.riserOneStateControl1.Name = "riserOneStateControl1";
            this.riserOneStateControl1.Size = new System.Drawing.Size(118, 23);
            this.riserOneStateControl1.State = null;
            this.riserOneStateControl1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel2.SetColumnSpan(this.panel1, 3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(25, 45);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(905, 1);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel2.SetColumnSpan(this.panel2, 3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(25, 227);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(905, 1);
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
            this.panel3.Size = new System.Drawing.Size(905, 1);
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
            this.panel4.Size = new System.Drawing.Size(905, 1);
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
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(23, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(215, 30);
            this.label12.TabIndex = 0;
            this.label12.Text = "IP-адрес канала связи:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMoxaIp
            // 
            this.lblMoxaIp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMoxaIp.Location = new System.Drawing.Point(244, 102);
            this.lblMoxaIp.Name = "lblMoxaIp";
            this.lblMoxaIp.Size = new System.Drawing.Size(112, 30);
            this.lblMoxaIp.TabIndex = 0;
            this.lblMoxaIp.Text = "------";
            this.lblMoxaIp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonFetch
            // 
            this.buttonFetch.Enabled = false;
            this.buttonFetch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFetch.Location = new System.Drawing.Point(389, 71);
            this.buttonFetch.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.buttonFetch.Name = "buttonFetch";
            this.buttonFetch.Size = new System.Drawing.Size(151, 29);
            this.buttonFetch.TabIndex = 4;
            this.buttonFetch.Text = "Опросить сейчас";
            this.buttonFetch.UseVisualStyleBackColor = true;
            this.buttonFetch.Visible = false;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(23, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(215, 40);
            this.label15.TabIndex = 0;
            this.label15.Text = "Назначение сегмента связи:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescriptor
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.lblDescriptor, 2);
            this.lblDescriptor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDescriptor.Location = new System.Drawing.Point(244, 0);
            this.lblDescriptor.Name = "lblDescriptor";
            this.lblDescriptor.Size = new System.Drawing.Size(648, 40);
            this.lblDescriptor.TabIndex = 0;
            this.lblDescriptor.Text = "-------";
            this.lblDescriptor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(23, 132);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(215, 30);
            this.label16.TabIndex = 0;
            this.label16.Text = "Скорость опроса канала:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFetchTime
            // 
            this.lblFetchTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFetchTime.Location = new System.Drawing.Point(244, 132);
            this.lblFetchTime.Name = "lblFetchTime";
            this.lblFetchTime.Size = new System.Drawing.Size(112, 30);
            this.lblFetchTime.TabIndex = 0;
            this.lblFetchTime.Text = "------";
            this.lblFetchTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.Location = new System.Drawing.Point(362, 132);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 30);
            this.label18.TabIndex = 0;
            this.label18.Text = "сек";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(23, 192);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(150, 30);
            this.label17.TabIndex = 0;
            this.label17.Text = "Время на ответ:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSentTimeout
            // 
            this.lblSentTimeout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSentTimeout.Location = new System.Drawing.Point(244, 162);
            this.lblSentTimeout.Name = "lblSentTimeout";
            this.lblSentTimeout.Size = new System.Drawing.Size(112, 30);
            this.lblSentTimeout.TabIndex = 0;
            this.lblSentTimeout.Text = "------";
            this.lblSentTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReceiveTimeout
            // 
            this.lblReceiveTimeout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblReceiveTimeout.Location = new System.Drawing.Point(244, 192);
            this.lblReceiveTimeout.Name = "lblReceiveTimeout";
            this.lblReceiveTimeout.Size = new System.Drawing.Size(112, 30);
            this.lblReceiveTimeout.TabIndex = 0;
            this.lblReceiveTimeout.Text = "------";
            this.lblReceiveTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(362, 162);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(44, 30);
            this.label20.TabIndex = 0;
            this.label20.Text = "сек";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(362, 192);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(44, 30);
            this.label21.TabIndex = 0;
            this.label21.Text = "сек";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.nudChannelByIndex);
            this.flowLayoutPanel1.Controls.Add(this.cbChannelByName);
            this.flowLayoutPanel1.Controls.Add(this.label11);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(254, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(935, 50);
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
            this.label1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(133, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Канал";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudChannelByIndex
            // 
            this.nudChannelByIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudChannelByIndex.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.nudChannelByIndex.Location = new System.Drawing.Point(136, 9);
            this.nudChannelByIndex.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.nudChannelByIndex.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.nudChannelByIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudChannelByIndex.Name = "nudChannelByIndex";
            this.nudChannelByIndex.Size = new System.Drawing.Size(65, 32);
            this.nudChannelByIndex.TabIndex = 2;
            this.nudChannelByIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudChannelByIndex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbChannelByName
            // 
            this.cbChannelByName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannelByName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbChannelByName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.cbChannelByName.FormattingEnabled = true;
            this.cbChannelByName.Location = new System.Drawing.Point(207, 9);
            this.cbChannelByName.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.cbChannelByName.Name = "cbChannelByName";
            this.cbChannelByName.Size = new System.Drawing.Size(422, 33);
            this.cbChannelByName.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(632, 6);
            this.label11.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label11.Size = new System.Drawing.Size(235, 38);
            this.label11.TabIndex = 1;
            this.label11.Text = "Тип:   Modbus RTU";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucTreeNavigator1
            // 
            this.ucTreeNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTreeNavigator1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ucTreeNavigator1.Location = new System.Drawing.Point(0, 50);
            this.ucTreeNavigator1.Margin = new System.Windows.Forms.Padding(0);
            this.ucTreeNavigator1.Name = "ucTreeNavigator1";
            this.ucTreeNavigator1.Size = new System.Drawing.Size(254, 528);
            this.ucTreeNavigator1.TabIndex = 4;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label28.Location = new System.Drawing.Point(680, 30);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(115, 20);
            this.label28.TabIndex = 0;
            this.label28.Text = "IP-адрес";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 500;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // riserOneStateControl0
            // 
            this.riserOneStateControl0.Caption = "Не активен";
            this.riserOneStateControl0.CaptionAtRight = false;
            this.riserOneStateControl0.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.riserOneStateControl0.LampColorNone = System.Drawing.Color.Gray;
            this.riserOneStateControl0.LampColorOff = System.Drawing.Color.Black;
            this.riserOneStateControl0.LampColorOn = System.Drawing.Color.Lime;
            this.riserOneStateControl0.Location = new System.Drawing.Point(547, 68);
            this.riserOneStateControl0.Margin = new System.Windows.Forms.Padding(20, 8, 0, 0);
            this.riserOneStateControl0.Name = "riserOneStateControl0";
            this.riserOneStateControl0.Size = new System.Drawing.Size(109, 26);
            this.riserOneStateControl0.State = null;
            this.riserOneStateControl0.TabIndex = 3;
            // 
            // UcOneChannelStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UcOneChannelStatus";
            this.Size = new System.Drawing.Size(1189, 578);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudChannelByIndex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblStatusName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private UcRiserOneState riserOneStateControl0;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.NumericUpDown nudChannelByIndex;
        private System.Windows.Forms.ComboBox cbChannelByName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Timer timerUpdate;
        private UcTreeNavigator ucTreeNavigator1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblMoxaIp;
        private System.Windows.Forms.Button buttonFetch;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblDescriptor;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblFetchTime;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblSentTimeout;
        private System.Windows.Forms.Label lblReceiveTimeout;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
    }
}
