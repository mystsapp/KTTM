using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class TonQuyViewModel
    {
        public TonQuy TonQuy { get; set; }
        public IEnumerable<TonQuy> TonQuies { get; set; }
        public string StrUrl { get; set; }
    }
}
