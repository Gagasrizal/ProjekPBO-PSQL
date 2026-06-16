using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models;       // Memastikan objek 'User' dikenali
using ProjekPBO_PSQL.View.Pemain;   // Jika nanti dibutuhkan interaksi antar view

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class MenuAdmin : Form
    {
        // Variabel global untuk menyimpan sesi data admin yang sedang login
        private AkunUser adminLogin;

        // Konstruktor menerima parameter data User dari FormLogin
        public MenuAdmin(AkunUser user)
        {
            InitializeComponent();
            this.adminLogin = user ?? throw new ArgumentNullException(nameof(user), "Sesi admin tidak valid.");
        }

        private void MenuAdmin_Load(object sender, EventArgs e)
        {
            // Logika awal saat halaman dashboard admin dimuat
        }

        // =======================================================================
        // NAVIGASI LINK LABEL SIDEBAR ADMIN (ESTAFET USER LOGIN)
        // =======================================================================

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Menu Profil
        {
            MenuProfilAdmin menuProfil = new MenuProfilAdmin(this.adminLogin);
            menuProfil.Show();
            this.Hide();
        }

        // 1. LINK LABEL KEDUA - UNTUK BUAT TOURNAMENT (Jangan Dihapus)
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuBuatTournament buatTournament = new MenuBuatTournament(this.adminLogin);
            buatTournament.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Menu Lihat Data Pemain
        {
            MenuLihatDataPemain lihatPemain = new MenuLihatDataPemain(this.adminLogin);
            lihatPemain.Show();
            this.Hide();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Menu Lihat Data Tournament
        {
            LihatDataTournament lihatTournament = new LihatDataTournament(this.adminLogin);

            // KUNCI KOORDINAT: Biar form baru numpuk persis di posisi form sekarang
            lihatTournament.StartPosition = FormStartPosition.Manual;
            lihatTournament.Location = this.Location;

            lihatTournament.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Menu Lihat Data Pembayaran
        {
            LihatDataPembayaran lihatPembayaran = new LihatDataPembayaran(this.adminLogin);
            lihatPembayaran.Show();
            this.Hide();
        }

        // =======================================================================
        // TOMBOL LOGOUT
        // =======================================================================
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah kamu yakin ingin keluar dari halaman Admin Hyper Chess?", "LogOut Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }
        }

        // 2. LINK LABEL KEDUA (DUPLIKAT NYA) - UNTUK MENU PERTANDINGAN (Tetap Dipertahankan agar Designer tidak Error)
        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuPertandingan formPertandingan = new MenuPertandingan(this.adminLogin);
            formPertandingan.Show();
            this.Hide();
        }
    }
}