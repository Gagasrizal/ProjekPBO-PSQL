using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.Data;
using System.Windows.Forms;
using ProjekPBO_PSQL.Helpers;
using System.Security.Cryptography;

namespace ProjekPBO_PSQL.Models.Context
{

    public class Detail_UserContext
{
    private readonly DBHelper dbHelper = new DBHelper();

        // 1.Validasi cek apakah username sudah terpakai
        public bool IsUsernameExists(string username)
        {
            using var conn = dbHelper.GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM users WHERE username = @username";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }
        //                   2.Validasi cek apakah email sudah terpakai
        public bool IsEmailExists(string email)
        {
            using var conn = dbHelper.GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM users WHERE email = @email";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", email);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        //3.Validasi diletakkan di sini karena mengecek tabel detail_user
        public bool IsNoTeleponExists(string noTelepon)
        {
            if (string.IsNullOrEmpty(noTelepon))
                return false;

            using var conn = dbHelper.GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM detail_user WHERE no_telepon = @no_telepon";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@no_telepon", noTelepon);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        //4.Proses autentikasi login user
        public User AuthenticateUser(string username, string password)
        {
            using var conn = dbHelper.GetConnection();
            conn.Open();

            string query = @"SELECT id_user, username, passwords, email, is_admin 
                             FROM users 
                             WHERE username = @username AND passwords = @password";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int id = reader.GetInt32(0);
                string dbUsername = reader.GetString(1);
                string dbPassword = reader.GetString(2);
                string email = reader.GetString(3);
                bool isAdmin = reader.GetBoolean(4);

                if (isAdmin)
                {
                    return new Admin(id, dbUsername, dbPassword, email);
                }

                else
                {
                    Detail_User profilPemain = GetDetailUserByUserId(id);

                    return new Pemain(id, dbUsername, dbPassword, email, profilPemain);

                }


               
            }

            return null;
        }
        


        //6. Mengambil profil lengkap satu user berdasarkan ID nya
        public Detail_User GetDetailUserByUserId(int idUser)
        {
            using var conn = dbHelper.GetConnection();
            conn.Open();

            string query = @"SELECT id_detail_user, id_user, nama_lengkap, negara, no_telepon, tanggal_lahir, elo_rating, created_at, deskripsi 
                             FROM detail_user 
                             WHERE id_user = @id_user";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id_user", idUser);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Detail_User(
                    reader.GetInt32(0),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetDateTime(5),
                    reader.GetInt32(6),
                    reader.GetDateTime(7),
                    reader.IsDBNull(8) ? "" : reader.GetString(8)
                );
            }
            return null;
        }

        // 7. GROUP BY: Mengambil papan peringkat (Leaderboard) total poin para user
        public DataTable AmbilLeaderboardTournament(int idKompetisi) //groupby
        {
            DataTable dt = new DataTable();
            using var conn =dbHelper.GetConnection();
            conn.Open();

            string query = @"SELECT ROW_NUMBER() OVER(ORDER BY COALESCE(skor_total.total_poin, 0) DESC) AS Peringkat,
                            du.nama_lengkap AS Nama_Pemain, 
                            du.negara AS Asal_Negara,
                            COALESCE(skor_total.total_poin, 0) AS Total_Poin
                     FROM pendaftaran_kompetisi pk
                     JOIN detail_user du ON pk.id_user = du.id_user
                     LEFT JOIN (
                         SELECT id_user, SUM(poin) AS total_poin
                         FROM (
                             SELECT pemain_putih AS id_user, skor_putih AS poin FROM pertandingan WHERE id_kompetisi = @id_kompetisi
                             UNION ALL
                             SELECT pemain_hitam AS id_user, skor_hitam AS poin FROM pertandingan WHERE id_kompetisi = @id_kompetisi
                         ) AS seluruh_match
                         GROUP BY id_user
                     ) AS skor_total ON pk.id_user = skor_total.id_user
                     WHERE pk.id_kompetisi = @id_kompetisi
                     ORDER BY Total_Poin DESC";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);

                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Gagal memuat leaderboard: {ex.Message}");
            }
            return dt;
        }



        public List<int> AmbilPemainTerdaftar(int idKompetisi)
        {
            List<int> listPemain = new List<int>();
            using var conn = dbHelper.GetConnection();
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
            return listPemain;
        }
    }
}





