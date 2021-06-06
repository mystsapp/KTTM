using Data.Dtos;
using Data.Models_KTTM;
using Data.Models_QLTour;
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
        public IEnumerable<Ngoaite> Ngoaites { get; set; }
        public IEnumerable<Phongban> Phongbans { get; set; }
        public string StrUrl { get; set; }
    }
}
