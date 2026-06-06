using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models; // Memastikan objek 'User' dikenali

namespace ProjekPBO_PSQL.View.Pemain
{
    public partial class MenuCariPemain : Form
    {
        // Variabel global untuk menyimpan sesi data user yang sedang login
        private User userLogin;

        // Konstruktor menerima data User yang dikirim dari form sebelumnya
        public MenuCariPemain(User user)
        {
            InitializeComponent();
            this.userLogin = user; // Menyimpan sesi user aktif (seperti Bangijal)
        }

        private void MenuCariPemain_Load(object sender, EventArgs e)
        {
            // Logika awal saat halaman pencarian dimuat
        }

        // =======================================================================
        // TOMBOL CARI PEMAIN
        // =======================================================================
        private void roundedButton2_Click(object sender, EventArgs e)
        {
            // Ambil input kata kunci nama dari textBox1
            string keyword = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Silakan masukkan nama pemain yang ingin dicari!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: Tambahkan query database menggunakan DBHelper di sini untuk mencari nama pemain
            // Contoh: dbHelper.SearchPlayerByName(keyword);
            MessageBox.Show($"Mencari pemain dengan nama: {keyword}", "Info Pencarian", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =======================================================================
        // NAVIGASI LINK LABEL SIKLUS MENU PEMAIN (ESTAFET USER LOGIN)
        // =======================================================================

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Menu Profil
        {
            // Mengoper kembali data userLogin ke MenuProfilPem agar profil tetap sinkron
            MenuProfilPem profilForm = new MenuProfilPem(this.userLogin);
            profilForm.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // List Tournament
        {
            MenuTournament tournamentForm = new MenuTournament(this.userLogin);
            tournamentForm.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Daftar Tournament
        {
            MenuDaftarTour daftarForm = new MenuDaftarTour(this.userLogin);
            daftarForm.Show();
            this.Hide();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Cari Pemain (Form Ini)
        {
            MessageBox.Show("Kamu sudah berada di halaman Cari Pemain.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // History Pertandingan
        {
            MenuHistoryPermainan historyForm = new MenuHistoryPermainan(this.userLogin);
            historyForm.Show();
            this.Hide();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Baca Peraturan
        {
            MenuAturan peraturanForm = new MenuAturan(this.userLogin);
            peraturanForm.Show();
            this.Hide();
        }

        // =======================================================================
        // TOMBOL LOGOUT
        // =======================================================================
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah kamu yakin ingin keluar dari Hyper Chess?", "LogOut", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close(); // Menutup form Cari Pemain dengan aman
            }
        }

        // =======================================================================
        // EVENT BAWAAN DESIGNER (JANGAN DIHAPUS)
        // =======================================================================
        private void roundedpanel1_Paint(object sender, PaintEventArgs e) { }
        private void transparentTextBox1_TextChanged(object sender, EventArgs e) { }
        private void transparentTextBox1_TextChanged_1(object sender, EventArgs e) { }
        private void roundedpanel2_Paint(object sender, PaintEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void roundedpanel1_Paint_1(object sender, PaintEventArgs e) { }
    }
}