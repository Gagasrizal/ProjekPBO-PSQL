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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuLihatDataPemain));
            roundedButton1 = new RoundedButton();
            linkLabel5 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            dataGridView1 = new DataGridView();
            dBHelperBindingSource = new BindingSource(components);
            comboBox1 = new ComboBox();
            linkLabel2 = new LinkLabel();
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
            roundedButton1.Location = new Point(9, 800);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(260, 48);
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
            linkLabel5.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel5.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel5.LinkColor = Color.Silver;
            linkLabel5.Location = new Point(11, 280);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(200, 68);
            linkLabel5.TabIndex = 35;
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
            linkLabel4.TabIndex = 34;
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
            linkLabel1.TabIndex = 33;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Profil";
            linkLabel1.TextAlign = ContentAlignment.TopRight;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.FromArgb(58, 74, 83);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Enabled = false;
            dataGridView1.Location = new Point(296, 180);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1286, 668);
            dataGridView1.TabIndex = 44;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // dBHelperBindingSource
            // 
            dBHelperBindingSource.DataSource = typeof(Helpers.DBHelper);
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.ForeColor = Color.DimGray;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(296, 100);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(358, 42);
            comboBox1.TabIndex = 38;
            comboBox1.Text = "Pilih Tournament";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
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
            linkLabel2.TabIndex = 45;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Lihat Data\r\npertandingan";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // MenuLihatDataPemain
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1594, 860);
            Controls.Add(linkLabel2);
            Controls.Add(dataGridView1);
            Controls.Add(comboBox1);
            Controls.Add(roundedButton1);
            Controls.Add(linkLabel5);
            Controls.Add(linkLabel4);
            Controls.Add(linkLabel1);
            Name = "MenuLihatDataPemain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuLihatDataPemain";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dBHelperBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RoundedButton roundedButton2;
        private RoundedButton roundedButton1;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel1;
        private RoundedButton roundedButton4;
        private DataGridView dataGridView1;
        private BindingSource dBHelperBindingSource;
        private ComboBox comboBox1;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private LinkLabel linkLabel2;
    }
}