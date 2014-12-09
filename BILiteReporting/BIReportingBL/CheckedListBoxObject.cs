using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BILiteReporting
{
    public class MyCheckedListBox : CheckedListBox
    {
        public event ScrollEventHandler Scroll;
        protected virtual void OnScroll(ScrollEventArgs e)
        {
            ScrollEventHandler handler = this.Scroll;
            if (handler != null) handler(this, e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x115 || m.Msg == 0x20A)
            {
                OnScroll(new ScrollEventArgs((ScrollEventType)(m.WParam.ToInt32() & 0xffff), 0));
            }

        }

        public MyCheckedListBox()
        {
        }
    }
}
