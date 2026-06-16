using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class PendaftaranKompetisi
    {
        private string _statusPendaftaran = "Belum Terdaftar";

        public int IdPendaftaranKompetisi { get; set; }
        public int IdUser { get; set; }
        public int IdKompetisi { get; set; }

        public string StatusPendaftaran
        {
            get => _statusPendaftaran;
            set
            {
                if (value != "Belum Terdaftar" && value != "Terdaftar")
                    throw new ArgumentException("Status pendaftaran hanya boleh 'Belum Terdaftar' atau 'Terdaftar'!");
                _statusPendaftaran = value;
            }
        }

        public override string ToString() =>
            $"[Pendaftaran] ID: {IdPendaftaranKompetisi} | " +
            $"User: {IdUser} | Kompetisi: {IdKompetisi} | Status: {StatusPendaftaran}";
    }
}
