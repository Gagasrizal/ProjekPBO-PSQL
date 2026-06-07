using System;
using System.Data;
using Npgsql;
using ProjekPBO_PSQL.Models;

namespace ProjekPBO_PSQL.Helpers
{
    public class DBHelper
    {
        private readonly string connString = "Host=localhost;Port=5432;Database=HyperChess;Username=postgres;Password=54321";

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connString);
        }

        public bool IsUsernameExists(string username)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM users WHERE username = @username";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }
        public bool RegisterUser(User user, Detail_User detail)
        {
            using var conn = GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                // 1. INSERT KE TABEL USERS
                string userQuery = @"INSERT INTO users (username, passwords, email, is_admin)
                             VALUES (@username, @password, @email, @is_admin)
                             RETURNING id_user";

                // Di dalam DBHelper.cs -> Method RegisterUser
                using var userCmd = new NpgsqlCommand(userQuery, conn);
                userCmd.Parameters.AddWithValue("@username", user.username); // Pastikan huruf kecil 'username'
                userCmd.Parameters.AddWithValue("@password", user.password); // Pastikan huruf kecil 'password'
                userCmd.Parameters.AddWithValue("@email", user.email);       // Pastikan huruf kecil 'email'
                userCmd.Parameters.AddWithValue("@is_admin", false);

                // Eksekusi dan ambil ID yang baru digenerate secara aman
                object userIdObj = userCmd.ExecuteScalar();
                if (userIdObj == null)
                {
                    throw new Exception("Gagal mendapatkan ID User baru.");
                }
                int idUser = Convert.ToInt32(userIdObj);

                // 2. INSERT KE TABEL DETAIL_USER
                string detailQuery = @"INSERT INTO detail_user (id_user, nama_lengkap, negara, no_telepon, tanggal_lahir, elo_rating, created_at, deskripsi)
                               VALUES (@id_user, @nama_lengkap, @negara, @no_telepon, @tanggal_lahir, 1200, @created_at, @deskripsi)";

                using var detailCmd = new NpgsqlCommand(detailQuery, conn);
                detailCmd.Parameters.AddWithValue("@id_user", idUser);
                detailCmd.Parameters.AddWithValue("@nama_lengkap", detail.Nama_lengkap);
                detailCmd.Parameters.AddWithValue("@negara", detail.Negara);
                detailCmd.Parameters.AddWithValue("@no_telepon", detail.No_telepon);
                detailCmd.Parameters.AddWithValue("@tanggal_lahir", detail.Tanggal_lahir.Date);
                detailCmd.Parameters.AddWithValue("@created_at", DateTime.Today); // Menggunakan tanggal hari ini saja tanpa jam

                // Solusi penanganan NULL di PostgreSQL
                detailCmd.Parameters.AddWithValue("@deskripsi", string.IsNullOrEmpty(detail.Deskripsi) ? (object)DBNull.Value : detail.Deskripsi);

                detailCmd.ExecuteNonQuery();

                // COMMIT SEBAGAI TANDA DATA FIX DISIMPAN
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();  // Batalkan jika ada error di tengah jalan
                throw new Exception($"Gagal menyimpan data ke Database: {ex.Message}", ex);
            }
        }
        //cek email
        public bool IsEmailExists(string email)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM users WHERE email = @email";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", email);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }
        //cek no_telpon
        public bool IsNoTeleponExists(string noTelepon)
        {
            if (string.IsNullOrEmpty(noTelepon))
                return false; // No HP kosong tidak perlu dicek

            using var conn = GetConnection();
            conn.Open();

            // Sesuaikan nama tabel dan kolom dengan database Anda
            string query = @"SELECT COUNT(*) FROM detail_user WHERE no_telepon = @no_telepon";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@no_telepon", noTelepon);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }
        //cek password

        public User AuthenticateUser(string username, string password)
        {
            using var conn = GetConnection();
            conn.Open();

            // Query disesuaikan 100% dengan nama kolom di pgAdmin kamu (passwords pakai s)
            string query = @"SELECT id_user, username, passwords, email, is_admin 
                     FROM users 
                     WHERE username = @username AND passwords = @password";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                // Urutan parameter Constructor User.cs: (id, username, password, email, isAdmin)
                return new User(
                    reader.GetInt32(0),       // id_user -> id
                    reader.GetString(1),      // username -> username
                    reader.GetString(2),      // passwords -> password
                    reader.GetString(3),      // email -> email
                    reader.GetBoolean(4)      // is_admin -> isAdmin
                );
            }

            return null; // Mengembalikan null jika user tidak ditemukan
        }
        public Detail_User GetDetailUserByUserId(int idUser)
        {
            using var conn = GetConnection();
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
                    reader.GetInt32(0),                                      // id_detail_user
                    reader.GetString(2),                                     // nama_lengkap
                    reader.GetString(3),                                     // negara
                    reader.GetString(4),                                     // no_telepon
                    reader.GetDateTime(5),                                   // tanggal_lahir
                    reader.GetInt32(6),                                      // elo_rating
                    reader.GetDateTime(7),                                   // created_at
                    reader.IsDBNull(8) ? "" : reader.GetString(8)            // deskripsii
                );
            }
            return null;
        }
    }
}