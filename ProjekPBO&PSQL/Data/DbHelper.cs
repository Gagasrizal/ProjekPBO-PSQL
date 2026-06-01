using System;
using System.Data;
using Npgsql;

namespace ProjekPBO_PSQL
{
    public static class DbHelper
    {
        // TODO: ganti connection string sesuai pengaturan PostgreSQL Anda
        private static readonly string ConnString =
            "Host=localhost;Port=5432;Username=postgres;Password=YourPassword;Database=YourDatabase;Pooling=true;";

        public static NpgsqlConnection GetConnection() => new NpgsqlConnection(ConnString);

        public static DataTable ExecuteQuery(string sql, params NpgsqlParameter[] parameters)
        {
            var dt = new DataTable();
            using var conn = GetConnection();
            using var cmd = new NpgsqlCommand(sql, conn);
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);
            using var da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public static int ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
        {
            using var conn = GetConnection();
            conn.Open();
            using var cmd = new NpgsqlCommand(sql, conn);
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteNonQuery();
        }
    }
}
