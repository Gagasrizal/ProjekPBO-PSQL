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
        public string SistemPertandingan { get; set; } // <--- 1. PROPERTI BARU UNTUK SINKRONISASI DB
        public List<User> DaftarPeserta { get; set; }

        // --- 2. CONSTRUCTOR KOSONG (Sangat berguna untuk penciptaan objek dinamis/tanpa parameter penuh) ---
        public Tournament()
        {
            this.DaftarPeserta = new List<User>();
        }

        // --- 3. CONSTRUCTOR BERPARAMETER (Sudah ditambahkan sistemPertandingan) ---
        public Tournament(int idKompetisi, int idUser, string namaKompetisi, string modeKompetisi, int hargaPendaftaran, string pelaksanaanPendaftaran, DateTime tanggalPelaksanaan, int hadiah, string sistemPertandingan)
        {
            this.IdKompetisi = idKompetisi;
            this.IdUser = idUser;
            this.NamaKompetisi = namaKompetisi;
            this.ModeKompetisi = modeKompetisi;
            this.HargaPendaftaran = hargaPendaftaran;
            this.PelaksanaanPendaftaran = pelaksanaanPendaftaran;
            this.TanggalPelaksanaan = tanggalPelaksanaan;
            this.Hadiah = hadiah;
            this.SistemPertandingan = sistemPertandingan; // <--- ISI PROPERTI BARU
            this.DaftarPeserta = new List<User>(); // List kosong untuk Agregasi
        }
    }
}