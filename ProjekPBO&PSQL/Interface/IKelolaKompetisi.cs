using ProjekPBO_PSQL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Interface
{
    public interface IKelolaKompetisi
    {
        void TambahKompetisi(Kompetisi kompetisi);
        void HapusKompetisi(int idKompetisi);
        void UpdateKompetisi(Kompetisi kompetisi);
    }
}
