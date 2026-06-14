using ProjekPBO_PSQL.View;

namespace ProjekPBO_PSQL
{
    partial class FormRegistrasi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistrasi));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label8 = new Label();
            label9 = new Label();
            dateTimePicker1 = new DateTimePicker();
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(611, 100);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(97, 26);
            label1.TabIndex = 1;
            label1.Text = "Sign Up";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(355, 240);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(101, 25);
            label2.TabIndex = 2;
            label2.Text = "Username";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(355, 320);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(86, 25);
            label3.TabIndex = 3;
            label3.Text = "Country";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(355, 280);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(59, 25);
            label4.TabIndex = 4;
            label4.Text = "Email";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label5.ForeColor = Color.White;
            label5.Location = new Point(355, 360);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(81, 25);
            label5.TabIndex = 5;
            label5.Text = "Contact";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label6.ForeColor = Color.White;
            label6.Location = new Point(355, 400);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(97, 25);
            label6.TabIndex = 6;
            label6.Text = "Password";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(355, 200);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(101, 25);
            label7.TabIndex = 7;
            label7.Text = "Full Name";
            label7.Click += label7_Click;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(494, 200);
            textBox1.Margin = new Padding(2, 2, 2, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(417, 29);
            textBox1.TabIndex = 8;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(494, 240);
            textBox2.Margin = new Padding(2, 2, 2, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(417, 29);
            textBox2.TabIndex = 9;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(494, 280);
            textBox3.Margin = new Padding(2, 2, 2, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(417, 29);
            textBox3.TabIndex = 10;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // textBox5
            // 
            textBox5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox5.Location = new Point(494, 360);
            textBox5.Margin = new Padding(2, 2, 2, 2);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(417, 29);
            textBox5.TabIndex = 12;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // textBox6
            // 
            textBox6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox6.Location = new Point(494, 400);
            textBox6.Margin = new Padding(2, 2, 2, 2);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(417, 29);
            textBox6.TabIndex = 13;
            textBox6.TextChanged += textBox6_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(581, 61);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(161, 28);
            label8.TabIndex = 17;
            label8.Text = "Hyper Chess";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label9.ForeColor = Color.White;
            label9.Location = new Point(355, 440);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(102, 25);
            label9.TabIndex = 21;
            label9.Text = "Birth Date";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.Location = new Point(494, 440);
            dateTimePicker1.Margin = new Padding(2, 2, 2, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 29);
            dateTimePicker1.TabIndex = 22;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Afganistan", "Afrika Selatan", "Afrika Tengah", "Albania", "Aljazair", "Amerika Serikat", "Andorra", "Angola", "Antigua dan Barbuda", "Arab Saudi", "Argentina", "Armenia", "Australia", "Austria", "Azerbaijan", "Bahama", "Bahrain", "Bangladesh", "Barbados", "Belanda", "Belarus", "Belgia", "Belize", "Benin", "Bhutan", "Bolivia", "Bosnia dan Herzegovina", "Botswana", "Brasil", "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Ceko", "Chad", "Chili", "China", "Ciprus", "Denmark", "Djibouti", "Dominika", "Ekuador", "El Salvador", "Eritrea", "Estonia", "Eswatini", "Etiopia", "Fiji", "Filipina", "Finlandia", "Gabon", "Gambia", "Georgia", "Ghana", "Grenada", "Guatemala", "Guinea", "Guinea-Bissau", "Guinea Khatulistiwa", "Guyana", "Haiti", "Honduras", "Hongaria", "India", "Indonesia", "Irak", "Iran", "Irlandia", "Islandia", "Israel", "Italia", "Jamaika", "Jepang", "Jerman", "Jordania", "Kamboja", "Kamerun", "Kanada", "Kazakhstan", "Kenya", "Kepulauan Marshall", "Kepulauan Solomon", "Kirgizstan", "Kiribati", "Kolombia", "Komoro", "Kongo", "Korea Utara", "Korea Selatan", "Kosta Rika", "Kroasia", "Kuba", "Kuwait", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libia", "Liechtenstein", "Lituania", "Luksemburg", "Madagaskar", "Maladewa", "Malawi", "Malaysia", "Mali", "Malta", "Maroko", "Mauritania", "Mauritius", "Meksiko", "Mikronesia", "Moldova", "Monako", "Mongolia", "Montenegro", "Mozambik", "Myanmar", "Namibia", "Nauru", "Nepal", "Niger", "Nigeria", "Nikaragua", "Norwegia", "Oman", "Pakistan", "Palau", "Panama", "Pantai Gading", "Papua Nugini", "Paraguay", "Prancis", "Peru", "Polandia", "Portugal", "Qatar", "Rumania", "Rusia", "Rwanda", "Saint Kitts dan Nevis", "Saint Lucia", "Saint Vincent dan Grenadines", "Samoa", "San Marino", "Sao Tome dan Principe", "Selandia Baru", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapura", "Siprus", "Slovakia", "Slovenia", "Somalia", "Spanyol", "Sri Lanka", "Sudan", "Sudan Selatan", "Suriname", "Suriah", "Swedia", "Swiss", "Tajikistan", "Tanjung Verde", "Tanzania", "Thailand", "Timor Leste", "Togo", "Tonga", "Trinidad dan Tobago", "Tunisia", "Turki", "Turkmenistan", "Tuvalu", "Uganda", "Ukraina", "Uni Emirat Arab", "Britania Raya (Inggris)", "Uruguay", "Uzbekistan", "Vanuatu", "Vatikan", "Venezuela", "Vietnam", "Yaman", "Yunani", "Zambia", "Zimbabwe" });
            comboBox1.Location = new Point(494, 320);
            comboBox1.Margin = new Padding(2, 2, 2, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(417, 29);
            comboBox1.TabIndex = 23;
            // 
            // FormRegistrasi
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1096, 599);
            Controls.Add(comboBox1);
            Controls.Add(dateTimePicker1);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = SystemColors.AppWorkspace;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2, 2, 2, 2);
            MaximizeBox = false;
            Name = "FormRegistrasi";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormRegistrasi";
            Load += FormRegistrasi_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox5;
        private TextBox textBox6;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label8;
        private RoundedButton roundedButton1;
        private RoundedButton roundedButton2;
        private Label label9;
        private DateTimePicker dateTimePicker1;
        private ComboBox comboBox1;
    }
}