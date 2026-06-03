using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProjekPBO_PSQL
{
    public class roundedpanel : Panel
    {
        private int borderRadius = 30;
        private int bgOpacity = 125;
        private Color panelColor = Color.FromArgb(40, 60, 70);

        [Category("Custom Properties")]
        [DefaultValue(30)]
        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value < 0 ? 0 : value; this.Invalidate(); }
        }

        [Category("Custom Properties")]
        [Description("Mengatur transparansi background (0 = Tembus Pandang, 255 = Padat).")]
        [DefaultValue(125)]
        public int BgOpacity
        {
            get { return bgOpacity; }
            set
            {
                if (value < 0) bgOpacity = 0;
                else if (value > 255) bgOpacity = 255;
                else bgOpacity = value;

                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "40, 60, 70")]
        public Color PanelColor
        {
            get { return panelColor; }
            set { panelColor = value; this.Invalidate(); }
        }

        public roundedpanel()
        {
            // Mengaktifkan fitur transparansi tingkat lanjut pada kontrol
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            this.BackColor = Color.Transparent;
            this.Size = new Size(300, 150);
        }

        // KUNCI UTAMA: Memaksa Windows Forms mendukung transparansi lapisan (WS_EX_TRANSPARENT)
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Buat warna semi-transparan berdasarkan nilai BgOpacity
            Color semiTransparentColor = Color.FromArgb(bgOpacity, panelColor.R, panelColor.G, panelColor.B);

            if (borderRadius > 2 && Width > borderRadius && Height > borderRadius)
            {
                using (GraphicsPath gp = new GraphicsPath())
                {
                    float curveSize = borderRadius * 2F;
                    gp.StartFigure();
                    gp.AddArc(0, 0, curveSize, curveSize, 180, 90);
                    gp.AddArc(Width - curveSize, 0, curveSize, curveSize, 270, 90);
                    gp.AddArc(Width - curveSize, Height - curveSize, curveSize, curveSize, 0, 90);
                    gp.AddArc(0, Height - curveSize, curveSize, curveSize, 90, 90);
                    gp.CloseFigure();

                    this.Region = new Region(gp);

                    using (Brush brush = new SolidBrush(semiTransparentColor))
                    {
                        e.Graphics.FillPath(brush, gp);
                    }
                }
            }
            else
            {
                this.Region = new Region(this.ClientRectangle);
                using (Brush brush = new SolidBrush(semiTransparentColor))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            }
        }
    }
}