using ProjekPBO_PSQL.View.Admin;
using ProjekPBO_PSQL.View.Pemain;
using ProjekPBO_PSQL.Models; // Ditambahkan agar class User bisa dikenali
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjekPBO_PSQL
{
    public partial class MenuPemain : Form
    {
        // Variabel global untuk menyimpan data user yang sedang login
        private User userLogin;

        // Constructor diubah untuk menerima parameter objek User dari FormLogin
        public MenuPemain(User user)
        {
            InitializeComponent();
            this.userLogin = user; // Simpan data user terlogin
        }

        private void MenuPemain_Load(object sender, EventArgs e)
        {

        }

        // =======================================================================
        // 1. MENU PROFIL -> Menuju MenuProfilPem.cs
        // =======================================================================
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Mengoper data userLogin ke MenuProfilPem agar data profil bisa ditampilkan
            MenuProfilPem profilForm = new MenuProfilPem(this.userLogin);
            profilForm.Show();
            this.Hide();
        }

        // =======================================================================
        // 2. MENU LIST TOURNAMENT -> Menuju MenuTournament.cs
        // =======================================================================
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuTournament listForm = new MenuTournament();
            listForm.Show();
            this.Hide();
        }

        // =======================================================================
        // 3. MENU DAFTAR TOURNAMENT -> Menuju MenuDaftarTour.cs
        // =======================================================================
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuDaftarTour daftarForm = new MenuDaftarTour();
            daftarForm.Show();
            this.Hide();
        }

        // =======================================================================
        // 4. MENU CARI PEMAIN -> Menuju MenuCariPemain.cs
        // =======================================================================
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuCariPemain cariForm = new MenuCariPemain();
            cariForm.Show();
            this.Hide();
        }

        // =======================================================================
        // 5. MENU HISTORY PERTANDINGAN -> Menuju MenuHistoryPermainan.cs
        // =======================================================================
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuHistoryPermainan historyForm = new MenuHistoryPermainan();
            historyForm.Show();
            this.Hide();
        }

        // =======================================================================
        // 6. MENU BACA PERATURAN -> Menuju MenuAturan.cs
        // =======================================================================
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuAturan peraturanForm = new MenuAturan();
            peraturanForm.Show();
            this.Hide();
        }

        // =======================================================================
        // 7. TOMBOL LOGOUT -> Menuju FormLogin.cs
        // =======================================================================
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah kamu yakin ingin keluar dari Hyper Chess?", "LogOut", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close(); // Menutup MenuPemain dengan aman
            }
        }
    }
}