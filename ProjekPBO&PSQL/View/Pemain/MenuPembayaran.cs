using ProjekPBO_PSQL.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Models.Context;

namespace ProjekPBO_PSQL.View.Pemain
{
    public partial class MenuPembayaran : Form
    {
        private User userLogin;
        private int idKompetisi;
        private string namaKompetisi;
        private int hargaPendaftaran;

        // Constructor untuk menerima lemparan data dari MenuTournament
        public MenuPembayaran(User user, int idKompetisi, string namaKompetisi, int hargaPendaftaran)
        {
            InitializeComponent();
            this.userLogin = user;
            this.idKompetisi = idKompetisi;
            this.namaKompetisi = namaKompetisi;
            this.hargaPendaftaran = hargaPendaftaran;
        }


        // =======================================================================
        // TOMBOL UTAMA (Di desainer kamu namanya Edit_Click)
        // =======================================================================
        private void Edit_Click(object sender, EventArgs e)
        {
            if (MetodePembayaran.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih metode pembayaran terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Silakan masukkan nominal pembayaran!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idUser = this.userLogin.id;
                int idKompetisi = this.idKompetisi;

                int idMetode = MetodePembayaran.SelectedIndex + 1;
                // Ambil nominal dari textBox1
                int nominal = Convert.ToInt32(textBox1.Text);

                Transaksi trxBaru = new Transaksi(0, idUser, idKompetisi, idMetode, nominal, "Sukses", DateTime.Now);

                // 4. Kirim objek model ke DBHelper
                TransaksiContext transaksiContext = new TransaksiContext();
                bool berhasil = transaksiContext.BayarDanDaftarOtomatis(trxBaru);

                if (berhasil)
                {
                    MessageBox.Show("Pembayaran Berhasil! Anda otomatis terdaftar dan disetujui (Auto-ACC).", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Pembayaran", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tombol cadangan jika sewaktu-waktu click event-nya beralih ke roundedButton1
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah kamu yakin ingin keluar dari Hyper Chess?", "LogOut", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close(); // Menutup form Cari Pemain dengan aman
            }// Langsung alihkan fungsinya ke Edit_Click agar sama-sama jalan dengan aman
        }

        // =======================================================================
        // LINK LABEL SIDEBAR NAVIGASI
        // =======================================================================
        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuProfilPem profilForm = new MenuProfilPem(this.userLogin);
            profilForm.Show();
            this.Close(); // Hancurkan form pembayaran saat ini
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuTournament tournamentForm = new MenuTournament(this.userLogin);
            tournamentForm.Show();
            this.Close(); // Hancurkan form pembayaran saat ini
        }


        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuHistoryPermainan historyForm = new MenuHistoryPermainan(this.userLogin);
            historyForm.Show();
            this.Close(); // Hancurkan form pembayaran saat ini
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuAturan peraturanForm = new MenuAturan(this.userLogin);
            peraturanForm.Show();
            this.Close(); // Hancurkan form pembayaran saat ini
        }

        private void label8_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void MetodePembayaran_SelectedIndexChanged(object sender, EventArgs e)
        { 

        }
    }
}