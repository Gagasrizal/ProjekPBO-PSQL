using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class Pertandingan
    {
        private double _skorPutih;
        private double _skorHitam;

        public int IdPertandingan { get; set; }
        public int IdKompetisi { get; set; }
        public int Babak { get; set; }
        public int PemainPutih { get; set; }
        public int PemainHitam { get; set; }
        public string Hasil { get; set; } = ""; 

        public double SkorPutih
        {
            get { return _skorPutih; }
            set
            {
                if (value != 0.0 && value != 0.5 && value != 1.0)
                    throw new ArgumentException("Skor Putih tidak valid! Harus 0, 0.5, atau 1.");
                _skorPutih = value;
            }
        }

        public double SkorHitam
        {
            get { return _skorHitam; }
            set
            {
                if (value != 0.0 && value != 0.5 && value != 1.0)
                    throw new ArgumentException("Skor Hitam tidak valid! Harus 0, 0.5, atau 1.");
                _skorHitam = value;
            }
        }

        public void SetSkorDanHasil(double skorP, double skorH)
        {
            if (skorP + skorH != 1.0)
            {
                throw new InvalidOperationException("Total skor dalam satu papan tanding harus tepat 1.0!");
            }

            this.SkorPutih = skorP;
            this.SkorHitam = skorH;

            if (skorP == 1.0) this.Hasil = "PUTIH";
            else if (skorH == 1.0) this.Hasil = "HITAM";
            else this.Hasil = "REMIS";
        }
    }
}