using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_DanhMucKT
{
    public partial class TaiKhoanNganHang
    {
        public decimal Id { get; set; }
        public string Code { get; set; }
        public string TknganHang { get; set; }
        public string TenNganHang { get; set; }
        public string Tpnh { get; set; }
    }
}
