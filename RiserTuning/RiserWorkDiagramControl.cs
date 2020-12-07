using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace MultiFilling.RiserTuning
{
    public partial class RiserWorkDiagramControl : UserControl, IFetchUpdate
    {
        public event WriteData OnWrite;

        private ushort _hr15;
        private ushort _hr16;
        private ushort _hr17;
        private ushort _hr18;

        public RiserWorkDiagramControl()
        {
            InitializeComponent();
        }

        public int NodeType { get; set; }

        public void UpdateData(ushort[] hregs, bool remoted)
        {
            if (hregs.Length != 61) return;
            _hr15 = hregs[0x15];
            _hr16 = hregs[0x16];
            _hr17 = hregs[0x17];
            _hr18 = hregs[0x18];

            HR03_0.Tag = (hregs[0x03] & 0x01) > 0 ? Color.Lime : Color.Black;
            HR03_1.Tag = (hregs[0x03] & 0x02) > 0 ? Color.Lime : Color.Black;
            HR03_2.Tag = (hregs[0x03] & 0x04) > 0 ? Color.Lime : Color.Black;
            HR03_3.Tag = (hregs[0x03] & 0x08) > 0 ? Color.Lime : Color.Black;
            HR03_4.Tag = (hregs[0x03] & 0x10) > 0 ? Color.Lime : Color.Black;
            HR03_4A.Tag = (hregs[0x03] & 0x10) > 0 ? Color.Lime : Color.Black;
            HR03_5.Tag = (hregs[0x03] & 0x20) > 0 ? Color.Lime : Color.Black;
            HR03_5A.Tag = (hregs[0x03] & 0x20) > 0 ? Color.Lime : Color.Black;
            HR03_6.Tag = (hregs[0x03] & 0x40) > 0 ? Color.Lime : Color.Black;
            HR03_6A.Tag = (hregs[0x03] & 0x40) > 0 ? Color.Lime : Color.Black;
            HR03_7.Tag = (hregs[0x03] & 0x80) > 0 ? Color.Lime : Color.Black;
            HR03_10.Tag = (hregs[0x03] & 0x0400) > 0 ? Color.Red : Color.Black;
            HR03_11.Tag = (hregs[0x03] & 0x0800) > 0 ? Color.Red : Color.Black;
            HR03_14.Tag = (hregs[0x03] & 0x4000) > 0 ? Color.Red : Color.Black;
            HR03_15.Tag = (hregs[0x03] & 0x8000) > 0 ? Color.Red : Color.Black;

            HR04_0.Tag = (hregs[0x04] & 0x01) > 0 ? Color.Red : Color.Black;
            HR04_0A.Tag = (hregs[0x04] & 0x01) > 0 ? Color.Red : Color.Black;
            HR04_2.Tag = (hregs[0x04] & 0x04) > 0 ? Color.Red : Color.Black;
            HR04_3.Tag = (hregs[0x04] & 0x08) > 0 ? Color.Red : Color.Black;
            HR04_5.Tag = (hregs[0x04] & 0x20) > 0 ? Color.Red : Color.Black;

            HR14_0.Tag = (hregs[0x14] & 0x01) > 0 ? Color.Lime : Color.Black;
            HR14_4.Tag = (hregs[0x14] & 0x10) > 0 ? Color.Lime : Color.Black;
            HR14_6.Tag = (hregs[0x14] & 0x040) > 0 ? Color.Lime : Color.Black;
            HR14_7.Tag = (hregs[0x14] & 0x80) > 0 ? Color.Lime : Color.Black;
            HR14_8.Tag = (hregs[0x14] & 0x0100) > 0 ? Color.Lime : Color.Black;
            HR14_9.Tag = (hregs[0x14] & 0x0200) > 0 ? Color.Lime : Color.Black;
            HR14_10.Tag = (hregs[0x14] & 0x0400) > 0 ? Color.Lime : Color.Black;
            HR14_11.Tag = (hregs[0x14] & 0x0800) > 0 ? Color.Lime : Color.Black;

            HR15_0.Tag = (hregs[0x15] & 0x01) > 0;
            HR15_1.Tag = (hregs[0x15] & 0x02) > 0;
            HR15_5.Tag = (hregs[0x15] & 0x20) > 0;

            HR18_0.Tag = (hregs[0x18] & 0x01) > 0;
            HR18_1.Tag = (hregs[0x18] & 0x02) > 0;
            HR18_2.Tag = (hregs[0x18] & 0x04) > 0;
            HR18_4.Tag = (hregs[0x18] & 0x10) > 0;
            HR18_5.Tag = (hregs[0x18] & 0x20) > 0;
            HR18_6.Tag = (hregs[0x18] & 0x40) > 0;
            HR18_7.Tag = (hregs[0x18] & 0x80) > 0;
            HR18_8.Tag = (hregs[0x18] & 0x0100) > 0;

            InvalidatePictureBoxes();

            lbHR00.Text = hregs[0x00].ToString("0");
            lbHR07.Text = hregs[0x07].ToString("0");
            lbHR16.Text = hregs[0x16].ToString("X4");
            lbHR17.Text = hregs[0x17].ToString("X4");
            lbHR1E.Text = hregs[0x1E].ToString("0");
            lbHR1F.Text = hregs[0x1F].ToString("0");
            lbHR20.Text = hregs[0x20].ToString("0");
            lbHR21.Text = hregs[0x21].ToString("0");
            lbHR22.Text = hregs[0x22].ToString("X4");
            lbHR23.Text = hregs[0x23].ToString("X4");
            lbHR24.Text = hregs[0x24].ToString("0");
            lbHR24A.Text = hregs[0x24].ToString("0");
            lbHR25.Text = hregs[0x25].ToString("0");
            lbHR25A.Text = hregs[0x25].ToString("0");
            lbHR26.Text = hregs[0x26].ToString("0");
            lbHR27.Text = hregs[0x27].ToString("0");
            lbHR2D.Text = hregs[0x2D].ToString("0");
            lbHR2E.Text = hregs[0x2E].ToString("0");
            lbHR2F.Text = hregs[0x2F].ToString("0");
            lbHR35.Text = hregs[0x35].ToString("0");

            var enabled = Data.UserLevel >= UserLevel.Oper && !remoted;
            HR15_0.Enabled = HR15_1.Enabled = HR15_5.Enabled = enabled;
            HR18_0.Enabled = HR18_1.Enabled = HR18_2.Enabled = HR18_4.Enabled =
                HR18_5.Enabled = HR18_6.Enabled = HR18_7.Enabled = HR18_8.Enabled = enabled;
            lbHR16.Enabled = lbHR17.Enabled = enabled;
        }

        private void ClearValues()
        {
            _hr15 = 0;
            _hr16 = 0;
            _hr17 = 0;
            _hr18 = 0;

            HR03_0.Tag = null;
            HR03_1.Tag = null;
            HR03_2.Tag = null;
            HR03_3.Tag = null;
            HR03_4.Tag = null;
            HR03_4A.Tag = null;
            HR03_5.Tag = null;
            HR03_5A.Tag = null;
            HR03_6.Tag = null;
            HR03_6A.Tag = null;
            HR03_7.Tag = null;
            HR03_10.Tag = null;
            HR03_11.Tag = null;
            HR03_14.Tag = null;
            HR03_15.Tag = null;

            HR04_0.Tag = null;
            HR04_0A.Tag = null;
            HR04_2.Tag = null;
            HR04_3.Tag = null;
            HR04_5.Tag = null;

            HR14_0.Tag = null;
            HR14_4.Tag = null;
            HR14_6.Tag = null;
            HR14_7.Tag = null;
            HR14_8.Tag = null;
            HR14_9.Tag = null;
            HR14_10.Tag = null;
            HR14_11.Tag = null;

            HR15_0.Tag = null;
            HR15_1.Tag = null;
            HR15_5.Tag = null;

            HR18_0.Tag = null;
            HR18_1.Tag = null;
            HR18_2.Tag = null;
            HR18_4.Tag = null;
            HR18_5.Tag = null;
            HR18_6.Tag = null;
            HR18_7.Tag = null;
            HR18_8.Tag = null;

            InvalidatePictureBoxes();

            lbHR16.Text = @"HR16";
            lbHR17.Text = @"HR17";
            lbHR26.Text = @"HR26";
            lbHR27.Text = @"HR27";
            lbHR22.Text = @"HR26";
            lbHR23.Text = @"HR27";
            lbHR1E.Text = @"HR1E";
            lbHR1F.Text = @"HR1F";
            lbHR20.Text = @"HR20";
            lbHR21.Text = @"HR21";
            lbHR24.Text = @"HR24";
            lbHR24A.Text = @"HR24";
            lbHR25.Text = @"HR25";
            lbHR25A.Text = @"HR25";
            lbHR07.Text = @"HR07";
            lbHR2D.Text = @"HR2D";
            lbHR2E.Text = @"HR2E";
            lbHR2F.Text = @"HR2F";
            lbHR35.Text = @"HR35";
            lbHR00.Text = @"HR00";
        }

        private void InvalidatePictureBoxes()
        {
            foreach (var pbox in Controls.OfType<PictureBox>())
            {
                pbox.Invalidate();
            }
        }

        void IFetchUpdate.UpdateTimeout()
        {
            ClearValues();
        }

        private void pbLamp_Paint(object sender, PaintEventArgs e)
        {
            var pbox = (PictureBox) sender;
            var graphics = e.Graphics;
            var rect = new RectangleF(1, 1, 18, 18);
            Color color;
            if (pbox.Tag == null)
                color = Color.Gray;
            else
            {
                if (pbox.Tag is Color)
                    color = (Color) pbox.Tag;
                else
                    color = (bool) pbox.Tag ? Color.Lime : Color.Black;
            }
            using (var brush = new SolidBrush(color))
            {
                graphics.FillEllipse(brush, rect);
                graphics.DrawEllipse(Pens.Black, rect);
                rect.Inflate(-1, -1);
                graphics.DrawEllipse(Pens.Gray, rect);
            }
            var blick = new RectangleF(rect.X + 3, rect.Y + 3, 4, 4);
            graphics.FillEllipse(Brushes.White, blick);
        }

        private static void SetHRegFlag(ref ushort hreg, int bit, bool value)
        {
            if (value)
                hreg |= (ushort)(0x01 << bit);
            else
                hreg &= (ushort)~(0x01 << bit);
        }

        private string[] PrepareForChangeLog()
        {
            var list = new List<string>();
            AddChangeItemHr15(list, HR15_0, 0x01, "Разрешение симуляции дискретных сигналов");
            AddChangeItemHr15(list, HR15_1, 0x02, "Разрешение симуляции кода для ADC0");
            AddChangeItemHr15(list, HR15_5, 0x20, "Разрешение симуляции кода для ADC1");
            AddChangeItemHr18(list, HR18_0, 0x01, "Симуляция кнопки ПУСК нормально открытый контакт");
            AddChangeItemHr18(list, HR18_1, 0x02, "Симуляция конечника клапана большого прохода");
            AddChangeItemHr18(list, HR18_2, 0x04, "Симуляция конечника клапана малого прохода");
            AddChangeItemHr18(list, HR18_4, 0x10, "Симуляция конечника рабочего положения");
            AddChangeItemHr18(list, HR18_5, 0x20, "Симуляция контроля заземления");
            AddChangeItemHr18(list, HR18_6, 0x40, "Симуляция исправности сигнализатора уровня");
            AddChangeItemHr18(list, HR18_7, 0x80, "Симуляция кнопки АВТОНОМНО");
            AddChangeItemHr18(list, HR18_8, 0x0100, "Запрет симуляции цепи готовности");

            if (nudHex.Tag != null && (int) nudHex.Tag == 0x16)
            {
                if (lbHR16.Text != ((int)nudHex.Value).ToString("X4"))
                    list.Add(string.Concat("HR16", "\t", lbHR16.Text, "\t", ((int)nudHex.Value).ToString("X4"), "\t", 
                        "Значение симуляции кода для ADC0"));
            }
            if (nudHex.Tag != null && (int)nudHex.Tag == 0x17)
            {
                if (lbHR17.Text != ((int)nudHex.Value).ToString("X4"))
                    list.Add(string.Concat("HR17", "\t", lbHR17.Text, "\t", ((int)nudHex.Value).ToString("X4"), "\t",
                        "Значение симуляции кода для ADC1"));
            }

            return list.ToArray();
        }

        private void AddChangeItemHr15(ICollection<string> list, Control control, int mask, string message)
        {
            if ((bool)control.Tag != (_hr15 & mask) > 0)
                list.Add(string.Concat(control.Name.Replace('_','.'), "\t", (bool)control.Tag ? "1" : "0",
                    "\t", (_hr15 & mask) > 0 ? "1" : "0", "\t", message));            
        }

        private void AddChangeItemHr18(ICollection<string> list, Control control, int mask, string message)
        {
            if ((bool)control.Tag != (_hr18 & mask) > 0)
                list.Add(string.Concat(control.Name.Replace('_', '.'), "\t", (bool)control.Tag ? "1" : "0",
                    "\t", (_hr18 & mask) > 0 ? "1" : "0", "\t", message));
        }

        private void UpdateBitHr15(Control control, int bit)
        {
            if (OnWrite == null) return;
            if (control.Tag == null) return;
            var value = (bool)control.Tag;
            SetHRegFlag(ref _hr15, bit, !value);
            var hregs = new ushort[1];
            hregs[0] = _hr15;
            OnWrite(0x15, 1, hregs, PrepareForChangeLog());
            control.Tag = null;
            control.Invalidate();
            nudHex.Visible = false;
        }

        private void UpdateBitHr18(Control control, int bit)
        {
            if (OnWrite == null) return;
            if (control.Tag == null) return;
            var value = (bool)control.Tag;
            SetHRegFlag(ref _hr18, bit, !value);
            var hregs = new ushort[1];
            hregs[0] = _hr18;
            OnWrite(0x18, 1, hregs, PrepareForChangeLog());
            control.Tag = null;
            control.Invalidate();
            nudHex.Visible = false;
        }

        private void HR15_0_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr15(HR15_0, 0);
        }

        private void HR15_1_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr15(HR15_1, 1);
        }

        private void HR15_5_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr15(HR15_5, 5);
        }

        private void HR18_0_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr18(HR18_0, 0);
        }

        private void HR18_1_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr18(HR18_1, 1);
        }

        private void HR18_2_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr18(HR18_2, 2);
        }

        private void HR18_4_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr18(HR18_4, 4);
        }

        private void HR18_5_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr18(HR18_5, 5);
        }

        private void HR18_6_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr18(HR18_6, 6);
        }

        private void HR18_7_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr18(HR18_7, 7);
        }

        private void HR18_8_DoubleClick(object sender, System.EventArgs e)
        {
            UpdateBitHr18(HR18_8, 8);
        }

        private void lbHR16_DoubleClick(object sender, System.EventArgs e)
        {
            var fp = CultureInfo.GetCultureInfo("en-US");
            ushort reg;
            switch (NodeType)
            {
                case 0: // Камышинский контроллер
                    if (!ushort.TryParse(lbHR16.Text, NumberStyles.HexNumber, fp, out reg) || reg < nudHex.Minimum ||
                        reg > nudHex.Maximum) return;
                    nudHex.Tag = 0x16;
                    nudHex.Value = reg;
                    nudHex.Location = new Point(lbHR16.Left + lbHR16.Width - nudHex.Width, lbHR16.Top - 3);
                    nudHex.Visible = true;
                    nudHex.Focus();
                    nudHex.Select(0, nudHex.Text.Length);
                    break;
                case 1: // CPM 712 Fastwel
                    if (!ushort.TryParse(lbHR24.Text, NumberStyles.Integer, fp, out reg) || reg < nudCurrent.Minimum ||
                        reg > nudCurrent.Maximum) return;
                    nudCurrent.Tag = 0x16;
                    nudCurrent.Value = reg;
                    nudCurrent.Location = new Point(lbHR16.Left + lbHR16.Width - nudCurrent.Width,
                                                    lbHR16.Top - 3);
                    nudCurrent.Visible = true;
                    nudCurrent.Focus();
                    nudCurrent.Select(0, nudCurrent.Text.Length);
                    break;
            }
        }

        private void lbHR17_DoubleClick(object sender, System.EventArgs e)
        {
            var fp = CultureInfo.GetCultureInfo("en-US");
            ushort reg;
            switch (NodeType)
            {
                case 0: // Камышинский контроллер
                    if (!ushort.TryParse(lbHR17.Text, NumberStyles.HexNumber, fp, out reg) || reg < nudHex.Minimum ||
                        reg > nudHex.Maximum) return;
                    nudHex.Tag = 0x17;
                    nudHex.Value = reg;
                    nudHex.Location = new Point(lbHR17.Left + lbHR17.Width - nudHex.Width, lbHR17.Top - 3);
                    nudHex.Visible = true;
                    nudHex.Focus();
                    nudHex.Select(0, nudHex.Text.Length);
                    break;
                case 1: // CPM 712 Fastwel
                    if (!ushort.TryParse(lbHR25.Text, NumberStyles.Integer, fp, out reg) || reg < nudCurrent.Minimum ||
                        reg > nudCurrent.Maximum) return;
                    nudCurrent.Tag = 0x17;
                    nudCurrent.Value = reg;
                    nudCurrent.Location = new Point(lbHR17.Left + lbHR17.Width - nudCurrent.Width, lbHR17.Top - 3);
                    nudCurrent.Visible = true;
                    nudCurrent.Focus();
                    nudCurrent.Select(0, nudCurrent.Text.Length);
                    break;
            }
        }

        private void nudHex_Leave(object sender, System.EventArgs e)
        {
            nudHex.Visible = false;
        }

        private void nudHex_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    var hregs = new ushort[1];
                    hregs[0] = (ushort) nudHex.Value;
                    OnWrite((int)nudHex.Tag, 1, hregs, PrepareForChangeLog());
                    nudHex.Visible = false;
                    break;
                case Keys.Escape:
                    nudHex.Visible = false;
                    break;
            }
        }

        private void nudCurrent_Leave(object sender, System.EventArgs e)
        {
            nudCurrent.Visible = false;
            nudHex.Visible = false;
        }

        private void nudCurrent_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    var hregs = new ushort[1];
                    hregs[0] = (ushort)nudCurrent.Value;
                    OnWrite((int)nudCurrent.Tag, 1, hregs, PrepareForChangeLog());
                    nudCurrent.Visible = false;
                    break;
                case Keys.Escape:
                    nudCurrent.Visible = false;
                    break;
            }
        }
    }
}
