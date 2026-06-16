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
    private AkunUser userLogin;
    private int idKompetisi;
    private string namaKompetisi;
    private int hargaPendaftaran;

    // Constructor untuk menerima lemparan data dari MenuTournament
    public MenuPembayaran(AkunUser user, int idKompetisi, string namaKompetisi, int hargaPendaftaran)
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
            int idMetode = MetodePembayaran.SelectedIndex + 1;
            int nominal = Convert.ToInt32(textBox1.Text);

            // =======================================================================
            // FIX TOTAL ERROR CS1729: Menyelaraskan dengan properti asli Transaksi.cs
            // =======================================================================
            Transaksi trxBaru = new Transaksi()
            {
                // Di model Anda, fieldnya bernama IdPendaftaranKompetisi, IdMetodePembayaran, dll.
                IdPendaftaranKompetisi = this.idKompetisi,
                IdMetodePembayaran = idMetode,
                NominalTransaksi = nominal,
                TanggalTransaksi = DateTime.Now,
                StatusTransaksi = "Sudah Lunas" // FIX VALIDASI: Harus "Sudah Lunas" agar tidak memicu ArgumentException!
            };

            // Kirim objek model ke DBHelper Anda
            TransaksiContext transaksiContext = new TransaksiContext();
            bool berhasil = transaksiContext.BayarDanDaftarOtomatis(trxBaru);

            if (berhasil)
            {
                MessageBox.Show("Pembayaran Berhasil! Anda otomatis terdaftar dan disetujui (Auto-ACC).", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            // Menangkap pesan error, termasuk jika nominal <= 0 dari aturan enkapsulasi model Anda
            MessageBox.Show(ex.Message, "Error Pembayaran", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

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

    // =======================================================================
    // LINK LABEL SIDEBAR NAVIGASI
    // =======================================================================
    private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
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
        MenuHistoryPermainan historyForm = new MenuHistoryPermainan(this.userLogin);
        historyForm.Show();
        this.Close();
    }

    private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        MenuAturan peraturanForm = new MenuAturan(this.userLogin);
        peraturanForm.Show();
        this.Close();
    }

    private void label8_Click(object sender, EventArgs e) { }
    private void textBox1_TextChanged(object sender, EventArgs e) { }
    private void MetodePembayaran_SelectedIndexChanged(object sender, EventArgs e) { }
}
}