namespace ProjekPBO_PSQL
{
    partial class MenuPemain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPemain));
            roundedButton1 = new RoundedButton();
            linkLabel7 = new LinkLabel();
            linkLabel8 = new LinkLabel();
            linkLabel10 = new LinkLabel();
            linkLabel11 = new LinkLabel();
            SuspendLayout();
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(217, 217, 217);
            roundedButton1.BackgroundColor = Color.FromArgb(217, 217, 217);
            roundedButton1.BorderColor = Color.PaleVioletRed;
            roundedButton1.BorderRadius = 5;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.Location = new Point(13, 400);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(125, 35);
            roundedButton1.TabIndex = 11;
            roundedButton1.Text = "LogOut";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // linkLabel7
            // 
            linkLabel7.AutoSize = true;
            linkLabel7.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel7.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel7.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel7.LinkColor = Color.Silver;
            linkLabel7.Location = new Point(13, 227);
            linkLabel7.Name = "linkLabel7";
            linkLabel7.Size = new Size(99, 42);
            linkLabel7.TabIndex = 129;
            linkLabel7.TabStop = true;
            linkLabel7.Text = "Baca\r\nPeraturan";
            linkLabel7.LinkClicked += linkLabel7_LinkClicked;
            // 
            // linkLabel8
            // 
            linkLabel8.AutoSize = true;
            linkLabel8.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel8.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel8.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel8.LinkColor = Color.Silver;
            linkLabel8.Location = new Point(9, 157);
            linkLabel8.Name = "linkLabel8";
            linkLabel8.Size = new Size(129, 42);
            linkLabel8.TabIndex = 128;
            linkLabel8.TabStop = true;
            linkLabel8.Text = "History \r\nPertandingan";
            linkLabel8.LinkClicked += linkLabel8_LinkClicked;
            // 
            // linkLabel10
            // 
            linkLabel10.AutoSize = true;
            linkLabel10.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel10.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel10.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel10.LinkColor = Color.Silver;
            linkLabel10.Location = new Point(11, 99);
            linkLabel10.Name = "linkLabel10";
            linkLabel10.Size = new Size(117, 42);
            linkLabel10.TabIndex = 126;
            linkLabel10.TabStop = true;
            linkLabel10.Text = "List\r\nTournament";
            linkLabel10.LinkClicked += linkLabel10_LinkClicked;
            // 
            // linkLabel11
            // 
            linkLabel11.AutoSize = true;
            linkLabel11.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel11.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel11.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel11.LinkColor = Color.Silver;
            linkLabel11.Location = new Point(11, 59);
            linkLabel11.Name = "linkLabel11";
            linkLabel11.Size = new Size(57, 21);
            linkLabel11.TabIndex = 125;
            linkLabel11.TabStop = true;
            linkLabel11.Text = "Profil";
            linkLabel11.TextAlign = ContentAlignment.TopRight;
            linkLabel11.LinkClicked += linkLabel11_LinkClicked;
            // 
            // MenuPemain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 451);
            Controls.Add(linkLabel7);
            Controls.Add(linkLabel8);
            Controls.Add(linkLabel10);
            Controls.Add(linkLabel11);
            Controls.Add(roundedButton1);
            MaximizeBox = false;
            Name = "MenuPemain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuPemain";
            Load += MenuPemain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RoundedButton roundedButton1;
        private LinkLabel linkLabel7;
        private LinkLabel linkLabel8;
        private LinkLabel linkLabel10;
        private LinkLabel linkLabel11;
    }
}