using Npgsql;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProjekPBO_PSQL.Models.Context
{
    public class KompetisiContext
    {
        public bool TambahTournament(Tournament tournament)
        {
            string query = @"INSERT INTO kompetisi (id_user_pembuat, nama_kompetisi, mode_kompetisi, harga_pendaftaran, pelaksanaan_pendaftaran, tanggal_pelaksanaan, hadiah, sistem_pertandingan, jumlah_babak) 
                     VALUES (@idUser, @nama, @mode, @harga, @pelaksanaanDaftar, @tanggalLaksana, @hadiah, @sistemPertandingan, @babak)";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@idUser", tournament.IdUser);
                cmd.Parameters.AddWithValue("@nama", tournament.NamaKompetisi);
                cmd.Parameters.AddWithValue("@mode", tournament.ModeKompetisi);
                cmd.Parameters.AddWithValue("@harga", tournament.HargaPendaftaran);
                cmd.Parameters.AddWithValue("@pelaksanaanDaftar", tournament.PelaksanaanPendaftaran);
                cmd.Parameters.AddWithValue("@tanggalLaksana", tournament.TanggalPelaksanaan.Date);
                cmd.Parameters.AddWithValue("@hadiah", tournament.Hadiah);
                cmd.Parameters.AddWithValue("@sistemPertandingan", tournament.SistemPertandingan ?? "Sistem Swiss");
                cmd.Parameters.AddWithValue("@babak", tournament.JumlahBabak);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menyimpan ke tabel Tournament: {ex.Message}", ex);
            }
        }
        public bool EditTournament(Tournament tournament)
        {
            string query = @"UPDATE kompetisi 
                             SET nama_kompetisi = @nama, 
                                 mode_kompetisi = @mode, 
                                 harga_pendaftaran = @harga, 
                                 pelaksanaan_pendaftaran = @pelaksanaanDaftar, 
                                 tanggal_pelaksanaan = @tanggalLaksana, 
                                 hadiah = @hadiah, 
                                 sistem_pertandingan = @sistemPertandingan, 
                                 jumlah_babak = @babak
                             WHERE id_kompetisi = @idKompetisi";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nama", tournament.NamaKompetisi);
                cmd.Parameters.AddWithValue("@mode", tournament.ModeKompetisi);
                cmd.Parameters.AddWithValue("@harga", tournament.HargaPendaftaran);
                cmd.Parameters.AddWithValue("@pelaksanaanDaftar", tournament.PelaksanaanPendaftaran);
                cmd.Parameters.AddWithValue("@tanggalLaksana", tournament.TanggalPelaksanaan.Date);
                cmd.Parameters.AddWithValue("@hadiah", tournament.Hadiah);
                cmd.Parameters.AddWithValue("@sistemPertandingan", tournament.SistemPertandingan ?? "Sistem Swiss");
                cmd.Parameters.AddWithValue("@babak", tournament.JumlahBabak);
                cmd.Parameters.AddWithValue("@idKompetisi", tournament.IdKompetisi);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengupdate data Tournament: {ex.Message}", ex);
            }
        }

        public DataTable AmbilSemuaTournament()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT id_kompetisi, nama_kompetisi, mode_kompetisi, harga_pendaftaran, 
                                    pelaksanaan_pendaftaran, tanggal_pelaksanaan, hadiah, sistem_pertandingan, jumlah_babak 
                             FROM kompetisi 
                             ORDER BY id_kompetisi ASC";
            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil semua data tournament: {ex.Message}", ex);
            }
        }

        public DataTable AmbilSemuaKompetisi()
        {
            DataTable dt = new DataTable();
            string query = "SELECT id_kompetisi, nama_kompetisi FROM kompetisi ORDER BY id_kompetisi ASC;";

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
                throw new Exception($"Gagal mengambil daftar kompetisi: {ex.Message}", ex);
            }
            return dt;
        }



        public bool IsBabakSudahGenerated(int idKompetisi, int babak)
        {
            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                string query = "SELECT COUNT(*) FROM pertandingan WHERE id_kompetisi = @idKompetisi AND babak = @babak";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                cmd.Parameters.AddWithValue("@babak", babak);

                long count = Convert.ToInt64(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal cek status babak: {ex.Message}", ex);
            }
        }
        public bool ApakahSudahAdaPendaftar(int idKompetisi)
        {
            // Hitung jumlah baris pendaftaran untuk id_kompetisi ini
            string query = "SELECT COUNT(*) FROM pendaftaran_kompetisi WHERE id_kompetisi = @idKompetisi";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);

                long count = Convert.ToInt64(cmd.ExecuteScalar());

                // Jika count > 0, artinya sudah ada yang daftar (return true)
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengecek pendaftar kompetisi: {ex.Message}", ex);
            }
        }
        public bool SimpanPertandinganGenerate(int idKompetisi, int babak, List<Tuple<int, int>> pasangan)
        {
            using var conn = DBHelper.GetConnection();
            conn.Open();
            using var trans = conn.BeginTransaction();
            try
            {
                // DISINKRONKAN: Menambahkan kolom tanggal_pertandingan agar teratur sesuai skema database
                string query = @"INSERT INTO pertandingan (id_kompetisi, tanggal_pertandingan, babak, pemain_putih, pemain_hitam, skor_putih, skor_hitam, hasil)  
                                 VALUES (@idKompetisi, @tanggal, @babak, @putih, @hitam, 0.00, 0.00, 'Belum Dimainkan')";

                foreach (var pair in pasangan)
                {
                    using var cmd = new NpgsqlCommand(query, conn, trans);
                    cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                    cmd.Parameters.AddWithValue("@tanggal", DateTime.Today);
                    cmd.Parameters.AddWithValue("@babak", babak);
                    cmd.Parameters.AddWithValue("@putih", pair.Item1);
                    cmd.Parameters.AddWithValue("@hitam", pair.Item2);
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception($"Gagal menyimpan data generate matchmaking: {ex.Message}", ex);
            }
        }

        public bool ApakahSemuaPertandinganSelesai(int idKompetisi, int babak)
        {
            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                string query = @"SELECT COUNT(*) FROM pertandingan 
                                 WHERE id_kompetisi = @idKompetisi AND babak = @babak AND (hasil = 'Belum Dimainkan' OR hasil IS NULL OR hasil = '')";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                cmd.Parameters.AddWithValue("@babak", babak);

                long count = Convert.ToInt64(cmd.ExecuteScalar());
                return count == 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal cek status penyelesaian babak: {ex.Message}", ex);
            }
        }

        public DataTable GetDaftarKompetisiAktif()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM v_daftar_kompetisi WHERE tanggal_pelaksanaan >= CURRENT_DATE ORDER BY tanggal_pelaksanaan ASC";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil data kompetisi aktif: {ex.Message}", ex);
            }
        }

        public DataTable AmbilIdDanNamaTournament()
        {
            DataTable dt = new DataTable();
            string query = "SELECT id_kompetisi, nama_kompetisi FROM kompetisi ORDER BY id_kompetisi ASC";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil list id dan nama turnamen: {ex.Message}", ex);
            }
            return dt;
        }

        public DataTable AmbilPendaftarBerdasarkanTournament(int idKompetisi)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT pk.id_pendaftaran_kompetisi, du.nama_lengkap, du.negara, du.elo_rating, pk.status_pendaftaran
                             FROM pendaftaran_kompetisi pk
                             JOIN detail_user du ON pk.id_user = du.id_user
                             WHERE pk.id_kompetisi = @idKompetisi";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil data pendaftar: {ex.Message}", ex);
            }
            return dt;
        }

        public int AmbilTotalBabakTournament(int idKompetisi)
        {
            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                string query = "SELECT jumlah_babak FROM kompetisi WHERE id_kompetisi = @idKompetisi";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil total babak: {ex.Message}", ex);
            }
        }

        public List<int> AmbilPemainTerdaftar(int idKompetisi)
        {
            List<int> listPemain = new List<int>();
            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();

                string query = @"SELECT id_user FROM pendaftaran_kompetisi 
                                 WHERE id_kompetisi = @idKompetisi AND status_pendaftaran = 'terdaftar'";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listPemain.Add(reader.GetInt32(0));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil list pemain terdaftar: {ex.Message}", ex);
            }
            return listPemain;
        }

        public DataTable AmbilLeaderboardTournament(int idKompetisi)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT ROW_NUMBER() OVER(ORDER BY COALESCE(skor_total.total_poin, 0) DESC) AS Peringkat,
                                    du.nama_lengkap AS Nama_Pemain, 
                                    du.negara AS Asal_Negara,
                                    COALESCE(skor_total.total_poin, 0) AS Total_Poin
                             FROM pendaftaran_kompetisi pk
                             JOIN detail_user du ON pk.id_user = du.id_user
                             LEFT JOIN (
                                 SELECT id_user, SUM(poin) AS total_poin
                                 FROM (
                                     SELECT pemain_putih AS id_user, skor_putih AS poin FROM pertandingan WHERE id_kompetisi = @idKompetisi
                                     UNION ALL
                                     SELECT pemain_hitam AS id_user, skor_hitam AS poin FROM pertandingan WHERE id_kompetisi = @idKompetisi
                                 ) AS seluruh_match
                                 GROUP BY id_user
                             ) AS skor_total ON pk.id_user = skor_total.id_user
                             WHERE pk.id_kompetisi = @idKompetisi
                             ORDER BY Total_Poin DESC";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal memuat leaderboard: {ex.Message}", ex);
            }
            return dt;
        }
    }
}