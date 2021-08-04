using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class TamUngModel
    {
        public long Id { get; set; }
        public DateTime? NgayCT { get; set; }
        public string SoCT { get; set; }
        public string DienGiai { get; set; }
        public decimal SoTienNT { get; set; }
        public string LT { get; set; }
        public decimal TyGia { get; set; }
        public decimal VND { get; set; }
        public string Name { get; set; }

        public string Name_Phong { get; set; }
    }
}
