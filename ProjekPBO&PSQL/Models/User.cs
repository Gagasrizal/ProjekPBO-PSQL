using System;

namespace ProjekPBO_PSQL.Models
{
    public abstract class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; } // Di C# memetakan kolom database 'passwords'
        public string email { get; set; }
        //public bool isAdmin { get; set; }

        public User(int id, string username, string password, string email)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.email = email;
            ////this.isAdmin = isAdmin;
            //bool isAdmin
        }

        public abstract void TampilkanAksesMenu();
    }
}