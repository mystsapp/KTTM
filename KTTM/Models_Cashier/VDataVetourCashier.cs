using System;
using System.Collections.Generic;

#nullable disable

namespace KTTM.Models_Cashier
{
    public partial class VDataVetourCashier
    {
        public decimal Id { get; set; }
        public string Tour { get; set; }
        public string Serial { get; set; }
        public string Macoquan { get; set; }
        public string Tencoquan { get; set; }
        public string Sgtcode { get; set; }
        public string Tenkhach { get; set; }
        public decimal? Sotiennt { get; set; }
        public decimal? Datcoc { get; set; }
        public string Diengiai { get; set; }
        public string Ghichu { get; set; }
        public string Kenhtt { get; set; }
        public DateTime? Ngayxuatve { get; set; }
        public string Xuatve { get; set; }
        public int? Sk { get; set; }
        public string Dailyxuatve { get; set; }
        public int? Ngaythutien { get; set; }
        public DateTime? Ngayhuyve { get; set; }
        public string Dailyhuyve { get; set; }
        public string Nguoihuyve { get; set; }
    }
}
