using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ProjekPBO_PSQL.View
{
    public class RoundedButton : Button
    {
        //Fields
        private int borderSize = 0;
        private int borderRadius = 20;
        private Color borderColor = Color.PaleVioletRed;

        // Field tambahan untuk efek klik
        private bool isPressed = false;

        //Properties
        [Category("RJ Code Advance")]
        [DefaultValue(0)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                // Pengaman agar border tidak minus
                borderSize = value < 0 ? 0 : value;
                this.Invalidate();
            }
        }

        [Category("RJ Code Advance")]
        [DefaultValue(20)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.Invalidate(); }
        }

        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        [Category("RJ Code Advance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }

        //Constructor
        public RoundedButton()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.ResizeRedraw, true);

            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;
            this.Cursor = Cursors.Hand;
            this.Resize += new EventHandler(Button_Resize);
        }

        // --- EFEK KLIK MASUK KE DALAM (DITEKAN) ---
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            isPressed = true;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            isPressed = false;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isPressed = false;
            this.Invalidate();
        }

        //Methods
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            // Pengaman jika ukuran tombol terlalu kecil dibanding radius
            if (curveSize > rect.Width) curveSize = rect.Width;
            if (curveSize > rect.Height) curveSize = rect.Height;
            if (curveSize <= 0) curveSize = 1; // Mencegah nilai 0 atau minus

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Rectangle rectSurface = this.ClientRectangle;

            // Perbaikan kalkulasi ukuran kotak border agar pas di tengah garis (PenAlignment.Center/Inset)
            Rectangle rectBorder = rectSurface;
            if (borderSize > 0)
            {
                rectBorder = new Rectangle(
                    rectSurface.X + borderSize / 2,
                    rectSurface.Y + borderSize / 2,
                    rectSurface.Width - borderSize,
                    rectSurface.Height - borderSize
                );
            }

            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color currentBackColor = this.BackColor;
            Color currentBorderColor = this.BorderColor;

            if (isPressed)
            {
                currentBackColor = ControlPaint.Dark(this.BackColor, 0.15f);
                currentBorderColor = ControlPaint.Dark(this.BorderColor, 0.15f);
            }

            if (borderRadius > 2) // Rounded button
            {
                // Pengaman rumus radius border agar tidak pernah bernilai 0 atau negatif
                int borderRadiusCalc = borderRadius - (borderSize / 2);
                if (borderRadiusCalc < 1) borderRadiusCalc = 1;

                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadiusCalc))
                using (Brush brushBackend = new SolidBrush(currentBackColor))
                using (Pen penBorder = new Pen(currentBorderColor, borderSize))
                {
                    this.Region = new Region(pathSurface);
                    pevent.Graphics.FillPath(brushBackend, pathSurface);

                    if (borderSize >= 1)
                    {
                        penBorder.Alignment = PenAlignment.Center; // Menggunakan Center agar border simetris
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else // Normal button
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                this.Region = new Region(rectSurface);

                using (Brush brushBackend = new SolidBrush(currentBackColor))
                {
                    pevent.Graphics.FillRectangle(brushBackend, rectSurface);
                }

                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(currentBorderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }

            // --- PENGATURAN TEKS MASUK KE DALAM ---
            Rectangle textBounds = this.ClientRectangle;
            if (isPressed)
            {
                textBounds.Offset(2, 2);
            }

            TextRenderer.DrawText(
                pevent.Graphics,
                this.Text,
                this.Font,
                textBounds,
                this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (this.Parent != null)
            {
                this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
            }
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }
    }
}