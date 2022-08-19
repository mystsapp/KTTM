using System;
using System.Collections.Generic;

#nullable disable

namespace KTTM.Models_Cashier
{
    public partial class VChitietBienNhanIb
    {
        public long Id { get; set; }
        public long BienNhanId { get; set; }
        public string Descript { get; set; }
        public decimal Amount { get; set; }
        public string Ghichu { get; set; }
        public string Httt { get; set; }
    }
}
