using ProjekPBO_PSQL.Models;

public class Tournament
{
        private string _namaKompetisi = string.Empty;
        private string _modeKompetisi = string.Empty;
        private string _sistemPertandingan = string.Empty;
        private string _pelaksanaanPendaftaran = string.Empty;
        private int _hargaPendaftaran;
        private int _hadiah;
        private int _jumlahBabak;

        public int IdKompetisi { get; set; }
        public int IdUser { get; set; }
        public DateTime TanggalPelaksanaan { get; set; }

        public string NamaKompetisi
        {
            get => _namaKompetisi;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    throw new ArgumentException("Nama kompetisi minimal 5 karakter.");
                _namaKompetisi = value;
            }
        }

        public string ModeKompetisi
        {
            get => _modeKompetisi;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Mode kompetisi wajib diisi.");
                _modeKompetisi = value;
            }
        }

        public string SistemPertandingan
        {
            get => _sistemPertandingan;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Sistem pertandingan wajib diisi.");
                _sistemPertandingan = value;
            }
        }

        public string PelaksanaanPendaftaran
        {
            get => _pelaksanaanPendaftaran;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Status pelaksanaan pendaftaran wajib diisi.");
                _pelaksanaanPendaftaran = value;
            }
        }

        public int HargaPendaftaran
        {
            get => _hargaPendaftaran;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Harga pendaftaran tidak boleh minus.");
                _hargaPendaftaran = value;
            }
        }

        public int Hadiah
        {
            get => _hadiah;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Hadiah tidak boleh minus.");
                _hadiah = value;
            }
        }

        public int JumlahBabak
        {
            get => _jumlahBabak;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Jumlah babak minimal 1.");
                _jumlahBabak = value;
            }
        }

        public override string ToString() =>
            $"[Kompetisi] {NamaKompetisi} | Mode: {ModeKompetisi} | " +
            $"Sistem: {SistemPertandingan} | Babak: {JumlahBabak} | " +
            $"Hadiah: Rp{Hadiah:N0} | Biaya Daftar: Rp{HargaPendaftaran:N0}";
    }
