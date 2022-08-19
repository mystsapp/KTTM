using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_Cashier
{
    public partial class VBiennhanXuatkhau
    {
        public decimal Id { get; set; }
        public string Sobiennhan { get; set; }
        public DateTime? Ngaydatcoc { get; set; }
        public string Sgtcode { get; set; }
        public decimal Sotien { get; set; }
        public string Tenkhach { get; set; }
        public string Loaitien { get; set; }
        public string Diengiai { get; set; }
        public string Nguoitao { get; set; }
        public decimal Tygia { get; set; }
        public string Httt { get; set; }
        public string Tencoquan { get; set; }
    }
}
