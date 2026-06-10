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
        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Jika linkLabel7 digunakan untuk navigasi lain (misal kembali ke Menu Utama Pemain)
            MenuPemain utamaForm = new MenuPemain(this.userLogin);
            utamaForm.Show();
            this.Close();
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

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuProfilPem profilForm = new MenuProfilPem(this.userLogin);
            profilForm.Show();
            this.Close(); // Hancurkan form lama dari memori
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuTournament tournamentForm = new MenuTournament(this.userLogin);
            tournamentForm.Show();
            this.Close(); // Hancurkan form lama dari memori
        }

        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuHistoryPermainan historyForm = new MenuHistoryPermainan(this.userLogin);
            historyForm.Show();
            this.Close(); // Hancurkan form lama dari memori
        }

        private void linkLabel6_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Kamu sudah berada di halaman Baca Peraturan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}