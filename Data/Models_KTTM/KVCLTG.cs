using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models_KTTM
{
    public class KVCLTG
    {
        public long Id { get; set; }
        [DisplayName("Số phiếu")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string SoCT { get; set; }

        [DisplayName("Ngày lập phiếu")]
        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Ngày không được để trống")]
        public DateTime? NgayCT { get; set; }

        [DisplayName("Diển giải")]
        [MaxLength(150, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "nvarchar(150)")]
        public string DienGiai { get; set; }

        [DisplayName("TK Nợ")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string TKNo { get; set; }

        [DisplayName("TK Có")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string TKCo { get; set; }

        [DisplayName("Số tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SoTien { get; set; }

        [DisplayName("Mã KH nợ")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string MaKhNo { get; set; } //

        [DisplayName("Mã KH có")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự"), Column(TypeName = "varchar(12)")]
        public string MaKhCo { get; set; } //

        [DisplayName("Nợ quầy")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string NoQuay { get; set; } //

        [DisplayName("Có quầy")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string CoQuay { get; set; } //

    }
}
