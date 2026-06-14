using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class MetodePembayaran
    {
        public int IdMetodePembayaran { get; set; }
        private string _namaMetodePembayaran = string.Empty;

        public string NamaMetodePembayaran
        {
            get { return _namaMetodePembayaran; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nama bank/metode pembayaran wajib diisi.");
                _namaMetodePembayaran = value;
            }
        }
    }
}
