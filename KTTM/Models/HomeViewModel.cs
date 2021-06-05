using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class HomeViewModel
    {
        public KVPCT KVPCT { get; set; }
        public IEnumerable<KVPCT> KVPCTs { get; set; }
        public IEnumerable<KVCTPCT> KVCTPCTs { get; set; }
        public string StrUrl { get; set; }
    }
}
