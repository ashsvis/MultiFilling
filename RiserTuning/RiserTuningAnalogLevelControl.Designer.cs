/*
 * Created by SharpDevelop.
 * User: Experionadmin
 * Date: 04/04/2017
 * Time: 9:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace MultiFilling.RiserTuning
{
	partial class RiserTuningAnalogLevelControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lbHR30 = new System.Windows.Forms.Label();
            this.lbHR24 = new System.Windows.Forms.Label();
            this.lbHR31 = new System.Windows.Forms.Label();
            this.lbHR32 = new System.Windows.Forms.Label();
            this.lbHR33 = new System.Windows.Forms.Label();
            this.lbHR34 = new System.Windows.Forms.Label();
            this.lbHR35 = new System.Windows.Forms.Label();
            this.lbHR36 = new System.Windows.Forms.Label();
            this.lbHR14_11 = new System.Windows.Forms.Label();
            this.lbHR1F = new System.Windows.Forms.Label();
            this.lbHR1E = new System.Windows.Forms.Label();
            this.lbHR14_10 = new System.Windows.Forms.Label();
            this.edHR30 = new System.Windows.Forms.TextBox();
            this.edHR31 = new System.Windows.Forms.TextBox();
            this.edHR32 = new System.Windows.Forms.TextBox();
            this.edHR33 = new System.Windows.Forms.TextBox();
            this.edHR34 = new System.Windows.Forms.TextBox();
            this.edHR35 = new System.Windows.Forms.TextBox();
            this.edHR36 = new System.Windows.Forms.TextBox();
            this.cbHR14_11 = new System.Windows.Forms.ComboBox();
            this.edHR1F = new System.Windows.Forms.TextBox();
            this.edHR1E = new System.Windows.Forms.TextBox();
            this.cbHR14_10 = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnEEPROM = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCopyFromStorage = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(344, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "R";
            // 
            // btnCopy
            // 
            this.btnCopy.Enabled = false;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCopy.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCopy.Location = new System.Drawing.Point(372, 6);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(139, 26);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "копировать>>>";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.BtnCopyClick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(518, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "W";
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "Заводской номер:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.lbHR30, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbHR24, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbHR31, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbHR32, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbHR33, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbHR34, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbHR35, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lbHR36, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lbHR14_11, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.lbHR1F, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.lbHR1E, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.lbHR14_10, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.edHR30, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.edHR31, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.edHR32, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.edHR33, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.edHR34, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.edHR35, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.edHR36, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.cbHR14_11, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.edHR1F, 2, 10);
            this.tableLayoutPanel1.Controls.Add(this.edHR1E, 2, 11);
            this.tableLayoutPanel1.Controls.Add(this.cbHR14_10, 2, 12);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 14);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 36);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(616, 393);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(4, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(270, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ток на выходе сигнализатора (мА):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(4, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(270, 22);
            this.label5.TabIndex = 3;
            this.label5.Text = "Контрольная точка 1 (мм):";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(4, 76);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(270, 22);
            this.label6.TabIndex = 3;
            this.label6.Text = "(мкА):";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(4, 103);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(270, 22);
            this.label7.TabIndex = 3;
            this.label7.Text = "Контрольная точка 2 (мм):";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoEllipsis = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(4, 130);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(270, 22);
            this.label8.TabIndex = 3;
            this.label8.Text = "(мкА):";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoEllipsis = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(4, 157);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(270, 22);
            this.label9.TabIndex = 3;
            this.label9.Text = "Глубина установки (мм):";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoEllipsis = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(4, 184);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(270, 22);
            this.label10.TabIndex = 3;
            this.label10.Text = "Рабочая длина (мм):";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoEllipsis = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(4, 211);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(270, 22);
            this.label11.TabIndex = 3;
            this.label11.Text = "Источник тока:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoEllipsis = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(4, 238);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(270, 22);
            this.label12.TabIndex = 3;
            this.label12.Text = "контроль исправности";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoEllipsis = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(4, 260);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(270, 22);
            this.label13.TabIndex = 3;
            this.label13.Text = "Минимальный ток (мкА):";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoEllipsis = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(4, 287);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(270, 22);
            this.label14.TabIndex = 3;
            this.label14.Text = "Максимальный ток (мкА):";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoEllipsis = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(4, 314);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(270, 22);
            this.label15.TabIndex = 3;
            this.label15.Text = "Инверсия релейного сигнала:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR30
            // 
            this.lbHR30.AutoEllipsis = true;
            this.lbHR30.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR30.Location = new System.Drawing.Point(282, 0);
            this.lbHR30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR30.Name = "lbHR30";
            this.lbHR30.Size = new System.Drawing.Size(126, 22);
            this.lbHR30.TabIndex = 3;
            this.lbHR30.Text = "------";
            this.lbHR30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR24
            // 
            this.lbHR24.AutoEllipsis = true;
            this.lbHR24.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR24.Location = new System.Drawing.Point(282, 27);
            this.lbHR24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR24.Name = "lbHR24";
            this.lbHR24.Size = new System.Drawing.Size(126, 22);
            this.lbHR24.TabIndex = 3;
            this.lbHR24.Text = "------";
            this.lbHR24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR31
            // 
            this.lbHR31.AutoEllipsis = true;
            this.lbHR31.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR31.Location = new System.Drawing.Point(282, 49);
            this.lbHR31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR31.Name = "lbHR31";
            this.lbHR31.Size = new System.Drawing.Size(126, 22);
            this.lbHR31.TabIndex = 3;
            this.lbHR31.Text = "------";
            this.lbHR31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR32
            // 
            this.lbHR32.AutoEllipsis = true;
            this.lbHR32.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR32.Location = new System.Drawing.Point(282, 76);
            this.lbHR32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR32.Name = "lbHR32";
            this.lbHR32.Size = new System.Drawing.Size(126, 22);
            this.lbHR32.TabIndex = 3;
            this.lbHR32.Text = "------";
            this.lbHR32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR33
            // 
            this.lbHR33.AutoEllipsis = true;
            this.lbHR33.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR33.Location = new System.Drawing.Point(282, 103);
            this.lbHR33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR33.Name = "lbHR33";
            this.lbHR33.Size = new System.Drawing.Size(126, 22);
            this.lbHR33.TabIndex = 3;
            this.lbHR33.Text = "------";
            this.lbHR33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR34
            // 
            this.lbHR34.AutoEllipsis = true;
            this.lbHR34.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR34.Location = new System.Drawing.Point(282, 130);
            this.lbHR34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR34.Name = "lbHR34";
            this.lbHR34.Size = new System.Drawing.Size(126, 22);
            this.lbHR34.TabIndex = 3;
            this.lbHR34.Text = "------";
            this.lbHR34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR35
            // 
            this.lbHR35.AutoEllipsis = true;
            this.lbHR35.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR35.Location = new System.Drawing.Point(282, 157);
            this.lbHR35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR35.Name = "lbHR35";
            this.lbHR35.Size = new System.Drawing.Size(126, 22);
            this.lbHR35.TabIndex = 3;
            this.lbHR35.Text = "------";
            this.lbHR35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR36
            // 
            this.lbHR36.AutoEllipsis = true;
            this.lbHR36.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR36.Location = new System.Drawing.Point(282, 184);
            this.lbHR36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR36.Name = "lbHR36";
            this.lbHR36.Size = new System.Drawing.Size(126, 22);
            this.lbHR36.TabIndex = 3;
            this.lbHR36.Text = "------";
            this.lbHR36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR14_11
            // 
            this.lbHR14_11.AutoEllipsis = true;
            this.lbHR14_11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR14_11.Location = new System.Drawing.Point(282, 211);
            this.lbHR14_11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR14_11.Name = "lbHR14_11";
            this.lbHR14_11.Size = new System.Drawing.Size(126, 22);
            this.lbHR14_11.TabIndex = 3;
            this.lbHR14_11.Text = "------";
            this.lbHR14_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR1F
            // 
            this.lbHR1F.AutoEllipsis = true;
            this.lbHR1F.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR1F.Location = new System.Drawing.Point(282, 260);
            this.lbHR1F.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR1F.Name = "lbHR1F";
            this.lbHR1F.Size = new System.Drawing.Size(126, 22);
            this.lbHR1F.TabIndex = 3;
            this.lbHR1F.Text = "------";
            this.lbHR1F.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR1E
            // 
            this.lbHR1E.AutoEllipsis = true;
            this.lbHR1E.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR1E.Location = new System.Drawing.Point(282, 287);
            this.lbHR1E.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR1E.Name = "lbHR1E";
            this.lbHR1E.Size = new System.Drawing.Size(126, 22);
            this.lbHR1E.TabIndex = 3;
            this.lbHR1E.Text = "------";
            this.lbHR1E.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHR14_10
            // 
            this.lbHR14_10.AutoEllipsis = true;
            this.lbHR14_10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHR14_10.Location = new System.Drawing.Point(282, 314);
            this.lbHR14_10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHR14_10.Name = "lbHR14_10";
            this.lbHR14_10.Size = new System.Drawing.Size(126, 22);
            this.lbHR14_10.TabIndex = 3;
            this.lbHR14_10.Text = "------";
            this.lbHR14_10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edHR30
            // 
            this.edHR30.Location = new System.Drawing.Point(412, 1);
            this.edHR30.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.edHR30.Name = "edHR30";
            this.edHR30.Size = new System.Drawing.Size(132, 26);
            this.edHR30.TabIndex = 1;
            this.edHR30.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // edHR31
            // 
            this.edHR31.Location = new System.Drawing.Point(412, 50);
            this.edHR31.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.edHR31.Name = "edHR31";
            this.edHR31.Size = new System.Drawing.Size(132, 26);
            this.edHR31.TabIndex = 2;
            this.edHR31.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // edHR32
            // 
            this.edHR32.Location = new System.Drawing.Point(412, 77);
            this.edHR32.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.edHR32.Name = "edHR32";
            this.edHR32.Size = new System.Drawing.Size(132, 26);
            this.edHR32.TabIndex = 3;
            this.edHR32.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // edHR33
            // 
            this.edHR33.Location = new System.Drawing.Point(412, 104);
            this.edHR33.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.edHR33.Name = "edHR33";
            this.edHR33.Size = new System.Drawing.Size(132, 26);
            this.edHR33.TabIndex = 4;
            this.edHR33.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // edHR34
            // 
            this.edHR34.Location = new System.Drawing.Point(412, 131);
            this.edHR34.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.edHR34.Name = "edHR34";
            this.edHR34.Size = new System.Drawing.Size(132, 26);
            this.edHR34.TabIndex = 5;
            this.edHR34.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // edHR35
            // 
            this.edHR35.Location = new System.Drawing.Point(412, 158);
            this.edHR35.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.edHR35.Name = "edHR35";
            this.edHR35.Size = new System.Drawing.Size(132, 26);
            this.edHR35.TabIndex = 6;
            this.edHR35.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // edHR36
            // 
            this.edHR36.Location = new System.Drawing.Point(412, 185);
            this.edHR36.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.edHR36.Name = "edHR36";
            this.edHR36.Size = new System.Drawing.Size(132, 26);
            this.edHR36.TabIndex = 7;
            this.edHR36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbHR14_11
            // 
            this.cbHR14_11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHR14_11.FormattingEnabled = true;
            this.cbHR14_11.Items.AddRange(new object[] {
            "Датчик",
            "Крышка"});
            this.cbHR14_11.Location = new System.Drawing.Point(412, 212);
            this.cbHR14_11.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.cbHR14_11.Name = "cbHR14_11";
            this.cbHR14_11.Size = new System.Drawing.Size(132, 26);
            this.cbHR14_11.TabIndex = 8;
            // 
            // edHR1F
            // 
            this.edHR1F.Location = new System.Drawing.Point(412, 261);
            this.edHR1F.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.edHR1F.Name = "edHR1F";
            this.edHR1F.Size = new System.Drawing.Size(132, 26);
            this.edHR1F.TabIndex = 9;
            this.edHR1F.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // edHR1E
            // 
            this.edHR1E.Location = new System.Drawing.Point(412, 288);
            this.edHR1E.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.edHR1E.Name = "edHR1E";
            this.edHR1E.Size = new System.Drawing.Size(132, 26);
            this.edHR1E.TabIndex = 10;
            this.edHR1E.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbHR14_10
            // 
            this.cbHR14_10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHR14_10.FormattingEnabled = true;
            this.cbHR14_10.Items.AddRange(new object[] {
            "Отключена",
            "Включена"});
            this.cbHR14_10.Location = new System.Drawing.Point(412, 315);
            this.cbHR14_10.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.cbHR14_10.Name = "cbHR14_10";
            this.cbHR14_10.Size = new System.Drawing.Size(132, 26);
            this.cbHR14_10.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flowLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Controls.Add(this.btnRestore);
            this.flowLayoutPanel1.Controls.Add(this.btnEEPROM);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(90, 350);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(435, 38);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // btnRestore
            // 
            this.btnRestore.Enabled = false;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRestore.Location = new System.Drawing.Point(10, 3);
            this.btnRestore.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(146, 32);
            this.btnRestore.TabIndex = 12;
            this.btnRestore.Text = "Восстановить";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.BtnRestoreClick);
            // 
            // btnEEPROM
            // 
            this.btnEEPROM.Enabled = false;
            this.btnEEPROM.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEEPROM.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEEPROM.Location = new System.Drawing.Point(176, 3);
            this.btnEEPROM.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnEEPROM.Name = "btnEEPROM";
            this.btnEEPROM.Size = new System.Drawing.Size(94, 32);
            this.btnEEPROM.TabIndex = 13;
            this.btnEEPROM.Text = "EEPROM";
            this.btnEEPROM.UseVisualStyleBackColor = true;
            this.btnEEPROM.Click += new System.EventHandler(this.BtnEepromClick);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(290, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(135, 32);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Установить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
            // 
            // btnCopyFromStorage
            // 
            this.btnCopyFromStorage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCopyFromStorage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCopyFromStorage.Location = new System.Drawing.Point(116, 6);
            this.btnCopyFromStorage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCopyFromStorage.Name = "btnCopyFromStorage";
            this.btnCopyFromStorage.Size = new System.Drawing.Size(221, 26);
            this.btnCopyFromStorage.TabIndex = 0;
            this.btnCopyFromStorage.TabStop = false;
            this.btnCopyFromStorage.Text = "копировать из архива>>>";
            this.btnCopyFromStorage.UseVisualStyleBackColor = true;
            this.btnCopyFromStorage.Visible = false;
            this.btnCopyFromStorage.Click += new System.EventHandler(this.BtnCopyFromStorageClick);
            // 
            // RiserTuningAnalogLevelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCopyFromStorage);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "RiserTuningAnalogLevelControl";
            this.Size = new System.Drawing.Size(554, 432);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
        private System.Windows.Forms.Button btnCopyFromStorage;
		private System.Windows.Forms.ComboBox cbHR14_10;
		private System.Windows.Forms.TextBox edHR1E;
		private System.Windows.Forms.TextBox edHR1F;
		private System.Windows.Forms.ComboBox cbHR14_11;
		private System.Windows.Forms.TextBox edHR36;
		private System.Windows.Forms.TextBox edHR35;
		private System.Windows.Forms.TextBox edHR34;
		private System.Windows.Forms.TextBox edHR33;
		private System.Windows.Forms.TextBox edHR32;
		private System.Windows.Forms.TextBox edHR31;
		private System.Windows.Forms.TextBox edHR30;
		private System.Windows.Forms.Label lbHR14_10;
		private System.Windows.Forms.Label lbHR1E;
		private System.Windows.Forms.Label lbHR1F;
		private System.Windows.Forms.Label lbHR14_11;
		private System.Windows.Forms.Label lbHR36;
		private System.Windows.Forms.Label lbHR35;
		private System.Windows.Forms.Label lbHR34;
		private System.Windows.Forms.Label lbHR33;
		private System.Windows.Forms.Label lbHR32;
		private System.Windows.Forms.Label lbHR31;
        private System.Windows.Forms.Label lbHR24;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnEEPROM;
		private System.Windows.Forms.Button btnRestore;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label lbHR30;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Label label1;
	}
}
