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
            roundedButton1 = new RoundedButton();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            dataGridView1 = new DataGridView();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            roundedButton1.Location = new Point(11, 400);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(125, 35);
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
            linkLabel5.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel5.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel5.LinkColor = Color.Silver;
            linkLabel5.Location = new Point(11, 160);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(122, 42);
            linkLabel5.TabIndex = 26;
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
            linkLabel4.Location = new Point(11, 100);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(117, 42);
            linkLabel4.TabIndex = 25;
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
            linkLabel1.Location = new Point(11, 60);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(57, 21);
            linkLabel1.TabIndex = 22;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Profil";
            linkLabel1.TextAlign = ContentAlignment.TopRight;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(165, 100);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(623, 339);
            dataGridView1.TabIndex = 28;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.ForeColor = Color.DimGray;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(165, 53);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(210, 28);
            comboBox1.TabIndex = 29;
            comboBox1.Text = "Pilih Tournament";
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.ForeColor = Color.DimGray;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Babak 1", "Babak 2", "Babak 3", "Babak 4", "Babak 5", "Babak 6", "Babak 7" });
            comboBox2.Location = new Point(401, 53);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(210, 28);
            comboBox2.TabIndex = 30;
            comboBox2.Text = "Pilih Babak";
            // 
            // MenuLihatDataPemain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 451);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(dataGridView1);
            Controls.Add(roundedButton1);
            Controls.Add(linkLabel5);
            Controls.Add(linkLabel4);
            Controls.Add(linkLabel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MenuLihatDataPemain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuLihatDataPemain";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RoundedButton roundedButton1;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel1;
        private DataGridView dataGridView1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
    }
}