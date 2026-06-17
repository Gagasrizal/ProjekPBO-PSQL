using Npgsql;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.View.Pemain;
using ProjekPBO_PSQL.View.Admin;
using ProjekPBO_PSQL.Models; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models.Context;

namespace ProjekPBO_PSQL
{
    public partial class FormLogin : Form
    {
        private DBHelper dbHelper = new DBHelper();

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
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username dan password wajib diisi!");
                return;
            }

            try
            {
                AutentikasiContext autentikasiContext = new AutentikasiContext();
                AkunUser user = autentikasiContext.AuthenticateUser(username, password);
                if (user == null)
                {
                    MessageBox.Show("Username atau password salah!");
                    return;
                }

                // Berhasil login
                MessageBox.Show($"Selamat datang, {user.Username}!");

                if (user.IsAdmin)
                {
                    MenuAdmin adminForm = new MenuAdmin(user);
                    adminForm.Show();
                }
                else
                {
                    MenuPemain pemainForm = new MenuPemain(user);
                    pemainForm.Show();
                }

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
        }

        private void roundedButton1_Click_1(object sender, EventArgs e)
        {
            FormRegistrasi registr = new FormRegistrasi();
            registr.Show();
            this.Hide();
        }
    }
}