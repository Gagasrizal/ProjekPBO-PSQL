using Npgsql;
using ProjekPBO_PSQL.Helpers;
using ProjekPBO_PSQL.Models;
using System;

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
    }
}