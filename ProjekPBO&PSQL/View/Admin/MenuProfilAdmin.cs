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
            // Cek apakah adminLogin kosong di awal fungsi
            if (adminLogin != null)
            {
                // 1. Isi data dasar dari objek login
                label12.Text = adminLogin.username;
                label17.Text = adminLogin.email;

                // 2. Ambil detail user dari PostgreSQL
                Detail_User detail = dbHelper.GetDetailUserByUserId(adminLogin.id);

                if (detail != null)
                {
                    label14.Text = detail.Negara;
                    label13.Text = detail.Elo_rating.ToString();
                    label20.Text = detail.No_telepon;
                    label18.Text = detail.Tanggal_lahir.ToString("dd MMMM yyyy");

                    // Pindahkan tulisan CreatedAt dari label3 ke label1 (label paling bawah)
                    label1.Text = "Account Created on: " + detail.CreatedAt.ToString("dd MMM yyyy");
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
                // Blok else ini sekarang aman berada di dalam fungsi Load
                MessageBox.Show("Data sesi login admin tidak ditemukan! Pastikan masuk dari FormLogin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =======================================================================
        // NAVIGASI LINK LABEL SIDEBAR ADMIN (ESTAFET SISI LOGIN)
        // =======================================================================

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Profil (Form Ini)
        {

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
        private void label13_Click(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e) { }
        private void label20_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label18_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label17_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

        private void roundedpanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Edit_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Buat instance dari form tujuan
            MenuPertandingan formPertandingan = new MenuPertandingan();

            // 2. Tampilkan form tujuan
            formPertandingan.Show();

            // 3. Sembunyikan form yang sedang aktif (opsional, agar tidak menumpuk)
            this.Hide();
        }
    }
}