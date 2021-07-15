using Data.Models_KTTM;
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
        public string CommentText { get; set; }
        public string StrUrl { get; set; }
        public string Page { get; set; }
    }
}
