using Data.Models_KTTM;
using Data.Models_QLTour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class KVCTPCTViewModel
    {
        public KVPCT KVPCT { get; set; }
        public KVCTPCT KVCTPCT { get; set; }
        public IEnumerable<KVCTPCT> KVCTPCTs { get; set; }
        public IEnumerable<Ngoaite> Ngoaites { get; set; }
    }
}
