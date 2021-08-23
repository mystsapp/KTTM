using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_KTTM_1
{
    public partial class Tt621
    {
        public long Id { get; set; }
        public long TamUngId { get; set; }
        public string MaKhNo { get; set; }
        public string SoCt { get; set; }
        public DateTime? NgayCt { get; set; }
        public string PhieuTc { get; set; }
        public string PhieuTu { get; set; }
        public string DienGiai { get; set; }
        public string LoaiTien { get; set; }
        public decimal SoTien { get; set; }
        public decimal SoTienNt { get; set; }
        public decimal TyGia { get; set; }
        public string Tkno { get; set; }
        public string Tkco { get; set; }
        public string MaKhCo { get; set; }
        public string Sgtcode { get; set; }
        public string Httc { get; set; }
        public string GhiSo { get; set; }
        public string MsThue { get; set; }
        public DateTime? NgayCtgoc { get; set; }
        public string LoaiHdgoc { get; set; }
        public string SoCtgoc { get; set; }
        public string KyHieuHd { get; set; }
        public decimal Vat { get; set; }
        public decimal DskhongVat { get; set; }
        public string BoPhan { get; set; }
        public string NoQuay { get; set; }
        public string CoQuay { get; set; }
        public string SoXe { get; set; }
        public string TenKh { get; set; }
        public string DiaChi { get; set; }
        public string KyHieu { get; set; }
        public string MauSoHd { get; set; }
        public string MatHang { get; set; }
        public bool DieuChinh { get; set; }
        public string LapPhieu { get; set; }
        public string DienGiaiP { get; set; }
        public string HoaDonDt { get; set; }
        public string LogFile { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }

        public virtual TamUng TamUng { get; set; }
    }
}
