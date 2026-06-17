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
            // 1. Validasi apakah metode pembayaran sudah dipilih
            if (MetodePembayaran.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih metode pembayaran terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validasi apakah nominal kosong
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Silakan masukkan nominal pembayaran!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Validasi format angka (mencegah crash jika diinput huruf/karakter aneh)
            if (!int.TryParse(textBox1.Text, out int nominalInput))
            {
                MessageBox.Show("Nominal pembayaran harus berupa angka seluruhnya!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Tentukan biaya asli turnamen (UFC CHESS Rapid = 25000)
            // Kerugian bandar teratasi karena ini dikirim sebagai benchmark validasi ke controller
            int biayaAsliTurnamen = this.hargaPendaftaran;

            // 5. Ambil ID Metode Pembayaran dari index komponen UI kamu
            int idMetode = MetodePembayaran.SelectedIndex + 1;

            try
            {
                // 6. Alihkan pembuatan objek dan validasi harga ke Controller
                TransaksiController transaksiController = new TransaksiController();
                string hasil = transaksiController.ProsesPembayaranOtomatis(
                    this.idKompetisi,
                    idMetode,
                    nominalInput,
                    this.userLogin.IdUser,
                    biayaAsliTurnamen
                );

                // 7. Cek respon balik dari Controller
                if (hasil == "SUKSES")
                {
                    MessageBox.Show("Pembayaran Berhasil! Anda otomatis terdaftar dan disetujui (Auto-ACC).", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MenuTournament tournamentForm = new MenuTournament(this.userLogin);
                    tournamentForm.Show();
                    this.Close();
                }
                else if (hasil.StartsWith("VALIDASI_GAGAL:"))
                {
                    // Menangkap jika harga tidak pas atau angka <= 0 tanpa bikin aplikasi force close
                    string pesanPeringatan = hasil.Replace("VALIDASI_GAGAL:", "").Trim();
                    MessageBox.Show(pesanPeringatan, "Peringatan Pembayaran", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Menangkap jika ada kegagalan query database PostgreSQL
                    MessageBox.Show(hasil, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Pengaman terakhir jika ada hal tidak terduga di luar logika bisnis
                MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message, "Error Global", MessageBoxButtons.OK, MessageBoxIcon.Error);
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