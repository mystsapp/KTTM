using Data.Models_DanhMucKT;
using Data.Models_QLTaiKhoan;
using Data.Services;
using Data.Utilities;
using KTTM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpPost, ActionName("KVCTPCT_Modal_Create_Partial")]
        public async Task<IActionResult> KVCTPCT_Modal_Create_Partial_Post(string strUrl)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                //HomeVM = new HomeViewModel()
                //{
                //    KVPCT = new KVPCT(),
                //    LoaiTiens = _kVPCTService.ListLoaiTien(),
                //    LoaiPhieus = _kVPCTService.ListLoaiPhieu(),
                //    Phongbans = _kVPCTService.GetAllPhongBan(),
                //    StrUrl = strUrl
                //};

                return View(KVCTPCTVM);
            }

            KVCTPCTVM.KVCTPCT.NguoiTao = user.Username;
            KVCTPCTVM.KVCTPCT.NgayTao = DateTime.Now;

            // ghi log
            KVCTPCTVM.KVCTPCT.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _kVCTPCTService.Create(KVCTPCTVM.KVCTPCT);

                SetAlert("Thêm mới thành công.", "success");

                return Redirect(KVCTPCTVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(KVCTPCTVM);
            }

        }

        public async Task<IActionResult> KVCTPCT_Modal_Partial(string soCT, string strUrl)
        {
            DmTk dmTkTmp = new DmTk() { Tkhoan = "" };
            var viewSupplierCode = new Data.Models_DanhMucKT.ViewSupplierCode() { Code = ""};
            KVCTPCTVM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVCTPCT.KVPCTId = soCT;
            KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(soCT);
            KVCTPCTVM.DmHttcs = _kVCTPCTService.GetAll_DmHttc_View();
            var dmTks = _kVCTPCTService.GetAll_DmTk().ToList();
            dmTks.Insert(0, dmTkTmp);
            KVCTPCTVM.DmTks = dmTks;
            KVCTPCTVM.GetAll_TkCongNo_With_TenTK = _kVCTPCTService.GetAll_TkCongNo_With_TenTK();
            KVCTPCTVM.GetAll_TaiKhoan_Except_TkConngNo = _kVCTPCTService.GetAll_TaiKhoan_Except_TkConngNo();
            KVCTPCTVM.Quays = _kVCTPCTService.GetAll_Quay_View();
            var viewSupplierCodes = _kVCTPCTService.GetAll_KhachHangs_ViewCode().ToList();
            viewSupplierCodes.Insert(0, viewSupplierCode);
            KVCTPCTVM.KhachHangs = viewSupplierCodes;
            KVCTPCTVM.MatHangs = _kVCTPCTService.GetAll_MatHangs_View();
            KVCTPCTVM.PhongBans = _kVCTPCTService.GetAll_PhongBans_View();
            KVCTPCTVM.StrUrl = strUrl;

            return PartialView(KVCTPCTVM);
        }

        public async Task<IActionResult> ThemChiTietPhieu(string soCT, string strUrl)
        {

            KVCTPCTVM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVCTPCT.KVPCTId = soCT;
            KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(soCT);
            KVCTPCTVM.DmHttcs = _kVCTPCTService.GetAll_DmHttc_View();
            //KVCTPCTVM.GetAll_TkCongNo_With_TenTK = _kVCTPCTService.GetAll_TkCongNo_With_TenTK();
            //KVCTPCTVM.GetAll_TaiKhoan_Except_TkConngNo = _kVCTPCTService.GetAll_TaiKhoan_Except_TkConngNo();
            KVCTPCTVM.DmTks = _kVCTPCTService.GetAll_DmTk();
            KVCTPCTVM.GetAll_TaiKhoan_Except_TkConngNo = _kVCTPCTService.GetAll_DmTk();
            KVCTPCTVM.Quays = _kVCTPCTService.GetAll_Quay_View();
            KVCTPCTVM.KhachHangs = _kVCTPCTService.GetAll_KhachHangs_ViewCode();
            //KVCTPCTVM.KhachHangJsons = JsonConvert.SerializeObject(KVCTPCTVM.KhachHangs);
            KVCTPCTVM.MatHangs = _kVCTPCTService.GetAll_MatHangs_View();
            KVCTPCTVM.StrUrl = strUrl;

            return View(KVCTPCTVM);
        }

        public JsonResult GetKhachHangs_By_Code(string code)
        {
            //var viewSupplier = _kVCTPCTService.GetAll_KhachHangs_View().Where(x => x.Code == code).FirstOrDefault();
            var viewSupplier = _kVCTPCTService.GetAll_KhachHangs_View_CodeName().Where(x => x.Code == code).FirstOrDefault();
            return Json(new
            {
                data = viewSupplier
                //data = listViewModels.Select(x => x.Name.Trim()).Take(10)
            }); ;
        }
        public JsonResult GetKhachHangs()
        {
            var listViewModels = _kVCTPCTService.GetAll_KhachHangs_View();
            return Json(new
            {
                data = JsonConvert.SerializeObject(listViewModels.Take(100))
                //data = listViewModels.Select(x => x.Name.Trim()).Take(10)
            }); ;
        }
        public JsonResult Get_TenTk_By_Tk(string tk)
        {
            var dmTk = _kVCTPCTService.GetAll_DmTk().Where(x => x.Tkhoan.Trim() == tk.Trim()).FirstOrDefault();
            return Json(new
            {
                data = dmTk.TenTk.Trim()
            });
        }
        public JsonResult Get_DienGiai_By_TkNo_TkCo(string tkNo, string tkCo)
        {
            var listViewModels = _kVCTPCTService.Get_DienGiai_By_TkNo_TkCo(tkNo, tkCo);
            return Json(new
            {
                data = listViewModels
            });
        }

        public JsonResult TinhSoTien(decimal soTienNT, decimal tyGia)
        {
            var soTien = soTienNT * tyGia;
            return Json(new
            {
                status = true,
                soTien = soTien.ToString()
            });
        }
    }
}
