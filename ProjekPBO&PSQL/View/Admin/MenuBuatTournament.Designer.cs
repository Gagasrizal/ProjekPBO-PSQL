namespace ProjekPBO_PSQL.View.Admin
{
    partial class MenuBuatTournament
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuBuatTournament));
            roundedButton1 = new RoundedButton();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            label1 = new Label();
            NamaTournament = new TextBox();
            label2 = new Label();
            Babak = new ComboBox();
            TipeGame = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            HargaPendaftaran = new TextBox();
            Hadiah = new TextBox();
            label5 = new Label();
            label6 = new Label();
            TanggalPelaksanaan = new DateTimePicker();
            label7 = new Label();
            TimeControl = new ComboBox();
            roundedButton2 = new RoundedButton();
            linkLabel2 = new LinkLabel();
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
            roundedButton1.Location = new Point(9, 800);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(260, 48);
            roundedButton1.TabIndex = 27;
            roundedButton1.Text = "LogOut";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel5.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel5.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel5.LinkColor = Color.Silver;
            linkLabel5.Location = new Point(11, 280);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(200, 68);
            linkLabel5.TabIndex = 26;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "Lihat Data\r\nPembayaran";
            linkLabel5.LinkClicked += linkLabel5_LinkClicked;
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel4.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel4.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel4.LinkColor = Color.Silver;
            linkLabel4.Location = new Point(11, 180);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(193, 68);
            linkLabel4.TabIndex = 25;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "Lihat Data\r\nTournament";
            linkLabel4.LinkClicked += linkLabel4_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel1.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel1.LinkColor = Color.Silver;
            linkLabel1.Location = new Point(11, 120);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(92, 34);
            linkLabel1.TabIndex = 22;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Profil";
            linkLabel1.TextAlign = ContentAlignment.TopRight;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(330, 120);
            label1.Name = "label1";
            label1.Size = new Size(459, 34);
            label1.TabIndex = 28;
            label1.Text = "Masukkan Nama Tournament :";
            // 
            // NamaTournament
            // 
            NamaTournament.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NamaTournament.Location = new Point(330, 175);
            NamaTournament.Name = "NamaTournament";
            NamaTournament.Size = new Size(544, 43);
            NamaTournament.TabIndex = 29;
            NamaTournament.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(330, 497);
            label2.Name = "label2";
            label2.Size = new Size(126, 34);
            label2.TabIndex = 30;
            label2.Text = "Babak :";
            // 
            // Babak
            // 
            Babak.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Babak.FormattingEnabled = true;
            Babak.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7" });
            Babak.Location = new Point(330, 554);
            Babak.Name = "Babak";
            Babak.Size = new Size(544, 45);
            Babak.TabIndex = 31;
            Babak.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // TipeGame
            // 
            TipeGame.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TipeGame.FormattingEnabled = true;
            TipeGame.Items.AddRange(new object[] { "Rapid (10+0)", "Blitz    (5+0)", "Bullet  (1+0)" });
            TipeGame.Location = new Point(330, 291);
            TipeGame.Name = "TipeGame";
            TipeGame.Size = new Size(544, 45);
            TipeGame.TabIndex = 32;
            TipeGame.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(330, 238);
            label3.Name = "label3";
            label3.Size = new Size(191, 34);
            label3.TabIndex = 33;
            label3.Text = "Tipe Game :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(1050, 120);
            label4.Name = "label4";
            label4.Size = new Size(312, 34);
            label4.TabIndex = 34;
            label4.Text = "Harga Pendaftaran :";
            // 
            // HargaPendaftaran
            // 
            HargaPendaftaran.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            HargaPendaftaran.Location = new Point(1050, 175);
            HargaPendaftaran.Name = "HargaPendaftaran";
            HargaPendaftaran.Size = new Size(444, 43);
            HargaPendaftaran.TabIndex = 35;
            HargaPendaftaran.TextChanged += textBox2_TextChanged;
            // 
            // Hadiah
            // 
            Hadiah.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Hadiah.Location = new Point(1050, 291);
            Hadiah.Name = "Hadiah";
            Hadiah.Size = new Size(444, 43);
            Hadiah.TabIndex = 36;
            Hadiah.TextChanged += textBox3_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(1050, 238);
            label5.Name = "label5";
            label5.Size = new Size(136, 34);
            label5.TabIndex = 37;
            label5.Text = "Hadiah :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(330, 628);
            label6.Name = "label6";
            label6.Size = new Size(346, 34);
            label6.TabIndex = 38;
            label6.Text = "Tanggal Pelaksanaan :";
            // 
            // TanggalPelaksanaan
            // 
            TanggalPelaksanaan.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TanggalPelaksanaan.Location = new Point(330, 695);
            TanggalPelaksanaan.Name = "TanggalPelaksanaan";
            TanggalPelaksanaan.Size = new Size(428, 43);
            TanggalPelaksanaan.TabIndex = 39;
            TanggalPelaksanaan.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(330, 364);
            label7.Name = "label7";
            label7.Size = new Size(220, 34);
            label7.TabIndex = 40;
            label7.Text = "Time Control :";
            // 
            // TimeControl
            // 
            TimeControl.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TimeControl.FormattingEnabled = true;
            TimeControl.Items.AddRange(new object[] { "1 Menit (Bullet)", "1 Menit + 1 Detik (Bullet)", "2 Menit + 1 Detik (Bullet)", "3 Menit (Blitz)", "3 Menit + 2 Detik (Blitz)", "5 Menit (Blitz)", "5 Menit + 5 Detik (Blitz)", "10 Menit (Rapid)", "15 Menit + 10 Detik (Rapid)", "30 Menit (Rapid)", "60 Menit (Classical)", "90 Menit + 30 Detik (Classical)" });
            TimeControl.Location = new Point(330, 415);
            TimeControl.Name = "TimeControl";
            TimeControl.Size = new Size(544, 45);
            TimeControl.TabIndex = 41;
            TimeControl.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(126, 217, 87);
            roundedButton2.BackgroundColor = Color.FromArgb(126, 217, 87);
            roundedButton2.BorderColor = Color.PaleVioletRed;
            roundedButton2.BorderRadius = 5;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(1310, 800);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(260, 48);
            roundedButton2.TabIndex = 42;
            roundedButton2.Text = "Create";
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click_1;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel2.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel2.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel2.LinkColor = Color.Silver;
            linkLabel2.Location = new Point(11, 380);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(212, 68);
            linkLabel2.TabIndex = 43;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Lihat Data\r\npertandingan";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // MenuBuatTournament
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1594, 860);
            Controls.Add(linkLabel2);
            Controls.Add(roundedButton2);
            Controls.Add(TimeControl);
            Controls.Add(label7);
            Controls.Add(TanggalPelaksanaan);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(Hadiah);
            Controls.Add(HargaPendaftaran);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(TipeGame);
            Controls.Add(Babak);
            Controls.Add(label2);
            Controls.Add(NamaTournament);
            Controls.Add(label1);
            Controls.Add(roundedButton1);
            Controls.Add(linkLabel5);
            Controls.Add(linkLabel4);
            Controls.Add(linkLabel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MenuBuatTournament";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuBuatTournament";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RoundedButton roundedButton1;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel1;
        private Label label1;
        private TextBox NamaTournament;
        private Label label2;
        private ComboBox Babak;
        private ComboBox TipeGame;
        private Label label3;
        private Label label4;
        private TextBox HargaPendaftaran;
        private TextBox Hadiah;
        private Label label5;
        private Label label6;
        private DateTimePicker TanggalPelaksanaan;
        private Label label7;
        private ComboBox TimeControl;
        private RoundedButton roundedButton2;
        private LinkLabel linkLabel2;
    }
}