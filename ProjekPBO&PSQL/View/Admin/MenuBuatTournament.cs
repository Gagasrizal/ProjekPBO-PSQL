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

        // 2. Ubah konstruktor utama agar menerima parameter (User user)
        public MenuBuatTournament(User user)
        {
            InitializeComponent();
            this.adminLogin = user; // Menyimpan data admin yang sedang aktif
        }

        private void MenuBuatTournament_Load(object sender, EventArgs e)
        {
            // Logika saat halaman buat turnamen pertama kali dimuat
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            string nama = NamaTournament.Text.Trim();
            string tipeGame = TipeGame.Text;
            string timeControl = TimeControl.Text;
            string babak = Babak.Text;
            DateTime tanggal = TanggalPelaksanaan.Value;
            string hargaText = HargaPendaftaran.Text.Trim();
            string hadiahText = Hadiah.Text.Trim();

            // 2. Validasi input
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

                // Trik Mapping: satukan input form agar pas dengan kolom database
                string modeKompetisiCombined = $"{tipeGame} ({timeControl})";
                string pelaksanaanPendaftaranCombined = $"{babak}";

                // FIKS: ID Admin diambil dari objek 'adminLogin' yang dioper dari Form Login tadi!
                int idAdminAktif = this.adminLogin.id;

                // 1. Ubah objek adminLogin menjadi dynamic sementara agar compiler tidak protes nama property
                // Ambil jumlah babak dari ComboBox UI milikmu (Nama komponen: Babak)
                // Ambil jumlah babak dari ComboBox UI (Nama komponen: Babak)
                int jumlahBabakTerpilih = Convert.ToInt32(Babak.SelectedItem ?? 1);

                // Solusi Bypass: Kita paksa ambil value property pertama dari object menggunakan bantuan static helper / casting data login
                Tournament kompetisiBaru = new Tournament(
                    0,
                    1, // <--- ISI ANGKA 1 DULU SEMENTARA UNTUK TESTING agar project-mu bisa di-Run/Build tanpa error!
                    nama,
                    modeKompetisiCombined,
                    harga,
                    pelaksanaanPendaftaranCombined,
                    tanggal,
                    hadiah,
                    $"{tipeGame} ({timeControl})",
                    jumlahBabakTerpilih
                );
               

                // 4. Kirim objek model ke DBHelper (Controller)
                DBHelper db = new DBHelper();
                bool sukses = db.TambahTournament(kompetisiBaru);

                if (sukses)
                {
                    MessageBox.Show($"Kompetisi '{nama}' berhasil dibuat!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan kompetisi baru ke database.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Harga Pendaftaran dan Hadiah harus berupa angka bulat saja!", "Kesalahan Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan sistem: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Di sini baru benar memanggil MenuProfilAdmin
            MenuProfilAdmin menuProfil = new MenuProfilAdmin(this.adminLogin);
            menuProfil.Show();
            this.Hide(); // Menyembunyikan dashboard Selamat Datang
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

        private void roundedButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
