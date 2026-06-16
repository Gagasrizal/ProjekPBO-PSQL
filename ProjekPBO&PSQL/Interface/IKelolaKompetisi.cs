using System;
using System.Collections.Generic;
using System.Text;
using ProjekPBO_PSQL.Models;

namespace ProjekPBO_PSQL.Interface
{
    public interface IKelolaKompetisi
    {
        void TambahKompetisi(Tournament kompetisi);
        void UpdateKompetisi(Tournament kompetisiUpdate);
        IReadOnlyList<Tournament> GetDaftarKompetisi();
    }
}
