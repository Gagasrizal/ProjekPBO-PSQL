namespace ProjekPBO_PSQL
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (username == "admin" && password == "123")
            {
                MessageBox.Show("Login berhasil!");

                MenuAdmin adminForm = new MenuAdmin();
                adminForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login gagal!");
            }
        }

        private void roundedButton1_Click_1(object sender, EventArgs e)
        {
            FormRegistrasi registr= new FormRegistrasi();
            registr.Show();
            this.Hide();
        }
    }
}
