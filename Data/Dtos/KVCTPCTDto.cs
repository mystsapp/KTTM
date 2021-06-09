using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Dtos
{
    public class KVCTPCTDto
    {
        public long Id { get; set; }

        [DisplayName("KVPCT")]
        public string KVPCTId { get; set; }


        [DisplayName("HTTC")]
        public string HTTC { get; set; } //

        [DisplayName("Diễn giải")]
        public string DienGiai { get; set; }

        [DisplayName("TK Nợ")]
        public string TKNo { get; set; }

        [DisplayName("TK Có")]
        public string TKCo { get; set; }

        public string Sgtcode { get; set; }

        [DisplayName("MaKhNo")]
        public string MaKhNo { get; set; } //

        [DisplayName("MaKhCo")]
        public string MaKhCo { get; set; } //

        [DisplayName("Số tiền NT")]
        public decimal SoTienNT { get; set; }

        [DisplayName("Loại tiền")]
        public string LoaiTien { get; set; }

        [DisplayName("Tỷ giá")]
        public decimal TyGia { get; set; }

        [DisplayName("Số tiền")]
        public decimal SoTien { get; set; }

        public string MaKh { get; set; } //

        [DisplayName("Khoãng mục")]
        public string KhoangMuc { get; set; } //

        public string HTTT { get; set; }

        public string CardNumber { get; set; }

        [DisplayName("SalesSlip")]
        public string SalesSlip { get; set; } //

        [DisplayName("Số xe")]
        public string SoXe { get; set; }

        [DisplayName("MS Thuế")]
        public string MsThue { get; set; }

        [DisplayName("Loại HD Gốc")]
        public string LoaiHDGoc { get; set; } //

        [DisplayName("Số CT Gốc")]
        public string SoCTGoc { get; set; }

        [DisplayName("Ngày CT Gốc")]
        public DateTime NgayCTGoc { get; set; } //

        public decimal VAT { get; set; }

        [DisplayName("DSKhongVAT")]
        public decimal DSKhongVAT { get; set; } //

        [DisplayName("Bộ phận")]
        public string BoPhan { get; set; } // phongban ?

        public string STT { get; set; } // HD ?

        //[DisplayName("Loại tiền")]
        public string NoQuay { get; set; } //

        //[DisplayName("Loại tiền")]
        public string CoQuay { get; set; } //

        [DisplayName("Tên KH")]
        public string TenKH { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("MatHang")]
        public string MatHang { get; set; } //

        [DisplayName("Ký hiệu")]
        public string KyHieu { get; set; }

        [DisplayName("Mẫu số HD")]
        public string MauSoHD { get; set; }

        [DisplayName("Điều chỉnh")]
        public bool DieuChinh { get; set; } //

        [DisplayName("KC141")]
        public DateTime KC141 { get; set; } //


        [DisplayName("Tạm ứng")]
        public string TamUng { get; set; } //

        [DisplayName("Diễn giải phụ")]
        public string DienGiaiP { get; set; } //

        [DisplayName("Hoá đơn DT")]
        public string HoaDonDT { get; set; } //

    }
}
