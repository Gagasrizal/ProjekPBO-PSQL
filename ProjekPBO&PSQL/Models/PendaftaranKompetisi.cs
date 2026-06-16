using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class PendaftaranKompetisi
    {
        public int IdPendaftaranKompetisi { get; set; }

        public int IdUser { get; set; }

        public int IdKompetisi { get; set; }

        public string StatusPendaftaran { get; set; } = "Belum Bayar";
    }
}
