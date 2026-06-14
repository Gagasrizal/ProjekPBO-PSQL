using System;

namespace ProjekPBO_PSQL.Models
{
    public abstract class AkunUser
    {
        public int IdUser { get; set; }
        public bool IsAdmin { get; set; }

        private string _username = string.Empty;
        private string _passwords = string.Empty;
        private string _email = string.Empty;

        public ProfilCatur Profil { get; set; }

        public AkunUser(int idUser, string username, string passwords, string email, bool isAdmin, ProfilCatur profil)
        {
            this.IdUser = idUser;
            this.Username = username;
            this.Passwords = passwords;
            this.Email = email;
            this.IsAdmin = isAdmin;
            this.Profil = profil;
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    throw new ArgumentException("Username tidak valid! Minimal harus 4 karakter.");
                }
                if (value.Contains(" "))
                {
                    throw new ArgumentException("Username tidak boleh mengandung spasi!");
                }
                _username = value;
            }
        }

        // 🌟 SATPAM VALIDASI PASSWORD
        public string Passwords
        {
            get
            {
                return _passwords;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                {
                    throw new ArgumentException("Password terlalu lemah! Minimal harus 6 karakter.");
                }
                _passwords = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                // 🌟 Tambahan validasi: Cek kosong dulu, baru cek format
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email tidak boleh kosong!");
                }
                if (!value.Contains("@") || !value.Contains("."))
                {
                    throw new ArgumentException("Format email tidak valid! Wajib menyertakan '@' dan '.'");
                }
                _email = value;
            }
        }
    }
}