using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class LayDataCashierModel
    {
        public string BaoCaoSo { get; set; }

        public bool TienMat { get; set; }
        public bool TTThe { get; set; }
        public bool NganHang { get; set; }

        //public bool SEC { get; set; }
        //public bool NganPhieu { get; set; }
        public string Tk { get; set; }
    }
}