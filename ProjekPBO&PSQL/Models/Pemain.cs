using Microsoft.VisualBasic.ApplicationServices;
using ProjekPBO_PSQL.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ProjekPBO_PSQL.Models
{
    public class Pemain : AkunUser, IDaftarKompetisi
    {
        private readonly List<PendaftaranKompetisi> _daftarPendaftaran = new();

        public Pemain(int id, string username, string password, string email, ProfilCatur? profil = null)
            : base(id, username, password, email, false, profil)
        {
        }

        public override string GetRole() => "Pemain";

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
                throw new InvalidOperationException($"Pemain sudah terdaftar di kompetisi ID {idKompetisi}.");

            var pendaftaran = new PendaftaranKompetisi
            {
                IdUser = IdUser,
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
                throw new InvalidOperationException($"Pendaftaran untuk kompetisi ID {idKompetisi} tidak ditemukan.");

            _daftarPendaftaran.Remove(pendaftaran);
            Console.WriteLine($"[PEMAIN] {Username} membatalkan pendaftaran kompetisi ID {idKompetisi}.");
        }

        public IReadOnlyList<PendaftaranKompetisi> GetDaftarPendaftaran() => _daftarPendaftaran.AsReadOnly();
    }
}

