using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models;


namespace ProjekPBO_PSQL.View.Admin
{
    public partial class LihatDataPembayaran : Form
    {
        private User adminLogin; // Tambahkan ini

        // Ubah dari public LihatDataPembayaran() menjadi:
        public LihatDataPembayaran(User user)
        {
            InitializeComponent();
            this.adminLogin = user; // Simpan sesi
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Di sini baru benar memanggil MenuProfilAdmin
            MenuProfilAdmin menuProfil = new MenuProfilAdmin(this.adminLogin);
            menuProfil.Show();
            this.Hide(); // Menyembunyikan dashboard Selamat Datang
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LihatDataTournament lihatTournament = new LihatDataTournament(this.adminLogin);
            lihatTournament.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

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
    }
}