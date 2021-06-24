using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_Cashier
{
    public partial class VHoadonVanchuyen
    {
        public string Idhoadon { get; set; }
        public string Chinhanh { get; set; }
        public string Stt { get; set; }
        public string Hdvat { get; set; }
        public string Macoquan { get; set; }
        public string Tencoquan { get; set; }
        public string Tenkhach { get; set; }
        public string Msthue { get; set; }
        public string Ghichu { get; set; }
        public string Diengiai { get; set; }
        public string Ngayin { get; set; }
        public DateTime? Ngayct { get; set; }
        public decimal? Sotien { get; set; }
    }
}
