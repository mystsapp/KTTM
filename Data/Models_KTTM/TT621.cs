using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models_KTTM
{
    public class TT621
    {
        
        public long Id { get; set; }

        public long TamUngId { get; set; }

        [ForeignKey("TamUngId")]
        public virtual TamUng TamUng { get; set; }

        [DisplayName("Mã KH nợ")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string MaKhNo { get; set; }

        [DisplayName("Số phiếu")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string SoCT { get; set; }

        [DisplayName("Ngày lập phiếu")]
        [Column(TypeName = "datetime")]
        public DateTime? NgayCT { get; set; }

        [DisplayName("Phiếu TC")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string PhieuTC { get; set; }
        
        [DisplayName("Phiếu TU")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string PhieuTU { get; set; }

        [DisplayName("Diển giải")]
        [MaxLength(150, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "nvarchar(150)")]
        public string DienGiai { get; set; }

        [DisplayName("Loại tiền")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string LoaiTien { get; set; }

        [DisplayName("Số tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SoTien { get; set; }

        [DisplayName("Số tiền NT")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SoTienNT { get; set; }

        [DisplayName("Tỷ giá")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TyGia { get; set; }

        [DisplayName("TK Nợ")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        [Required(ErrorMessage = "TK không được để trống")]
        public string TKNo { get; set; }

        [DisplayName("TK Có")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        [Required(ErrorMessage = "TK không được để trống")]
        public string TKCo { get; set; }

        [DisplayName("Mã KH có")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string MaKhCo { get; set; }

        [MaxLength(17, ErrorMessage = "Chiều dài tối đa 17 ký tự"), MinLength(17, ErrorMessage = "Chiều dài tối thiểu 17 ký tự"), Column(TypeName = "varchar(17)")]
        public string Sgtcode { get; set; }

        [DisplayName("HTTC")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string HTTC { get; set; }
        
        [DisplayName("Ghi sổ")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(50)")]
        public string GhiSo { get; set; }

        [DisplayName("MS Thuế")]
        [MaxLength(16, ErrorMessage = "Chiều dài tối đa 16 ký tự"), Column(TypeName = "varchar(16)")]
        public string MsThue { get; set; }

        [DisplayName("Ngày CT Gốc")]
        [Column(TypeName = "datetime")]
        public DateTime? NgayCTGoc { get; set; } //?

        [DisplayName("Loại CT Gốc")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string LoaiHDGoc { get; set; } //

        [DisplayName("Số CT Gốc")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string SoCTGoc { get; set; }

        [DisplayName("Ký hiệu HĐ")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string KyHieuHD { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal VAT { get; set; }

        [DisplayName("Doanh số chưa thuế")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DSKhongVAT { get; set; } //

        [DisplayName("Bộ phận")]
        [MaxLength(2, ErrorMessage = "Chiều dài tối đa 2 ký tự"), Column(TypeName = "varchar(2)")]
        public string BoPhan { get; set; } // phongban ?

        [DisplayName("Nợ quầy")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string NoQuay { get; set; } //

        [DisplayName("Có quầy")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string CoQuay { get; set; } //

        [DisplayName("Số xe")]
        [MaxLength(8, ErrorMessage = "Chiều dài tối đa 8 ký tự"), Column(TypeName = "varchar(8)")]
        public string SoXe { get; set; }

        [DisplayName("Tên KH")]
        [MaxLength(100, ErrorMessage = "Chiều dài tối đa 100 ký tự"), Column(TypeName = "nvarchar(100)")]
        public string TenKH { get; set; }

        [DisplayName("Địa chỉ")]
        [MaxLength(200, ErrorMessage = "Chiều dài tối đa 200 ký tự"), Column(TypeName = "nvarchar(200)")]
        public string DiaChi { get; set; }

        [DisplayName("Ký hiệu")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string KyHieu { get; set; }

        [DisplayName("Mẫu số HĐ")]
        [MaxLength(11, ErrorMessage = "Chiều dài tối đa 11 ký tự"), Column(TypeName = "varchar(11)")]
        public string MauSoHD { get; set; }

        [DisplayName("MatHang")]
        [MaxLength(60, ErrorMessage = "Chiều dài tối đa 60 ký tự"), Column(TypeName = "nvarchar(60)")]
        public string MatHang { get; set; } //

        [DisplayName("Điều chỉnh")]
        public bool DieuChinh { get; set; } //

        [DisplayName("Người lập phiếu")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "nvarchar(50)")]
        public string LapPhieu { get; set; }

        [DisplayName("Diễn giải phụ")]
        [MaxLength(150, ErrorMessage = "Chiều dài tối đa 150 ký tự"), Column(TypeName = "nvarchar(150)")]
        public string DienGiaiP { get; set; } //

        [DisplayName("Hoá đơn DT")]
        [MaxLength(120, ErrorMessage = "Chiều dài tối đa 120 ký tự"), Column(TypeName = "nvarchar(120)")]
        public string HoaDonDT { get; set; } //

        [Column(TypeName = "nvarchar(MAX)")]
        public string LogFile { get; set; }



        [DisplayName("Người tạo")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "varchar(50)")]
        public string NguoiTao { get; set; }

        //[DisplayName("Ngày khoá")]
        [Column(TypeName = "datetime")]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        //[DisplayName("Ngày khoá")]
        [Column(TypeName = "datetime")]
        public DateTime? NgaySua { get; set; }

    }
}
