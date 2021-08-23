using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_KTTM_1
{
    public partial class Kvctptc
    {
        public long Id { get; set; }
        public string Kvptcid { get; set; }
        public string Httc { get; set; }
        public string DienGiai { get; set; }
        public string Tkno { get; set; }
        public string Tkco { get; set; }
        public string Sgtcode { get; set; }
        public string MaKhNo { get; set; }
        public string MaKhCo { get; set; }
        public decimal? SoTienNt { get; set; }
        public string LoaiTien { get; set; }
        public decimal? TyGia { get; set; }
        public decimal? SoTien { get; set; }
        public string MaKh { get; set; }
        public string KhoangMuc { get; set; }
        public string Httt { get; set; }
        public string CardNumber { get; set; }
        public string SalesSlip { get; set; }
        public string SoXe { get; set; }
        public string MsThue { get; set; }
        public string LoaiHdgoc { get; set; }
        public string SoCtgoc { get; set; }
        public DateTime? NgayCtgoc { get; set; }
        public decimal? Vat { get; set; }
        public decimal? DskhongVat { get; set; }
        public string BoPhan { get; set; }
        public string Stt { get; set; }
        public string NoQuay { get; set; }
        public string CoQuay { get; set; }
        public string TenKh { get; set; }
        public string DiaChi { get; set; }
        public string MatHang { get; set; }
        public string KyHieu { get; set; }
        public string MauSoHd { get; set; }
        public bool? DieuChinh { get; set; }
        public DateTime? Kc141 { get; set; }
        public string TamUng { get; set; }
        public string DienGiaiP { get; set; }
        public string HoaDonDt { get; set; }
        public string LogFile { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }
        public string LinkHddt { get; set; }
        public string MaTraCuu { get; set; }
        public string TkTruyCap { get; set; }
        public string Password { get; set; }

        public virtual Kvptc Kvptc { get; set; }
        public virtual TamUng TamUngNavigation { get; set; }
    }
}
