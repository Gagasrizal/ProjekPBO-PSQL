using Npgsql;
using ProjekPBO_PSQL.Models;
using System;
using System.Data;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ProjekPBO_PSQL.Helpers
{
    public class DBHelper
    {
        private static string connString = "Host=localhost;Port=5432;Database=HyperChess;Username=postgres;Password=54321";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connString);
        }
    }
}