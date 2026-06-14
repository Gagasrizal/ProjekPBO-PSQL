using Npgsql;
using ProjekPBO_PSQL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProjekPBO_PSQL.Models.Context
{
    public bool UpdateHasilPertandingan(int idPertandingan, string hasil)
    {
        double skorPutih = 0;
        double skorHitam = 0;

        // Menentukan poin papan skor turnamen
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
            using var conn = DBHelper.GetConnection();
            conn.Open();

            // Penerapan TRANSACTION: Kunci proses data pertandingan agar aman
            using var transaction = conn.BeginTransaction();

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@skor_putih", skorPutih);
            cmd.Parameters.AddWithValue("@skor_hitam", skorHitam);
            cmd.Parameters.AddWithValue("@hasil", hasil);
            cmd.Parameters.AddWithValue("@id_pertandingan", idPertandingan);

            int rowsAffected = cmd.ExecuteNonQuery();

            // Sahkan transaksi ke PostgreSQL
            transaction.Commit();

            // Di sini trigger otomatis bekerja di DB untuk update ELO (+8/-8/0)
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal mengupdate skor dan ELO pertandingan: {ex.Message}");
        }
    }


    public DataTable AmbilPertandinganDenganTotalPoin(int idKompetisi, int babak)
    {
        DataTable dt = new DataTable();

        // KODINGAN QUERY-NYA JADI SINGKAT SEPERTI INI, GAS!
        string query = @"
            SELECT id_pertandingan, babak, pemain_putih_label, pemain_hitam_label, hasil 
            FROM v_pertandingan_lengkap 
            WHERE id_kompetisi = @id_kompetisi AND babak = @babak";

        try
        {
            // Mengambil koneksi via DBHelper static milik kelompokmu
            using var conn = DBHelper.GetConnection();
            conn.Open();

            using var cmd = new NpgsqlCommand(query, conn);
            // Penerapan Statement (Parameterized Query)
            cmd.Parameters.AddWithValue("@id_kompetisi", idKompetisi);
            cmd.Parameters.AddWithValue("@babak", babak);

            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dt);
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal memuat data pertandingan lewat VIEW: {ex.Message}");
        }

        return dt;
    }
}




   