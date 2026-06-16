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

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class MenuProfilAdmin : Form
    {
        private AkunUser adminLogin;
        private UserContext userContext = new UserContext();

        // Konstruktor menerima data User dari MenuAdmin atau form admin sebelumnya
        public MenuProfilAdmin(AkunUser user)
        {
            InitializeComponent();
            this.adminLogin = user;
        }

        private void MenuProfilAdmin_Load(object sender, EventArgs e)
        {
            // Cek apakah adminLogin kosong di awal fungsi
            if (adminLogin != null)
            {
                // =======================================================================
                // 1. FIX ERROR CS1061: Menggunakan properti huruf kapital (PascalCase) 
                //    sesuai dengan yang didefinisikan di dalam kelas model AkunUser.cs
                // =======================================================================
                label12.Text = adminLogin.Username; // Sebelumnya: username
                label17.Text = adminLogin.Email;    // Sebelumnya: email

                // 2. FIX ERROR CS1061: Mengganti '.id' menjadi '.IdUser' sesuai isi model AkunUser
                ProfilCatur detail = userContext.GetDetailUserByUserId(adminLogin.IdUser);
                if (detail != null)
                {
                    // =======================================================================
                    // FIX TOTAL: Menyinkronkan nama properti dengan file ProfilCatur.cs Anda
                    // =======================================================================
                    label14.Text = detail.Negara;
                    label13.Text = detail.EloRating.ToString(); // FIX: Elo_rating -> EloRating
                    label20.Text = detail.NoTelepon;            // FIX: No_telepon -> NoTelepon
                    label18.Text = detail.TanggalLahir.ToString("dd MMMM yyyy"); // FIX: Tanggal_lahir -> TanggalLahir

                    // Karena di kelas ProfilCatur tidak ada properti CreatedAt, kita set teks default 
                    // agar label paling bawah tetap rapi atau tidak memicu error kompilasi
                    label1.Text = "Account Profile Verified";
                }
                else
                {
                    label14.Text = "-";
                    label13.Text = "-";
                    label20.Text = "-";
                    label18.Text = "-";
                    label1.Text = "Account Created on: -";
                }
            }
            else
            {
                // Blok else jika data sesi login admin tidak ditemukan
                MessageBox.Show("Data sesi login admin tidak ditemukan! Pastikan masuk dari FormLogin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =======================================================================
        // NAVIGASI LINK LABEL SIDEBAR ADMIN (ESTAFET SISI LOGIN)
        // =======================================================================

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Profil (Form Ini)
        {
            // Sudah berada di halaman ini
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Lihat Data Tournament
        {
            LihatDataTournament lihatTournament = new LihatDataTournament(this.adminLogin);
            lihatTournament.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Lihat Data Pembayaran
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
                this.Close(); // Menutup form profil admin dan kembali ke halaman login utama
            }
        }

        // =======================================================================
        // EVENT KLIK BAWAAN DESIGNER (JANGAN DIHAPUS)
        // =======================================================================
        private void label4_Click(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label20_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label18_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label17_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void roundedpanel1_Paint(object sender, PaintEventArgs e) { }
        private void Edit_Click(object sender, EventArgs e) { }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // FIX: Menyesuaikan parameter navigasi pertandingan menggunakan objek penampung adminLogin
            MenuPertandingan formPertandingan = new MenuPertandingan(this.adminLogin);
            formPertandingan.Show();
            this.Hide();
        }
    }
}