//using Npgsql;
//using System.Data;

//namespace ProjekPBO_PSQL.Helpers
//{
//    public static class DBHelper
//    {
//        private const string ConnString = "Host=localhost;Port=5432;Username=postgres;Password=qwerty;Database=Projek_sem2;Include Error Detail=true";

//        public static NpgsqlConnection BukaKoneksi()
//        {
//            var conn = new NpgsqlConnection(ConnString);
//            conn.Open();
//            return conn;
//        }

//        //select
//        public static DataTable EksekusiQuery(string query, NpgsqlParameter[]? parameters = null)
//        {
//            using var conn = BukaKoneksi();
//            using var cmd = new NpgsqlCommand(query, conn);

//            if (parameters != null)
//                cmd.Parameters.AddRange(parameters);

//            using var adapter = new NpgsqlDataAdapter(cmd);
//            var dt = new DataTable();
//            adapter.Fill(dt);
//            return dt;
//        }

//        //action
//        public static int EksekusiNonQuery(string query, NpgsqlParameter[]? parameters = null)
//        {
//            using var conn = BukaKoneksi();
//            using var tx = conn.BeginTransaction();
//            try
//            {
//                using var cmd = new NpgsqlCommand(query, conn, tx);
//                if (parameters != null)
//                    cmd.Parameters.AddRange(parameters);

//                int result = cmd.ExecuteNonQuery();
//                tx.Commit(); //permanen jika sukses
//                return result;
//            }
//            catch (Exception)
//            {
//                tx.Rollback(); //return klo gagal
//                throw;
//            }

//        }

//        //scalar 
//        public static object? EksekusiScalar(string query, NpgsqlParameter[]? parameters = null)
//        {
//            using var conn = BukaKoneksi();
//            using var cmd = new NpgsqlCommand(query, conn);

//            if (parameters != null)
//                cmd.Parameters.AddRange(parameters);

//            return cmd.ExecuteScalar();
//        }

//    }
//}