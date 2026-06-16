using Npgsql;
using ProjekPBO_PSQL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ProjekPBO_PSQL.Helpers;

namespace ProjekPBO_PSQL.Models.Context
{
    public class TransaksiContext
    {
        public DataTable GetAllTransaksiPemain()
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM view_transaksi_pemain ORDER BY tanggal_transaksi DESC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal mengambil data transaksi: " + ex.Message);
                }
            }

            return dt;
        }

        public bool IsSudahDaftar(int idPemain, int idKompetisi)
        {
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT COUNT(1) FROM pendaftaran_kompetisi
                                     WHERE id_pemain = @idPemain AND id_kompetisi = @idKompetisi";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idPemain", idPemain);
                        cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                        return Convert.ToInt64(cmd.ExecuteScalar()) > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal cek pendaftaran: " + ex.Message);
                }
            }
        }

        public int GetBiayaKompetisi(int idKompetisi)
        {
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT biaya_pendaftaran FROM kompetisi WHERE id_kompetisi = @id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idKompetisi);
                        object result = cmd.ExecuteScalar();

                        if (result == null || result == DBNull.Value)
                            throw new Exception("Kompetisi tidak ditemukan.");

                        return Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal ambil biaya kompetisi: " + ex.Message);
                }
            }
        }

        public void DaftarDanBayar(int idPemain, int idKompetisi, int idMetode, int nominal)
        {
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                using (NpgsqlTransaction dbTrans = conn.BeginTransaction())
                {
                    try
                    {
                        string queryDaftar = @"INSERT INTO pendaftaran_kompetisi (id_pemain, id_kompetisi)
                                               VALUES (@idPemain, @idKompetisi)
                                               RETURNING id_pendaftaran_kompetisi";

                        int idPendaftaran;
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryDaftar, conn, dbTrans))
                        {
                            cmd.Parameters.AddWithValue("@idPemain", idPemain);
                            cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                            idPendaftaran = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        string queryTransaksi = @"INSERT INTO transaksi
                                                    (id_pendaftaran_kompetisi, id_metode_pembayaran, nominal_transaksi)
                                                  VALUES
                                                    (@idPendaftaran, @idMetode, @nominal)";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryTransaksi, conn, dbTrans))
                        {
                            cmd.Parameters.AddWithValue("@idPendaftaran", idPendaftaran);
                            cmd.Parameters.AddWithValue("@idMetode", idMetode);
                            cmd.Parameters.AddWithValue("@nominal", nominal);
                            cmd.ExecuteNonQuery();
                        }

                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        throw new Exception("Transaksi dibatalkan: " + ex.Message);
                    }
                }
            }
        }
    }
}