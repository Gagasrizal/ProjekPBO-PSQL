using ProjekPBO_PSQL.Models;
using ProjekPBO_PSQL.Models.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProjekPBO_PSQL.Controller
{
    public class TransaksiController
    {
        private readonly TransaksiContext _context;

        public TransaksiController()
        {
            _context = new TransaksiContext();
        }

        public void MuatTransaksiAdmin(DataGridView dgv)
        {
            try
            {
                DataTable dt = _context.GetAllTransaksiPemain();
                dgv.DataSource = dt;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.ReadOnly = true;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int GetBiayaKompetisi(int idKompetisi)
        {
            try
            {
                return _context.GetBiayaKompetisi(idKompetisi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public bool DaftarTurnamen(int idPemain, int idKompetisi, int idMetode, int nominalDiinput)
        {
            try
            {
                if (_context.IsSudahDaftar(idPemain, idKompetisi))
                {
                    MessageBox.Show("Kamu sudah terdaftar di kompetisi ini!",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                int biayaResmi = _context.GetBiayaKompetisi(idKompetisi);

                Transaksi transaksi = new Transaksi
                {
                    NominalTransaksi = biayaResmi  
                };

                transaksi.LakukanPembayaran(nominalDiinput); 

                _context.DaftarDanBayar(idPemain, idKompetisi, idMetode, biayaResmi);

                MessageBox.Show("Pendaftaran berhasil! Pembayaran lunas.",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Data tidak valid: " + ex.Message,
                    "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}

