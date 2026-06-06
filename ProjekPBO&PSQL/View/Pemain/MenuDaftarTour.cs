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
    public partial class MenuDaftarTour : Form
    {
        // Variabel global untuk menyimpan sesi data user yang sedang login
        private User userLogin;

        // Konstruktor diubah agar menerima data User yang dikirim dari form sebelumnya
        public MenuDaftarTour(User user)
        {
            InitializeComponent();
            this.userLogin = user; // Menyimpan sesi user aktif (seperti Bangijal)
        }

        private void MenuDaftarTour_Load(object sender, EventArgs e)
        {
            // Tempat logika load data ketika form dibuka (jika ada)
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Event klik isi tabel pendaftaran tournament
        }

        // =======================================================================
        // NAVIGASI LINK LABEL SIKLUS MENU PEMAIN (ESTAFET USER LOGIN)
        // =======================================================================

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Menu Profil
        {
            // Mengoper kembali data userLogin ke MenuProfilPem agar profil menampilkan data yang benar
            MenuProfilPem profilForm = new MenuProfilPem(this.userLogin);
            profilForm.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // List Tournament
        {
            // Mengoper data userLogin ke MenuTournament
            MenuTournament tournamentForm = new MenuTournament(this.userLogin);
            tournamentForm.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Daftar Tournament (Form Ini)
        {
            MessageBox.Show("Kamu sudah berada di halaman Daftar Tournament.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Cari Pemain
        {
            // Mengoper data userLogin agar sesi tidak terputus
            MenuCariPemain cariForm = new MenuCariPemain(this.userLogin);
            cariForm.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // History Pertandingan
        {
            // Mengoper data userLogin agar sesi tidak terputus
            MenuHistoryPermainan historyForm = new MenuHistoryPermainan(this.userLogin);
            historyForm.Show();
            this.Hide();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Baca Peraturan
        {
            // Mengoper data userLogin agar sesi tidak terputus
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
                this.Close(); // Menutup form Daftar Tournament dan kembali ke halaman login awal
            }
        }
    }
}