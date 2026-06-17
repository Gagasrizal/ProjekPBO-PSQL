using ProjekPBO_PSQL.Controller;
using ProjekPBO_PSQL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjekPBO_PSQL.View.Pemain
{
    public partial class EditProfil : Form
    {
        private AkunUser userLogin;

        // Panggil Controller, bukan Context lagi!
        private ProfilController controller = new ProfilController();

        public EditProfil(AkunUser user)
        {
            InitializeComponent();
            this.userLogin = user;
        }

        private void EditProfil_Load(object sender, EventArgs e)
        {
            try
            {
                // View meminta data ke Controller
                DataTable dt = controller.AmbilDataProfil(userLogin.IdUser);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // Tampilkan data ke komponen UI
                    textBox2.Text = row["username"].ToString();    // Username
                    textBox3.Text = row["no_telepon"].ToString();  // No HP
                    textBox4.Text = row["password"].ToString();    // Password
                    textBox1.Text = row["deskripsi"].ToString();   // Deskripsi

                    if (comboBox1.Items.Contains(row["negara"].ToString()))
                    {
                        comboBox1.SelectedItem = row["negara"].ToString(); // Country
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data profil: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================================
        // FITUR EDIT DATA (Tombol Selesai)
        // =======================================================================
        private void Edit_Click(object sender, EventArgs e)
        {
            // View HANYA bertugas mengumpulkan input dari teks boks
            string username = textBox2.Text.Trim();
            string noTelepon = textBox3.Text.Trim();
            string password = textBox4.Text.Trim();
            string deskripsi = textBox1.Text.Trim();
            string negara = comboBox1.SelectedItem?.ToString() ?? "";

            // View melempar data inputan ke Controller untuk divalidasi dan diproses
            string hasil = controller.ProsesUpdateProfil(userLogin.IdUser, username, password, negara, noTelepon, deskripsi);

            if (hasil == "SUKSES")
            {
                MessageBox.Show("Perubahan profil berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Sinkronisasi session lokal
                userLogin.Username = username;

                // Pindah halaman
                MenuPemain menuUtama = new MenuPemain(this.userLogin);
                menuUtama.Show();
                this.Close();
            }
            else if (hasil.StartsWith("VALIDASI_GAGAL:"))
            {
                // Jika gagal validasi, potong pesan kodenya lalu tampilkan warning
                string pesanPeringatan = hasil.Replace("VALIDASI_GAGAL: ", "");
                MessageBox.Show(pesanPeringatan, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Jika terjadi error database
                MessageBox.Show(hasil, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================================
        // NAVIGASI SIDEBAR MENU (Tetap sama seperti sebelumnya)
        // =======================================================================
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Kamu sudah berada di halaman profil.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuTournament formDaftar = new MenuTournament(this.userLogin);
            formDaftar.Show();
            this.Close();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuHistoryPermainan formHistory = new MenuHistoryPermainan(this.userLogin);
            formHistory.Show();
            this.Close();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuAturan formAturan = new MenuAturan(this.userLogin);
            formAturan.Show();
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

        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}