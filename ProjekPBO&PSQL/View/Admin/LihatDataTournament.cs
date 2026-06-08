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
    
    dataGridView1.AutoGenerateColumns = true;
    DataTable dt = db.AmbilSemuaTournament();
    
    if (dt != null)
    {
        dataGridView1.DataSource = dt;
        
        // 1. MATIKAN mode Fill agar kolom bisa melar melebihi batas kanan form
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        
        // 2. Aktifkan scroll bar horizontal & vertikal secara paksa
        dataGridView1.ScrollBars = ScrollBars.Both;

        // 3. Ubah nama header menjadi rapi dan tentukan LEBAR MINIMAL tiap kolom (dalam pixel)
        if (dataGridView1.Columns.Count > 9)
        {
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;  // Kolom ID cukup ramping

            dataGridView1.Columns[1].HeaderText = "Nama Turnamen";
            dataGridView1.Columns[1].Width = 180; // Kolom Nama dikasih space lebar biar terbaca utuh

            dataGridView1.Columns[2].HeaderText = "Mode Pertandingan";
            dataGridView1.Columns[2].Width = 120;

            dataGridView1.Columns[3].HeaderText = "Biaya Pendaftaran";
            dataGridView1.Columns[3].Width = 140;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "N0"; // Format ribuan

            dataGridView1.Columns[4].HeaderText = "Batas Registrasi";
            dataGridView1.Columns[4].Width = 130;

            dataGridView1.Columns[5].HeaderText = "Tanggal Main";
            dataGridView1.Columns[5].Width = 110;

            dataGridView1.Columns[6].HeaderText = "Total Hadiah";
            dataGridView1.Columns[6].Width = 130;
            dataGridView1.Columns[6].DefaultCellStyle.Format = "N0"; // Format ribuan

            dataGridView1.Columns[7].HeaderText = "Sistem Match";
            dataGridView1.Columns[7].Width = 150; // Kolom sistem pertandingan dikasih space lebar

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