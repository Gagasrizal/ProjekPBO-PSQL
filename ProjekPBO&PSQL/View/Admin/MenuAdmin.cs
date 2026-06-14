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
            this.adminLogin = user; // Menyimpan data admin aktif
        }
        private void MenuAdmin_Load(object sender, EventArgs e)
        {
            // Logika awal saat halaman dashboard admin dimuat (jika ada)
        }

        // =======================================================================
        // NAVIGASI LINK LABEL SIDEBAR ADMIN (ESTAFET USER LOGIN)
        // =======================================================================

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Menu Profil
        {
            // Di sini baru benar memanggil MenuProfilAdmin
            MenuProfilAdmin menuProfil = new MenuProfilAdmin(this.adminLogin);
            menuProfil.Show();
            this.Hide(); // Menyembunyikan dashboard Selamat Datang
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Menu Buat Tournament
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

            // 2. KUNCI KOORDINAT: Biar form baru numpuk persis di posisi form sekarang
            lihatTournament.StartPosition = FormStartPosition.Manual;
            lihatTournament.Location = this.Location;

            // 3. Tampilkan form baru, lalu sembunyikan yang lama
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
                this.Close(); // Menutup form MenuAdmin dengan aman dan kembali ke Login awal
            }
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuPertandingan formPertandingan = new MenuPertandingan();

            // 2. Tampilkan form tujuan
            formPertandingan.Show();

            // 3. Sembunyikan form yang sedang aktif (opsional, agar tidak menumpuk)
            this.Hide();
        }
    }
}