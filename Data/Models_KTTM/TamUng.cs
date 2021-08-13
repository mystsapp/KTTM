using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models_KTTM
{
    public class TamUng
    {
        [Key, ForeignKey("KVCTPTC")]
        public long Id { get; set; }
        public virtual KVCTPTC KVCTPTC { get; set; }

        [DisplayName("Mã KH nợ")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string MaKhNo { get; set; } 

        [DisplayName("Số tạm ứng")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string SoCT { get; set; }

        [DisplayName("Ngày tạm ứng")]
        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Ngày không được để trống")]
        public DateTime? NgayCT { get; set; }

        [DisplayName("Phiếu chi")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string PhieuChi { get; set; }

        [DisplayName("Diển giải")]
        [MaxLength(150, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "nvarchar(150)")]
        public string DienGiai { get; set; }

        [DisplayName("Loại tiền")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string LoaiTien { get; set; }

        [DisplayName("Số tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SoTien { get; set; }

        [DisplayName("Số tiền NT")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SoTienNT { get; set; }

        [DisplayName("Nợ VNĐ")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ConLai { get; set; }

        [DisplayName("Còn nợ NT")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ConLaiNT { get; set; }

        [DisplayName("Tỷ giá")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TyGia { get; set; }

        [DisplayName("TK Nợ")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string TKNo { get; set; }

        [DisplayName("TK Có")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string TKCo { get; set; }

        [DisplayName("Phòng")]
        [MaxLength(20, ErrorMessage = "Chiều dài tối đa 20 ký tự"), Column(TypeName = "nvarchar(20)")]
        public string Phong { get; set; }

        public bool? TTTP { get; set; }

        //[DisplayName("Phòng")]
        [MaxLength(80, ErrorMessage = "Chiều dài tối đa 80 ký tự"), Column(TypeName = "nvarchar(80)")]
        public string PhieuTT { get; set; }


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
