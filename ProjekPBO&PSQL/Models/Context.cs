using System;
using System.Collections.Generic;
using System.Text;

namespace ProjekPBO_PSQL.Models
{ 
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

    public bool RegisterUser(Admin user, ProfilCatur detail)
    {
        using var conn = GetConnection();
        conn.Open();
        using var transaction = conn.BeginTransaction();

        try
        {

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

    public Admin AuthenticateUser(string username, string password)
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
            return new Admin(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetBoolean(4)
            );
        }

        return null;
    }

    public bool TambahTournament(Tournament tournament)
    {

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
            cmd.Parameters.AddWithValue("@babak", tournament.JumlahBabak);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal menyimpan ke tabel Tournament: {ex.Message}", ex);
        }
    }

    public bool DaftarKeKompetisi(int idUser, int idKompetisi, int idMetodeBayar, int nominal)
    {
        using var conn = GetConnection();
        conn.Open();


        using var transaction = conn.BeginTransaction();

        try
        {

            string queryDaftar = @"INSERT INTO pendaftaran_kompetisi (id_user, id_kompetisi, status_pendaftaran) 
                                      VALUES (@id_user, @id_kompetisi, @status) 
                                      RETURNING id_pendaftaran_kompetisi";

            using var cmdDaftar = new NpgsqlCommand(queryDaftar, conn);
            cmdDaftar.Parameters.AddWithValue("@id_user", idUser);
            cmdDaftar.Parameters.AddWithValue("@id_kompetisi", idKompetisi);
            cmdDaftar.Parameters.AddWithValue("@status", "terdaftar");


            object daftarIdObj = cmdDaftar.ExecuteScalar();
            if (daftarIdObj == null)
            {
                throw new Exception("Gagal mendapatkan nomor pendaftaran kompetisi.");
            }
            int idPendaftaranBaru = Convert.ToInt32(daftarIdObj);


            string queryBayar = @"INSERT INTO pembayaran (id_pendaftaran_kompetisi, id_metode_pembayaran, nominal_pembayaran, tanggal_pembayaran) 
                                      VALUES (@id_daftar, @id_metode, @nominal, @tanggal)";

            using var cmdBayar = new NpgsqlCommand(queryBayar, conn);
            cmdBayar.Parameters.AddWithValue("@id_daftar", idPendaftaranBaru);
            cmdBayar.Parameters.AddWithValue("@id_metode", idMetodeBayar);
            cmdBayar.Parameters.AddWithValue("@nominal", nominal);
            cmdBayar.Parameters.AddWithValue("@tanggal", DateTime.Now); //

            cmdBayar.ExecuteNonQuery();


            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {

            transaction.Rollback();
            throw new Exception($"Proses pendaftaran turnamen gagal: {ex.Message}", ex);
        }
    }

    public bool EditTournament(Tournament tournament)
    {

        string query = @"UPDATE kompetisi 
                             SET nama_kompetisi = @nama, 
                                 mode_kompetisi = @mode, 
                                 harga_pendaftaran = @harga, 
                                 pelaksanaan_pendaftaran = @pelaksanaanDaftar, 
                                 tanggal_pelaksanaan = @tanggalLaksana, 
                                 hadiah = @hadiah, 
                                 sistem_pertandingan = @sistemPertandingan, 
                                 jumlah_babak = @babak
                             WHERE id_kompetisi = @idKompetisi";

        try
        {
            using var conn = GetConnection();
            conn.Open();
            using var cmd = new NpgsqlCommand(query, conn);


            cmd.Parameters.AddWithValue("@nama", tournament.NamaKompetisi);
            cmd.Parameters.AddWithValue("@mode", tournament.ModeKompetisi);
            cmd.Parameters.AddWithValue("@harga", tournament.HargaPendaftaran);
            cmd.Parameters.AddWithValue("@pelaksanaanDaftar", tournament.PelaksanaanPendaftaran);
            cmd.Parameters.AddWithValue("@tanggalLaksana", tournament.TanggalPelaksanaan.Date);
            cmd.Parameters.AddWithValue("@hadiah", tournament.Hadiah);
            cmd.Parameters.AddWithValue("@sistemPertandingan", tournament.SistemPertandingan ?? "Sistem Swiss");
            cmd.Parameters.AddWithValue("@babak", tournament.JumlahBabak);


            cmd.Parameters.AddWithValue("@idKompetisi", tournament.IdKompetisi);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal mengupdate data Tournament: {ex.Message}", ex);
        }
    }

    public DataTable AmbilSemuaTournament()
    {
        DataTable dt = new DataTable();
        using var conn = GetConnection();
        conn.Open();


        string query = @"SELECT id_kompetisi, nama_kompetisi, mode_kompetisi, harga_pendaftaran, 
                            pelaksanaan_pendaftaran, tanggal_pelaksanaan, hadiah, sistem_pertandingan, jumlah_babak 
                     FROM kompetisi 
                     ORDER BY id_kompetisi ASC";

        using var cmd = new NpgsqlCommand(query, conn);
        using var adapter = new NpgsqlDataAdapter(cmd);
        adapter.Fill(dt);
        return dt;
    }

    public ProfilCatur GetDetailUserByUserId(int idUser)
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
            return new ProfilCatur(
                reader.GetInt32(0),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetDateTime(5),
                reader.GetInt32(6),
                reader.GetDateTime(7),
                reader.IsDBNull(8) ? "" : reader.GetString(8)
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


    public DataTable AmbilPendaftarBerdasarkanTournament(int idKompetisi)
    {
        DataTable dt = new DataTable();


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

    public DataTable AmbilPertandinganPerBabak(int idKompetisi, int babak)
    {
        DataTable dt = new DataTable();
        string query = @"
        SELECT 
            p.id_pertandingan AS ""ID"",
            p.babak AS ""Babak"",
            COALESCE(u1.nama_lengkap, 'ID User: ' || p.pemain_putih) AS ""Pemain Putih"",
            COALESCE(u2.nama_lengkap, 'ID User: ' || p.pemain_hitam) AS ""Pemain Hitam"",
            p.skor_putih AS ""Skor Putih"",
            p.skor_hitam AS ""Skor Hitam"",
            p.hasil AS ""Hasil""
        FROM pertandingan p
        LEFT JOIN detail_user u1 ON p.pemain_putih = u1.id_user
        LEFT JOIN detail_user u2 ON p.pemain_hitam = u2.id_user
        WHERE p.id_kompetisi = @id_kompetisi AND p.babak = @babak
        ORDER BY p.id_pertandingan ASC;";

        try
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);
                    cmd.Parameters.AddWithValue("@babak", babak);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal memuat tabel pertandingan: {ex.Message}");
        }
        return dt;
    }
    public DataTable AmbilPertandinganDenganTotalPoin(int idKompetisi, int babak) //group by, subquery, teori himpunan
    {
        DataTable dt = new DataTable();

        string query = @"
WITH akumulasi_poin AS (
    -- Hitung total poin setiap pemain dari seluruh babak yang sudah selesai
    SELECT id_user, COALESCE(SUM(poin), 0) AS total_poin
    FROM (
        SELECT pemain_putih AS id_user, skor_putih AS poin
        FROM pertandingan
        WHERE id_kompetisi = @id_kompetisi
          AND hasil IS NOT NULL
          AND hasil <> 'Belum Dimainkan'
          AND hasil <> ''
        UNION ALL
        SELECT pemain_hitam AS id_user, skor_hitam AS poin
        FROM pertandingan
        WHERE id_kompetisi = @id_kompetisi
          AND hasil IS NOT NULL
          AND hasil <> 'Belum Dimainkan'
          AND hasil <> ''
    ) sub
    GROUP BY id_user
)
SELECT
    p.id_pertandingan,
    p.babak,
    -- Label Pemain Hitam: 'Nama (total_poin)'
    COALESCE(dh.nama_lengkap, 'ID: ' || p.pemain_hitam::text)
        || ' (' || COALESCE(ah.total_poin, 0)::text || ')' AS pemain_hitam_label,
    -- Label Pemain Putih: 'Nama (total_poin)'
    COALESCE(dp.nama_lengkap, 'ID: ' || p.pemain_putih::text)
        || ' (' || COALESCE(ap.total_poin, 0)::text || ')' AS pemain_putih_label,
    -- Hasil babak ini
    p.hasil
FROM pertandingan p
LEFT JOIN detail_user dh  ON p.pemain_hitam = dh.id_user
LEFT JOIN detail_user dp  ON p.pemain_putih = dp.id_user
LEFT JOIN akumulasi_poin ah ON p.pemain_hitam = ah.id_user
LEFT JOIN akumulasi_poin ap ON p.pemain_putih = ap.id_user
WHERE p.id_kompetisi = @id_kompetisi
  AND p.babak = @babak
ORDER BY p.id_pertandingan ASC;";

        try
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);
                    cmd.Parameters.AddWithValue("@babak", babak);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal memuat pertandingan dengan poin: {ex.Message}");
        }

        return dt;
    }

    public DataTable AmbilLeaderboardTournament(int idKompetisi) //groupby
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
    public bool BayarDanDaftarOtomatis(Transaksi trx)
    {

        string queryDaftar = @"
        INSERT INTO pendaftaran_kompetisi (id_user, id_kompetisi, status_pendaftaran) 
        VALUES (@id_user, @id_kompetisi, 'terdaftar')
        RETURNING id_pendaftaran_kompetisi;";


        string queryTransaksi = @"
        INSERT INTO transaksi (id_pendaftaran_kompetisi, id_metode_pembayaran, nominal_transaksi, status_transaksi, tanggal_transaksi) 
        VALUES (@id_pendaftaran, @id_metode, @nominal, 'Sukses', @tanggal);";

        using (NpgsqlConnection conn = GetConnection())
        {
            conn.Open();
            using (NpgsqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    int idPendaftaranBaru = 0;

                    // --- PROSES 1: INSERT KE TABEL PENDAFTARAN ---
                    using (NpgsqlCommand cmdDaftar = new NpgsqlCommand(queryDaftar, conn))
                    {
                        cmdDaftar.Parameters.AddWithValue("@id_user", trx.IdUser);
                        cmdDaftar.Parameters.AddWithValue("@id_kompetisi", trx.IdKompetisi);

                        object result = cmdDaftar.ExecuteScalar();
                        if (result == null)
                        {
                            throw new Exception("Gagal mendapatkan ID pendaftaran kompetisi.");
                        }
                        idPendaftaranBaru = Convert.ToInt32(result);
                    }

                    // --- PROSES 2: INSERT KE TABEL TRANSAKSI ---
                    using (NpgsqlCommand cmdTrx = new NpgsqlCommand(queryTransaksi, conn))
                    {
                        cmdTrx.Parameters.AddWithValue("@id_pendaftaran", idPendaftaranBaru);
                        cmdTrx.Parameters.AddWithValue("@id_metode", trx.IdMetodePembayaran);
                        cmdTrx.Parameters.AddWithValue("@nominal", trx.NominalTransaksi);
                        cmdTrx.Parameters.AddWithValue("@tanggal", DateTime.Now);

                        cmdTrx.ExecuteNonQuery();
                    }

                    // Jika kedua proses berhasil tanpa interupsi, commit data secara permanen
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // Jika salah satu proses gagal, batalkan semuanya (rollback) agar tidak ada data menggantung
                    transaction.Rollback();
                    throw new Exception($"Gagal memproses transaksi (Model): {ex.Message}", ex);
                }
            }
        }
    }
    public DataTable AmbilSemuaPembayaran()
    {
        DataTable dt = new DataTable();
        string query = @"
        SELECT 
            t.id_transaksi AS ""ID Transaksi"",
            u.username AS ""Username Pemain"",
            k.nama_kompetisi AS ""Nama Turnamen"",
            m.nama_metode_pembayaran AS ""Metode"",
            t.nominal_transaksi AS ""Nominal"",
            t.status_transaksi AS ""Status"",
            t.tanggal_transaksi AS ""Tanggal""
        FROM transaksi t
        JOIN pendaftaran_kompetisi pk ON t.id_pendaftaran_kompetisi = pk.id_pendaftaran_kompetisi
        JOIN users u ON pk.id_user = u.id_user
        JOIN kompetisi k ON pk.id_kompetisi = k.id_kompetisi
        JOIN metode_pembayaran m ON t.id_metode_pembayaran = m.id_metode_pembayaran
        ORDER BY t.id_transaksi DESC;";

        try
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal mengambil data transaksi: {ex.Message}", ex);
        }

        return dt;
    }
    public bool UpdateHasilPertandingan(int idPertandingan, string hasil)
    {
        double skorPutih = 0;
        double skorHitam = 0;

        if (hasil == "1-0") { skorPutih = 1.0; skorHitam = 0.0; }
        else if (hasil == "0-1") { skorPutih = 0.0; skorHitam = 1.0; }
        else if (hasil == "1/2-1/2") { skorPutih = 0.5; skorHitam = 0.5; }

        string query = @"
        UPDATE pertandingan 
        SET skor_putih = @skor_putih, 
            skor_hitam = @skor_hitam, 
            hasil      = @hasil 
        WHERE id_pertandingan = @id_pertandingan";

        try
        {
            using var conn = GetConnection();
            conn.Open();
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@skor_putih", skorPutih);
            cmd.Parameters.AddWithValue("@skor_hitam", skorHitam);
            cmd.Parameters.AddWithValue("@hasil", hasil);
            cmd.Parameters.AddWithValue("@id_pertandingan", idPertandingan);

            int rowsAffected = cmd.ExecuteNonQuery();


            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal mengupdate skor pertandingan: {ex.Message}");
        }
    }
    public bool ApakahSemuaPertandinganSelesai(int idKompetisi, int babak)
    {
        string query = @"
        SELECT COUNT(*) 
        FROM pertandingan 
        WHERE id_kompetisi = @id_kompetisi 
          AND babak = @babak 
          AND (hasil IS NULL OR hasil = '' OR hasil = 'Belum Dimainkan');";

        try
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);
                    cmd.Parameters.AddWithValue("@babak", babak);

                    long hitungKosong = Convert.ToInt64(cmd.ExecuteScalar());
                    return hitungKosong == 0;
                }
            }
        }
        catch
        {
            return false;
        }
    }

    public (string NamaPutih, int EloPutih, string NamaHitam, int EloHitam) AmbilEloSetelahUpdate(int idPertandingan)
    {
        string query = @"
        SELECT 
            dp.nama_lengkap AS nama_putih, 
            dp.elo_rating   AS elo_putih,
            dh.nama_lengkap AS nama_hitam, 
            dh.elo_rating   AS elo_hitam
        FROM pertandingan p
        JOIN detail_user dp ON p.pemain_putih = dp.id_user
        JOIN detail_user dh ON p.pemain_hitam = dh.id_user
        WHERE p.id_pertandingan = @id";

        using var conn = GetConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", idPertandingan);
        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return (
                    reader["nama_putih"].ToString(),
                    Convert.ToInt32(reader["elo_putih"]),
                    reader["nama_hitam"].ToString(),
                    Convert.ToInt32(reader["elo_hitam"])
                );
        }

        return ("?", 0, "?", 0);
    }
    public DataTable AmbilSemuaKompetisi()
    {
        DataTable dt = new DataTable();


        string query = "SELECT id_kompetisi, nama_kompetisi FROM kompetisi ORDER BY id_kompetisi ASC;";

        try
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal mengambil daftar kompetisi: {ex.Message}", ex);
        }

        return dt;
    }
    public DataTable AmbilHistoryPertandingan(int idUser)//
    {
        DataTable dt = new DataTable();
        string query = @"
        SELECT
            k.nama_kompetisi                AS ""Tournament"",
            p.babak                         AS ""Babak"",
            -- Subquery: ambil nama lawan
            CASE 
                WHEN p.pemain_putih = @id_user 
                    THEN COALESCE(lawan.nama_lengkap, 'Unknown')
                ELSE 
                    COALESCE(lawan2.nama_lengkap, 'Unknown')
            END                             AS ""Lawan"",
            -- Warna bidak pemain
            CASE 
                WHEN p.pemain_putih = @id_user THEN 'Putih'
                ELSE 'Hitam'
            END                             AS ""Warna"",
            -- Hasil dari sudut pandang pemain ini
            CASE
                WHEN p.hasil = 'Belum Dimainkan' THEN 'Belum Dimainkan'
                WHEN p.pemain_putih = @id_user AND p.hasil = '1-0' THEN 'Menang'
                WHEN p.pemain_putih = @id_user AND p.hasil = '0-1' THEN 'Kalah'
                WHEN p.pemain_hitam = @id_user AND p.hasil = '0-1' THEN 'Menang'
                WHEN p.pemain_hitam = @id_user AND p.hasil = '1-0' THEN 'Kalah'
                WHEN p.hasil = '1/2-1/2'                           THEN 'Remis' 
                ELSE p.hasil
            END                             AS ""Hasil""
        FROM pertandingan p
        JOIN kompetisi k ON p.id_kompetisi = k.id_kompetisi
        -- Join untuk dapat nama lawan (kalau pemain = putih, lawan = hitam)
        LEFT JOIN detail_user lawan  ON p.pemain_hitam = lawan.id_user
        LEFT JOIN detail_user lawan2 ON p.pemain_putih = lawan2.id_user
        WHERE p.pemain_putih = @id_user OR p.pemain_hitam = @id_user
        ORDER BY k.nama_kompetisi, p.babak ASC;";

        try
        {
            using var conn = GetConnection();
            conn.Open();
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id_user", idUser);
            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dt);
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal mengambil history pertandingan: {ex.Message}");
        }
        return dt;//d
    }
}
}
}
