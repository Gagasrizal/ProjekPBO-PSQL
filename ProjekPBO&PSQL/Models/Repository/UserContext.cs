using Npgsql;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using System;

namespace ProjekPBO_PSQL.Models.Context
{
    public class UserContext
    {

        public Detail_User GetDetailUserByUserId(int idUser)
        {
            using var conn = DBHelper.GetConnection();
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
    }
}