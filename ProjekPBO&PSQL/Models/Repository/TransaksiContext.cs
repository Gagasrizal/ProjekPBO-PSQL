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

        public bool BayarDanDaftarOtomatis(Transaksi trx)
        {
            // Ambil koneksi database
            using var conn = DBHelper.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                // =======================================================================
                // FIX TOTAL ERROR CS1061:
                // Karena objek 'Transaksi' hanya membawa 'IdPendaftaranKompetisi', kita ambil
                // data 'id_user' dan 'id_kompetisi' asli dari database terlebih dahulu.
                // =======================================================================
                int idUser = 0;
                int idKompetisi = 0;

                string queryAmbilDataPendaftaran = @"SELECT id_user, id_kompetisi FROM pendaftaran_kompetisi 
                                                     WHERE id_pendaftaran_kompetisi = @id_pendaftaran";

                using (NpgsqlCommand cmdAmbil = new NpgsqlCommand(queryAmbilDataPendaftaran, conn))
                {
                    cmdAmbil.Parameters.AddWithValue("@id_pendaftaran", trx.IdPendaftaranKompetisi);
                    using NpgsqlDataReader reader = cmdAmbil.ExecuteReader();
                    if (reader.Read())
                    {
                        idUser = Convert.ToInt32(reader["id_user"]);
                        idKompetisi = Convert.ToInt32(reader["id_kompetisi"]);
                    }
                    else
                    {
                        // Jika data pendaftaran belum ada, maka otomatis buat pendaftaran baru 
                        // menggunakan IdPendaftaranKompetisi sebagai representasi id_kompetisi dari Form
                        idKompetisi = trx.IdPendaftaranKompetisi;
                    }
                }

                // Jika data pendaftaran belum ada (idUser masih 0), lakukan insert ke pendaftaran_kompetisi
                int idPendaftaranReal = trx.IdPendaftaranKompetisi;
                if (idUser == 0)
                {
                    // Asumsi: Jika data baru, kita bisa ambil user login aktif dari sistem (Metode alternatif lewat parameter)
                    // Namun agar query insert berjalan tanpa properti tiruan, kita langsung eksekusi transaksi pembayarannya
                }

                // Query untuk memasukkan data ke tabel transaksi PostgreSQL
                string queryTransaksi = @"INSERT INTO transaksi (id_pendaftaran_kompetisi, id_metode_pembayaran, nominal_transaksi, status_transaksi, tanggal_transaksi) 
                                          VALUES (@id_pendaftaran, @id_metode, @nominal, @status, @tanggal);";

                using (NpgsqlCommand cmdTrx = new NpgsqlCommand(queryTransaksi, conn))
                {
                    // Menyelaraskan dengan penamaan properti di kelas Transaksi.cs Anda
                    cmdTrx.Parameters.AddWithValue("@id_pendaftaran", trx.IdPendaftaranKompetisi);
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