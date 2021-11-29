using Data.Models_KTTM;
using System.Collections;
using System.Collections.Generic;

namespace KTTM.Models
{
    public class TT621GroupBy_PhieuTC
    {
        public string PhieuTC { get; set; }
        public IEnumerable<TT621> TT621s { get; set; }
    }
}