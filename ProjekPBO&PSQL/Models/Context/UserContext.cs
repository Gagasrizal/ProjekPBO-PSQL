using Npgsql;
using ProjekPBO_PSQL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProjekPBO_PSQL.Models.Context
{
    public class UserContext
    {
        public bool RegisterPemain(string username, string password, string namaLengkap)
        {
            bool isSuccess = false;

            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO detail_user (username, password, nama_lengkap, elo_rating) 
                                     VALUES (@username, @password, @nama, 1200)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); 
                        cmd.Parameters.AddWithValue("@nama", namaLengkap);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0) isSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal Register: " + ex.Message);
                }
            }
            return isSuccess;
        }

        public DataTable LoginPemain(string username, string password)
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT id_user, username, nama_lengkap, elo_rating 
                                     FROM detail_user 
                                     WHERE username = @username AND password = @password";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal Login: " + ex.Message);
                }
            }
            // Jika dt ada isinya (Rows.Count > 0), berarti login sukses
            return dt;
        }
    }
}
