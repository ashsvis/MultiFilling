namespace MultiFilling
{
    partial class UcFillingPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcFillingPage));
            this.tlpMainGrid = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFillinCaption = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cbRisers = new System.Windows.Forms.ToolStripComboBox();
            this.btnTask = new System.Windows.Forms.ToolStripButton();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnClearAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAllTasks = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStartAllSelected = new System.Windows.Forms.ToolStripButton();
            this.btnStopAllRuns = new System.Windows.Forms.ToolStripButton();
            this.splitTopBottom = new System.Windows.Forms.SplitContainer();
            this.splitLeftRight = new System.Windows.Forms.SplitContainer();
            this.tvNavigator = new System.Windows.Forms.TreeView();
            this.pictBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lvLogView = new MultiFilling.ListViewEx();
            this.chDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMeggage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.tlpMainGrid.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTopBottom)).BeginInit();
            this.splitTopBottom.Panel1.SuspendLayout();
            this.splitTopBottom.Panel2.SuspendLayout();
            this.splitTopBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitLeftRight)).BeginInit();
            this.splitLeftRight.Panel1.SuspendLayout();
            this.splitLeftRight.Panel2.SuspendLayout();
            this.splitLeftRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainGrid
            // 
            this.tlpMainGrid.BackColor = System.Drawing.Color.Silver;
            this.tlpMainGrid.ColumnCount = 1;
            this.tlpMainGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1156F));
            this.tlpMainGrid.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpMainGrid.Controls.Add(this.splitTopBottom, 0, 1);
            this.tlpMainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainGrid.Location = new System.Drawing.Point(0, 0);
            this.tlpMainGrid.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMainGrid.Name = "tlpMainGrid";
            this.tlpMainGrid.RowCount = 2;
            this.tlpMainGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMainGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMainGrid.Size = new System.Drawing.Size(1156, 679);
            this.tlpMainGrid.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblFillinCaption, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1156, 30);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // lblFillinCaption
            // 
            this.lblFillinCaption.AutoSize = true;
            this.lblFillinCaption.BackColor = System.Drawing.SystemColors.Control;
            this.lblFillinCaption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFillinCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFillinCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFillinCaption.ForeColor = System.Drawing.Color.Red;
            this.lblFillinCaption.Location = new System.Drawing.Point(696, 0);
            this.lblFillinCaption.Margin = new System.Windows.Forms.Padding(0);
            this.lblFillinCaption.Name = "lblFillinCaption";
            this.lblFillinCaption.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblFillinCaption.Size = new System.Drawing.Size(460, 30);
            this.lblFillinCaption.TabIndex = 1;
            this.lblFillinCaption.Text = "Эстакада налива продукта";
            this.lblFillinCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbRisers,
            this.btnTask,
            this.btnClear,
            this.btnClearAll,
            this.toolStripSeparator1,
            this.btnAllTasks,
            this.toolStripSeparator3,
            this.btnStartAllSelected,
            this.btnStopAllRuns});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(696, 30);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cbRisers
            // 
            this.cbRisers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRisers.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbRisers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbRisers.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.cbRisers.Name = "cbRisers";
            this.cbRisers.Size = new System.Drawing.Size(100, 30);
            this.cbRisers.ToolTipText = "Список стояков группы";
            this.cbRisers.SelectedIndexChanged += new System.EventHandler(this.cbRisers_SelectedIndexChanged);
            // 
            // btnTask
            // 
            this.btnTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnTask.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTask.Image = ((System.Drawing.Image)(resources.GetObject("btnTask.Image")));
            this.btnTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTask.Name = "btnTask";
            this.btnTask.Size = new System.Drawing.Size(64, 27);
            this.btnTask.Text = "Задание";
            this.btnTask.ToolTipText = "Установка задания выделенного стояка";
            this.btnTask.Click += new System.EventHandler(this.btnTask_Click);
            // 
            // btnClear
            // 
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(71, 27);
            this.btnClear.Text = "Очистить";
            this.btnClear.ToolTipText = "Очистка задания выделенного стояка";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnClearAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClearAll.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAll.Image")));
            this.btnClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(96, 27);
            this.btnClearAll.Text = "Очистить все";
            this.btnClearAll.ToolTipText = "Очистка задания всех стояков";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // btnAllTasks
            // 
            this.btnAllTasks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAllTasks.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAllTasks.Image = ((System.Drawing.Image)(resources.GetObject("btnAllTasks.Image")));
            this.btnAllTasks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAllTasks.Name = "btnAllTasks";
            this.btnAllTasks.Size = new System.Drawing.Size(88, 27);
            this.btnAllTasks.Text = "Все задания";
            this.btnAllTasks.ToolTipText = "Вызов таблицы заданий всех стояков";
            this.btnAllTasks.Click += new System.EventHandler(this.btnAllTasks_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 30);
            // 
            // btnStartAllSelected
            // 
            this.btnStartAllSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStartAllSelected.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStartAllSelected.Image = ((System.Drawing.Image)(resources.GetObject("btnStartAllSelected.Image")));
            this.btnStartAllSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartAllSelected.Name = "btnStartAllSelected";
            this.btnStartAllSelected.Size = new System.Drawing.Size(99, 27);
            this.btnStartAllSelected.Text = "Запустить все";
            this.btnStartAllSelected.ToolTipText = "Запустить налив всеми стояками группы";
            this.btnStartAllSelected.Click += new System.EventHandler(this.btnStartAllSelected_Click);
            // 
            // btnStopAllRuns
            // 
            this.btnStopAllRuns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStopAllRuns.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStopAllRuns.Image = ((System.Drawing.Image)(resources.GetObject("btnStopAllRuns.Image")));
            this.btnStopAllRuns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStopAllRuns.Name = "btnStopAllRuns";
            this.btnStopAllRuns.Size = new System.Drawing.Size(111, 27);
            this.btnStopAllRuns.Text = "Остановить все";
            this.btnStopAllRuns.ToolTipText = "Остановить налив всеми стояками группы";
            this.btnStopAllRuns.Click += new System.EventHandler(this.btnStopAllRuns_Click);
            // 
            // splitTopBottom
            // 
            this.splitTopBottom.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.splitTopBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTopBottom.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitTopBottom.Location = new System.Drawing.Point(0, 30);
            this.splitTopBottom.Margin = new System.Windows.Forms.Padding(0);
            this.splitTopBottom.Name = "splitTopBottom";
            this.splitTopBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitTopBottom.Panel1
            // 
            this.splitTopBottom.Panel1.Controls.Add(this.splitLeftRight);
            // 
            // splitTopBottom.Panel2
            // 
            this.splitTopBottom.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitTopBottom.Size = new System.Drawing.Size(1156, 649);
            this.splitTopBottom.SplitterDistance = 429;
            this.splitTopBottom.SplitterWidth = 3;
            this.splitTopBottom.TabIndex = 3;
            // 
            // splitLeftRight
            // 
            this.splitLeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLeftRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitLeftRight.Location = new System.Drawing.Point(0, 0);
            this.splitLeftRight.Margin = new System.Windows.Forms.Padding(0);
            this.splitLeftRight.Name = "splitLeftRight";
            // 
            // splitLeftRight.Panel1
            // 
            this.splitLeftRight.Panel1.Controls.Add(this.tvNavigator);
            // 
            // splitLeftRight.Panel2
            // 
            this.splitLeftRight.Panel2.Controls.Add(this.pictBox);
            this.splitLeftRight.Size = new System.Drawing.Size(1156, 429);
            this.splitLeftRight.SplitterDistance = 183;
            this.splitLeftRight.SplitterWidth = 3;
            this.splitLeftRight.TabIndex = 0;
            // 
            // tvNavigator
            // 
            this.tvNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvNavigator.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tvNavigator.FullRowSelect = true;
            this.tvNavigator.HideSelection = false;
            this.tvNavigator.Location = new System.Drawing.Point(0, 0);
            this.tvNavigator.Margin = new System.Windows.Forms.Padding(0);
            this.tvNavigator.Name = "tvNavigator";
            this.tvNavigator.Size = new System.Drawing.Size(183, 429);
            this.tvNavigator.TabIndex = 1;
            this.tvNavigator.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvNavigator_AfterSelect);
            // 
            // pictBox
            // 
            this.pictBox.BackColor = System.Drawing.Color.Silver;
            this.pictBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictBox.Location = new System.Drawing.Point(0, 0);
            this.pictBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictBox.Name = "pictBox";
            this.pictBox.Size = new System.Drawing.Size(970, 429);
            this.pictBox.TabIndex = 0;
            this.pictBox.TabStop = false;
            this.pictBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictBox_Paint);
            this.pictBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictBox_MouseDoubleClick);
            this.pictBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictBox_MouseDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lvLogView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.vScrollBar1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1156, 217);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lvLogView
            // 
            this.lvLogView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDateTime,
            this.chMeggage});
            this.lvLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLogView.FullRowSelect = true;
            this.lvLogView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvLogView.Location = new System.Drawing.Point(0, 0);
            this.lvLogView.Margin = new System.Windows.Forms.Padding(0);
            this.lvLogView.MultiSelect = false;
            this.lvLogView.Name = "lvLogView";
            this.lvLogView.Scrollable = false;
            this.lvLogView.ShowItemToolTips = true;
            this.lvLogView.Size = new System.Drawing.Size(1139, 217);
            this.lvLogView.TabIndex = 0;
            this.lvLogView.UseCompatibleStateImageBehavior = false;
            this.lvLogView.View = System.Windows.Forms.View.Details;
            // 
            // chDateTime
            // 
            this.chDateTime.Text = "Дата и время";
            this.chDateTime.Width = 170;
            // 
            // chMeggage
            // 
            this.chMeggage.Text = "Сообщение";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar1.LargeChange = 1;
            this.vScrollBar1.Location = new System.Drawing.Point(1139, 0);
            this.vScrollBar1.Maximum = 500;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 217);
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Value = 500;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Время";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Стояк";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Событие";
            this.columnHeader3.Width = 800;
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // UcFillingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMainGrid);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UcFillingPage";
            this.Size = new System.Drawing.Size(1156, 679);
            this.Load += new System.EventHandler(this.UcFillingPage_Load);
            this.tlpMainGrid.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitTopBottom.Panel1.ResumeLayout(false);
            this.splitTopBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTopBottom)).EndInit();
            this.splitTopBottom.ResumeLayout(false);
            this.splitLeftRight.Panel1.ResumeLayout(false);
            this.splitLeftRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitLeftRight)).EndInit();
            this.splitLeftRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainGrid;
        private System.Windows.Forms.TreeView tvNavigator;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox cbRisers;
        private System.Windows.Forms.ToolStripButton btnTask;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnClearAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAllTasks;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnStartAllSelected;
        private System.Windows.Forms.ToolStripButton btnStopAllRuns;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.PictureBox pictBox;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Label lblFillinCaption;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.SplitContainer splitTopBottom;
        private System.Windows.Forms.SplitContainer splitLeftRight;
        private ListViewEx lvLogView;
        private System.Windows.Forms.ColumnHeader chDateTime;
        private System.Windows.Forms.ColumnHeader chMeggage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
    }
}
