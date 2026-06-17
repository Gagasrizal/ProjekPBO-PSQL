using Npgsql;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using System;
using System.Data;

namespace ProjekPBO_PSQL.Models.Context
{
    public class UserContext
    {
        public ProfilCatur GetDetailUserByUserId(int idUser)
        {
            using var conn = DBHelper.GetConnection();
            conn.Open();

            string query = @"SELECT id_detail_user, nama_lengkap, negara, no_telepon, tanggal_lahir, elo_rating, deskripsi 
                             FROM detail_user 
                             WHERE id_user = @id_user";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id_user", idUser);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ProfilCatur
                {
                    IdDetailUser = reader.GetInt32(0),
                    NamaLengkap = reader.GetString(1),
                    Negara = reader.GetString(2),
                    NoTelepon = reader.GetString(3),
                    TanggalLahir = reader.GetDateTime(4),
                    EloRating = reader.GetInt32(5),
                    Deskripsi = reader.IsDBNull(6) ? "" : reader.GetString(6)
                };
            }
            return null;
        }

        public DataTable AmbilDetailProfilFull(int idUser)
        {
            DataTable dt = new DataTable();
            // Menggunakan u.passwords AS password agar sesuai dengan nama kolom tabel users kamu
            string query = @"SELECT u.username, u.email, u.passwords AS password, d.negara, d.elo_rating, d.no_telepon, d.tanggal_lahir, d.created_at, d.deskripsi 
                             FROM users u
                             JOIN detail_user d ON u.id_user = d.id_user
                             WHERE u.id_user = @id_user";

            try
            {
                using var conn = DBHelper.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_user", idUser);
                using var da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Gagal mengambil detail profil: {ex.Message}");
            }
            return dt;
        }
        public bool UpdateProfilFull(int idUser, string username, string password, string negara, string noTelepon, string deskripsi)
        {
            using var conn = DBHelper.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                string queryUser = "UPDATE users SET username = @username, passwords = @password WHERE id_user = @id_user";
                using var cmdUser = new NpgsqlCommand(queryUser, conn, transaction);
                cmdUser.Parameters.AddWithValue("@username", username);
                cmdUser.Parameters.AddWithValue("@password", password);
                cmdUser.Parameters.AddWithValue("@id_user", idUser);
                cmdUser.ExecuteNonQuery();

                string queryDetail = @"UPDATE detail_user 
                                       SET negara = @negara, no_telepon = @no_telepon, deskripsi = @deskripsi 
                                       WHERE id_user = @id_user";
                using var cmdDetail = new NpgsqlCommand(queryDetail, conn, transaction);
                cmdDetail.Parameters.AddWithValue("@negara", negara);
                cmdDetail.Parameters.AddWithValue("@no_telepon", noTelepon);
                cmdDetail.Parameters.AddWithValue("@deskripsi", deskripsi);
                cmdDetail.Parameters.AddWithValue("@id_user", idUser);
                cmdDetail.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Gagal mengupdate database: {ex.Message}");
            }
        }
    }
}