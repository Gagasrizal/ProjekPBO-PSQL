using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class ProfilCatur
    {
        private string _namaLengkap;
        private string _negara;
        private string _noTelepon;

        public int EloRating { get; private set; }
        public DateTime TanggalLahir { get; set; }
        public string Deskripsi { get; set; }
        public DateTime CreatedAt { get; set; }

        public string NamaLengkap
        {
            get { return _namaLengkap; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    throw new ArgumentException("Nama Lengkap tidak valid! Minimal harus 3 huruf.");
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
                    throw new ArgumentException("Asal Negara wajib diisi, tidak boleh kosong!");
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
                    throw new ArgumentException("Nomor Telepon tidak valid! Minimal harus 10 digit.");
                }
                _noTelepon = value;
            }
        }
    }
}