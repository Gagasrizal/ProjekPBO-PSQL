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
    public partial class MenuBuatTournament : Form
    {
        private User adminLogin;

        // 2. Ubah konstruktor utama agar menerima parameter (User user)
        public MenuBuatTournament(User user)
        {
            InitializeComponent();
            this.adminLogin = user; // Menyimpan data admin yang sedang aktif
        }

        private void MenuBuatTournament_Load(object sender, EventArgs e)
        {
            // Logika saat halaman buat turnamen pertama kali dimuat
        }
    }
}