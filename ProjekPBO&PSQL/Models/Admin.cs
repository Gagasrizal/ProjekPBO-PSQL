using System;
using System.Collections.Generic;
using System.Text;
using ProjekPBO_PSQL.Interface;

namespace ProjekPBO_PSQL.Models
{
    public class Admin : AkunUser, IKelolaKompetisi
    {
        private readonly List<Tournament> _daftarKompetisiDikelola = new();

        public Admin(int id, string username, string password, string email, ProfilCatur? profil = null)
            : base(id, username, password, email, true, profil)
        {
        }

        public override string GetRole() => "Admin";

        public override void TampilkanInfo()
        {
            base.TampilkanInfo();
            Console.WriteLine($"       Kompetisi Dikelola: {_daftarKompetisiDikelola.Count} kompetisi");
        }

        public override void LakukanAktivitasUtama()
        {
            Console.WriteLine($"[ADMIN] {Username} mengelola sistem dan kompetisi catur.");
        }

        public void TambahKompetisi(Tournament kompetisi)
        {
            ArgumentNullException.ThrowIfNull(kompetisi);
            _daftarKompetisiDikelola.Add(kompetisi);
        }

        public void UpdateKompetisi(Tournament kompetisiUpdate)
        {
            ArgumentNullException.ThrowIfNull(kompetisiUpdate);

            int index = _daftarKompetisiDikelola.FindIndex(k => k.IdKompetisi == kompetisiUpdate.IdKompetisi);
            if (index == -1)
                throw new InvalidOperationException($"Kompetisi dengan ID {kompetisiUpdate.IdKompetisi} tidak ditemukan.");

            _daftarKompetisiDikelola[index] = kompetisiUpdate;
        }

        public IReadOnlyList<Tournament> GetDaftarKompetisi() => _daftarKompetisiDikelola.AsReadOnly();
    }
}
