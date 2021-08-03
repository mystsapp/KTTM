using Data.Dtos;
using Data.Models_DanhMucKT;
using Data.Models_KTTM;
//using Data.Models_QLTour;
using Data.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace KTTM.Models
{
    public class HomeViewModel
    {
        public KVPCT KVPCT { get; set; }
        public IPagedList<KVPTCDto> KVPTCDtos { get; set; }
        public IEnumerable<KVCTPCT> KVCTPCTs { get; set; }
        public IEnumerable<ListViewModel> LoaiTiens { get; set; }
        public IEnumerable<PhongBan> Phongbans { get; set; }
        public IEnumerable<ListViewModel> LoaiPhieus { get; set; }

        public IEnumerable<TkCongNo> TkCongNos { get; set; }
        public string StrUrl { get; set; }
        public int Page { get; set; }

    }
}