using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
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
        private User adminLogin;
        private DBHelper dbHelper = new DBHelper();

        // Konstruktor menerima data User dari MenuAdmin atau form admin sebelumnya
        public MenuProfilAdmin(User user)
        {
            InitializeComponent();
            this.adminLogin = user;
        }

        private void MenuProfilAdmin_Load(object sender, EventArgs e)
        {
            if (adminLogin != null)
            {
                // 1. Tampilkan data dasar dari tabel 'users' milik Admin yang login
                label12.Text = adminLogin.username; // Mengisi RJalsn
                label17.Text = adminLogin.email;    // Mengisi Email utama admin
                label1.Text = adminLogin.email;     // Mengisi Email informasi bawah

                // 2. Ambil detail profile dari database berdasarkan id_user admin
                Detail_User detail = dbHelper.GetDetailUserByUserId(adminLogin.id);

                if (detail != null)
                {
                    label14.Text = detail.Negara;                                 // Memunculkan negara (Indonesia)
                    label13.Text = detail.Elo_rating.ToString();                  // Memunculkan Elo Admin
                    label20.Text = detail.No_telepon;                             // Memunculkan nomor handphone
                    label18.Text = detail.Tanggal_lahir.ToString("dd MMMM yyyy"); // Memunculkan tanggal lahir

                    // Karena label3 di desainer kamu posisinya kosong/kosongan, kita pakai untuk tempat tanggal buat akun:
                    label3.Text = "Account Created on: " + detail.CreatedAt.ToString("dd MMM yyyy");
                }
                else
                {
                    // JIKA DATA DETAIL BELUM ADA DI DATABASE:
                    // Tetap tampilkan data login utamanya agar form tidak terlihat kosong melompong
                    label14.Text = "-";
                    label13.Text = "-";
                    label20.Text = "-";
                    label18.Text = "-";
                    label3.Text = "Detail profile belum diatur di database.";
                }
            }
            else
            {
                MessageBox.Show("Data sesi login admin tidak ditemukan! Pastikan masuk dari FormLogin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =======================================================================
        // NAVIGASI LINK LABEL SIDEBAR ADMIN (ESTAFET SISI LOGIN)
        // =======================================================================

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Profil (Form Ini)
        {
            MessageBox.Show("Anda sudah berada di halaman Profil Admin.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Buat Tournament
        {
            MenuBuatTournament buatTournament = new MenuBuatTournament(this.adminLogin);
            buatTournament.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Lihat Data Pemain
        {
            MenuLihatDataPemain lihatPemain = new MenuLihatDataPemain(this.adminLogin);
            lihatPemain.Show();
            this.Hide();
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
    }
}