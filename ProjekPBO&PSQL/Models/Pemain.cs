using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class Pemain : AkunUser
    {
        public Pemain(int id, string username, string password, string email, ProfilCatur profil)
            : base(id, username, password, email, false, profil)
        {
            // Cukup lempar data ke base constructor induk
        }
    }
}
