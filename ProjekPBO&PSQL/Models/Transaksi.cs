using System;

namespace ProjekPBO_PSQL.Models
{
    public class Transaksi
    {
        public int IdTransaksi { get; set; }
        public int IdPendaftaranKompetisi { get; set; }
        public int IdMetodePembayaran { get; set; }
        public DateTime TanggalTransaksi { get; set; } = DateTime.Now;
        public MetodePembayaran BankPilihan { get; set; }

        private int _nominalTransaksi;

        private string _statusTransaksi = "Belum Bayar";

        public int NominalTransaksi
        {
            get { return _nominalTransaksi; }
            set
            {
                if (value <= 0) throw new ArgumentException("Nominal transaksi harus lebih dari 0.");
                _nominalTransaksi = value;
            }
        }

        public string StatusTransaksi
        {
            get { return _statusTransaksi; }
            set
            {
                if (value != "Belum Bayar" && value != "Sudah Lunas")
                {
                    throw new ArgumentException("Status transaksi hanya boleh 'Belum Bayar' atau 'Sudah Lunas'!");
                }
                _statusTransaksi = value;
            }
        }
    }
}
