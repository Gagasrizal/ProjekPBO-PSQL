using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models; // Memastikan objek 'User' dikenali
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjekPBO_PSQL.View.Pemain
{
    public partial class MenuHistoryPermainan : Form
    {
        // Variabel global untuk menyimpan sesi data user yang sedang login
        private Models.Admin userLogin;

        // Konstruktor diubah agar menerima data User dari form sebelumnya
        public MenuHistoryPermainan(Models.Admin user)
        {
            InitializeComponent();
            this.userLogin = user;
            this.Load += MenuHistoryPermainan_Load;
        }

        private void MenuHistoryPermainan_Load(object sender, EventArgs e)
        {
            TampilkanHistoryPertandingan();
        }

        // Tambah method tampil data
        private void TampilkanHistoryPertandingan()
        {
            try
            {
                DBHelper db = new DBHelper();
                DataTable dt = db.AmbilHistoryPertandingan(userLogin.id);

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                if (dt != null && dataGridView1.Columns.Count >= 5)
                {
                    dataGridView1.Columns[0].HeaderText = "Tournament";
                    dataGridView1.Columns[1].HeaderText = "Babak";
                    dataGridView1.Columns[2].HeaderText = "Lawan";
                    dataGridView1.Columns[3].HeaderText = "Warna";
                    dataGridView1.Columns[4].HeaderText = "Hasil";

                    dataGridView1.ColumnHeadersHeight = 36;
                    dataGridView1.RowTemplate.Height = 34;
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                this.Close(); // Menutup form History dengan aman
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Event klik di grid history pertandingan
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Mengoper kembali data userLogin ke MenuProfilPem agar profil tetap sinkron
            MenuProfilPem profilForm = new MenuProfilPem(this.userLogin);
            profilForm.Show();
            this.Close();
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuTournament tournamentForm = new MenuTournament(this.userLogin);
            tournamentForm.Show();
            this.Close();
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Kamu sudah berada di halaman History Pertandingan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuAturan peraturanForm = new MenuAturan(this.userLogin);
            peraturanForm.Show();
            this.Close();
        }
    }
}