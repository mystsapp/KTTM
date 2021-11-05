using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class TonQuy_Model_GroupBy_NgayCT
    {
        public DateTime? NgayCT { get; set; }
        public IEnumerable<TonQuy> TonQuies { get; set; }
        
    }
}
