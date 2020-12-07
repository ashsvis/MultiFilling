namespace MultiFilling.TypesList
{
    partial class FormWaggonTypeDataEditor
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
            this.tbNtype = new System.Windows.Forms.TextBox();
            this.lbNtype = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDiameter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbThroat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDefLevel = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNtype
            // 
            this.tbNtype.Location = new System.Drawing.Point(213, 13);
            this.tbNtype.Margin = new System.Windows.Forms.Padding(4);
            this.tbNtype.MaxLength = 3;
            this.tbNtype.Name = "tbNtype";
            this.tbNtype.Size = new System.Drawing.Size(102, 29);
            this.tbNtype.TabIndex = 0;
            this.tbNtype.TextChanged += new System.EventHandler(this.tbNtype_TextChanged);
            this.tbNtype.Validated += new System.EventHandler(this.tbNtype_Validated);
            // 
            // lbNtype
            // 
            this.lbNtype.AutoSize = true;
            this.lbNtype.Location = new System.Drawing.Point(22, 16);
            this.lbNtype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNtype.Name = "lbNtype";
            this.lbNtype.Size = new System.Drawing.Size(113, 21);
            this.lbNtype.TabIndex = 2;
            this.lbNtype.Text = "Тип цистерны:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Диаметр цистерны:";
            // 
            // tbDiameter
            // 
            this.tbDiameter.Location = new System.Drawing.Point(213, 50);
            this.tbDiameter.Margin = new System.Windows.Forms.Padding(4);
            this.tbDiameter.MaxLength = 4;
            this.tbDiameter.Name = "tbDiameter";
            this.tbDiameter.Size = new System.Drawing.Size(102, 29);
            this.tbDiameter.TabIndex = 1;
            this.tbDiameter.TextChanged += new System.EventHandler(this.tbNtype_TextChanged);
            this.tbDiameter.Validated += new System.EventHandler(this.tbDiameter_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Высота горловины:";
            // 
            // tbThroat
            // 
            this.tbThroat.Location = new System.Drawing.Point(213, 87);
            this.tbThroat.Margin = new System.Windows.Forms.Padding(4);
            this.tbThroat.MaxLength = 3;
            this.tbThroat.Name = "tbThroat";
            this.tbThroat.Size = new System.Drawing.Size(102, 29);
            this.tbThroat.TabIndex = 2;
            this.tbThroat.TextChanged += new System.EventHandler(this.tbNtype_TextChanged);
            this.tbThroat.Validated += new System.EventHandler(this.tbThroat_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 127);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "Уровень по умолчанию:";
            // 
            // tbDefLevel
            // 
            this.tbDefLevel.Location = new System.Drawing.Point(213, 124);
            this.tbDefLevel.Margin = new System.Windows.Forms.Padding(4);
            this.tbDefLevel.MaxLength = 4;
            this.tbDefLevel.Name = "tbDefLevel";
            this.tbDefLevel.Size = new System.Drawing.Size(102, 29);
            this.tbDefLevel.TabIndex = 3;
            this.tbDefLevel.TextChanged += new System.EventHandler(this.tbDefLevel_TextChanged);
            this.tbDefLevel.Validated += new System.EventHandler(this.tbDefLevel_Validated);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(111, 170);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 31);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ввод";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(216, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormWaggonTypeDataEditor
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(342, 213);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbDefLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbThroat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDiameter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNtype);
            this.Controls.Add(this.lbNtype);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWaggonTypeDataEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор типа вагона-цистерны";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNtype;
        private System.Windows.Forms.Label lbNtype;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDiameter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbThroat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDefLevel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}