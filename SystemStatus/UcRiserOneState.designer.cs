namespace MultiFilling.SystemStatus
{
    partial class UcRiserOneState
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
            this.tlpLeftToRight = new System.Windows.Forms.TableLayoutPanel();
            this.lbText = new System.Windows.Forms.Label();
            this.pbLamp = new System.Windows.Forms.PictureBox();
            this.tlpRightToLeft = new System.Windows.Forms.TableLayoutPanel();
            this.lbTextRight = new System.Windows.Forms.Label();
            this.pbLampRight = new System.Windows.Forms.PictureBox();
            this.tlpLeftToRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLamp)).BeginInit();
            this.tlpRightToLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLampRight)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpLeftToRight
            // 
            this.tlpLeftToRight.ColumnCount = 2;
            this.tlpLeftToRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpLeftToRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeftToRight.Controls.Add(this.lbText, 1, 0);
            this.tlpLeftToRight.Controls.Add(this.pbLamp, 0, 0);
            this.tlpLeftToRight.Location = new System.Drawing.Point(0, 0);
            this.tlpLeftToRight.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLeftToRight.Name = "tlpLeftToRight";
            this.tlpLeftToRight.RowCount = 1;
            this.tlpLeftToRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLeftToRight.Size = new System.Drawing.Size(517, 23);
            this.tlpLeftToRight.TabIndex = 2;
            // 
            // lbText
            // 
            this.lbText.AutoEllipsis = true;
            this.lbText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbText.Location = new System.Drawing.Point(24, 0);
            this.lbText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(489, 23);
            this.lbText.TabIndex = 1;
            this.lbText.Text = "Некоторый текст";
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbLamp
            // 
            this.pbLamp.Enabled = false;
            this.pbLamp.Location = new System.Drawing.Point(0, 2);
            this.pbLamp.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.pbLamp.Name = "pbLamp";
            this.pbLamp.Size = new System.Drawing.Size(20, 20);
            this.pbLamp.TabIndex = 0;
            this.pbLamp.TabStop = false;
            this.pbLamp.Paint += new System.Windows.Forms.PaintEventHandler(this.pbLamp_Paint);
            // 
            // tlpRightToLeft
            // 
            this.tlpRightToLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRightToLeft.ColumnCount = 2;
            this.tlpRightToLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRightToLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRightToLeft.Controls.Add(this.lbTextRight, 0, 0);
            this.tlpRightToLeft.Controls.Add(this.pbLampRight, 1, 0);
            this.tlpRightToLeft.Location = new System.Drawing.Point(240, 0);
            this.tlpRightToLeft.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRightToLeft.Name = "tlpRightToLeft";
            this.tlpRightToLeft.RowCount = 1;
            this.tlpRightToLeft.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRightToLeft.Size = new System.Drawing.Size(292, 23);
            this.tlpRightToLeft.TabIndex = 3;
            this.tlpRightToLeft.Visible = false;
            // 
            // lbTextRight
            // 
            this.lbTextRight.AutoEllipsis = true;
            this.lbTextRight.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbTextRight.Location = new System.Drawing.Point(4, 0);
            this.lbTextRight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTextRight.Name = "lbTextRight";
            this.lbTextRight.Size = new System.Drawing.Size(264, 23);
            this.lbTextRight.TabIndex = 1;
            this.lbTextRight.Text = "Некоторый текст";
            this.lbTextRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbLampRight
            // 
            this.pbLampRight.Enabled = false;
            this.pbLampRight.Location = new System.Drawing.Point(272, 2);
            this.pbLampRight.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.pbLampRight.Name = "pbLampRight";
            this.pbLampRight.Size = new System.Drawing.Size(20, 20);
            this.pbLampRight.TabIndex = 0;
            this.pbLampRight.TabStop = false;
            this.pbLampRight.Paint += new System.Windows.Forms.PaintEventHandler(this.pbLamp_Paint);
            // 
            // UcRiserOneState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpLeftToRight);
            this.Controls.Add(this.tlpRightToLeft);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UcRiserOneState";
            this.Size = new System.Drawing.Size(538, 23);
            this.tlpLeftToRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLamp)).EndInit();
            this.tlpRightToLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLampRight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpLeftToRight;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.PictureBox pbLamp;
        private System.Windows.Forms.TableLayoutPanel tlpRightToLeft;
        private System.Windows.Forms.Label lbTextRight;
        private System.Windows.Forms.PictureBox pbLampRight;

    }
}
