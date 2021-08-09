using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class KVCTPCT_Model_GroupBy_SoCT
    {
        public string SoCT { get; set; }
        public IEnumerable<KVCTPCT> KVCTPCTs { get; set; }
        public decimal TongCong { get; set; }
        public decimal CongPhatSinh_Thu { get; set; }
        public decimal CongPhatSinh_Chi { get; set; }
        public decimal TonCuoi { get; set; }
        
    }
}
