using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjekPBO_PSQL.View.Admin
{
    public partial class MenuProfilAdmin : Form
    {
        private AkunUser adminLogin;
        private UserContext userContext = new UserContext();

        public MenuProfilAdmin(AkunUser user)
        {
            InitializeComponent();
            this.adminLogin = user;
        }

        private void MenuProfilAdmin_Load(object sender, EventArgs e)
        {
            if (adminLogin != null)
            {
                label12.Text = adminLogin.Username; 
                label17.Text = adminLogin.Email;    

                ProfilCatur detail = userContext.GetDetailUserByUserId(adminLogin.IdUser);
                if (detail != null)
                {
 
                    label14.Text = detail.Negara;
                    label13.Text = detail.EloRating.ToString(); 
                    label20.Text = detail.NoTelepon;            
                    label18.Text = detail.TanggalLahir.ToString("dd MMMM yyyy"); 


                    label1.Text = "Account Profile Verified";
                }
                else
                {
                    label14.Text = "-";
                    label13.Text = "-";
                    label20.Text = "-";
                    label18.Text = "-";
                    label1.Text = "Account Created on: -";
                }
            }
            else
            {
                // Blok else jika data sesi login admin tidak ditemukan
                MessageBox.Show("Data sesi login admin tidak ditemukan! Pastikan masuk dari FormLogin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) 
        {

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
        private void label4_Click(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label20_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label18_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label17_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void roundedpanel1_Paint(object sender, PaintEventArgs e) { }
        private void Edit_Click(object sender, EventArgs e) { }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MenuPertandingan formPertandingan = new MenuPertandingan(this.adminLogin);
            formPertandingan.Show();
            this.Hide();
        }
    }
}