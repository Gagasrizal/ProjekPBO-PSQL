using ProjekPBO_PSQL.View.Admin;
using ProjekPBO_PSQL.View.Pemain;
using ProjekPBO_PSQL.Models;
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
        // Variabel global untuk menyimpan sesi data user yang sedang login
        private User userLogin;

        // Konstruktor Utama menerima data User yang dikirim dari FormLogin
        public MenuPemain(User user)
        {
            InitializeComponent();
            this.userLogin = user;
        }

        private void MenuPemain_Load(object sender, EventArgs e)
        {
            // Bisa dikosongkan
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
                this.Close();
            }
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuProfilPem formProfil = new MenuProfilPem(this.userLogin);
            formProfil.Show();
            this.Close(); // Hancurkan form saat ini agar memori bersih
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuTournament formDaftar = new MenuTournament(this.userLogin);
            formDaftar.Show();
            this.Close(); // Hancurkan form saat ini agar memori bersih
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuCariPemain formCari = new MenuCariPemain(this.userLogin);
            formCari.Show();
            this.Close(); // Hancurkan form saat ini agar memori bersih
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuHistoryPermainan formHistory = new MenuHistoryPermainan(this.userLogin);
            formHistory.Show();
            this.Close(); // Hancurkan form saat ini agar memori bersih
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuAturan formAturan = new MenuAturan(this.userLogin);
            formAturan.Show();
            this.Close(); // Hancurkan form saat ini agar memori bersih
        }
    }
}