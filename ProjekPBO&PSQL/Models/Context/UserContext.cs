using Npgsql;
using ProjekPBO_PSQL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models.Context
{
    public class UserContext
    {
        private readonly DBHelper dbHelper = new DBHelper();

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



        public bool RegisterUser(User user, Detail_User detail)
        {
            using var conn = dbHelper.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {

                string userQuery = @"INSERT INTO users (username, passwords, email, is_admin)
                                     VALUES (@username, @password, @email, @is_admin)
                                     RETURNING id_user";

                using var userCmd = new NpgsqlCommand(userQuery, conn);
                userCmd.Parameters.AddWithValue("@username", user.username);
                userCmd.Parameters.AddWithValue("@password", user.password);
                userCmd.Parameters.AddWithValue("@email", user.email);
                userCmd.Parameters.AddWithValue("@is_admin", false);

                object userIdObj = userCmd.ExecuteScalar();
                if (userIdObj == null)
                {
                    throw new Exception("Gagal mendapatkan ID User baru.");
                }
                int idUser = Convert.ToInt32(userIdObj);


                string detailQuery = @"INSERT INTO detail_user (id_user, nama_lengkap, negara, no_telepon, tanggal_lahir, elo_rating, created_at, deskripsi)
                                       VALUES (@id_user, @nama_lengkap, @negara, @no_telepon, @tanggal_lahir, 1200, @created_at, @deskripsi)";

                using var detailCmd = new NpgsqlCommand(detailQuery, conn);
                detailCmd.Parameters.AddWithValue("@id_user", idUser);
                detailCmd.Parameters.AddWithValue("@nama_lengkap", detail.Nama_lengkap);
                detailCmd.Parameters.AddWithValue("@negara", detail.Negara);
                detailCmd.Parameters.AddWithValue("@no_telepon", detail.No_telepon);
                detailCmd.Parameters.AddWithValue("@tanggal_lahir", detail.Tanggal_lahir.Date);
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



        public User AuthenticateUser(string username, string password)
        {
            using var conn =dbHelper.GetConnection();
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
                return new User(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetBoolean(4)
                );
            }

            return null;
        }




    }
}
