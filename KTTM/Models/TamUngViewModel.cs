using Data.Models_KTTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace KTTM.Models
{
    public class TamUngViewModel
    {
        public TamUng TamUng { get; set; }
        public IPagedList<TamUng> TamUngs { get; set; }
        public string StrUrl { get; set; }
    }
}
