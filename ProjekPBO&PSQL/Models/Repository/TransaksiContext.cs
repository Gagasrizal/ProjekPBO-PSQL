using Npgsql;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using System;
using System.Data;

namespace ProjekPBO_PSQL.Models.Context
{
    public class TransaksiContext
    {
        public bool DaftarKeKompetisi(int idUser, int idKompetisi, int idMetodeBayar, int nominal)
        {
            using var conn = DBHelper.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                string queryDaftar = @"INSERT INTO pendaftaran_kompetisi (id_user, id_kompetisi, status_pendaftaran) 
                                       VALUES (@id_user, @id_kompetisi, @status) 
                                       RETURNING id_pendaftaran_kompetisi";

                using var cmdDaftar = new NpgsqlCommand(queryDaftar, conn);
                cmdDaftar.Parameters.AddWithValue("@id_user", idUser);
                cmdDaftar.Parameters.AddWithValue("@id_kompetisi", idKompetisi);
                cmdDaftar.Parameters.AddWithValue("@status", "terdaftar");

                object daftarIdObj = cmdDaftar.ExecuteScalar();
                if (daftarIdObj == null) throw new Exception("Gagal mendapatkan nomor pendaftaran kompetisi.");
                int idPendaftaranBaru = Convert.ToInt32(daftarIdObj);

                string queryBayar = @"INSERT INTO pembayaran (id_pendaftaran_kompetisi, id_metode_pembayaran, nominal_pembayaran, tanggal_pembayaran) 
                                       VALUES (@id_daftar, @id_metode, @nominal, @tanggal)";

                using var cmdBayar = new NpgsqlCommand(queryBayar, conn);
                cmdBayar.Parameters.AddWithValue("@id_daftar", idPendaftaranBaru);
                cmdBayar.Parameters.AddWithValue("@id_metode", idMetodeBayar);
                cmdBayar.Parameters.AddWithValue("@nominal", nominal);
                cmdBayar.Parameters.AddWithValue("@tanggal", DateTime.Now);

                cmdBayar.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Proses pendaftaran turnamen gagal: {ex.Message}", ex);
            }
        }


public bool BayarDanDaftarOtomatis(Transaksi trx, int idUser)
        {
            using var conn = DBHelper.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                int idKompetisi = trx.IdPendaftaranKompetisi;
                int idPendaftaranReal = 0;

                string queryCek = "SELECT id_pendaftaran_kompetisi FROM pendaftaran_kompetisi WHERE id_user = @id_user AND id_kompetisi = @id_kompetisi";

                using (var cmdCek = new NpgsqlCommand(queryCek, conn, transaction))
                {
                    cmdCek.Parameters.AddWithValue("@id_user", idUser);
                    cmdCek.Parameters.AddWithValue("@id_kompetisi", idKompetisi);

                    var result = cmdCek.ExecuteScalar();
                    if (result != null)
                    {
                        idPendaftaranReal = Convert.ToInt32(result);
                    }
                }

                if (idPendaftaranReal == 0)
                {
                    string queryInsertDaftar = @"INSERT INTO pendaftaran_kompetisi (id_user, id_kompetisi, status_pendaftaran) 
                                         VALUES (@id_user, @id_kompetisi, @status) 
                                         RETURNING id_pendaftaran_kompetisi";

                    using var cmdInsertDaftar = new NpgsqlCommand(queryInsertDaftar, conn, transaction);
                    cmdInsertDaftar.Parameters.AddWithValue("@id_user", idUser);
                    cmdInsertDaftar.Parameters.AddWithValue("@id_kompetisi", idKompetisi);
                    cmdInsertDaftar.Parameters.AddWithValue("@status", "terdaftar");

                    idPendaftaranReal = Convert.ToInt32(cmdInsertDaftar.ExecuteScalar());
                }

                string queryTransaksi = @"INSERT INTO transaksi (id_pendaftaran_kompetisi, id_metode_pembayaran, nominal_transaksi, status_transaksi, tanggal_transaksi) 
                                  VALUES (@id_pendaftaran, @id_metode, @nominal, @status, @tanggal);";

                using (NpgsqlCommand cmdTrx = new NpgsqlCommand(queryTransaksi, conn, transaction))
                {
                    cmdTrx.Parameters.AddWithValue("@id_pendaftaran", idPendaftaranReal);
                    cmdTrx.Parameters.AddWithValue("@id_metode", trx.IdMetodePembayaran);
                    cmdTrx.Parameters.AddWithValue("@nominal", trx.NominalTransaksi);
                    cmdTrx.Parameters.AddWithValue("@status", trx.StatusTransaksi);
                    cmdTrx.Parameters.AddWithValue("@tanggal", trx.TanggalTransaksi);

                    cmdTrx.ExecuteNonQuery();
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Gagal memproses transaksi (Model): {ex.Message}", ex);
            }
        }

       

        public DataTable AmbilSemuaPembayaran()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT t.id_transaksi AS ""ID Transaksi"", u.username AS ""Username Pemain"",
                                    k.nama_kompetisi AS ""Nama Turnamen"", m.nama_metode_pembayaran AS ""Metode"",
                                    t.nominal_transaksi AS ""Nominal"", t.status_transaksi AS ""Status"", t.tanggal_transaksi AS ""Tanggal""
                             FROM transaksi t
                             JOIN pendaftaran_kompetisi pk ON t.id_pendaftaran_kompetisi = pk.id_pendaftaran_kompetisi
                             JOIN users u ON pk.id_user = u.id_user
                             JOIN kompetisi k ON pk.id_kompetisi = k.id_kompetisi
                             JOIN metode_pembayaran m ON t.id_metode_pembayaran = m.id_metode_pembayaran
                             ORDER BY t.id_transaksi ASC;";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                using var da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil data transaksi: {ex.Message}", ex);
            }
            return dt;
        }
    }
}