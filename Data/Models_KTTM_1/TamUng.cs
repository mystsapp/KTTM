using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_KTTM_1
{
    public partial class TamUng
    {
        public TamUng()
        {
            Tt621s = new HashSet<Tt621>();
        }

        public long Id { get; set; }
        public string MaKhNo { get; set; }
        public string SoCt { get; set; }
        public DateTime NgayCt { get; set; }
        public string PhieuChi { get; set; }
        public string DienGiai { get; set; }
        public string LoaiTien { get; set; }
        public decimal? SoTien { get; set; }
        public decimal? SoTienNt { get; set; }
        public decimal? ConLai { get; set; }
        public decimal? ConLaiNt { get; set; }
        public decimal? TyGia { get; set; }
        public string Tkno { get; set; }
        public string Tkco { get; set; }
        public string Phong { get; set; }
        public bool? Tttp { get; set; }
        public string PhieuTt { get; set; }
        public string LogFile { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }

        public virtual Kvctptc IdNavigation { get; set; }
        public virtual ICollection<Tt621> Tt621s { get; set; }
    }
}
