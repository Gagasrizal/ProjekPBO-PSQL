namespace ProjekPBO_PSQL.View.Pemain
{
    partial class MenuCariPemain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuCariPemain));
            roundedButton1 = new RoundedButton();
            roundedpanel1 = new roundedpanel();
            roundedButton2 = new RoundedButton();
            textBox1 = new TextBox();
            roundedpanel2 = new roundedpanel();
            linkLabel6 = new LinkLabel();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            roundedpanel1.SuspendLayout();
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
            roundedButton1.Location = new Point(11, 403);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(125, 35);
            roundedButton1.TabIndex = 30;
            roundedButton1.Text = "LogOut";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // roundedpanel1
            // 
            roundedpanel1.BackColor = Color.Transparent;
            roundedpanel1.BorderRadius = 5;
            roundedpanel1.Controls.Add(roundedButton2);
            roundedpanel1.Controls.Add(textBox1);
            roundedpanel1.CustomBackColor = Color.White;
            roundedpanel1.Location = new Point(226, 89);
            roundedpanel1.Name = "roundedpanel1";
            roundedpanel1.Opacity = 150;
            roundedpanel1.Size = new Size(525, 29);
            roundedpanel1.TabIndex = 48;
            roundedpanel1.Paint += roundedpanel1_Paint_1;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.White;
            roundedButton2.BackgroundColor = Color.White;
            roundedButton2.BorderColor = Color.PaleVioletRed;
            roundedButton2.BorderRadius = 3;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton2.ForeColor = Color.Black;
            roundedButton2.Location = new Point(398, 1);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(127, 25);
            roundedButton2.TabIndex = 50;
            roundedButton2.Text = "Cari";
            roundedButton2.TextColor = Color.Black;
            roundedButton2.UseCompatibleTextRendering = true;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(0, 1);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(345, 27);
            textBox1.TabIndex = 50;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // roundedpanel2
            // 
            roundedpanel2.BackColor = Color.Transparent;
            roundedpanel2.BorderRadius = 30;
            roundedpanel2.CustomBackColor = Color.White;
            roundedpanel2.Location = new Point(226, 160);
            roundedpanel2.Name = "roundedpanel2";
            roundedpanel2.Opacity = 150;
            roundedpanel2.Size = new Size(516, 268);
            roundedpanel2.TabIndex = 49;
            roundedpanel2.Paint += roundedpanel2_Paint;
            // 
            // linkLabel6
            // 
            linkLabel6.AutoSize = true;
            linkLabel6.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel6.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel6.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel6.LinkColor = Color.Silver;
            linkLabel6.Location = new Point(13, 279);
            linkLabel6.Name = "linkLabel6";
            linkLabel6.Size = new Size(99, 42);
            linkLabel6.TabIndex = 119;
            linkLabel6.TabStop = true;
            linkLabel6.Text = "Baca\r\nPeraturan";
            linkLabel6.LinkClicked += linkLabel6_LinkClicked;
            // 
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel5.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel5.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel5.LinkColor = Color.Silver;
            linkLabel5.Location = new Point(9, 209);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(129, 42);
            linkLabel5.TabIndex = 118;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "History \r\nPertandingan";
            linkLabel5.LinkClicked += linkLabel5_LinkClicked_1;
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel4.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel4.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel4.LinkColor = Color.Silver;
            linkLabel4.Location = new Point(11, 169);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(118, 21);
            linkLabel4.TabIndex = 117;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "Cari Pemain";
            linkLabel4.LinkClicked += linkLabel4_LinkClicked_1;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel2.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel2.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel2.LinkColor = Color.Silver;
            linkLabel2.Location = new Point(11, 98);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(117, 42);
            linkLabel2.TabIndex = 116;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "List\r\nTournament";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked_1;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel1.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel1.LinkColor = Color.Silver;
            linkLabel1.Location = new Point(11, 58);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(57, 21);
            linkLabel1.TabIndex = 115;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Profil";
            linkLabel1.TextAlign = ContentAlignment.TopRight;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked_1;
            // 
            // MenuCariPemain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(linkLabel6);
            Controls.Add(linkLabel5);
            Controls.Add(linkLabel4);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(roundedpanel2);
            Controls.Add(roundedpanel1);
            Controls.Add(roundedButton1);
            Name = "MenuCariPemain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuCariPemain";
            roundedpanel1.ResumeLayout(false);
            roundedpanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RoundedButton roundedButton1;
        private roundedpanel roundedpanel1;
        private roundedpanel roundedpanel2;
        private RoundedButton roundedButton2;
        private TextBox textBox1;
        private LinkLabel linkLabel6;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
    }
}