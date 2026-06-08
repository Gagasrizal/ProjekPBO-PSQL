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

            // --- SANGAT PENTING: Paksa ikat Event Load agar fungsi tampil data pasti jalan ---
            this.Load += new System.EventHandler(this.LihatDataTournament_Load);

            // --- TETAP DIKASIH INI BIAR DATA GRID VIEW NYA GA BERKEDIP PAS DI-LOAD ---
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        private void LihatDataTournament_Load(object sender, EventArgs e)
        {
            // 1. Tampilkan semua data turnamen di grid view saat halaman dibuka
            TampilkanDataTournament();

            // 2. Isi data ke ComboBox
            LoadTurnamenToComboBox();
        }

        private void TampilkanDataTournament()
        {
            DBHelper db = new DBHelper();

            // Pastikan DataGridView diizinkan membuat kolom otomatis dari DataTable
            dataGridView1.AutoGenerateColumns = true;

            DataTable dt = db.AmbilSemuaTournament();

            if (dt != null)
            {
                dataGridView1.DataSource = dt;

                // Memaksa DataGridView meremajakan tampilannya agar data langsung muncul
                dataGridView1.Refresh();
            }

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void LoadTurnamenToComboBox()
        {
            DBHelper db = new DBHelper();
            DataTable dt = db.AmbilIdDanNamaTournament();

            if (dt != null && dt.Rows.Count > 0)
            {
                // Putus sementara event agar tidak terpicu crash saat data di-bind
                comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "nama_kompetisi"; // Nama yang muncul di UI ComboBox
                comboBox1.ValueMember = "id_kompetisi";     // ID asli di database kompetisi
                comboBox1.SelectedIndex = -1;                // Default kosong, tidak langsung milih

                // Sambungkan kembali event-nya
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedValue == null)
                return;

            if (int.TryParse(comboBox1.SelectedValue.ToString(), out int idKompetisiTerpilih))
            {
                // Pindah ke MenuLihatDataPemain dengan membawa objek adminLogin DAN idKompetisiTerpilih
                MenuLihatDataPemain lihatPemain = new MenuLihatDataPemain(this.adminLogin, idKompetisiTerpilih);
                lihatPemain.Show();
                this.Hide(); // Sembunyikan halaman turnamen ini
            }
        }

        // --- SEMUA NAVIGASI DI BAWAH INI DIKEMBALIKAN SESUAI KODINGAN ASLIMU ---

        private void button1_Click(object sender, EventArgs e)
        {
            MenuBuatTournament buatTournament = new MenuBuatTournament(this.adminLogin);
            buatTournament.Show();
            this.Hide();
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

        // Kosongan bawaan desainer visual studio
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
    }
}