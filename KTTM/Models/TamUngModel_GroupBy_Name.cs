using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class TamUngModel_GroupBy_Name
    {
        public IEnumerable<TamUngModel> TamUngModels { get; set; }
        public string Name { get; set; }
        public string Name_Phong { get; set; }
        public decimal TongCong { get; set; }
        public decimal TongCongNT { get; set; }

        // for MaKh null
        public bool Status { get; set; } = true;

        public string MaKh { get; set; }
    }
}