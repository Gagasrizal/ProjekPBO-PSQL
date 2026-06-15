using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class Pemain : User
    {
        public Pemain(int id, string username, string password, string email, Detail_User profil)
            :base(id, username, password, email)
        {
            this.Profile = profil;
        }

        public override void TampilkanAksesMenu()
        {
            Console.WriteLine($"Login berhasil sebagai Pemain: (Rating: {Profile.Elo_rating}");
        }
    }
}
