using Microsoft.VisualBasic.ApplicationServices;
using Npgsql;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using System;

namespace ProjekPBO_PSQL.Models.Context
{
    public class AutentikasiContext
    {
        public bool IsUsernameExists(string username)
        {
            using var conn = DBHelper.GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM users WHERE username = @username";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        public bool IsEmailExists(string email)
        {
            using var conn = DBHelper.GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM users WHERE email = @email";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", email);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        public bool IsNoTeleponExists(string noTelepon)
        {
            if (string.IsNullOrEmpty(noTelepon)) return false;

            using var conn = DBHelper.GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM detail_user WHERE no_telepon = @no_telepon";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@no_telepon", noTelepon);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        public AkunUser AuthenticateUser(string username, string password)
        {
            using var conn = DBHelper.GetConnection();
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
                // FIX CS1729: Menggunakan kelas 'Pemain' dan mencocokkan jumlah parameter 
                // sesuai konstruktor yang ada di Pemain.cs (4 argumen wajib)
                return new Pemain(
                    reader.GetInt32(0),  // id
                    reader.GetString(1), // username
                    reader.GetString(2), // passwords
                    reader.GetString(3)  // email
                );
            }
            return null;
        }

        // FIX: Menggunakan tipe data 'ProfilCatur' dan parameter string plainPassword terpisah
        public bool RegisterUser(AkunUser user, string plainPassword, ProfilCatur detail)
        {
            using var conn = DBHelper.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                string userQuery = @"INSERT INTO users (username, passwords, email, is_admin)
                                     VALUES (@username, @password, @email, @is_admin)
                                     RETURNING id_user";

                using var userCmd = new NpgsqlCommand(userQuery, conn);

                // FIX CS1061: Menggunakan Properti berhuruf Kapital (PascalCase) dari AkunUser
                userCmd.Parameters.AddWithValue("@username", user.Username);
                userCmd.Parameters.AddWithValue("@password", plainPassword);
                userCmd.Parameters.AddWithValue("@email", user.Email);
                userCmd.Parameters.AddWithValue("@is_admin", false);

                object userIdObj = userCmd.ExecuteScalar();
                if (userIdObj == null) throw new Exception("Gagal mendapatkan ID User baru.");
                int idUser = Convert.ToInt32(userIdObj);

                string detailQuery = @"INSERT INTO detail_user (id_user, nama_lengkap, negara, no_telepon, tanggal_lahir, elo_rating, created_at, deskripsi)
                                       VALUES (@id_user, @nama_lengkap, @negara, @no_telepon, @tanggal_lahir, @elo_rating, @created_at, @deskripsi)";

                using var detailCmd = new NpgsqlCommand(detailQuery, conn);
                detailCmd.Parameters.AddWithValue("@id_user", idUser);

                // FIX CS1061: Menggunakan properti PascalCase dari ProfilCatur Anda
                detailCmd.Parameters.AddWithValue("@nama_lengkap", detail.NamaLengkap);
                detailCmd.Parameters.AddWithValue("@negara", detail.Negara);
                detailCmd.Parameters.AddWithValue("@no_telepon", detail.NoTelepon);
                detailCmd.Parameters.AddWithValue("@tanggal_lahir", detail.TanggalLahir.Date);
                detailCmd.Parameters.AddWithValue("@elo_rating", detail.EloRating == 0 ? 1200 : detail.EloRating);
                detailCmd.Parameters.AddWithValue("@created_at", DateTime.Today);
                detailCmd.Parameters.AddWithValue("@deskripsi", string.IsNullOrEmpty(detail.Deskripsi) ? (object)DBNull.Value : detail.Deskripsi);

                detailCmd.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Gagal menyimpan data ke Database: {ex.Message}", ex);
            }
        }
    }
}