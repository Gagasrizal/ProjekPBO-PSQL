using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProjekPBO_PSQL
{
    public class TransparentTextBox : TextBox
    {
        private bool _isTransparent = true;

        [Category("Custom Properties")]
        [DefaultValue(true)]
        public bool IsTransparent
        {
            get { return _isTransparent; }
            set
            {
                _isTransparent = value;
                this.Invalidate();
            }
        }

        public TransparentTextBox()
        {
            this.BorderStyle = BorderStyle.None;
            this.ForeColor = Color.White;
        }

        // --- TRIK KHUSUS DESAINER VS ---
        // Jika sedang di dalam desainer, kita paksa textbox mengambil warna background panel di bawahnya
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent != null)
            {
                // Menyamakan warna latar belakang textbox dengan warna induknya di desainer
                this.BackColor = this.Parent.BackColor;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (_isTransparent)
                {
                    cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT untuk runtime asli
                }
                return cp;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_ERASEBKGND = 0x0014;
            if (m.Msg == WM_ERASEBKGND && _isTransparent)
            {
                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
        }
    }
}