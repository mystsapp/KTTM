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

            KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVCTPTC.KVPTCId = KVPTCId;
            KVCTPCTVM.KVCTPTC.TyGia = 1;
            KVCTPCTVM.KVCTPTC.LoaiTien = "VND";

            KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVPTCId);

            if (KVCTPCTVM.KVPTC.MFieu == "C")
            {
                KVCTPCTVM.KVCTPTC.TKCo = "1111000000";
            }
            else
            {
                KVCTPCTVM.KVCTPTC.TKNo = "1111000000";
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
            KVCTPCTVM.StrUrl = strUrl;
            KVCTPCTVM.Page = page; // page for redirect

            // btnThemdong + copy dong da click
            if (id_Dong_Da_Click > 0)
            {
                var dongCu = await _kVCTPTCService.GetById(id_Dong_Da_Click);
                KVCTPCTVM.KVCTPTC = dongCu;
            }

            return View(KVCTPCTVM);
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
        public async Task<IActionResult> ThemDong_ContextMenu(Guid KVPTCId, int page)
        {
            ViewSupplierCode viewSupplierCode = new Data.Models_DanhMucKT.ViewSupplierCode() { Code = "" };
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVCTPTC.KVPTCId = KVPTCId;
            KVCTPCTVM.KVCTPTC.TyGia = 1;
            KVCTPCTVM.KVCTPTC.LoaiTien = "VND";
            KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVPTCId);

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
                KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                Get_TkNo_TkCo();

                return View(KVCTPCTVM);
            }

            KVCTPCTVM.KVCTPTC.SoCT = KVCTPCTVM.KVPTC.SoCT;
            KVCTPCTVM.KVCTPTC.NguoiTao = user.Username;
            KVCTPCTVM.KVCTPTC.MaCn = user.Macn;
            KVCTPCTVM.KVCTPTC.NgayTao = DateTime.Now;
            KVCTPCTVM.KVCTPTC.MaKh = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.MaKhNo) ? KVCTPCTVM.KVCTPTC.MaKhCo : KVCTPCTVM.KVCTPTC.MaKhNo;
            KVCTPCTVM.KVCTPTC.MaKhNo = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.MaKhNo) ? "" : KVCTPCTVM.KVCTPTC.MaKhNo.ToUpper();

            // ghi log
            KVCTPCTVM.KVCTPTC.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _kVCTPTCService.Create(KVCTPCTVM.KVCTPTC);

                SetAlert("Thêm mới thành công.", "success");
                return BackIndex(KVPTCId, KVCTPCTVM.Page); // redirect to Home/Index/?soCT
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(KVCTPCTVM);
            }
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
                KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
                Get_TkNo_TkCo();

                return View(KVCTPCTVM);
            }

            KVCTPCTVM.KVCTPTC.SoCT = KVCTPCTVM.KVPTC.SoCT;
            KVCTPCTVM.KVCTPTC.VAT = KVCTPCTVM.KVCTPTC.VAT ?? 0;
            KVCTPCTVM.KVCTPTC.DSKhongVAT = KVCTPCTVM.KVCTPTC.DSKhongVAT ?? 0;
            KVCTPCTVM.KVCTPTC.NguoiTao = user.Username;
            KVCTPCTVM.KVCTPTC.MaCn = user.Macn;
            KVCTPCTVM.KVCTPTC.NgayTao = DateTime.Now;
            KVCTPCTVM.KVCTPTC.MaKh = string.IsNullOrEmpty(KVCTPCTVM.KVCTPTC.MaKhNo) ? KVCTPCTVM.KVCTPTC.MaKhCo : KVCTPCTVM.KVCTPTC.MaKhNo;
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
        public async Task<IActionResult> LayDataCashierPartial_Post()
        {
            var kVPTCId = KVCTPCTVM.KVPTC.Id;
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                return View(KVCTPCTVM);
            }

            // data tu cashier
            var kVCTPTCs = _kVCTPTCService.GetKVCTPTCs(KVCTPCTVM.LayDataCashierModel.BaoCaoSo.Trim(),
                kVPTCId, KVCTPCTVM.KVPTC.SoCT, user.Username, user.Macn, KVCTPCTVM.KVPTC.MFieu,
                KVCTPCTVM.LayDataCashierModel.Tk.Trim(), KVCTPCTVM.LayDataCashierModel.TienMat,
                KVCTPCTVM.LayDataCashierModel.TTThe);
            // ghi log ben service

            try
            {
                await _kVCTPTCService.CreateRange(kVCTPTCs);

                // save to cashier
                var kVPCT = await _kVPTCService.GetByGuidIdAsync(kVPTCId);
                var noptien = await _unitOfWork.nopTienRepository.GetById(KVCTPCTVM.LayDataCashierModel.BaoCaoSo.Trim(), user.Macn);
                noptien.Phieuthu = KVCTPCTVM.KVPTC.SoCT;
                noptien.Ngaypt = kVPCT.NgayCT;
                //noptien.Ghichu = kVPCT.ghichu; ??
                await _kVCTPTCService.UpdateAsync_NopTien(noptien);

                // TTThe, phieu thu, cap tk(no = 1111000000, co = 1311)
                // -> tu tao phieu chi cap tk(no = 1131, co =1311110000)
                if (KVCTPCTVM.LayDataCashierModel.TTThe &&
                    kVPCT.MFieu == "T" && KVCTPCTVM.LayDataCashierModel.Tk == "1311110000")
                {
                    KVCTPCTVM.KVPTC = new KVPTC();
                    // tao phieuchi
                    KVCTPCTVM.KVPTC.Create = DateTime.Now;
                    KVCTPCTVM.KVPTC.LapPhieu = user.Username;
                    KVCTPCTVM.KVPTC.MaCn = user.Macn;
                    KVCTPCTVM.KVPTC.MFieu = "C";
                    KVCTPCTVM.KVPTC.NgayCT = DateTime.Now;
                    KVCTPCTVM.KVPTC.DonVi = "CÔNG TY TNHH MỘT THÀNH VIÊN DỊCH VỤ LỮ HÀNH SAIGONTOURIST";
                    KVCTPCTVM.KVPTC.NgoaiTe = kVPCT.NgoaiTe;
                    KVCTPCTVM.KVPTC.HoTen = "VCB(CHAU)"; // thao
                    KVCTPCTVM.KVPTC.Phong = kVPCT.Phong;

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
                        return View(KVCTPCTVM);
                    }
                    // tao phieuchi

                    // tao ct phieuchi
                    List<KVCTPTC> kVCTPTCs1 = new List<KVCTPTC>();
                    List<KVCTPTC> kVCTPTCs2 = kVCTPTCs.ToList();

                    foreach (var item in kVCTPTCs2) // dao cap tk
                    {
                        item.Id = 0;
                        item.TKNo = "1131";
                        item.TKCo = "1111000000";
                        item.KVPTCId = kVPTC1.Id;
                        item.SoCT = kVPTC1.SoCT;
                        item.MaKh = ""; // thao
                        item.BoPhan = "CH"; // thao
                        item.NoQuay = ""; // thao
                        item.CoQuay = ""; // thao
                        kVCTPTCs1.Add(item);
                    }
                    if (kVCTPTCs1.Count > 0)
                    {
                        await _kVCTPTCService.CreateRange(kVCTPTCs1);
                    }
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
                return View(KVCTPCTVM);
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

            KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            KVCTPCTVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(KVCTPCTVM.KVCTPTC.KVPTCId);
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
            KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
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
            if (id == 0)
                return NotFound();
            var kVCTPCT = await _kVCTPTCService.GetById(id);
            if (kVCTPCT == null)
                return NotFound();
            try
            {
                await _kVCTPTCService.DeleteAsync(kVCTPCT);

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

        public IActionResult BackIndex(Guid id, int page)
        {
            return RedirectToAction(nameof(Index), "Home", new { id = id, page });
        }

        public JsonResult GetKhachHangs_By_Code(string code)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            var supplier = _kVCTPTCService.GetSuppliersByCode(code, user.Macn).FirstOrDefault();
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
            KVCTPCTVM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName(code, user.Macn);
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
    }
}