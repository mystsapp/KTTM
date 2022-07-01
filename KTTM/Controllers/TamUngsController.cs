using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using KTTM.Services;
using Data.Utilities;
using KTTM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;

namespace KTTM.Controllers
{
    public class TamUngsController : BaseController
    {
        private readonly ITamUngService _tamUngService;
        private readonly IKVCTPTCService _kVCTPTCService;
        private readonly IKVPTCService _kVPTCService;

        [BindProperty]
        public TamUngViewModel TamUngVM { get; set; }

        public TamUngsController(ITamUngService tamUngService, IKVCTPTCService kVCTPTCService, IKVPTCService kVPTCService)
        {
            TamUngVM = new TamUngViewModel();

            _tamUngService = tamUngService;
            _kVCTPTCService = kVCTPTCService;
            _kVPTCService = kVPTCService;
        }

        public async Task<IActionResult> Index(string searchString, string searchFromDate, string searchToDate, long id, int page = 1)
        {
            if (id == 0)
            {
                ViewBag.id = "";
            }

            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            TamUngVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            TamUngVM.Page = page;

            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            if (id != 0) // for redirect with id
            {
                TamUngVM.TamUng = await _tamUngService.GetByIdAsync(id);
                ViewBag.id = TamUngVM.TamUng.Id;
            }
            else
            {
                TamUngVM.TamUng = new TamUng();
            }
            TamUngVM.TamUngs = await _tamUngService.ListTamUng(searchString, searchFromDate, searchToDate, page, user.Macn);
            return View(TamUngVM);
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(long id) // loaitien == "VN" ==> "UV"(0378UV2014) or "NT" ==> "UN"(0133UN2014)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (id == 0)// kVCTPTCId
            {
                return Json(new
                {
                    status = false,
                    message = "Chi tiết này không có thật!"
                });
            }

            var kVCTPCT = await _kVCTPTCService.GetById(id);
            if (kVCTPCT == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Chi tiết này không có thật!"
                });
            }

            var kVPCT = await _kVPTCService.GetByGuidIdAsync(kVCTPCT.KVPTCId);

            string loaiTienUng;
            if (kVPCT.NgoaiTe == "VN")
            {
                loaiTienUng = "UV";
            }
            else
            {
                loaiTienUng = "UN";
            }

            var tamUng = new TamUng();

            tamUng.Id = kVCTPCT.Id;
            tamUng.MaKhNo = kVCTPCT.MaKhNo;
            tamUng.SoCT = _tamUngService.GetSoCT(loaiTienUng, user.Macn);
            tamUng.NgayCT = kVPCT.NgayCT;// DateTime.Now; // ??????????????
            tamUng.PhieuChi = kVCTPCT.SoCT; // soCT ben KVPCT
            tamUng.DienGiai = kVCTPCT.DienGiaiP;
            tamUng.LoaiTien = kVCTPCT.LoaiTien;
            tamUng.SoTien = kVCTPCT.SoTien.Value;
            tamUng.SoTienNT = kVCTPCT.SoTienNT.Value;
            tamUng.ConLai = kVCTPCT.SoTien.Value;
            tamUng.ConLaiNT = kVCTPCT.SoTienNT.Value;
            tamUng.TyGia = kVCTPCT.TyGia.Value;
            tamUng.TKNo = kVCTPCT.TKNo;
            tamUng.TKCo = kVCTPCT.TKCo;
            tamUng.Phong = kVPCT.Phong;
            tamUng.MaCn = kVPCT.MaCn;
            //tamUng.TTTP ??
            tamUng.PhieuTT = ""; // soCT ben KVPCT khi thanh toan;

            tamUng.NgayTao = DateTime.Now;
            tamUng.NguoiTao = user.Username;
            // ghi log
            tamUng.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    status = false,
                    message = "ModelState is not invalid"
                });
            }
            try
            {
                await _tamUngService.CreateAsync(tamUng);

                // cap nhat cot tamung trong kvctpct
                kVCTPCT.TamUng = tamUng.SoCT; // so tamung
                await _kVCTPTCService.UpdateAsync(kVCTPCT);

                return Json(new
                {
                    status = true
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> CheckTamUng_In_PhieuChi(string soCT, string maCn)
        {
            var tamUngs_By_PhieuChi = await _tamUngService.Find_TamUngs_By_PhieuChi_Include(soCT, maCn);
            if (tamUngs_By_PhieuChi.Count() > 0) // da ton tai 1 cai' tamung nao do'
            {
                return Json(new
                {
                    status = true,
                    message = "Phiếu chi này đã tồn tại tạm ứng."
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public async Task<JsonResult> CheckTamUng(long kVCTPCTId)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            // de hien btnTamUng -> phieuC, TKNo == 1411 or 1412, chưa ketchuyen
            var kVCTPCT = await _kVCTPTCService.GetById(kVCTPCTId);
            if(kVCTPCT.NguoiTao != user.Username)
            {
                return Json(false);
            }
            var kVPCT = await _kVPTCService.GetByGuidIdAsync(kVCTPCT.KVPTCId);
            var tamUng = await _tamUngService.GetByIdAsync(kVCTPCTId);
            if (tamUng == null) // chưa them TU
            {
                if (kVPCT.MFieu == "C")
                {
                    if (kVCTPCT.TKNo.Trim() == "1411" ||
                        kVCTPCT.TKNo.Trim() == "1412") // tamung VND || NgoaiTe
                    {
                        if (string.IsNullOrEmpty(kVCTPCT.SoTT_DaTao)) // chua tao TT nao het
                        {
                            return Json(true);
                        }
                    }
                }
            }

            return Json(false);
        }

        [HttpPost]
        public async Task<JsonResult> CheckTT141(long kVCTPCTId)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            // de hiện btnTT141 TKNo, TKCo == 1411 or 1412, chua TU, chưa Ketchuyen

            var kVCTPCT = await _kVCTPTCService.GetById(kVCTPCTId);
            if (kVCTPCT.NguoiTao != user.Username)
            {
                return Json(false);
            }
            //var result = await CheckTamUng(kVCTPCTId);
            //var checkTU = (bool)result.Value;
            var tamUng = await _tamUngService.GetByIdAsync(kVCTPCTId);

            // return Json(checkTU);

            if (kVCTPCT.TKNo == "1411" || kVCTPCT.TKCo.Trim() == "1411" ||
                kVCTPCT.TKNo == "1412" || kVCTPCT.TKCo.Trim() == "1412") // TT141 VND || NgoaiTe
            {
                if (tamUng == null) // chua TU
                {
                    if (await CheckThanhToan(kVCTPCT))
                    {
                        return Json(true); // cho TU
                    }
                }
            }

            return Json(false);
        }

        private async Task<bool> CheckThanhToan(KVCTPTC kVCTPTC)
        {
            if (!string.IsNullOrEmpty(kVCTPTC.SoTU_DaTT)) return false; // TT rồi -> ko cho TT nua
            else return true;  // chua ketchuyen TT -> cho TT

            //var tamUngs = await _tamUngService.Find_TamUngs_By_PhieuTT(kVCTPTC.SoCT, kVCTPTC.MaCn);
            //if (tamUngs.Count() > 0) // ket chuyen rồi
            //{
            //    return false; // TT rồi -> ko cho TT nua
            //}
            //return true; // chua TT -> cho TT

            //if (kVCTPTC.TKNo == "1411" || kVCTPTC.TKNo == "1412") // thanh toan phieu chi or phieu thu
            //{
            //    if (string.IsNullOrEmpty(kVCTPTC.TamUng)) // chua TU
            //    {
            //        // kiem tra xem có ketchuyen TT141 chua
            //        var tamUngs = await _tamUngService.Find_TamUngs_By_PhieuTT(kVCTPTC.SoCT, kVCTPTC.MaCn);
            //        if (tamUngs.Count() == 0)
            //        {
            //            return false; // chua ketchuyen
            //        }
            //    }
            //}

            //var kVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(id);
            //kVCTPTCs = kVCTPTCs.Where(x => x.TKNo == "1411" || x.TKNo == "1412"); // dang tao phieu TU
            //if (kVCTPTCs.Count() > 0) //
            //{
            //    kVCTPTCs = kVCTPTCs.Where(x => string.IsNullOrEmpty(x.TamUng)); // nhung phieu chua TU
            //    if (kVCTPTCs.Count() > 0) // chua ketchuyen TU -> kt xem ketchuyen TT141 chua
            //    {
            //        // kiem tra xem có ketchuyen TT141 chua
            //        var tamUngs = await _tamUngService.Find_TamUngs_By_PhieuTT(kVPTC.SoCT, kVPTC.MaCn);
            //        if (tamUngs.Count() > 0)
            //        {
            //            return Json(true); // cho inphieu
            //        }
            //        return Json(false);
            //    }
            //}
            //return Json(true);
        }
    }
}