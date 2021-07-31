using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class BaoCaoViewModel
    {
        public IEnumerable<TT621> TT621s { get; set; }
        public string StrUrl { get; set; }
    }
}
