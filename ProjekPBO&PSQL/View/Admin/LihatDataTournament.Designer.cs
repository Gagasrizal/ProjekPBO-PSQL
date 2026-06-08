namespace ProjekPBO_PSQL.View.Admin
{
    partial class LihatDataTournament
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LihatDataTournament));
            roundedButton1 = new RoundedButton();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            dataGridView1 = new DataGridView();
            dBHelperBindingSource = new BindingSource(components);
            roundedButton2 = new RoundedButton();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dBHelperBindingSource).BeginInit();
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.BackgroundColor = Color.FromArgb(58, 74, 83);
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(162, 100);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(626, 335);
            dataGridView1.TabIndex = 28;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // dBHelperBindingSource
            // 
            dBHelperBindingSource.DataSource = typeof(Helpers.DBHelper);
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
            roundedButton2.Location = new Point(688, 66);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(100, 28);
            roundedButton2.TabIndex = 29;
            roundedButton2.Text = "Buat Tour";
            roundedButton2.TextColor = Color.Silver;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.ForeColor = Color.DimGray;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(162, 66);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(210, 28);
            comboBox1.TabIndex = 30;
            comboBox1.Text = "Pilih Tournament";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // LihatDataTournament
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBox1);
            Controls.Add(roundedButton2);
            Controls.Add(dataGridView1);
            Controls.Add(roundedButton1);
            Controls.Add(linkLabel5);
            Controls.Add(linkLabel4);
            Controls.Add(linkLabel1);
            Name = "LihatDataTournament";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LihatDataTournament";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dBHelperBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RoundedButton roundedButton1;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel1;
        private DataGridView dataGridView1;
        private BindingSource dBHelperBindingSource;
        private RoundedButton roundedButton2;
        private ComboBox comboBox1;
    }
}