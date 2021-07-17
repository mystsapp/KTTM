using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class TT621ViewModel
    {
        public TT621 TT621 { get; set; }
        public KVCTPCT KVCTPCT { get; set; }
        public IEnumerable<TamUng> TamUngs { get; set; }
        public IEnumerable<TT621> TT621s { get; set; }
        public string CommentText { get; set; }
        public string StrUrl { get; set; }
        public string Page { get; set; }

        public IEnumerable<ViewDmHttc> DmHttcs { get; set; }
        public IEnumerable<Ngoaite> Ngoaites { get; set; }
        public IEnumerable<DmTk> DmTks_TkNo { get; set; }
        public IEnumerable<DmTk> DmTks_TkCo { get; set; }
        public IEnumerable<ViewQuay> Quays { get; set; }
        public IEnumerable<ViewMatHang> MatHangs { get; set; }
        public IEnumerable<ViewPhongBan> PhongBans { get; set; }
    }
}
