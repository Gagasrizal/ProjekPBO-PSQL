using Npgsql;
using ProjekPBO_PSQL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProjekPBO_PSQL.Models.Context
{
    public class PertandinganContext
    {
        public DataTable GetAllKompetisi()
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT id_kompetisi, nama_kompetisi, jumlah_babak FROM kompetisi ORDER BY nama_kompetisi";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        public int GetJumlahBabak(int idKompetisi)
        {
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT jumlah_babak FROM kompetisi WHERE id_kompetisi = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idKompetisi);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public DataTable GetPertandinganByBabak(int idKompetisi, int babak)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT
                        p.id_pertandingan,
                        p.pemain_hitam        AS id_pemain_hitam,
                        p.pemain_putih        AS id_pemain_putih,
                        dh.nama_lengkap || ' (' || dh.elo_rating || ')' AS pemain_hitam,
                        dp.nama_lengkap || ' (' || dp.elo_rating || ')' AS pemain_putih,
                        CASE
                            WHEN p.hasil = ''       THEN 'Belum Dimainkan'
                            ELSE p.hasil
                        END AS hasil_babak_ini
                    FROM pertandingan p
                    JOIN detail_user dp ON p.pemain_putih = dp.id_user
                    JOIN detail_user dh ON p.pemain_hitam = dh.id_user
                    WHERE p.id_kompetisi = @idKompetisi AND p.babak = @babak
                    ORDER BY p.id_pertandingan";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                    cmd.Parameters.AddWithValue("@babak", babak);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            return dt;
        }

        public bool IsBabakSudahDibuat(int idKompetisi, int babak)
        {
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT COUNT(1) FROM pertandingan
                                 WHERE id_kompetisi = @idKompetisi AND babak = @babak";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                    cmd.Parameters.AddWithValue("@babak", babak);
                    return Convert.ToInt64(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool IsBabakSebelumnyaSelesai(int idKompetisi, int babak)
        {
            if (babak == 1) return true;

            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT COUNT(1) FROM pertandingan
                                 WHERE id_kompetisi = @idKompetisi
                                 AND babak = @babakSebelumnya
                                 AND hasil = ''";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                    cmd.Parameters.AddWithValue("@babakSebelumnya", babak - 1);
                    return Convert.ToInt64(cmd.ExecuteScalar()) == 0;
                }
            }
        }

        public List<(int idUser, string nama, int elo)> GetPemainKompetisi(int idKompetisi)
        {
            var list = new List<(int, string, int)>();
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT u.id_user, du.nama_lengkap, du.elo_rating
                    FROM pendaftaran_kompetisi pk
                    JOIN users u ON pk.id_pemain = u.id_user
                    JOIN detail_user du ON u.id_user = du.id_user
                    WHERE pk.id_kompetisi = @idKompetisi
                    ORDER BY du.elo_rating DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add((reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
            }
            return list;
        }


        public HashSet<string> GetPasanganSudahMain(int idKompetisi)
        {
            var set = new HashSet<string>();
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT pemain_putih, pemain_hitam FROM pertandingan
                                 WHERE id_kompetisi = @idKompetisi";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int p = reader.GetInt32(0);
                            int h = reader.GetInt32(1);
                            set.Add($"{Math.Min(p, h)}-{Math.Max(p, h)}");
                        }
                    }
                }
            }
            return set;
        }

        public void InsertMatchmaking(int idKompetisi, int babak,
            List<(int idPutih, int idHitam)> pasangan)
        {
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (NpgsqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var (idPutih, idHitam) in pasangan)
                        {
                            bool isBye = idHitam == -1;

                            string query = @"
                                INSERT INTO pertandingan
                                    (id_kompetisi, babak, pemain_putih, pemain_hitam,
                                     tanggal_pertandingan, skor_putih, skor_hitam, hasil)
                                VALUES
                                    (@idKompetisi, @babak, @putih, @hitam,
                                     CURRENT_DATE, @skorPutih, @skorHitam, @hasil)";

                            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                                cmd.Parameters.AddWithValue("@babak", babak);
                                cmd.Parameters.AddWithValue("@putih", idPutih);
                                cmd.Parameters.AddWithValue("@hitam", isBye ? idPutih : idHitam);
                                cmd.Parameters.AddWithValue("@skorPutih", isBye ? 1.0m : 0.0m);
                                cmd.Parameters.AddWithValue("@skorHitam", 0.0m);
                                cmd.Parameters.AddWithValue("@hasil", isBye ? "BYE" : "");
                                cmd.ExecuteNonQuery();
                            }

                            if (isBye)
                            {
                                string queryElo = @"
                                    UPDATE detail_user
                                    SET elo_rating = GREATEST(0, elo_rating + 8)
                                    WHERE id_user = @idUser";
                                using (NpgsqlCommand cmd = new NpgsqlCommand(queryElo, conn, tx))
                                {
                                    cmd.Parameters.AddWithValue("@idUser", idPutih);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public void SimpanHasil(int idPertandingan, string hasil,
            int idPutih, int idHitam)
        {
            decimal skorPutih, skorHitam;
            int eloPutih, eloHitam;

            switch (hasil)
            {
                case "1-0":
                    skorPutih = 1.0m; skorHitam = 0.0m;
                    eloPutih = 8; eloHitam = -8;
                    break;
                case "0-1":
                    skorPutih = 0.0m; skorHitam = 1.0m;
                    eloPutih = -8; eloHitam = 8;
                    break;
                case "1/2-1/2":
                    skorPutih = 0.5m; skorHitam = 0.5m;
                    eloPutih = 0; eloHitam = 0;
                    break;
                default:
                    throw new Exception("Hasil tidak valid.");
            }

            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                using (NpgsqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string queryHasil = @"
                            UPDATE pertandingan
                            SET hasil = @hasil, skor_putih = @skorPutih, skor_hitam = @skorHitam
                            WHERE id_pertandingan = @id";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryHasil, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@hasil", hasil);
                            cmd.Parameters.AddWithValue("@skorPutih", skorPutih);
                            cmd.Parameters.AddWithValue("@skorHitam", skorHitam);
                            cmd.Parameters.AddWithValue("@id", idPertandingan);
                            cmd.ExecuteNonQuery();
                        }

                        string queryElo = @"
                            UPDATE detail_user
                            SET elo_rating = GREATEST(0, elo_rating + @delta)
                            WHERE id_user = @idUser";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryElo, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@delta", eloPutih);
                            cmd.Parameters.AddWithValue("@idUser", idPutih);
                            cmd.ExecuteNonQuery();
                        }

                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryElo, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@delta", eloHitam);
                            cmd.Parameters.AddWithValue("@idUser", idHitam);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable GetLeaderboard(int idKompetisi)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT
                        ROW_NUMBER() OVER (ORDER BY du.elo_rating DESC) AS peringkat,
                        du.nama_lengkap AS pemain,
                        du.elo_rating   AS elo,
                        COUNT(CASE WHEN (p.pemain_putih = u.id_user AND p.hasil = '1-0')
                                     OR (p.pemain_hitam = u.id_user AND p.hasil = '0-1')
                                     OR p.hasil = 'BYE' AND p.pemain_putih = u.id_user
                                   THEN 1 END) AS menang,
                        COUNT(CASE WHEN p.hasil = '1/2-1/2' THEN 1 END) AS draw,
                        COUNT(CASE WHEN (p.pemain_putih = u.id_user AND p.hasil = '0-1')
                                     OR (p.pemain_hitam = u.id_user AND p.hasil = '1-0')
                                   THEN 1 END) AS kalah
                    FROM pendaftaran_kompetisi pk
                    JOIN users u         ON pk.id_pemain  = u.id_user
                    JOIN detail_user du  ON u.id_user     = du.id_user
                    LEFT JOIN pertandingan p
                        ON  p.id_kompetisi = pk.id_kompetisi
                        AND (p.pemain_putih = u.id_user OR p.pemain_hitam = u.id_user)
                        AND p.hasil <> 'BYE' OR (p.hasil = 'BYE' AND p.pemain_putih = u.id_user)
                    WHERE pk.id_kompetisi = @idKompetisi
                    GROUP BY u.id_user, du.nama_lengkap, du.elo_rating
                    ORDER BY du.elo_rating DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetHistoryPertandinganPemain(int idUser)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT
                        k.nama_kompetisi AS tournament,
                        p.babak,
                        CASE
                            WHEN p.pemain_putih = @idUser THEN dh.nama_lengkap
                            ELSE dp.nama_lengkap
                        END AS lawan,
                        CASE
                            WHEN p.pemain_putih = @idUser THEN 'Putih'
                            ELSE 'Hitam'
                        END AS warna,
                        CASE
                            WHEN p.hasil = ''        THEN 'Belum Dimainkan'
                            WHEN p.hasil = 'BYE'     THEN 'Menang (BYE)'
                            WHEN p.pemain_putih = @idUser AND p.hasil = '1-0'   THEN 'Menang'
                            WHEN p.pemain_hitam = @idUser AND p.hasil = '0-1'   THEN 'Menang'
                            WHEN p.pemain_putih = @idUser AND p.hasil = '0-1'   THEN 'Kalah'
                            WHEN p.pemain_hitam = @idUser AND p.hasil = '1-0'   THEN 'Kalah'
                            WHEN p.hasil = '1/2-1/2' THEN 'Remis'
                            ELSE p.hasil
                        END AS hasil
                    FROM pertandingan p
                    JOIN kompetisi k    ON p.id_kompetisi  = k.id_kompetisi
                    JOIN detail_user dp ON p.pemain_putih  = dp.id_user
                    JOIN detail_user dh ON p.pemain_hitam  = dh.id_user
                    WHERE p.pemain_putih = @idUser OR p.pemain_hitam = @idUser
                    ORDER BY k.nama_kompetisi, p.babak";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idUser", idUser);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            return dt;
        }
    }
}