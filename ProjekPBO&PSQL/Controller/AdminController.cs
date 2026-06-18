using ProjekPBO_PSQL.Models.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ProjekPBO_PSQL.Interface;

namespace ProjekPBO_PSQL.Controller
{
    public class AdminController : IKelolaKompetisi
    {
        private readonly KompetisiContext _kompetisiContext;
        private readonly TransaksiContext _transaksiContext;
        private readonly PertandinganContext _pertandinganContext;

        public AdminController()
        {
            _kompetisiContext = new KompetisiContext();
            _transaksiContext = new TransaksiContext();
            _pertandinganContext = new PertandinganContext();
        }
        public void TambahKompetisi(Tournament kompetisi)
        {
            if (kompetisi == null) throw new ArgumentNullException(nameof(kompetisi));
            bool sukses = _kompetisiContext.TambahTournament(kompetisi);
            if (!sukses) throw new Exception("Gagal menambahkan kompetisi baru.");
        }


        public void UpdateKompetisi(Tournament kompetisiUpdate)
        {
            if (kompetisiUpdate == null) throw new ArgumentNullException(nameof(kompetisiUpdate));
            bool sukses = _kompetisiContext.EditTournament(kompetisiUpdate);
            if (!sukses) throw new Exception("Gagal memperbarui data kompetisi.");
        }

        public IReadOnlyList<Tournament> GetDaftarKompetisi()
        {
            DataTable dt = _kompetisiContext.AmbilSemuaTournament();
            List<Tournament> listTournament = new List<Tournament>();

            foreach (DataRow row in dt.Rows)
            {
                listTournament.Add(new Tournament
                {
                    IdKompetisi = Convert.ToInt32(row["id_kompetisi"]),
                    IdUser = Convert.ToInt32(row["id_user"]),
                    NamaKompetisi = row["nama_kompetisi"].ToString(),
                    ModeKompetisi = row["mode_kompetisi"].ToString(),
                    HargaPendaftaran = Convert.ToInt32(row["harga_pendaftaran"]),
                    PelaksanaanPendaftaran = row["pelaksanaan_pendaftaran"].ToString(),
                    TanggalPelaksanaan = Convert.ToDateTime(row["tanggal_pelaksanaan"]),
                    Hadiah = Convert.ToInt32(row["hadiah"]),
                    SistemPertandingan = row["sistem_pertandingan"].ToString(),
                    JumlahBabak = Convert.ToInt32(row["jumlah_babak"])
                });
            }
            return listTournament.AsReadOnly();
        }


        public bool TambahTournament(Tournament tournament) => _kompetisiContext.TambahTournament(tournament);
        public bool EditTournament(Tournament tournament) => _kompetisiContext.EditTournament(tournament);
        //public DataTable AmbilSemuaTournament() => _kompetisiContext.AmbilSemuaTournament();
        public DataTable AmbilSemuaPembayaran() => _transaksiContext.AmbilSemuaPembayaran();
        //public DataTable AmbilPendaftarTournament(int idKompetisi) => _kompetisiContext.AmbilPendaftarBerdasarkanTournament(idKompetisi);

        //public bool GenerateBabakPertandingan(int idKompetisi, int babak)
        //{
        //    if (_kompetisiContext.IsBabakSudahGenerated(idKompetisi, babak))
        //        throw new Exception($"Pertandingan untuk Babak {babak} sudah pernah dibuat!");

        //    List<int> listPemain = _kompetisiContext.AmbilPemainTerdaftar(idKompetisi);
        //    if (listPemain.Count < 2)
        //        throw new Exception("Pemain terdaftar kurang dari 2 orang.");

        //    List<Tuple<int, int>> pasangan = new List<Tuple<int, int>>();
        //    for (int i = 0; i < listPemain.Count - 1; i += 2)
        //    {
        //        pasangan.Add(new Tuple<int, int>(listPemain[i], listPemain[i + 1]));
        //    }
        //    return _kompetisiContext.SimpanPertandinganGenerate(idKompetisi, babak, pasangan);
        //}
    }
}
