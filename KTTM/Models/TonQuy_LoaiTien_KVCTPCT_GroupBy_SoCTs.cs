using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class TonQuy_LoaiTien_KVCTPCT_GroupBy_SoCTs
    {
        public NgoaiTe NgoaiTe { get; set; }
        public TonQuy TonQuy { get; set; }
        public IEnumerable<KVCTPTC_NT_GroupBy_SoCTs> KVCTPTC_NT_GroupBy_SoCTs { get; set; }
    }
}