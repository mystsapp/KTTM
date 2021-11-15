using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class InPhieuView_Groupby_TkNo_TkCo
    {
        public string TkNo { get; set; }
        public string TkCo { get; set; }
        public string LoaiTien { get; set; }
        public decimal SoTien { get; set; }
        public decimal SoTienNT { get; set; }
        public string SoTienNT_BangChu { get; set; }
        public IEnumerable<KVCTPTC> KVCTPTCs { get; set; }
    }
}