using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_Cashier
{
    public partial class VCuocHangkhong
    {
        public int Id { get; set; }
        public string Stt { get; set; }
        public string Hdvat { get; set; }
        public DateTime Ngayct { get; set; }
        public string Coquan { get; set; }
        public string Tencoquan { get; set; }
        public string Tenkhach { get; set; }
        public string Msthue { get; set; }
        public string Httt { get; set; }
        public string Ghichu { get; set; }
        public string Chinhanh { get; set; }
        public int? Sk { get; set; }
        public decimal? Sotien { get; set; }
    }
}
