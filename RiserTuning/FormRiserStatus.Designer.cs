namespace MultiFilling.RiserTuning
{
    partial class FormRiserStatus
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
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.riserStatusControl1 = new RiserStatusControl();
            this.SuspendLayout();
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 500;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // riserStatusControl1
            // 
            this.riserStatusControl1.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.riserStatusControl1.Location = new System.Drawing.Point(2, 1);
            this.riserStatusControl1.Margin = new System.Windows.Forms.Padding(0);
            this.riserStatusControl1.Name = "riserStatusControl1";
            this.riserStatusControl1.Size = new System.Drawing.Size(558, 620);
            this.riserStatusControl1.TabIndex = 0;
            // 
            // FormRiserStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 621);
            this.Controls.Add(this.riserStatusControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRiserStatus";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Состояние [ Стояк 247 ]";
            this.ResumeLayout(false);

        }

        #endregion

        private RiserStatusControl riserStatusControl1;
        private System.Windows.Forms.Timer timerUpdate;
    }
}