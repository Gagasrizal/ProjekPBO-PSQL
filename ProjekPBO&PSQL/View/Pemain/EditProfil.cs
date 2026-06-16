using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models; // Pastikan namespace Model User terbaca

namespace ProjekPBO_PSQL.View.Pemain
{
    public partial class EditProfil : Form
    {
        // Sesi user yang sedang login
        private AkunUser userLogin;

        // Ubah konstruktor agar wajib menerima objek User
        public EditProfil(AkunUser user)
        {
            InitializeComponent();
            this.userLogin = user;
        }

        private void EditProfil_Load(object sender, EventArgs e)
        {
            // Di sini kamu bisa menampilkan data user ke TextBox saat form dibuka, contoh:
            // txtUsername.Text = userLogin.username;
            // txtEmail.Text = userLogin.email;
        }

        // =======================================================================
        // NAVIGASI SIDEBAR MENU
        // =======================================================================

        // 1. PROFIL (Karena sudah di halaman profil/edit profil)
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Kamu sudah berada di halaman profil.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 2. LIST TOURNAMENT
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuTournament formDaftar = new MenuTournament(this.userLogin);
            formDaftar.Show();
            this.Close(); // Hancurkan form lama dari memori
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuHistoryPermainan formHistory = new MenuHistoryPermainan(this.userLogin);
            formHistory.Show();
            this.Close(); // Hancurkan form lama dari memori
        }

        // 5. BACA PERATURAN
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuAturan formAturan = new MenuAturan(this.userLogin);
            formAturan.Show();
            this.Close(); // Hancurkan form lama dari memori
        }

        // 6. TOMBOL LOGOUT (roundedButton1)
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah kamu yakin ingin keluar dari Hyper Chess?", "LogOut", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                FormLogin login = new FormLogin();
                login.Show();
                this.Close(); // Hancurkan form agar memori bersih
            }
        }

        // =======================================================================
        // FITUR EDIT DATA (Tombol Selesai di pojok kanan atas)
        // =======================================================================
        private void Edit_Click(object sender, EventArgs e)
        {
            // Taruh logika UPDATE database kamu di sini menggunakan DBHelper
            // Setelah sukses update, kembalikan ke MenuPemain atau MenuProfilPem
            MessageBox.Show("Perubahan profil berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MenuPemain menuUtama = new MenuPemain(this.userLogin);
            menuUtama.Show();
            this.Close();
        }

        // Event kosong bawaan design (Biarkan saja jangan dihapus agar tidak error)
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}