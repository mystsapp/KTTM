using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_Cashier
{
    public partial class VBiennhanVanchuyen
    {
        public int Id { get; set; }
        public string Sobiennhan { get; set; }
        public DateTime? Ngay { get; set; }
        public string Ngaybn { get; set; }
        public string Httt { get; set; }
        public string Codedoan { get; set; }
        public decimal? Sotien { get; set; }
        public string Noidung { get; set; }
        public string Tencoquan { get; set; }
        public string Tenkhach { get; set; }
        public string Chinhanh { get; set; }
        public string Nguoitao { get; set; }
    }
}
