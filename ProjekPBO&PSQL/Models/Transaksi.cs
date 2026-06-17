using System;
using ProjekPBO_PSQL.Interface;

namespace ProjekPBO_PSQL.Models
{
    public class Transaksi : IBayar
    {
        private int _nominalTransaksi;
        private string _statusTransaksi = "Belum Bayar";

        public int IdTransaksi { get; set; }
        public int IdPendaftaranKompetisi { get; set; }
        public int IdMetodePembayaran { get; set; }
        public DateTime TanggalTransaksi { get; set; } = DateTime.Now;
        public MetodePembayaran? BankPilihan { get; set; }

        public int NominalTransaksi
        {
            get => _nominalTransaksi;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Nominal transaksi harus sesuai.");
                _nominalTransaksi = value;
            }
        }

        public string StatusTransaksi
        {
            get => _statusTransaksi;
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
                throw new InvalidOperationException("Nominal pembayaran kurang!");
            StatusTransaksi = "Sudah Lunas";
        }

        public string CekStatusPembayaran() => $"Status Transaksi ID {IdTransaksi}: {StatusTransaksi}";
    }
}