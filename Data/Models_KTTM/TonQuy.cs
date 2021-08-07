using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models_KTTM
{
    public class TonQuy
    {
        public long Id { get; set; }
        [DisplayName("Ngày CT")]
        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Ngày không được để trống")]
        public DateTime? NgayCT { get; set; }

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

    }
}
