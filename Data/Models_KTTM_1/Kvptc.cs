using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_KTTM_1
{
    public partial class Kvptc
    {
        public Kvptc()
        {
            Kvctptcs = new HashSet<Kvctptc>();
        }

        public string SoCt { get; set; }
        public DateTime NgayCt { get; set; }
        public string Mfieu { get; set; }
        public string NgoaiTe { get; set; }
        public string HoTen { get; set; }
        public string DonVi { get; set; }
        public string Phong { get; set; }
        public string LapPhieu { get; set; }
        public DateTime Create { get; set; }
        public string MayTinh { get; set; }
        public DateTime? Lock { get; set; }
        public string Locker { get; set; }
        public string LogFile { get; set; }
        public string NguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }

        public virtual ICollection<Kvctptc> Kvctptcs { get; set; }
    }
}
