using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjekPBO_PSQL.Models;


namespace ProjekPBO_PSQL.View.Admin
{
    public partial class LihatDataTournament : Form
    {
        private User adminLogin;

        // Ubah menjadi:
        public LihatDataTournament(User user)
        {
            InitializeComponent();
            this.adminLogin = user;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
