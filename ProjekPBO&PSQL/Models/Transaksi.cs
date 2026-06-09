using System;

namespace ProjekPBO_PSQL.Models
{
    public class Transaksi
    {
        public int IdTransaksi { get; set; }
        public int IdUser { get; set; }
        public int IdKompetisi { get; set; }
        public int IdMetodePembayaran { get; set; }
        public int NominalTransaksi { get; set; }
        public string StatusTransaksi { get; set; }
        public DateTime TanggalTransaksi { get; set; }

        // Constructor untuk inisialisasi data objek Transaksi
        public Transaksi(int idTransaksi, int idUser, int idKompetisi, int idMetodePembayaran, int nominalTransaksi, string statusTransaksi, DateTime tanggalTransaksi)
        {
            this.IdTransaksi = idTransaksi;
            this.IdUser = idUser;
            this.IdKompetisi = idKompetisi;
            this.IdMetodePembayaran = idMetodePembayaran;
            this.NominalTransaksi = nominalTransaksi;
            this.StatusTransaksi = statusTransaksi;
            this.TanggalTransaksi = tanggalTransaksi;
        }
    }
}