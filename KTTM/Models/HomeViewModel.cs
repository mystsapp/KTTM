using Data.Dtos;
using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace KTTM.Models
{
    public class HomeViewModel
    {
        public KVPCT KVPCT { get; set; }
        public IPagedList<KVPTCDto> KVPTCDtos { get; set; }
        public IEnumerable<KVCTPCT> KVCTPCTs { get; set; }
        public string StrUrl { get; set; }
    }
}
