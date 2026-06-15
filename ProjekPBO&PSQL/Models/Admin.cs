using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class Admin : User
    {
        public Admin(int id, string username, string password, string email)
            : base(id, username, password, email)
        {
        }

        public override void TampilkanAksesMenu()
        {
            Console.WriteLine($"Login berhasil sebagai admin: {username}. Membuka Dashboard Admin..");
        }
        
        
    }
}
