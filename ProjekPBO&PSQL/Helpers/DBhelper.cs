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
        private readonly string connString = "Host=localhost;Port=5432;Database=hyperchess;Username=postgres;Password=atmaw12";

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connString);
        }
    }
}