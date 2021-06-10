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
        public IActionResult KVCTPCT_Create_Partial(string kvpctid)
        {
            
            KVCTPCTVM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVCTPCT.KVPCTId = kvpctid;

            return PartialView(KVCTPCTVM);
        }

        [HttpPost, ActionName("KVCTPCT_Create_Partial")]
        public async Task<IActionResult> KVCTPCT_Create_Partial_Post()
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            KVCTPCTVM.KVCTPCT.NguoiTao = user.Username;
            KVCTPCTVM.KVCTPCT.NgayTao = DateTime.Now;

            // ghi log
            KVCTPCTVM.KVCTPCT.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
            try
            {
                try
                {
                    await _kVCTPCTService.Create(KVCTPCTVM.KVCTPCT);
                }
                catch (Exception ex)
                {

                    throw;
                }
                

                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    status = false,
                    message = ex.Message
                });
            }

        }

        public JsonResult TinhSoTien(decimal soTienNT, decimal tyGia)
        {
            var soTien = soTienNT * tyGia;
            return Json(new
            {
                status = true,
                soTien = soTien.ToString()
            }) ;
        }
    }
}
