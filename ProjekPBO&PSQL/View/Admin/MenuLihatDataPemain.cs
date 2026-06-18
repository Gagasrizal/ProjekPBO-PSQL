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
        private AkunUser? adminLogin;
        private int _idKompetisiTerpilih = 0;

        public MenuLihatDataPemain()
        {
            InitializeComponent();
        }

        public MenuLihatDataPemain(AkunUser user) : this()
        {
            this.adminLogin = user;
            this.Load += MenuLihatDataPemain_Load;
        }

        public MenuLihatDataPemain(AkunUser user, int idKompetisi) : this()
        {
            this.adminLogin = user;
            this._idKompetisiTerpilih = idKompetisi;
            this.Load += MenuLihatDataPemain_Load;
        }
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

        private void LoadDataTournamentToComboBox()
        {
            try
            {
                KompetisiContext kompetisiContext = new KompetisiContext();
                DataTable dt = kompetisiContext.AmbilSemuaKompetisi();

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tidak ada data turnamen di database!", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "nama_kompetisi";
                comboBox1.ValueMember = "id_kompetisi";

                if (_idKompetisiTerpilih > 0)
                {
                    comboBox1.SelectedValue = _idKompetisiTerpilih;
                }

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

        private void TampilkanDataPendaftar()
        {
            try
            {
                if (comboBox1.SelectedValue == null) return;
                if (!int.TryParse(comboBox1.SelectedValue.ToString(), out int idKompetisi)) return;

                KompetisiContext kompetisiContext = new KompetisiContext();
                DataTable dt = kompetisiContext.AmbilPendaftarBerdasarkanTournament(idKompetisi);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;

                if (dt != null && dataGridView1.Columns.Count >= 5)
                {
 
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    dataGridView1.ScrollBars = ScrollBars.Both;

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

                PertandinganContext pertandinganContext = new PertandinganContext();
                DataTable dt = pertandinganContext.AmbilPertandinganPerBabak(idKompetisi, babak);

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
        //private void MulaiSinkronisasiBabakDanData()
        //{
        //    if (comboBox1.SelectedValue == null || comboBox1.SelectedIndex == -1) return;

        //    if (int.TryParse(comboBox1.SelectedValue.ToString(), out int idKompetisi))
        //    {
        //        KompetisiContext kompetisiContext = new KompetisiContext();
        //        int totalBabak = kompetisiContext.AmbilTotalBabakTournament(idKompetisi);

        //        comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
        //        comboBox2.Items.Clear();

        //        for (int i = 1; i <= totalBabak; i++)
        //        {
        //            comboBox2.Items.Add($"Babak {i}");
        //        }

        //        if (comboBox2.Items.Count > 0)
        //            comboBox2.SelectedIndex = 0;

        //        comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        //    }
        //}
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TampilkanDataPendaftar();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TampilkanDataPertandingan();
        }

        //private void roundedButton3_Click(object sender, EventArgs e)
        //{
        //    if (comboBox1.SelectedValue == null || comboBox2.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Pilih turnamen dan babak terlebih dahulu!", "Peringatan",
        //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    int idKompetisi = Convert.ToInt32(comboBox1.SelectedValue);
        //    string babakRaw = comboBox2.SelectedItem.ToString().Replace("Babak ", "").Trim();
        //    int babakTerpilih = Convert.ToInt32(babakRaw);

        //    KompetisiContext kompetisiContext = new KompetisiContext();

        //    if (kompetisiContext.IsBabakSudahGenerated(idKompetisi, babakTerpilih))
        //    {
        //        MessageBox.Show($"Pertandingan Babak {babakTerpilih} sudah pernah di-generate!", "Informasi",
        //            MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }

        //    List<int> listPemain = kompetisiContext.AmbilPemainTerdaftar(idKompetisi);
        //    if (listPemain.Count < 2)
        //    {
        //        MessageBox.Show("Jumlah pemain tidak mencukupi (Minimal 2 pemain).", "Peringatan",
        //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    Random rng = new Random();
        //    int n = listPemain.Count;
        //    while (n > 1)
        //    {
        //        n--;
        //        int k = rng.Next(n + 1);
        //        int value = listPemain[k];
        //        listPemain[k] = listPemain[n];
        //        listPemain[n] = value;
        //    }

        //    List<Tuple<int, int>> pasanganMatch = new List<Tuple<int, int>>();
        //    for (int i = 0; i < listPemain.Count - 1; i += 2)
        //    {
        //        pasanganMatch.Add(new Tuple<int, int>(listPemain[i], listPemain[i + 1]));
        //    }

        //    if (listPemain.Count % 2 != 0)
        //    {
        //        MessageBox.Show(
        //            $"Jumlah pemain ganjil ({listPemain.Count} orang).\n" +
        //            $"1 pemain akan mendapat BYE (tidak dapat lawan) dan otomatis menang di babak ini.",
        //            "Info BYE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //    if (kompetisiContext.SimpanPertandinganGenerate(idKompetisi, babakTerpilih, pasanganMatch))
        //    {
        //        MessageBox.Show($"Berhasil generate pasangan pertandingan Babak {babakTerpilih}!", "Sukses",
        //            MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        MulaiSinkronisasiBabakDanData();
        //        TampilkanDataPertandingan();
        //    }
        //}

        //private void roundedButton2_Click(object sender, EventArgs e)
        //{
        //    if (comboBox1.SelectedValue == null || comboBox2.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Pilih turnamen dan babak aktif terlebih dahulu!", "Informasi",
        //            MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }

        //    int idKompetisi = Convert.ToInt32(comboBox1.SelectedValue);
        //    string babakRaw = comboBox2.SelectedItem.ToString().Replace("Babak ", "").Trim();
        //    int babakAktif = Convert.ToInt32(babakRaw);

        //    PertandinganContext pertandinganContext = new PertandinganContext();
        //    bool isSelesaiSemua = pertandinganContext.ApakahSemuaPertandinganSelesai(idKompetisi, babakAktif);

        //    if (isSelesaiSemua)
        //    {
        //        MessageBox.Show(
        //            "Validasi sukses! Seluruh pertandingan di babak ini telah diisi.\nMengalihkan ke Halaman Leaderboard.",
        //            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show(
        //            $"Akses ditolak! Masih ada pertandingan di Babak {babakAktif} yang belum diisi hasilnya.",
        //            "Gagal Rekap Juara", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //}

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
            MenuPertandingan formPertandingan = new MenuPertandingan(adminLogin); 

            formPertandingan.Show();

            this.Hide();
        }

        //private void roundedButton4_Click(object sender, EventArgs e)
        //{

        //}
    }
}