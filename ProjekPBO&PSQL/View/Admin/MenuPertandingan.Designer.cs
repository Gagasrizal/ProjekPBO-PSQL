namespace ProjekPBO_PSQL.View.Admin
{
    partial class MenuPertandingan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPertandingan));
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            roundedButton1 = new RoundedButton();
            dataGridView1 = new DataGridView();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            roundedButton2 = new RoundedButton();
            roundedButton3 = new RoundedButton();
            comboBox1 = new ComboBox();
            roundedButton4 = new RoundedButton();
            linkLabel2 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
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
            linkLabel5.TabIndex = 38;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "Lihat Data\r\nPembayaran";           
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
            linkLabel4.TabIndex = 37;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "Lihat Data\r\nTournament";
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
            linkLabel1.TabIndex = 36;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Profil";
            linkLabel1.TextAlign = ContentAlignment.TopRight;
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
            roundedButton1.TabIndex = 39;
            roundedButton1.Text = "LogOut";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(296, 137);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1286, 658);
            dataGridView1.TabIndex = 40;
            // 
            // comboBox3
            // 
            comboBox3.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox3.ForeColor = Color.DimGray;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(296, 800);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(260, 42);
            comboBox3.TabIndex = 43;
            comboBox3.Text = "Input Point";
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.ForeColor = Color.DimGray;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "" });
            comboBox2.Location = new Point(562, 799);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(320, 42);
            comboBox2.TabIndex = 44;
            comboBox2.Text = "Pilih Babak";
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.Transparent;
            roundedButton2.BackgroundColor = Color.Transparent;
            roundedButton2.BackgroundImageLayout = ImageLayout.Stretch;
            roundedButton2.BorderColor = Color.PaleGreen;
            roundedButton2.BorderRadius = 0;
            roundedButton2.BorderSize = 2;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton2.ForeColor = Color.Silver;
            roundedButton2.Location = new Point(1322, 801);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(260, 48);
            roundedButton2.TabIndex = 45;
            roundedButton2.Text = "Hasil";
            roundedButton2.TextColor = Color.Silver;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // roundedButton3
            // 
            roundedButton3.BackColor = Color.Transparent;
            roundedButton3.BackgroundColor = Color.Transparent;
            roundedButton3.BackgroundImageLayout = ImageLayout.Stretch;
            roundedButton3.BorderColor = Color.PaleGreen;
            roundedButton3.BorderRadius = 0;
            roundedButton3.BorderSize = 2;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton3.ForeColor = Color.Silver;
            roundedButton3.Location = new Point(1322, 83);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(260, 48);
            roundedButton3.TabIndex = 46;
            roundedButton3.Text = "Matchmaking";
            roundedButton3.TextColor = Color.Silver;
            roundedButton3.UseVisualStyleBackColor = false;
            roundedButton3.Click += roundedButton3_Click;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.ForeColor = Color.DimGray;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(296, 82);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(358, 42);
            comboBox1.TabIndex = 47;
            comboBox1.Text = "Pilih Tournament";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = Color.Transparent;
            roundedButton4.BackgroundColor = Color.Transparent;
            roundedButton4.BackgroundImageLayout = ImageLayout.Stretch;
            roundedButton4.BorderColor = Color.PaleGreen;
            roundedButton4.BorderRadius = 0;
            roundedButton4.BorderSize = 2;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton4.ForeColor = Color.Silver;
            roundedButton4.Location = new Point(681, 82);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(260, 48);
            roundedButton4.TabIndex = 48;
            roundedButton4.Text = "Lihat";
            roundedButton4.TextColor = Color.Silver;
            roundedButton4.UseVisualStyleBackColor = false;
            roundedButton4.Click += roundedButton4_Click;
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
            linkLabel2.Size = new Size(213, 68);
            linkLabel2.TabIndex = 49;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Lihat Data\r\nPertandingan";
            // 
            // MenuPertandingan
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1594, 860);
            Controls.Add(linkLabel2);
            Controls.Add(roundedButton4);
            Controls.Add(comboBox1);
            Controls.Add(roundedButton3);
            Controls.Add(roundedButton2);
            Controls.Add(comboBox2);
            Controls.Add(comboBox3);
            Controls.Add(dataGridView1);
            Controls.Add(roundedButton1);
            Controls.Add(linkLabel5);
            Controls.Add(linkLabel4);
            Controls.Add(linkLabel1);
            Name = "MenuPertandingan";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuPertandingan";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel1;
        private RoundedButton roundedButton1;
        private DataGridView dataGridView1;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private RoundedButton roundedButton2;
        private RoundedButton roundedButton3;
        private ComboBox comboBox1;
        private RoundedButton roundedButton4;
        private LinkLabel linkLabel2;
    }
}