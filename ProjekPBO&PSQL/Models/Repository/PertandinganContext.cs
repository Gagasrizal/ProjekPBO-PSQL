using Npgsql;
using ProjekPBO_PSQL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProjekPBO_PSQL.Models.Context
{
    public class PertandinganContext
    {
        public DataTable AmbilPertandinganPerBabak(int idKompetisi, int babak)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT p.id_pertandingan AS ""ID"", p.babak AS ""Babak"",
                                    COALESCE(u1.nama_lengkap, 'ID User: ' || p.pemain_putih) AS ""Pemain Putih"",
                                    COALESCE(u2.nama_lengkap, 'ID User: ' || p.pemain_hitam) AS ""Pemain Hitam"",
                                    p.skor_putih AS ""Skor Putih"", p.skor_hitam AS ""Skor Hitam"", p.hasil AS ""Hasil""
                             FROM pertandingan p
                             LEFT JOIN detail_user u1 ON p.pemain_putih = u1.id_user
                             LEFT JOIN detail_user u2 ON p.pemain_hitam = u2.id_user
                             WHERE p.id_kompetisi = @id_kompetisi AND p.babak = @babak
                             ORDER BY p.id_pertandingan ASC;";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);
                cmd.Parameters.AddWithValue("@babak", babak);
                using var da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memuat tabel pertandingan: {ex.Message}");
            }
            return dt;
        }

        public DataTable AmbilPertandinganDenganTotalPoin(int idKompetisi, int babak)
        {
            DataTable dt = new DataTable();
            string query = @"WITH akumulasi_poin AS (
                                SELECT id_user, COALESCE(SUM(poin), 0) AS total_poin
                                FROM (
                                    SELECT pemain_putih AS id_user, skor_putih AS poin FROM pertandingan WHERE id_kompetisi = @id_kompetisi AND hasil IS NOT NULL AND hasil <> 'Belum Dimainkan' AND hasil <> ''
                                    UNION ALL
                                    SELECT pemain_hitam AS id_user, skor_hitam AS poin FROM pertandingan WHERE id_kompetisi = @id_kompetisi AND hasil IS NOT NULL AND hasil <> 'Belum Dimainkan' AND hasil <> ''
                                ) sub
                                GROUP BY id_user
                             )
                             SELECT p.id_pertandingan, p.babak,
                                    COALESCE(dh.nama_lengkap, 'ID: ' || p.pemain_hitam::text) || ' (' || COALESCE(ah.total_poin, 0)::text || ')' AS pemain_hitam_label,
                                    COALESCE(dp.nama_lengkap, 'ID: ' || p.pemain_putih::text) || ' (' || COALESCE(ap.total_poin, 0)::text || ')' AS pemain_putih_label,
                                    p.hasil
                             FROM pertandingan p
                             LEFT JOIN detail_user dh   ON p.pemain_hitam = dh.id_user
                             LEFT JOIN detail_user dp   ON p.pemain_putih = dp.id_user
                             LEFT JOIN akumulasi_poin ah ON p.pemain_hitam = ah.id_user
                             LEFT JOIN akumulasi_poin ap ON p.pemain_putih = ap.id_user
                             WHERE p.id_kompetisi = @id_kompetisi AND p.babak = @babak
                             ORDER BY p.id_pertandingan ASC;";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);
                cmd.Parameters.AddWithValue("@babak", babak);
                using var da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memuat pertandingan dengan poin: {ex.Message}");
            }
            return dt;
        }

        public bool IsBabakSudahGenerated(int idKompetisi, int babak)
        {
            using var conn = DBHelper.GetConnection();
            conn.Open();
            string query = "SELECT COUNT(*) FROM pertandingan WHERE id_kompetisi = @id AND babak = @babak";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", idKompetisi);
            cmd.Parameters.AddWithValue("@babak", babak);

            long count = Convert.ToInt64(cmd.ExecuteScalar());
            return count > 0;
        }

        public bool SimpanPertandinganGenerate(int idKompetisi, int babak, List<Tuple<int, int>> pasanganMatch)
        {
            using var conn = DBHelper.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                string query = @"INSERT INTO pertandingan (id_kompetisi, tanggal_pertandingan, babak, pemain_putih, pemain_hitam, skor_putih, skor_hitam, hasil)  
                                 VALUES (@idKompetisi, @tanggal, @babak, @putih, @hitam, 0.00, 0.00, 'Belum Dimainkan')";

                foreach (var match in pasanganMatch)
                {
                    using var cmd = new NpgsqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                    cmd.Parameters.AddWithValue("@tanggal", DateTime.Today);
                    cmd.Parameters.AddWithValue("@babak", babak);
                    cmd.Parameters.AddWithValue("@putih", match.Item1);
                    cmd.Parameters.AddWithValue("@hitam", match.Item2);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                System.Windows.Forms.MessageBox.Show($"Gagal menyimpan generate match: {ex.Message}");
                return false;
            }
        }

        public bool UpdateHasilPertandingan(int idPertandingan, string hasil)
        {
            double skorPutih = 0, skorHitam = 0;
            if (hasil == "1-0") { skorPutih = 1.0; skorHitam = 0.0; }
            else if (hasil == "0-1") { skorPutih = 0.0; skorHitam = 1.0; }
            else if (hasil == "1/2-1/2") { skorPutih = 0.5; skorHitam = 0.5; }

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();

                using var trans = conn.BeginTransaction();

                string queryGet = @"SELECT p.pemain_putih, p.pemain_hitam, p.hasil AS hasil_lama,
                                   COALESCE(dp.elo_rating, 1000) AS elo_putih, 
                                   COALESCE(dh.elo_rating, 1000) AS elo_hitam
                            FROM pertandingan p
                            JOIN detail_user dp ON p.pemain_putih = dp.id_user
                            JOIN detail_user dh ON p.pemain_hitam = dh.id_user
                            WHERE p.id_pertandingan = @id_pertandingan";

                int idPemainPutih = 0, idPemainHitam = 0;
                int eloPutihLama = 1000, eloHitamLama = 1000;
                string hasilLama = "";

                using (var cmdGet = new NpgsqlCommand(queryGet, conn, trans))
                {
                    cmdGet.Parameters.AddWithValue("@id_pertandingan", idPertandingan);
                    using var reader = cmdGet.ExecuteReader();
                    if (reader.Read())
                    {
                        idPemainPutih = Convert.ToInt32(reader["pemain_putih"]);
                        idPemainHitam = Convert.ToInt32(reader["pemain_hitam"]);
                        eloPutihLama = Convert.ToInt32(reader["elo_putih"]);
                        eloHitamLama = Convert.ToInt32(reader["elo_hitam"]);
                        hasilLama = reader["hasil_lama"].ToString();
                    }
                    else
                    {
                        return false;
                    }
                }

                string queryUpdateMatch = @"UPDATE pertandingan 
                                   SET skor_putih = @skor_putih, skor_hitam = @skor_hitam, hasil = @hasil 
                                   WHERE id_pertandingan = @id_pertandingan";

                using (var cmdMatch = new NpgsqlCommand(queryUpdateMatch, conn, trans))
                {
                    cmdMatch.Parameters.AddWithValue("@skor_putih", skorPutih);
                    cmdMatch.Parameters.AddWithValue("@skor_hitam", skorHitam);
                    cmdMatch.Parameters.AddWithValue("@hasil", hasil);
                    cmdMatch.Parameters.AddWithValue("@id_pertandingan", idPertandingan);
                    cmdMatch.ExecuteNonQuery();
                }

                if (hasilLama == "*" || hasilLama == "Belum Dimainkan" || string.IsNullOrEmpty(hasilLama))
                {
                    int eloPutihBaru = eloPutihLama;
                    int eloHitamBaru = eloHitamLama;

                    if (hasil == "1-0")
                    {
                        eloPutihBaru = eloPutihLama + 8;
                        eloHitamBaru = Math.Max(0, eloHitamLama - 8); 
                    }
                    else if (hasil == "0-1") // Hitam Menang, Putih Kalah
                    {
                        eloPutihBaru = Math.Max(0, eloPutihLama - 8);
                        eloHitamBaru = eloHitamLama + 8;
                    }

                    string queryUpdatePutih = "UPDATE detail_user SET elo_rating = @elo WHERE id_user = @id_user";
                    using (var cmdPutih = new NpgsqlCommand(queryUpdatePutih, conn, trans))
                    {
                        cmdPutih.Parameters.AddWithValue("@elo", eloPutihBaru);
                        cmdPutih.Parameters.AddWithValue("@id_user", idPemainPutih);
                        cmdPutih.ExecuteNonQuery();
                    }

                    string queryUpdateHitam = "UPDATE detail_user SET elo_rating = @elo WHERE id_user = @id_user";
                    using (var cmdHitam = new NpgsqlCommand(queryUpdateHitam, conn, trans))
                    {
                        cmdHitam.Parameters.AddWithValue("@elo", eloHitamBaru);
                        cmdHitam.Parameters.AddWithValue("@id_user", idPemainHitam);
                        cmdHitam.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengupdate hasil pertandingan dan rating: {ex.Message}");
            }
        }

        public bool ApakahSemuaPertandinganSelesai(int idKompetisi, int babak)
        {
            string query = @"SELECT COUNT(*) FROM pertandingan 
                             WHERE id_kompetisi = @id_kompetisi AND babak = @babak 
                               AND (hasil IS NULL OR hasil = '' OR hasil = 'Belum Dimainkan');";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);
                cmd.Parameters.AddWithValue("@babak", babak);

                long hitungKosong = Convert.ToInt64(cmd.ExecuteScalar());
                return hitungKosong == 0;
            }
            catch
            {
                return false;
            }
        }

        public (string NamaPutih, int EloPutih, string NamaHitam, int EloHitam) AmbilEloSetelahUpdate(int idPertandingan)
        {
            string query = @"SELECT dp.nama_lengkap AS nama_putih, dp.elo_rating AS elo_putih,
                                    dh.nama_lengkap AS nama_hitam, dh.elo_rating AS elo_hitam
                             FROM pertandingan p
                             JOIN detail_user dp ON p.pemain_putih = dp.id_user
                             JOIN detail_user dh ON p.pemain_hitam = dh.id_user
                             WHERE p.id_pertandingan = @id";

            using var conn = DBHelper.GetConnection();
            conn.Open();
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", idPertandingan);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return (
                    reader["nama_putih"].ToString(),
                    Convert.ToInt32(reader["elo_putih"]),
                    reader["nama_hitam"].ToString(),
                    Convert.ToInt32(reader["elo_hitam"])
                );
            }
            return ("?", 0, "?", 0);
        }
        public List<int> AmbilDaftarPemainTournament(int idKompetisi)
        {
            List<int> listPemain = new List<int>();

            string query = @"SELECT id_user FROM pendaftaran_kompetisi 
                     WHERE id_kompetisi = @idKompetisi 
                     AND (LOWER(status_pendaftaran) = 'terdaftar' 
                          OR LOWER(status_pendaftaran) = 'lunas' 
                          OR status_pendaftaran IS NOT NULL)";
            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listPemain.Add(Convert.ToInt32(reader["id_user"]));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil daftar pemain aktif: {ex.Message}");
            }

            return listPemain;
        }

        public DataTable AmbilHistoryPertandingan(int idUser)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT k.nama_kompetisi AS ""Tournament"", p.babak AS ""Babak"",
                            CASE WHEN p.pemain_putih = @id_user THEN COALESCE(lawan.nama_lengkap, 'Unknown') ELSE COALESCE(lawan2.nama_lengkap, 'Unknown') END AS ""Lawan"",
                            CASE WHEN p.pemain_putih = @id_user THEN 'Putih' ELSE 'Hitam' END AS ""Warna"",
                            CASE WHEN p.hasil = 'Belum Dimainkan' THEN 'Belum Dimainkan'
                                 WHEN p.pemain_putih = @id_user AND p.hasil = '1-0' THEN 'Menang'
                                 WHEN p.pemain_putih = @id_user AND p.hasil = '0-1' THEN 'Kalah'
                                 WHEN p.pemain_hitam = @id_user AND p.hasil = '0-1' THEN 'Menang' -- FIX: Ditambahkan 'AND' di sini
                                 WHEN p.pemain_hitam = @id_user AND p.hasil = '1-0' THEN 'Kalah'
                                 WHEN p.hasil = '1/2-1/2' THEN 'Remis' ELSE p.hasil END AS ""Hasil""
                     FROM pertandingan p
                     JOIN kompetisi k ON p.id_kompetisi = k.id_kompetisi
                     LEFT JOIN detail_user lawan  ON p.pemain_hitam = lawan.id_user
                     LEFT JOIN detail_user lawan2 ON p.pemain_putih = lawan2.id_user
                     WHERE p.pemain_putih = @id_user OR p.pemain_hitam = @id_user
                     ORDER BY k.nama_kompetisi, p.babak ASC;";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_user", idUser);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil history pertandingan: {ex.Message}");
            }
            return dt;
        }
    }
}