using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class Tournament
    {
        public int IdKompetisi { get; set; }
        public int IdUser { get; set; } // Admin pembuat kompetisi
        public string NamaKompetisi { get; set; }
        public string ModeKompetisi { get; set; }
        public int HargaPendaftaran { get; set; }
        public string PelaksanaanPendaftaran { get; set; }
        public DateTime TanggalPelaksanaan { get; set; }
        public int Hadiah { get; set; }
        public List<User> DaftarPeserta { get; set; }

        // Constructor Model disesuaikan dengan tabel kompetisi
        public Tournament(int idKompetisi, int idUser, string namaKompetisi, string modeKompetisi, int hargaPendaftaran, string pelaksanaanPendaftaran, DateTime tanggalPelaksanaan, int hadiah)
        {
            this.IdKompetisi = idKompetisi;
            this.IdUser = idUser;
            this.NamaKompetisi = namaKompetisi;
            this.ModeKompetisi = modeKompetisi;
            this.HargaPendaftaran = hargaPendaftaran;
            this.PelaksanaanPendaftaran = pelaksanaanPendaftaran;
            this.TanggalPelaksanaan = tanggalPelaksanaan;
            this.Hadiah = hadiah;
            this.DaftarPeserta = new List<User>(); // List kosong untuk Agregasi
        }
    }
}