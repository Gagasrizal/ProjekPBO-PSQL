using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#nullable disable

namespace ProjekPBO_PSQL.View.Pemain
{
    public partial class MenuProfilPem : Form
    {
        public MenuProfilPem()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Label label21;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MenuProfilPem));
            label3 = new Label();
            linkLabel6 = new LinkLabel();
            roundedButton1 = new RoundedButton();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            roundedPictureBox1 = new RoundedPictureBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label17 = new Label();
            label18 = new Label();
            label20 = new Label();
            roundedpanel1 = new roundedpanel();
            label21 = new Label();
            ((ISupportInitialize)roundedPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.BackColor = Color.Transparent;
            label21.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label21.ForeColor = Color.Transparent;
            label21.Location = new Point(350, 425);
            label21.Name = "label21";
            label21.Size = new Size(233, 18);
            label21.TabIndex = 46;
            label21.Text = "Account Created on 26 Jan 2026";
            label21.TextAlign = ContentAlignment.TopCenter;
            label21.Click += label21_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Silver;
            label3.Location = new Point(334, 373);
            label3.Name = "label3";
            label3.Size = new Size(0, 32);
            label3.TabIndex = 26;
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // linkLabel6
            // 
            linkLabel6.AutoSize = true;
            linkLabel6.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel6.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel6.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel6.LinkColor = Color.Silver;
            linkLabel6.Location = new Point(11, 320);
            linkLabel6.Name = "linkLabel6";
            linkLabel6.Size = new Size(99, 42);
            linkLabel6.TabIndex = 24;
            linkLabel6.TabStop = true;
            linkLabel6.Text = "Baca\r\nPeraturan\r\n";
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
            roundedButton1.TabIndex = 23;
            roundedButton1.Text = "LogOut";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel5.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel5.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel5.LinkColor = Color.Silver;
            linkLabel5.Location = new Point(11, 260);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(129, 42);
            linkLabel5.TabIndex = 22;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "History\r\nPertandingan";
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel4.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel4.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel4.LinkColor = Color.Silver;
            linkLabel4.Location = new Point(11, 220);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(118, 21);
            linkLabel4.TabIndex = 21;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "Cari Pemain";
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel3.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel3.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel3.LinkColor = Color.Silver;
            linkLabel3.Location = new Point(11, 160);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(117, 42);
            linkLabel3.TabIndex = 20;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "Daftar\r\nTournament";
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel2.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel2.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel2.LinkColor = Color.Silver;
            linkLabel2.Location = new Point(11, 100);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(117, 42);
            linkLabel2.TabIndex = 19;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "List\r\nTournament";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel1.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel1.LinkColor = Color.Silver;
            linkLabel1.Location = new Point(11, 60);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(57, 21);
            linkLabel1.TabIndex = 18;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Profil";
            linkLabel1.TextAlign = ContentAlignment.TopRight;
            // 
            // roundedPictureBox1
            // 
            roundedPictureBox1.BorderColor = Color.PaleVioletRed;
            roundedPictureBox1.Image = Properties.Resources.Profil;
            roundedPictureBox1.Location = new Point(169, 60);
            roundedPictureBox1.Name = "roundedPictureBox1";
            roundedPictureBox1.Size = new Size(80, 80);
            roundedPictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            roundedPictureBox1.TabIndex = 28;
            roundedPictureBox1.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(277, 50);
            label4.Name = "label4";
            label4.Size = new Size(100, 21);
            label4.TabIndex = 29;
            label4.Text = "Username";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(277, 90);
            label5.Name = "label5";
            label5.Size = new Size(80, 21);
            label5.TabIndex = 30;
            label5.Text = "Country";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.ImageAlign = ContentAlignment.MiddleLeft;
            label6.Location = new Point(277, 130);
            label6.Name = "label6";
            label6.Size = new Size(99, 21);
            label6.TabIndex = 31;
            label6.Text = "Elo Rating";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(277, 170);
            label7.Name = "label7";
            label7.Size = new Size(185, 21);
            label7.TabIndex = 32;
            label7.Text = "Handphone Number\r\n";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.ImageAlign = ContentAlignment.MiddleLeft;
            label9.Location = new Point(277, 210);
            label9.Name = "label9";
            label9.Size = new Size(97, 21);
            label9.TabIndex = 34;
            label9.Text = "Birth Date";
            label9.TextAlign = ContentAlignment.TopCenter;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.White;
            label10.ImageAlign = ContentAlignment.MiddleLeft;
            label10.Location = new Point(277, 250);
            label10.Name = "label10";
            label10.Size = new Size(59, 21);
            label10.TabIndex = 35;
            label10.Text = "Email\r\n";
            label10.TextAlign = ContentAlignment.TopCenter;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.White;
            label11.ImageAlign = ContentAlignment.MiddleLeft;
            label11.Location = new Point(175, 301);
            label11.Name = "label11";
            label11.Size = new Size(110, 21);
            label11.TabIndex = 36;
            label11.Text = "Description";
            label11.TextAlign = ContentAlignment.TopCenter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.White;
            label12.ImageAlign = ContentAlignment.MiddleLeft;
            label12.Location = new Point(500, 50);
            label12.Name = "label12";
            label12.Size = new Size(69, 21);
            label12.TabIndex = 37;
            label12.Text = "RJalsn";
            label12.TextAlign = ContentAlignment.TopCenter;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = Color.Transparent;
            label13.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.Transparent;
            label13.ImageAlign = ContentAlignment.MiddleLeft;
            label13.Location = new Point(500, 130);
            label13.Name = "label13";
            label13.Size = new Size(50, 21);
            label13.TabIndex = 38;
            label13.Text = "1000";
            label13.TextAlign = ContentAlignment.TopCenter;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = Color.Transparent;
            label14.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.White;
            label14.ImageAlign = ContentAlignment.MiddleLeft;
            label14.Location = new Point(500, 90);
            label14.Name = "label14";
            label14.Size = new Size(95, 21);
            label14.TabIndex = 38;
            label14.Text = "Indonesia";
            label14.TextAlign = ContentAlignment.TopCenter;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.ImageAlign = ContentAlignment.MiddleLeft;
            label15.Location = new Point(464, 184);
            label15.Name = "label15";
            label15.Size = new Size(0, 20);
            label15.TabIndex = 39;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.BackColor = Color.Transparent;
            label17.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.ForeColor = Color.Transparent;
            label17.ImageAlign = ContentAlignment.MiddleLeft;
            label17.Location = new Point(500, 250);
            label17.Name = "label17";
            label17.Size = new Size(174, 21);
            label17.TabIndex = 41;
            label17.Text = "Gagas@gmail.com";
            label17.TextAlign = ContentAlignment.TopCenter;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.Transparent;
            label18.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.ForeColor = Color.Transparent;
            label18.ImageAlign = ContentAlignment.MiddleLeft;
            label18.Location = new Point(500, 210);
            label18.Name = "label18";
            label18.Size = new Size(150, 21);
            label18.TabIndex = 42;
            label18.Text = "28 January 2006";
            label18.TextAlign = ContentAlignment.TopCenter;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.BackColor = Color.Transparent;
            label20.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label20.ForeColor = Color.Transparent;
            label20.ImageAlign = ContentAlignment.MiddleLeft;
            label20.Location = new Point(500, 170);
            label20.Name = "label20";
            label20.Size = new Size(130, 21);
            label20.TabIndex = 44;
            label20.Text = "082330505712";
            label20.TextAlign = ContentAlignment.TopCenter;
            // 
            // roundedpanel1
            // 
            roundedpanel1.BackColor = Color.Transparent;
            roundedpanel1.Location = new Point(175, 325);
            roundedpanel1.Name = "roundedpanel1";
            roundedpanel1.Opacity = 200;
            roundedpanel1.Size = new Size(613, 97);
            roundedpanel1.TabIndex = 47;
            // 
            // MenuProfilPem
            // 
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(roundedpanel1);
            Controls.Add(label21);
            Controls.Add(label20);
            Controls.Add(label18);
            Controls.Add(label17);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(roundedPictureBox1);
            Controls.Add(label3);
            Controls.Add(linkLabel6);
            Controls.Add(roundedButton1);
            Controls.Add(linkLabel5);
            Controls.Add(linkLabel4);
            Controls.Add(linkLabel3);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            ForeColor = Color.Transparent;
            Name = "MenuProfilPem";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuProfilPemain";
            Load += MenuProfilPem_Load;
            ((ISupportInitialize)roundedPictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private Label label3;
        private LinkLabel linkLabel6;
        private RoundedButton roundedButton1;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private RoundedPictureBox roundedPictureBox1;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label17;
        private Label label18;
        private Label label20;
        private roundedpanel roundedpanel1;
        private Label label15;

        private void MenuProfilPem_Load(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void roundedpanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
