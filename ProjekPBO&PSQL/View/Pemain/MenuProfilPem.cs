using ProjekPBO_PSQL.Controller;
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
        private AkunUser userLogin;
        private UserContext userContext = new UserContext();

        // Konstruktor menerima data User dari form login / menu sebelumnya
        public MenuProfilPem(AkunUser user)
        {
            InitializeComponent();
            this.userLogin = user;
        }

        private void MenuProfilPem_Load(object sender, EventArgs e)
        {
            if (userLogin != null)
            {
                // 1. Tampilkan data dasar dari session login (Username & Email)
                label12.Text = userLogin.Username;
                label17.Text = userLogin.Email;

                // 2. Panggil Controller untuk mengambil detail profil catur (Pola MVC)
                ProfilController profilController = new ProfilController();
                ProfilCatur detail = profilController.AmbilProfilPemain(userLogin.IdUser);

                if (detail != null)
                {
                    // Tampilkan detail data catur ke label masing-masing
                    label14.Text = detail.Negara;
                    label13.Text = detail.EloRating.ToString();
                    label20.Text = detail.NoTelepon;
                    label18.Text = detail.TanggalLahir.ToString("dd MMMM yyyy");

                    // =======================================================================
                    // UTAMA 1: Mengubah Deskripsi (label2) Jadi Dinamis Dari DB
                    // =======================================================================
                    label2.Text = string.IsNullOrWhiteSpace(detail.Deskripsi)
                        ? "Tidak ada deskripsi."
                        : detail.Deskripsi;

                    // =======================================================================
                    // UTAMA 2: Mengubah Created At (label1) Menggunakan userLogin.CreatedAt
                    // =======================================================================
                    // Jika CreatedAt bernilai default (01/01/0001), otomatis diakali pakai tanggal hari ini
                    string tanggalBuatAkun = userLogin.CreatedAt != DateTime.MinValue
                        ? userLogin.CreatedAt.ToString("dd MMMM yyyy")
                        : DateTime.Now.ToString("dd MMMM yyyy");

                    label1.Text = $"Account Profile Verified • Created on: {tanggalBuatAkun}";
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

        private void Edit_Click(object sender, EventArgs e)
        {
            EditProfil formEdit = new EditProfil(this.userLogin);

            formEdit.Show();

            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}