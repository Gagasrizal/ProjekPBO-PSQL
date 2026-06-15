using System;

namespace ProjekPBO_PSQL.Models
{
    public class Admin : AkunUser, IKelolaKompetisi
    {
        private List<Kompetisi> _daftarKompetisiDikelola;

        public Admin(int id, string username, string password, string email, ProfilCatur profil)
            : base(id, username, password, email, true, profil)
        {
            _daftarKompetisiDikelola = new List<Kompetisi>();
        }

        public override string GetRole()
        {
            return "Admin";
        }

        public override void TampilkanInfo()
        {
            base.TampilkanInfo();
            Console.WriteLine($"       Kompetisi Dikelola: {_daftarKompetisiDikelola.Count} kompetisi");
        }
        public override void LakukanAktivitasUtama()
        {
            Console.WriteLine($"[ADMIN] {Username} sedang mengelola sistem dan kompetisi.");
        }
        public void TambahKompetisi(Kompetisi kompetisi)
        {
            if (kompetisi == null)
                throw new ArgumentNullException("Kompetisi tidak boleh null.");

            _daftarKompetisiDikelola.Add(kompetisi);
            Console.WriteLine($"[ADMIN] Kompetisi '{kompetisi.NamaKompetisi}' berhasil ditambahkan.");
        }

        public void HapusKompetisi(int idKompetisi)
        {
            var kompetisi = _daftarKompetisiDikelola.Find(k => k.IdKompetisi == idKompetisi);
            if (kompetisi == null)
                throw new Exception($"Kompetisi dengan ID {idKompetisi} tidak ditemukan.");

            _daftarKompetisiDikelola.Remove(kompetisi);
            Console.WriteLine($"[ADMIN] Kompetisi ID {idKompetisi} berhasil dihapus.");
        }

        public void UpdateKompetisi(Kompetisi kompetisiUpdate)
        {
            var index = _daftarKompetisiDikelola.FindIndex(k => k.IdKompetisi == kompetisiUpdate.IdKompetisi);
            if (index == -1)
                throw new Exception($"Kompetisi dengan ID {kompetisiUpdate.IdKompetisi} tidak ditemukan.");

            _daftarKompetisiDikelola[index] = kompetisiUpdate;
            Console.WriteLine($"[ADMIN] Kompetisi '{kompetisiUpdate.NamaKompetisi}' berhasil diupdate.");
        }

        public List<Kompetisi> GetDaftarKompetisi()
        {
            return _daftarKompetisiDikelola;
        }
    }
}
