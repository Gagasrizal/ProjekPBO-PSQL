namespace ProjekPBO_PSQL
{
    partial class FormLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            roundedButton1 = new RoundedButton();
            roundedButton2 = new RoundedButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(306, 22);
            label1.Name = "label1";
            label1.Size = new Size(116, 25);
            label1.TabIndex = 0;
            label1.Text = "HyperChess";
            label1.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(235, 128);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(260, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Transparent;
            label2.Location = new Point(132, 130);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 2;
            label2.Text = "Username";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(132, 154);
            label3.Name = "label3";
            label3.Size = new Size(76, 20);
            label3.TabIndex = 3;
            label3.Text = "Password";
            label3.Click += label3_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(235, 153);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(260, 23);
            textBox2.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(337, 53);
            label4.Name = "label4";
            label4.Size = new Size(53, 16);
            label4.TabIndex = 8;
            label4.Text = "Sign In";
            label4.Click += label4_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(97, 83);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.Green;
            roundedButton1.BackgroundColor = Color.Green;
            roundedButton1.BorderColor = Color.PaleVioletRed;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(228, 280);
            roundedButton1.Margin = new Padding(3, 2, 3, 2);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(280, 28);
            roundedButton1.TabIndex = 10;
            roundedButton1.Text = "Didn't Have Account? Sign Up Here";
            roundedButton1.TextColor = Color.White;
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.LightSlateGray;
            roundedButton2.BackgroundColor = Color.LightSlateGray;
            roundedButton2.BorderColor = Color.PaleVioletRed;
            roundedButton2.BorderRadius = 5;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(571, 280);
            roundedButton2.Margin = new Padding(3, 2, 3, 2);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(115, 28);
            roundedButton2.TabIndex = 20;
            roundedButton2.Text = "Confirm";
            roundedButton2.TextColor = Color.White;
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.HyperChessOI;
            ClientSize = new Size(719, 338);
            Controls.Add(roundedButton2);
            Controls.Add(roundedButton1);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormLogin";
            Text = "zzzzzz";
            Load += FormLogin_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label4;
        private PictureBox pictureBox1;
        private RoundedButton roundedButton1;
        private RoundedButton roundedButton2;
    }
}
