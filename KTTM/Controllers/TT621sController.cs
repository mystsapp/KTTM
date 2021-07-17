using Data.Models_DanhMucKT;
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
                TT621VM.CommentText = "Tạm ứng " + tamUng.SoCT + " còn nợ " + tamUng.SoTien.ToString("N0") + " số tiền cần kết chuyển 141: "
                                      + (tamUng.SoTien - kVCTPCT.SoTien).ToString("N0");
            }

            return View(TT621VM);
        }

        public IActionResult ThemMoiCT_TT_Partial(long tamUngId, long kVCTPCTId_PhieuT) // tamungid == kvctpctid // 1 <-> 1
        {
            TT621VM.TamUngId = tamUngId;
            TT621 tT621 = _tT621Service.GetDummyTT621_By_KVCTPCT(kVCTPCTId_PhieuT);
            TT621VM.TT621 = tT621;

            // tentk
            TT621VM.TenTkNo = _kVCTPCTService.Get_DmTk_By_TaiKhoan(tT621.TKNo).TenTk;
            TT621VM.TenTkCo = _kVCTPCTService.Get_DmTk_By_TaiKhoan(tT621.TKCo).TenTk;
            TT621VM.Dgiais = _kVCTPCTService.Get_DienGiai_By_TkNo_TkCo(tT621.TKNo, tT621.TKCo);

            // ddl
            Data.Models_HDVATOB.Supplier supplier = new Data.Models_HDVATOB.Supplier() { Code = "" };
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            TT621VM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            var viewDmHttcs = _kVCTPCTService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            TT621VM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            TT621VM.Quays = _kVCTPCTService.GetAll_Quay_View();
            var viewMatHangs = _kVCTPCTService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            TT621VM.MatHangs = viewMatHangs;
            TT621VM.PhongBans = _kVCTPCTService.GetAll_PhongBans_View();
            
            return PartialView(TT621VM);
        }

        private void Get_TkNo_TkCo()
        {

            DmTk dmTk = new DmTk() { Tkhoan = "" };
            var dmTks_TienMat = _kVCTPCTService.GetAll_DmTk_TienMat().ToList();
            var dmTks_TaiKhoan = _kVCTPCTService.GetAll_DmTk_TaiKhoan().ToList();
            dmTks_TienMat.Insert(0, dmTk);
            dmTks_TaiKhoan.Insert(0, dmTk);
            //if (TT621VM.KVPCT.MFieu == "T")
            //{ chac chan la phieu T
            TT621VM.DmTks_TkNo = dmTks_TienMat;
            TT621VM.DmTks_TkCo = dmTks_TaiKhoan;
            //}
            //else
            //{
            //    TT621VM.DmTks_TkNo = dmTks_TaiKhoan;
            //    TT621VM.DmTks_TkCo = dmTks_TienMat;
            //}
        }

        public async Task<JsonResult> GetCommentText_By_TamUng(long tamUngId, decimal soTien) // tamUngId == kvctpctId
        {
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            string commentText = "Tạm ứng " + tamUng.SoCT + " còn nợ " + tamUng.SoTien.ToString("N0") + " số tiền cần kết chuyển 141: "
                                  + (tamUng.SoTien - soTien).ToString("N0");
            return Json(commentText);
        }
        public async Task<JsonResult> GetTT621s_By_TamUng(long tamUngId)
        {
            var tT621s = await _tT621Service.GetTT621s_By_TamUng(tamUngId);
            if (tT621s.Count() > 0)
            {
                return Json(new
                {
                    status = true,
                    data = tT621s
                });
            }
            return Json(new
            {
                status = false
            });
        }
    }
}
