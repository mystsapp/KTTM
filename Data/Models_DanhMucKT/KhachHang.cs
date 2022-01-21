using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_DanhMucKT
{
    public partial class KhachHang
    {
        public string Code { get; set; }
        public string TenGiaoDich { get; set; }
        public string TenThuongMai { get; set; }
        public string MaSoThue { get; set; }
        public string TknganHang { get; set; }
        public string TenNganHang { get; set; }
        public string Tpnh { get; set; }
        public string ThanhPho { get; set; }
        public string DiaChi { get; set; }
        public string QuocGia { get; set; }
        public string DienThoai { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string KyHieuHd { get; set; }
        public string MauSoHd { get; set; }
        public string LogFile { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }
        public string LoaiKh { get; set; }
        public bool? DaiLyVmb { get; set; }
        public string LinkHddt { get; set; }
    }
}
