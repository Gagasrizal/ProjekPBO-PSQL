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
    public partial class MenuLihatDataPemain : Form
    {
        private User adminLogin;

        // SEKARANG SUDAH FIX: Konstruktor menerima parameter User
        public MenuLihatDataPemain(User user)
        {
            InitializeComponent();
            this.adminLogin = user; // Menyimpan data operan admin aktif
        }
    }
}
