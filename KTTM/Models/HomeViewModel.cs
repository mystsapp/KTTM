using Data.Dtos;
using Data.Models_DanhMucKT;
using Data.Models_KTTM;

//using Data.Models_QLTour;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using X.PagedList;

namespace KTTM.Models
{
    public class HomeViewModel
    {
        public List<IFormFile> files { get; set; }
        public KVPTC KVPTC { get; set; }
        public IPagedList<KVPTCDto> KVPTCDtos { get; set; }
        public IEnumerable<KVCTPTC> KVCTPTCs { get; set; }
        public IEnumerable<ListViewModel> LoaiTiens { get; set; }
        public IEnumerable<PhongBan> Phongbans { get; set; }
        public IEnumerable<ListViewModel> LoaiPhieus { get; set; }

        public IEnumerable<TkCongNo> TkCongNos { get; set; }
        public string StrUrl { get; set; }
        public int Page { get; set; }

        public List<InPhieuView_Groupby_TkNo_TkCo> InPhieuView_Groupby_TkNo_TkCos { get; set; }

        public string ChiNhanh { get; set; }
    }
}