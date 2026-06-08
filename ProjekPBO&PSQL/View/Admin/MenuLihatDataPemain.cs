using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Helpers; // Tambahkan ini agar bisa memanggil DBHelper

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class MenuLihatDataPemain : Form
    {
        private User adminLogin;
        private int _idKompetisiTerpilih;

        // Konstruktor 1: Bawaan (jika diakses langsung tanpa lewat combobox)
        public MenuLihatDataPemain(User user)
        {
            InitializeComponent();
            this.adminLogin = user;
        }

        // Konstruktor 2: OVERLOAD (Dipakai saat dilempar otomatis dari ComboBox)
        public MenuLihatDataPemain(User user, int idKompetisi)
        {
            InitializeComponent();
            this.adminLogin = user;
            this._idKompetisiTerpilih = idKompetisi;
        }

        private void MenuLihatDataPemain_Load(object sender, EventArgs e)
        {
            // Jika masuk ke halaman ini membawa ID Kompetisi dari ComboBox, langsung load data pemainnya
            if (_idKompetisiTerpilih > 0)
            {
                TampilkanPendaftarTournament(_idKompetisiTerpilih);
            }
        }

        private void TampilkanPendaftarTournament(int idKompetisi)
        {
            DBHelper db = new DBHelper();
            // Pastikan nama dataGridView di MenuLihatDataPemain disesuaikan (misal: dataGridView1)
            dataGridView1.DataSource = db.AmbilPendaftarBerdasarkanTournament(idKompetisi);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LihatDataTournament lihatTournament = new LihatDataTournament(this.adminLogin);
            lihatTournament.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuProfilAdmin menuProfil = new MenuProfilAdmin(this.adminLogin);
            menuProfil.Show();
            this.Hide(); 
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LihatDataPembayaran lihatPembayaran = new LihatDataPembayaran(this.adminLogin);
            lihatPembayaran.Show();
            this.Hide();
        }

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
    }
}