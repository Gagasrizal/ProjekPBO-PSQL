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
            if (nominal <= 0)
                throw new ArgumentException("Nominal pembayaran harus lebih dari 0.");
            if (nominal < _nominalTransaksi)
                throw new Exception($"Nominal kurang! Dibutuhkan Rp{_nominalTransaksi:N0}, dibayar Rp{nominal:N0}.");

            NominalTransaksi = nominal;
            StatusTransaksi = "Sudah Lunas";
            TanggalTransaksi = DateTime.Now;

            Console.WriteLine($"[TRANSAKSI] Pembayaran berhasil! " +
                              $"Nominal: Rp{nominal:N0} | Metode: {BankPilihan?.NamaMetodePembayaran} | " +
                              $"Tanggal: {TanggalTransaksi:dd/MM/yyyy HH:mm}");
        }

        public string CekStatusPembayaran()
        {
            return $"Status Transaksi ID {IdTransaksi}: {StatusTransaksi}";
        }

        public override string ToString()
        {
            return $"[Transaksi] ID: {IdTransaksi} | Nominal: Rp{NominalTransaksi:N0} | " +
                   $"Status: {StatusTransaksi} | Tanggal: {TanggalTransaksi:dd/MM/yyyy}";
        }
    }
}