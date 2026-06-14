using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class Kompetisi
    {
        public int IdKompetisi { get; set; }
        public int IdUser { get; set; }
        public DateTime TanggalPelaksanaan { get; set; }
        public string PelaksanaanPendaftaran { get; set; }

        private string _namaKompetisi = string.Empty;
        private string _modeKompetisi = string.Empty;
        private string _sistemPertandingan = string.Empty;
        private int _hargaPendaftaran;
        private int _hadiah;
        private int _jumlahBabak;

        public string NamaKompetisi
        {
            get { return _namaKompetisi; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    throw new ArgumentException("Nama kompetisi minimal 5 karakter.");
                _namaKompetisi = value;
            }
        }

        public string ModeKompetisi
        {
            get { return _modeKompetisi; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Mode kompetisi wajib diisi.");
                _modeKompetisi = value;
            }
        }

        public string SistemPertandingan
        {
            get { return _sistemPertandingan; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Sistem pertandingan wajib diisi.");
                _sistemPertandingan = value;
            }
        }

        public int HargaPendaftaran
        {
            get { return _hargaPendaftaran; }
            set
            {
                if (value < 0) throw new ArgumentException("Harga pendaftaran tidak boleh minus.");
                _hargaPendaftaran = value;
            }
        }

        public int Hadiah
        {
            get { return _hadiah; }
            set
            {
                if (value < 0) throw new ArgumentException("Hadiah tidak boleh minus.");
                _hadiah = value;
            }
        }

        public int JumlahBabak
        {
            get { return _jumlahBabak; }
            set
            {
                if (value <= 0) throw new ArgumentException("Jumlah babak minimal 1.");
                _jumlahBabak = value;
            }
        }
    }
}
