using System.Collections.Generic;

namespace KTTM.Models
{
    public class TamUngModel_GroupByLoaiTien
    {
        public IEnumerable<TamUngModel> TamUngModels { get; set; }
        public string LoaiTien { get; set; }
    }
}