using System;
using System.Windows.Forms;

namespace MultiFilling.RiserTuning
{
    public partial class FormRiserStatus : Form
    {
        public RiserAddress? RiserAddress { get; set; }

        public FormRiserStatus()
        {
            InitializeComponent();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;
            if (RiserAddress == null)
            {
                riserStatusControl1.UpdateTimeout();
                return;
            }
            bool active;
            ushort[] hregs;
            int riser, channel;
            var addr = (RiserAddress) RiserAddress;
            lock (Data.RiserNodes)
            {
                if (!Data.RiserNodes.ContainsKey(addr)) return;
                var riserNode = Data.RiserNodes[addr];
                active = riserNode.Active;
                hregs = riserNode.Hregs;
                channel = riserNode.Channel;
                riser = riserNode.Riser;
            }
            var remoted = true;
            lock (Data.ChannelNodes)
            {
                if (channel >= 0 && channel < Data.ChannelNodes.Count)
                    remoted = !Data.ChannelNodes[channel].Active;
            }
            Text = string.Format("Состояние [ Стояк {0} ]", riser);
            if (active)
                riserStatusControl1.UpdateData(hregs, remoted);
            else
                riserStatusControl1.UpdateTimeout();

        }
    }
}
