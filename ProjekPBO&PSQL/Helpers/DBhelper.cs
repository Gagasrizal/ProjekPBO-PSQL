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
        public bool RegisterUser(User user, detail_User detail)
        {
            using var conn = GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                // 1. INSERT KE TABEL USERS (Kolom: passwords)
                string userQuery = @"INSERT INTO users (username, passwords, email, is_admin)
                             VALUES (@username, @password, @email, @is_admin)
                             RETURNING id_user";

                using var userCmd = new NpgsqlCommand(userQuery, conn);
                userCmd.Parameters.AddWithValue("@username", user.username);
                userCmd.Parameters.AddWithValue("@password", user.password); // Map ke kolom passwords
                userCmd.Parameters.AddWithValue("@email", user.email);
                userCmd.Parameters.AddWithValue("@is_admin", false); // Pendaftar baru otomatis player biasa

                int idUser = Convert.ToInt32(userCmd.ExecuteScalar());

                // 2. INSERT KE TABEL DETAIL_USER (Sesuai gambar pgAdmin terbaru kamu)
                string detailQuery = @"INSERT INTO detail_user (id_user, nama_lengkap, negara, no_telepon, tanggal_lahir, elo_rating, created_at, deskripsi)
                               VALUES (@id_user, @nama_lengkap, @negara, @no_telepon, @tanggal_lahir, 1200, @created_at, @deskripsi)"; 

                using var detailCmd = new NpgsqlCommand(detailQuery, conn);
                detailCmd.Parameters.AddWithValue("@id_user", idUser);
                detailCmd.Parameters.AddWithValue("@nama_lengkap", detail.nama_lengkap);
                detailCmd.Parameters.AddWithValue("@negara", detail.negara);
                detailCmd.Parameters.AddWithValue("@no_telepon", detail.no_telepon);
                detailCmd.Parameters.AddWithValue("@tanggal_lahir", detail.tanggal_lahir.Date);
                detailCmd.Parameters.AddWithValue("@created_at", DateTime.Now.Date);
                detailCmd.Parameters.AddWithValue("@deskripsi", detail.deskripsi ?? (object)DBNull.Value);

                detailCmd.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)   // <-- INI LETAK CATCH
            {
                transaction.Rollback();  // batalkan perubahan
                // Lempar ulang exception agar form menangkapnya
                throw new Exception($"Gagal menyimpan data: {ex.Message}", ex);
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
    }
}