using Data.Models_KTTM;
using KTTM.Models;
using KTTM.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Controllers
{
    public class TT621sController : BaseController
    {
        private readonly IKVCTPCTService _kVCTPCTService;
        private readonly ITamUngService _tamUngService;
        private readonly ITT621Service _tT621Service;

        [BindProperty]
        public TT621ViewModel TT621VM { get; set; }

        public TT621sController(IKVCTPCTService kVCTPCTService, ITamUngService tamUngService, ITT621Service tT621Service)
        {
            TT621VM = new TT621ViewModel()
            {
                TT621 = new Data.Models_KTTM.TT621(),
                KVCTPCT = new Data.Models_KTTM.KVCTPCT()
            };

            _kVCTPCTService = kVCTPCTService;
            _tamUngService = tamUngService;
            _tT621Service = tT621Service;
        }
        public async Task<IActionResult> TT621Create(long kvctpctId, string strUrl, string page)
        {
            TT621VM.StrUrl = strUrl;
            TT621VM.Page = page;

            if (kvctpctId == 0)
            {
                ViewBag.ErrorMessage = "Chi tiết này không tồn tại";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            var kVCTPCT = await _kVCTPCTService.GetById(kvctpctId);

            if (kVCTPCT == null)
            {
                ViewBag.ErrorMessage = "Chi tiết này không tồn tại";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            TT621VM.KVCTPCT = kVCTPCT;
            // lay het chi tiet ma co' maKhNo co tkNo = 1411 va tamung.contai > 0 (chua thanh toan het)
            TT621VM.TamUngs = await _tamUngService.Find_TamUngs_By_MaKh_Include(kVCTPCT.MaKh); // MaKh == MaKhNo
            TT621VM.TamUngs = TT621VM.TamUngs.OrderByDescending(x => x.NgayCT);
            TamUng tamUng = new TamUng();
            if (TT621VM.TamUngs.Count() != 0)
            {
                tamUng = TT621VM.TamUngs.FirstOrDefault();
                TT621VM.CommentText = "Tạm ứng " + tamUng.SoCT + " còn nợ " + tamUng.SoTien + " số tiền cần kết chuyển 141: "
                                      + (tamUng.SoTien - kVCTPCT.SoTien).ToString("N0");
            }

            return View(TT621VM);
        }
    }
}
