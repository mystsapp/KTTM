using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class InPhieuView_Groupby_TkNo
    {
        public string TkNo { get; set; }
        public decimal SoTien { get; set; }
        public decimal SoTienNT { get; set; }
        public IEnumerable<KVCTPTC> KVCTPTCs { get; set; }
    }
}