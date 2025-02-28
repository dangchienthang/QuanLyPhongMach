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
    public partial class FQLLichKham : Form
    { 
        BUS_LichKham busLK;
        public FQLLichKham()
        {
            InitializeComponent();
            busLK = new BUS_LichKham();
        }

        public void HienThiDSLichKham()
        {
            gVLK.DataSource = null;
            busLK.LayDSLichKham(gVLK);
            gVLK.Columns[0].Width = (int)(gVLK.Width * 0.12);
            gVLK.Columns[1].Width = (int)(gVLK.Width * 0.22);
            gVLK.Columns[2].Width = (int)(gVLK.Width * 0.22);
            gVLK.Columns[3].Width = (int)(gVLK.Width * 0.22);
            gVLK.Columns[4].Width = (int)(gVLK.Width * 0.22);

            gVLK.Columns[0].HeaderText = "Mã lịch khám";
            gVLK.Columns[1].HeaderText = "Tên bệnh nhân";
            gVLK.Columns[2].HeaderText = "Y tá tạo lịch";
            gVLK.Columns[3].HeaderText = "Bác sĩ chỉ định";
            gVLK.Columns[4].HeaderText = "Ngày hẹn";
        }

        public void CapNhatDGLK()
        {
            //Cập nhật lại textbox sau khi thực hiện hành động thêm, sửa hoặc xóa
            txtMaLK.Text = "";
            cbBN.Text = "";
            cbYT.Text = "";
            cbBS.Text = "";

            //Cập nhật DataGridView
            gVLK.Columns.Clear();
            HienThiDSLichKham();

            dtpNgayHen.Value = DateTime.Now;
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

        private void FQLLichKham_Load(object sender, EventArgs e)
        {
            DoiMauNut();
            HienThiDSLichKham();
            busLK.LayDSTenBenhNhan(cbBN);
            busLK.LayDSTenBacSi(cbBS);
            busLK.LayDSTenYTa(cbYT);
        }

        private void btXoaLich_Click(object sender, EventArgs e)
        {
            if (txtMaLK.Text != "")
            {
                busLK.XoaLichKham(Int32.Parse(txtMaLK.Text));
                busLK.LayDSLichKham(gVLK);
                CapNhatDGLK();
            }
            else
            {
                MessageBox.Show("Chưa chọn lịch để xóa");
            }
        }

        private void btSuaLich_Click(object sender, EventArgs e)
        {
            if (txtMaLK.Text == "" || cbBN.Text == "" || cbBS.Text == "" || cbYT.Text == "")
                MessageBox.Show("Vui lòng chọn hàng dữ liệu cần sửa!");
            else
            {
                LichKham lk = new LichKham();

                lk.MaLK = int.Parse(txtMaLK.Text);
                lk.MaBN = int.Parse(cbBN.SelectedValue.ToString());
                lk.MaBS = int.Parse(cbBS.SelectedValue.ToString());
                lk.MaYT = int.Parse(cbYT.SelectedValue.ToString());
                lk.NgayHen = dtpNgayHen.Value;

                if (busLK.SuaLichKham(lk))
                {
                    MessageBox.Show("Sửa thành công");
                    busLK.SuaLichKham(lk);
                    busLK.LayDSLichKham(gVLK);
                    CapNhatDGLK();
                }
                else
                {
                    MessageBox.Show("Sửa thất bại");
                }
            }
        }

        private void btThemLich_Click(object sender, EventArgs e)
        {
            if (cbBN.Text == "" || cbBS.Text == "" || cbYT.Text == "")
            {
                MessageBox.Show("Diền đủ thông tin trước khi thêm");
            }
            else
            {
                LichKham lk = new LichKham();

                lk.MaBN = Int32.Parse(cbBN.SelectedValue.ToString());
                lk.MaBS = Int32.Parse(cbBS.SelectedValue.ToString());
                lk.MaYT = Int32.Parse(cbYT.SelectedValue.ToString());
                lk.NgayHen = dtpNgayHen.Value;

                if (busLK.TaoLichKham(lk))
                {
                    MessageBox.Show("Tạo lịch khám thành công");
                    busLK.LayDSLichKham(gVLK);
                    CapNhatDGLK();
                }
                else
                {
                    MessageBox.Show("Tạo lịch khám thất bại");
                }
            }    
        }

        private void btThongKe_Click(object sender, EventArgs e)
        {

        }

        private void gVLK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gVLK.Rows.Count)
            {
                txtMaLK.Text = gVLK.Rows[e.RowIndex].Cells["MaLK"].Value.ToString();
                cbBN.Text = gVLK.Rows[e.RowIndex].Cells[1].Value.ToString();
                cbYT.Text = gVLK.Rows[e.RowIndex].Cells[2].Value.ToString();
                cbBS.Text = gVLK.Rows[e.RowIndex].Cells[3].Value.ToString();
                dtpNgayHen.Text = gVLK.Rows[e.RowIndex].Cells[4].Value.ToString();
            }

        }
    }
}
