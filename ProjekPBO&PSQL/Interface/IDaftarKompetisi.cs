using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Interface
{
    public interface IDaftarKompetisi
    {
        void DaftarKompetisi(int idKompetisi);
        void BatalkanPendaftaran(int idKompetisi);
    }
}

