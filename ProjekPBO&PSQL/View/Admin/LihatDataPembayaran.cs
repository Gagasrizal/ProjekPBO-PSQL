using ProjekPBO_PSQL.Controller;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class LihatDataPembayaran : Form
    {
        // Menyimpan sesi user/admin yang sedang login
        private AkunUser adminLogin;

        // FIX MVC: Menambahkan field untuk memegang object Controller
        private readonly AdminController _adminController;

        public LihatDataPembayaran(AkunUser user)
        {
            InitializeComponent();

            // Validasi jika objek user yang dikirim ternyata null
            this.adminLogin = user ?? throw new ArgumentNullException(nameof(user), "Sesi user tidak valid atau kosong.");

            // FIX MVC: Instansiasi controller di konstruktor form
            this._adminController = new AdminController();

            this.Load += new System.EventHandler(this.LihatDataPembayaran_Load);
            linkLabel1.LinkClicked += linkLabel1_LinkClicked; // Profil Admin
            linkLabel4.LinkClicked += linkLabel4_LinkClicked; // Lihat Data Tournament
            linkLabel5.LinkClicked += linkLabel5_LinkClicked; // Lihat Data Pembayaran (Halaman ini)
        }

        // =======================================================================
        // EVENT LOAD: OTOMATIS TARIK DATA PEMBAYARAN VIA CONTROLLER
        // =======================================================================
        private void LihatDataPembayaran_Load(object sender, EventArgs e)
        {
            try
            {
                // FIX MVC: Mengambil data pembayaran melalui Controller (Bukan dari Context langsung)
                DataTable dtPembayaran = _adminController.AmbilSemuaPembayaran();

                dataGridView1.DataSource = dtPembayaran;
                dataGridView1.ReadOnly = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Load Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuProfilAdmin menuProfil = new MenuProfilAdmin(this.adminLogin);
            menuProfil.Show();
            this.Close();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LihatDataTournament lihatTournament = new LihatDataTournament(this.adminLogin);
            lihatTournament.Show();
            this.Close();
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
            MenuPertandingan formPertandingan = new MenuPertandingan(this.adminLogin);
            formPertandingan.Show();
            this.Hide();
        }
    }
}