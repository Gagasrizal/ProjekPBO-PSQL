using Npgsql;
using ProjekPBO_PSQL.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProjekPBO_PSQL.Helpers
{
    public static class DatabaseHelper
    {
        private static string connString = "Host=localhost;Port=5432;Database=HyperChess;Username=postgres;Password=54321";

        public static NpgsqlConnection Connect()
        {
            return new NpgsqlConnection(connString);
        }
    }
}