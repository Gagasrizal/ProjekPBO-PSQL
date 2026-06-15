using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Models.Context;
using ProjekPBO_PSQL.View.Pemain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjekPBO_PSQL.View.Pemain
{
    public partial class MenuProfilPem : Form
    {
        private User userLogin;
        private Detail_UserContext detail_UserContext = new Detail_UserContext();

        // Konstruktor menerima data User dari form login / menu sebelumnya
        public MenuProfilPem(User user)
        {
            InitializeComponent();
            this.userLogin = user;
        }

        private void MenuProfilPem_Load(object sender, EventArgs e)
        {
            if (userLogin != null)
            {
                // 1. Tampilkan data dari tabel 'users' milik Bangijal
                label12.Text = userLogin.username;              // Akan muncul: Bangijal
                label17.Text = userLogin.email;                 // Akan muncul: bangizals@turnamen.c...

                // 2. Ambil detail profile berdasarkan id_user = 5
                Detail_User detail = detail_UserContext.GetDetailUserByUserId(userLogin.id);

                if (detail != null)
                {
                    label14.Text = detail.Negara;                                 // Akan muncul: Indonesia
                    label13.Text = detail.Elo_rating.ToString();                  // Akan muncul: 1200
                    label20.Text = detail.No_telepon;                             // Akan muncul: +6482234671231
                    label18.Text = detail.Tanggal_lahir.ToString("dd MMMM yyyy"); // Akan muncul: 28 September 1995

                    // Menampilkan tanggal dibuatnya akun Bangijal
                    label1.Text = "Account Created on " + detail.CreatedAt.ToString("dd MMM yyyy"); // Akan muncul: 06 Jun 2026

                    
                }
            }
            else
            {
                MessageBox.Show("Data sesi login tidak ditemukan! Pastikan login dari FormLogin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)// History Pertandingan
        {
            MenuHistoryPermainan historyForm = new MenuHistoryPermainan(this.userLogin);
            historyForm.Show();
            this.Close();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Baca Peraturan
        {
            MenuAturan peraturanForm = new MenuAturan(this.userLogin);
            peraturanForm.Show();
            this.Close();
        }

        // =======================================================================
        // TOMBOL LOGOUT
        // =======================================================================
        private void roundedButton1_Click(object sender, EventArgs e) // Button LogOut
        {
            DialogResult dialogResult = MessageBox.Show("Apakah kamu yakin ingin keluar dari Hyper Chess?", "LogOut", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
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
        private void roundedpanel1_Paint(object sender, PaintEventArgs e) { }

        // Event click baru dari label yang baru kamu tambahkan tadi
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Kamu sudah berada di halaman profil.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuTournament daftarForm = new MenuTournament(this.userLogin);
            daftarForm.Show();
            this.Close(); // Hancurkan form lama dari memori
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuHistoryPermainan historyForm = new MenuHistoryPermainan(this.userLogin);
            historyForm.Show();
            this.Close(); // Hancurkan form lama dari memori
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuAturan peraturanForm = new MenuAturan(this.userLogin);
            peraturanForm.Show();
            this.Close(); // Hancurkan form lama dari memori
        }

        private void roundedPictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}