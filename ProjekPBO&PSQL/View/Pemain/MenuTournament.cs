using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.View.Pemain;
using ProjekPBO_PSQL.Models.Context; // Memastikan Context bisa dibaca

namespace ProjekPBO_PSQL
{
    public partial class MenuTournament : Form
    {
        // Variabel global untuk menyimpan sesi data user yang sedang login
        private AkunUser userLogin;

        // Konstruktor diubah agar menerima data User yang dikirim dari form sebelumnya
        public MenuTournament(AkunUser user)
        {
            InitializeComponent();
            this.userLogin = user;

            // =======================================================================
            // KUNCI UTAMA: Daftarkan secara paksa agar tombol Load & Sidebar Aktif
            // =======================================================================
            this.Load += new System.EventHandler(this.MenuTournament_Load);
            linkLabel1.LinkClicked += linkLabel1_LinkClicked_1; // Profil
            linkLabel2.LinkClicked += linkLabel2_LinkClicked_1; // List Tournament
            linkLabel5.LinkClicked += linkLabel5_LinkClicked_1; // History Permainan
            linkLabel6.LinkClicked += linkLabel6_LinkClicked_1; // Baca Peraturan
        }

        // =======================================================================
        // EVENT LOAD: MENAMPILKAN DATA DARI DATABASE KE DATAGRIDVIEW TOURNAMENT
        // =======================================================================
        private void MenuTournament_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Instansiasi objek KompetisiContext (Menggantikan DBHelper)
                KompetisiContext kompetisiCtx = new KompetisiContext();

                // 2. Ambil data dengan fungsi AmbilSemuaTournament() yang ada di KompetisiContext
                DataTable dtTournament = kompetisiCtx.AmbilSemuaTournament();

                // 3. Masukkan datanya sebagai sumber data DataGridView
                dataGridView1.DataSource = dtTournament;

                // =======================================================================
                // KUNCI TABEL DI SINI (Biar tidak bisa diedit)
                // =======================================================================
                dataGridView1.ReadOnly = true;

                // 4. Merapikan nama judul kolom (Header) di tabel visual aplikasi biar rapi
                if (dataGridView1.Columns["id_kompetisi"] != null) dataGridView1.Columns["id_kompetisi"].HeaderText = "ID";
                if (dataGridView1.Columns["nama_kompetisi"] != null) dataGridView1.Columns["nama_kompetisi"].HeaderText = "Nama Turnamen";
                if (dataGridView1.Columns["mode_kompetisi"] != null) dataGridView1.Columns["mode_kompetisi"].HeaderText = "Mode";
                if (dataGridView1.Columns["harga_pendaftaran"] != null) dataGridView1.Columns["harga_pendaftaran"].HeaderText = "Biaya Daftar";
                if (dataGridView1.Columns["hadiah"] != null) dataGridView1.Columns["hadiah"].HeaderText = "Hadiah Utama";

                // Memastikan baris langsung terpilih utuh saat diklik oleh user
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar turnamen: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Event klik isi tabel jika diperlukan nanti
        }

        // =======================================================================
        // TOMBOL UTAMA: PROSES MENDAFTAR (LEMBAR KE HALAMAN BAYAR)
        // =======================================================================
        private void roundedButton2_Click(object sender, EventArgs e)
        {
            // 1. Validasi: Pastikan user sudah memilih salah satu turnamen di DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih turnamen yang ingin diikuti pada tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Ambil data kompetisi dari baris DataGridView yang sedang dipilih/diklik oleh user
                var rowTerpilih = dataGridView1.SelectedRows[0];
                int idKompetisi = Convert.ToInt32(rowTerpilih.Cells["id_kompetisi"].Value);
                int hargaPendaftaran = Convert.ToInt32(rowTerpilih.Cells["harga_pendaftaran"].Value);
                string namaKompetisi = Convert.ToString(rowTerpilih.Cells["nama_kompetisi"].Value);

                // 3. Ambil ID User asli yang sedang aktif login dari sesi userLogin
                // Catatan: Jika di class User property ID-mu menggunakan huruf besar (PascalCase), ganti .id menjadi .Id
                int idUserLogin = this.userLogin.IdUser;

                // 4. LEMPAR DATA KE MENU PEMBAYARAN
                MenuPembayaran bayarForm = new MenuPembayaran(this.userLogin, idKompetisi, namaKompetisi, hargaPendaftaran);
                bayarForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memproses pendaftaran: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================================
        // NAVIGASI LINK LABEL SIKLUS MENU PEMAIN
        // =======================================================================
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuProfilPem profilForm = new MenuProfilPem(this.userLogin);
            profilForm.Show();
            this.Close();
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Kamu sudah berada di halaman List Tournament.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuHistoryPermainan historyForm = new MenuHistoryPermainan(this.userLogin);
            historyForm.Show();
            this.Close();
        }

        private void linkLabel6_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuAturan peraturanForm = new MenuAturan(this.userLogin);
            peraturanForm.Show();
            this.Close();
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
    }
}