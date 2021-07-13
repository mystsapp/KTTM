using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Data.Services;
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
        public async Task<IActionResult> Create(long id) // loaitien == "VN" ==> "UV"(0378UV2014) or "NT" ==> "UN"(0133UN2014)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (id == 0)
            {
                return Json(new
                {
                    status = false,
                    message = "Chi tiết này không có thật!"
                });
            }

            var kVCTPCT = await _kVCTPCTService.GetById(id);
            
            if (kVCTPCT == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Chi tiết này không có thật!"
                });
            }

            var kVPCT = await _kVPCTService.GetBySoCT(kVCTPCT.KVPCTId);

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

            tamUng.MaKhNo = kVCTPCT.MaKhNo;
            tamUng.SoCT = _tamUngService.GetSoCT(loaiTienUng);
            tamUng.NgayCT = DateTime.Now; // ??
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

            tamUng.NgayTao = DateTime.Now;
            tamUng.NguoiTao = user.Username;
            // ghi log
            tamUng.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            await _tamUngService.CreateAsync(tamUng);
            return Json(new
            {
                status = true
            });
        }
    }
}
