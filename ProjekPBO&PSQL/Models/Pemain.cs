using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{
    public class Pemain : AkunUser, IDaftarKompetisi
    {
        private List<PendaftaranKompetisi> _daftarPendaftaran;

        public Pemain(int id, string username, string password, string email, ProfilCatur profil)
            : base(id, username, password, email, false, profil)
        {
            _daftarPendaftaran = new List<PendaftaranKompetisi>();
        }
        public override string GetRole()
        {
            return "Pemain";
        }

        public override void TampilkanInfo()
        {
            base.TampilkanInfo();
            Console.WriteLine($"       ELO Rating  : {Profil.EloRating}");
            Console.WriteLine($"       Terdaftar di: {_daftarPendaftaran.Count} kompetisi");
        }
        public override void LakukanAktivitasUtama()
        {
            Console.WriteLine($"[PEMAIN] {Username} sedang bermain catur. ELO: {Profil.EloRating}");
        }

        public void DaftarKompetisi(int idKompetisi)
        {
            bool sudahDaftar = _daftarPendaftaran.Exists(p => p.IdKompetisi == idKompetisi);
            if (sudahDaftar)
                throw new Exception($"Pemain sudah terdaftar di kompetisi ID {idKompetisi}.");

            var pendaftaran = new PendaftaranKompetisi
            {
                IdUser = this.IdUser,
                IdKompetisi = idKompetisi,
                StatusPendaftaran = "Terdaftar"
            };

            _daftarPendaftaran.Add(pendaftaran);
            Console.WriteLine($"[PEMAIN] {Username} berhasil mendaftar kompetisi ID {idKompetisi}.");
        }

        public void BatalkanPendaftaran(int idKompetisi)
        {
            var pendaftaran = _daftarPendaftaran.Find(p => p.IdKompetisi == idKompetisi);
            if (pendaftaran == null)
                throw new Exception($"Pendaftaran untuk kompetisi ID {idKompetisi} tidak ditemukan.");

            _daftarPendaftaran.Remove(pendaftaran);
            Console.WriteLine($"[PEMAIN] {Username} membatalkan pendaftaran kompetisi ID {idKompetisi}.");
        }

        public List<PendaftaranKompetisi> GetDaftarPendaftaran()
        {
            return _daftarPendaftaran;
        }
    }
}