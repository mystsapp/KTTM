using Data.Services;
using KTTM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Controllers
{
    public class KVCTPCTsController : BaseController
    {
        private readonly IKVCTPCTService _kVCTPCTService;
        private readonly IKVPCTService _kVPCTService;

        [BindProperty]
        public KVCTPCTViewModel KVCTPCTVM { get; set; }
        public KVCTPCTsController(IKVCTPCTService kVCTPCTService, IKVPCTService kVPCTService)
        {
            _kVCTPCTService = kVCTPCTService;
            _kVPCTService = kVPCTService;

            KVCTPCTVM = new KVCTPCTViewModel()
            {
                KVPCT = new Data.Models_KTTM.KVPCT(),
                KVCTPCT = new Data.Models_KTTM.KVCTPCT()
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> KVCTPCTPartial(string soCT)
        {
            // KVCTPCT
            KVCTPCTVM.KVCTPCTs = await _kVCTPCTService.List_KVCTPCT_By_SoCT(soCT);
            KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(soCT);

            return PartialView(KVCTPCTVM);
        }
        public async Task<IActionResult> KVCTPCT_Create_Partial(string kvpctid)
        {
            // KVPCT
            KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(kvpctid);
            KVCTPCTVM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes();

            return PartialView(KVCTPCTVM);
        }
    }
}
