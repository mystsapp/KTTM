using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_QLXe
{
    public partial class Vandoanh
    {
        public long VanDoanhId { get; set; }
        public string SoPhieu { get; set; }
        public string MaDoan { get; set; }
        public string YeuCauXe { get; set; }
        public string NguonXe { get; set; }
        public int? Sttxe { get; set; }
        public string SoXe { get; set; }
        public string TaiXe { get; set; }
        public string DienThoai { get; set; }
        public int? SoKhach { get; set; }
        public DateTime? NgayDon { get; set; }
        public TimeSpan? GioDon { get; set; }
        public string DiemDon { get; set; }
        public DateTime? DenNgay { get; set; }
        public string KhachHang { get; set; }
        public string LoTrinh { get; set; }
        public int? SoKm { get; set; }
        public int? SoKmThucTe { get; set; }
        public string HuongDan { get; set; }
        public string DienThoaiHd { get; set; }
        public string PhuongThucTt { get; set; }
        public decimal? TongTien { get; set; }
        public string GhiChu { get; set; }
        public string SoCtp { get; set; }
        public DateTime? NgayCtp { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string NguoiCapNhat { get; set; }
        public bool? DaThuChi { get; set; }
        public string ChiNhanh { get; set; }
        public string LoaiXe { get; set; }
    }
}
