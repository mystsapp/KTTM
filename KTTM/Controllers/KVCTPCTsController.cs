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

namespace KTTM.Controllers
{
    public class KVCTPCTsController : BaseController
    {
        private readonly IKVCTPCTService _kVCTPCTService;
        private readonly IKVPCTService _kVPCTService;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public KVCTPCTViewModel KVCTPCTVM { get; set; }
        public KVCTPCTsController(IKVCTPCTService kVCTPCTService, IKVPCTService kVPCTService, IUnitOfWork unitOfWork)
        {
            _kVCTPCTService = kVCTPCTService;
            _kVPCTService = kVPCTService;
            _unitOfWork = unitOfWork;
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

        public async Task<IActionResult> KVCTPCTPartial(string soCT, int page)
        {
            // KVCTPCT
            KVCTPCTVM.Page = page;
            KVCTPCTVM.KVCTPCTs = await _kVCTPCTService.List_KVCTPCT_By_SoCT(soCT);
            KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(soCT);

            return PartialView(KVCTPCTVM);
        }

        public async Task<IActionResult> ThemDong(string soCT, string strUrl, int page, long id_Dong_Da_Click)
        {
            if (!ModelState.IsValid) // check id_Dong_Da_Click valid (da gang' = 0 trong home/index)
            {

                return View();
            }

            Data.Models_HDVATOB.Supplier supplier = new Data.Models_HDVATOB.Supplier() { Code = "" };
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            KVCTPCTVM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVCTPCT.KVPCTId = soCT;
            KVCTPCTVM.KVCTPCT.TyGia = 1;
            KVCTPCTVM.KVCTPCT.LoaiTien = "VND";
            if (KVCTPCTVM.KVCTPCT.KVPCTId.Contains("C"))
            {
                KVCTPCTVM.KVCTPCT.TKCo = "1111000000";
            }
            else
            {
                KVCTPCTVM.KVCTPCT.TKNo = "1111000000";
            }
            KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(soCT);
            
            var viewDmHttcs = _kVCTPCTService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            KVCTPCTVM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            KVCTPCTVM.Quays = _kVCTPCTService.GetAll_Quay_View();
            var viewMatHangs = _kVCTPCTService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            KVCTPCTVM.MatHangs = viewMatHangs;
            KVCTPCTVM.PhongBans = _kVCTPCTService.GetAll_PhongBans_View();
            KVCTPCTVM.LoaiHDGocs = _kVCTPCTService.LoaiHDGocs();
            KVCTPCTVM.StrUrl = strUrl;
            KVCTPCTVM.Page = page; // page for redirect

            // R + btnThemdong
            if (id_Dong_Da_Click > 0)
            {
                var dongCu = await _kVCTPCTService.GetById(id_Dong_Da_Click);
                KVCTPCTVM.KVCTPCT = dongCu;
            }

            return View(KVCTPCTVM);
        }

        private void Get_TkNo_TkCo()
        {

            DmTk dmTk = new DmTk() { Tkhoan = "" };
            var dmTks_TienMat = _kVCTPCTService.GetAll_DmTk_TienMat().ToList();
            var dmTks_TaiKhoan = _kVCTPCTService.GetAll_DmTk_TaiKhoan().ToList();
            dmTks_TienMat.Insert(0, dmTk);
            dmTks_TaiKhoan.Insert(0, dmTk);
            if (KVCTPCTVM.KVPCT.MFieu == "T")
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
        public async Task<IActionResult> ThemDong_ContextMenu(string soCT, int page)
        {
            ViewSupplierCode viewSupplierCode = new Data.Models_DanhMucKT.ViewSupplierCode() { Code = "" };
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            KVCTPCTVM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVCTPCT.KVPCTId = soCT;
            KVCTPCTVM.KVCTPCT.TyGia = 1;
            KVCTPCTVM.KVCTPCT.LoaiTien = "VND";
            KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(soCT);
            
            var viewDmHttcs = _kVCTPCTService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            KVCTPCTVM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            KVCTPCTVM.Quays = _kVCTPCTService.GetAll_Quay_View();

            var viewMatHangs = _kVCTPCTService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            KVCTPCTVM.MatHangs = viewMatHangs;
            KVCTPCTVM.PhongBans = _kVCTPCTService.GetAll_PhongBans_View();
            KVCTPCTVM.Page = page;
            KVCTPCTVM.LoaiHDGocs = _kVCTPCTService.LoaiHDGocs();

            return View(KVCTPCTVM);
        }

        [HttpPost, ActionName("ThemDong_ContextMenu")]
        public async Task<IActionResult> ThemDong_ContextMenu_Post(string soCT, int page)
        {
            //var soCT = KVCTPCTVM.KVCTPCT.KVPCTId;
            
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {

                // not valid
                KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(KVCTPCTVM.KVCTPCT.KVPCTId);
                KVCTPCTVM.Page = page;
                KVCTPCTVM.DmHttcs = _kVCTPCTService.GetAll_DmHttc_View().ToList();
                KVCTPCTVM.Quays = _kVCTPCTService.GetAll_Quay_View();
                KVCTPCTVM.MatHangs = _kVCTPCTService.GetAll_MatHangs_View().ToList();
                KVCTPCTVM.PhongBans = _kVCTPCTService.GetAll_PhongBans_View();
                KVCTPCTVM.LoaiHDGocs = _kVCTPCTService.LoaiHDGocs();
                KVCTPCTVM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                Get_TkNo_TkCo();
                
                return View(KVCTPCTVM);
            }

            KVCTPCTVM.KVCTPCT.NguoiTao = user.Username;
            KVCTPCTVM.KVCTPCT.NgayTao = DateTime.Now;
            KVCTPCTVM.KVCTPCT.MaKh = string.IsNullOrEmpty(KVCTPCTVM.KVCTPCT.MaKhNo) ? KVCTPCTVM.KVCTPCT.MaKhCo : KVCTPCTVM.KVCTPCT.MaKhNo;
            KVCTPCTVM.KVCTPCT.MaKhNo = string.IsNullOrEmpty(KVCTPCTVM.KVCTPCT.MaKhNo) ? "" : KVCTPCTVM.KVCTPCT.MaKhNo.ToUpper();

            // ghi log
            KVCTPCTVM.KVCTPCT.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _kVCTPCTService.Create(KVCTPCTVM.KVCTPCT);

                SetAlert("Thêm mới thành công.", "success");
                return BackIndex(soCT, KVCTPCTVM.Page); // redirect to Home/Index/?soCT
            }
            catch (Exception ex)
            {

                SetAlert(ex.Message, "error");
                return View(KVCTPCTVM);
            }

        }

        [HttpPost, ActionName("ThemDong")]
        public async Task<IActionResult> ThemDong_Post(string soCT, int page)
        {
            // var soCT = KVCTPCTVM.KVCTPCT.KVPCTId;
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {

                // not valid
                KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(KVCTPCTVM.KVCTPCT.KVPCTId);
                KVCTPCTVM.Page = page;
                KVCTPCTVM.DmHttcs = _kVCTPCTService.GetAll_DmHttc_View().ToList();
                KVCTPCTVM.Quays = _kVCTPCTService.GetAll_Quay_View();
                KVCTPCTVM.MatHangs = _kVCTPCTService.GetAll_MatHangs_View().ToList();
                KVCTPCTVM.PhongBans = _kVCTPCTService.GetAll_PhongBans_View();
                KVCTPCTVM.LoaiHDGocs = _kVCTPCTService.LoaiHDGocs();
                KVCTPCTVM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                Get_TkNo_TkCo();
               
                return View(KVCTPCTVM);
            }

            KVCTPCTVM.KVCTPCT.NguoiTao = user.Username;
            KVCTPCTVM.KVCTPCT.NgayTao = DateTime.Now;
            KVCTPCTVM.KVCTPCT.MaKh = string.IsNullOrEmpty(KVCTPCTVM.KVCTPCT.MaKhNo) ? KVCTPCTVM.KVCTPCT.MaKhCo : KVCTPCTVM.KVCTPCT.MaKhNo;
            KVCTPCTVM.KVCTPCT.MaKhNo = string.IsNullOrEmpty(KVCTPCTVM.KVCTPCT.MaKhNo) ? "" : KVCTPCTVM.KVCTPCT.MaKhNo.ToUpper();
            KVCTPCTVM.KVCTPCT.MaKhCo = string.IsNullOrEmpty(KVCTPCTVM.KVCTPCT.MaKhCo) ? "" : KVCTPCTVM.KVCTPCT.MaKhCo.ToUpper();

            // ghi log
            KVCTPCTVM.KVCTPCT.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _kVCTPCTService.Create(KVCTPCTVM.KVCTPCT);

                SetAlert("Thêm mới thành công.", "success");
                return BackIndex(soCT, KVCTPCTVM.Page); // redirect to Home/Index/?soCT
            }
            catch (Exception ex)
            {

                SetAlert(ex.Message, "error");
                return View(KVCTPCTVM);
            }

        }

        //-----------LayDataCashierPartial------------
        public async Task<IActionResult> LayDataCashierPartial(string id, string strUrl, int page)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            KVCTPCTVM.StrUrl = strUrl;
            KVCTPCTVM.Page = page;
            KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(id);
            KVCTPCTVM.DmTks_Cashier = _kVCTPCTService.GetAll_DmTk_Cashier();

            return PartialView(KVCTPCTVM);
        }

        [HttpPost, ActionName("LayDataCashierPartial")]
        public async Task<IActionResult> LayDataCashierPartial_Post()
        {
            var soCT = KVCTPCTVM.KVPCT.SoCT;
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {

                return View(KVCTPCTVM);
            }

            // data tu cashier
            var kVCTPCTs = _kVCTPCTService.GetKVCTPCTs(KVCTPCTVM.LayDataCashierModel.BaoCaoSo, soCT, user.Username, user.Macn, KVCTPCTVM.KVPCT.MFieu, KVCTPCTVM.LayDataCashierModel.Tk.Trim());
            // ghi log ben service

            try
            {
                await _kVCTPCTService.CreateRange(kVCTPCTs);

                // save to cashier
                var kVPCT = await _kVPCTService.GetBySoCT(soCT);
                var noptien = await _unitOfWork.nopTienRepository.GetById(KVCTPCTVM.LayDataCashierModel.BaoCaoSo, user.Macn);
                noptien.Phieuthu = soCT;
                noptien.Ngaypt = kVPCT.NgayCT;
                //noptien.Ghichu = kVPCT.ghichu; ??
                await _kVCTPCTService.UpdateAsync_NopTien(noptien);

                SetAlert("Thêm mới thành công.", "success");
                return BackIndex(soCT, KVCTPCTVM.Page); // redirect to Home/Index/?soCT
            }
            catch (Exception ex)
            {

                SetAlert(ex.Message, "error");
                return View(KVCTPCTVM);
            }

        }

        // Edit_KVCTPCT
        public async Task<IActionResult> Edit_KVCTPCT(long id, int page)
        {
            KVCTPCTVM.Page = page;
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (id == 0)
            {
                ViewBag.ErrorMessage = "Chi tiết phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            KVCTPCTVM.KVCTPCT = await _kVCTPCTService.GetById(id);

            // tentk
            KVCTPCTVM.TenTkNo = _kVCTPCTService.Get_DmTk_By_TaiKhoan(KVCTPCTVM.KVCTPCT.TKNo).TenTk;
            KVCTPCTVM.TenTkCo = _kVCTPCTService.Get_DmTk_By_TaiKhoan(KVCTPCTVM.KVCTPCT.TKCo).TenTk;
            KVCTPCTVM.Dgiais = _kVCTPCTService.Get_DienGiai_By_TkNo_TkCo(KVCTPCTVM.KVCTPCT.TKNo, KVCTPCTVM.KVCTPCT.TKCo);

            if (KVCTPCTVM.KVCTPCT == null)
            {
                ViewBag.ErrorMessage = "Chi tiết phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            KVCTPCTVM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(KVCTPCTVM.KVCTPCT.KVPCTId);
            var viewDmHttcs = _kVCTPCTService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            KVCTPCTVM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            KVCTPCTVM.Quays = _kVCTPCTService.GetAll_Quay_View();

            var viewMatHangs = _kVCTPCTService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            KVCTPCTVM.MatHangs = viewMatHangs;
            KVCTPCTVM.PhongBans = _kVCTPCTService.GetAll_PhongBans_View();
            KVCTPCTVM.LoaiHDGocs = _kVCTPCTService.LoaiHDGocs();

            return View(KVCTPCTVM);
        }


        [HttpPost, ActionName("Edit_KVCTPCT")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_KVCTPCT_Post(long id, int page)
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
                KVCTPCTVM.KVCTPCT.NgaySua = DateTime.Now;
                KVCTPCTVM.KVCTPCT.NguoiSua = user.Username;

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _kVCTPCTService.GetBySoCTAsNoTracking(id);

                if (t.HTTC != KVCTPCTVM.KVCTPCT.HTTC)
                {
                    temp += String.Format("- HTTC thay đổi: {0}->{1}", t.HTTC, KVCTPCTVM.KVCTPCT.HTTC);
                }

                if (t.DienGiai != KVCTPCTVM.KVCTPCT.DienGiai)
                {
                    temp += String.Format("- DienGiai thay đổi: {0}->{1}", t.DienGiai, KVCTPCTVM.KVCTPCT.DienGiai);
                }

                if (t.TKNo != KVCTPCTVM.KVCTPCT.TKNo)
                {
                    temp += String.Format("- TKNo thay đổi: {0}->{1}", t.TKNo, KVCTPCTVM.KVCTPCT.TKNo);
                }

                if (t.TKCo != KVCTPCTVM.KVCTPCT.TKCo)
                {
                    temp += String.Format("- TKCo thay đổi: {0}->{1}", t.TKCo, KVCTPCTVM.KVCTPCT.TKCo);
                }

                if (t.Sgtcode != KVCTPCTVM.KVCTPCT.Sgtcode)
                {
                    temp += String.Format("- Sgtcode thay đổi: {0}->{1}", t.Sgtcode, KVCTPCTVM.KVCTPCT.Sgtcode);
                }

                if (t.MaKhNo != KVCTPCTVM.KVCTPCT.MaKhNo)
                {
                    temp += String.Format("- MaKhNo thay đổi: {0}->{1}", t.MaKhNo, KVCTPCTVM.KVCTPCT.MaKhNo);
                }

                if (t.MaKhCo != KVCTPCTVM.KVCTPCT.MaKhCo)
                {
                    temp += String.Format("- MaKhCo thay đổi: {0}->{1}", t.MaKhCo, KVCTPCTVM.KVCTPCT.MaKhCo);
                }

                if (t.SoTienNT != KVCTPCTVM.KVCTPCT.SoTienNT)
                {
                    temp += String.Format("- SoTienNT thay đổi: {0:N0}->{1:N0}", t.SoTienNT, KVCTPCTVM.KVCTPCT.SoTienNT);
                }

                if (t.LoaiTien != KVCTPCTVM.KVCTPCT.LoaiTien)
                {
                    temp += String.Format("- LoaiTien thay đổi: {0}->{1}", t.LoaiTien, KVCTPCTVM.KVCTPCT.LoaiTien);
                }

                if (t.TyGia != KVCTPCTVM.KVCTPCT.TyGia)
                {
                    temp += String.Format("- TyGia thay đổi: {0:N0}->{1:N0}", t.TyGia, KVCTPCTVM.KVCTPCT.TyGia);
                }

                if (t.SoTien != KVCTPCTVM.KVCTPCT.SoTien)
                {
                    temp += String.Format("- SoTien thay đổi: {0:N0}->{1:N0}", t.SoTien, KVCTPCTVM.KVCTPCT.SoTien);
                }

                if (t.MaKh != KVCTPCTVM.KVCTPCT.MaKh)
                {
                    temp += String.Format("- MaKh thay đổi: {0}->{1}", t.MaKh, KVCTPCTVM.KVCTPCT.MaKh);
                }

                if (t.KhoangMuc != KVCTPCTVM.KVCTPCT.KhoangMuc)
                {
                    temp += String.Format("- KhoangMuc thay đổi: {0}->{1}", t.KhoangMuc, KVCTPCTVM.KVCTPCT.KhoangMuc);
                }

                if (t.HTTT != KVCTPCTVM.KVCTPCT.HTTT)
                {
                    temp += String.Format("- HTTT thay đổi: {0}->{1}", t.HTTT, KVCTPCTVM.KVCTPCT.HTTT);
                }

                if (t.CardNumber != KVCTPCTVM.KVCTPCT.CardNumber)
                {
                    temp += String.Format("- CardNumber thay đổi: {0}->{1}", t.CardNumber, KVCTPCTVM.KVCTPCT.CardNumber);
                }

                if (t.SalesSlip != KVCTPCTVM.KVCTPCT.SalesSlip)
                {
                    temp += String.Format("- SalesSlip thay đổi: {0}->{1}", t.SalesSlip, KVCTPCTVM.KVCTPCT.SalesSlip);
                }

                if (t.SoXe != KVCTPCTVM.KVCTPCT.SoXe)
                {
                    temp += String.Format("- SoXe thay đổi: {0}->{1}", t.SoXe, KVCTPCTVM.KVCTPCT.SoXe);
                }

                if (t.MsThue != KVCTPCTVM.KVCTPCT.MsThue)
                {
                    temp += String.Format("- MsThue thay đổi: {0}->{1}", t.MsThue, KVCTPCTVM.KVCTPCT.MsThue);
                }

                if (t.LoaiHDGoc != KVCTPCTVM.KVCTPCT.LoaiHDGoc)
                {
                    temp += String.Format("- LoaiHDGoc thay đổi: {0}->{1}", t.LoaiHDGoc, KVCTPCTVM.KVCTPCT.LoaiHDGoc);
                }

                if (t.SoCTGoc != KVCTPCTVM.KVCTPCT.SoCTGoc)
                {
                    temp += String.Format("- SoCTGoc thay đổi: {0}->{1}", t.SoCTGoc, KVCTPCTVM.KVCTPCT.SoCTGoc);
                }

                if (t.NgayCTGoc != KVCTPCTVM.KVCTPCT.NgayCTGoc)
                {
                    temp += String.Format("- NgayCTGoc thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayCTGoc, KVCTPCTVM.KVCTPCT.NgayCTGoc);
                }

                if (t.VAT != KVCTPCTVM.KVCTPCT.VAT)
                {
                    temp += String.Format("- VAT thay đổi: {0:N0}->{1:N0}", t.NgayCTGoc, KVCTPCTVM.KVCTPCT.VAT);
                }

                if (t.DSKhongVAT != KVCTPCTVM.KVCTPCT.DSKhongVAT)
                {
                    temp += String.Format("- DSKhongVAT thay đổi: {0:N0}->{1:N0}", t.DSKhongVAT, KVCTPCTVM.KVCTPCT.DSKhongVAT);
                }

                if (t.BoPhan != KVCTPCTVM.KVCTPCT.BoPhan)
                {
                    temp += String.Format("- DSKhongVAT thay đổi: {0}->{1}", t.BoPhan, KVCTPCTVM.KVCTPCT.BoPhan);
                }

                if (t.STT != KVCTPCTVM.KVCTPCT.STT)
                {
                    temp += String.Format("- STT thay đổi: {0}->{1}", t.STT, KVCTPCTVM.KVCTPCT.STT);
                }

                if (t.NoQuay != KVCTPCTVM.KVCTPCT.NoQuay)
                {
                    temp += String.Format("- NoQuay thay đổi: {0}->{1}", t.NoQuay, KVCTPCTVM.KVCTPCT.NoQuay);
                }

                if (t.CoQuay != KVCTPCTVM.KVCTPCT.CoQuay)
                {
                    temp += String.Format("- CoQuay thay đổi: {0}->{1}", t.CoQuay, KVCTPCTVM.KVCTPCT.CoQuay);
                }

                if (t.TenKH != KVCTPCTVM.KVCTPCT.TenKH)
                {
                    temp += String.Format("- TenKH thay đổi: {0}->{1}", t.TenKH, KVCTPCTVM.KVCTPCT.TenKH);
                }

                if (t.DiaChi != KVCTPCTVM.KVCTPCT.DiaChi)
                {
                    temp += String.Format("- DiaChi thay đổi: {0}->{1}", t.DiaChi, KVCTPCTVM.KVCTPCT.DiaChi);
                }

                if (t.MatHang != KVCTPCTVM.KVCTPCT.MatHang)
                {
                    temp += String.Format("- MatHang thay đổi: {0}->{1}", t.MatHang, KVCTPCTVM.KVCTPCT.MatHang);
                }

                if (t.KyHieu != KVCTPCTVM.KVCTPCT.KyHieu)
                {
                    temp += String.Format("- MatHang thay đổi: {0}->{1}", t.KyHieu, KVCTPCTVM.KVCTPCT.KyHieu);
                }

                if (t.MauSoHD != KVCTPCTVM.KVCTPCT.MauSoHD)
                {
                    temp += String.Format("- MauSoHD thay đổi: {0}->{1}", t.MauSoHD, KVCTPCTVM.KVCTPCT.MauSoHD);
                }

                if (t.DieuChinh != KVCTPCTVM.KVCTPCT.DieuChinh)
                {
                    temp += String.Format("- DieuChinh thay đổi: {0}->{1}", t.DieuChinh, KVCTPCTVM.KVCTPCT.DieuChinh);
                }

                if (t.KC141 != KVCTPCTVM.KVCTPCT.KC141)
                {
                    temp += String.Format("- KC141 thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.KC141, KVCTPCTVM.KVCTPCT.KC141);
                }

                if (t.TamUng != KVCTPCTVM.KVCTPCT.TamUng)
                {
                    temp += String.Format("- TamUng thay đổi: {0}->{1}", t.TamUng, KVCTPCTVM.KVCTPCT.TamUng);
                }

                if (t.DienGiaiP != KVCTPCTVM.KVCTPCT.DienGiaiP)
                {
                    temp += String.Format("- DienGiaiP thay đổi: {0}->{1}", t.DienGiaiP, KVCTPCTVM.KVCTPCT.DienGiaiP);
                }

                if (t.HoaDonDT != KVCTPCTVM.KVCTPCT.HoaDonDT)
                {
                    temp += String.Format("- HoaDonDT thay đổi: {0}->{1}", t.HoaDonDT, KVCTPCTVM.KVCTPCT.HoaDonDT);
                }

                if (t.NguoiSua != KVCTPCTVM.KVCTPCT.NguoiSua)
                {
                    temp += String.Format("- NguoiSua thay đổi: {0}->{1}", t.NguoiSua, KVCTPCTVM.KVCTPCT.NguoiSua);
                }

                if (t.NgaySua != KVCTPCTVM.KVCTPCT.NgaySua)
                {
                    temp += String.Format("- NgaySua thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgaySua, KVCTPCTVM.KVCTPCT.NgaySua);
                }

                if (t.LinkHDDT != KVCTPCTVM.KVCTPCT.LinkHDDT)
                {
                    temp += String.Format("- LinkHDDT thay đổi: {0}->{1}", t.LinkHDDT, KVCTPCTVM.KVCTPCT.LinkHDDT);
                }

                if (t.MaTraCuu != KVCTPCTVM.KVCTPCT.MaTraCuu)
                {
                    temp += String.Format("- MaTraCuu thay đổi: {0}->{1}", t.MaTraCuu, KVCTPCTVM.KVCTPCT.MaTraCuu);
                }

                if (t.TkTruyCap != KVCTPCTVM.KVCTPCT.TkTruyCap)
                {
                    temp += String.Format("- TkTruyCap thay đổi: {0}->{1}", t.TkTruyCap, KVCTPCTVM.KVCTPCT.TkTruyCap);
                }

                if (t.Password != KVCTPCTVM.KVCTPCT.Password)
                {
                    temp += String.Format("- Password thay đổi: {0}->{1}", t.Password, KVCTPCTVM.KVCTPCT.Password);
                }

                // kiem tra thay doi
                if (temp.Length > 0)
                {

                    string log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật tour: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    KVCTPCTVM.KVCTPCT.LogFile = t.LogFile;
                }
                #endregion
                try
                {
                    await _kVCTPCTService.UpdateAsync(KVCTPCTVM.KVCTPCT);
                    SetAlert("Cập nhật thành công", "success");

                    return BackIndex(KVCTPCTVM.KVCTPCT.KVPCTId, KVCTPCTVM.Page);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");

                    return View(KVCTPCTVM);
                }
            }

            // not valid
            KVCTPCTVM.KVCTPCT = await _kVCTPCTService.GetById(id);
            KVCTPCTVM.KVPCT = await _kVPCTService.GetBySoCT(KVCTPCTVM.KVCTPCT.KVPCTId);
            KVCTPCTVM.Page = page;
            KVCTPCTVM.DmHttcs = _kVCTPCTService.GetAll_DmHttc_View().ToList();
            KVCTPCTVM.Quays = _kVCTPCTService.GetAll_Quay_View();
            KVCTPCTVM.MatHangs = _kVCTPCTService.GetAll_MatHangs_View().ToList();
            KVCTPCTVM.PhongBans = _kVCTPCTService.GetAll_PhongBans_View();
            KVCTPCTVM.LoaiHDGocs = _kVCTPCTService.LoaiHDGocs();
            KVCTPCTVM.Ngoaites = _kVCTPCTService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            Get_TkNo_TkCo();
            // tentk
            KVCTPCTVM.TenTkNo = _kVCTPCTService.Get_DmTk_By_TaiKhoan(KVCTPCTVM.KVCTPCT.TKNo).TenTk;
            KVCTPCTVM.TenTkCo = _kVCTPCTService.Get_DmTk_By_TaiKhoan(KVCTPCTVM.KVCTPCT.TKCo).TenTk;
            KVCTPCTVM.Dgiais = _kVCTPCTService.Get_DienGiai_By_TkNo_TkCo(KVCTPCTVM.KVCTPCT.TKNo, KVCTPCTVM.KVCTPCT.TKCo);


            return View(KVCTPCTVM);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (id == 0)
                return NotFound();
            var kVCTPCT = await _kVCTPCTService.GetById(id);
            if (kVCTPCT == null)
                return NotFound();
            try
            {
                await _kVCTPCTService.DeleteAsync(kVCTPCT);

                //SetAlert("Xóa thành công.", "success");
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


        public IActionResult BackIndex(string soCT, int page)
        {
            return RedirectToAction(nameof(Index), "Home", new { soCT = soCT, page });
        }

        public JsonResult GetKhachHangs_By_Code(string code)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            var supplier = _kVCTPCTService.GetSuppliersByCode(code, user.Macn).FirstOrDefault();
            if (supplier != null)
            {
                return Json(new
                {
                    status = true,
                    data = supplier
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
        public IActionResult GetKhachHangs_HDVATOB_By_Code(string code)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            code ??= "";
            KVCTPCTVM.KhachHangs_HDVATOB = _kVCTPCTService.GetSuppliersByCode(code, user.Macn);
            return PartialView(KVCTPCTVM);
        }
        public JsonResult Get_TenTk_By_Tk(string tk)
        {
            tk ??= "";
            var dmTk = _kVCTPCTService.GetAll_DmTk().Where(x => x.Tkhoan.Trim() == tk.Trim()).FirstOrDefault();
            return Json(new
            {
                data = dmTk == null ? "" : dmTk.TenTk.Trim()
            });
        }
        public JsonResult Get_DienGiai_By_TkNo_TkCo(string tkNo, string tkCo)
        {
            var listViewModels = _kVCTPCTService.Get_DienGiai_By_TkNo_TkCo(tkNo, tkCo);
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
            var listViewModels = _kVCTPCTService.Get_DienGiai_By_TkNo(tkNo);
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
            var dsKhongVat = soTien - ((vat / 100) * soTien);
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
            string sgtcode = _kVCTPCTService.AutoSgtcode(param);
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
    }
}
