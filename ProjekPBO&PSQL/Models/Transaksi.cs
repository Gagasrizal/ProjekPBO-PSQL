using ProjekPBO_PSQL.Interface;
using System;

namespace ProjekPBO_PSQL.Models
{
    public class Transaksi : IBayar
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
                if (value <= 0)
                    throw new ArgumentException("Nominal transaksi harus lebih dari 0.");
                _nominalTransaksi = value;
            }
        }

        public string StatusTransaksi
        {
            get { return _statusTransaksi; }
            set
            {
                if (value != "Belum Bayar" && value != "Sudah Lunas")
                    throw new ArgumentException("Status transaksi hanya boleh 'Belum Bayar' atau 'Sudah Lunas'!");
                _statusTransaksi = value;
            }
        }

        public void LakukanPembayaran(int nominal)
        {
            if (nominal < NominalTransaksi)
                throw new Exception("Nominal pembayaran kurang!");
            StatusTransaksi = "Sudah Lunas";
        }

        public string CekStatusPembayaran()
        {
            return $"Status Transaksi ID {IdTransaksi}: {StatusTransaksi}";
        }
    }
}