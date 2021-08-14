using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Dtos
{
    public class TT621Dto
    {
        public long Id { get; set; }

        public long TamUngId { get; set; }

        public string MaKhNo { get; set; }

        public string SoCT { get; set; }

        public DateTime? NgayCT { get; set; }

        public string PhieuTC { get; set; }

        public string PhieuTU { get; set; }

        public string DienGiai { get; set; }

        public string LoaiTien { get; set; }

        public decimal? SoTien { get; set; }

        public decimal? SoTienNT { get; set; }

        public decimal? TyGia { get; set; }

        public string TKNo { get; set; }

        public string TKCo { get; set; }

        public string MaKhCo { get; set; }

        public string Sgtcode { get; set; }

        public string HTTC { get; set; }

        public string GhiSo { get; set; }

        public string MsThue { get; set; }

        public DateTime? NgayCTGoc { get; set; } //?

        public string LoaiHDGoc { get; set; } //

        public string SoCTGoc { get; set; }

        public string KyHieuHD { get; set; }

        public decimal? VAT { get; set; }

        public decimal? DSKhongVAT { get; set; } //

        public string BoPhan { get; set; } // phongban ?

        public string NoQuay { get; set; } //

        public string CoQuay { get; set; } //

        public string SoXe { get; set; }

        public string TenKH { get; set; }

        public string DiaChi { get; set; }

        public string KyHieu { get; set; }

        public string MauSoHD { get; set; }

        public string MatHang { get; set; } //

        public bool? DieuChinh { get; set; } //

        public string LapPhieu { get; set; }

        public string DienGiaiP { get; set; } //

        public string HoaDonDT { get; set; } //

        public string LogFile { get; set; }


        public string NguoiTao { get; set; }

        public DateTime? NgayTao { get; set; }

        public string NguoiSua { get; set; }

        public DateTime? NgaySua { get; set; }

        public string LoaiPhieu { get; set; }
    }
}
