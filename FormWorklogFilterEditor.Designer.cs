namespace MultiFilling
{
    partial class FormWorklogFilterEditor
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Путь 2");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Путь 4");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Путь 3,5");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Путь 12");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Путь 13");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Бензин");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("ДТ");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("ТС");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Мазут, газойль");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Эстакада 1");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Эстакада 2");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("Эстакада 4");
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpAddrAndParam = new System.Windows.Forms.TabPage();
            this.btnResetRisers = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbEndRisers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbStartRisers = new System.Windows.Forms.ComboBox();
            this.lvWay = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvProduct = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvOverpass = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpDateTimeRanges = new System.Windows.Forms.TabPage();
            this.btnNextDay = new System.Windows.Forms.Button();
            this.btnPrevDay = new System.Windows.Forms.Button();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cbEndedTime = new System.Windows.Forms.CheckBox();
            this.cbStartedTime = new System.Windows.Forms.CheckBox();
            this.tbEvents = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelectAllEvents = new System.Windows.Forms.Button();
            this.btnClearAllEvents = new System.Windows.Forms.Button();
            this.lvEvents = new System.Windows.Forms.ListView();
            this.chEventNames = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpAddrAndParam.SuspendLayout();
            this.tpDateTimeRanges.SuspendLayout();
            this.tbEvents.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(596, 406);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnApply);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(413, 371);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(180, 32);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(3, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(93, 26);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(102, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpAddrAndParam);
            this.tabControl1.Controls.Add(this.tpDateTimeRanges);
            this.tabControl1.Controls.Add(this.tbEvents);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(590, 362);
            this.tabControl1.TabIndex = 1;
            // 
            // tpAddrAndParam
            // 
            this.tpAddrAndParam.BackColor = System.Drawing.SystemColors.Window;
            this.tpAddrAndParam.Controls.Add(this.btnResetRisers);
            this.tpAddrAndParam.Controls.Add(this.label5);
            this.tpAddrAndParam.Controls.Add(this.label6);
            this.tpAddrAndParam.Controls.Add(this.label3);
            this.tpAddrAndParam.Controls.Add(this.label2);
            this.tpAddrAndParam.Controls.Add(this.label4);
            this.tpAddrAndParam.Controls.Add(this.cbEndRisers);
            this.tpAddrAndParam.Controls.Add(this.label1);
            this.tpAddrAndParam.Controls.Add(this.cbStartRisers);
            this.tpAddrAndParam.Controls.Add(this.lvWay);
            this.tpAddrAndParam.Controls.Add(this.lvProduct);
            this.tpAddrAndParam.Controls.Add(this.lvOverpass);
            this.tpAddrAndParam.Location = new System.Drawing.Point(4, 26);
            this.tpAddrAndParam.Name = "tpAddrAndParam";
            this.tpAddrAndParam.Padding = new System.Windows.Forms.Padding(3);
            this.tpAddrAndParam.Size = new System.Drawing.Size(582, 332);
            this.tpAddrAndParam.TabIndex = 1;
            this.tpAddrAndParam.Text = "Устройства";
            // 
            // btnResetRisers
            // 
            this.btnResetRisers.Enabled = false;
            this.btnResetRisers.Location = new System.Drawing.Point(472, 117);
            this.btnResetRisers.Name = "btnResetRisers";
            this.btnResetRisers.Size = new System.Drawing.Size(62, 26);
            this.btnResetRisers.TabIndex = 3;
            this.btnResetRisers.Text = "Сброс";
            this.btnResetRisers.UseVisualStyleBackColor = true;
            this.btnResetRisers.Click += new System.EventHandler(this.btnResetRisers_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(345, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Начальный номер:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(345, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Стояки:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(215, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Продукты:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(123, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Пути:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Конечный номер:";
            // 
            // cbEndRisers
            // 
            this.cbEndRisers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEndRisers.FormattingEnabled = true;
            this.cbEndRisers.Location = new System.Drawing.Point(472, 77);
            this.cbEndRisers.Name = "cbEndRisers";
            this.cbEndRisers.Size = new System.Drawing.Size(62, 25);
            this.cbEndRisers.TabIndex = 1;
            this.cbEndRisers.SelectionChangeCommitted += new System.EventHandler(this.cbEndRisers_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Эстакады:";
            // 
            // cbStartRisers
            // 
            this.cbStartRisers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStartRisers.FormattingEnabled = true;
            this.cbStartRisers.Location = new System.Drawing.Point(472, 46);
            this.cbStartRisers.Name = "cbStartRisers";
            this.cbStartRisers.Size = new System.Drawing.Size(62, 25);
            this.cbStartRisers.TabIndex = 1;
            this.cbStartRisers.SelectionChangeCommitted += new System.EventHandler(this.cbStartRisers_SelectionChangeCommitted);
            // 
            // lvWay
            // 
            this.lvWay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvWay.CheckBoxes = true;
            this.lvWay.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvWay.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.StateImageIndex = 0;
            listViewItem1.Tag = "2";
            listViewItem2.StateImageIndex = 0;
            listViewItem2.Tag = "4";
            listViewItem3.StateImageIndex = 0;
            listViewItem3.Tag = "35";
            listViewItem4.StateImageIndex = 0;
            listViewItem4.Tag = "12";
            listViewItem5.StateImageIndex = 0;
            listViewItem5.Tag = "13";
            this.lvWay.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.lvWay.Location = new System.Drawing.Point(126, 32);
            this.lvWay.MultiSelect = false;
            this.lvWay.Name = "lvWay";
            this.lvWay.Scrollable = false;
            this.lvWay.ShowGroups = false;
            this.lvWay.Size = new System.Drawing.Size(86, 111);
            this.lvWay.TabIndex = 0;
            this.lvWay.UseCompatibleStateImageBehavior = false;
            this.lvWay.View = System.Windows.Forms.View.Details;
            this.lvWay.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvEvents_ItemChecked);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 100;
            // 
            // lvProduct
            // 
            this.lvProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvProduct.CheckBoxes = true;
            this.lvProduct.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lvProduct.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem6.StateImageIndex = 0;
            listViewItem6.Tag = "B";
            listViewItem7.StateImageIndex = 0;
            listViewItem7.Tag = "D";
            listViewItem8.StateImageIndex = 0;
            listViewItem8.Tag = "T";
            listViewItem9.StateImageIndex = 0;
            listViewItem9.Tag = "M";
            this.lvProduct.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.lvProduct.Location = new System.Drawing.Point(218, 32);
            this.lvProduct.Name = "lvProduct";
            this.lvProduct.Scrollable = false;
            this.lvProduct.ShowGroups = false;
            this.lvProduct.Size = new System.Drawing.Size(121, 111);
            this.lvProduct.TabIndex = 0;
            this.lvProduct.UseCompatibleStateImageBehavior = false;
            this.lvProduct.View = System.Windows.Forms.View.Details;
            this.lvProduct.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvEvents_ItemChecked);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 120;
            // 
            // lvOverpass
            // 
            this.lvOverpass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvOverpass.CheckBoxes = true;
            this.lvOverpass.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvOverpass.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem10.StateImageIndex = 0;
            listViewItem10.Tag = "1";
            listViewItem11.StateImageIndex = 0;
            listViewItem11.Tag = "2";
            listViewItem12.StateImageIndex = 0;
            listViewItem12.Tag = "4";
            this.lvOverpass.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.lvOverpass.Location = new System.Drawing.Point(19, 32);
            this.lvOverpass.MultiSelect = false;
            this.lvOverpass.Name = "lvOverpass";
            this.lvOverpass.Scrollable = false;
            this.lvOverpass.ShowGroups = false;
            this.lvOverpass.Size = new System.Drawing.Size(101, 111);
            this.lvOverpass.TabIndex = 0;
            this.lvOverpass.UseCompatibleStateImageBehavior = false;
            this.lvOverpass.View = System.Windows.Forms.View.Details;
            this.lvOverpass.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvEvents_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 100;
            // 
            // tpDateTimeRanges
            // 
            this.tpDateTimeRanges.BackColor = System.Drawing.SystemColors.Window;
            this.tpDateTimeRanges.Controls.Add(this.btnNextDay);
            this.tpDateTimeRanges.Controls.Add(this.btnPrevDay);
            this.tpDateTimeRanges.Controls.Add(this.btnYesterday);
            this.tpDateTimeRanges.Controls.Add(this.button1);
            this.tpDateTimeRanges.Controls.Add(this.btnToday);
            this.tpDateTimeRanges.Controls.Add(this.dtpEndTime);
            this.tpDateTimeRanges.Controls.Add(this.dtpStartTime);
            this.tpDateTimeRanges.Controls.Add(this.dtpEndDate);
            this.tpDateTimeRanges.Controls.Add(this.dtpStartDate);
            this.tpDateTimeRanges.Controls.Add(this.cbEndedTime);
            this.tpDateTimeRanges.Controls.Add(this.cbStartedTime);
            this.tpDateTimeRanges.Location = new System.Drawing.Point(4, 26);
            this.tpDateTimeRanges.Name = "tpDateTimeRanges";
            this.tpDateTimeRanges.Padding = new System.Windows.Forms.Padding(3);
            this.tpDateTimeRanges.Size = new System.Drawing.Size(582, 332);
            this.tpDateTimeRanges.TabIndex = 2;
            this.tpDateTimeRanges.Text = "Дата и время";
            // 
            // btnNextDay
            // 
            this.btnNextDay.Location = new System.Drawing.Point(447, 121);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(90, 29);
            this.btnNextDay.TabIndex = 8;
            this.btnNextDay.Text = "+ сутки";
            this.btnNextDay.UseVisualStyleBackColor = true;
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.Location = new System.Drawing.Point(351, 121);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(90, 29);
            this.btnPrevDay.TabIndex = 8;
            this.btnPrevDay.Text = "- сутки";
            this.btnPrevDay.UseVisualStyleBackColor = true;
            this.btnPrevDay.Click += new System.EventHandler(this.btnPrevDay_Click);
            // 
            // btnYesterday
            // 
            this.btnYesterday.Location = new System.Drawing.Point(240, 121);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(90, 29);
            this.btnYesterday.TabIndex = 8;
            this.btnYesterday.Text = "Вчера";
            this.btnYesterday.UseVisualStyleBackColor = true;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "Сброс";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(144, 121);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(90, 29);
            this.btnToday.TabIndex = 7;
            this.btnToday.Text = "Сегодня";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndTime.Location = new System.Drawing.Point(441, 62);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(76, 25);
            this.dtpEndTime.TabIndex = 5;
            this.dtpEndTime.Value = new System.DateTime(2017, 7, 11, 23, 59, 0, 0);
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.DataChanged);
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(441, 31);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(76, 25);
            this.dtpStartTime.TabIndex = 2;
            this.dtpStartTime.Value = new System.DateTime(2017, 7, 11, 0, 0, 0, 0);
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.DataChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(233, 62);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 25);
            this.dtpEndDate.TabIndex = 4;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.DataChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(233, 31);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 25);
            this.dtpStartDate.TabIndex = 1;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.DataChanged);
            // 
            // cbEndedTime
            // 
            this.cbEndedTime.AutoSize = true;
            this.cbEndedTime.Location = new System.Drawing.Point(61, 66);
            this.cbEndedTime.Name = "cbEndedTime";
            this.cbEndedTime.Size = new System.Drawing.Size(159, 21);
            this.cbEndedTime.TabIndex = 3;
            this.cbEndedTime.Text = "Конечное время лога";
            this.cbEndedTime.UseVisualStyleBackColor = true;
            this.cbEndedTime.Click += new System.EventHandler(this.DataChanged);
            // 
            // cbStartedTime
            // 
            this.cbStartedTime.AutoSize = true;
            this.cbStartedTime.Location = new System.Drawing.Point(61, 35);
            this.cbStartedTime.Name = "cbStartedTime";
            this.cbStartedTime.Size = new System.Drawing.Size(166, 21);
            this.cbStartedTime.TabIndex = 0;
            this.cbStartedTime.Text = "Начальное время лога";
            this.cbStartedTime.UseVisualStyleBackColor = true;
            this.cbStartedTime.Click += new System.EventHandler(this.DataChanged);
            // 
            // tbEvents
            // 
            this.tbEvents.BackColor = System.Drawing.SystemColors.Window;
            this.tbEvents.Controls.Add(this.tableLayoutPanel2);
            this.tbEvents.Location = new System.Drawing.Point(4, 26);
            this.tbEvents.Name = "tbEvents";
            this.tbEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tbEvents.Size = new System.Drawing.Size(582, 332);
            this.tbEvents.TabIndex = 0;
            this.tbEvents.Text = "События";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lvEvents, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(576, 326);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.btnSelectAllEvents);
            this.flowLayoutPanel2.Controls.Add(this.btnClearAllEvents);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 291);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(237, 32);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // btnSelectAllEvents
            // 
            this.btnSelectAllEvents.Location = new System.Drawing.Point(3, 3);
            this.btnSelectAllEvents.Name = "btnSelectAllEvents";
            this.btnSelectAllEvents.Size = new System.Drawing.Size(117, 26);
            this.btnSelectAllEvents.TabIndex = 0;
            this.btnSelectAllEvents.Text = "Установить все";
            this.btnSelectAllEvents.UseVisualStyleBackColor = true;
            this.btnSelectAllEvents.Click += new System.EventHandler(this.btnSelectAllEvents_Click);
            // 
            // btnClearAllEvents
            // 
            this.btnClearAllEvents.Location = new System.Drawing.Point(126, 3);
            this.btnClearAllEvents.Name = "btnClearAllEvents";
            this.btnClearAllEvents.Size = new System.Drawing.Size(108, 26);
            this.btnClearAllEvents.TabIndex = 0;
            this.btnClearAllEvents.Text = "Очистить все";
            this.btnClearAllEvents.UseVisualStyleBackColor = true;
            this.btnClearAllEvents.Click += new System.EventHandler(this.btnClearAllEvents_Click);
            // 
            // lvEvents
            // 
            this.lvEvents.CheckBoxes = true;
            this.lvEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEventNames});
            this.lvEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEvents.FullRowSelect = true;
            this.lvEvents.Location = new System.Drawing.Point(3, 3);
            this.lvEvents.MultiSelect = false;
            this.lvEvents.Name = "lvEvents";
            this.lvEvents.ShowGroups = false;
            this.lvEvents.Size = new System.Drawing.Size(570, 282);
            this.lvEvents.TabIndex = 1;
            this.lvEvents.UseCompatibleStateImageBehavior = false;
            this.lvEvents.View = System.Windows.Forms.View.Details;
            this.lvEvents.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvEvents_ItemChecked);
            // 
            // chEventNames
            // 
            this.chEventNames.Text = "Список событий";
            this.chEventNames.Width = 544;
            // 
            // FormWorklogFilterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 406);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWorklogFilterEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Фильтр журнала";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWorklogFilterEditor_FormClosing);
            this.Load += new System.EventHandler(this.FormWorklogFilterEditor_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpAddrAndParam.ResumeLayout(false);
            this.tpAddrAndParam.PerformLayout();
            this.tpDateTimeRanges.ResumeLayout(false);
            this.tpDateTimeRanges.PerformLayout();
            this.tbEvents.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbEvents;
        private System.Windows.Forms.TabPage tpAddrAndParam;
        private System.Windows.Forms.TabPage tpDateTimeRanges;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnSelectAllEvents;
        private System.Windows.Forms.Button btnClearAllEvents;
        private System.Windows.Forms.ListView lvEvents;
        private System.Windows.Forms.ColumnHeader chEventNames;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.CheckBox cbEndedTime;
        private System.Windows.Forms.CheckBox cbStartedTime;
        private System.Windows.Forms.ListView lvOverpass;
        private System.Windows.Forms.ListView lvProduct;
        private System.Windows.Forms.ListView lvWay;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbEndRisers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbStartRisers;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.Button btnResetRisers;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnNextDay;
        private System.Windows.Forms.Button btnPrevDay;
    }
}