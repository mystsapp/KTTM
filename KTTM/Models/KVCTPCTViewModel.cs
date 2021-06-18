﻿using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTour;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Models
{
    public class KVCTPCTViewModel
    {
        public KVPCT KVPCT { get; set; }
        public KVCTPCT KVCTPCT { get; set; }
        public IEnumerable<KVCTPCT> KVCTPCTs { get; set; }
        public IEnumerable<Ngoaite> Ngoaites { get; set; }
        public IEnumerable<DmHttc> DmHttcs { get; set; }
        public IEnumerable<DmTk> DmTks { get; set; }
        public IEnumerable<ListViewModel> GetAll_TkCongNo_With_TenTK { get; set; }
        public IEnumerable<ListViewModel> GetAll_TaiKhoan_Except_TkConngNo { get; set; }
        //public IEnumerable<ListViewModel> DienGiai_By_TkNo_TkCo { get; set; }
        
    }
}
