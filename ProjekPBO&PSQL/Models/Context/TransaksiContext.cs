using Npgsql;
using ProjekPBO_PSQL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProjekPBO_PSQL.Models.Context
{
    public class TransaksiContext
    {
        private readonly DBHelper dBHelper = new DBHelper();

        public bool BayarDanDaftarOtomatis(Transaksi trx)
        {

            string queryDaftar = @"
        INSERT INTO pendaftaran_kompetisi (id_user, id_kompetisi, status_pendaftaran) 
        VALUES (@id_user, @id_kompetisi, 'terdaftar')
        RETURNING id_pendaftaran_kompetisi;";


            string queryTransaksi = @"
        INSERT INTO transaksi (id_pendaftaran_kompetisi, id_metode_pembayaran, nominal_transaksi, status_transaksi, tanggal_transaksi) 
        VALUES (@id_pendaftaran, @id_metode, @nominal, 'Sukses', @tanggal);";

            using (NpgsqlConnection conn = dBHelper.GetConnection())
            {
                conn.Open();
                using (NpgsqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int idPendaftaranBaru = 0;

                        // --- PROSES 1: INSERT KE TABEL PENDAFTARAN ---
                        using (NpgsqlCommand cmdDaftar = new NpgsqlCommand(queryDaftar, conn))
                        {
                            cmdDaftar.Parameters.AddWithValue("@id_user", trx.IdUser);
                            cmdDaftar.Parameters.AddWithValue("@id_kompetisi", trx.IdKompetisi);

                            object result = cmdDaftar.ExecuteScalar();
                            if (result == null)
                            {
                                throw new Exception("Gagal mendapatkan ID pendaftaran kompetisi.");
                            }
                            idPendaftaranBaru = Convert.ToInt32(result);
                        }

                        // --- PROSES 2: INSERT KE TABEL TRANSAKSI ---
                        using (NpgsqlCommand cmdTrx = new NpgsqlCommand(queryTransaksi, conn))
                        {
                            cmdTrx.Parameters.AddWithValue("@id_pendaftaran", idPendaftaranBaru);
                            cmdTrx.Parameters.AddWithValue("@id_metode", trx.IdMetodePembayaran);
                            cmdTrx.Parameters.AddWithValue("@nominal", trx.NominalTransaksi);
                            cmdTrx.Parameters.AddWithValue("@tanggal", DateTime.Now);

                            cmdTrx.ExecuteNonQuery();
                        }

                        // Jika kedua proses berhasil tanpa interupsi, commit data secara permanen
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Jika salah satu proses gagal, batalkan semuanya (rollback) agar tidak ada data menggantung
                        transaction.Rollback();
                        throw new Exception($"Gagal memproses transaksi (Model): {ex.Message}", ex);
                    }
                }
            }
        }

        public DataTable AmbilSemuaPembayaran()
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT 
            t.id_transaksi AS ""ID Transaksi"",
            u.username AS ""Username Pemain"",
            k.nama_kompetisi AS ""Nama Turnamen"",
            m.nama_metode_pembayaran AS ""Metode"",
            t.nominal_transaksi AS ""Nominal"",
            t.status_transaksi AS ""Status"",
            t.tanggal_transaksi AS ""Tanggal""
        FROM transaksi t
        JOIN pendaftaran_kompetisi pk ON t.id_pendaftaran_kompetisi = pk.id_pendaftaran_kompetisi
        JOIN users u ON pk.id_user = u.id_user
        JOIN kompetisi k ON pk.id_kompetisi = k.id_kompetisi
        JOIN metode_pembayaran m ON t.id_metode_pembayaran = m.id_metode_pembayaran
        ORDER BY t.id_transaksi DESC;";

            try
            {
                using (NpgsqlConnection conn = dBHelper.GetConnection())
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil data transaksi: {ex.Message}", ex);
            }

            return dt;
        }
    }
}

