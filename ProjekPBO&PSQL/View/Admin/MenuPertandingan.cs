using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Models.Context;

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class MenuPertandingan : Form
    {
        private AkunUser adminLogin;
        private int _idKompetisi = 0;
        private int _babakAktif = 1;
        private string _namaTournament = "";

        // Constructor Utama - Digunakan oleh semua Form Admin untuk berpindah halaman tanpa kehilangan session
        public MenuPertandingan(AkunUser user)
        {
            InitializeComponent();
            this.adminLogin = user;
            this.Load += MenuPertandingan_Load;
        }

        // Overload Constructor jika Anda ingin membuka form langsung menargetkan turnamen & babak tertentu
        public MenuPertandingan(AkunUser user, int idKompetisi, int babakAktif, string namaTournament) : this(user)
        {
            this._idKompetisi = idKompetisi;
            this._babakAktif = babakAktif;
            this._namaTournament = namaTournament;
        }

        private void MenuPertandingan_Load(object sender, EventArgs e)
        {
            IsiComboBoxHasil();
            LoadTournamentKeComboBox();
            SinkronisasiBabak();
        }

        private void IsiComboBoxHasil()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("1-0");
            comboBox3.Items.Add("0-1");
            comboBox3.Items.Add("1/2-1/2");
            comboBox3.SelectedIndex = -1;
        }

        private void LoadTournamentKeComboBox()
        {
            try
            {
                KompetisiContext kompetisiCtx = new KompetisiContext();
                DataTable dt = kompetisiCtx.AmbilSemuaKompetisi();

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

        private void SinkronisasiBabak()
        {
            if (comboBox1.SelectedValue == null) return;
            if (!int.TryParse(comboBox1.SelectedValue.ToString(), out int idKompetisi)) return;

            KompetisiContext kompetisiCtx = new KompetisiContext();
            int totalBabak = kompetisiCtx.AmbilTotalBabakTournament(idKompetisi);

            comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
            comboBox2.Items.Clear();

            for (int i = 1; i <= totalBabak; i++)
                comboBox2.Items.Add($"Babak {i}");

            int indexBabak = _babakAktif - 1;
            comboBox2.SelectedIndex = (indexBabak >= 0 && indexBabak < comboBox2.Items.Count) ? indexBabak : 0;

            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        }

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

                // FIKS: Menggunakan PertandinganContext karena method ini ada di PertandinganContext
                PertandinganContext pertandinganCtx = new PertandinganContext();
                DataTable dtRaw = pertandinganCtx.AmbilPertandinganDenganTotalPoin(idKompetisi, babak);

                DataTable dt = new DataTable();
                dt.Columns.Add("id_pertandingan", typeof(int));
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
                    dataGridView1.Columns[0].Visible = false; // Hidden ID

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SinkronisasiBabak();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Dikocok / diganti manual lewat klik button tampilkan data
        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {
            TampilkanDataPertandingan();
        }

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

            KompetisiContext kompetisiCtx = new KompetisiContext();

            if (kompetisiCtx.IsBabakSudahGenerated(idKompetisi, babakTerpilih))
            {
                MessageBox.Show($"Babak {babakTerpilih} sudah pernah di-generate!", "Informasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                TampilkanDataPertandingan();
                return;
            }

            List<int> listPemain = kompetisiCtx.AmbilPemainTerdaftar(idKompetisi);
            if (listPemain.Count < 2)
            {
                MessageBox.Show("Jumlah pemain tidak mencukupi (minimal 2).", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Fisher-Yates Shuffle Matchmaking
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

            if (kompetisiCtx.SimpanPertandinganGenerate(idKompetisi, babakTerpilih, pasangan))
            {
                MessageBox.Show($"Berhasil generate pasangan Babak {babakTerpilih}!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                TampilkanDataPertandingan();
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

                // FIKS: Menggunakan PertandinganContext
                PertandinganContext pertandinganCtx = new PertandinganContext();
                KompetisiContext kompetisiCtx = new KompetisiContext();

                if (pertandinganCtx.UpdateHasilPertandingan(idPertandingan, hasil))
                {
                    var eloInfo = pertandinganCtx.AmbilEloSetelahUpdate(idPertandingan);

                    MessageBox.Show(
                        $"Hasil berhasil disimpan: {hasil}\n\n" +
                        $"ELO terupdate (via Trigger):\n" +
                        $"• {eloInfo.NamaPutih}: {eloInfo.EloPutih}\n" +
                        $"• {eloInfo.NamaHitam}: {eloInfo.EloHitam}",
                        "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TampilkanDataPertandingan();

                    int idKompetisi = Convert.ToInt32(comboBox1.SelectedValue);
                    int totalBabak = kompetisiCtx.AmbilTotalBabakTournament(idKompetisi);

                    int babakSekarang = 1;
                    if (comboBox2.SelectedItem != null)
                    {
                        string teks = comboBox2.SelectedItem.ToString().Replace("Babak ", "").Trim();
                        int.TryParse(teks, out babakSekarang);
                    }

                    if (babakSekarang == totalBabak && kompetisiCtx.ApakahSemuaPertandinganSelesai(idKompetisi, babakSekarang))
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

        private void TampilkanLeaderboard(int idKompetisi)
        {
            try
            {
                KompetisiContext kompetisiCtx = new KompetisiContext();
                DataTable dt = kompetisiCtx.AmbilLeaderboardTournament(idKompetisi);

                MessageBox.Show("🏆 Semua babak telah selesai!\nBerikut adalah Leaderboard akhir tournament.",
                    "Tournament Selesai!", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                    dataGridView1.ColumnHeadersHeight = 36;
                    dataGridView1.RowTemplate.Height = 38;
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);

                    dataGridView1.RowPrePaint -= DataGridView1_RowPrePaint;
                    dataGridView1.RowPrePaint += DataGridView1_RowPrePaint;

                    dataGridView1.Refresh();
                }

                comboBox3.Enabled = false;
                comboBox2.Enabled = false;
                roundedButton2.Enabled = false;
                roundedButton3.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menampilkan leaderboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                object val = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                if (val == null) return;

                int peringkat = Convert.ToInt32(val);

                switch (peringkat)
                {
                    case 1:
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 215, 0);
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case 2:
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 192);
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case 3:
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(205, 127, 50);
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                }
            }
            catch { }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuProfilAdmin profil = new MenuProfilAdmin(adminLogin);
            profil.Show();
            this.Hide();
        }

        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LihatDataTournament tournament = new LihatDataTournament(adminLogin);
            tournament.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LihatDataPembayaran pembayaran = new LihatDataPembayaran(adminLogin);
            pembayaran.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuPertandingan formPertandingan = new MenuPertandingan(adminLogin);

            // 2. Tampilkan form tujuan
            formPertandingan.Show();

            // 3. Sembunyikan form yang sedang aktif (opsional, agar tidak menumpuk)
            this.Hide();
        }

        private void roundedButton1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah kamu yakin ingin keluar?", "LogOut Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                string hasilSaatIni = dataGridView1.Rows[e.RowIndex].Cells[4].Value?.ToString();

                if (!string.IsNullOrEmpty(hasilSaatIni) && hasilSaatIni != "Belum Dimainkan" && comboBox3.Items.Contains(hasilSaatIni))
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
    }
}