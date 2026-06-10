namespace ProjekPBO_PSQL.View.Pemain
{
    partial class MenuPembayaran
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPembayaran));
            label1 = new Label();
            label8 = new Label();
            label2 = new Label();
            label3 = new Label();
            MetodePembayaran = new ComboBox();
            textBox1 = new TextBox();
            label6 = new Label();
            label7 = new Label();
            linkLabel7 = new LinkLabel();
            linkLabel8 = new LinkLabel();
            linkLabel10 = new LinkLabel();
            linkLabel11 = new LinkLabel();
            roundedButton1 = new RoundedButton();
            Edit = new RoundedButton();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Rounded MT Bold", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(360, 110);
            label1.Name = "label1";
            label1.Size = new Size(218, 39);
            label1.TabIndex = 55;
            label1.Text = "UFC CHESS";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(411, 429);
            label8.Name = "label8";
            label8.Size = new Size(148, 21);
            label8.TabIndex = 64;
            label8.Text = "20 januari 2026";
            label8.Click += label8_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(439, 167);
            label2.Name = "label2";
            label2.Size = new Size(61, 21);
            label2.TabIndex = 56;
            label2.Text = "Rapid";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(254, 209);
            label3.Name = "label3";
            label3.Size = new Size(246, 21);
            label3.TabIndex = 57;
            label3.Text = "Pilih Metode Pembayaran :";
            // 
            // MetodePembayaran
            // 
            MetodePembayaran.FormattingEnabled = true;
            MetodePembayaran.Items.AddRange(new object[] { "Bank BCA", "", "Bank Mandiri", "Bank BNI", "Bank BRI", "Bank Syariah Indonesia" });
            MetodePembayaran.Location = new Point(254, 233);
            MetodePembayaran.Name = "MetodePembayaran";
            MetodePembayaran.Size = new Size(384, 28);
            MetodePembayaran.TabIndex = 60;
            MetodePembayaran.SelectedIndexChanged += MetodePembayaran_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(254, 300);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(384, 27);
            textBox1.TabIndex = 61;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(254, 276);
            label6.Name = "label6";
            label6.Size = new Size(188, 21);
            label6.TabIndex = 62;
            label6.Text = "Masukkan Nominal :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(159, 410);
            label7.Name = "label7";
            label7.Size = new Size(341, 40);
            label7.TabIndex = 63;
            label7.Text = "Pendaftaran di tutup Sebelum Kompetisi\r\nDilaksanakan. Pada Tanggal :";
            // 
            // linkLabel7
            // 
            linkLabel7.AutoSize = true;
            linkLabel7.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel7.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel7.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel7.LinkColor = Color.Silver;
            linkLabel7.Location = new Point(13, 225);
            linkLabel7.Name = "linkLabel7";
            linkLabel7.Size = new Size(99, 42);
            linkLabel7.TabIndex = 134;
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
            linkLabel8.Location = new Point(9, 155);
            linkLabel8.Name = "linkLabel8";
            linkLabel8.Size = new Size(129, 42);
            linkLabel8.TabIndex = 133;
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
            linkLabel10.TabIndex = 131;
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
            linkLabel11.TabIndex = 130;
            linkLabel11.TabStop = true;
            linkLabel11.Text = "Profil";
            linkLabel11.TextAlign = ContentAlignment.TopRight;
            linkLabel11.LinkClicked += linkLabel11_LinkClicked;
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
            roundedButton1.TabIndex = 135;
            roundedButton1.Text = "LogOut";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // Edit
            // 
            Edit.BackColor = Color.LimeGreen;
            Edit.BackgroundColor = Color.LimeGreen;
            Edit.BorderColor = Color.PaleVioletRed;
            Edit.BorderRadius = 8;
            Edit.FlatAppearance.BorderSize = 0;
            Edit.FlatStyle = FlatStyle.Flat;
            Edit.ForeColor = Color.White;
            Edit.Location = new Point(668, 399);
            Edit.Name = "Edit";
            Edit.Size = new Size(102, 36);
            Edit.TabIndex = 199;
            Edit.Text = "Bayar";
            Edit.TextColor = Color.White;
            Edit.UseVisualStyleBackColor = false;
            Edit.Click += Edit_Click;
            // 
            // MenuPembayaran
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(Edit);
            Controls.Add(roundedButton1);
            Controls.Add(linkLabel7);
            Controls.Add(linkLabel8);
            Controls.Add(linkLabel10);
            Controls.Add(linkLabel11);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(textBox1);
            Controls.Add(MetodePembayaran);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MenuPembayaran";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuPembayaran";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label8;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox MetodePembayaran;
        private TextBox textBox1;
        private Label label6;
        private Label label7;
        private LinkLabel linkLabel7;
        private LinkLabel linkLabel8;
        private LinkLabel linkLabel10;
        private LinkLabel linkLabel11;
        private RoundedButton roundedButton1;
        private RoundedButton Edit;
    }
}