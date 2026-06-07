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
    public partial class LihatDataPembayaran : Form
    {
        private User adminLogin; // Tambahkan ini

        // Ubah dari public LihatDataPembayaran() menjadi:
        public LihatDataPembayaran(User user)
        {
            InitializeComponent();
            this.adminLogin = user; // Simpan sesi
        }
    }
}