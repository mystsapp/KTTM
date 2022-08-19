using System;
using System.Collections.Generic;

#nullable disable

namespace KTTM.Models_Cashier
{
    public partial class VBiennhanInBound
    {
        public long Id { get; set; }
        public string SoBn { get; set; }
        public DateTime Ngaybiennhan { get; set; }
        public string Ngaybn { get; set; }
        public string Sgtcode { get; set; }
        public string Macoquan { get; set; }
        public string Tencoquan { get; set; }
        public string TenKhach { get; set; }
        public string LoaiTien { get; set; }
        public decimal TyGia { get; set; }
        public decimal Sotiennt { get; set; }
        public decimal SoTien { get; set; }
        public string Ghichu { get; set; }
        public int SoKhachDk { get; set; }
        public string Diengiai { get; set; }
        public string Chinhanh { get; set; }
        public string PhongDh { get; set; }
        public string Httt { get; set; }
    }
}
