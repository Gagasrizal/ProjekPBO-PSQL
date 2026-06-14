using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ProjekPBO_PSQL.Helpers;

namespace ProjekPBO_PSQL.Models.Context
{
public class KompetisiContext
    {
        public DataTable TampilkanPesertaTurnamen(int idKompetisi)
        {
            DataTable dt = new DataTable();

            using (NpgsqlConnection conn = DBHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT id_user, nama_lengkap, elo_rating 
                                     FROM detail_user 
                                     WHERE id_user IN (
                                         SELECT id_user FROM pendaftaran WHERE id_kompetisi = @id_kompetisi
                                     )";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);

                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Gagal mengambil data peserta: " + ex.Message);
                }
            }

            return dt; 
        }
    }
}