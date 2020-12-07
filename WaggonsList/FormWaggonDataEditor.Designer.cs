namespace MultiFilling.WaggonsList
{
    partial class FormWaggonDataEditor
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbFactHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.lbNumber = new System.Windows.Forms.Label();
            this.cbNtype = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(217, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 31);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(112, 131);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 31);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ввод";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // tbFactHeight
            // 
            this.tbFactHeight.Location = new System.Drawing.Point(204, 86);
            this.tbFactHeight.Margin = new System.Windows.Forms.Padding(4);
            this.tbFactHeight.MaxLength = 4;
            this.tbFactHeight.Name = "tbFactHeight";
            this.tbFactHeight.Size = new System.Drawing.Size(112, 29);
            this.tbFactHeight.TabIndex = 2;
            this.tbFactHeight.TextChanged += new System.EventHandler(this.tbFactHeight_TextChanged);
            this.tbFactHeight.Enter += new System.EventHandler(this.tbFactHeight_Enter);
            this.tbFactHeight.Validated += new System.EventHandler(this.tbFactHeight_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "Фактическая высота:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "Тип цистерны:";
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(204, 11);
            this.tbNumber.Margin = new System.Windows.Forms.Padding(4);
            this.tbNumber.MaxLength = 8;
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(112, 29);
            this.tbNumber.TabIndex = 0;
            this.tbNumber.TextChanged += new System.EventHandler(this.tbNumber_TextChanged);
            this.tbNumber.Enter += new System.EventHandler(this.tbFactHeight_Enter);
            this.tbNumber.Validated += new System.EventHandler(this.tbNumber_Validated);
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Location = new System.Drawing.Point(13, 14);
            this.lbNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(135, 21);
            this.lbNumber.TabIndex = 11;
            this.lbNumber.Text = "Номер цистерны:";
            // 
            // cbNtype
            // 
            this.cbNtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNtype.FormattingEnabled = true;
            this.cbNtype.Location = new System.Drawing.Point(204, 49);
            this.cbNtype.Name = "cbNtype";
            this.cbNtype.Size = new System.Drawing.Size(112, 29);
            this.cbNtype.TabIndex = 1;
            this.cbNtype.SelectionChangeCommitted += new System.EventHandler(this.cbNtype_SelectionChangeCommitted);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormWaggonDataEditor
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(337, 178);
            this.Controls.Add(this.cbNtype);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbFactHeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.lbNumber);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWaggonDataEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор вагона-цистерны";
            this.Load += new System.EventHandler(this.FormWaggonDataEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbFactHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.ComboBox cbNtype;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}