using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models; // Memastikan objek 'User' dikenali
using ProjekPBO_PSQL.View.Pemain; // Agar bisa mengenali MenuProfilPem jika berada di folder berbeda

namespace ProjekPBO_PSQL
{
    public partial class MenuTournament : Form
    {
        // Variabel global untuk menyimpan sesi data user yang sedang login
        private User userLogin;

        // Konstruktor diubah agar menerima data User yang dikirim dari form sebelumnya
        public MenuTournament(User user)
        {
            InitializeComponent(); // Ini dijamin aman dan tidak error lagi!
            this.userLogin = user;  // Menyimpan sesi user aktif (seperti Bangijal)
        }

        private void MenuTournament_Load(object sender, EventArgs e)
        {
            // Tempat untuk menampilkan list tournament dari database nantinya
        }

        // =======================================================================
        // NAVIGASI LINK LABEL SIKLUS MENU PEMAIN (ESTAFET USER LOGIN)
        // =======================================================================

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Menu Profil
        {
            // Mengoper kembali data userLogin ke MenuProfilPem agar data profil muncul sempurna
            MenuProfilPem profilForm = new MenuProfilPem(this.userLogin);
            profilForm.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // List Tournament (Form Ini)
        {
            MessageBox.Show("Kamu sudah berada di halaman List Tournament.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Baca Peraturan
        {
            MenuAturan peraturanForm = new MenuAturan(this.userLogin);
            peraturanForm.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Event klik isi tabel jika diperlukan nanti
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
                this.Close(); // Menutup form Tournament dengan aman
            }
        }
    }
}