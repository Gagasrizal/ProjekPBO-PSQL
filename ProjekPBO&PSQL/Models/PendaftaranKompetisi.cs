using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class PendaftaranKompetisi
    {
        public int IdPendaftaranKompetisi { get; set; }
        public int IdUser { get; set; }
        public int IdKompetisi { get; set; }
        public Kompetisi DetailKompetisi { get; set; }

        private string _statusPendaftaran = "Belum Terdaftar";

        public string StatusPendaftaran
        {
            get { return _statusPendaftaran; }
            set
            {
                if (value != "Belum Terdaftar" && value != "Terdaftar")
                {
                    throw new ArgumentException("Status pendaftaran hanya boleh 'Belum Terdaftar' atau 'Terdaftar'!");
                }
                _statusPendaftaran = value;
            }
        }
    }
}

