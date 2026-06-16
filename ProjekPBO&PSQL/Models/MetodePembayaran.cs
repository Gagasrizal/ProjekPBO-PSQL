using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class MetodePembayaran
    {
        public int IdMetodePembayaran { get; set; }
        public string NamaMetodePembayaran { get; set; }
    }
}
