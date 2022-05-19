using Data.Models_DanhMucKT;
using Data.Models_QLTaiKhoan;
using Data.Repository;
using KTTM.Services;
using Data.Utilities;
using Data.ViewModels;
using KTTM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_KTTM;
using Data.Models_Cashier;
using Data.Models_QLXe;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;

namespace KTTM.Controllers
{
    public class KVCTPTCsController : BaseController
    {
        private readonly IKVCTPTCService _kVCTPTCService;
        private readonly IKVPTCService _kVPTCService;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public KVCTPCTViewModel KVCTPCTVM { get; set; }

        public KVCTPTCsController(IKVCTPTCService kVCTPTCService, IKVPTCService kVPTCService, IUnitOfWork unitOfWork)
        {
            _kVCTPTCService = kVCTPTCService;
            _kVPTCService = kVPTCService;
            _unitOfWork = unitOfWork;
            KVCTPCTVM = new KVCTPCTViewModel()
            {
                KVPTC = new Data.Models_KTTM.KVPTC(),
                KVCTPTC = new Data.Models_KTTM.KVCTPTC()
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> KVCTPTCPartial(Guid KVPTCid, int page)
        {
            // KVCTPCT
            KVCTPCTVM.Page = page;
            KVCTPCTVM.KVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(KVPTCid);
            KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVPTCid);

            return PartialView(KVCTPCTVM);
        }

        public async Task<IActionResult> ThemDong(Guid KVPTCId, string strUrl, int page, long id_Dong_Da_Click)
        {
            if (!ModelState.IsValid) // check id_Dong_Da_Click valid (da gang' = 0 trong home/index)
            {
                return View();
            }

            Data.Models_HDVATOB.Supplier supplier = new Data.Models_HDVATOB.Supplier() { Code = "" };
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVCTPTC.KVPTCId = KVPTCId;
            KVCTPCTVM.KVCTPTC.TyGia = 1;

            
            //IEnumerable<DmTk> dM1411 = _kVCTPTCService.Get1411();
            //IEnumerable<DmTk> dM1412 = _kVCTPTCService.Get1412();
            //IEnumerable<DmTk> dM1111000000 = _kVCTPTCService.Get1111000000();

            KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVPTCId);
            Get_TkNo_TkCo();
            if (KVCTPCTVM.KVPTC.NgoaiTe == "VN")
            {
                KVCTPCTVM.KVCTPTC.LoaiTien = "VND";

                if (KVCTPCTVM.KVPTC.MFieu == "C")
                {
                    KVCTPCTVM.KVCTPTC.TKCo = "1111000000";
                    KVCTPCTVM.DmTks_TkCo = _kVCTPTCService.GetAll_DmTk_TienMat().Where(x => x.Tkhoan.Trim() == "1111000000"); // 3429: 1111000000
                    var dmTks_TkNoEx = KVCTPCTVM.DmTks_TkNo.Where(x => x.Tkhoan.Trim() == "1412" || x.Tkhoan.StartsWith("111"));
                    KVCTPCTVM.DmTks_TkNo = KVCTPCTVM.DmTks_TkNo.Except(dmTks_TkNoEx);//.Except(dmTks_TkNo1112);
                }
                else // T
                {
                    KVCTPCTVM.KVCTPTC.TKNo = "1111000000";
                    KVCTPCTVM.DmTks_TkNo = _kVCTPTCService.GetAll_DmTk_TienMat().Where(x => x.Tkhoan.Trim() == "1111000000"); // 3429: 1111000000
                    var dmTks_TkCoEx = KVCTPCTVM.DmTks_TkCo.Where(x => x.Tkhoan.Trim() == "1412" || x.Tkhoan.StartsWith("111"));
                    KVCTPCTVM.DmTks_TkCo = KVCTPCTVM.DmTks_TkCo.Except(dmTks_TkCoEx);
                }
            }
            if (KVCTPCTVM.KVPTC.NgoaiTe == "NT")
            {
                //KVCTPCTVM.Ngoaites.Where
                if (KVCTPCTVM.KVPTC.MFieu == "C")
                {
                    KVCTPCTVM.DmTks_TkCo = KVCTPCTVM.DmTks_TkCo.Where(x => x.Tkhoan.StartsWith("1112"));
                    var dmTks_TkNoEx = KVCTPCTVM.DmTks_TkNo.Where(x => x.Tkhoan.Trim() == "1411" || x.Tkhoan.StartsWith("111"));
                    KVCTPCTVM.DmTks_TkNo = KVCTPCTVM.DmTks_TkNo.Except(dmTks_TkNoEx);
                    
                    

                }
                else // T
                {
                    KVCTPCTVM.DmTks_TkNo = KVCTPCTVM.DmTks_TkNo.Where(x => x.Tkhoan.StartsWith("1112"));
                    var dmTks_TkCoEx = KVCTPCTVM.DmTks_TkCo.Where(x => x.Tkhoan.Trim() == "1411" || x.Tkhoan.StartsWith("111"));
                    KVCTPCTVM.DmTks_TkCo = KVCTPCTVM.DmTks_TkCo.Except(dmTks_TkCoEx);
                    

                }
            }

            

            var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            KVCTPCTVM.DmHttcs = viewDmHttcs;

            //Get_TkNo_TkCo();

            KVCTPCTVM.Quays = _kVCTPTCService.GetAll_Quay_View();
            var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            KVCTPCTVM.MatHangs = viewMatHangs;
            KVCTPCTVM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();
            KVCTPCTVM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
            KVCTPCTVM.StrUrl = strUrl;
            KVCTPCTVM.Page = page; // page for redirect
            KVCTPCTVM.KVCTPTC.NgayCTGoc = DateTime.Now; // Thao

            // btnThemdong + copy dong da click
            if (id_Dong_Da_Click > 0)
            {
                var dongCu = await _kVCTPTCService.GetById(id_Dong_Da_Click);
                KVCTPCTVM.KVCTPTC = dongCu;
            }

            return View(KVCTPCTVM);
        }

        [HttpPost, ActionName("ThemDong")]
        public async Task<IActionResult> ThemDong_Post(Guid KVPTCId, int page)
        {
            // var soCT = KVCTPCTVM.KVCTPCT.KVPCTId;
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                // not valid
                KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVCTPCTVM.KVCTPTC.KVPTCId);
                KVCTPCTVM.Page = page;
                KVCTPCTVM.DmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
                KVCTPCTVM.Quays = _kVCTPTCService.GetAll_Quay_View();
                KVCTPCTVM.MatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
                KVCTPCTVM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();
                KVCTPCTVM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
                //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
                //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                Get_TkNo_TkCo();

                return View(KVCTPCTVM);
            }
            if (KVCTPCTVM.KVPTC.MFieu == "T") // check makhno required
            {
                var result = TkChoMaKh(KVCTPCTVM.KVCTPTC.TKCo);
                var boolResult = result.Value.ToString();

                if (bool.Parse(boolResult) && string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.MaKhCo))
                {
                    ModelState.AddModelError("", "MaKhCo không được để trống.");
                    // not valid
                    KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVCTPCTVM.KVCTPTC.KVPTCId);
                    KVCTPCTVM.Page = page;
                    KVCTPCTVM.DmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
                    KVCTPCTVM.Quays = _kVCTPTCService.GetAll_Quay_View();
                    KVCTPCTVM.MatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
                    KVCTPCTVM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();
                    KVCTPCTVM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
                    //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                    KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
                    //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                    Get_TkNo_TkCo();

                    return View(KVCTPCTVM);
                }
            }
            else // check makhco required
            {
                var result = TkChoMaKh(KVCTPCTVM.KVCTPTC.TKNo);
                var boolResult = result.Value.ToString();

                if (bool.Parse(boolResult) && string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.MaKhNo))
                {
                    ModelState.AddModelError("", "MaKhNo không được để trống.");
                    // not valid
                    KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVCTPCTVM.KVCTPTC.KVPTCId);
                    KVCTPCTVM.Page = page;
                    KVCTPCTVM.DmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
                    KVCTPCTVM.Quays = _kVCTPTCService.GetAll_Quay_View();
                    KVCTPCTVM.MatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
                    KVCTPCTVM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();
                    KVCTPCTVM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
                    //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                    KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
                    //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                    Get_TkNo_TkCo();

                    return View(KVCTPCTVM);
                }
            }

            KVCTPCTVM.KVCTPTC.SoCT = KVCTPCTVM.KVPTC.SoCT;
            KVCTPCTVM.KVCTPTC.SoTienNT = KVCTPCTVM.KVCTPTC.SoTienNT == null ? 0 : KVCTPCTVM.KVCTPTC.SoTienNT.Value;
            KVCTPCTVM.KVCTPTC.SoTien = KVCTPCTVM.KVCTPTC.SoTien == null ? 0 : KVCTPCTVM.KVCTPTC.SoTien.Value;
            KVCTPCTVM.KVCTPTC.VAT = KVCTPCTVM.KVCTPTC.VAT ?? 0;
            //KVCTPCTVM.KVCTPTC.DSKhongVAT = KVCTPCTVM.KVCTPTC.DSKhongVAT ?? 0;
            KVCTPCTVM.KVCTPTC.NguoiTao = user.Username;
            KVCTPCTVM.KVCTPTC.MaCn = user.Macn;
            KVCTPCTVM.KVCTPTC.DienGiaiP = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.DienGiaiP) ? "" : KVCTPCTVM.KVCTPTC.DienGiaiP.Trim().ToUpper();
            KVCTPCTVM.KVCTPTC.NgayTao = DateTime.Now;
            //KVCTPCTVM.KVCTPTC.MaKh = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.MaKhNo) ? KVCTPCTVM.KVCTPTC.MaKhCo : KVCTPCTVM.KVCTPTC.MaKhNo;
            KVCTPCTVM.KVCTPTC.MaKh = (KVCTPCTVM.KVPTC.MFieu == "T") ? KVCTPCTVM.KVCTPTC.MaKhCo : KVCTPCTVM.KVCTPTC.MaKhNo; // T : láy MaKhCo, C: MaKhNo
            KVCTPCTVM.KVCTPTC.MaKhNo = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.MaKhNo) ? "" : KVCTPCTVM.KVCTPTC.MaKhNo.ToUpper();
            KVCTPCTVM.KVCTPTC.MaKhCo = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.MaKhCo) ? "" : KVCTPCTVM.KVCTPTC.MaKhCo.ToUpper();

            // ghi log
            KVCTPCTVM.KVCTPTC.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _kVCTPTCService.Create(KVCTPCTVM.KVCTPTC);

                SetAlert("Thêm mới thành công.", "success");
                return BackIndex(KVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?id
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(KVCTPCTVM);
            }
        }

        private void Get_TkNo_TkCo()
        {
            DmTk dmTk = new DmTk() { Tkhoan = "" };
            var dmTks_TienMat = _kVCTPTCService.GetAll_DmTk_TienMat().ToList();
            var dmTks_TaiKhoan = _kVCTPTCService.GetAll_DmTk_TaiKhoan().ToList();
            dmTks_TienMat.Insert(0, dmTk);
            dmTks_TaiKhoan.Insert(0, dmTk);
            if (KVCTPCTVM.KVPTC.MFieu == "T")
            {
                KVCTPCTVM.DmTks_TkNo = dmTks_TienMat;
                KVCTPCTVM.DmTks_TkCo = dmTks_TaiKhoan;
            }
            else
            {
                KVCTPCTVM.DmTks_TkNo = dmTks_TaiKhoan;
                KVCTPCTVM.DmTks_TkCo = dmTks_TienMat;
            }
        }

        // ThemDong_ContextMenu
        public async Task<IActionResult> ThemDong_ContextMenu(Guid KVPTCId, int page, long? kvctptcId) // kvctptcId: copy dong dang chon
        {
            if (kvctptcId == 0 || kvctptcId == null) // ko co copy dong
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                ViewSupplierCode viewSupplierCode = new Data.Models_DanhMucKT.ViewSupplierCode() { Code = "" };
                ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
                ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

                //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
                //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                KVCTPCTVM.KVCTPTC.KVPTCId = KVPTCId;
                KVCTPCTVM.KVCTPTC.TyGia = 1;
                KVCTPCTVM.KVCTPTC.LoaiTien = "VND";
                KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVPTCId);
                if (KVCTPCTVM.KVPTC.NgoaiTe == "VN")
                {
                    KVCTPCTVM.KVCTPTC.LoaiTien = "VND";
                }

                var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
                viewDmHttcs.Insert(0, viewDmHttc);
                KVCTPCTVM.DmHttcs = viewDmHttcs;

                Get_TkNo_TkCo();

                KVCTPCTVM.Quays = _kVCTPTCService.GetAll_Quay_View();

                var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
                viewMatHangs.Insert(0, viewMatHang);
                KVCTPCTVM.MatHangs = viewMatHangs;
                KVCTPCTVM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();
                KVCTPCTVM.Page = page;
                KVCTPCTVM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
                KVCTPCTVM.KVCTPTC.NgayCTGoc = DateTime.Now; // Thao

                //// kvctptcId: copy dong dang chon
                //var kVCTPTC = await _kVCTPTCService.GetById(kvctptcId); // dong cu
                //KVCTPCTVM.KVCTPTC = kVCTPTC;
                KVCTPCTVM.Dgiais = _kVCTPTCService.GetAll_DienGiai();//.Get_DienGiai_By_TkNo_TkCo(KVCTPCTVM.KVCTPTC.TKNo, KVCTPCTVM.KVCTPTC.TKCo);
            }
            else
            {
                KVCTPCTVM.Page = page;
                // from session
                var user = HttpContext.Session.GetSingle<User>("loginUser");

                if (kvctptcId == 0)
                {
                    ViewBag.ErrorMessage = "Chi tiết phiếu này không tồn tại.";
                    return View("~/Views/Shared/NotFound.cshtml");
                }
                else
                {
                    KVCTPCTVM.KVCTPTC = await _kVCTPTCService.GetById(kvctptcId.Value);
                }

                // tentk
                KVCTPCTVM.TenTkNo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(KVCTPCTVM.KVCTPTC.TKNo).TenTk;
                KVCTPCTVM.TenTkCo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(KVCTPCTVM.KVCTPTC.TKCo).TenTk;
                KVCTPCTVM.Dgiais = _kVCTPTCService.Get_DienGiai_By_TkNo_TkCo(KVCTPCTVM.KVCTPTC.TKNo, KVCTPCTVM.KVCTPTC.TKCo);

                if (KVCTPCTVM.KVCTPTC == null)
                {
                    ViewBag.ErrorMessage = "Chi tiết phiếu này không tồn tại.";
                    return View("~/Views/Shared/NotFound.cshtml");
                }

                ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
                ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

                //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
                //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVCTPCTVM.KVCTPTC.KVPTCId);
                if (KVCTPCTVM.KVPTC.NgoaiTe == "VN")
                {
                    KVCTPCTVM.KVCTPTC.LoaiTien = "VND";
                }
                var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
                viewDmHttcs.Insert(0, viewDmHttc);
                KVCTPCTVM.DmHttcs = viewDmHttcs;

                Get_TkNo_TkCo();

                KVCTPCTVM.Quays = _kVCTPTCService.GetAll_Quay_View();

                var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
                viewMatHangs.Insert(0, viewMatHang);
                KVCTPCTVM.MatHangs = viewMatHangs;
                KVCTPCTVM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();
                KVCTPCTVM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
            }

            return View(KVCTPCTVM);
        }

        [HttpPost, ActionName("ThemDong_ContextMenu")]
        public async Task<IActionResult> ThemDong_ContextMenu_Post(Guid KVPTCId, int page)
        {
            //var soCT = KVCTPCTVM.KVCTPCT.KVPCTId;

            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                // not valid
                KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVCTPCTVM.KVCTPTC.KVPTCId);
                KVCTPCTVM.Page = page;
                KVCTPCTVM.DmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
                KVCTPCTVM.Quays = _kVCTPTCService.GetAll_Quay_View();
                KVCTPCTVM.MatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
                KVCTPCTVM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();
                KVCTPCTVM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
                //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
                //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                Get_TkNo_TkCo();

                return View(KVCTPCTVM);
            }

            // dong full
            KVCTPCTVM.KVCTPTC.SoCT = KVCTPCTVM.KVPTC.SoCT;
            KVCTPCTVM.KVCTPTC.NguoiTao = user.Username;
            KVCTPCTVM.KVCTPTC.DienGiaiP = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.DienGiaiP) ? "" : KVCTPCTVM.KVCTPTC.DienGiaiP.Trim().ToUpper();// KVCTPCTVM.KVCTPTC.DienGiaiP.Trim().ToUpper();
            KVCTPCTVM.KVCTPTC.MaCn = user.Macn;
            KVCTPCTVM.KVCTPTC.NgayTao = DateTime.Now;
            KVCTPCTVM.KVCTPTC.MaKh = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.MaKhNo) ? KVCTPCTVM.KVCTPTC.MaKhCo : KVCTPCTVM.KVCTPTC.MaKhNo;
            KVCTPCTVM.KVCTPTC.MaKhNo = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.MaKhNo) ? "" : KVCTPCTVM.KVCTPTC.MaKhNo.ToUpper();
            KVCTPCTVM.KVCTPTC.SoTien = KVCTPCTVM.KVCTPTC.SoTienNT * KVCTPCTVM.KVCTPTC.TyGia;
            KVCTPCTVM.KVCTPTC.DSKhongVAT = KVCTPCTVM.KVCTPTC.SoTienNT.Value;
            //KVCTPCTVM.KVCTPTC.DSKhongVAT ??= 0;
            //KVCTPCTVM.KVCTPTC.SoTienNT : "co roi: tren view"

            // ghi log
            KVCTPCTVM.KVCTPTC.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _kVCTPTCService.Create(KVCTPCTVM.KVCTPTC);

                // dong tach
                KVCTPTC kVCTPTC = KVCTPCTVM.KVCTPTC;
                kVCTPTC.Id = 0;
                kVCTPTC.SoTienNT = decimal.Parse(KVCTPCTVM.ThueGTGT);
                kVCTPTC.SoTien = kVCTPTC.SoTienNT * kVCTPTC.TyGia;
                kVCTPTC.TKNo = "1331000010";
                kVCTPTC.DSKhongVAT = KVCTPCTVM.KVCTPTC.DSKhongVAT;
                await _kVCTPTCService.Create(kVCTPTC);

                SetAlert("Thêm mới thành công.", "success");
                return BackIndex(KVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?soCT
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(KVCTPCTVM);
            }
        }

        //-----------LayDataQLXePartial------------
        public async Task<IActionResult> LayDataQLXePartial(Guid kVPTCId, string strUrl, int page)
        {
            if (kVPTCId == null)
                return NotFound();

            KVCTPCTVM.StrUrl = strUrl;
            KVCTPCTVM.Page = page;
            KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(kVPTCId);
            //KVCTPCTVM.DmTks_Cashier = _kVCTPTCService.GetAll_DmTk_Cashier();

            return PartialView(KVCTPCTVM);
        }

        [HttpPost, ActionName("LayDataQLXePartial")]
        [ValidateAntiForgeryToken] // ko cho submit nhieu lan
        public async Task<IActionResult> LayDataQLXePartial_Post(string soPhieu)
        {
            var kVPTCId = KVCTPCTVM.KVPTC.Id;
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                return View(KVCTPCTVM);
            }

            // data tu qlxe
            IEnumerable<KVCTPTC> kVCTPTCs = await _kVCTPTCService.GetKVCTPTCs_QLXe(soPhieu.Trim(),
                                            kVPTCId, KVCTPCTVM.KVPTC.SoCT, user.Username, user.Macn);

            if (!kVCTPTCs.Any())
            {
                SetAlert("Số phiếu này đã kéo rồi!", "warning");
                return BackIndex(kVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?id
            }
            // ghi log ben service

            if (kVCTPTCs == null) //545 ben kvctptcService
            {
                SetAlert("Phiếu này không có bất cứ dòng nào!", "warning");
                return BackIndex(kVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?id
            }

            try
            {
                await _kVCTPTCService.CreateRange(kVCTPTCs);

                // save to qlxe
                var kVPCT = await _kVPTCService.GetByGuidIdAsync(kVPTCId);
                List<Thuchi> thuchis = _kVCTPTCService.GetThuChiXe_By_SoPhieu(soPhieu).ToList();
                foreach (var item in thuchis)
                {
                    item.SoCtKttm = kVPCT.SoCT;
                    await _kVCTPTCService.UpdateAsync_ThuChiXe(item);
                }

                SetAlert("Kéo thành công.", "success");
                return BackIndex(kVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?id
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return BackIndex(kVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?id
            }
        }

        //-----------LayDataCashierPartial------------
        public async Task<IActionResult> LayDataCashierPartial(Guid kVPTCId, string strUrl, int page)
        {
            if (kVPTCId == null)
                return NotFound();

            KVCTPCTVM.StrUrl = strUrl;
            KVCTPCTVM.Page = page;
            KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(kVPTCId);
            KVCTPCTVM.DmTks_Cashier = _kVCTPTCService.GetAll_DmTk_Cashier();

            return PartialView(KVCTPCTVM);
        }

        [HttpPost, ActionName("LayDataCashierPartial")]
        [ValidateAntiForgeryToken] // ko cho submit nhieu lan
        public async Task<IActionResult> LayDataCashierPartial_Post()
        {
            var ngayCT = KVCTPCTVM.KVPTC.NgayCT;
            var kVPTCId = KVCTPCTVM.KVPTC.Id;
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                return View(KVCTPCTVM);
            }

            // data tu cashier
            var kVCTPTCs = _kVCTPTCService.GetKVCTPTCs(KVCTPCTVM.LayDataCashierModel.BaoCaoSo.ToUpper().Trim(),
                kVPTCId, KVCTPCTVM.KVPTC.SoCT, user.Username, user.Macn, KVCTPCTVM.KVPTC.MFieu,
                KVCTPCTVM.LayDataCashierModel.Tk.Trim(), KVCTPCTVM.LayDataCashierModel.TienMat,
                KVCTPCTVM.LayDataCashierModel.TTThe);
            // ghi log ben service

            if (kVCTPTCs.Count() == 0) //545 ben kvctptcService
            {
                SetAlert("Báo cáo số không có bất cứ dòng nào!", "warning");
                return BackIndex(kVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?id
            }

            try
            {
                await _kVCTPTCService.CreateRange(kVCTPTCs);

                // save to cashier
                var kVPCT = await _kVPTCService.GetByGuidIdAsync(kVPTCId);
                var noptien = await _unitOfWork.nopTienRepository.GetById(KVCTPCTVM.LayDataCashierModel.BaoCaoSo.Trim(), user.Macn);

                noptien.Ngaypt = kVPCT.NgayCT;
                if (KVCTPCTVM.LayDataCashierModel.TienMat)
                {
                    noptien.Phieuthu = KVCTPCTVM.KVPTC.SoCT;
                }
                if (KVCTPCTVM.LayDataCashierModel.TTThe)
                {
                    noptien.Phieuthucc = KVCTPCTVM.KVPTC.SoCT;
                }
                //noptien.Ghichu = kVPCT.ghichu; ??
                await _kVCTPTCService.UpdateAsync_NopTien(noptien);

                // TTThe, phieu thu, cap tk(no = 1111000000, co = 1311)
                // -> tu tao phieu chi cap tk(no = 1131, co =1311110000)
                if (KVCTPCTVM.LayDataCashierModel.TTThe &&
                    kVPCT.MFieu == "T")
                {
                    if (KVCTPCTVM.LayDataCashierModel.Tk == "1311110000" ||
                        KVCTPCTVM.LayDataCashierModel.Tk == "1311120000" ||
                        KVCTPCTVM.LayDataCashierModel.Tk == "1368000000")
                        KVCTPCTVM.KVPTC = new KVPTC();

                    // tao phieuchi
                    KVCTPCTVM.KVPTC.Create = DateTime.Now;
                    KVCTPCTVM.KVPTC.LapPhieu = user.Username;
                    KVCTPCTVM.KVPTC.MaCn = user.Macn;
                    KVCTPCTVM.KVPTC.MFieu = "C";
                    KVCTPCTVM.KVPTC.NgayCT = ngayCT;// DateTime.Now;
                    KVCTPCTVM.KVPTC.DonVi = "CÔNG TY TNHH MỘT THÀNH VIÊN DỊCH VỤ LỮ HÀNH SAIGONTOURIST";
                    KVCTPCTVM.KVPTC.NgoaiTe = kVPCT.NgoaiTe;
                    KVCTPCTVM.KVPTC.HoTen = "VCB(CHAU)"; // thao
                    KVCTPCTVM.KVPTC.Phong = "KT";

                    // next SoCT --> bat buoc phai co'
                    KVCTPCTVM.KVPTC.SoCT = _kVPTCService.GetSoCT("QC", user.Macn); // chi VND
                                                                                   // next SoCT

                    KVCTPCTVM.KVPTC.LapPhieu = user.Username;
                    // ghi log
                    KVCTPCTVM.KVPTC.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

                    KVPTC kVPTC1 = new KVPTC();
                    try
                    {
                        //await _kVPTCService.CreateAsync(KVCTPCTVM.KVPTC); // save
                        kVPTC1 = await _kVPTCService.CreateAsync_ReturnEntity(KVCTPCTVM.KVPTC);
                    }
                    catch (Exception ex)
                    {
                        SetAlert(ex.Message, "error");
                        return BackIndex(kVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?id
                    }
                    // tao phieuchi

                    // tao ct phieuchi
                    List<KVCTPTC> kVCTPTCs1 = new List<KVCTPTC>();
                    //List<KVCTPTC> kVCTPTCs2 = kVCTPTCs.ToList();
                    // lay chitiet ntbill cho vcb(chau)
                    var kVCTPTCs2 = _kVCTPTCService.GetKVCTPTCs_VCBChau(KVCTPCTVM.LayDataCashierModel.BaoCaoSo.ToUpper().Trim(),
                kVPTCId, KVCTPCTVM.KVPTC.SoCT, user.Username, user.Macn, kVPCT.MFieu, // chi lam phieu T
                KVCTPCTVM.LayDataCashierModel.Tk.Trim(), KVCTPCTVM.LayDataCashierModel.TienMat,
                KVCTPCTVM.LayDataCashierModel.TTThe);

                    foreach (var item in kVCTPTCs2) // dao cap tk
                    {
                        item.Id = 0;
                        item.TKNo = "1131";
                        item.TKCo = "1111000000";
                        item.KVPTCId = kVPTC1.Id;
                        item.SoCT = kVPTC1.SoCT;
                        item.MaKh = ""; // thao
                        item.MaKhCo = ""; // thao
                        item.MaKhNo = ""; // thao
                        item.BoPhan = "CH"; // thao
                        item.NoQuay = ""; // thao
                        item.CoQuay = ""; // thao
                        item.Sgtcode = ""; // thao
                        item.DienGiai = "CHI NOP NGAN HANG =CC"; // thao
                        item.DienGiaiP = "Chi nộp " + item.LoaiThe + " " + item.SalesSlip + " " + item.CardNumber; // thao
                        kVCTPTCs1.Add(item);

                        await _kVCTPTCService.Create(item);
                    }
                    //if (kVCTPTCs1.Count > 0)
                    //{
                    //    foreach(var item in kVCTPTCs1)
                    //    {
                    //        await _kVCTPTCService.Create(item);
                    //    }
                    //    //try
                    //    //{
                    //    //    await _kVCTPTCService.CreateRange(kVCTPTCs1);
                    //    //}
                    //    //catch (Exception ex)
                    //    //{

                    //    //    throw;
                    //    //}
                        
                    //}
                    // tao ct phieuchi
                }
                // TTThe, phieu thu, cap tk(no = 1111000000, co = 1311)
                // -> tu tao phieu chi cap tk(no = 1131, co =1111000000)

                SetAlert("Kéo thành công.", "success");
                return BackIndex(kVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?id
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return BackIndex(kVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?id
            }
        }

        // Edit_KVCTPCT
        public async Task<IActionResult> Edit_KVCTPTC(long id, int page)
        {
            KVCTPCTVM.Page = page;
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (id == 0)
            {
                ViewBag.ErrorMessage = "Chi tiết phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            KVCTPCTVM.KVCTPTC = await _kVCTPTCService.GetById(id);

            // tentk
            KVCTPCTVM.TenTkNo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(KVCTPCTVM.KVCTPTC.TKNo).TenTk;
            KVCTPCTVM.TenTkCo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(KVCTPCTVM.KVCTPTC.TKCo).TenTk;
            KVCTPCTVM.Dgiais = _kVCTPTCService.Get_DienGiai_By_TkNo_TkCo(KVCTPCTVM.KVCTPTC.TKNo, KVCTPCTVM.KVCTPTC.TKCo);

            if (KVCTPCTVM.KVCTPTC == null)
            {
                ViewBag.ErrorMessage = "Chi tiết phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVCTPCTVM.KVCTPTC.KVPTCId);
            if (KVCTPCTVM.KVPTC.NgoaiTe == "VN")
            {
                KVCTPCTVM.KVCTPTC.LoaiTien = "VND";
            }
            var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            KVCTPCTVM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            KVCTPCTVM.Quays = _kVCTPTCService.GetAll_Quay_View();

            var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            KVCTPCTVM.MatHangs = viewMatHangs;
            KVCTPCTVM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();
            KVCTPCTVM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();

            return View(KVCTPCTVM);
        }

        [HttpPost, ActionName("Edit_KVCTPTC")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_KVCTPTC_Post(long id, int page)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");
            string temp = "";

            if (id == 0)
            {
                ViewBag.ErrorMessage = "Chi tiết phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            if (ModelState.IsValid)
            {
                KVCTPCTVM.KVCTPTC.NgaySua = DateTime.Now;
                KVCTPCTVM.KVCTPTC.NguoiSua = user.Username;
                KVCTPCTVM.KVCTPTC.DienGiaiP = KVCTPCTVM.KVCTPTC.DienGiaiP.Trim().ToUpper();
                KVCTPCTVM.KVCTPTC.TenKH = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.TenKH) ? "" : KVCTPCTVM.KVCTPTC.TenKH.Trim().ToUpper();
                KVCTPCTVM.KVCTPTC.MaKh = (KVCTPCTVM.KVPTC.MFieu == "T") ? KVCTPCTVM.KVCTPTC.MaKhCo : KVCTPCTVM.KVCTPTC.MaKhNo; // T : láy MaKhCo, C: MaKhNo

                // kiem tra thay doi : trong getbyid() va ngoai view

                #region log file

                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _kVCTPTCService.GetBySoCTAsNoTracking(id);

                if (t.HTTC != KVCTPCTVM.KVCTPTC.HTTC)
                {
                    temp += String.Format("- HTTC thay đổi: {0}->{1}", t.HTTC, KVCTPCTVM.KVCTPTC.HTTC);
                }

                if (t.DienGiai != KVCTPCTVM.KVCTPTC.DienGiai)
                {
                    temp += String.Format("- DienGiai thay đổi: {0}->{1}", t.DienGiai, KVCTPCTVM.KVCTPTC.DienGiai);
                }

                if (t.TKNo != KVCTPCTVM.KVCTPTC.TKNo)
                {
                    temp += String.Format("- TKNo thay đổi: {0}->{1}", t.TKNo, KVCTPCTVM.KVCTPTC.TKNo);
                }

                if (t.TKCo != KVCTPCTVM.KVCTPTC.TKCo)
                {
                    temp += String.Format("- TKCo thay đổi: {0}->{1}", t.TKCo, KVCTPCTVM.KVCTPTC.TKCo);
                }

                if (t.Sgtcode != KVCTPCTVM.KVCTPTC.Sgtcode)
                {
                    temp += String.Format("- Sgtcode thay đổi: {0}->{1}", t.Sgtcode, KVCTPCTVM.KVCTPTC.Sgtcode);
                }

                if (t.MaKhNo != KVCTPCTVM.KVCTPTC.MaKhNo)
                {
                    temp += String.Format("- MaKhNo thay đổi: {0}->{1}", t.MaKhNo, KVCTPCTVM.KVCTPTC.MaKhNo);
                }

                if (t.MaKhCo != KVCTPCTVM.KVCTPTC.MaKhCo)
                {
                    temp += String.Format("- MaKhCo thay đổi: {0}->{1}", t.MaKhCo, KVCTPCTVM.KVCTPTC.MaKhCo);
                }

                if (t.SoTienNT != KVCTPCTVM.KVCTPTC.SoTienNT)
                {
                    temp += String.Format("- SoTienNT thay đổi: {0:N0}->{1:N0}", t.SoTienNT, KVCTPCTVM.KVCTPTC.SoTienNT);
                }

                if (t.LoaiTien != KVCTPCTVM.KVCTPTC.LoaiTien)
                {
                    temp += String.Format("- LoaiTien thay đổi: {0}->{1}", t.LoaiTien, KVCTPCTVM.KVCTPTC.LoaiTien);
                }

                if (t.TyGia != KVCTPCTVM.KVCTPTC.TyGia)
                {
                    temp += String.Format("- TyGia thay đổi: {0:N0}->{1:N0}", t.TyGia, KVCTPCTVM.KVCTPTC.TyGia);
                }

                if (t.SoTien != KVCTPCTVM.KVCTPTC.SoTien)
                {
                    temp += String.Format("- SoTien thay đổi: {0:N0}->{1:N0}", t.SoTien, KVCTPCTVM.KVCTPTC.SoTien);
                }

                if (t.MaKh != KVCTPCTVM.KVCTPTC.MaKh)
                {
                    temp += String.Format("- MaKh thay đổi: {0}->{1}", t.MaKh, KVCTPCTVM.KVCTPTC.MaKh);
                }

                if (t.KhoangMuc != KVCTPCTVM.KVCTPTC.KhoangMuc)
                {
                    temp += String.Format("- KhoangMuc thay đổi: {0}->{1}", t.KhoangMuc, KVCTPCTVM.KVCTPTC.KhoangMuc);
                }

                if (t.HTTT != KVCTPCTVM.KVCTPTC.HTTT)
                {
                    temp += String.Format("- HTTT thay đổi: {0}->{1}", t.HTTT, KVCTPCTVM.KVCTPTC.HTTT);
                }

                if (t.CardNumber != KVCTPCTVM.KVCTPTC.CardNumber)
                {
                    temp += String.Format("- CardNumber thay đổi: {0}->{1}", t.CardNumber, KVCTPCTVM.KVCTPTC.CardNumber);
                }

                if (t.SalesSlip != KVCTPCTVM.KVCTPTC.SalesSlip)
                {
                    temp += String.Format("- SalesSlip thay đổi: {0}->{1}", t.SalesSlip, KVCTPCTVM.KVCTPTC.SalesSlip);
                }

                if (t.SoXe != KVCTPCTVM.KVCTPTC.SoXe)
                {
                    temp += String.Format("- SoXe thay đổi: {0}->{1}", t.SoXe, KVCTPCTVM.KVCTPTC.SoXe);
                }

                if (t.MsThue != KVCTPCTVM.KVCTPTC.MsThue)
                {
                    temp += String.Format("- MsThue thay đổi: {0}->{1}", t.MsThue, KVCTPCTVM.KVCTPTC.MsThue);
                }

                if (t.LoaiHDGoc != KVCTPCTVM.KVCTPTC.LoaiHDGoc)
                {
                    temp += String.Format("- LoaiHDGoc thay đổi: {0}->{1}", t.LoaiHDGoc, KVCTPCTVM.KVCTPTC.LoaiHDGoc);
                }

                if (t.SoCTGoc != KVCTPCTVM.KVCTPTC.SoCTGoc)
                {
                    temp += String.Format("- SoCTGoc thay đổi: {0}->{1}", t.SoCTGoc, KVCTPCTVM.KVCTPTC.SoCTGoc);
                }

                if (t.NgayCTGoc != KVCTPCTVM.KVCTPTC.NgayCTGoc)
                {
                    temp += String.Format("- NgayCTGoc thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayCTGoc, KVCTPCTVM.KVCTPTC.NgayCTGoc);
                }

                if (t.VAT != KVCTPCTVM.KVCTPTC.VAT)
                {
                    temp += String.Format("- VAT thay đổi: {0:N0}->{1:N0}", t.NgayCTGoc, KVCTPCTVM.KVCTPTC.VAT);
                }

                if (t.DSKhongVAT != KVCTPCTVM.KVCTPTC.DSKhongVAT)
                {
                    temp += String.Format("- DSKhongVAT thay đổi: {0:N0}->{1:N0}", t.DSKhongVAT, KVCTPCTVM.KVCTPTC.DSKhongVAT);
                }

                if (t.BoPhan != KVCTPCTVM.KVCTPTC.BoPhan)
                {
                    temp += String.Format("- DSKhongVAT thay đổi: {0}->{1}", t.BoPhan, KVCTPCTVM.KVCTPTC.BoPhan);
                }

                if (t.STT != KVCTPCTVM.KVCTPTC.STT)
                {
                    temp += String.Format("- STT thay đổi: {0}->{1}", t.STT, KVCTPCTVM.KVCTPTC.STT);
                }

                if (t.NoQuay != KVCTPCTVM.KVCTPTC.NoQuay)
                {
                    temp += String.Format("- NoQuay thay đổi: {0}->{1}", t.NoQuay, KVCTPCTVM.KVCTPTC.NoQuay);
                }

                if (t.CoQuay != KVCTPCTVM.KVCTPTC.CoQuay)
                {
                    temp += String.Format("- CoQuay thay đổi: {0}->{1}", t.CoQuay, KVCTPCTVM.KVCTPTC.CoQuay);
                }

                if (t.TenKH != KVCTPCTVM.KVCTPTC.TenKH)
                {
                    temp += String.Format("- TenKH thay đổi: {0}->{1}", t.TenKH, KVCTPCTVM.KVCTPTC.TenKH);
                }

                if (t.DiaChi != KVCTPCTVM.KVCTPTC.DiaChi)
                {
                    temp += String.Format("- DiaChi thay đổi: {0}->{1}", t.DiaChi, KVCTPCTVM.KVCTPTC.DiaChi);
                }

                if (t.MatHang != KVCTPCTVM.KVCTPTC.MatHang)
                {
                    temp += String.Format("- MatHang thay đổi: {0}->{1}", t.MatHang, KVCTPCTVM.KVCTPTC.MatHang);
                }

                if (t.KyHieu != KVCTPCTVM.KVCTPTC.KyHieu)
                {
                    temp += String.Format("- MatHang thay đổi: {0}->{1}", t.KyHieu, KVCTPCTVM.KVCTPTC.KyHieu);
                }

                if (t.MauSoHD != KVCTPCTVM.KVCTPTC.MauSoHD)
                {
                    temp += String.Format("- MauSoHD thay đổi: {0}->{1}", t.MauSoHD, KVCTPCTVM.KVCTPTC.MauSoHD);
                }

                if (t.DieuChinh != KVCTPCTVM.KVCTPTC.DieuChinh)
                {
                    temp += String.Format("- DieuChinh thay đổi: {0}->{1}", t.DieuChinh, KVCTPCTVM.KVCTPTC.DieuChinh);
                }

                if (t.KC141 != KVCTPCTVM.KVCTPTC.KC141)
                {
                    temp += String.Format("- KC141 thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.KC141, KVCTPCTVM.KVCTPTC.KC141);
                }

                if (t.TamUng != KVCTPCTVM.KVCTPTC.TamUng)
                {
                    temp += String.Format("- TamUng thay đổi: {0}->{1}", t.TamUng, KVCTPCTVM.KVCTPTC.TamUng);
                }

                if (t.DienGiaiP != KVCTPCTVM.KVCTPTC.DienGiaiP)
                {
                    temp += String.Format("- DienGiaiP thay đổi: {0}->{1}", t.DienGiaiP, KVCTPCTVM.KVCTPTC.DienGiaiP);
                }

                if (t.HoaDonDT != KVCTPCTVM.KVCTPTC.HoaDonDT)
                {
                    temp += String.Format("- HoaDonDT thay đổi: {0}->{1}", t.HoaDonDT, KVCTPCTVM.KVCTPTC.HoaDonDT);
                }

                if (t.NguoiSua != KVCTPCTVM.KVCTPTC.NguoiSua)
                {
                    temp += String.Format("- NguoiSua thay đổi: {0}->{1}", t.NguoiSua, KVCTPCTVM.KVCTPTC.NguoiSua);
                }

                if (t.NgaySua != KVCTPCTVM.KVCTPTC.NgaySua)
                {
                    temp += String.Format("- NgaySua thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgaySua, KVCTPCTVM.KVCTPTC.NgaySua);
                }

                if (t.LinkHDDT != KVCTPCTVM.KVCTPTC.LinkHDDT)
                {
                    temp += String.Format("- LinkHDDT thay đổi: {0}->{1}", t.LinkHDDT, KVCTPCTVM.KVCTPTC.LinkHDDT);
                }

                if (t.MaTraCuu != KVCTPCTVM.KVCTPTC.MaTraCuu)
                {
                    temp += String.Format("- MaTraCuu thay đổi: {0}->{1}", t.MaTraCuu, KVCTPCTVM.KVCTPTC.MaTraCuu);
                }

                if (t.TkTruyCap != KVCTPCTVM.KVCTPTC.TkTruyCap)
                {
                    temp += String.Format("- TkTruyCap thay đổi: {0}->{1}", t.TkTruyCap, KVCTPCTVM.KVCTPTC.TkTruyCap);
                }

                if (t.Password != KVCTPCTVM.KVCTPTC.Password)
                {
                    temp += String.Format("- Password thay đổi: {0}->{1}", t.Password, KVCTPCTVM.KVCTPTC.Password);
                }

                // kiem tra thay doi
                if (temp.Length > 0)
                {
                    string log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật tour: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    KVCTPCTVM.KVCTPTC.LogFile = t.LogFile;
                }

                #endregion log file

                try
                {
                    await _kVCTPTCService.UpdateAsync(KVCTPCTVM.KVCTPTC);
                    SetAlert("Cập nhật thành công", "success");

                    return BackIndex(KVCTPCTVM.KVCTPTC.KVPTCId, KVCTPCTVM.Page);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");

                    return View(KVCTPCTVM);
                }
            }

            // not valid
            KVCTPCTVM.KVCTPTC = await _kVCTPTCService.GetById(id);
            KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVCTPCTVM.KVCTPTC.KVPTCId);
            KVCTPCTVM.Page = page;
            KVCTPCTVM.DmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
            KVCTPCTVM.Quays = _kVCTPTCService.GetAll_Quay_View();
            KVCTPCTVM.MatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
            KVCTPCTVM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();
            KVCTPCTVM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);

            Get_TkNo_TkCo();
            // tentk
            KVCTPCTVM.TenTkNo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(KVCTPCTVM.KVCTPTC.TKNo).TenTk;
            KVCTPCTVM.TenTkCo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(KVCTPCTVM.KVCTPTC.TKCo).TenTk;
            KVCTPCTVM.Dgiais = _kVCTPTCService.Get_DienGiai_By_TkNo_TkCo(KVCTPCTVM.KVCTPTC.TKNo, KVCTPCTVM.KVCTPTC.TKCo);

            return View(KVCTPCTVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (id == 0)
                return NotFound();
            var kVCTPCT = await _kVCTPTCService.GetById(id);
            if (kVCTPCT == null)
                return NotFound();
            try
            {
                // chi tiet nay keo ben cashier qua
                if (!string.IsNullOrEmpty(kVCTPCT.STT) && kVCTPCT.STT != "0") // kVCTPCT.STT: stt trong ntbill lay qua / "0":Xe
                {
                    var kVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(kVCTPCT.KVPTCId);
                    await _kVCTPTCService.DeleteRangeAsync(kVCTPTCs);

                    // Noptien
                    Ntbill ntbill = _kVCTPTCService.GetNtbillBySTT(kVCTPCT.STT);
                    Noptien noptien = await _kVCTPTCService.GetNopTienById(ntbill.Soct, user.Macn);
                    noptien.Phieuthu = "";
                    noptien.Phieuthucc = "";
                    await _kVCTPTCService.UpdateAsync_NopTien(noptien);
                }

                //// chi tiet nay keo ben qlxe qua
                //else if (!string.IsNullOrEmpty(kVCTPCT.KhoangMuc)) // kVCTPCT.KhoangMuc: KhoangMuc trong thuchi(qlxe) lay qua
                //{
                //    var kVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(kVCTPCT.KVPTCId);
                //    await _kVCTPTCService.DeleteRangeAsync(kVCTPTCs);

                //    // ThuChi
                //    var thuchis = _kVCTPTCService.GetThuChiXe_By_SoCT_KTTM(kVCTPCT.SoCT);
                //    foreach (var thuchi in thuchis)
                //    {
                //        thuchi.SoCtKttm = "";
                //        await _kVCTPTCService.UpdateAsync_ThuChiXe(thuchi);
                //    }
                //}
                else
                {
                    await _kVCTPTCService.DeleteAsync(kVCTPCT);
                }

                return Json(new
                {
                    status = true
                });
            }
            catch (Exception)
            {
                //SetAlert(ex.Message, "error");

                return Json(new
                {
                    status = false
                });
            }
        }

        public IActionResult BackIndex(Guid id, int page)
        {
            return RedirectToAction(nameof(Index), "Home", new { id = id, page });
        }

        public JsonResult GetKhachHangs_By_Code(string code)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            var khachHang = _kVCTPTCService.GetSuppliersByCode(code).FirstOrDefault();
            if (khachHang != null)
            {
                return Json(new
                {
                    status = true,
                    data = khachHang
                }); ;
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public IActionResult GetKhachHangs_HDVATOB_By_Code_ContextMenu(string code, int page = 1)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            code ??= "";
            //KVCTPCTVM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName(code, user.Macn);
            KVCTPCTVM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName_PagedList(code, user.Macn, page);
            KVCTPCTVM.MaKhText = code;
            return PartialView(KVCTPCTVM);
        }

        public IActionResult GetKhachHangs_HDVATOB_By_Code(string code, int page = 1)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            code ??= "";
            KVCTPCTVM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName_PagedList(code, user.Macn, page);
            KVCTPCTVM.MaKhText = code;
            return PartialView(KVCTPCTVM);
        }

        public IActionResult GetKhachHangs_By_Code_Edit(string code, int page = 1)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            code ??= "";
            KVCTPCTVM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName_PagedList(code, user.Macn, page);
            KVCTPCTVM.MaKhText = code;
            return PartialView(KVCTPCTVM);
        }

        public JsonResult Get_TenTk_By_Tk(string tk)
        {
            tk ??= "";
            var dmTk = _kVCTPTCService.GetAll_DmTk().Where(x => x.Tkhoan.Trim() == tk.Trim()).FirstOrDefault();
            return Json(new
            {
                data = dmTk == null ? "" : dmTk.TenTk.Trim()
            });
        }

        public JsonResult Get_DienGiai_By_TkNo_TkCo(string tkNo, string tkCo)
        {
            var listViewModels = _kVCTPTCService.Get_DienGiai_By_TkNo_TkCo(tkNo, tkCo);
            if (listViewModels.Count() > 0)
                return Json(new
                {
                    status = true,
                    data = listViewModels
                });
            else
                return Json(new
                {
                    status = false
                });
        }

        public JsonResult Get_DienGiai_By_TkNo(string tkNo)
        {
            var listViewModels = _kVCTPTCService.Get_DienGiai_By_TkNo(tkNo);
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

        public JsonResult TinhDsKhongVat(decimal vat, decimal soTien)
        {
            //var dsKhongVat = soTien - ((vat / 100) * soTien);
            var dsKhongVat = (soTien * 100) / vat;
            return Json(new
            {
                status = true,
                soTien = dsKhongVat.ToString()
            });
        }

        public JsonResult TinhThueGTGT(decimal tongTien, decimal chiPhi)
        {
            var thueGTGT = tongTien - chiPhi;
            return Json(new
            {
                status = true,
                thueGTGT = thueGTGT.ToString()
            });
        }

        /// <summary>
        ///"033-58" sẽ ra " SGT033-2021-00058"(ĐAY LÀ CODE ĐOÀN inbound")
        ///"084/58" sẽ ra " STN084-2021-00058"(ĐAY LÀ CODE ĐOÀN nội địa")
        ///" 58OB"  sẽ ra "STSTOB-2021-00058" (ĐAY LÀ CODE ĐOÀN outbound")
        /// </summary>
        /// <param name="param"></param>
        /// <returns>Sgtcode</returns>
        public JsonResult AutoSgtcode(string param)
        {
            string sgtcode = _kVCTPTCService.AutoSgtcode(param);
            if (!string.IsNullOrEmpty(sgtcode))
            {
                return Json(new
                {
                    status = true,
                    data = sgtcode
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public JsonResult CheckBaoCaoSo_Hoan(string baoCaoSo)
        {
            string baoCaoSoCheck = baoCaoSo.Trim().Substring(5, 1);
            if (baoCaoSoCheck == "H")
            {
                return Json(false);
            }
            return Json(true);
        }

        public JsonResult TkChoMaKh(string tk)
        {
            List<string> tkChoMaKh = new List<string>()
            {
                "1411", "1412", "1311110000", "1311120000", "1388110001", "1368000000", "331110", "338811"
            };
            if (tkChoMaKh.Exists(x => x == tk))
            {
                return Json(true); // ko cho makh bo trong'
            }

            return Json(false);
        }

        public async Task<IActionResult> ThuHo(string searchString, string searchFromDate, string searchToDate, int page = 1)
        {
            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate)) // mới load vao
            {
                ModelState.AddModelError("", "Từ ngày đến ngày không được để trống.");
                KVCTPCTVM.KVCTPTCs = new List<KVCTPTC>();
                return View(KVCTPCTVM);
            }

            DateTime fromDate = DateTime.Parse(searchFromDate);
            DateTime toDate = DateTime.Parse(searchToDate);
            if (fromDate > toDate)
            {
                ModelState.AddModelError("", "Từ ngày không được lớn hơn đến ngày.");
                return View(KVCTPCTVM);
            }

            KVCTPCTVM.ListThuHo = await _kVCTPTCService.ListThuHo(searchString, searchFromDate, searchToDate, page, user.Macn);

            return View(KVCTPCTVM);
        }

        [HttpPost]
        public async Task<IActionResult> ExportThuHo(string searchString, string searchFromDate, string searchToDate)
        {
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate))
            {
                return RedirectToAction(nameof(ThuHo), new { searchFromDate = searchFromDate, searchToDate = searchToDate });
            }

            // export
            KVCTPCTVM.KVCTPTCs = await _kVCTPTCService.ExportThuHo(searchString, searchFromDate, searchToDate, user.Macn);

            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Supplier");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 40;// diengiaip
            xlSheet.Column(2).Width = 20;//  sotiennt
            xlSheet.Column(3).Width = 10;// loaitien
            xlSheet.Column(4).Width = 10;// tygia
            xlSheet.Column(5).Width = 20;// sotien
            xlSheet.Column(6).Width = 10;// httc
            xlSheet.Column(7).Width = 30;// tkno
            xlSheet.Column(8).Width = 30;// makhno
            xlSheet.Column(9).Width = 30;// tkco

            xlSheet.Column(10).Width = 30;// makhco
            xlSheet.Column(11).Width = 20;//  vat
            xlSheet.Column(12).Width = 20;// dskhongvat
            xlSheet.Column(13).Width = 10;// loaihdgoc
            xlSheet.Column(14).Width = 30;// ngayctgoc
            xlSheet.Column(15).Width = 20;// soctgoc
            xlSheet.Column(16).Width = 20;// kyhieu
            xlSheet.Column(17).Width = 20;// mausohd
            xlSheet.Column(18).Width = 30;// sgtcode

            xlSheet.Column(19).Width = 20;// msthue
            xlSheet.Column(20).Width = 40;//  diengiai
            xlSheet.Column(21).Width = 40;// tenkh
            xlSheet.Column(22).Width = 40;// diachi
            xlSheet.Column(23).Width = 20;// mathang
            xlSheet.Column(24).Width = 10;// bophan
            xlSheet.Column(25).Width = 10;// noquay
            xlSheet.Column(26).Width = 10;// coquay
            xlSheet.Column(27).Width = 20;// soxe
            xlSheet.Column(28).Width = 20;// number
            xlSheet.Column(29).Width = 20;// soct
            xlSheet.Column(30).Width = 20;// ngayct
            xlSheet.Column(31).Width = 20;// tenphieu

            //xlSheet.Cells[2, 1].Value = "BẢNG KÊ 1368 TỪ " + (user.Macn == "STS" ? "STN" : "STS");
            //xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 18, FontStyle.Bold));
            //xlSheet.Cells[2, 1, 2, 8].Merge = true;

            //ExcelTool.setCenterAligment(1, 1, 2, 1, xlSheet);

            // Tạo header
            xlSheet.Cells[1, 1].Value = "diengiaip";
            xlSheet.Cells[1, 2].Value = "sotiennt";
            xlSheet.Cells[1, 3].Value = "loaitien";
            xlSheet.Cells[1, 4].Value = "tygia";
            xlSheet.Cells[1, 5].Value = "sotien";
            xlSheet.Cells[1, 6].Value = "httc";
            xlSheet.Cells[1, 7].Value = "tkno";
            xlSheet.Cells[1, 8].Value = "makhno";
            xlSheet.Cells[1, 9].Value = "tkco";
            xlSheet.Cells[1, 10].Value = "makhco";
            xlSheet.Cells[1, 11].Value = "vat";
            xlSheet.Cells[1, 12].Value = "dskhongvat";
            xlSheet.Cells[1, 13].Value = "loaihdgoc";
            xlSheet.Cells[1, 14].Value = "ngayctgoc";
            xlSheet.Cells[1, 15].Value = "soctgoc";
            xlSheet.Cells[1, 16].Value = "kyhieu";
            xlSheet.Cells[1, 17].Value = "mausohd";
            xlSheet.Cells[1, 18].Value = "sgtcode";
            xlSheet.Cells[1, 19].Value = "msthue";
            xlSheet.Cells[1, 20].Value = "diengiai";
            xlSheet.Cells[1, 21].Value = "tenkh";
            xlSheet.Cells[1, 22].Value = "diachi";
            xlSheet.Cells[1, 23].Value = "mathang";
            xlSheet.Cells[1, 24].Value = "bophan";
            xlSheet.Cells[1, 25].Value = "noquay";
            xlSheet.Cells[1, 26].Value = "coquay";
            xlSheet.Cells[1, 27].Value = "soxe";
            xlSheet.Cells[1, 28].Value = "number";
            xlSheet.Cells[1, 29].Value = "soct";
            xlSheet.Cells[1, 30].Value = "ngayct";
            xlSheet.Cells[1, 31].Value = "tenphieu";

            ExcelTool.setBorder(1, 1, 1, 31, xlSheet);
            ExcelTool.setCenterAligment(1, 1, 6, 31, xlSheet);
            xlSheet.Cells[1, 1, 1, 31].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[1, 1, 1, 31].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // canh giữa cột
            xlSheet.Cells[1, 1, 1, 31].Style.Fill.PatternType = ExcelFillStyle.DarkHorizontal;
            xlSheet.Cells[1, 1, 1, 31].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            // xlSheet.Column(1).Style.WrapText = true; // WrapText for column
            xlSheet.Column(20).Style.WrapText = true;
            xlSheet.Column(21).Style.WrapText = true;
            xlSheet.Column(22).Style.WrapText = true;

            // do du lieu tu table
            int dong = 2;

            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            if (KVCTPCTVM.KVCTPTCs.Count() > 0)
            {
                foreach (var item in KVCTPCTVM.KVCTPTCs)
                {
                    //xlSheet.Cells[dong, 1].Value = idem;
                    //ExcelTool.TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    ////xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 1].Value = item.DienGiaiP;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 2].Value = item.SoTienNT;// == true ? "Có" : "Không";
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 3].Value = item.LoaiTien;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 4].Value = item.TyGia;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 5].Value = item.SoTien;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 6].Value = item.HTTC;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 7].Value = item.TKNo;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    if (item.KVPTC.MFieu == "T")
                    {
                        xlSheet.Cells[dong, 7].Value = item.TKCo;
                        ExcelTool.TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 7].Value = "1311110000";
                        ExcelTool.TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }
                    xlSheet.Cells[dong, 8].Value = "";// item.MaKhNo;// == true ? "Kích hoạt" : "Khoá";
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 9].Value = item.TKCo;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    if (item.KVPTC.MFieu == "T")
                    {
                        xlSheet.Cells[dong, 9].Value = "1311110000";// item.TKCo;
                        ExcelTool.TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 9].Value = "1368000000";// item.TKCo;
                        ExcelTool.TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }
                    xlSheet.Cells[dong, 10].Value = "";// item.MaKhCo;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 10, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 11].Value = item.VAT;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 11, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 12].Value = item.DSKhongVAT;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 12, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 13].Value = item.LoaiHDGoc;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 13, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 14].Value = item.NgayCTGoc;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 14, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 15].Value = item.SoCTGoc;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 15, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 16].Value = item.KyHieu;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 16, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 17].Value = item.MauSoHD;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 17, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 18].Value = item.Sgtcode;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 18, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 19].Value = item.MsThue;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 19, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 20].Value = item.DienGiai;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 20, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 21].Value = item.TenKH;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 21, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 22].Value = item.DiaChi;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 22, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 23].Value = item.MatHang;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 23, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 24].Value = "";// item.BoPhan;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 24, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 25].Value = item.NoQuay;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 25, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 26].Value = "";// item.CoQuay;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 26, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 27].Value = item.SoXe;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 27, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 28].Value = item.Number;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 28, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 29].Value = item.SoCT;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 29, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 30].Value = item.KVPTC.NgayCT;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 30, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 31].Value = item.KVPTC.HoTen;
                    ExcelTool.TrSetCellBorder(xlSheet, dong, 31, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    //setBorder(5, 1, dong, 10, xlSheet);
                    ExcelTool.NumberFormat(dong, 2, dong, 5, xlSheet);

                    dong++;
                    idem++;
                }

                //xlSheet.Cells[dong, 5].Value = "Tổng cộng:";
                //xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                //xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (dong - 1) + ")";

                //ExcelTool.NumberFormat(dong, 2, dong, 4, xlSheet);
                ////setFontBold(dong, 1, dong, 10, 12, xlSheet);
                //ExcelTool.setBorder(dong, 1, dong, 8, xlSheet);
                ExcelTool.DateFormat(5, 14, dong, 14, xlSheet);
                ExcelTool.DateFormat(5, 30, dong, 30, xlSheet);

                //xlSheet.Cells[dong + 2, 1].Value = "Người lập bảng kê";
                //xlSheet.Cells[dong + 2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));
                //xlSheet.Cells[dong + 2, 4].Value = "Kế toán trưởng";
                //xlSheet.Cells[dong + 2, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));

                //setCenterAligment(dong + 2, 1, dong + 2, 4, xlSheet);
            }
            else
            {
                SetAlert("Không có chi tiết nào.", "warning");
                return RedirectToAction(nameof(ThuHo), new { searchFromDate = searchFromDate, searchToDate = searchToDate });
            }

            //dong++;
            //// Merger cot 4,5 ghi tổng tiền
            //setRightAligment(dong, 3, dong, 3, xlSheet);
            //xlSheet.Cells[dong, 1, dong, 2].Merge = true;
            //xlSheet.Cells[dong, 1].Value = "Tổng tiền: ";

            // Sum tổng tiền
            // xlSheet.Cells[dong, 5].Value = "TC:";
            //DateTimeFormat(6, 4, 6 + d.Count(), 4, xlSheet);
            // DateTimeFormat(6, 4, 9, 4, xlSheet);
            // setCenterAligment(6, 4, 9, 4, xlSheet);
            // xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (6 + d.Count() - 1) + ")";

            //setBorder(5, 1, 5 + d.Count() + 2, 10, xlSheet);

            //setFontBold(5, 1, 5, 8, 11, xlSheet);
            //setFontSize(6, 1, 6 + d.Count() + 2, 8, 11, xlSheet);
            // canh giua cot stt
            //setCenterAligment(6, 1, 6 + dong + 2, 1, xlSheet);
            // canh giua code chinhanh
            //setCenterAligment(6, 3, 6 + dong + 2, 3, xlSheet);
            // NumberFormat(6, 6, 6 + d.Count(), 6, xlSheet);
            // định dạng số cot, đơn giá, thành tiền tong cong
            // NumberFormat(6, 8, dong, 9, xlSheet);

            ExcelTool.setBorder(dong - 1, 1, dong - 1, 9, xlSheet);
            // setFontBold(dong, 5, dong, 6, 12, xlSheet);

            //xlSheet.View.FreezePanes(6, 20);

            //end du lieu

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                SetAlert("Good job!", "success");
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "BangKe1368_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception ex)
            {
                throw;
            }
            //return View(KVCTPCTVM);
        }
    }
}