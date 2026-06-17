using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Models.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProjekPBO_PSQL.Controller
{
    public class ProfilController
    {
        private UserContext dbUser = new UserContext();

        public DataTable AmbilDataProfil(int idUser)
        {
            return dbUser.AmbilDetailProfilFull(idUser);
        }
        public string ProsesUpdateProfil(int idUser, string username, string password, string negara, string noTelepon, string deskripsi)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return "VALIDASI_GAGAL: Username dan Password tidak boleh kosong!";
            }

            if (username.Length <= 5)
            {
                return "VALIDASI_GAGAL: Username tidak valid! Harus lebih dari 5 karakter.";
            }


            if (password.Length < 6)
            {
                return "VALIDASI_GAGAL: Password tidak valid! Minimal harus 6 karakter.";
            }

            if (string.IsNullOrEmpty(noTelepon))
            {
                return "VALIDASI_GAGAL: Nomor Telepon tidak boleh kosong!";
            }

            if (noTelepon.Length < 10 || noTelepon.Length > 13)
            {
                return "VALIDASI_GAGAL: Nomor Telepon tidak valid! Harus antara 10 hingga 13 digit.";
            }

            foreach (char c in noTelepon)
            {
                if (!char.IsDigit(c))
                {
                    return "VALIDASI_GAGAL: Nomor Telepon harus berupa angka seluruhnya!";
                }
            }

            try
            {
                bool berhasil = dbUser.UpdateProfilFull(idUser, username, password, negara, noTelepon, deskripsi);

                if (berhasil)
                {
                    return "SUKSES";
                }
                return "Gagal menyimpan perubahan ke database.";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }

        public ProfilCatur AmbilProfilPemain(int idUser)
        {
            return dbUser.GetDetailUserByUserId(idUser);
        }
    }
}