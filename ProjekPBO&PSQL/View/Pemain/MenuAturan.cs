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
    public partial class MenuAturan : Form
    {
        // Variabel global untuk menyimpan sesi data user yang sedang login
        private User userLogin;

        // Konstruktor diubah agar menerima data User dari form sebelumnya
        public MenuAturan(User user)
        {
            InitializeComponent();
            this.userLogin = user; // Menyimpan sesi user aktif (seperti Bangijal)
        }

        private void MenuAturan_Load(object sender, EventArgs e)
        {
            // Tempat logika load aturan jika teks dibaca dari database / file tekstual
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

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Cari Pemain
        {
            MenuCariPemain cariForm = new MenuCariPemain(this.userLogin);
            cariForm.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // History Pertandingan
        {
            MenuHistoryPermainan historyForm = new MenuHistoryPermainan(this.userLogin);
            historyForm.Show();
            this.Hide();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Baca Peraturan (Form Ini)
        {
            MessageBox.Show("Kamu sudah berada di halaman Baca Peraturan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Jika linkLabel7 digunakan untuk navigasi lain (misal kembali ke Menu Utama Pemain)
            MenuPemain utamaForm = new MenuPemain(this.userLogin);
            utamaForm.Show();
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
                this.Close(); // Menutup form Aturan dengan aman
            }
        }
    }
}