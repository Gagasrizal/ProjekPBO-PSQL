using Npgsql;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.View.Pemain;
using ProjekPBO_PSQL.View.Admin;
using ProjekPBO_PSQL.Models; // Tambahkan ini agar program mengenali objek 'User'
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjekPBO_PSQL
{
    public partial class FormLogin : Form
    {
        // 1. Buat instance DBHelper agar bisa memanggil fungsi query database
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

        // CATATAN: Pastikan nama textBox1 ini adalah TextBox untuk USERNAME kamu di Design.
        // Jika di design namanya berbeda, sesuaikan variabel di bawah.
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // CATATAN: Jika kamu punya textBox2 untuk PASSWORD, pastikan namanya sesuai.
        // Di kode bawaanmu belum muncul event TextChanged untuk password, tidak apa-apa, 
        // kita bisa langsung panggil nama komponen TextBox-nya di dalam tombol Confirm.

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

        // =======================================================================
        // TOMBOL CONFIRM (LOGIN)
        // =======================================================================
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
                Admin user = dbHelper.AuthenticateUser(username, password);
                if (user == null)
                {
                    MessageBox.Show("Username atau password salah!");
                    return;
                }

                // Berhasil login
                MessageBox.Show($"Selamat datang, {user.username}!");

                if (user.isAdmin)
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
        // =======================================================================
        // TOMBOL SIGN UP (PINDAH HALAMAN)
        // =======================================================================
        private void roundedButton1_Click_1(object sender, EventArgs e)
        {
            FormRegistrasi registr = new FormRegistrasi();
            registr.Show();
            this.Hide();
        }
    }
}