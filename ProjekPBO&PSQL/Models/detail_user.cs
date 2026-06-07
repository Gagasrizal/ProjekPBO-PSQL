using System;

namespace ProjekPBO_PSQL.Models
{
    public class Detail_User
    {
        public int Id { get; set; }
        public string Nama_lengkap { get; set; }
        public string Negara { get; set; }
        public string No_telepon { get; set; }
        public DateTime Tanggal_lahir { get; set; }
        public int Elo_rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Deskripsi { get; set; }

        public Detail_User(int id, string nama_lengkap, string negara, string no_telepon, DateTime tanggal_lahir, int elo_rating, DateTime createdAt, string deskripsi)
        {
            this.Id = id;
            this.Nama_lengkap = nama_lengkap;
            this.Negara = negara;
            this.No_telepon = no_telepon;
            this.Tanggal_lahir = tanggal_lahir.Date;
            this.Elo_rating = elo_rating;
            this.CreatedAt = createdAt.Date;
            this.Deskripsi = deskripsi;
        }
    }
}