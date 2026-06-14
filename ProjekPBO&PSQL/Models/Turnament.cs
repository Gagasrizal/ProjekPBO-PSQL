public class Tournament
{
    public class Kompetisi
    {
        public int IdKompetisi { get; set; }
        public int IdUserPembuat { get; set; } // Admin yang membuat
        public string NamaKompetisi { get; set; }
        public string ModeKompetisi { get; set; } // Rapid, Blitz
        public int HargaPendaftaran { get; set; }
        public string PelaksanaanPendaftaran { get; set; }
        public DateTime TanggalPelaksanaan { get; set; }
        public int Hadiah { get; set; }
        public string SistemPertandingan { get; set; } // Swiss, Round Robin
        public int JumlahBabak { get; set; }

        public Kompetisi(int id, int idPembuat, string nama, string mode, int harga, string infoDaftar, DateTime tglMain, int hadiah, string sistem, int babak)
        {
            IdKompetisi = id;
            IdUserPembuat = idPembuat;
            NamaKompetisi = nama;
            ModeKompetisi = mode;
            HargaPendaftaran = harga;
            PelaksanaanPendaftaran = infoDaftar;
            TanggalPelaksanaan = tglMain;
            Hadiah = hadiah;
            SistemPertandingan = sistem;
            JumlahBabak = babak;
        }
    }
}