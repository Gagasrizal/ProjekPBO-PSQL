using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class User
        {
            public int id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string email { get; set; }
         
            public bool isAdmin { get; set; }



            public User(int id, string username, string password, string email,  bool isAdmin)
            {
                this.id = id;
                this.username = username;
                this.password = password;
                this.email = email;
                this.isAdmin = isAdmin;
            }
        }
    }