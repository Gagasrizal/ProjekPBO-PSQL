using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Controller;

namespace ProjekPBO_PSQL.Models.Context
{
    public class KompetisiContext
    {
        public DataTable GetSemuaKompetisi()
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            k.id_kompetisi,
                            k.id_user_pembuat,
                            k.nama_kompetisi,
                            k.mode_kompetisi,
                            k.harga_pendaftaran,
                            k.pelaksanaan_pendaftaran,
                            k.tanggal_pelaksanaan,
                            k.hadiah,
                            k.sistem_pertandingan,
                            k.jumlah_babak
                        FROM kompetisi k
                        ORDER BY k.id_kompetisi ASC";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal mengambil data kompetisi: " + ex.Message);
                }
            }

            return dt;
        }
        public Kompetisi GetKompetisiById(int idKompetisi)
        {
            Kompetisi kompetisi = null;

            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            id_kompetisi,
                            id_user_pembuat,
                            nama_kompetisi,
                            mode_kompetisi,
                            harga_pendaftaran,
                            pelaksanaan_pendaftaran,
                            tanggal_pelaksanaan,
                            hadiah,
                            sistem_pertandingan,
                            jumlah_babak
                        FROM kompetisi
                        WHERE id_kompetisi = @id_kompetisi";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                kompetisi = new Kompetisi
                                {
                                    IdKompetisi = reader.GetInt32(0),
                                    IdUser = reader.GetInt32(1),
                                    NamaKompetisi = reader.GetString(2),
                                    ModeKompetisi = reader.GetString(3),
                                    HargaPendaftaran = reader.GetInt32(4),
                                    PelaksanaanPendaftaran = reader.GetString(5),
                                    TanggalPelaksanaan = reader.GetDateTime(6),
                                    Hadiah = reader.GetInt32(7),
                                    SistemPertandingan = reader.GetString(8),
                                    JumlahBabak = reader.GetInt32(9)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal mengambil data kompetisi: " + ex.Message);
                }
            }

            return kompetisi;
        }
        public void TambahKompetisi(Kompetisi kompetisi)
        {
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO kompetisi 
                            (id_user_pembuat, nama_kompetisi, mode_kompetisi, 
                             harga_pendaftaran, pelaksanaan_pendaftaran, 
                             tanggal_pelaksanaan, hadiah, sistem_pertandingan, jumlah_babak)
                        VALUES 
                            (@id_user_pembuat, @nama_kompetisi, @mode_kompetisi, 
                             @harga_pendaftaran, @pelaksanaan_pendaftaran, 
                             @tanggal_pelaksanaan, @hadiah, @sistem_pertandingan, @jumlah_babak)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_user_pembuat", kompetisi.IdUser);
                        cmd.Parameters.AddWithValue("@nama_kompetisi", kompetisi.NamaKompetisi);
                        cmd.Parameters.AddWithValue("@mode_kompetisi", kompetisi.ModeKompetisi);
                        cmd.Parameters.AddWithValue("@harga_pendaftaran", kompetisi.HargaPendaftaran);
                        cmd.Parameters.AddWithValue("@pelaksanaan_pendaftaran", kompetisi.PelaksanaanPendaftaran);
                        cmd.Parameters.AddWithValue("@tanggal_pelaksanaan", kompetisi.TanggalPelaksanaan);
                        cmd.Parameters.AddWithValue("@hadiah", kompetisi.Hadiah);
                        cmd.Parameters.AddWithValue("@sistem_pertandingan", kompetisi.SistemPertandingan);
                        cmd.Parameters.AddWithValue("@jumlah_babak", kompetisi.JumlahBabak);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal menambah kompetisi: " + ex.Message);
                }
            }
        }

        public void UpdateKompetisi(Kompetisi kompetisi)
        {
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        UPDATE kompetisi SET
                            nama_kompetisi          = @nama_kompetisi,
                            mode_kompetisi          = @mode_kompetisi,
                            harga_pendaftaran       = @harga_pendaftaran,
                            pelaksanaan_pendaftaran = @pelaksanaan_pendaftaran,
                            tanggal_pelaksanaan     = @tanggal_pelaksanaan,
                            hadiah                  = @hadiah,
                            sistem_pertandingan     = @sistem_pertandingan,
                            jumlah_babak            = @jumlah_babak
                        WHERE id_kompetisi = @id_kompetisi";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_kompetisi", kompetisi.IdKompetisi);
                        cmd.Parameters.AddWithValue("@nama_kompetisi", kompetisi.NamaKompetisi);
                        cmd.Parameters.AddWithValue("@mode_kompetisi", kompetisi.ModeKompetisi);
                        cmd.Parameters.AddWithValue("@harga_pendaftaran", kompetisi.HargaPendaftaran);
                        cmd.Parameters.AddWithValue("@pelaksanaan_pendaftaran", kompetisi.PelaksanaanPendaftaran);
                        cmd.Parameters.AddWithValue("@tanggal_pelaksanaan", kompetisi.TanggalPelaksanaan);
                        cmd.Parameters.AddWithValue("@hadiah", kompetisi.Hadiah);
                        cmd.Parameters.AddWithValue("@sistem_pertandingan", kompetisi.SistemPertandingan);
                        cmd.Parameters.AddWithValue("@jumlah_babak", kompetisi.JumlahBabak);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal mengupdate kompetisi: " + ex.Message);
                }
            }
        }

        public DataTable TampilkanPesertaTurnamen(int idKompetisi)
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT id_user, nama_lengkap, elo_rating 
                        FROM detail_user 
                        WHERE id_user IN (
                            SELECT id_user FROM pendaftaran WHERE id_kompetisi = @id_kompetisi
                        )";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);

                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal mengambil data peserta: " + ex.Message);
                }
            }

            return dt;
        }
    }
}
