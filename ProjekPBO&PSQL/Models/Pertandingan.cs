using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class Pertandingan
    {
        private decimal _skorPutih;
        private decimal _skorHitam;

        public int IdPertandingan { get; set; }
        public int IdKompetisi { get; set; }
        public DateTime TanggalPertandingan { get; set; }
        public int Babak { get; set; }
        public int PemainPutih { get; set; }
        public int PemainHitam { get; set; }
        public string Hasil { get; set; } = string.Empty;

        public decimal SkorPutih
        {
            get => _skorPutih;
            set
            {
                if (value < 0.0m || value > 1.0m)
                    throw new ArgumentException("Skor tidak valid! Rentang skor catur per babak adalah 0, 0.5, atau 1.");
                _skorPutih = value;
            }
        }

        public decimal SkorHitam
        {
            get => _skorHitam;
            set
            {
                if (value < 0.0m || value > 1.0m)
                    throw new ArgumentException("Skor tidak valid! Rentang skor catur per babak adalah 0, 0.5, atau 1.");
                _skorHitam = value;
            }
        }

        public override string ToString() =>
            $"Match ID: {IdPertandingan} | Babak: {Babak} | User {PemainPutih} (P) vs User {PemainHitam} (H) | Skor: {SkorPutih} - {SkorHitam} | Hasil: {Hasil}";
    }
}

