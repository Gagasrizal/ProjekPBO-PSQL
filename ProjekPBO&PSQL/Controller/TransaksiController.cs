using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Models.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Controller
{
    public class TransaksiController
    {
        private TransaksiContext dbTransaksi = new TransaksiContext();

        // Menerima data mentah dari View
        public string ProsesPembayaranOtomatis(int idKompetisi, int idMetode, int nominalInput, int idUser, int biayaTurnamenAsli)
        {
            if (nominalInput <= 0)
            {
                return "VALIDASI_GAGAL: Nominal pembayaran tidak valid! Harus lebih besar dari 0.";
            }

            if (nominalInput != biayaTurnamenAsli)
            {
                return "VALIDASI_GAGAL: Nominal tidak sesuai dengan biaya turnamen asli (Rp " + biayaTurnamenAsli.ToString("N0") + ")!";
            }

            try
            {
                Transaksi trxBaru = new Transaksi()
                {
                    IdPendaftaranKompetisi = idKompetisi,
                    IdMetodePembayaran = idMetode,
                    NominalTransaksi = nominalInput, 
                    TanggalTransaksi = DateTime.Now,
                    StatusTransaksi = "Sudah Lunas"
                };

                bool berhasil = dbTransaksi.BayarDanDaftarOtomatis(trxBaru, idUser);

                if (berhasil)
                {
                    return "SUKSES";
                }
                return "Gagal memproses pembayaran ke database.";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }
    }
}