using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProjekPBO_PSQL
{
    public class RoundedPictureBox : PictureBox
    {
        // Fields
        private int borderSize = 0;
        private int borderRadius = 20;
        private Color borderColor = Color.PaleVioletRed;

        // Properties (Akan muncul di tab Properties Visual Studio)
        [Category("Custom Properties")]
        [DefaultValue(0)]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value < 0 ? 0 : value;
                this.Invalidate(); // Gambar ulang saat properti diubah
            }
        }

        [Category("Custom Properties")]
        [DefaultValue(20)]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value < 0 ? 0 : value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        [DefaultValue(0)]
        public Color BorderColor

        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        // Constructor
        public RoundedPictureBox()
        {
            this.Size = new Size(150, 150);
            this.SizeMode = PictureBoxSizeMode.Zoom; // Default biar gambar rapi pas dipotong
        }

        // Method untuk membuat rute melengkung
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            // Pengaman agar ukuran lengkungan tidak melebihi ukuran PictureBox
            if (curveSize > rect.Width) curveSize = rect.Width;
            if (curveSize > rect.Height) curveSize = rect.Height;
            if (curveSize <= 0) curveSize = 1;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = rectSurface;

            // Hitung ukuran kotak border agar pas di tengah-tengah garis tepi
            if (borderSize > 0)
            {
                rectBorder = new Rectangle(
                    rectSurface.X + borderSize / 2,
                    rectSurface.Y + borderSize / 2,
                    rectSurface.Width - borderSize,
                    rectSurface.Height - borderSize
                );
            }

            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (borderRadius > 2) // Jika sudutnya melengkung
            {
                // Hitung radius untuk border agar seimbang dengan tebal garis
                int borderRadiusCalc = borderRadius - (borderSize / 2);
                if (borderRadiusCalc < 1) borderRadiusCalc = 1;

                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadiusCalc))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    // Potong PictureBox sesuai lekukan
                    this.Region = new Region(pathSurface);

                    // Gambar garis tepi jika borderSize lebih dari 0
                    if (borderSize >= 1)
                    {
                        penBorder.Alignment = PenAlignment.Center;
                        pe.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else // Jika kotak biasa (Radius <= 2)
            {
                pe.Graphics.SmoothingMode = SmoothingMode.None;
                this.Region = new Region(rectSurface);

                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pe.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }

        // Otomatis batasi radius jika ukuran picturebox mengecil
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (borderRadius > this.Height) borderRadius = this.Height;
            if (borderRadius > this.Width) borderRadius = this.Width;
        }
    }
}