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
    public partial class MenuBuatTournament : Form
    {
        private User adminLogin;
        private bool isEditMode = false;
        private int idTournamentDiedit = 0;

        // --- CONSTRUCTOR 1: Dipakai saat membuat turnamen baru (Normal Mode) ---
        public MenuBuatTournament(User user)
        {
            InitializeComponent();
            this.adminLogin = user;
            this.isEditMode = false;
        }

        // --- CONSTRUCTOR 2: Dipakai saat mengedit turnamen (Edit Mode) ---
        public MenuBuatTournament(User user, int idKompetisi, string namaLama, int hargaLama, int hadiahLama)
        {
            InitializeComponent();
            this.adminLogin = user;

            // Set status ke mode edit dan simpan ID-nya
            this.isEditMode = true;
            this.idTournamentDiedit = idKompetisi;

            // Ubah text tombol simpan menjadi Update
            roundedButton2.Text = "Update";

            // Lempar data lama ke dalam inputan Form secara otomatis
            NamaTournament.Text = namaLama;
            HargaPendaftaran.Text = hargaLama.ToString();
            Hadiah.Text = hadiahLama.ToString();
        }

        private void MenuBuatTournament_Load(object sender, EventArgs e)
        {
            // Logika saat halaman buat turnamen pertama kali dimuat
        }

        private void ClearForm()
        {
            NamaTournament.Clear();
            HargaPendaftaran.Clear();
            Hadiah.Clear();
            TipeGame.SelectedIndex = -1;
            TimeControl.SelectedIndex = -1;
            Babak.SelectedIndex = -1;
            TanggalPelaksanaan.Value = DateTime.Now;
        }

        private void roundedButton2_Click_1(object sender, EventArgs e)
        {
            string nama = NamaTournament.Text.Trim();
            string tipeGame = TipeGame.Text;
            string timeControl = TimeControl.Text;
            string babak = Babak.Text;
            DateTime tanggal = TanggalPelaksanaan.Value;
            string hargaText = HargaPendaftaran.Text.Trim();
            string hadiahText = Hadiah.Text.Trim();

            // Validasi input
            if (string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(tipeGame) ||
                string.IsNullOrEmpty(timeControl) || string.IsNullOrEmpty(babak) ||
                string.IsNullOrEmpty(hargaText) || string.IsNullOrEmpty(hadiahText))
            {
                MessageBox.Show("Semua kolom data kompetisi wajib diisi atau dipilih!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int harga = Convert.ToInt32(hargaText);
                int hadiah = Convert.ToInt32(hadiahText);

                string modeKompetisiCombined = $"{tipeGame} ({timeControl})";
                DateTime tanggalDitutup = tanggal.AddDays(-1);
                string pelaksanaanPendaftaranCombined = $"{DateTime.Today:dd MMM yyyy} s.d {tanggalDitutup:dd MMM yyyy}";

                int jumlahBabakTerpilih = Convert.ToInt32(Babak.SelectedItem ?? 1);
                int idYangDipakai = isEditMode ? idTournamentDiedit : 0;

                Tournament kompetisiBaru = new Tournament(
                    idYangDipakai,
                    1,
                    nama,
                    modeKompetisiCombined,
                    harga,
                    pelaksanaanPendaftaranCombined,
                    tanggal,
                    hadiah,
                    $"Sistem Swiss",
                    jumlahBabakTerpilih
                );

                DBHelper db = new DBHelper();
                bool sukses = false;

                if (isEditMode)
                {
                    sukses = db.EditTournament(kompetisiBaru);
                }
                else
                {
                    sukses = db.TambahTournament(kompetisiBaru);
                }

                if (sukses)
                {
                    string aksi = isEditMode ? "diperbarui" : "berhasil dibuat";
                    MessageBox.Show($"Kompetisi '{nama}' {aksi}!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (isEditMode)
                    {
                        this.Close();
                    }
                    else
                    {
                        ClearForm();
                    }
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan data kompetisi ke database.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Harga Pendaftaran dan Hadiah harus berupa angka bulat saja!", "Kesalahan Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metode penampung event bawaan desainer
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }

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
            LihatDataPembayaran lihatPembayaran = new LihatDataPembayaran(this.adminLogin);
            lihatPembayaran.Show();
            this.Hide();
        }

        private void roundedButton1_Click(object sender, EventArgs e) { }
    }
}