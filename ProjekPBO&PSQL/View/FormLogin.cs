using Npgsql;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.View.Pemain;
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
            // Ambil input dari TextBox di Form. 
            // PENTING: textBox1 untuk username, textBox2 untuk password (sesuaikan dengan nama komponenmu)
            string usernameInput = textBox1.Text.Trim();
            string passwordInput = textBox2.Text.Trim();

            // Validasi jika ada input yang masih kosong
            if (string.IsNullOrEmpty(usernameInput) || string.IsNullOrEmpty(passwordInput))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Panggil fungsi AuthenticateUser dari DBHelper.cs
                User userTerlogin = dbHelper.AuthenticateUser(usernameInput, passwordInput);

                if (userTerlogin != null)
                {
                    MessageBox.Show($"Selamat datang kembali, {userTerlogin.username}!", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // SEKARANG SUDAH FIX: menggunakan isAdmin sesuai dengan isi User.cs kamu
                    if (userTerlogin.isAdmin)
                    {
                        // Jika admin, arahkan ke Form Admin kamu
                        MessageBox.Show("Anda masuk sebagai Admin.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // MenuAdmin adminForm = new MenuAdmin();
                        // adminForm.Show();
                    }
                    else
                    {
                        // Jika pemain, arahkan ke MenuPemain.cs dengan MELEMPAR data userTerlogin
                        MenuPemain pemainForm = new MenuPemain(userTerlogin); // <--- TAMBAHKAN userTerlogin DI SINI
                        pemainForm.Show();
                    }

                    this.Hide(); // Sembunyikan FormLogin setelah berhasil masuk
                }
                else
                {
                    // Jika data tidak ditemukan di database atau password salah
                    MessageBox.Show("Username atau Password salah! Silakan coba lagi.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Menangkap error jika ada kendala pada koneksi PostgreSQL atau query
                MessageBox.Show($"Terjadi kesalahan koneksi database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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