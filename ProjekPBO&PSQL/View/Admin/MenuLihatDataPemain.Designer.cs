namespace ProjekPBO_PSQL.View.Admin
{
    partial class MenuLihatDataPemain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuLihatDataPemain));
            roundedButton3 = new RoundedButton();
            roundedButton2 = new RoundedButton();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            roundedButton1 = new RoundedButton();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
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
            roundedButton3.Location = new Point(655, 52);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(131, 28);
            roundedButton3.TabIndex = 41;
            roundedButton3.Text = "Matchmaking";
            roundedButton3.TextColor = Color.Silver;
            roundedButton3.UseVisualStyleBackColor = false;
            roundedButton3.Click += roundedButton3_Click;
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
            roundedButton2.Location = new Point(686, 419);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(100, 28);
            roundedButton2.TabIndex = 40;
            roundedButton2.Text = "Hasil";
            roundedButton2.TextColor = Color.Silver;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.ForeColor = Color.DimGray;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Babak 1", "Babak 2", "Babak 3", "Babak 4", "Babak 5", "Babak 6", "Babak 7" });
            comboBox2.Location = new Point(399, 53);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(210, 28);
            comboBox2.TabIndex = 39;
            comboBox2.Text = "Pilih Babak";
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.ForeColor = Color.DimGray;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(163, 53);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(210, 28);
            comboBox1.TabIndex = 38;
            comboBox1.Text = "Pilih Tournament";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.FromArgb(58, 74, 83);
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(163, 100);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(623, 313);
            dataGridView1.TabIndex = 37;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
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
            roundedButton1.Location = new Point(9, 400);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(125, 35);
            roundedButton1.TabIndex = 36;
            roundedButton1.Text = "LogOut";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel5.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel5.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel5.LinkColor = Color.Silver;
            linkLabel5.Location = new Point(9, 160);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(122, 42);
            linkLabel5.TabIndex = 35;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "Lihat Data\r\nPembayaran";
            linkLabel5.LinkClicked += linkLabel5_LinkClicked;
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel4.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel4.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel4.LinkColor = Color.Silver;
            linkLabel4.Location = new Point(9, 100);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(117, 42);
            linkLabel4.TabIndex = 34;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "Lihat Data\r\nTournament";
            linkLabel4.LinkClicked += linkLabel4_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel1.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel1.LinkColor = Color.Silver;
            linkLabel1.Location = new Point(9, 60);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(57, 21);
            linkLabel1.TabIndex = 33;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Profil";
            linkLabel1.TextAlign = ContentAlignment.TopRight;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // MenuLihatDataPemain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(roundedButton3);
            Controls.Add(roundedButton2);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(dataGridView1);
            Controls.Add(roundedButton1);
            Controls.Add(linkLabel5);
            Controls.Add(linkLabel4);
            Controls.Add(linkLabel1);
            Name = "MenuLihatDataPemain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuLihatDataPemain";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RoundedButton roundedButton3;
        private RoundedButton roundedButton2;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private RoundedButton roundedButton1;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel1;
    }
}