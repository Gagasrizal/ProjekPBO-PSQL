using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjekPBO_PSQL.View.Pemain
{
    public partial class MenuProfilPem : Form
    {
        public MenuProfilPem()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // MenuProfilPem
            // 
            ClientSize = new Size(800, 450);
            Name = "MenuProfilPem";
            Text = "MenuProfilPemain";
            ResumeLayout(false);

        }
    }
}
