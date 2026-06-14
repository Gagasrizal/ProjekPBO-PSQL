using System;

namespace ProjekPBO_PSQL.Models
{
    public class ProfilCatur
    {
        public int IdDetailUser { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string Deskripsi { get; set; }

        private string _namaLengkap = string.Empty;
        private string _negara = string.Empty;
        private string _noTelepon = string.Empty;
        private int _eloRating;

        public string NamaLengkap
        {
            get { return _namaLengkap; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException("Nama lengkap tidak valid! Minimal harus 5 huruf.");
                }
                _namaLengkap = value;
            }
        }

        public string Negara
        {
            get { return _negara; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Negara asal tidak boleh kosong!");
                }
                _negara = value;
            }
        }

        public string NoTelepon
        {
            get { return _noTelepon; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 10)
                {
                    throw new ArgumentException("Nomor telepon tidak valid! Minimal 10 digit.");
                }
                _noTelepon = value;
            }
        }

        public int EloRating
        {
            get { return _eloRating; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("ELO Rating tidak valid! Tidak boleh minus.");
                }
                _eloRating = value;
            }
        }
    }
}