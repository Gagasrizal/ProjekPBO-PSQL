using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Helpers; // Memastikan DBHelper bisa dipanggil

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class LihatDataPembayaran : Form
    {
        private User adminLogin;

        public LihatDataPembayaran(User user)
        {
            InitializeComponent();
            this.adminLogin = user; // Simpan sesi admin
            this.Load += new System.EventHandler(this.LihatDataPembayaran_Load);
            linkLabel1.LinkClicked += linkLabel1_LinkClicked; // Profil Admin
            linkLabel4.LinkClicked += linkLabel4_LinkClicked; // Lihat Data Tournament
            linkLabel5.LinkClicked += linkLabel5_LinkClicked; // Lihat Data Pembayaran (Halaman ini)
        }

        // =======================================================================
        // EVENT LOAD: OTOMATIS TARIK DATA PEMBAYARAN DARI DATABASE
        // =======================================================================
        private void LihatDataPembayaran_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Panggil DBHelper
                // 1. Panggil DBHelper
                DBHelper db = new DBHelper();

                // 2. Ambil data pembayaran lewat fungsi yang pas dengan skema database barumu
                DataTable dtPembayaran = db.AmbilSemuaPembayaran();

                // 3. Masukkan ke DataGridView admin
                dataGridView1.DataSource = dtPembayaran;

                // 4. Kunci tabel biar gak bisa diedit sembarangan oleh admin jancok haha
                dataGridView1.ReadOnly = true;

                // 5. Pengaturan layout agar kolom otomatis memenuhi lebar layar secara rapi
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Setting seleksi baris utuh
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Load Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================================
        // NAVIGASI LINK LABEL SIDEBAR ADMIN
        // =======================================================================
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuProfilAdmin menuProfil = new MenuProfilAdmin(this.adminLogin);
            menuProfil.Show();
            this.Hide();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LihatDataTournament lihatTournament = new LihatDataTournament(this.adminLogin);
            lihatTournament.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Kamu sudah berada di halaman Data Pembayaran Admin.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =======================================================================
        // TOMBOL LOGOUT ADMIN
        // =======================================================================
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah kamu yakin ingin keluar dari halaman Admin Hyper Chess?", "LogOut Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Event klik isi tabel jika diperlukan nanti
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuPertandingan formPertandingan = new MenuPertandingan();

            // 2. Tampilkan form tujuan
            formPertandingan.Show();

            // 3. Sembunyikan form yang sedang aktif (opsional, agar tidak menumpuk)
            this.Hide();
        }
    }
}