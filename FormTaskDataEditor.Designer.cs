namespace MultiFilling
{
    partial class FormTaskDataEditor
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
            this.cbNtype = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbFactHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.lbNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSetpoint = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbDiameter = new System.Windows.Forms.Label();
            this.lbThroat = new System.Windows.Forms.Label();
            this.lbMaximum = new System.Windows.Forms.Label();
            this.lbMinimum = new System.Windows.Forms.Label();
            this.lbInfo = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbNtype
            // 
            this.cbNtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNtype.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.cbNtype.FormattingEnabled = true;
            this.cbNtype.Location = new System.Drawing.Point(180, 51);
            this.cbNtype.Name = "cbNtype";
            this.cbNtype.Size = new System.Drawing.Size(88, 30);
            this.cbNtype.TabIndex = 2;
            this.cbNtype.SelectionChangeCommitted += new System.EventHandler(this.cbNtype_SelectionChangeCommitted);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(306, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 31);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(204, 170);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 31);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ввод";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // tbFactHeight
            // 
            this.tbFactHeight.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.tbFactHeight.Location = new System.Drawing.Point(180, 88);
            this.tbFactHeight.Margin = new System.Windows.Forms.Padding(4);
            this.tbFactHeight.MaxLength = 4;
            this.tbFactHeight.Name = "tbFactHeight";
            this.tbFactHeight.Size = new System.Drawing.Size(88, 30);
            this.tbFactHeight.TabIndex = 3;
            this.tbFactHeight.TextChanged += new System.EventHandler(this.tbFactHeight_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 21);
            this.label3.TabIndex = 17;
            this.label3.Text = "Фактическая высота:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 21);
            this.label2.TabIndex = 18;
            this.label2.Text = "Тип цистерны:";
            // 
            // tbNumber
            // 
            this.tbNumber.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbNumber.Location = new System.Drawing.Point(180, 13);
            this.tbNumber.Margin = new System.Windows.Forms.Padding(4);
            this.tbNumber.MaxLength = 8;
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(88, 30);
            this.tbNumber.TabIndex = 0;
            this.tbNumber.Enter += new System.EventHandler(this.tbNumber_Enter);
            this.tbNumber.Leave += new System.EventHandler(this.tbNumber_Leave);
            this.tbNumber.Validating += new System.ComponentModel.CancelEventHandler(this.tbNumber_Validating);
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Location = new System.Drawing.Point(13, 16);
            this.lbNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(135, 21);
            this.lbNumber.TabIndex = 19;
            this.lbNumber.Text = "Номер цистерны:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 128);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.label1.TabIndex = 17;
            this.label1.Text = "Задание:";
            // 
            // tbSetpoint
            // 
            this.tbSetpoint.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.tbSetpoint.Location = new System.Drawing.Point(180, 125);
            this.tbSetpoint.Margin = new System.Windows.Forms.Padding(4);
            this.tbSetpoint.MaxLength = 4;
            this.tbSetpoint.Name = "tbSetpoint";
            this.tbSetpoint.Size = new System.Drawing.Size(88, 30);
            this.tbSetpoint.TabIndex = 4;
            this.tbSetpoint.TextChanged += new System.EventHandler(this.tbSetpoint_TextChanged);
            this.tbSetpoint.Enter += new System.EventHandler(this.tbSetpoint_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(279, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "д. цистерны";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(279, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "выс. горлов.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(280, 121);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "максимум";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(280, 138);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "минимум";
            // 
            // lbDiameter
            // 
            this.lbDiameter.AutoSize = true;
            this.lbDiameter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbDiameter.Location = new System.Drawing.Point(365, 46);
            this.lbDiameter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDiameter.Name = "lbDiameter";
            this.lbDiameter.Size = new System.Drawing.Size(0, 17);
            this.lbDiameter.TabIndex = 18;
            // 
            // lbThroat
            // 
            this.lbThroat.AutoSize = true;
            this.lbThroat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbThroat.Location = new System.Drawing.Point(365, 63);
            this.lbThroat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbThroat.Name = "lbThroat";
            this.lbThroat.Size = new System.Drawing.Size(0, 17);
            this.lbThroat.TabIndex = 18;
            // 
            // lbMaximum
            // 
            this.lbMaximum.AutoSize = true;
            this.lbMaximum.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbMaximum.Location = new System.Drawing.Point(350, 120);
            this.lbMaximum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbMaximum.Name = "lbMaximum";
            this.lbMaximum.Size = new System.Drawing.Size(0, 17);
            this.lbMaximum.TabIndex = 18;
            // 
            // lbMinimum
            // 
            this.lbMinimum.AutoSize = true;
            this.lbMinimum.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbMinimum.Location = new System.Drawing.Point(350, 137);
            this.lbMinimum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbMinimum.Name = "lbMinimum";
            this.lbMinimum.Size = new System.Drawing.Size(0, 17);
            this.lbMinimum.TabIndex = 18;
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbInfo.Location = new System.Drawing.Point(7, 201);
            this.lbInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(38, 17);
            this.lbInfo.TabIndex = 18;
            this.lbInfo.Text = "------";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // FormTaskDataEditor
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(412, 220);
            this.Controls.Add(this.cbNtype);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbSetpoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFactHeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbMinimum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbMaximum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbThroat);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbDiameter);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.lbNumber);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTaskDataEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Задание налива";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTaskDataEditor_FormClosing);
            this.Load += new System.EventHandler(this.FormTaskDataEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbNtype;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbFactHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSetpoint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbDiameter;
        private System.Windows.Forms.Label lbThroat;
        private System.Windows.Forms.Label lbMaximum;
        private System.Windows.Forms.Label lbMinimum;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Timer timerUpdate;
    }
}