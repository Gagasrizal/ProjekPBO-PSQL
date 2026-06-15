using System;
using System.Collections.Generic;
using System.Text;
using ProjekPBO_PSQL.Models;         
using ProjekPBO_PSQL.Models.Context;

namespace ProjekPBO_PSQL.Controller
{
    public class KompetisiController
    {
        private KompetisiContext _kompetisiContext;

        public KompetisiController()
        {
            _kompetisiContext = new KompetisiContext();
        }

        public bool HandleTambahKompetisi(Kompetisi baru)
        {
 
            return _kompetisiContext.MenambahKompetisi(baru);
        }

        public bool HandleEditKompetisi(Kompetisi dataEdit)
        {
            if (dataEdit.IdKompetisi <= 0)
            {
                throw new Exception("ID Kompetisi tidak valid untuk diupdate!");
            }

            // Memanggil fungsi Edit di KompetisiContext (Sudah disamakan namanya)
            return _kompetisiContext.UpdateKompetisi(dataEdit);
        }
    }
}