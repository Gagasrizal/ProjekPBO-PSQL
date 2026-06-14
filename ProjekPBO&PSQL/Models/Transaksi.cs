using System;

namespace ProjekPBO_PSQL.Models
{
    public class Transaksi
    {
            public int IdTransaksi { get; set; }
            public int IdPendaftaranKompetisi { get; set; }
            public int IdMetodePembayaran { get; set; }
            public int NominalTransaksi { get; set; }
            public string StatusTransaksi { get; set; } // Sukses, Gagal
            public DateTime TanggalTransaksi { get; set; }

            public Transaksi(int id, int idPendaftaran, int idMetode, int nominal, string status, DateTime tanggal)
            {
                IdTransaksi = id;
                IdPendaftaranKompetisi = idPendaftaran;
                IdMetodePembayaran = idMetode;
                NominalTransaksi = nominal;
                StatusTransaksi = status;
                TanggalTransaksi = tanggal;
            }
        }
    }