using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using ProjekPBO_PSQL.Helpers;

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
            string username = textBox1.Text.Trim();
            string password = textBox2.Text; // production: gunakan hashing

            try
            {
                var dt = DbHelper.ExecuteQuery(
                    "SELECT role, nama FROM users WHERE username = @u AND password = @p LIMIT 1",
                    new NpgsqlParameter("@u", username),
                    new NpgsqlParameter("@p", password)
                );

                if (dt.Rows.Count > 0)
                {
                    string role = dt.Rows[0]["role"].ToString();
                    string nama = dt.Rows[0]["nama"].ToString();

                    if (role == "admin")
                    {
                        MessageBox.Show($"Login berhasil! Selamat datang, {nama} (Admin).");
                        MenuAdmin adminForm = new MenuAdmin();
                        adminForm.Show();
                        this.Hide();
                    }
                    else if (role == "pemain")
                    {
                        MessageBox.Show($"Login berhasil! Selamat datang, {nama}.");
                        MenuPemain pemainForm = new MenuPemain();
                        pemainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login berhasil, tapi role tidak dikenali.");
                    }
                }
                else
                {
                    MessageBox.Show("Login gagal! Periksa username atau password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal koneksi ke database: " + ex.Message);
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
