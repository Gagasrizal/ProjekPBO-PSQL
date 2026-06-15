using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Models.Context;

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class MenuLihatDataPemain : Form
    {
        private User? adminLogin;
        private int _idKompetisiTerpilih = 0;

        // Konstruktor bawaan — JANGAN DIHAPUS supaya GUI Designer tidak crash
        public MenuLihatDataPemain()
        {
            InitializeComponent();
        }

        // Konstruktor tanpa ID kompetisi
        public MenuLihatDataPemain(User user) : this()
        {
            this.adminLogin = user;
            this.Load += MenuLihatDataPemain_Load;
        }

        // Konstruktor dengan ID kompetisi — dipanggil dari LihatDataTournament
        public MenuLihatDataPemain(User user, int idKompetisi) : this()
        {
            this.adminLogin = user;
            this._idKompetisiTerpilih = idKompetisi;
            this.Load += MenuLihatDataPemain_Load;
        }

        // =======================================================================
        // FORM LOAD
        // =======================================================================
        private void MenuLihatDataPemain_Load(object sender, EventArgs e)
        {
            LoadDataTournamentToComboBox();
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Enabled = true;

            LoadDataTournamentToComboBox();
        }

        // =======================================================================
        // LOAD COMBOBOX + PRE-SELECT TOURNAMENT
        // =======================================================================
        private void LoadDataTournamentToComboBox()
        {
            try
            {
                TurnamentContext turnamentContext = new TurnamentContext();
                DataTable dt = turnamentContext.AmbilSemuaKompetisi();

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tidak ada data turnamen di database!", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Matikan event dulu agar tidak trigger saat binding
                comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "nama_kompetisi";
                comboBox1.ValueMember = "id_kompetisi";

                // Pre-select tournament yang dikirim dari LihatDataTournament
                if (_idKompetisiTerpilih > 0)
                {
                    comboBox1.SelectedValue = _idKompetisiTerpilih;
                }

                // Aktifkan kembali event setelah binding selesai
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

                // Langsung tampilkan data pemain
                TampilkanDataPendaftar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat daftar turnamen: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================================
        // TAMPILKAN DATA PEMAIN YANG MENDAFTAR
        // =======================================================================
        private void TampilkanDataPendaftar()
        {
            try
            {
                if (comboBox1.SelectedValue == null) return;
                if (!int.TryParse(comboBox1.SelectedValue.ToString(), out int idKompetisi)) return;

                TurnamentContext turnamenContext = new TurnamentContext();
                DataTable dt = turnamenContext.AmbilPendaftarBerdasarkanTournament(idKompetisi);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;

                if (dt != null && dataGridView1.Columns.Count >= 5)
                {
                    // WAJIB: matikan AutoSizeColumns dulu sebelum set width manual
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    // WAJIB: aktifkan scrollbar horizontal
                    dataGridView1.ScrollBars = ScrollBars.Both;

                    // WAJIB: jangan biarkan kolom otomatis mengisi sisa ruang
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    dataGridView1.Columns[0].HeaderText = "ID Daftar";
                    dataGridView1.Columns[0].Width = 70;
                    dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Columns[1].HeaderText = "Nama Lengkap";
                    dataGridView1.Columns[1].Width = 180;

                    dataGridView1.Columns[2].HeaderText = "Negara";
                    dataGridView1.Columns[2].Width = 100;

                    dataGridView1.Columns[3].HeaderText = "ELO Rating";
                    dataGridView1.Columns[3].Width = 90;
                    dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Columns[4].HeaderText = "Status";
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menampilkan data pemain: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================================
        // TAMPILKAN DATA PERTANDINGAN PER BABAK
        // =======================================================================
        private void TampilkanDataPertandingan()
        {
            try
            {
                if (comboBox1.SelectedValue == null) return;
                if (!int.TryParse(comboBox1.SelectedValue.ToString(), out int idKompetisi)) return;

                int babak = 1;
                if (comboBox2.SelectedItem != null)
                {
                    string teksBabak = comboBox2.SelectedItem.ToString()
                        .Replace("Babak ", "").Replace("Babak", "").Trim();
                    int.TryParse(teksBabak, out babak);
                }

                TurnamentContext turnamentContext = new TurnamentContext();
                DataTable dt = turnamentContext.AmbilPertandinganPerBabak(idKompetisi, babak);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;

                if (dt != null && dataGridView1.Columns.Count >= 7)
                {
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dataGridView1.ScrollBars = ScrollBars.Both;

                    dataGridView1.Columns[0].HeaderText = "ID Match";
                    dataGridView1.Columns[0].Width = 70;
                    dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Columns[1].HeaderText = "Babak";
                    dataGridView1.Columns[1].Width = 60;
                    dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Columns[2].HeaderText = "Pemain Putih";
                    dataGridView1.Columns[2].Width = 160;

                    dataGridView1.Columns[3].HeaderText = "Pemain Hitam";
                    dataGridView1.Columns[3].Width = 160;

                    dataGridView1.Columns[4].HeaderText = "Skor Putih";
                    dataGridView1.Columns[4].Width = 90;
                    dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Columns[5].HeaderText = "Skor Hitam";
                    dataGridView1.Columns[5].Width = 90;
                    dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Columns[6].HeaderText = "Hasil Akhir";
                    dataGridView1.Columns[6].Width = 100;
                    dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error menampilkan pertandingan: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================================
        // SINKRONISASI DROPDOWN BABAK
        // =======================================================================
        private void MulaiSinkronisasiBabakDanData()
        {
            if (comboBox1.SelectedValue == null || comboBox1.SelectedIndex == -1) return;

            if (int.TryParse(comboBox1.SelectedValue.ToString(), out int idKompetisi))
            {
                TurnamentContext turnamentContext = new TurnamentContext();
                int totalBabak = turnamentContext.AmbilTotalBabakTournament(idKompetisi);

                comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
                comboBox2.Items.Clear();

                for (int i = 1; i <= totalBabak; i++)
                {
                    comboBox2.Items.Add($"Babak {i}");
                }

                if (comboBox2.Items.Count > 0)
                    comboBox2.SelectedIndex = 0;

                comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            }
        }

        // =======================================================================
        // COMBOBOX EVENTS
        // =======================================================================
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ganti tournament → tampilkan ulang data pemain
            TampilkanDataPendaftar();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ganti babak → tampilkan data pertandingan babak itu
            TampilkanDataPertandingan();
        }

        // =======================================================================
        // TOMBOL MATCHMAKING — Generate & acak pasangan
        // =======================================================================
        private void roundedButton3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih turnamen dan babak terlebih dahulu!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idKompetisi = Convert.ToInt32(comboBox1.SelectedValue);
            string babakRaw = comboBox2.SelectedItem.ToString().Replace("Babak ", "").Trim();
            int babakTerpilih = Convert.ToInt32(babakRaw);

            TurnamentContext turnamentContext = new TurnamentContext();
            Detail_UserContext detail_UserContext = new Detail_UserContext();

            if (turnamentContext.IsBabakSudahGenerated(idKompetisi, babakTerpilih))
            {
                MessageBox.Show($"Pertandingan Babak {babakTerpilih} sudah pernah di-generate!", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<int> listPemain = detail_UserContext.AmbilPemainTerdaftar(idKompetisi);
            if (listPemain.Count < 2)
            {
                MessageBox.Show("Jumlah pemain tidak mencukupi (Minimal 2 pemain).", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Fisher-Yates Shuffle
            Random rng = new Random();
            int n = listPemain.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = listPemain[k];
                listPemain[k] = listPemain[n];
                listPemain[n] = value;
            }

            // Pasangkan dua-dua
            List<Tuple<int, int>> pasanganMatch = new List<Tuple<int, int>>();
            for (int i = 0; i < listPemain.Count - 1; i += 2)
            {
                pasanganMatch.Add(new Tuple<int, int>(listPemain[i], listPemain[i + 1]));
            }

            // Notifikasi jika pemain ganjil — ada yang tidak dapat lawan (BYE)
            if (listPemain.Count % 2 != 0)
            {
                MessageBox.Show(
                    $"Jumlah pemain ganjil ({listPemain.Count} orang).\n" +
                    $"1 pemain akan mendapat BYE (tidak dapat lawan) dan otomatis menang di babak ini.",
                    "Info BYE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (turnamentContext.SimpanPertandinganGenerate(idKompetisi, babakTerpilih, pasanganMatch))
            {
                MessageBox.Show($"Berhasil generate pasangan pertandingan Babak {babakTerpilih}!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                MulaiSinkronisasiBabakDanData();
                TampilkanDataPertandingan();
            }
        }


        // =======================================================================
        // TOMBOL REKAP JUARA / CEK SEMUA PERTANDINGAN SELESAI
        // =======================================================================
        private void roundedButton2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih turnamen dan babak aktif terlebih dahulu!", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idKompetisi = Convert.ToInt32(comboBox1.SelectedValue);
            string babakRaw = comboBox2.SelectedItem.ToString().Replace("Babak ", "").Trim();
            int babakAktif = Convert.ToInt32(babakRaw);

            TurnamentContext turnamentContext = new TurnamentContext();
            bool isSelesaiSemua = turnamentContext.ApakahSemuaPertandinganSelesai(idKompetisi, babakAktif);

            if (isSelesaiSemua)
            {
                MessageBox.Show(
                    "Validasi sukses! Seluruh pertandingan di babak ini telah diisi.\nMengalihkan ke Halaman Leaderboard.",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Buka Leaderboard
                // MenuLeaderboard leaderboard = new MenuLeaderboard(adminLogin, idKompetisi);
                // leaderboard.Show();
                // this.Hide();
            }
            else
            {
                MessageBox.Show(
                    $"Akses ditolak! Masih ada pertandingan di Babak {babakAktif} yang belum diisi hasilnya.",
                    "Gagal Rekap Juara", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        // =======================================================================
        // SIDEBAR NAVIGATION
        // =======================================================================
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (adminLogin != null)
            {
                MenuProfilAdmin profil = new MenuProfilAdmin(adminLogin);
                profil.Show();
                this.Hide();
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (adminLogin != null)
            {
                LihatDataTournament tournament = new LihatDataTournament(adminLogin);
                tournament.Show();
                this.Hide();
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (adminLogin != null)
            {
                LihatDataPembayaran pembayaran = new LihatDataPembayaran(adminLogin);
                pembayaran.Show();
                this.Hide();
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Apakah kamu yakin ingin keluar dari halaman Admin Hyper Chess?",
                "LogOut Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Buat instance dari form tujuan
            MenuPertandingan formPertandingan = new MenuPertandingan();

            // 2. Tampilkan form tujuan
            formPertandingan.Show();

            // 3. Sembunyikan form yang sedang aktif (opsional, agar tidak menumpuk)
            this.Hide();
        }
    }
}