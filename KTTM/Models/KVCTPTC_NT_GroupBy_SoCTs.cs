using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class KVCTPTC_NT_GroupBy_SoCTs
    {
        public string SoCT { get; set; }
        public DateTime NgayCT { get; set; }
        public string HoTen { get; set; }
        public IEnumerable<KVCTPTC> KVCTPTCs { get; set; }
        public decimal TongCong { get; set; }
        public decimal TongCong_NT { get; set; }
        public decimal CongPhatSinh_Thu { get; set; }
        public decimal CongPhatSinh_Chi { get; set; }
        public decimal CongPhatSinh_Thu_NT { get; set; }
        public decimal CongPhatSinh_Chi_NT { get; set; }
        public decimal TonCuoi { get; set; }
    }
}