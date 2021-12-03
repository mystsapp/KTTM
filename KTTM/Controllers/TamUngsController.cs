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

        public IActionResult Index()
        {
            return View();
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
            tamUng.NgayCT = DateTime.Now; // ??
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
            var kVCTPCT = await _kVCTPTCService.GetById(kVCTPCTId);
            var kVPCT = await _kVPTCService.GetByGuidIdAsync(kVCTPCT.KVPTCId);
            var tamUng = await _tamUngService.GetByIdAsync(kVCTPCTId);
            if (tamUng == null) // chưa them
                if (kVPCT.MFieu == "C")
                {
                    if (kVCTPCT.TKNo.Trim() == "1411" ||
                        kVCTPCT.TKNo.Trim() == "1412") // tamung VND || NgoaiTe
                        return Json(true);
                }
            return Json(false);
        }

        [HttpPost]
        public async Task<JsonResult> CheckTT141(long kVCTPCTId)
        {
            var kVCTPCT = await _kVCTPTCService.GetById(kVCTPCTId);
            var result = await CheckTamUng(kVCTPCTId);
            var checkTU = (bool)result.Value;
            var tamUng = await _tamUngService.GetByIdAsync(kVCTPCTId);

            // return Json(checkTU);

            if (kVCTPCT.TKNo == "1411" || kVCTPCT.TKCo.Trim() == "1411" ||
                kVCTPCT.TKNo == "1412" || kVCTPCT.TKCo.Trim() == "1412") // TT141 VND || NgoaiTe
            {
                if (tamUng == null) // chua TU
                {
                    return Json(true);
                }
            }

            return Json(false);
        }
    }
}