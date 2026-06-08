using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class MenuLihatDataPemain : Form
    {
        private User? adminLogin;
        private int _idKompetisiTerpilih = 0;

        // Konstruktor bawaan (JANGAN DIHAPUS/DIUBAH supaya GUI Designer tidak crash)
        public MenuLihatDataPemain()
        {
            InitializeComponent();
        }

        // Konstruktor Overload kalau kamu melempar data User Admin dari Form Login/Menu Utama
        public MenuLihatDataPemain(User user) : this()
        {
            this.adminLogin = user;
        }

        // Konstruktor Overload kalau dipanggil dari Menu Lihat Tournament sambil bawa ID Kompetisi
        public MenuLihatDataPemain(User user, int idKompetisi) : this()
        {
            this.adminLogin = user;
            this._idKompetisiTerpilih = idKompetisi;
        }

        private void MenuLihatDataPemain_Load(object sender, EventArgs e)
        {
            // Ambil data turnamen untuk dimasukkan ke comboBox1 saat form pertama kali dibuka
            LoadTurnamenToComboBox();

            // Jika form ini dibuka karena operan/klik dari form turnamen sebelumnya
            if (_idKompetisiTerpilih > 0)
            {
                comboBox1.SelectedValue = _idKompetisiTerpilih;
                MulaiSinkronisasiBabakDanData();
            }
        }

        private void LoadTurnamenToComboBox()
        {
            DBHelper db = new DBHelper();
            DataTable dt = db.AmbilIdDanNamaTournament();

            if (dt != null && dt.Rows.Count > 0)
            {
                // Putus event sementara agar tidak memicu loop/error saat data diikat
                comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "nama_kompetisi";
                comboBox1.ValueMember = "id_kompetisi";
                comboBox1.SelectedIndex = -1; // Default awal kosong

                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //untuk melihat data profil admin yang sedang login
        {
            if (adminLogin != null)
            {
                MenuProfilAdmin profil = new MenuProfilAdmin(adminLogin);
                profil.Show();
                this.Hide();
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //untuk melihat data turnamen yang sudah terdaftar di sistem
        {
            if (adminLogin != null)
            {
                LihatDataTournament tournament = new LihatDataTournament(adminLogin);
                tournament.Show();
                this.Hide();
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //untuk melihat data pembayaran pemain yang sudah terdaftar di turnamen
        {
            if (adminLogin != null)
            {
                LihatDataPembayaran pembayaran = new LihatDataPembayaran(adminLogin);
                pembayaran.Show();
                this.Hide();
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e) // tombol logout untuk keluar dari halaman admin dan kembali ke halaman login
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //untuk memilih turnamen yang ingin dilihat datanya
        {
            MulaiSinkronisasiBabakDanData();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//untuk memilih babak yang ingin dilihat datanya
        {
            // Setiap kali admin mengganti babak, perbarui isi DataGridView pertandingan
            RefreshTabelPertandingan();
        }

        private void MulaiSinkronisasiBabakDanData()
        {
            if (comboBox1.SelectedValue == null || comboBox1.SelectedIndex == -1) return;

            if (int.TryParse(comboBox1.SelectedValue.ToString(), out int idKompetisi))
            {
                DBHelper db = new DBHelper();
                int totalBabak = db.AmbilTotalBabakTournament(idKompetisi);

                comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
                comboBox2.Items.Clear();

                for (int i = 1; i <= totalBabak; i++)
                {
                    comboBox2.Items.Add($"Babak {i}");
                }

                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = 0; // Default otomatis ke Babak 1
                }

                comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
                RefreshTabelPertandingan();
            }
        }

        private void RefreshTabelPertandingan()
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedIndex == -1)
            {
                dataGridView1.DataSource = null;
                return;
            }

            int idKompetisi = Convert.ToInt32(comboBox1.SelectedValue);
            int babakAktif = comboBox2.SelectedIndex + 1;

            DBHelper db = new DBHelper();
            dataGridView1.AutoGenerateColumns = true;

            // Mengambil pertandingan berdasarkan turnamen dan babak yang dipilih
            DataTable dt = db.AmbilPertandinganPerBabak(idKompetisi, babakAktif);
            dataGridView1.DataSource = dt;
        }

        private void roundedButton3_Click(object sender, EventArgs e) //generate sistem acak untuk menentukan siapa lawan siapa di babak selanjutnya
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih turnamen dan babak terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idKompetisi = Convert.ToInt32(comboBox1.SelectedValue);
            int babakTerpilih = comboBox2.SelectedIndex + 1;

            DBHelper db = new DBHelper();

            if (db.IsBabakSudahGenerated(idKompetisi, babakTerpilih))
            {
                MessageBox.Show($"Pertandingan untuk Babak {babakTerpilih} sudah pernah di-generate!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<int> listPemain = db.AmbilPemainTerdaftar(idKompetisi);
            if (listPemain.Count < 2)
            {
                MessageBox.Show("Jumlah pemain yang terdaftar tidak mencukupi untuk membuat pertandingan (Minimal 2).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Algoritma Acak Lawan (Matchmaking)
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

            List<Tuple<int, int>> pasanganMatch = new List<Tuple<int, int>>();
            for (int i = 0; i < listPemain.Count - 1; i += 2)
            {
                pasanganMatch.Add(new Tuple<int, int>(listPemain[i], listPemain[i + 1]));
            }

            if (db.SimpanPertandinganGenerate(idKompetisi, babakTerpilih, pasanganMatch))
            {
                MessageBox.Show($"Berhasil mengacak pasangan pertandingan untuk Babak {babakTerpilih}!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshTabelPertandingan();
            }
        }

        private void roundedButton2_Click(object sender, EventArgs e) //hasil keseluran poin dari seluruh babak pemain untuk menentukan juara
        {
            // Membuka ulang/memperbarui data klasemen poin terbaru di tabel
            RefreshTabelPertandingan();
            MessageBox.Show("Menampilkan pembaharuan total poin seluruh babak.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) //untuk menampilkan data pemain yang sudah terdaftar di turnamen berdasarkan pilihan turnamen dan babak yang dipilih
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (row.Cells["ID Match"].Value == null) return;

            int idPertandingan = Convert.ToInt32(row.Cells["ID Match"].Value);
            string pemainPutih = row.Cells["Pemain Putih"].Value?.ToString() ?? "Pemain 1";
            string pemainHitam = row.Cells["Pemain Hitam"].Value?.ToString() ?? "Pemain 2";

            // Tampilkan popup input menang/kalah catur
            Form promptSkor = new Form()
            {
                Width = 450,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Input Hasil Match",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lbl = new Label() { Left = 20, Top = 20, Width = 400, Text = $"{pemainPutih} (PUTIH) vs {pemainHitam} (HITAM)", Font = new Font(this.Font, FontStyle.Bold) };
            Button btnP1 = new Button() { Text = "Putih Menang", Left = 20, Width = 110, Top = 60, DialogResult = DialogResult.Yes };
            Button btnP2 = new Button() { Text = "Hitam Menang", Left = 150, Width = 110, Top = 60, DialogResult = DialogResult.No };
            Button btnSeri = new Button() { Text = "Remis (Seri)", Left = 280, Width = 110, Top = 60, DialogResult = DialogResult.Cancel };

            promptSkor.Controls.AddRange(new Control[] { lbl, btnP1, btnP2, btnSeri });
            DialogResult res = promptSkor.ShowDialog();

            decimal skorPutih = 0.0m, skorHitam = 0.0m;
            string hasilStr = "";

            if (res == DialogResult.Yes) { skorPutih = 1.0m; hasilStr = "Putih Menang"; }
            else if (res == DialogResult.No) { skorHitam = 1.0m; hasilStr = "Hitam Menang"; }
            else if (res == DialogResult.Cancel) { skorPutih = 0.5m; skorHitam = 0.5m; hasilStr = "Remis"; }
            else return;

            DBHelper db = new DBHelper();
            if (db.UpdateSkorPertandingan(idPertandingan, skorPutih, skorHitam, hasilStr))
            {
                MessageBox.Show($"Hasil tercatat: {hasilStr}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshTabelPertandingan();
            }
        }
    }
}