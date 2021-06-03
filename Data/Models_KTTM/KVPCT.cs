using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models_KTTM
{
    public class KVPCT
    {
        [Key]
        [DisplayName("Số biên nhận")]
        [MaxLength(10, ErrorMessage = "Chiều dài tối đa 10 ký tự"), Column(TypeName = "varchar(10)")]
        public string SoCT { get; set; }

        [DisplayName("Ngày BN")]
        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Ngày không được để trống")]
        public DateTime NgayCT { get; set; }

        [DisplayName("Số biên nhận")]
        [MaxLength(1, ErrorMessage = "Chiều dài tối đa 1 ký tự"), Column(TypeName = "varchar(1)")]
        public string MFieu { get; set; }
        
        [DisplayName("Số biên nhận")]
        [MaxLength(3, ErrorMessage = "Chiều dài tối đa 3 ký tự"), Column(TypeName = "varchar(3)")]
        public string NgoaiTe { get; set; }

        [DisplayName("Số biên nhận")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "nvarchar(50)")]
        public string HoTen { get; set; }

        [DisplayName("Số biên nhận")]
        [MaxLength(150, ErrorMessage = "Chiều dài tối đa 150 ký tự"), Column(TypeName = "nvarchar(150)")]
        public string DonVi { get; set; }

        [DisplayName("Số biên nhận")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "nvarchar(50)")]
        public string Phong { get; set; }

        [DisplayName("Số biên nhận")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "nvarchar(50)")]
        public string LapPhieu { get; set; }

        [DisplayName("Ngày BN")]
        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Ngày không được để trống")]
        public DateTime Create { get; set; }

        [DisplayName("Số biên nhận")]
        [MaxLength(15, ErrorMessage = "Chiều dài tối đa 15 ký tự"), Column(TypeName = "varchar(15)")]
        public string MayTinh { get; set; }

        [DisplayName("Ngày BN")]
        [Column(TypeName = "datetime")]
        public DateTime Lock { get; set; }

        [DisplayName("Số biên nhận")]
        [MaxLength(50, ErrorMessage = "Chiều dài tối đa 50 ký tự"), Column(TypeName = "nvarchar(50)")]
        public string Locker { get; set; }

    }
}
