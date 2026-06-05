using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProjekPBO_PSQL
{
    public class roundedpanel : Panel
    {
        private int _borderRadius = 20;
        private int _opacity = 125;
        private Color _customBackColor = Color.FromArgb(50, 60, 70);

        // 1. Default Value untuk Kelengkungan Sudut (20)
        [Category("Custom Properties")]
        [DefaultValue(20)]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value;
                UpdateRegion();
                this.Invalidate();
            }
        }

        // 2. Default Value untuk Transparansi (125)
        [Category("Custom Properties")]
        [DefaultValue(125)]
        public int Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = Math.Max(0, Math.Min(255, value));
                this.Invalidate();
            }
        }

        // 3. Default Value untuk Warna Kustom (Abu-abu gelap tema Hyper Chess: R=50, G=60, B=70)
        [Category("Custom Properties")]
        [Description("Warna latar belakang kustom untuk panel melengkung.")]
        [DefaultValue(typeof(Color), "50, 60, 70")] // Siasat default value untuk tipe data Color
        public Color CustomBackColor
        {
            get { return _customBackColor; }
            set
            {
                _customBackColor = value;
                this.Invalidate();
            }
        }

        public roundedpanel()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            base.BackColor = Color.Transparent;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent != null)
            {
                this.Parent.Invalidate();
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            UpdateRegion();
            this.Invalidate();
        }

        private void UpdateRegion()
        {
            if (this.Width > 0 && this.Height > 0)
            {
                using (GraphicsPath path = GetRoundedPath(this.ClientRectangle, _borderRadius))
                {
                    this.Region = new Region(path);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color finalPanelColor = Color.FromArgb(_opacity, _customBackColor);

            using (SolidBrush brush = new SolidBrush(finalPanelColor))
            {
                using (GraphicsPath path = GetRoundedPath(this.ClientRectangle, _borderRadius))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            if (diameter > rect.Width) diameter = rect.Width;
            if (diameter > rect.Height) diameter = rect.Height;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}