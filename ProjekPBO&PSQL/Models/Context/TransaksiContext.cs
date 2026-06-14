using Npgsql;
using ProjekPBO_PSQL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ProjekPBO_PSQL.Helpers;

namespace ProjekPBO_PSQL.Models.Context
{
    public DataTable GetAllTransaksiPemain()
    {
        DataTable dt = new DataTable();

        using (NpgsqlConnection conn = DBHelper.GetConnection())
        {
            try
            {
                conn.Open();

                string query = "SELECT * FROM view_transaksi_pemain ORDER BY tanggal_transaksi DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal mengambil data transaksi: " + ex.Message);
            }
        }
        return dt;
    }
}
