using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models_KTTM
{
    public class KVCTPTC
    {
        public long Id { get; set; }

        [DisplayName("KVPTC")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string KVPTCId { get; set; }

        [ForeignKey("KVPTCId")]
        public virtual KVPTC KVPTC { get; set; }

        [DisplayName("HTTC")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string HTTC { get; set; } 
        
        [DisplayName("Diễn giải")]
        [MaxLength(150, ErrorMessage = "Chiều dài tối đa 150 ký tự"), Column(TypeName = "nvarchar(150)")]
        public string DienGiai { get; set; }

        [DisplayName("TK Nợ")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        [Required(ErrorMessage = "TK không được để trống")]
        public string TKNo { get; set; } 

        [DisplayName("TK Có")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        [Required(ErrorMessage = "TK không được để trống")]
        public string TKCo { get; set; } 
        
        [MaxLength(17, ErrorMessage = "Chiều dài tối đa 17 ký tự"), MinLength(17, ErrorMessage = "Chiều dài tối thiểu 17 ký tự"), Column(TypeName = "varchar(17)")]
        public string Sgtcode { get; set; } 
        
        [DisplayName("Mã KH nợ") ]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string MaKhNo { get; set; } //
        
        [DisplayName("Mã KH có")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string MaKhCo { get; set; } //

        [DisplayName("Số tiền NT")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SoTienNT { get; set; }

        [DisplayName("Loại tiền")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string LoaiTien { get; set; }

        [DisplayName("Tỷ giá")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TyGia { get; set; }

        [DisplayName("Số tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SoTien { get; set; }

        [MaxLength(5, ErrorMessage = "Chiều dài tối đa 5 ký tự"), Column(TypeName = "varchar(5)")]
        public string MaKh { get; set; } //
        
        [DisplayName("Khoãng mục")]
        [MaxLength(2, ErrorMessage = "Chiều dài tối đa 2 ký tự"), Column(TypeName = "varchar(2)")]
        public string KhoangMuc { get; set; } //
        
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "nvarchar(2)")]
        public string HTTT { get; set; }

        [DisplayName("Số thẻ tín dụng")]
        [MaxLength(20, ErrorMessage = "Chiều dài tối đa 20 ký tự"), Column(TypeName = "varchar(20)")]
        public string CardNumber { get; set; }

        [DisplayName("SalesSlip")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "nvarchar(10)")]
        public string SalesSlip { get; set; } //

        [DisplayName("Số xe")]
        [MaxLength(8, ErrorMessage = "Chiều dài tối đa 8 ký tự"), Column(TypeName = "varchar(8)")]
        public string SoXe { get; set; }

        [DisplayName("MS Thuế")]
        [MaxLength(16, ErrorMessage = "Chiều dài tối đa 16 ký tự"), Column(TypeName = "varchar(16)")]
        public string MsThue { get; set; }
        
        [DisplayName("Loại CT Gốc")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string LoaiHDGoc { get; set; } //
        
        [DisplayName("Số CT Gốc")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string SoCTGoc { get; set; }

        [DisplayName("Ngày CT Gốc")]
        [Column(TypeName = "datetime")]
        public DateTime? NgayCTGoc { get; set; } //?

        [Column(TypeName = "decimal(18,2)")]
        public decimal? VAT { get; set; }
        
        [DisplayName("Doanh số chưa thuế")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DSKhongVAT { get; set; } //

        [DisplayName("Bộ phận")]
        [MaxLength(2, ErrorMessage = "Chiều dài tối đa 2 ký tự"), Column(TypeName = "varchar(2)")]
        public string BoPhan { get; set; } // phongban ?

        [MaxLength(13, ErrorMessage = "Chiều dài tối đa 13 ký tự"), Column(TypeName = "varchar(13)")]
        public string STT { get; set; } // HD ?

        [DisplayName("Nợ quầy")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string NoQuay { get; set; } //
        
        [DisplayName("Có quầy")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string CoQuay { get; set; } //

        [DisplayName("Tên KH")]
        [MaxLength(100, ErrorMessage = "Chiều dài tối đa 100 ký tự"), Column(TypeName = "nvarchar(100)")]
        public string TenKH { get; set; }
        
        [DisplayName("Địa chỉ")]
        [MaxLength(200, ErrorMessage = "Chiều dài tối đa 200 ký tự"), Column(TypeName = "nvarchar(200)")]
        public string DiaChi { get; set; }
        
        [DisplayName("MatHang")]
        [MaxLength(60, ErrorMessage = "Chiều dài tối đa 60 ký tự"), Column(TypeName = "nvarchar(60)")]
        public string MatHang { get; set; } //
        
        [DisplayName("Ký hiệu")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string KyHieu { get; set; }
        
        [DisplayName("Mẫu số HĐ")]
        [MaxLength(11, ErrorMessage = "Chiều dài tối đa 11 ký tự"), Column(TypeName = "varchar(11)")]
        public string MauSoHD { get; set; }
        
        [DisplayName("Điều chỉnh")]
        public bool? DieuChinh { get; set; } //

        [DisplayName("KC141")]
        [Column(TypeName = "datetime")]
        public DateTime? KC141 { get; set; } //?


        [DisplayName("Tạm ứng")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string TamUng { get; set; } //

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



        [DisplayName("Link HDDT")]
        [MaxLength(150, ErrorMessage = "Chiều dài tối đa 150 ký tự"), Column(TypeName = "varchar(150)")]
        public string LinkHDDT { get; set; }


        [DisplayName("Mã tra cứu")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "varchar(50)")]
        public string MaTraCuu { get; set; }
        
        [DisplayName("Tk truy cập")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "varchar(50)")]
        public string TkTruyCap { get; set; }


        [DisplayName("Password")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "varchar(50)")]
        public string Password { get; set; }

    }
}
