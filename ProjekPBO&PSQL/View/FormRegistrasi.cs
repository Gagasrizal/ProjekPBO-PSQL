using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Models.Context;
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
        private AutentikasiContext dbHelper = new AutentikasiContext();

        public FormRegistrasi()
        {
            InitializeComponent();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            // 1. Ambil semua data dari input form
            string namaInput = textBox1.Text.Trim();
            string usernameInput = textBox2.Text.Trim();
            string emailInput = textBox3.Text.Trim();
            string negaraInput = comboBox1.Text.Trim();
            string telponInput = textBox5.Text.Trim();
            string passwordInput = textBox6.Text.Trim();
            DateTime tanggalLahirInput = dateTimePicker1.Value;

            // 2. Validasi input kosong
            if (string.IsNullOrEmpty(namaInput) || string.IsNullOrEmpty(usernameInput) ||
                string.IsNullOrEmpty(emailInput) || string.IsNullOrEmpty(negaraInput) ||
                string.IsNullOrEmpty(telponInput) || string.IsNullOrEmpty(passwordInput))
            {
                MessageBox.Show("Semua kolom data wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 3. Validasi duplikasi data ke database
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

                Pemain akunBaru = new Pemain(0, usernameInput, passwordInput, emailInput);

                ProfilCatur detailBaru = new ProfilCatur()
                {
                    NamaLengkap = namaInput,
                    Negara = negaraInput,
                    NoTelepon = telponInput,
                    TanggalLahir = tanggalLahirInput.Date,
                    EloRating = 1200, // Rating awal standar pemain baru
                    Deskripsi = ""
                };

                bool isSukses = dbHelper.RegisterUser(akunBaru, passwordInput, detailBaru);

                if (isSukses)
                {
                    MessageBox.Show("Registrasi berhasil! Silakan login.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Pindah ke Form Login
                    FormLogin loginForm = new FormLogin();
                    loginForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Registrasi gagal. Cek kembali koneksi database Anda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void FormRegistrasi_Load(object sender, EventArgs e) { }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}