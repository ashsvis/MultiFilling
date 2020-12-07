using System.Drawing;
using System.Windows.Forms;

namespace MultiFilling.SystemStatus
{
    public partial class UcRiserOneState : UserControl
    {
        private bool _captionAtRight;
        private string _caption = "Некоторый текст";
        private Color _lampColorOn = SystemColors.Control;
        private Color _lampColorOff = SystemColors.Control;
        private Color _lampColorNone = Color.Gray;
        private bool? _state;

        public bool CaptionAtRight
        {
            get { return _captionAtRight; }
            set
            {
                _captionAtRight = value;
                tlpLeftToRight.Visible = !_captionAtRight;
                tlpRightToLeft.Visible = _captionAtRight;
            }
        }

        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                lbText.Text = value;
                lbTextRight.Text = value;
            }
        }

        public bool? State
        {
            get { return _state; }
            set
            {
                _state = value;
                pbLamp.Invalidate();
                pbLampRight.Invalidate();
            }
        }

        public Color LampColorOn
        {
            get { return _lampColorOn; }
            set { _lampColorOn = value; }
        }

        public Color LampColorOff
        {
            get { return _lampColorOff; }
            set { _lampColorOff = value; }
        }

        public Color LampColorNone
        {
            get { return _lampColorNone; }
            set { _lampColorNone = value; }
        }

        public UcRiserOneState()
        {
            InitializeComponent();
        }

        private void pbLamp_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var rect = new RectangleF(1, 1, 18, 18);
            using (var brush = new SolidBrush(State != null ? 
                ((bool)State ? LampColorOn : LampColorOff) : LampColorNone))
            {
                graphics.FillEllipse(brush, rect);
                graphics.DrawEllipse(Pens.Black, rect);
                rect.Inflate(-1, -1);
                graphics.DrawEllipse(Pens.Gray, rect);
            }
			var blick = new RectangleF(rect.X + 3, rect.Y + 3, 4, 4);
			graphics.FillEllipse(Brushes.White, blick);
        }
    }
}
