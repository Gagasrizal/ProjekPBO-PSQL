using System;

namespace ProjekPBO_PSQL.Models
{
    public abstract class AkunUser
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;

        public int IdUser { get; set; }
        public bool IsAdmin { get; protected set; }
        public ProfilCatur Profil { get; private set; }
        public DateTime CreatedAt { get; set; }
        protected AkunUser(int id, string username, string password, string email, bool isAdmin, ProfilCatur? profil = null)
        {
            IdUser = id;
            Username = username;
            Password = password;
            Email = email;
            IsAdmin = isAdmin;
            Profil = profil ?? new ProfilCatur();
        }

        public string Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length <= 5)
                    throw new ArgumentException("Username tidak valid! Harus lebih dari 5 karakter.");
                _username = value;
            }
        }

        public string Password
        {
            protected get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new ArgumentException("Password tidak valid! Minimal harus 6 karakter.");
                _password = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains('@') || !value.Contains('.'))
                    throw new ArgumentException("Format email salah atau kosong!");
                _email = value;
            }
        }

        public virtual string GetRole() => "User";

        public virtual void TampilkanInfo()
        {
            Console.WriteLine($"[USER] ID: {IdUser} | Username: {Username} | Email: {Email}");
            Console.WriteLine($"       Role   : {GetRole()}");
            Console.WriteLine($"       Profil : {Profil}");
        }

        public abstract void LakukanAktivitasUtama();
    }
}