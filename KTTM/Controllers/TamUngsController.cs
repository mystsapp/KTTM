using Data.Models_KTTM;
using Data.Services;
using KTTM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Controllers
{
    public class TamUngsController : BaseController
    {
        private readonly ITamUngService _tamUngService;
        private readonly IKVCTPCTService _kVCTPCTService;
        private readonly IKVPCTService _kVPCTService;

        [BindProperty]
        public TamUngViewModel TamUngVM { get; set; }
        public TamUngsController(ITamUngService tamUngService, IKVCTPCTService kVCTPCTService, IKVPCTService kVPCTService)
        {
            TamUngVM = new TamUngViewModel();

            _tamUngService = tamUngService;
            _kVCTPCTService = kVCTPCTService;
            _kVPCTService = kVPCTService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(long id, string loaiTienUng) // loaitien == "VN" ==> "UV"(0378UV2014) or "NT" ==> "UN"(0133UN2014)
        {
            if (id == 0)
                return NotFound();

            var kVCTPCT = await _kVCTPCTService.GetById(id);
            var kVPCT = await _kVPCTService.GetBySoCT(kVCTPCT.KVPCTId);
            if (kVCTPCT == null)
                return NotFound();

            var tamUng = new TamUng();

            tamUng.MaKhNo = kVCTPCT.MaKhNo;
            tamUng.SoCT = _tamUngService.GetSoCT(loaiTienUng);
            tamUng.NgayCT = DateTime.Now;
            tamUng.PhieuChi = kVCTPCT.KVPCTId; // soCT ben KVPCT
            tamUng.DienGiai = kVCTPCT.DienGiai;
            tamUng.LoaiTien = kVCTPCT.LoaiTien;
            tamUng.SoTien = kVCTPCT.SoTien;
            tamUng.SoTienNT = kVCTPCT.SoTienNT;
            tamUng.ConLai = 0;
            tamUng.Conlaint = 0;
            tamUng.TyGia = kVCTPCT.TyGia;
            tamUng.TKNo = kVCTPCT.TKNo;
            tamUng.TKCo = kVCTPCT.TKCo;
            tamUng.Phong = kVPCT.Phong;
            //tamUng.TTTP ??
            tamUng.PhieuTT = kVCTPCT.KVPCTId; // soCT ben KVPCT;
        }
    }
}
