using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjekPBO_PSQL
{
    public class WelcomePemain : Form
    {
        private readonly string _username;

        public WelcomePemain(string username)
        {
            _username = username;
            Initialize();
        }

        private void Initialize()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Selamat Datang";
            this.ClientSize = new Size(420, 180);

            var label = new Label
            {
                Text = $"Selamat datang, {_username}!",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold)
            };

            var button = new Button
            {
                Text = "Lanjut",
                Dock = DockStyle.Bottom,
                Height = 40
            };
            button.Click += Button_Click;

            this.Controls.Add(label);
            this.Controls.Add(button);
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            var pemainForm = new MenuPemain();
            pemainForm.Show();
            this.Hide();
        }
    }
}
