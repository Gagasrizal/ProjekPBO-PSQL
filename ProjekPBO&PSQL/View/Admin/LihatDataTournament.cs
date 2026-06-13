using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class LihatDataTournament : Form
    {
        private User adminLogin;

        public LihatDataTournament(User user)
        {
            InitializeComponent();
            this.adminLogin = user;

            this.Load += new System.EventHandler(this.LihatDataTournament_Load);

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        private void LihatDataTournament_Load(object sender, EventArgs e)
        {
            TampilkanDataTournament();
            LoadTurnamenToComboBox();
        }
        private int idKompetisiTerpilih = 0;

        private void TampilkanDataTournament()
        {
            DBHelper db = new DBHelper();
            dataGridView1.AutoGenerateColumns = true;
            DataTable dt = db.AmbilSemuaTournament();

            if (dt != null)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.ScrollBars = ScrollBars.Both;

                if (dataGridView1.Columns.Count >= 9)
                {
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[0].Width = 50;

                    dataGridView1.Columns[1].HeaderText = "Nama Turnamen";
                    dataGridView1.Columns[1].Width = 180;

                    dataGridView1.Columns[2].HeaderText = "Mode Pertandingan";
                    dataGridView1.Columns[2].Width = 120;

                    dataGridView1.Columns[3].HeaderText = "Biaya Pendaftaran";
                    dataGridView1.Columns[3].Width = 140;
                    dataGridView1.Columns[3].DefaultCellStyle.Format = "N0";

                    dataGridView1.Columns[4].HeaderText = "Batas Registrasi";
                    dataGridView1.Columns[4].Width = 130;

                    dataGridView1.Columns[5].HeaderText = "Tanggal Main";
                    dataGridView1.Columns[5].Width = 110;

                    dataGridView1.Columns[6].HeaderText = "Total Hadiah";
                    dataGridView1.Columns[6].Width = 130;
                    dataGridView1.Columns[6].DefaultCellStyle.Format = "N0";

                    dataGridView1.Columns[7].HeaderText = "Sistem Match";
                    dataGridView1.Columns[7].Width = 150;

                    dataGridView1.Columns[8].HeaderText = "Total Babak";
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dataGridView1.Refresh();
            }
        }

        private void LoadTurnamenToComboBox()
        {
            DBHelper db = new DBHelper();
            DataTable dt = db.AmbilIdDanNamaTournament();

            if (dt != null && dt.Rows.Count > 0)
            {
                comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "nama_kompetisi";
                comboBox1.ValueMember = "id_kompetisi";
                comboBox1.SelectedIndex = -1;

                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedValue == null)
                return;

            // Hanya simpan ID-nya saja, JANGAN langsung buka form baru di sini
            int.TryParse(comboBox1.SelectedValue.ToString(), out idKompetisiTerpilih);
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            MenuBuatTournament buatTournament = new MenuBuatTournament(this.adminLogin);
            buatTournament.Show();
            this.Hide();
        }

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

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah kamu yakin ingin keluar dari halaman Admin Hyper Chess?", "LogOut Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void roundedButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Silakan klik salah satu baris turnamen pada tabel yang ingin diedit terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
               
                DataGridViewRow row = dataGridView1.CurrentRow;

            
                int idKompetisi = Convert.ToInt32(row.Cells[0].Value);
                string namaLama = row.Cells[1].Value?.ToString() ?? "";
                int hargaLama = Convert.ToInt32(row.Cells[3].Value);
                int hadiahLama = Convert.ToInt32(row.Cells[6].Value);

               
                MenuBuatTournament formEdit = new MenuBuatTournament(this.adminLogin, idKompetisi, namaLama, hargaLama, hadiahLama);

               
                formEdit.FormClosed += (s, args) =>
                {
                    TampilkanDataTournament(); 
                    this.Show();          
                };


                formEdit.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengambil data turnamen dari tabel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void roundedButton3_Click_1(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Silakan klik salah satu baris turnamen pada tabel terlebih dahulu untuk melihat data pemain!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
   
                DataGridViewRow row = dataGridView1.CurrentRow;


                int idKompetisiDariTabel = Convert.ToInt32(row.Cells[0].Value);


                MenuLihatDataPemain lihatPemain = new MenuLihatDataPemain(this.adminLogin, idKompetisiDariTabel);
                lihatPemain.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memindahkan data turnamen: {ex.Message}", "Error Navigasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuPertandingan formPertandingan = new MenuPertandingan();

            // 2. Tampilkan form tujuan
            formPertandingan.Show();

            // 3. Sembunyikan form yang sedang aktif (opsional, agar tidak menumpuk)
            this.Hide();
        }
    }
}