using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_KTTM_1
{
    public partial class TonQuy
    {
        public long Id { get; set; }
        public DateTime NgayCt { get; set; }
        public string LoaiTien { get; set; }
        public decimal SoTien { get; set; }
        public decimal SoTienNt { get; set; }
        public decimal TyGia { get; set; }
        public string LogFile { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
