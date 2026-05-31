using ProjekPBO_PSQL.View.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjekPBO_PSQL
{
    public partial class MenuAdmin : Form
    {
        public MenuAdmin()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Membuat objek baru dari form MenuProfilAdmin
            MenuProfilAdmin menuProfil = new MenuProfilAdmin();

            // 2. Menampilkan form MenuProfilAdmin
            menuProfil.Show();

            // 3. (Opsional) Menutup atau menyembunyikan form yang sekarang aktif
            this.Hide(); // Menggunakan Hide() agar form saat ini tidak terlihat
                         // atau gunakan: this.Close(); jika ingin menutup total form saat ini
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Membuat objek baru dari form MenuBuatTournament
            MenuBuatTournament buatTournament = new MenuBuatTournament();

            // 2. Menampilkan form MenuBuatTournament
            buatTournament.Show();

            // 3. (Opsional) Menyembunyikan form yang sekarang aktif
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Membuat objek baru dari form MenuLihatDataPemain
            MenuLihatDataPemain lihatPemain = new MenuLihatDataPemain();

            // 2. Menampilkan form MenuLihatDataPemain
            lihatPemain.Show();

            // 3. (Opsional) Menyembunyikan form yang sekarang aktif
            this.Hide();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Membuat objek baru dari form LihatDataTournament
            LihatDataTournament lihatTournament = new LihatDataTournament();

            // 2. Menampilkan form LihatDataTournament
            lihatTournament.Show();

            // 3. (Opsional) Menyembunyikan form yang sekarang aktif
            this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Membuat objek baru dari form LihatDataPembayaran
            LihatDataPembayaran lihatPembayaran = new LihatDataPembayaran();

            // 2. Menampilkan form LihatDataPembayaran
            lihatPembayaran.Show();

            // 3. (Opsional) Menyembunyikan form yang sekarang aktif
            this.Hide();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
