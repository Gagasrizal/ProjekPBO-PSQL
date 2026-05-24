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
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            button1 = new Button();
            linkLabel1 = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(338, 52);
            label1.Name = "label1";
            label1.Size = new Size(122, 28);
            label1.TabIndex = 0;
            label1.Text = "HyperChess";
            label1.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(246, 170);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(296, 27);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(108, 170);
            label2.Name = "label2";
            label2.Size = new Size(106, 28);
            label2.TabIndex = 2;
            label2.Text = "Username";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(108, 229);
            label3.Name = "label3";
            label3.Size = new Size(101, 28);
            label3.TabIndex = 3;
            label3.Text = "Password";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(246, 233);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(296, 27);
            textBox2.TabIndex = 4;
            // 
            // button1
            // 
            button1.BackColor = Color.Lime;
            button1.Location = new Point(648, 385);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 5;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = false;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(267, 389);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(244, 20);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Didn't Have Account? Sign Up Here";
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(linkLabel1);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "FormLogin";
            Text = "Form1";
            Load += FormLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private Button button1;
        private LinkLabel linkLabel1;
    }
}
