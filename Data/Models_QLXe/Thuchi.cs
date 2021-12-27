using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_QLXe
{
    public partial class Thuchi
    {
        public int ThuChiId { get; set; }
        public string LoaiThuChi { get; set; }
        public string SoPhieu { get; set; }
        public DateTime? Ngay { get; set; }
        public string SoXe { get; set; }
        public string TaiXe { get; set; }
        public string KhoanMuc { get; set; }
        public long? VanDoanhId { get; set; }
        public string LoaiTien { get; set; }
        public decimal? TyGia { get; set; }
        public decimal? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public string Dvt { get; set; }
        public decimal? SoTien { get; set; }
        public string SoChungTuNb { get; set; }
        public string KyHieu { get; set; }
        public int? Stt { get; set; }
        public string HoaDonVat { get; set; }
        public DateTime? NgayVat { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string NguoiCapNhat { get; set; }
        public string ChiNhanh { get; set; }
    }
}
