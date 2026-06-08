using System;
using System.Data;
using System.Windows.Forms; // Pastikan namespace ini ada untuk menampung MessageBox.Show
using Npgsql;
using ProjekPBO_PSQL.Models;

namespace ProjekPBO_PSQL.Helpers
{
    public class DBHelper
    {
        private readonly string connString = "Host=localhost;Port=5432;Database=HyperChess;Username=postgres;Password=54321";

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connString);
        }

        public bool IsUsernameExists(string username)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM users WHERE username = @username";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        public bool RegisterUser(User user, Detail_User detail)
        {
            using var conn = GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                // 1. INSERT KE TABEL USERS
                string userQuery = @"INSERT INTO users (username, passwords, email, is_admin)
                                     VALUES (@username, @password, @email, @is_admin)
                                     RETURNING id_user";

                using var userCmd = new NpgsqlCommand(userQuery, conn);
                userCmd.Parameters.AddWithValue("@username", user.username);
                userCmd.Parameters.AddWithValue("@password", user.password);
                userCmd.Parameters.AddWithValue("@email", user.email);
                userCmd.Parameters.AddWithValue("@is_admin", false);

                object userIdObj = userCmd.ExecuteScalar();
                if (userIdObj == null)
                {
                    throw new Exception("Gagal mendapatkan ID User baru.");
                }
                int idUser = Convert.ToInt32(userIdObj);

                // 2. INSERT KE TABEL DETAIL_USER
                string detailQuery = @"INSERT INTO detail_user (id_user, nama_lengkap, negara, no_telepon, tanggal_lahir, elo_rating, created_at, deskripsi)
                                       VALUES (@id_user, @nama_lengkap, @negara, @no_telepon, @tanggal_lahir, 1200, @created_at, @deskripsi)";

                using var detailCmd = new NpgsqlCommand(detailQuery, conn);
                detailCmd.Parameters.AddWithValue("@id_user", idUser);
                detailCmd.Parameters.AddWithValue("@nama_lengkap", detail.Nama_lengkap);
                detailCmd.Parameters.AddWithValue("@negara", detail.Negara);
                detailCmd.Parameters.AddWithValue("@no_telepon", detail.No_telepon);
                detailCmd.Parameters.AddWithValue("@tanggal_lahir", detail.Tanggal_lahir.Date);
                detailCmd.Parameters.AddWithValue("@created_at", DateTime.Today);

                detailCmd.Parameters.AddWithValue("@deskripsi", string.IsNullOrEmpty(detail.Deskripsi) ? (object)DBNull.Value : detail.Deskripsi);

                detailCmd.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Gagal menyimpan data ke Database: {ex.Message}", ex);
            }
        }

        public bool IsEmailExists(string email)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM users WHERE email = @email";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", email);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        public bool IsNoTeleponExists(string noTelepon)
        {
            if (string.IsNullOrEmpty(noTelepon))
                return false;

            using var conn = GetConnection();
            conn.Open();

            string query = @"SELECT COUNT(*) FROM detail_user WHERE no_telepon = @no_telepon";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@no_telepon", noTelepon);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        public User AuthenticateUser(string username, string password)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = @"SELECT id_user, username, passwords, email, is_admin 
                             FROM users 
                             WHERE username = @username AND passwords = @password";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new User(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetBoolean(4)
                );
            }

            return null;
        }

        // ====== DIUBAH: Menambahkan kolom sistem_pertandingan (NOT NULL di DB baru) ======
        public bool TambahTournament(Tournament tournament)
        {
            // 1. Tambahkan kolom 'jumlah_babak' dan parameter '@babak' ke dalam query SQL
            string query = @"INSERT INTO kompetisi (id_user, nama_kompetisi, mode_kompetisi, harga_pendaftaran, pelaksanaan_pendaftaran, tanggal_pelaksanaan, hadiah, sistem_pertandingan, jumlah_babak) 
                     VALUES (@idUser, @nama, @mode, @harga, @pelaksanaanDaftar, @tanggalLaksana, @hadiah, @sistemPertandingan, @babak)";

            try
            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@idUser", tournament.IdUser);
                cmd.Parameters.AddWithValue("@nama", tournament.NamaKompetisi);
                cmd.Parameters.AddWithValue("@mode", tournament.ModeKompetisi);
                cmd.Parameters.AddWithValue("@harga", tournament.HargaPendaftaran);
                cmd.Parameters.AddWithValue("@pelaksanaanDaftar", tournament.PelaksanaanPendaftaran);
                cmd.Parameters.AddWithValue("@tanggalLaksana", tournament.TanggalPelaksanaan.Date);
                cmd.Parameters.AddWithValue("@hadiah", tournament.Hadiah);
                cmd.Parameters.AddWithValue("@sistemPertandingan", tournament.SistemPertandingan ?? "Sistem Swiss"); // Antisipasi NULL

                // 2. Daftarkan parameter baru untuk mengambil nilai JumlahBabak dari model Tournament
                cmd.Parameters.AddWithValue("@babak", tournament.JumlahBabak);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menyimpan ke tabel Tournament: {ex.Message}", ex);
            }
        }

        // ====== DIUBAH: Menambahkan kolom sistem_pertandingan agar muncul di DataGridView ======
        public DataTable AmbilSemuaTournament()
        {
            DataTable dt = new DataTable();
            using var conn = GetConnection();
            conn.Open();

            // Pastikan field jumlah_babak ikut di-SELECT dari tabel kompetisi
            string query = @"SELECT id_kompetisi, nama_kompetisi, mode_kompetisi, harga_pendaftaran, 
                            pelaksanaan_pendaftaran, tanggal_pelaksanaan, hadiah, sistem_pertandingan, jumlah_babak 
                     FROM kompetisi 
                     ORDER BY id_kompetisi DESC";

            using var cmd = new NpgsqlCommand(query, conn);
            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public Detail_User GetDetailUserByUserId(int idUser)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = @"SELECT id_detail_user, id_user, nama_lengkap, negara, no_telepon, tanggal_lahir, elo_rating, created_at, deskripsi 
                             FROM detail_user 
                             WHERE id_user = @id_user";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id_user", idUser);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Detail_User(
                    reader.GetInt32(0),                                      // id_detail_user
                    reader.GetString(2),                                     // nama_lengkap
                    reader.GetString(3),                                     // negara
                    reader.GetString(4),                                     // no_telepon
                    reader.GetDateTime(5),                                   // tanggal_lahir
                    reader.GetInt32(6),                                      // elo_rating
                    reader.GetDateTime(7),                                   // created_at
                    reader.IsDBNull(8) ? "" : reader.GetString(8)            // deskripsi
                );
            }
            return null;
        }

        public DataTable GetDaftarKompetisiAktif()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM v_daftar_kompetisi WHERE tanggal_pelaksanaan >= CURRENT_DATE ORDER BY tanggal_pelaksanaan ASC";

            try
            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal mengambil data kompetisi aktif: {ex.Message}", ex);
            }
        }

        public DataTable AmbilIdDanNamaTournament()
        {
            DataTable dt = new DataTable();
            string query = "SELECT id_kompetisi, nama_kompetisi FROM kompetisi";

            try
            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengambil list turnamen: {ex.Message}");
            }
            return dt;
        }

        // ====== DIUBAH: Menyesuaikan nama tabel dari pendaftaran ke pendaftaran_kompetisi ======
        public DataTable AmbilPendaftarBerdasarkanTournament(int idKompetisi)
        {
            DataTable dt = new DataTable();

            // Query disesuaikan dengan skema tabel pendaftaran_kompetisi dan status_pendaftaran barumu
            string query = @"SELECT pk.id_pendaftaran_kompetisi, du.nama_lengkap, du.negara, du.elo_rating, pk.status_pendaftaran
                             FROM pendaftaran_kompetisi pk
                             JOIN detail_user du ON pk.id_user = du.id_user
                             WHERE pk.id_kompetisi = @idKompetisi";

            try
            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengambil data pendaftar: {ex.Message}");
            }
            return dt;
        }
        // FUNGSI 1: Mengambil data pertandingan berdasarkan turnamen dan babak tertentu
        public DataTable AmbilPertandinganPerBabak(int idKompetisi, int babak)
        {
            DataTable dt = new DataTable();
            using var conn = GetConnection();
            conn.Open();

            // Query untuk mengambil data pertandingan sekaligus men-JOIN nama pemain dari detail_user
            string query = @"SELECT p.id_pertandingan AS ""ID Match"",
                            du_putih.nama_lengkap AS ""Pemain Putih"",
                            du_hitam.nama_lengkap AS ""Pemain Hitam"",
                            p.skor_putih AS ""Skor Putih"",
                            p.skor_hitam AS ""Skor Hitam"",
                            p.hasil AS ""Hasil Match""
                     FROM pertandingan p
                     LEFT JOIN detail_user du_putih ON p.pemain_putih = du_putih.id_user
                     LEFT JOIN detail_user du_hitam ON p.pemain_hitam = du_hitam.id_user
                     WHERE p.id_kompetisi = @idKompetisi AND p.babak = @babak
                     ORDER BY p.id_pertandingan ASC";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                cmd.Parameters.AddWithValue("@babak", babak);

                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Gagal ambil data match: {ex.Message}");
            }

            return dt;
        }

        // FUNGSI 2: Menyimpan inputan skor dari Admin ke tabel pertandingan
        public bool UpdateSkorPertandingan(int idPertandingan, decimal skorPutih, decimal skorHitam, string hasilMatch)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = @"UPDATE pertandingan 
                     SET skor_putih = @sp, skor_hitam = @sh, hasil = @hasil 
                     WHERE id_pertandingan = @id";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sp", skorPutih);
                cmd.Parameters.AddWithValue("@sh", skorHitam);
                cmd.Parameters.AddWithValue("@hasil", hasilMatch);
                cmd.Parameters.AddWithValue("@id", idPertandingan);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Gagal menyimpan skor: {ex.Message}");
                return false;
            }
        }

        // FUNGSI 3: Menghitung LEADERBOARD secara otomatis (Real-time matematika SQL)
        public DataTable AmbilLeaderboardTournament(int idKompetisi)
        {
            DataTable dt = new DataTable();
            using var conn = GetConnection();
            conn.Open();

            string query = @"SELECT ROW_NUMBER() OVER(ORDER BY COALESCE(skor_total.total_poin, 0) DESC) AS Peringkat,
                            du.nama_lengkap AS Nama_Pemain, 
                            du.negara AS Asal_Negara,
                            COALESCE(skor_total.total_poin, 0) AS Total_Poin
                     FROM pendaftaran_kompetisi pk
                     JOIN detail_user du ON pk.id_user = du.id_user
                     LEFT JOIN (
                         SELECT id_user, SUM(poin) AS total_poin
                         FROM (
                             SELECT pemain_putih AS id_user, skor_putih AS poin FROM pertandingan WHERE id_kompetisi = @id_kompetisi
                             UNION ALL
                             SELECT pemain_hitam AS id_user, skor_hitam AS poin FROM pertandingan WHERE id_kompetisi = @id_kompetisi
                         ) AS seluruh_match
                         GROUP BY id_user
                     ) AS skor_total ON pk.id_user = skor_total.id_user
                     WHERE pk.id_kompetisi = @id_kompetisi
                     ORDER BY Total_Poin DESC";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);

                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Gagal memuat leaderboard: {ex.Message}");
            }
            return dt;
        }
        public int AmbilTotalBabakTournament(int idKompetisi)
        {
            using var conn = GetConnection();
            conn.Open();
            string query = "SELECT jumlah_babak FROM kompetisi WHERE id_kompetisi = @id";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", idKompetisi);

            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 1;
        }
        // FUNGSI A: Ambil semua ID User yang terdaftar di suatu turnamen
        public List<int> AmbilPemainTerdaftar(int idKompetisi)
        {
            List<int> listPemain = new List<int>();
            using var conn = GetConnection();
            conn.Open();

            string query = @"SELECT id_user FROM pendaftaran_kompetisi 
                     WHERE id_kompetisi = @idKompetisi AND status_pendaftaran = 'terdaftar'";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listPemain.Add(reader.GetInt32(0));
            }
            return listPemain;
        }

        // FUNGSI B: Cek apakah babak tersebut sudah pernah di-generate matches-nya
        public bool IsBabakSudahGenerated(int idKompetisi, int babak)
        {
            using var conn = GetConnection();
            conn.Open();
            string query = "SELECT COUNT(*) FROM pertandingan WHERE id_kompetisi = @id AND babak = @babak";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", idKompetisi);
            cmd.Parameters.AddWithValue("@babak", babak);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        // FUNGSI C: Simpan rombongan data pertandingan baru hasil generate ke database
        public bool SimpanPertandinganGenerate(int idKompetisi, int babak, List<Tuple<int, int>> pasanganMatch)
        {
            using var conn = GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                string query = @"INSERT INTO pertandingan (id_kompetisi, tanggal_pertandingan, babak, pemain_putih, pemain_hitam, skor_putih, skor_hitam, hasil) 
                         VALUES (@idKompetisi, @tanggal, @babak, @putih, @hitam, 0.00, 0.00, 'Belum Dimainkan')";

                foreach (var match in pasanganMatch)
                {
                    using var cmd = new NpgsqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idKompetisi", idKompetisi);
                    cmd.Parameters.AddWithValue("@tanggal", DateTime.Today); // Default tanggal hari ini saat digenerate
                    cmd.Parameters.AddWithValue("@babak", babak);
                    cmd.Parameters.AddWithValue("@putih", match.Item1); // ID Pemain Putih
                    cmd.Parameters.AddWithValue("@hitam", match.Item2); // ID Pemain Hitam
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                System.Windows.Forms.MessageBox.Show($"Gagal menyimpan generate match: {ex.Message}");
                return false;
            }
        }
    }
}