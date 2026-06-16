using ProjekPBO_PSQL.Models.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProjekPBO_PSQL.Controller
{
    public class PertandinganController
    {
        private readonly PertandinganContext _context;

        public PertandinganController()
        {
            _context = new PertandinganContext();
        }


        public void MuatKompetisi(ComboBox cmb)
        {
            try
            {
                DataTable dt = _context.GetAllKompetisi();
                cmb.DataSource = dt;
                cmb.DisplayMember = "nama_kompetisi";
                cmb.ValueMember = "id_kompetisi";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat kompetisi: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MuatBabak(ComboBox cmb, int idKompetisi)
        {
            try
            {
                int jumlah = _context.GetJumlahBabak(idKompetisi);
                cmb.Items.Clear();
                for (int i = 1; i <= jumlah; i++)
                    cmb.Items.Add($"Babak {i}");

                if (cmb.Items.Count > 0)
                    cmb.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat babak: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void MuatPertandingan(DataGridView dgv, int idKompetisi, int babak)
        {
            try
            {
                DataTable dt = _context.GetPertandinganByBabak(idKompetisi, babak);
                dgv.DataSource = dt;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.ReadOnly = true;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Sembunyikan kolom ID (tetap ada untuk dipakai saat simpan hasil)
                SembunyikanKolom(dgv, "id_pertandingan");
                SembunyikanKolom(dgv, "id_pemain_hitam");
                SembunyikanKolom(dgv, "id_pemain_putih");

                // Rename header sesuai tampilan
                SetHeader(dgv, "pemain_hitam", "Pemain Hitam");
                SetHeader(dgv, "pemain_putih", "Pemain Putih");
                SetHeader(dgv, "hasil_babak_ini", "Hasil Babak Ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat pertandingan: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool BuatMatchmaking(int idKompetisi, int babak)
        {
            try
            {
                if (_context.IsBabakSudahDibuat(idKompetisi, babak))
                {
                    MessageBox.Show($"Matchmaking babak {babak} sudah pernah dibuat.",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!_context.IsBabakSebelumnyaSelesai(idKompetisi, babak))
                {
                    MessageBox.Show(
                        $"Babak {babak - 1} belum selesai semua.\nSelesaikan dulu sebelum buat matchmaking babak {babak}.",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var pemain = _context.GetPemainKompetisi(idKompetisi);

                if (pemain.Count < 2)
                {
                    MessageBox.Show("Jumlah pemain kurang dari 2.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                var sudahMain = _context.GetPasanganSudahMain(idKompetisi);

                List<(int idPutih, int idHitam)> pasangan = babak == 1
                    ? MatchmakingRandom(pemain)
                    : MatchmakingSwiss(pemain, sudahMain);

                _context.InsertMatchmaking(idKompetisi, babak, pasangan);

                MessageBox.Show($"Matchmaking babak {babak} berhasil dibuat!",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal buat matchmaking: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool SimpanHasil(DataGridView dgv, string hasilDipilih)
        {
            try
            {
                if (dgv.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Pilih baris pertandingan terlebih dahulu.",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                DataGridViewRow baris = dgv.SelectedRows[0];

                // Cek apakah sudah ada hasil sebelumnya
                string hasilSaatIni = baris.Cells["hasil_babak_ini"].Value?.ToString() ?? "";
                if (hasilSaatIni != "Belum Dimainkan" && hasilSaatIni != "")
                {
                    MessageBox.Show("Pertandingan ini sudah memiliki hasil.",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                int idPertandingan = Convert.ToInt32(baris.Cells["id_pertandingan"].Value);
                int idPutih = Convert.ToInt32(baris.Cells["id_pemain_putih"].Value);
                int idHitam = Convert.ToInt32(baris.Cells["id_pemain_hitam"].Value);

                if (hasilDipilih != "1-0" && hasilDipilih != "0-1" && hasilDipilih != "1/2-1/2")
                {
                    MessageBox.Show("Pilih hasil yang valid: 1-0, 0-1, atau 1/2-1/2.",
                        "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                _context.SimpanHasil(idPertandingan, hasilDipilih, idPutih, idHitam);

                MessageBox.Show("Hasil disimpan dan ELO diperbarui.",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal simpan hasil: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void MuatLeaderboard(DataGridView dgv, int idKompetisi)
        {
            try
            {
                DataTable dt = _context.GetLeaderboard(idKompetisi);
                dgv.DataSource = dt;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.ReadOnly = true;

                SetHeader(dgv, "peringkat", "Peringkat");
                SetHeader(dgv, "pemain", "Nama Pemain");
                SetHeader(dgv, "elo", "ELO Rating");
                SetHeader(dgv, "menang", "Menang");
                SetHeader(dgv, "draw", "Draw");
                SetHeader(dgv, "kalah", "Kalah");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat leaderboard: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void MuatHistoryPemain(DataGridView dgv, int idUser)
        {
            try
            {
                DataTable dt = _context.GetHistoryPertandinganPemain(idUser);
                dgv.DataSource = dt;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.ReadOnly = true;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                SetHeader(dgv, "tournament", "Tournament");
                SetHeader(dgv, "babak", "Babak");
                SetHeader(dgv, "lawan", "Lawan");
                SetHeader(dgv, "warna", "Warna");
                SetHeader(dgv, "hasil", "Hasil");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat history: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<(int, int)> MatchmakingRandom(List<(int id, string nama, int elo)> pemain)
        {
            var acak = new List<(int, string, int)>(pemain);
            var rng = new Random();

            // Fisher-Yates shuffle
            for (int i = acak.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                var tmp = acak[i]; acak[i] = acak[j]; acak[j] = tmp;
            }

            return PasangkanDanHandleBye(acak);
        }

        private List<(int, int)> MatchmakingSwiss(
            List<(int id, string nama, int elo)> pemain,
            HashSet<string> sudahMain)
        {
            // Pemain sudah diurutkan ELO DESC dari context
            var tersisa = new List<(int id, string nama, int elo)>(pemain);
            var hasil = new List<(int, int)>();

            while (tersisa.Count >= 2)
            {
                var p1 = tersisa[0];
                tersisa.RemoveAt(0);
                bool paired = false;

                for (int i = 0; i < tersisa.Count; i++)
                {
                    var p2 = tersisa[i];
                    string key = $"{Math.Min(p1.id, p2.id)}-{Math.Max(p1.id, p2.id)}";

                    if (!sudahMain.Contains(key))
                    {
                        hasil.Add((p1.id, p2.id));
                        tersisa.RemoveAt(i);
                        paired = true;
                        break;
                    }
                }

                // Semua sudah pernah lawan — pasangkan saja yang pertama (rare case)
                if (!paired && tersisa.Count > 0)
                {
                    hasil.Add((p1.id, tersisa[0].id));
                    tersisa.RemoveAt(0);
                }
            }

            // Sisa 1 pemain = BYE
            if (tersisa.Count == 1)
                hasil.Add((tersisa[0].id, -1));

            return hasil;
        }

        private List<(int, int)> PasangkanDanHandleBye(List<(int id, string nama, int elo)> acak)
        {
            var hasil = new List<(int, int)>();

            for (int i = 0; i + 1 < acak.Count; i += 2)
                hasil.Add((acak[i].id, acak[i + 1].id));

            if (acak.Count % 2 != 0)
                hasil.Add((acak[acak.Count - 1].id, -1)); // BYE

            return hasil;
        }

        private void SetHeader(DataGridView dgv, string col, string header)
        {
            if (dgv.Columns[col] != null)
                dgv.Columns[col].HeaderText = header;
        }

        private void SembunyikanKolom(DataGridView dgv, string col)
        {
            if (dgv.Columns[col] != null)
                dgv.Columns[col].Visible = false;
        }
    }
}