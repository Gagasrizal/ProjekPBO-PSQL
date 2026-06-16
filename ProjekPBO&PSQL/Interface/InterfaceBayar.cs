using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Interface
{
    public interface IBayar
    {
        void LakukanPembayaran(int nominal);
        string CekStatusPembayaran();
    }
}

