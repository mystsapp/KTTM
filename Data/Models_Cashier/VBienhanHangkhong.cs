using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_Cashier
{
    public partial class VBienhanHangkhong
    {
        public int Id { get; set; }
        public string Biennhan { get; set; }
        public DateTime? Ngayct { get; set; }
        public string Coquan { get; set; }
        public string Tencoquan { get; set; }
        public string Tenkhach { get; set; }
        public string Diengiai { get; set; }
        public string Httt { get; set; }
        public decimal? Sotien { get; set; }
        public string Loaitien { get; set; }
        public decimal? Tygia { get; set; }
        public string Ghichu { get; set; }
        public string Chinhanh { get; set; }
    }
}
