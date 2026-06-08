public class Tournament
{
    public int IdKompetisi { get; set; }
    public int IdUser { get; set; }
    public string NamaKompetisi { get; set; }
    public string ModeKompetisi { get; set; }
    public int HargaPendaftaran { get; set; }
    public string PelaksanaanPendaftaran { get; set; }
    public DateTime TanggalPelaksanaan { get; set; }
    public int Hadiah { get; set; }
    public string SistemPertandingan { get; set; }
    public int JumlahBabak { get; set; } // <--- TAMBAHKAN PROPERTY INI

    // Update Constructor-nya agar urutan parameternya pas dengan yang kamu panggil
    public Tournament(int idKompetisi, int idUser, string namaKompetisi, string modeKompetisi,
                      int hargaPendaftaran, string pelaksanaanPendaftaran, DateTime tanggalPelaksanaan,
                      int hadiah, string sistemPertandingan, int jumlahBabak)
    {
        IdKompetisi = idKompetisi;
        IdUser = idUser;
        NamaKompetisi = namaKompetisi;
        ModeKompetisi = modeKompetisi;
        HargaPendaftaran = hargaPendaftaran;
        PelaksanaanPendaftaran = pelaksanaanPendaftaran;
        TanggalPelaksanaan = tanggalPelaksanaan;
        Hadiah = hadiah;
        SistemPertandingan = sistemPertandingan;
        JumlahBabak = jumlahBabak; // <--- ISI DISINI
    }
}