using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class detail_User
    {
        public int id { get; set; }
        public string nama_lengkap { get; set; }
        public string negara { get; set; }
        public string no_telepon { get; set; }
        public DateTime tanggal_lahir { get; set; }
        public int elo_rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public string deskripsi { get; set; }





        public detail_User(int id, string nama_lengkap, string negara, string no_telepon, DateTime tanggal_lahir, int elo_rating, DateTime createdAt, string deskripsi)
        {
            this.id = id;
            this.nama_lengkap = nama_lengkap;
            this.negara = negara;
            this.no_telepon = no_telepon;
            this.tanggal_lahir = tanggal_lahir.Date;
            this.elo_rating = elo_rating;
            this.CreatedAt = createdAt;
            this.deskripsi = deskripsi;
        }
    }
}