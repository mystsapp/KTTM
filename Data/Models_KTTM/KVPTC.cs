using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models_KTTM
{
    public class KVPTC
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DisplayName("Số phiếu")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string SoCT { get; set; }
        
        [DisplayName("Chi nhánh")]
        [MaxLength(5, ErrorMessage = "Chiều dài tối đa 5 ký tự"), Column(TypeName = "varchar(5)")]
        public string MaCn { get; set; }

        [DisplayName("Ngày lập phiếu")]
        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Ngày không được để trống")]
        public DateTime? NgayCT { get; set; }

        [DisplayName("Loại phiếu")]
        [MaxLength(1, ErrorMessage = "Chiều dài tối đa 1 ký tự"), Column(TypeName = "varchar(1)")]
        public string MFieu { get; set; }
        
        [DisplayName("Loại tiền")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string NgoaiTe { get; set; }

        [DisplayName("Họ và tên")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "nvarchar(50)")]
        public string HoTen { get; set; }

        [DisplayName("Đơn vị")]
        [MaxLength(150, ErrorMessage = "Chiều dài tối đa 150 ký tự"), Column(TypeName = "nvarchar(150)")]
        public string DonVi { get; set; }

        [DisplayName("Phòng")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "nvarchar(50)")]
        public string Phong { get; set; }

        [DisplayName("Người lập phiếu")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "nvarchar(50)")]
        public string LapPhieu { get; set; }

        //[DisplayName("Ngày tạo")]
        [Column(TypeName = "datetime")]
        public DateTime? Create { get; set; }

        //[DisplayName("Số biên nhận")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "varchar(50)")]
        public string MayTinh { get; set; }

        //[DisplayName("Ngày khoá")]
        [Column(TypeName = "datetime")]
        public DateTime? Lock { get; set; }

        //[DisplayName("Người khoá")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "nvarchar(50)")]
        public string Locker { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string LogFile { get; set; }

        [DisplayName("Người sửa")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "varchar(50)")]
        public string NguoiSua { get; set; }

        //[DisplayName("Ngày khoá")]
        [Column(TypeName = "datetime")]
        public DateTime? NgaySua { get; set; }

    }
}
