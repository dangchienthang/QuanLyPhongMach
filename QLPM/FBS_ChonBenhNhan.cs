﻿using QLPM.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLPM
{
    public partial class FBS_ChonBenhNhan : Form
    {
        BUS_LichKham busLK;

        public FBS_ChonBenhNhan()
        {
            InitializeComponent();
            busLK = new BUS_LichKham();
        }

        public void HienThiDSLichKham()
        {
            gVLK.DataSource = null;
            busLK.LayDSLK(gVLK);
            gVLK.Columns[0].Width = (int)(gVLK.Width * 0.12);
            gVLK.Columns[1].Width = (int)(gVLK.Width * 0.22);
            gVLK.Columns[2].Width = (int)(gVLK.Width * 0.22);
            gVLK.Columns[3].Width = (int)(gVLK.Width * 0.22);
            gVLK.Columns[4].Width = (int)(gVLK.Width * 0.22);

            gVLK.Columns[0].HeaderText = "Mã lịch khám";
            gVLK.Columns[1].HeaderText = "Mã bệnh nhân";
            gVLK.Columns[2].HeaderText = "Mã y tá";
            gVLK.Columns[3].HeaderText = "Mã bác sĩ";
            gVLK.Columns[4].HeaderText = "Ngày hẹn";
        }

        private void DoiMauNut()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = Mau.MauChinh;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = Mau.MauPhu;
                }
            }
        }

        private void FChonBenhNhan_Load(object sender, EventArgs e)
        {
            HienThiDSLichKham();
            DoiMauNut();
        }

        private void gVLK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gVLK.Rows.Count)
            {
                txtMaLK.Text = gVLK.Rows[e.RowIndex].Cells["MaLK"].Value.ToString();
                txtMaBN.Text = gVLK.Rows[e.RowIndex].Cells["MaBN"].Value.ToString();
                txtMaYT.Text = gVLK.Rows[e.RowIndex].Cells["MaYT"].Value.ToString();
                txtMaBS.Text = gVLK.Rows[e.RowIndex].Cells["MaBS"].Value.ToString();
                dtpNgayHen.Text = gVLK.Rows[e.RowIndex].Cells["NgayHen"].Value.ToString();
            }
        }

        private void btKham_Click(object sender, EventArgs e)
        {
            if (txtMaLK.Text == "")
            {
                MessageBox.Show("Vui lòng chọn bệnh nhân để khám", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int maBN, maBS;
                maBN = Int32.Parse(gVLK.CurrentRow.Cells["MaBN"].Value.ToString());
                maBS = Int32.Parse(gVLK.CurrentRow.Cells["MaBS"].Value.ToString());
                FKhamBenh f = new FKhamBenh();
                f.maBenhNhan = maBN;
                f.maBacSi = maBS;
                f.ShowDialog();
            }
        }
    }
}
