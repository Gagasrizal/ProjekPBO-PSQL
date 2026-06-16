using ProjekPBO_PSQL.Models;         
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ProjekPBO_PSQL.Models.Context;

namespace ProjekPBO_PSQL.Controller
{
    public class KompetisiController
    {
        private readonly KompetisiContext _context = new KompetisiContext();

        public DataTable GetSemuaKompetisi()
        {
            try
            {
                return _context.GetSemuaKompetisi();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Kompetisi GetKompetisiById(int idKompetisi)
        {
            try
            {
                return _context.GetKompetisiById(idKompetisi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void TambahKompetisi(int idUserPembuat, string nama, string mode,
                                    int harga, string pelaksanaan,
                                    DateTime tanggal, int hadiah,
                                    string sistem, int jumlahBabak)
        {
            try
            {
                var kompetisi = new Kompetisi
                {
                    IdUser = idUserPembuat,
                    NamaKompetisi = nama,
                    ModeKompetisi = mode,
                    HargaPendaftaran = harga,
                    PelaksanaanPendaftaran = pelaksanaan,
                    TanggalPelaksanaan = tanggal,
                    Hadiah = hadiah,
                    SistemPertandingan = sistem,
                    JumlahBabak = jumlahBabak
                };

                _context.TambahKompetisi(kompetisi);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Data tidak valid: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateKompetisi(int idKompetisi, string nama, string mode,
                                    int harga, string pelaksanaan,
                                    DateTime tanggal, int hadiah,
                                    string sistem, int jumlahBabak)
        {
            try
            {
                var kompetisi = new Kompetisi
                {
                    IdKompetisi = idKompetisi,
                    NamaKompetisi = nama,
                    ModeKompetisi = mode,
                    HargaPendaftaran = harga,
                    PelaksanaanPendaftaran = pelaksanaan,
                    TanggalPelaksanaan = tanggal,
                    Hadiah = hadiah,
                    SistemPertandingan = sistem,
                    JumlahBabak = jumlahBabak
                };

                _context.UpdateKompetisi(kompetisi);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Data tidak valid: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetPesertaTurnamen(int idKompetisi)
        {
            try
            {
                return _context.TampilkanPesertaTurnamen(idKompetisi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}