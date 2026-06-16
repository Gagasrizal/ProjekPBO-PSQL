using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class MetodePembayaran
    {
        private string _namaMetodePembayaran = string.Empty;

        public int IdMetodePembayaran { get; set; }

        public string NamaMetodePembayaran
        {
            get => _namaMetodePembayaran;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama bank/metode pembayaran wajib diisi.");
                _namaMetodePembayaran = value;
            }
        }

        public override string ToString() => NamaMetodePembayaran;
    }
}
