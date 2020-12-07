using System.Windows.Forms;

namespace MultiFilling
{
    public class ListViewEx : ListView
    {
        public void SetDoubleBuffered(bool value)
        {
            DoubleBuffered = value;
        }
    }
}