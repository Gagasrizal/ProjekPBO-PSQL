using System;

namespace ProjekPBO_PSQL.Models
{
    public class Admin : AkunUser
    {
        public Admin(int id, string username, string password, string email, ProfilCatur profil)
            : base(id, username, password, email, true, profil)
        {
            // Cukup lempar data ke base constructor induk
        }
    }
}