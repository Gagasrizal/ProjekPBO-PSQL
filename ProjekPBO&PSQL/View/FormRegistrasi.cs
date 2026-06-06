using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjekPBO_PSQL
{
    public partial class FormRegistrasi : Form
    {
        // Instance dbHelper untuk memanggil fungsi database
        private DBHelper dbHelper = new DBHelper();

        public FormRegistrasi()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void FormRegistrasi_Load(object sender, EventArgs e)
        {

        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Hide();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            // Ambil semua input
            string namaInput = textBox1.Text.Trim();
            string usernameInput = textBox2.Text.Trim();
            string emailInput = textBox3.Text.Trim();
            string negaraInput = comboBox1.Text.Trim();   // WAJIB, tidak boleh kosong
            string telponInput = textBox5.Text.Trim();    // WAJIB, tidak boleh kosong
            string passwordInput = textBox6.Text.Trim();
            DateTime tanggalLahirInput = dateTimePicker1.Value;

            // ========= VALIDASI WAJIB ISI (semua kecuali deskripsi) =========
            if (string.IsNullOrEmpty(namaInput))
            {
                MessageBox.Show("Nama lengkap wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(usernameInput))
            {
                MessageBox.Show("Username wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(emailInput))
            {
                MessageBox.Show("Email wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(negaraInput))
            {
                MessageBox.Show("Negara wajib dipilih!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(telponInput))
            {
                MessageBox.Show("Nomor telepon wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(passwordInput))
            {
                MessageBox.Show("Password wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Tanggal lahir: dateTimePicker pasti punya nilai, bisa ditambahkan jika perlu minimal umur
            // Contoh validasi umur minimal 18 tahun (opsional)
            // if (tanggalLahirInput > DateTime.Now.AddYears(-18))
            // {
            //     MessageBox.Show("Usia minimal 18 tahun.");
            //     return;
            // }

            // ========= CEK DUPLIKASI DI DATABASE =========
            try
            {
                if (dbHelper.IsUsernameExists(usernameInput))
                {
                    MessageBox.Show("Username sudah terpakai!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dbHelper.IsEmailExists(emailInput))
                {
                    MessageBox.Show("Email sudah terdaftar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dbHelper.IsNoTeleponExists(telponInput))
                {
                    MessageBox.Show("Nomor telepon sudah digunakan akun lain!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Pengecekan password duplikat (opsional, jika mau)
                // if (dbHelper.IsPasswordExists(passwordInput))
                // {
                //     MessageBox.Show("Password sudah pernah dipakai, pilih yang lain.");
                //     return;
                // }

                // ========= BUNGKUS OBJEK =========
                // ========= BUNGKUS OBJEK =========
                // ========= BUNGKUS OBJEK =========
                // Memakai huruf kecil kembali sesuai model User yang baru diperbaiki
                User akunBaru = new User(0, usernameInput, passwordInput, emailInput, false);

                Detail_User detailBaru = new Detail_User(
                    0,
                    namaInput,
                    negaraInput,
                    telponInput,
                    tanggalLahirInput.Date,
                    1200,
                    DateTime.Today,
                    "" // Menggunakan string kosong "" agar aman dari error NULL di PostgreSQL
                );

                bool isSukses = dbHelper.RegisterUser(akunBaru, detailBaru);

                if (isSukses)
                {
                    MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormLogin loginForm = new FormLogin();
                    loginForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Registrasi gagal. Cek koneksi database atau data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}