using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class MenuPertandingan : Form
    {
        private User adminLogin;
        private int _idKompetisi;
        private int _babakAktif = 1;
        private string _namaTournament;

        // Konstruktor bawaan
        public MenuPertandingan()
        {
            InitializeComponent();
            this.Load += MenuPertandingan_Load; // ← Load event di sini agar selalu terpasang
        }

        // Konstruktor yang dipanggil dari form lain
        public MenuPertandingan(User user, int idKompetisi, int babakAktif, string namaTournament) : this()
        {
            this.adminLogin = user;
            this._idKompetisi = idKompetisi;
            this._babakAktif = babakAktif;
            this._namaTournament = namaTournament;
        }

        // =======================================================================
        // FORM LOAD
        // =======================================================================
        private void MenuPertandingan_Load(object sender, EventArgs e)
        {
            IsiComboBoxHasil();
            LoadTournamentKeComboBox();
            SinkronisasiBabak();
            // DataGridView sengaja kosong dulu — user klik Lihat baru tampil
        }

        // =======================================================================
        // ISI comboBox3 — Input Point / Hasil
        // =======================================================================
        private void IsiComboBoxHasil()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("1-0");
            comboBox3.Items.Add("0-1");
            comboBox3.Items.Add("1/2-1/2");
            comboBox3.SelectedIndex = -1;
        }

        // =======================================================================
        // LOAD comboBox1 — Pilih Tournament
        // =======================================================================
        private void LoadTournamentKeComboBox()
        {
            try
            {
                DBHelper db = new DBHelper();
                DataTable dt = db.AmbilSemuaKompetisi();

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tidak ada data tournament di database!");
                    return;
                }

                comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "nama_kompetisi";
                comboBox1.ValueMember = "id_kompetisi";

                if (_idKompetisi > 0)
                    comboBox1.SelectedValue = _idKompetisi;

                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal load tournament: {ex.Message}");
            }
        }

        // =======================================================================
        // SINKRONISASI comboBox2 — Pilih Babak
        // =======================================================================
        private void SinkronisasiBabak()
        {
            if (comboBox1.SelectedValue == null) return;
            if (!int.TryParse(comboBox1.SelectedValue.ToString(), out int idKompetisi)) return;

            DBHelper db = new DBHelper();
            int totalBabak = db.AmbilTotalBabakTournament(idKompetisi);

            comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
            comboBox2.Items.Clear();

            for (int i = 1; i <= totalBabak; i++)
                comboBox2.Items.Add($"Babak {i}");

            int indexBabak = _babakAktif - 1;
            comboBox2.SelectedIndex = (indexBabak >= 0 && indexBabak < comboBox2.Items.Count) ? indexBabak : 0;

            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        }

        // =======================================================================
        // TAMPILKAN DATA PERTANDINGAN
        // Kolom: [hidden id] | Pemain Hitam (poin) | VS | Pemain Putih (poin) | Hasil
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
                    string teks = comboBox2.SelectedItem.ToString().Replace("Babak ", "").Trim();
                    int.TryParse(teks, out babak);
                }

                DBHelper db = new DBHelper();
                DataTable dtRaw = db.AmbilPertandinganDenganTotalPoin(idKompetisi, babak);

                // Susun ulang kolom sesuai tampilan yang diinginkan
                DataTable dt = new DataTable();
                dt.Columns.Add("id_pertandingan", typeof(int));    // hidden, untuk simpan hasil
                dt.Columns.Add("Pemain Hitam", typeof(string));
                dt.Columns.Add("VS", typeof(string));
                dt.Columns.Add("Pemain Putih", typeof(string));
                dt.Columns.Add("Hasil Babak ini", typeof(string));

                foreach (DataRow raw in dtRaw.Rows)
                {
                    dt.Rows.Add(
                        raw["id_pertandingan"],
                        raw["pemain_hitam_label"],
                        "VS",
                        raw["pemain_putih_label"],
                        raw["hasil"]
                    );
                }

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.ScrollBars = ScrollBars.Both;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.MultiSelect = false;

                if (dataGridView1.Columns.Count >= 5)
                {
                    // Kolom 0: id — sembunyikan, tetap dipakai untuk ambil id saat simpan hasil
                    dataGridView1.Columns[0].Visible = false;

                    dataGridView1.Columns[1].HeaderText = "Pemain Hitam";
                    dataGridView1.Columns[1].Width = 210;

                    dataGridView1.Columns[2].HeaderText = "";
                    dataGridView1.Columns[2].Width = 40;
                    dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[2].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    dataGridView1.Columns[2].DefaultCellStyle.ForeColor = Color.LimeGreen;

                    dataGridView1.Columns[3].HeaderText = "Pemain Putih";
                    dataGridView1.Columns[3].Width = 210;

                    dataGridView1.Columns[4].HeaderText = "Hasil Babak ini";
                    dataGridView1.Columns[4].Width = 130;
                    dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Style baris
                    dataGridView1.ColumnHeadersHeight = 36;
                    dataGridView1.RowTemplate.Height = 38;
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);

                    dataGridView1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menampilkan pertandingan: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================================
        // comboBox1 — Ganti Tournament → update babak saja, tidak auto-refresh grid
        // =======================================================================
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SinkronisasiBabak();
        }

        // =======================================================================
        // comboBox2 — Ganti Babak → tidak auto-refresh, user klik Lihat sendiri
        // =======================================================================
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Sengaja kosong
        }

        // =======================================================================
        // roundedButton4 — Tombol LIHAT
        // =======================================================================
        private void roundedButton4_Click(object sender, EventArgs e)
        {
            TampilkanDataPertandingan();
        }

        // =======================================================================
        // roundedButton3 — Tombol MATCHMAKING
        // =======================================================================
        private void roundedButton3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih tournament dan babak terlebih dahulu!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idKompetisi = Convert.ToInt32(comboBox1.SelectedValue);
            string babakRaw = comboBox2.SelectedItem.ToString().Replace("Babak ", "").Trim();
            int babakTerpilih = Convert.ToInt32(babakRaw);

            DBHelper db = new DBHelper();

            if (db.IsBabakSudahGenerated(idKompetisi, babakTerpilih))
            {
                MessageBox.Show($"Babak {babakTerpilih} sudah pernah di-generate!", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                TampilkanDataPertandingan();
                return;
            }

            List<int> listPemain = db.AmbilPemainTerdaftar(idKompetisi);
            if (listPemain.Count < 2)
            {
                MessageBox.Show("Jumlah pemain tidak mencukupi (minimal 2).", "Peringatan",
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
                int val = listPemain[k];
                listPemain[k] = listPemain[n];
                listPemain[n] = val;
            }

            List<Tuple<int, int>> pasangan = new List<Tuple<int, int>>();
            for (int i = 0; i < listPemain.Count - 1; i += 2)
                pasangan.Add(new Tuple<int, int>(listPemain[i], listPemain[i + 1]));

            if (listPemain.Count % 2 != 0)
                MessageBox.Show($"Jumlah pemain ganjil ({listPemain.Count}). 1 pemain mendapat BYE.",
                    "Info BYE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (db.SimpanPertandinganGenerate(idKompetisi, babakTerpilih, pasangan))
            {
                MessageBox.Show($"Berhasil generate pasangan Babak {babakTerpilih}!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                TampilkanDataPertandingan();
            }
        }

        // =======================================================================
        // roundedButton2 — Tombol HASIL (simpan skor)
        // =======================================================================
        private void DataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                // Kolom 0 = Peringkat
                object val = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                if (val == null) return;

                int peringkat = Convert.ToInt32(val);

                switch (peringkat)
                {
                    case 1:
                        // Emas
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 215, 0);
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case 2:
                        // Perak
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 192);
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case 3:
                        // Perunggu
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(205, 127, 50);
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                }
            }
            catch { }
        }
        private void TampilkanLeaderboard(int idKompetisi) //group by
        {
            try
            {
                DBHelper db = new DBHelper();
                DataTable dt = db.AmbilLeaderboardTournament(idKompetisi);

                // Kasih tahu user bahwa tournament selesai
                MessageBox.Show("🏆 Semua babak telah selesai!\nBerikut adalah Leaderboard akhir tournament.",
                    "Tournament Selesai!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tampilkan leaderboard di DataGridView yang sama
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.MultiSelect = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                if (dt != null && dataGridView1.Columns.Count >= 4)
                {
                    dataGridView1.Columns[0].HeaderText = "Peringkat";
                    dataGridView1.Columns[0].Width = 80;
                    dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Columns[1].HeaderText = "Nama Pemain";
                    dataGridView1.Columns[1].Width = 220;

                    dataGridView1.Columns[2].HeaderText = "Asal Negara";
                    dataGridView1.Columns[2].Width = 130;

                    dataGridView1.Columns[3].HeaderText = "Total Poin";
                    dataGridView1.Columns[3].Width = 100;
                    dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Style header
                    dataGridView1.ColumnHeadersHeight = 36;
                    dataGridView1.RowTemplate.Height = 38;
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);

                    // Warna khusus baris peringkat 1, 2, 3
                    dataGridView1.RowPrePaint -= DataGridView1_RowPrePaint; // hindari double subscribe
                    dataGridView1.RowPrePaint += DataGridView1_RowPrePaint;

                    dataGridView1.Refresh();
                }

                // Nonaktifkan tombol yang tidak relevan saat leaderboard tampil
                comboBox3.Enabled = false;
                comboBox2.Enabled = false;
                roundedButton2.Enabled = false; // tombol Hasil
                roundedButton3.Enabled = false; // tombol Matchmaking
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menampilkan leaderboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void roundedButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris pertandingan terlebih dahulu!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox3.SelectedIndex == -1 || comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Pilih hasil di dropdown Input Point terlebih dahulu!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idPertandingan = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string hasil = comboBox3.SelectedItem.ToString();

                DBHelper db = new DBHelper();
                if (db.UpdateHasilPertandingan(idPertandingan, hasil))
                {
                    var eloInfo = db.AmbilEloSetelahUpdate(idPertandingan);

                    MessageBox.Show(
                        $"Hasil berhasil disimpan: {hasil}\n\n" +
                        $"ELO terupdate (via Trigger):\n" +
                        $"• {eloInfo.NamaPutih}: {eloInfo.EloPutih}\n" +
                        $"• {eloInfo.NamaHitam}: {eloInfo.EloHitam}",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TampilkanDataPertandingan();

                    // =====================================================
                    // CEK APAKAH BABAK TERAKHIR DAN SEMUA SUDAH SELESAI
                    // =====================================================
                    int idKompetisi = Convert.ToInt32(comboBox1.SelectedValue);
                    int totalBabak = db.AmbilTotalBabakTournament(idKompetisi);

                    // Ambil babak sekarang dari comboBox2
                    int babakSekarang = 1;
                    if (comboBox2.SelectedItem != null)
                    {
                        string teks = comboBox2.SelectedItem.ToString().Replace("Babak ", "").Trim();
                        int.TryParse(teks, out babakSekarang);
                    }

                    // Kalau ini babak terakhir DAN semua pertandingan sudah selesai
                    if (babakSekarang == totalBabak &&
                        db.ApakahSemuaPertandinganSelesai(idKompetisi, babakSekarang))
                    {
                        TampilkanLeaderboard(idKompetisi);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menyimpan hasil: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                // Cells[4] = kolom "Hasil Babak ini"
                string hasilSaatIni = dataGridView1.Rows[e.RowIndex].Cells[4].Value?.ToString();

                if (!string.IsNullOrEmpty(hasilSaatIni)
                    && hasilSaatIni != "Belum Dimainkan"
                    && comboBox3.Items.Contains(hasilSaatIni))
                {
                    comboBox3.SelectedItem = hasilSaatIni;
                }
                else
                {
                    comboBox3.SelectedIndex = -1;
                }
            }
            catch { }
        }

        // =======================================================================
        // SIDEBAR NAVIGATION
        // =======================================================================
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                MenuProfilAdmin profil = new MenuProfilAdmin(adminLogin);
                profil.Show();
                this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                MenuLihatDataPemain lihatPemain = new MenuLihatDataPemain(adminLogin);
                lihatPemain.Show();
                this.Hide();
        }


        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                LihatDataTournament tournament = new LihatDataTournament(adminLogin);
                tournament.Show();
                this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                LihatDataPembayaran pembayaran = new LihatDataPembayaran(adminLogin);
                pembayaran.Show();
                this.Hide();
        }

        // =======================================================================
        // LOGOUT
        // =======================================================================
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Apakah kamu yakin ingin keluar?",
                "LogOut Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }
        }
    }
}