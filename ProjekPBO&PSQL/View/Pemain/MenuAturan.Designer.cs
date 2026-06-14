namespace ProjekPBO_PSQL.View.Pemain
{
    partial class MenuAturan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuAturan));
            roundedButton1 = new RoundedButton();
            linkLabel7 = new LinkLabel();
            linkLabel6 = new LinkLabel();
            linkLabel5 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
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
            roundedButton1.Location = new Point(11, 800);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(260, 48);
            roundedButton1.TabIndex = 30;
            roundedButton1.Text = "LogOut";
            roundedButton1.TextColor = Color.Black;
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // linkLabel7
            // 
            linkLabel7.BackColor = Color.Transparent;
            linkLabel7.Font = new Font("Arial Rounded MT Bold", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel7.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel7.LinkColor = Color.White;
            linkLabel7.Location = new Point(279, 104);
            linkLabel7.Name = "linkLabel7";
            linkLabel7.Size = new Size(1303, 757);
            linkLabel7.TabIndex = 32;
            linkLabel7.TabStop = true;
            linkLabel7.Text = resources.GetString("linkLabel7.Text");
            linkLabel7.LinkClicked += linkLabel7_LinkClicked;
            // 
            // linkLabel6
            // 
            linkLabel6.AutoSize = true;
            linkLabel6.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel6.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel6.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel6.LinkColor = Color.Silver;
            linkLabel6.Location = new Point(11, 380);
            linkLabel6.Name = "linkLabel6";
            linkLabel6.Size = new Size(162, 68);
            linkLabel6.TabIndex = 119;
            linkLabel6.TabStop = true;
            linkLabel6.Text = "Baca\r\nPeraturan";
            linkLabel6.LinkClicked += linkLabel6_LinkClicked_1;
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
            linkLabel5.Size = new Size(213, 68);
            linkLabel5.TabIndex = 118;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "History \r\nPertandingan";
            linkLabel5.LinkClicked += linkLabel5_LinkClicked_1;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.BackColor = Color.FromArgb(38, 48, 54);
            linkLabel2.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel2.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel2.LinkColor = Color.Silver;
            linkLabel2.Location = new Point(11, 180);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(193, 68);
            linkLabel2.TabIndex = 116;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "List\r\nTournament";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked_1;
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
            linkLabel1.TabIndex = 115;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Profil";
            linkLabel1.TextAlign = ContentAlignment.TopRight;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked_1;
            // 
            // MenuAturan
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1594, 860);
            Controls.Add(linkLabel6);
            Controls.Add(linkLabel5);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(linkLabel7);
            Controls.Add(roundedButton1);
            Name = "MenuAturan";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuAturan";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RoundedButton roundedButton1;
        private LinkLabel linkLabel7;
        private LinkLabel linkLabel6;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
    }
}