﻿namespace MultiFilling
{
    partial class UcFillinglogPage
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFilterEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBackward = new System.Windows.Forms.ToolStripButton();
            this.tsbForward = new System.Windows.Forms.ToolStripButton();
            this.tsbEnd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAnchor = new System.Windows.Forms.ToolStripButton();
            this.lvLogView = new MultiFilling.ListViewEx();
            this.chDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chWaggon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSetpoint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chComment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lvLogView, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1118, 612);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.label1.Size = new System.Drawing.Size(1112, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Протокол налива";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrint,
            this.tsbPreview,
            this.toolStripSeparator3,
            this.tsbFilterEdit,
            this.toolStripSeparator2,
            this.tsbBackward,
            this.tsbForward,
            this.tsbEnd,
            this.toolStripSeparator1,
            this.tsbAnchor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(466, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 4;
            // 
            // tsbPrint
            // 
            this.tsbPrint.Enabled = false;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(59, 22);
            this.tsbPrint.Text = "Печать...";
            // 
            // tsbPreview
            // 
            this.tsbPreview.Enabled = false;
            this.tsbPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPreview.Name = "tsbPreview";
            this.tsbPreview.Size = new System.Drawing.Size(68, 22);
            this.tsbPreview.Text = "Просмотр";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFilterEdit
            // 
            this.tsbFilterEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilterEdit.Name = "tsbFilterEdit";
            this.tsbFilterEdit.Size = new System.Drawing.Size(61, 22);
            this.tsbFilterEdit.Text = "Фильтр...";
            this.tsbFilterEdit.Click += new System.EventHandler(this.tsbFilterEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBackward
            // 
            this.tsbBackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBackward.Name = "tsbBackward";
            this.tsbBackward.Size = new System.Drawing.Size(43, 22);
            this.tsbBackward.Text = "Назад";
            this.tsbBackward.Click += new System.EventHandler(this.tsbBackward_Click);
            // 
            // tsbForward
            // 
            this.tsbForward.Enabled = false;
            this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbForward.Name = "tsbForward";
            this.tsbForward.Size = new System.Drawing.Size(44, 22);
            this.tsbForward.Text = "Далее";
            this.tsbForward.Click += new System.EventHandler(this.tsbForward_Click);
            // 
            // tsbEnd
            // 
            this.tsbEnd.Enabled = false;
            this.tsbEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEnd.Name = "tsbEnd";
            this.tsbEnd.Size = new System.Drawing.Size(96, 22);
            this.tsbEnd.Text = "Конец журнала";
            this.tsbEnd.Click += new System.EventHandler(this.tsbEnd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbAnchor
            // 
            this.tsbAnchor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAnchor.Name = "tsbAnchor";
            this.tsbAnchor.Size = new System.Drawing.Size(65, 22);
            this.tsbAnchor.Text = "Обновить";
            this.tsbAnchor.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lvLogView
            // 
            this.lvLogView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDate,
            this.chAddr,
            this.chWaggon,
            this.chType,
            this.chSetpoint,
            this.chStart,
            this.chEnd,
            this.chLevel,
            this.chUser,
            this.chComment});
            this.lvLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLogView.FullRowSelect = true;
            this.lvLogView.GridLines = true;
            this.lvLogView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvLogView.Location = new System.Drawing.Point(3, 52);
            this.lvLogView.MultiSelect = false;
            this.lvLogView.Name = "lvLogView";
            this.lvLogView.Scrollable = false;
            this.lvLogView.ShowGroups = false;
            this.lvLogView.ShowItemToolTips = true;
            this.lvLogView.Size = new System.Drawing.Size(1112, 557);
            this.lvLogView.TabIndex = 5;
            this.lvLogView.UseCompatibleStateImageBehavior = false;
            this.lvLogView.View = System.Windows.Forms.View.Details;
            // 
            // chDate
            // 
            this.chDate.Text = "Дата";
            this.chDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chDate.Width = 80;
            // 
            // chAddr
            // 
            this.chAddr.Text = "Адрес устройства";
            this.chAddr.Width = 260;
            // 
            // chWaggon
            // 
            this.chWaggon.Text = "Вагон";
            this.chWaggon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chWaggon.Width = 80;
            // 
            // chType
            // 
            this.chType.Text = "Тип";
            this.chType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chType.Width = 40;
            // 
            // chSetpoint
            // 
            this.chSetpoint.Text = "Задание";
            this.chSetpoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chSetpoint.Width = 70;
            // 
            // chStart
            // 
            this.chStart.Text = "Начало";
            this.chStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chStart.Width = 80;
            // 
            // chEnd
            // 
            this.chEnd.Text = "Окончание";
            this.chEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chEnd.Width = 80;
            // 
            // chLevel
            // 
            this.chLevel.Text = "Уровень";
            this.chLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chLevel.Width = 70;
            // 
            // chUser
            // 
            this.chUser.Text = "Оператор";
            this.chUser.Width = 120;
            // 
            // chComment
            // 
            this.chComment.Text = "Примечание";
            this.chComment.Width = 450;
            // 
            // UcFillinglogPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "UcFillinglogPage";
            this.Size = new System.Drawing.Size(1118, 612);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripButton tsbPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbFilterEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbBackward;
        private System.Windows.Forms.ToolStripButton tsbForward;
        private System.Windows.Forms.ToolStripButton tsbEnd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbAnchor;
        private ListViewEx lvLogView;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chAddr;
        private System.Windows.Forms.ColumnHeader chWaggon;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chSetpoint;
        private System.Windows.Forms.ColumnHeader chStart;
        private System.Windows.Forms.ColumnHeader chEnd;
        private System.Windows.Forms.ColumnHeader chLevel;
        private System.Windows.Forms.ColumnHeader chUser;
        private System.Windows.Forms.ColumnHeader chComment;
    }
}
