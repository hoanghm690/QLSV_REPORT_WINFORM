﻿using HHT_QLSV_REPORT.Model;
using HHT_QLSV_REPORT.Reporting;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HHT_QLSV_REPORT
{
    public partial class FrmThongKeKetQuaSV : Form
    {
        StudentDB db = new StudentDB();
        public FrmThongKeKetQuaSV()
        {
            InitializeComponent();
        }

        private void FrmThongKeKetQuaSV_Load(object sender, EventArgs e)
        {
            HienThiThongKeDiemSV();
            //this.rpvThongKeDiem.RefreshReport();
        }

        private void HienThiThongKeDiemSV()
        {
            string truyVanSQL = "SELECT s.MaSV, TenSV, TenMH, Diem " +
                                "FROM MonHoc m, SinhVien s, SV_Diem svd " +
                                "WHERE m.MaMH = svd.MaMH AND s.MaSV = svd.MaSV";
            List<ThongKeDiem> list = db.Database.SqlQuery<ThongKeDiem>(truyVanSQL).ToList();
            if (txtHoTen.Text != "")
            {
                list = list.Where(x => x.TenSV.ToLower() == txtHoTen.Text.ToLower()).ToList();
            }
            this.rpvThongKeDiem.LocalReport.ReportPath = "ReportKetQuaSV.rdlc";
            var reportDataSource = new ReportDataSource("ThongKeDiemDataset", list);
            this.rpvThongKeDiem.LocalReport.DataSources.Clear();
            this.rpvThongKeDiem.LocalReport.DataSources.Add(reportDataSource);
            this.rpvThongKeDiem.RefreshReport();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            HienThiThongKeDiemSV();
        }

        //event keydown enter
        private void txtHoTen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnBaoCao.PerformClick();
        }
    }
}
