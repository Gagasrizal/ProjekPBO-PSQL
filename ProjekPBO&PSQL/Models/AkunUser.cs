using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public abstract class AkunUser
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;

        public int IdUser { get; set; }
        public bool IsAdmin { get; protected set; }
        public ProfilCatur Profil { get; set; }

        public AkunUser(int id, string username, string password, string email, bool isAdmin, ProfilCatur profil)
        {
            this.IdUser = id;
            this.Username = username;
            this.Passwords = password;
            this.Email = email;
            this.IsAdmin = isAdmin;
            this.Profil = profil;
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length <= 5)
                {
                    throw new ArgumentException("Username tidak valid! Harus lebih dari 5 karakter.");
                }
                _username = value;
            }
        }

        public string Passwords
        {
            protected get { return _password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                {
                    throw new ArgumentException("Password tidak valid! Minimal harus 6 karakter.");
                }
                _password = value;
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Contains("@") == false || value.Contains(".") == false)
                {
                    throw new ArgumentException("Format email salah atau kosong!");
                }
                _email = value;
            }
        }
    }
}
A