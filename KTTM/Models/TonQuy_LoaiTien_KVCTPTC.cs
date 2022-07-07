using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class TonQuy_LoaiTien_KVCTPTC
    {
        public NgoaiTe NgoaiTe { get; set; }
        public TonQuy TonQuy { get; set; }
        public IEnumerable<KVCTPTC> KVCTPTCs { get; set; }
        public IEnumerable<KVCTPTC_NT_GroupBy_SoCTs> KVCTPTC_NT_GroupBy_SoCTs { get; set; }

        public decimal CongPhatSinh_Thu { get; set; }
        public decimal CongPhatSinh_Chi { get; set; }
        public decimal CongPhatSinh_Thu_NT { get; set; }
        public decimal CongPhatSinh_Chi_NC { get; set; }
        public decimal TonCuoi { get; set; }
    }
}