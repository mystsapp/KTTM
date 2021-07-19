﻿using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Data.Utilities;
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
        private readonly IKVPCTService _kVPCTService;

        [BindProperty]
        public TT621ViewModel TT621VM { get; set; }

        public TT621sController(IKVCTPCTService kVCTPCTService, ITamUngService tamUngService, ITT621Service tT621Service, IKVPCTService kVPCTService)
        {
            TT621VM = new TT621ViewModel()
            {
                TT621 = new Data.Models_KTTM.TT621(),
                KVCTPCT = new Data.Models_KTTM.KVCTPCT()
            };

            _kVCTPCTService = kVCTPCTService;
            _tamUngService = tamUngService;
            _tT621Service = tT621Service;
            _kVPCTService = kVPCTService;
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

        public async Task<IActionResult> ThemMoiCT_TT_Partial(long tamUngId, long kVCTPCTId_PhieuTC) // tamungid == kvctpctid // 1 <-> 1
        {
            TT621VM.TamUngId = tamUngId; //tamungId phia tren khi click
            TT621 tT621 = _tT621Service.GetDummyTT621_By_KVCTPCT(kVCTPCTId_PhieuTC);
            TT621VM.TT621 = tT621;

            // lay sotien can de ket chuyen
            TamUng tamUngPhiaTren = await _tamUngService.GetByIdAsync(tamUngId);
            TT621VM.KVCTPCT = await _kVCTPCTService.FindByIdInclude(kVCTPCTId_PhieuTC);
            var soTienNT_TrongTT621_TheoTamUng = await _tT621Service.GetSoTienNT_TrongTT621_TheoTamUngAsync(tamUngId);
            TT621VM.TT621.SoTienNT = tamUngPhiaTren.SoTienNT - TT621VM.KVCTPCT.SoTienNT - soTienNT_TrongTT621_TheoTamUng; // kVCTPCT.SoTien trong phieuT
            TT621VM.TT621.SoTien = TT621VM.TT621.SoTienNT * tT621.TyGia;

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

            TT621VM.LoaiHDGocs = _kVCTPCTService.LoaiHDGocs();

            return PartialView(TT621VM);
        }

        public async Task<IActionResult> CapNhatCT_TT_Partial(long tt621Id, long kVCTPCTId_PhieuTC) // tamungid == kvctpctid // 1 <-> 1
        {
            
            if (tt621Id == 0)
                return NotFound();

            TT621 tT621 = await _tT621Service.FindById_Include(tt621Id);

            if (tT621 == null)
                return NotFound();

            TT621VM.KVCTPCT = await _kVCTPCTService.FindByIdInclude(kVCTPCTId_PhieuTC);

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

            TT621VM.LoaiHDGocs = _kVCTPCTService.LoaiHDGocs();
            return PartialView(TT621VM);
        }

        [HttpPost, ActionName("CapNhatCT_TT_Partial")]
        public async Task<IActionResult> CapNhatCT_TT_Partial_Post(long tt621Id, long tamUngId) // tamungid phia tren khi click
        {
            if (tamUngId == 0)
                return NotFound();
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            if (tamUng == null)
                return NotFound();

            TT621VM.TT621.TamUngId = tamUngId;

            if (!ModelState.IsValid)
            {
                return View(TT621VM);
            }

            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {

                return View(TT621VM);
            }

            TT621VM.TT621.NguoiSua = user.Username;
            TT621VM.TT621.NgaySua = DateTime.Now;
            TT621VM.TT621.MaKhNo = string.IsNullOrEmpty(TT621VM.TT621.MaKhNo) ? "" : TT621VM.TT621.MaKhNo.ToUpper();
            TT621VM.TT621.MaKhCo = string.IsNullOrEmpty(TT621VM.TT621.MaKhCo) ? "" : TT621VM.TT621.MaKhCo.ToUpper();

            // ghi log
            #region log file
            //var t = _unitOfWork.tourRepository.GetById(id);
            string temp = "";
            TT621 t = _tT621Service.GetBySoCTAsNoTracking(tt621Id);

            if (t.HTTC != TT621VM.TT621.HTTC)
            {
                temp += String.Format("- HTTC thay đổi: {0}->{1}", t.HTTC, TT621VM.TT621.HTTC);
            }

            if (t.DienGiai != TT621VM.TT621.DienGiai)
            {
                temp += String.Format("- DienGiai thay đổi: {0}->{1}", t.DienGiai, TT621VM.TT621.DienGiai);
            }

            if (t.TKNo != TT621VM.TT621.TKNo)
            {
                temp += String.Format("- TKNo thay đổi: {0}->{1}", t.TKNo, TT621VM.TT621.TKNo);
            }

            if (t.TKCo != TT621VM.TT621.TKCo)
            {
                temp += String.Format("- TKCo thay đổi: {0}->{1}", t.TKCo, TT621VM.TT621.TKCo);
            }

            if (t.Sgtcode != TT621VM.TT621.Sgtcode)
            {
                temp += String.Format("- Sgtcode thay đổi: {0}->{1}", t.Sgtcode, TT621VM.TT621.Sgtcode);
            }

            if (t.MaKhNo != TT621VM.TT621.MaKhNo)
            {
                temp += String.Format("- MaKhNo thay đổi: {0}->{1}", t.MaKhNo, TT621VM.TT621.MaKhNo);
            }

            if (t.MaKhCo != TT621VM.TT621.MaKhCo)
            {
                temp += String.Format("- MaKhCo thay đổi: {0}->{1}", t.MaKhCo, TT621VM.TT621.MaKhCo);
            }

            if (t.SoTienNT != TT621VM.TT621.SoTienNT)
            {
                temp += String.Format("- SoTienNT thay đổi: {0:N0}->{1:N0}", t.SoTienNT, TT621VM.TT621.SoTienNT);
            }

            if (t.LoaiTien != TT621VM.TT621.LoaiTien)
            {
                temp += String.Format("- LoaiTien thay đổi: {0}->{1}", t.LoaiTien, TT621VM.TT621.LoaiTien);
            }

            if (t.TyGia != TT621VM.TT621.TyGia)
            {
                temp += String.Format("- TyGia thay đổi: {0:N0}->{1:N0}", t.TyGia, TT621VM.TT621.TyGia);
            }

            if (t.SoTien != TT621VM.TT621.SoTien)
            {
                temp += String.Format("- SoTien thay đổi: {0:N0}->{1:N0}", t.SoTien, TT621VM.TT621.SoTien);
            }

            if (t.SoXe != TT621VM.TT621.SoXe)
            {
                temp += String.Format("- SoXe thay đổi: {0}->{1}", t.SoXe, TT621VM.TT621.SoXe);
            }

            if (t.MsThue != TT621VM.TT621.MsThue)
            {
                temp += String.Format("- MsThue thay đổi: {0}->{1}", t.MsThue, TT621VM.TT621.MsThue);
            }

            if (t.LoaiHDGoc != TT621VM.TT621.LoaiHDGoc)
            {
                temp += String.Format("- LoaiHDGoc thay đổi: {0}->{1}", t.LoaiHDGoc, TT621VM.TT621.LoaiHDGoc);
            }

            if (t.SoCTGoc != TT621VM.TT621.SoCTGoc)
            {
                temp += String.Format("- SoCTGoc thay đổi: {0}->{1}", t.SoCTGoc, TT621VM.TT621.SoCTGoc);
            }

            if (t.NgayCTGoc != TT621VM.TT621.NgayCTGoc)
            {
                temp += String.Format("- NgayCTGoc thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayCTGoc, TT621VM.TT621.NgayCTGoc);
            }

            if (t.VAT != TT621VM.TT621.VAT)
            {
                temp += String.Format("- VAT thay đổi: {0:N0}->{1:N0}", t.NgayCTGoc, TT621VM.TT621.VAT);
            }

            if (t.DSKhongVAT != TT621VM.TT621.DSKhongVAT)
            {
                temp += String.Format("- DSKhongVAT thay đổi: {0:N0}->{1:N0}", t.DSKhongVAT, TT621VM.TT621.DSKhongVAT);
            }

            if (t.BoPhan != TT621VM.TT621.BoPhan)
            {
                temp += String.Format("- DSKhongVAT thay đổi: {0}->{1}", t.BoPhan, TT621VM.TT621.BoPhan);
            }

            if (t.NoQuay != TT621VM.TT621.NoQuay)
            {
                temp += String.Format("- NoQuay thay đổi: {0}->{1}", t.NoQuay, TT621VM.TT621.NoQuay);
            }

            if (t.CoQuay != TT621VM.TT621.CoQuay)
            {
                temp += String.Format("- CoQuay thay đổi: {0}->{1}", t.CoQuay, TT621VM.TT621.CoQuay);
            }

            if (t.TenKH != TT621VM.TT621.TenKH)
            {
                temp += String.Format("- TenKH thay đổi: {0}->{1}", t.TenKH, TT621VM.TT621.TenKH);
            }

            if (t.DiaChi != TT621VM.TT621.DiaChi)
            {
                temp += String.Format("- DiaChi thay đổi: {0}->{1}", t.DiaChi, TT621VM.TT621.DiaChi);
            }

            if (t.MatHang != TT621VM.TT621.MatHang)
            {
                temp += String.Format("- MatHang thay đổi: {0}->{1}", t.MatHang, TT621VM.TT621.MatHang);
            }

            if (t.KyHieu != TT621VM.TT621.KyHieu)
            {
                temp += String.Format("- MatHang thay đổi: {0}->{1}", t.KyHieu, TT621VM.TT621.KyHieu);
            }

            if (t.MauSoHD != TT621VM.TT621.MauSoHD)
            {
                temp += String.Format("- MauSoHD thay đổi: {0}->{1}", t.MauSoHD, TT621VM.TT621.MauSoHD);
            }

            if (t.DieuChinh != TT621VM.TT621.DieuChinh)
            {
                temp += String.Format("- DieuChinh thay đổi: {0}->{1}", t.DieuChinh, TT621VM.TT621.DieuChinh);
            }

            if (t.TamUng != TT621VM.TT621.TamUng)
            {
                temp += String.Format("- TamUng thay đổi: {0}->{1}", t.TamUng, TT621VM.TT621.TamUng);
            }

            if (t.DienGiaiP != TT621VM.TT621.DienGiaiP)
            {
                temp += String.Format("- DienGiaiP thay đổi: {0}->{1}", t.DienGiaiP, TT621VM.TT621.DienGiaiP);
            }

            if (t.HoaDonDT != TT621VM.TT621.HoaDonDT)
            {
                temp += String.Format("- HoaDonDT thay đổi: {0}->{1}", t.HoaDonDT, TT621VM.TT621.HoaDonDT);
            }

            if (t.NguoiSua != TT621VM.TT621.NguoiSua)
            {
                temp += String.Format("- NguoiSua thay đổi: {0}->{1}", t.NguoiSua, TT621VM.TT621.NguoiSua);
            }

            if (t.NgaySua != TT621VM.TT621.NgaySua)
            {
                temp += String.Format("- NgaySua thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgaySua, TT621VM.TT621.NgaySua);
            }

            // kiem tra thay doi
            if (temp.Length > 0)
            {

                string log = System.Environment.NewLine;
                log += "=============";
                log += System.Environment.NewLine;
                log += temp + " -User cập nhật tour: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                t.LogFile = t.LogFile + log;
                TT621VM.TT621.LogFile = t.LogFile;
            }
            #endregion
            try
            {
                await _tT621Service.UpdateAsync(TT621VM.TT621);

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
        
        [HttpPost, ActionName("ThemMoiCT_TT_Partial")]
        public async Task<IActionResult> ThemMoiCT_TT_Partial_Post(long tamUngId) // tamungid phia tren khi click
        {
            if (tamUngId == 0)
                return NotFound();
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            if (tamUng == null)
                return NotFound();

            TT621VM.TT621.TamUngId = tamUngId;

            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {

                return View(TT621VM);
            }

            TT621VM.TT621.NgayCT = DateTime.Now;
            TT621VM.TT621.NguoiTao = user.Username;
            TT621VM.TT621.NgayTao = DateTime.Now;
            TT621VM.TT621.MaKhNo = string.IsNullOrEmpty(TT621VM.TT621.MaKhNo) ? "" : TT621VM.TT621.MaKhNo.ToUpper();
            TT621VM.TT621.MaKhCo = string.IsNullOrEmpty(TT621VM.TT621.MaKhCo) ? "" : TT621VM.TT621.MaKhCo.ToUpper();
            // lay soct cua tt621
            if (TT621VM.TT621.LoaiTien == "VND")
            {
                TT621VM.TT621.SoCT = _tT621Service.GetSoCT("TV");
            }
            else
            {
                TT621VM.TT621.SoCT = _tT621Service.GetSoCT("TN");
            }

            // PhieuTC: tuy vao loai phieu lam TT
            TT621VM.TT621.PhieuTC = TT621VM.KVCTPCT.KVPCTId; // SoCT ben KVPCT

            // phieuTU
            TT621VM.TT621.PhieuTU = tamUng.SoCT;

            // Lapphieu
            TT621VM.TT621.LapPhieu = user.Username;
            // ghi log
            TT621VM.TT621.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _tT621Service.CreateAsync(TT621VM.TT621);

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
        
        private void Get_TkNo_TkCo()
        {

            DmTk dmTk = new DmTk() { Tkhoan = "" };
            var dmTks_TienMat = _kVCTPCTService.GetAll_DmTk_TienMat().ToList();
            var dmTks_TaiKhoan = _kVCTPCTService.GetAll_DmTk_TaiKhoan().ToList();
            dmTks_TienMat.Insert(0, dmTk);
            dmTks_TaiKhoan.Insert(0, dmTk);
            if (TT621VM.KVCTPCT.KVPCT.MFieu == "T")
            {

                TT621VM.DmTks_TkNo = dmTks_TienMat;
                TT621VM.DmTks_TkCo = dmTks_TaiKhoan;
            }
            else
            {
                TT621VM.DmTks_TkNo = dmTks_TaiKhoan;
                TT621VM.DmTks_TkCo = dmTks_TienMat;
            }
        }

        public async Task<JsonResult> GetCommentText_By_TamUng(long tamUngId, decimal soTienNT) // tamUngId == kvctpctId
        {
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            decimal soTienNTTrongTT621_TheoTamUng = await _tT621Service.GetSoTienNT_TrongTT621_TheoTamUngAsync(tamUngId);
            string commentText = "Tạm ứng " + tamUng.SoCT + " còn nợ " + tamUng.SoTien.ToString("N0") + " số tiền cần kết chuyển 141: "
                                  + (tamUng.SoTien - soTienNT - soTienNTTrongTT621_TheoTamUng).ToString("N0");
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
        public async Task<JsonResult> Check_KetChuyenBtnStatus(long tamUngId, decimal soTienNT_Tren_TT621Create)
        {
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            decimal soTienNTTrongTT621_TheoTamUng = await _tT621Service.GetSoTienNT_TrongTT621_TheoTamUngAsync(tamUngId);
            decimal tongTienNT = soTienNTTrongTT621_TheoTamUng + soTienNT_Tren_TT621Create;
            if (tamUng.SoTienNT - tongTienNT == 0)
            {
                return Json(false); // btn on
            }
            return Json(true); // btn off

        }

        [HttpPost]
        public async Task<JsonResult> KetChuyen(long tamUngId, decimal soTienNT_PhieuTC)
        {
            TamUng tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            decimal soTienNTTrongTT621_TheoTamUng = await _tT621Service.GetSoTienNT_TrongTT621_TheoTamUngAsync(tamUngId);

            if (tamUng.SoTienNT == soTienNTTrongTT621_TheoTamUng + soTienNT_PhieuTC)
            {
                tamUng.ConLaiNT = 0;
                tamUng.ConLai = 0;
                await _tamUngService.UpdateAsync(tamUng);

                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public async Task<JsonResult> Check_SoTienNTCanKetChuyen_For_BtnThemMoiCTTT_Status(long tamUngId, long kVCTPCTId_PhieuTC)
        {

            // lay sotien can de ket chuyen
            TamUng tamUngPhiaTren = await _tamUngService.GetByIdAsync(tamUngId);
            var kVCTPCT = await _kVCTPCTService.FindByIdInclude(kVCTPCTId_PhieuTC);
            decimal soTienNT_TrongTT621_TheoTamUng = await _tT621Service.GetSoTienNT_TrongTT621_TheoTamUngAsync(tamUngId);
            decimal soTienNT_CanKetChuyen = tamUngPhiaTren.SoTienNT - kVCTPCT.SoTienNT - soTienNT_TrongTT621_TheoTamUng; // kVCTPCT.SoTien trong phieuT

            if (soTienNT_CanKetChuyen == 0)
                return Json(true); // == 0 ==> ko can new them CTTT ==> disable btnThemMoiCT
            
            return Json(false);
        }

    }
}