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
        private readonly IKVCTPTCService _kVCTPTCService;
        private readonly ITamUngService _tamUngService;
        private readonly ITT621Service _tT621Service;
        private readonly IKVPTCService _kVPTCService;

        [BindProperty]
        public TT621ViewModel TT621VM { get; set; }

        public TT621sController(IKVCTPTCService kVCTPTCService, ITamUngService tamUngService, ITT621Service tT621Service, IKVPTCService kVPTCService)
        {
            TT621VM = new TT621ViewModel()
            {
                TT621 = new Data.Models_KTTM.TT621(),
                KVCTPTC = new Data.Models_KTTM.KVCTPTC(),
                KVPTC = new KVPTC()
            };

            _kVCTPTCService = kVCTPTCService;
            _tamUngService = tamUngService;
            _tT621Service = tT621Service;
            _kVPTCService = kVPTCService;
        }

        public async Task<IActionResult> KhongTC_141(Guid kvptcId, string strUrl, string page, string maKh, string tenKh)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            TT621VM.StrUrl = strUrl;
            TT621VM.Page = page;

            ViewBag.maKh = maKh;
            ViewBag.tenKh = tenKh;

            if (kvptcId != Guid.Empty) // nguoi ta có chọn 1 dóng phiếu nào đó
            {
                TT621VM.KVPTC = await _kVPTCService.GetByGuidIdAsync(kvptcId);
            }

            if (!string.IsNullOrEmpty(maKh))
            {
                // lay het chi tiet ma co' maKhNo co tkNo = 1411 va tamung.contai > 0 (chua thanh toan het)
                TT621VM.TamUngs = await _tamUngService.Find_TamUngs_By_MaKh_Include_KhongTC(maKh, user.Macn);
                TT621VM.TamUngs = TT621VM.TamUngs.OrderByDescending(x => x.NgayCT);

                // get commenttext
                if (TT621VM.TamUngs.Count() > 0)
                {
                    var jsonResult = GetCommentText_By_TamUng(TT621VM.TamUngs.FirstOrDefault().Id, 0, "T");
                    TT621VM.CommentText = jsonResult.Result.Value.ToString();

                    // get all TT621 thep tamungs -> clear het
                    List<TT621> tT621s = new List<TT621>();
                    foreach (var item in TT621VM.TamUngs)
                    {
                        var tT621s1 = await _tT621Service.GetTT621s_By_TamUng(item.Id);
                        if (tT621s1 != null)
                        {
                            tT621s.AddRange(tT621s1);
                        }
                    }
                    if (tT621s != null)
                    {
                        await _tT621Service.DeleteRangeAsync(tT621s);
                    }
                    // get all TT621 thep tamungs -> clear het
                }
            }

            return View(TT621VM);
        }

        public JsonResult GetKhachHangs_By_Code_KhongTC(string code)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            var supplier = _tT621Service.GetSuppliersByCode(code, user.Macn).FirstOrDefault();
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

        public IActionResult GetKhachHangs_HDVATOB_By_Code_KhongTC(string code, string kvpctId, string strUrl, int page = 1)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            code ??= "";
            //TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName(code, user.Macn);
            TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName_PagedList(code, user.Macn, page);
            TT621VM.MaKhText = code;

            ViewBag.kvpctId = kvpctId;
            TT621VM.StrUrl = strUrl;
            //TT621VM.Page = page;

            return PartialView(TT621VM);
        }

        public IActionResult GetKhachHangs_HDVATOB_By_Code_KhongTC_CapNhat(string code, int page = 1)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            code ??= "";
            //TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName(code, user.Macn);
            TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName_PagedList(code, user.Macn, page);
            TT621VM.MaKhText = code;

            return PartialView(TT621VM);
        }

        public IActionResult GetKhachHangs_HDVATOB_By_Code_KhongTC_ThemMoi(string code, int page = 1)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            code ??= "";
            //TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName(code, user.Macn);
            TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName_PagedList(code, user.Macn, page);
            TT621VM.MaKhText = code;

            return PartialView(TT621VM);
        }

        ////////////////////////////////////////////// TT141 //////////////////////////////////////////////
        public async Task<IActionResult> TT621Create(long kvctptcId, string strUrl, string page, long tamUngId)
        {
            if (tamUngId == 0)
            {
                ViewBag.tamUngId = "";
            }
            else
            {
                ViewBag.tamUngId = tamUngId;
            }
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            TT621VM.StrUrl = strUrl;
            TT621VM.Page = page;

            if (kvctptcId == 0)
            {
                ViewBag.ErrorMessage = "Chi tiết này không tồn tại";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            var kVCTPCT = await _kVCTPTCService.GetById(kvctptcId);

            if (kVCTPCT == null)
            {
                ViewBag.ErrorMessage = "Chi tiết này không tồn tại";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            if (!string.IsNullOrEmpty(kVCTPCT.HoanUngTU))
            {
                //ViewBag.errorMessage = "chi tiết này đã hoàn ứng: " + kVCTPCT.HoanUngTU;
                //return View("~/Views/Shared/Error.cshtml");

                SetAlert("Chi tiết này đã hoàn ứng: " + kVCTPCT.HoanUngTU, "warning");
                return Redirect(strUrl);
            }

            TT621VM.KVCTPTC = kVCTPCT;
            TT621VM.KVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);

            // lay het chi tiet ma co' maKhNo co tkNo = 1411 || 1412 va tamung.contai > 0 (chua thanh toan het)
            if (TT621VM.KVPTC.MFieu == "C")
            {
                TT621VM.TamUngs = await _tamUngService.Find_TamUngs_By_MaKh_Include(kVCTPCT.MaKh,
                user.Macn, kVCTPCT.TKNo); // MaKh == MaKhNo && theo TK (ung VND or NgoaiTe)
            }
            else
            {
                TT621VM.TamUngs = await _tamUngService.Find_TamUngs_By_MaKh_Include(kVCTPCT.MaKh,
                user.Macn, kVCTPCT.TKCo); // MaKh == MaKhCo && theo TK (ung VND or NgoaiTe)
            }

            TT621VM.TamUngs = TT621VM.TamUngs.OrderByDescending(x => x.NgayCT);

            if (!string.IsNullOrEmpty(kVCTPCT.SoTT_DaTao) && tamUngId == 0) // moi load vao
            {
                ViewBag.SoTT_DaTao = kVCTPCT.SoTT_DaTao;
                var tT621 = await _tT621Service.GetBySoCT(kVCTPCT.SoTT_DaTao, user.Macn);
                if (tT621 != null)
                    ViewBag.tamUngId = tT621.TamUngId;
            }

            if (!string.IsNullOrEmpty(kVCTPCT.SoTT_DaTao)) // redirect
            {
                ViewBag.SoTT_DaTao = kVCTPCT.SoTT_DaTao;
            }

            // get commenttext
            if (TT621VM.TamUngs.Count() > 0)
            {
                var jsonResult = GetCommentText_By_TamUng(TT621VM.TamUngs.FirstOrDefault().Id, kVCTPCT.SoTienNT.Value, TT621VM.KVPTC.MFieu);
                TT621VM.CommentText = jsonResult.Result.Value.ToString();

                //// get all TT621 thep tamungs -> clear het
                //List<TT621> tT621s = new List<TT621>();
                //foreach (var item in TT621VM.TamUngs)
                //{
                //    var tT621s1 = await _tT621Service.GetTT621s_By_TamUng(item.Id);
                //    if (tT621s1 != null)
                //    {
                //        tT621s.AddRange(tT621s1);
                //    }
                //}
                //if (tT621s != null)
                //{
                //    await _tT621Service.DeleteRangeAsync(tT621s);
                //}
                //// get all TT621 thep tamungs -> clear het
            }

            return View(TT621VM);
        }

        public async Task<IActionResult> CapNhatCT_TT_KhongTC_Partial(long tt621Id) // tamungid == kvctpctid // 1 <-> 1
        {
            if (tt621Id == 0)
                return NotFound();

            TT621 tT621 = await _tT621Service.FindById_Include(tt621Id);

            if (tT621 == null)
                return NotFound();

            // TT621VM.KVCTPCT = await _kVCTPTCService.FindByIdInclude(kVCTPCTId_PhieuTC);

            TT621VM.TT621 = tT621;

            // tentk
            TT621VM.TenTkNo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(tT621.TKNo).TenTk;
            TT621VM.TenTkCo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(tT621.TKCo).TenTk;
            TT621VM.Dgiais = _kVCTPTCService.Get_DienGiai_By_TkNo_TkCo(tT621.TKNo, tT621.TKCo);

            // ddl
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            //TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().OrderByDescending(x => x.MaNt);
            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);

            var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            TT621VM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            TT621VM.Quays = _kVCTPTCService.GetAll_Quay_View();
            var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            TT621VM.MatHangs = viewMatHangs;
            TT621VM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();

            TT621VM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();

            return PartialView(TT621VM);
        }

        [HttpPost, ActionName("CapNhatCT_TT_KhongTC_Partial")]
        public async Task<IActionResult> CapNhatCT_TT_KhongTC_Partial_Post(long tt621Id)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (string.IsNullOrEmpty(TT621VM.TT621.MaKhNo))
            {
                return Json(new
                {
                    status = false,
                    message = "Mã KH nợ không được để trống."
                });
            }

            if (!ModelState.IsValid)
            {
                return View(TT621VM);
            }

            // khong cho sua sotienNT

            TT621VM.TT621.NguoiSua = user.Username;
            TT621VM.TT621.DienGiaiP = TT621VM.TT621.DienGiaiP.Trim().ToUpper();
            TT621VM.TT621.NgaySua = DateTime.Now;
            TT621VM.TT621.MaKhNo = string.IsNullOrEmpty(TT621VM.TT621.MaKhNo) ? "" : TT621VM.TT621.MaKhNo.ToUpper();
            TT621VM.TT621.MaKhCo = string.IsNullOrEmpty(TT621VM.TT621.MaKhCo) ? "" : TT621VM.TT621.MaKhCo.ToUpper();

            // ghi log

            #region log file

            //var t = _unitOfWork.tourRepository.GetById(id);
            string temp = "";
            TT621 t = _tT621Service.GetByIdAsNoTracking(TT621VM.TT621.Id);

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
                log += temp + " -User cập nhật: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                t.LogFile = t.LogFile + log;
                TT621VM.TT621.LogFile = t.LogFile;
            }

            #endregion log file

            try
            {
                await _tT621Service.UpdateAsync(TT621VM.TT621);

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

        public async Task<IActionResult> ThemMoiCT_TT_KhongTC_Partial(long tamUngId) // tamungid == kvctpctid // 1 <-> 1
        {
            //TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);

            TT621VM.TamUngId = tamUngId; //tamungId phia tren khi click
            TT621VM.KVCTPTC = await _kVCTPTCService.GetById(tamUngId);
            TT621VM.KVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);

            // lay sotien can de ket chuyen
            TamUng tamUngPhiaTren = await _tamUngService.GetByIdAsync(tamUngId);
            // var soTienNT_TrongTT621_TheoTamUng = _tT621Service.GetSoTienNT_TrongTT621_TheoTamUng(tamUngId);
            TT621VM.TT621.SoTienNT = tamUngPhiaTren.SoTienNT.Value;// - soTienNT_TrongTT621_TheoTamUng; // soTienNT_TrongTT621_TheoTamUng = 0 vi chi có dong CTTT
            TT621VM.TT621.SoTien = TT621VM.TT621.SoTienNT * tamUngPhiaTren.TyGia.Value;
            TT621VM.TT621.TyGia = 1;

            // ddl
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            TT621VM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            TT621VM.Quays = _kVCTPTCService.GetAll_Quay_View();
            var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            TT621VM.MatHangs = viewMatHangs;
            TT621VM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();

            TT621VM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
            TT621VM.TT621.NgayCTGoc = DateTime.Now; // Thao

            return PartialView(TT621VM);
        }

        [HttpPost, ActionName("ThemMoiCT_TT_KhongTC_Partial")]
        public async Task<IActionResult> ThemMoiCT_TT_KhongTC_Partial_Post(long tamUngId) // tamungid phia tren khi click
        {
            if (tamUngId == 0)
                return NotFound();
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            if (tamUng == null)
                return NotFound();

            TT621VM.TT621.TamUngId = tamUngId;

            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (string.IsNullOrEmpty(TT621VM.TT621.TKNo))
            {
                return Json(new
                {
                    status = false,
                    message = "<b>Tài khoãn</b> không được bỏ trống."
                });
            }

            if (string.IsNullOrEmpty(TT621VM.TT621.MaKhNo))
            {
                return Json(new
                {
                    status = false,
                    message = "<b>Mã KH nợ</> không được để trống."
                });
            }

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    status = false,
                    message = "Model invalid."
                });
            }

            ////
            //decimal soTienNT_CanKetChuyen = _tT621Service.Get_SoTienNT_CanKetChuyen(TT621VM.TT621.TamUngId, 0); // TT621VM.KVCTPCT.SoTienNT tu view qua
            //// txtSoTienNT nhập vào không được vượt quá soTienNT_ChuaCapNhat(cũ) + soTienNT_CanKetChuyen (tt621 theo tamung, sotienNT theo phieu TC)
            //if (TT621VM.TT621.SoTienNT > soTienNT_CanKetChuyen)
            //{
            //    return Json(new
            //    {
            //        status = false,
            //        message = "<b>Số tiền NT</b> đã vượt quá số tiền cần kết chuyển."
            //    });
            //}

            TT621VM.TT621.NgayCT = DateTime.Now;
            TT621VM.TT621.NguoiTao = user.Username;
            TT621VM.TT621.DienGiaiP = string.IsNullOrEmpty(TT621VM.TT621.DienGiaiP) ? "" : TT621VM.TT621.DienGiaiP.Trim().ToUpper();// TT621VM.TT621.DienGiaiP.Trim().ToUpper();
            TT621VM.TT621.NgayTao = DateTime.Now;
            TT621VM.TT621.MaKhNo = string.IsNullOrEmpty(TT621VM.TT621.MaKhNo) ? "" : TT621VM.TT621.MaKhNo.ToUpper();
            TT621VM.TT621.MaKhCo = string.IsNullOrEmpty(TT621VM.TT621.MaKhCo) ? "" : TT621VM.TT621.MaKhCo.ToUpper();
            //// lay soct cua tt621
            //if (TT621VM.TT621.LoaiTien == "VND")
            //{
            TT621VM.TT621.SoCT = _tT621Service.GetSoCT("TV", user.Macn);
            //}
            //else
            //{
            //    TT621VM.TT621.SoCT = _tT621Service.GetSoCT("TN");
            //}

            TT621VM.KVCTPTC = await _kVCTPTCService.GetById(tamUngId); // tamUngId == kvctpctId 1 <-> 1
            // PhieuTC: tuy vao loai phieu lam TT
            TT621VM.TT621.PhieuTC = "KHONG PTC"; // anh Son: m.phieutc='KHONG PTC'

            // phieuTU
            TT621VM.TT621.PhieuTU = tamUng.SoCT;

            // Lapphieu
            TT621VM.TT621.LapPhieu = user.Username;
            // ghi log
            TT621VM.TT621.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString() + " từ 141 Không TC " + TT621VM.KVCTPTC.KVPTCId; // user.Username

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

        public async Task<IActionResult> ThemMoiCT_TT_Partial(long tamUngId, long kVCTPCTId_PhieuTC, long id_Dong_Da_Click,
            string dienGiaiP, string hTTC, string soTienNT) // tamungid == kvctpctid // 1 <-> 1
        {
            TT621VM.TamUngId = tamUngId; //tamungId phia tren khi click
            TT621 tT621 = _tT621Service.GetDummyTT621_By_KVCTPCT(kVCTPCTId_PhieuTC);
            TT621VM.TT621 = tT621;
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            TT621VM.TT621.MaKhCo = tamUng.MaKhNo; // makh cua nguoi tamung

            //TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);

            // lay sotien can de ket chuyen
            TamUng tamUngPhiaTren = await _tamUngService.GetByIdAsync(tamUngId);
            TT621VM.KVCTPTC = await _kVCTPTCService.FindByIdInclude(kVCTPCTId_PhieuTC);
            TT621VM.KVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);
            if (TT621VM.KVPTC.NgoaiTe == "VN")
            {
                TT621VM.TT621.LoaiTien = "VND";
            }

            var soTienNT_TrongTT621_TheoTamUng = _tT621Service.GetSoTienNT_TrongTT621_TheoTamUng(tamUngId);
            if (TT621VM.KVCTPTC.KVPTC.MFieu == "C")
            {
                TT621VM.TT621.SoTienNT = tamUngPhiaTren.SoTienNT.Value + TT621VM.KVCTPTC.SoTienNT.Value - soTienNT_TrongTT621_TheoTamUng; // kVCTPCT.SoTien trong phieuC
                TT621VM.TT621.TKNo = tT621.TKCo; // dao nguoc
                TT621VM.TT621.TKCo = "1411"; // tT621.TKNo; // dao nguoc
                if (TT621VM.KVCTPTC.KVPTC.NgoaiTe == "NT")
                {
                    TT621VM.TT621.TKCo = "1412"; // tT621.TKNo; // dao nguoc
                }
            }
            else
            {
                TT621VM.TT621.SoTienNT = tamUngPhiaTren.ConLaiNT.Value - TT621VM.KVCTPTC.SoTienNT.Value - soTienNT_TrongTT621_TheoTamUng; // kVCTPCT.SoTien trong phieuT
            }

            TT621VM.TT621.SoTien = TT621VM.TT621.SoTienNT * tT621.TyGia;

            // tentk
            TT621VM.TenTkNo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(tT621.TKNo).TenTk;
            TT621VM.TenTkCo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(tT621.TKCo).TenTk;
            TT621VM.Dgiais = _kVCTPTCService.Get_DienGiai_By_TkNo_TkCo(tT621.TKNo, tT621.TKCo);

            // ddl
            Data.Models_HDVATOB.Supplier supplier = new Data.Models_HDVATOB.Supplier() { Code = "" };
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            TT621VM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            TT621VM.Quays = _kVCTPTCService.GetAll_Quay_View();
            var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            TT621VM.MatHangs = viewMatHangs;
            TT621VM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();

            TT621VM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
            TT621VM.TT621.NgayCTGoc = DateTime.Now; // Thao

            // btnThemdong + copy dong da click
            if (id_Dong_Da_Click > 0)
            {
                var dongCu = _tT621Service.GetByIdAsNoTracking(id_Dong_Da_Click);
                TT621VM.TT621 = dongCu;
                TT621VM.TT621.SoTienNT = tT621.SoTienNT;
            }

            if (!string.IsNullOrEmpty(dienGiaiP))
            {
                TT621VM.TT621.DienGiaiP = dienGiaiP;
                TT621VM.TT621.HTTC = hTTC;
                TT621VM.TT621.SoTienNT = decimal.Parse(soTienNT);
                TT621VM.TT621.SoTien = TT621VM.TT621.SoTienNT * TT621VM.TT621.TyGia;
            }

            return PartialView(TT621VM);
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
            KVPTC kVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);

            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                return View(TT621VM);
            }

            //
            decimal soTienNT_CanKetChuyen = _tT621Service.Get_SoTienNT_CanKetChuyen(
                TT621VM.TT621.TamUngId, TT621VM.KVCTPTC.SoTienNT.Value, kVPTC.MFieu); // TT621VM.KVCTPCT.SoTienNT tu view qua
            // txtSoTienNT nhập vào không được vượt quá soTienNT_ChuaCapNhat(cũ) + soTienNT_CanKetChuyen (tt621 theo tamung, sotienNT theo phieu TC)
            if (TT621VM.TT621.SoTienNT > soTienNT_CanKetChuyen)
            {
                return Json(new
                {
                    status = false,
                    message = "<b>Số tiền NT</b> đã vượt quá số tiền cần kết chuyển."
                });
            }

            TT621VM.TT621.MaCn = user.Macn;
            TT621VM.TT621.NgayCT = DateTime.Now;
            TT621VM.TT621.NguoiTao = user.Username;
            TT621VM.TT621.DienGiaiP = string.IsNullOrEmpty(TT621VM.TT621.DienGiaiP) ? "" : TT621VM.TT621.DienGiaiP.Trim().ToUpper();// TT621VM.TT621.DienGiaiP.Trim().ToUpper();
            TT621VM.TT621.NgayTao = DateTime.Now;
            TT621VM.TT621.MaKhNo = string.IsNullOrEmpty(TT621VM.TT621.MaKhNo) ? "" : TT621VM.TT621.MaKhNo.ToUpper();
            TT621VM.TT621.MaKhCo = string.IsNullOrEmpty(TT621VM.TT621.MaKhCo) ? "" : TT621VM.TT621.MaKhCo.ToUpper();

            IEnumerable<TT621> tt621_Theo_PhieuTC = await _tT621Service.GetTT621s_By_TamUng(tamUngId);//.GetByPhieuTC(TT621VM.KVCTPTC.SoCT, user.Macn);
            if (tt621_Theo_PhieuTC.Count() > 0) // có tồn tại phieu TT nào đó rồi -> lay chung soCT Cua TT621
            {
                TT621VM.TT621.SoCT = tt621_Theo_PhieuTC.FirstOrDefault().SoCT;
            }
            else
            {
                // lay soct cua tt621
                if (TT621VM.TT621.LoaiTien == "VND")
                {
                    TT621VM.TT621.SoCT = _tT621Service.GetSoCT("TV", user.Macn);
                }
                else
                {
                    TT621VM.TT621.SoCT = _tT621Service.GetSoCT("TN", user.Macn);
                }
            }

            // PhieuTC: tuy vao loai phieu lam TT
            TT621VM.TT621.PhieuTC = TT621VM.KVCTPTC.SoCT; // SoCT ben KVPCT or KVCTPTC.SoCT

            // phieuTU
            TT621VM.TT621.PhieuTU = tamUng.SoCT;

            // Lapphieu
            TT621VM.TT621.LapPhieu = user.Username;
            // ghi log
            TT621VM.TT621.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _tT621Service.CreateAsync(TT621VM.TT621);

                // capnhat SoTU_DaTT vào kvctptc
                KVCTPTC kVCTPTC = await _kVCTPTCService.GetById(TT621VM.KVCTPTC.Id); // kVCTPCTId_PhieuTC
                kVCTPTC.SoTT_DaTao = TT621VM.TT621.SoCT;
                await _kVCTPTCService.UpdateAsync(kVCTPTC);

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

        public async Task<IActionResult> ThemMoiCT_TT_ContextMenu_Partial(long tamUngId, long kVCTPCTId_PhieuTC, long id_Dong_Da_Click,
            string dienGiaiP, string hTTC, string soTienNT) // tamungid == kvctpctid // 1 <-> 1
        {
            TT621VM.TamUngId = tamUngId; //tamungId phia tren khi click
            TT621 tT621 = _tT621Service.GetDummyTT621_By_KVCTPCT(kVCTPCTId_PhieuTC);
            TT621VM.TT621 = tT621;
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            TT621VM.TT621.MaKhCo = tamUng.MaKhNo; // makh cua nguoi tamung

            //TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);

            // lay sotien can de ket chuyen
            TamUng tamUngPhiaTren = await _tamUngService.GetByIdAsync(tamUngId);
            TT621VM.KVCTPTC = await _kVCTPTCService.FindByIdInclude(kVCTPCTId_PhieuTC);
            TT621VM.KVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);
            if (TT621VM.KVPTC.NgoaiTe == "VN")
            {
                TT621VM.TT621.LoaiTien = "VND";
            }

            var soTienNT_TrongTT621_TheoTamUng = _tT621Service.GetSoTienNT_TrongTT621_TheoTamUng(tamUngId);
            if (TT621VM.KVCTPTC.KVPTC.MFieu == "C")
            {
                TT621VM.TT621.SoTienNT = tamUngPhiaTren.SoTienNT.Value + TT621VM.KVCTPTC.SoTienNT.Value - soTienNT_TrongTT621_TheoTamUng; // kVCTPCT.SoTien trong phieuC
                TT621VM.TT621.TKNo = tT621.TKCo; // dao nguoc
                TT621VM.TT621.TKCo = "1411"; // tT621.TKNo; // dao nguoc
                if (TT621VM.KVCTPTC.KVPTC.NgoaiTe == "NT")
                {
                    TT621VM.TT621.TKCo = "1412"; // tT621.TKNo; // dao nguoc
                }
            }
            else
            {
                TT621VM.TT621.SoTienNT = tamUngPhiaTren.SoTienNT.Value - TT621VM.KVCTPTC.SoTienNT.Value - soTienNT_TrongTT621_TheoTamUng; // kVCTPCT.SoTien trong phieuT
            }

            TT621VM.TT621.SoTien = TT621VM.TT621.SoTienNT * tT621.TyGia;

            // tentk
            TT621VM.TenTkNo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(tT621.TKNo).TenTk;
            TT621VM.TenTkCo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(tT621.TKCo).TenTk;
            TT621VM.Dgiais = _kVCTPTCService.Get_DienGiai_By_TkNo_TkCo(tT621.TKNo, tT621.TKCo);

            // ddl
            Data.Models_HDVATOB.Supplier supplier = new Data.Models_HDVATOB.Supplier() { Code = "" };
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            TT621VM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            TT621VM.Quays = _kVCTPTCService.GetAll_Quay_View();
            var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            TT621VM.MatHangs = viewMatHangs;
            TT621VM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();

            TT621VM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
            TT621VM.TT621.NgayCTGoc = DateTime.Now; // Thao

            // btnThemdong + copy dong da click
            if (id_Dong_Da_Click > 0)
            {
                var dongCu = _tT621Service.GetByIdAsNoTracking(id_Dong_Da_Click);
                TT621VM.TT621 = dongCu;
                TT621VM.TT621.SoTienNT = tT621.SoTienNT;
            }

            if (!string.IsNullOrEmpty(soTienNT))
            {
                TT621VM.TT621.DienGiaiP = dienGiaiP;
                TT621VM.TT621.HTTC = hTTC;
                //TT621VM.TT621.SoTienNT = decimal.Parse(soTienNT);
                //ViewBag.soTienNT = decimal.Parse(soTienNT);
                TT621VM.TT621.SoTien = decimal.Parse(soTienNT) * TT621VM.TT621.TyGia;
            }

            return PartialView(TT621VM);
        }

        [HttpPost, ActionName("ThemMoiCT_TT_ContextMenu_Partial")]
        public async Task<IActionResult> ThemMoiCT_TT_ContextMenu_Partial_Post(long tamUngId) // tamungid phia tren khi click
        {
            if (tamUngId == 0)
                return NotFound();
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            if (tamUng == null)
                return NotFound();

            TT621VM.TT621.TamUngId = tamUngId;
            KVPTC kVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);

            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                return View(TT621VM);
            }

            //
            decimal soTienNT_CanKetChuyen = _tT621Service.Get_SoTienNT_CanKetChuyen(
                TT621VM.TT621.TamUngId, TT621VM.KVCTPTC.SoTienNT.Value, kVPTC.MFieu); // TT621VM.KVCTPCT.SoTienNT tu view qua
            // txtSoTienNT nhập vào không được vượt quá soTienNT_ChuaCapNhat(cũ) + soTienNT_CanKetChuyen (tt621 theo tamung, sotienNT theo phieu TC)
            if (TT621VM.TT621.SoTienNT > soTienNT_CanKetChuyen)
            {
                return Json(new
                {
                    status = false,
                    message = "<b>Số tiền NT</b> đã vượt quá số tiền cần kết chuyển."
                });
            }

            // dong full
            TT621VM.TT621.MaCn = user.Macn;
            TT621VM.TT621.NgayCT = DateTime.Now;
            TT621VM.TT621.NguoiTao = user.Username;
            TT621VM.TT621.DienGiaiP = string.IsNullOrEmpty(TT621VM.TT621.DienGiaiP) ? "" : TT621VM.TT621.DienGiaiP.Trim().ToUpper();// TT621VM.TT621.DienGiaiP.Trim().ToUpper();
            TT621VM.TT621.NgayTao = DateTime.Now;
            TT621VM.TT621.MaKhNo = string.IsNullOrEmpty(TT621VM.TT621.MaKhNo) ? "" : TT621VM.TT621.MaKhNo.ToUpper();
            TT621VM.TT621.MaKhCo = string.IsNullOrEmpty(TT621VM.TT621.MaKhCo) ? "" : TT621VM.TT621.MaKhCo.ToUpper();
            TT621VM.TT621.SoTien = TT621VM.TT621.SoTienNT * TT621VM.TT621.TyGia;
            TT621VM.TT621.DSKhongVAT = TT621VM.TT621.SoTienNT;

            IEnumerable<TT621> tt621_Theo_PhieuTC = await _tT621Service.GetTT621s_By_TamUng(tamUngId);//.GetByPhieuTC(TT621VM.KVCTPTC.SoCT, user.Macn);
            if (tt621_Theo_PhieuTC.Count() > 0) // có tồn tại phieu TT nào đó rồi -> lay chung soCT Cua TT621
            {
                TT621VM.TT621.SoCT = tt621_Theo_PhieuTC.FirstOrDefault().SoCT;
            }
            else
            {
                // lay soct cua tt621
                if (TT621VM.TT621.LoaiTien == "VND")
                {
                    TT621VM.TT621.SoCT = _tT621Service.GetSoCT("TV", user.Macn);
                }
                else
                {
                    TT621VM.TT621.SoCT = _tT621Service.GetSoCT("TN", user.Macn);
                }
            }

            // PhieuTC: tuy vao loai phieu lam TT
            TT621VM.TT621.PhieuTC = TT621VM.KVCTPTC.SoCT; // SoCT ben KVPCT or KVCTPTC.SoCT

            // phieuTU
            TT621VM.TT621.PhieuTU = tamUng.SoCT;

            // Lapphieu
            TT621VM.TT621.LapPhieu = user.Username;
            // ghi log
            TT621VM.TT621.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _tT621Service.CreateAsync(TT621VM.TT621);

                // dong tach
                TT621 tT621 = TT621VM.TT621;
                tT621.Id = 0;
                tT621.SoTienNT = decimal.Parse(TT621VM.ThueGTGT);
                tT621.SoTien = tT621.SoTienNT * tT621.TyGia;
                tT621.TKNo = "1331000010";
                tT621.DSKhongVAT = TT621VM.TT621.DSKhongVAT;
                await _tT621Service.CreateAsync(tT621);

                // capnhat SoTU_DaTT vào kvctptc
                KVCTPTC kVCTPTC = await _kVCTPTCService.GetById(TT621VM.KVCTPTC.Id); // kVCTPCTId_PhieuTC
                kVCTPTC.SoTT_DaTao = TT621VM.TT621.SoCT;
                await _kVCTPTCService.UpdateAsync(kVCTPTC);

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

        public async Task<IActionResult> ThemMoiCT_TT_ContextMenu(long tamUngId, long kVCTPCTId_PhieuTC, long id_Dong_Da_Click, string strUrl, string page) // tamungid == kvctpctid // 1 <-> 1
        {
            TT621VM.StrUrl = strUrl;
            TT621VM.Page = page;
            TT621VM.TamUngId = tamUngId; //tamungId phia tren khi click
            TT621 tT621 = _tT621Service.GetDummyTT621_By_KVCTPCT(kVCTPCTId_PhieuTC);
            TT621VM.TT621 = tT621;
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            TT621VM.TT621.MaKhCo = tamUng.MaKhNo; // makh cua nguoi tamung

            //TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);

            // lay sotien can de ket chuyen
            TamUng tamUngPhiaTren = await _tamUngService.GetByIdAsync(tamUngId);
            TT621VM.KVCTPTC = await _kVCTPTCService.FindByIdInclude(kVCTPCTId_PhieuTC);
            TT621VM.KVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);
            if (TT621VM.KVPTC.NgoaiTe == "VN")
            {
                TT621VM.TT621.LoaiTien = "VND";
            }

            var soTienNT_TrongTT621_TheoTamUng = _tT621Service.GetSoTienNT_TrongTT621_TheoTamUng(tamUngId);
            if (TT621VM.KVCTPTC.KVPTC.MFieu == "C")
            {
                TT621VM.TT621.SoTienNT = tamUngPhiaTren.ConLaiNT.Value + TT621VM.KVCTPTC.SoTienNT.Value - soTienNT_TrongTT621_TheoTamUng; // kVCTPCT.SoTien trong phieuC
                TT621VM.TT621.TKNo = tT621.TKCo; // dao nguoc
                TT621VM.TT621.TKCo = "1411"; // tT621.TKNo; // dao nguoc
                if (TT621VM.KVCTPTC.KVPTC.NgoaiTe == "NT")
                {
                    TT621VM.TT621.TKCo = "1412"; // tT621.TKNo; // dao nguoc
                }
            }
            else
            {
                TT621VM.TT621.SoTienNT = tamUngPhiaTren.ConLaiNT.Value - TT621VM.KVCTPTC.SoTienNT.Value - soTienNT_TrongTT621_TheoTamUng; // kVCTPCT.SoTien trong phieuT
            }

            TT621VM.TT621.SoTien = TT621VM.TT621.SoTienNT * tT621.TyGia;

            // tentk
            TT621VM.TenTkNo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(tT621.TKNo).TenTk;
            TT621VM.TenTkCo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(tT621.TKCo).TenTk;
            TT621VM.Dgiais = _kVCTPTCService.Get_DienGiai_By_TkNo_TkCo(tT621.TKNo, tT621.TKCo);

            // ddl
            Data.Models_HDVATOB.Supplier supplier = new Data.Models_HDVATOB.Supplier() { Code = "" };
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            TT621VM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            TT621VM.Quays = _kVCTPTCService.GetAll_Quay_View();
            var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            TT621VM.MatHangs = viewMatHangs;
            TT621VM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();

            TT621VM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
            TT621VM.TT621.NgayCTGoc = DateTime.Now; // Thao

            // btnThemdong + copy dong da click
            if (id_Dong_Da_Click > 0)
            {
                var dongCu = _tT621Service.GetByIdAsNoTracking(id_Dong_Da_Click);
                TT621VM.TT621 = dongCu;
                TT621VM.TT621.SoTienNT = tT621.SoTienNT;
            }

            //if (!string.IsNullOrEmpty(soTienNT))
            //{
            //    TT621VM.TT621.DienGiaiP = dienGiaiP;
            //    TT621VM.TT621.HTTC = hTTC;
            //    //TT621VM.TT621.SoTienNT = decimal.Parse(soTienNT);
            //    //ViewBag.soTienNT = decimal.Parse(soTienNT);
            //    TT621VM.TT621.SoTien = decimal.Parse(soTienNT) * TT621VM.TT621.TyGia;
            //}

            return View(TT621VM);
        }

        [HttpPost, ActionName("ThemMoiCT_TT_ContextMenu")]
        public async Task<IActionResult> ThemMoiCT_TT_ContextMenu_Post(long tamUngId) // tamungid phia tren khi click
        {
            if (tamUngId == 0)
                return NotFound();
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            if (tamUng == null)
                return NotFound();

            TT621VM.TT621.TamUngId = tamUngId;
            KVPTC kVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);

            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                return View(TT621VM);
            }

            //
            decimal soTienNT_CanKetChuyen = _tT621Service.Get_SoTienNT_CanKetChuyen(
                TT621VM.TT621.TamUngId, TT621VM.KVCTPTC.SoTienNT.Value, kVPTC.MFieu); // TT621VM.KVCTPCT.SoTienNT tu view qua
            // txtSoTienNT nhập vào không được vượt quá soTienNT_ChuaCapNhat(cũ) + soTienNT_CanKetChuyen (tt621 theo tamung, sotienNT theo phieu TC)
            if (TT621VM.TT621.SoTien > soTienNT_CanKetChuyen)
            {
                //return Json(new
                //{
                //    status = false,
                //    message = "<b>Số tiền NT</b> đã vượt quá số tiền cần kết chuyển."
                //});

                TT621VM.KVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);
                TT621 tT621 = _tT621Service.GetDummyTT621_By_KVCTPCT(tamUngId);
                TT621VM.Dgiais = _kVCTPTCService.Get_DienGiai_By_TkNo_TkCo(tT621.TKNo, tT621.TKCo);
                TT621VM.KVCTPTC = await _kVCTPTCService.FindByIdInclude(TT621VM.KVCTPTC.Id);

                // ddl
                Get_TkNo_TkCo();
                TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);

                Data.Models_HDVATOB.Supplier supplier = new Data.Models_HDVATOB.Supplier() { Code = "" };
                ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
                ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

                var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
                viewDmHttcs.Insert(0, viewDmHttc);
                TT621VM.DmHttcs = viewDmHttcs;

                Get_TkNo_TkCo();

                TT621VM.Quays = _kVCTPTCService.GetAll_Quay_View();
                var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
                viewMatHangs.Insert(0, viewMatHang);
                TT621VM.MatHangs = viewMatHangs;
                TT621VM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();

                TT621VM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();

                SetAlert("Số tiền đã vượt quá số tiền cần kết chuyển.", "warning");
                return View(TT621VM);
            }

            // dong full
            TT621VM.TT621.MaCn = user.Macn;
            TT621VM.TT621.NgayCT = DateTime.Now;
            TT621VM.TT621.NguoiTao = user.Username;
            TT621VM.TT621.DienGiaiP = string.IsNullOrEmpty(TT621VM.TT621.DienGiaiP) ? "" : TT621VM.TT621.DienGiaiP.Trim().ToUpper();// TT621VM.TT621.DienGiaiP.Trim().ToUpper();
            TT621VM.TT621.NgayTao = DateTime.Now;
            TT621VM.TT621.MaKhNo = string.IsNullOrEmpty(TT621VM.TT621.MaKhNo) ? "" : TT621VM.TT621.MaKhNo.ToUpper();
            TT621VM.TT621.MaKhCo = string.IsNullOrEmpty(TT621VM.TT621.MaKhCo) ? "" : TT621VM.TT621.MaKhCo.ToUpper();
            TT621VM.TT621.SoTien = TT621VM.TT621.SoTienNT * TT621VM.TT621.TyGia;
            TT621VM.TT621.DSKhongVAT = TT621VM.TT621.SoTienNT;

            IEnumerable<TT621> tt621_Theo_PhieuTC = await _tT621Service.GetTT621s_By_TamUng(tamUngId);//.GetByPhieuTC(TT621VM.KVCTPTC.SoCT, user.Macn);
            if (tt621_Theo_PhieuTC.Count() > 0) // có tồn tại phieu TT nào đó rồi -> lay chung soCT Cua TT621
            {
                TT621VM.TT621.SoCT = tt621_Theo_PhieuTC.FirstOrDefault().SoCT;
            }
            else
            {
                // lay soct cua tt621
                if (TT621VM.TT621.LoaiTien == "VND")
                {
                    TT621VM.TT621.SoCT = _tT621Service.GetSoCT("TV", user.Macn);
                }
                else
                {
                    TT621VM.TT621.SoCT = _tT621Service.GetSoCT("TN", user.Macn);
                }
            }

            // PhieuTC: tuy vao loai phieu lam TT
            TT621VM.TT621.PhieuTC = TT621VM.KVCTPTC.SoCT; // SoCT ben KVPCT or KVCTPTC.SoCT

            // phieuTU
            TT621VM.TT621.PhieuTU = tamUng.SoCT;

            // Lapphieu
            TT621VM.TT621.LapPhieu = user.Username;
            // ghi log
            TT621VM.TT621.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _tT621Service.CreateAsync(TT621VM.TT621);

                //TT621VM.ThueGTGT = (TT621VM.TT621.SoTien - TT621VM.TT621.SoTienNT).ToString();
                // dong tach
                TT621 tT621 = TT621VM.TT621;
                tT621.Id = 0;
                tT621.SoTienNT = decimal.Parse(TT621VM.ThueGTGT);
                tT621.SoTien = tT621.SoTienNT * tT621.TyGia;
                tT621.TKNo = "1331000010";
                tT621.DSKhongVAT = TT621VM.TT621.DSKhongVAT;
                await _tT621Service.CreateAsync(tT621);

                // capnhat SoTU_DaTT vào kvctptc
                KVCTPTC kVCTPTC = await _kVCTPTCService.GetById(TT621VM.KVCTPTC.Id); // kVCTPCTId_PhieuTC
                kVCTPTC.SoTT_DaTao = TT621VM.TT621.SoCT;
                await _kVCTPTCService.UpdateAsync(kVCTPTC);

                //return Json(new
                //{
                //    status = true
                //});

                return RedirectToAction(nameof(TT621Create), new { kvctptcId = TT621VM.KVCTPTC.Id, tamUngId = tamUngId });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public async Task<IActionResult> CapNhatCT_TT_Partial(long tt621Id, long kVCTPCTId_PhieuTC) // tamungid == kvctpctid // 1 <-> 1
        {
            if (tt621Id == 0)
                return NotFound();

            TT621 tT621 = await _tT621Service.FindById_Include(tt621Id);

            if (tT621 == null)
                return NotFound();

            //TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);
            TT621VM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes_DanhMucKT().Where(x => x.MaNt != "VND").OrderByDescending(x => x.MaNt);
            //KVCTPCTVM.Ngoaites = _kVCTPTCService.GetAll_NgoaiTes().OrderByDescending(x => x.MaNt);

            TT621VM.KVCTPTC = await _kVCTPTCService.FindByIdInclude(kVCTPCTId_PhieuTC);
            TT621VM.KVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);
            if (TT621VM.KVPTC.NgoaiTe == "VN")
            {
                TT621VM.TT621.LoaiTien = "VND";
            }

            TT621VM.TT621 = tT621;

            // TenKHCo
            KhachHang khachHang = await _tT621Service.GetKhachHangById(TT621VM.TT621.MaKhCo);
            ViewBag.tenKhCo = khachHang.TenThuongMai;
            // tentk
            TT621VM.TenTkNo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(tT621.TKNo).TenTk;
            TT621VM.TenTkCo = _kVCTPTCService.Get_DmTk_By_TaiKhoan(tT621.TKCo).TenTk;
            TT621VM.Dgiais = _kVCTPTCService.Get_DienGiai_By_TkNo_TkCo(tT621.TKNo, tT621.TKCo);

            // ddl
            Data.Models_HDVATOB.Supplier supplier = new Data.Models_HDVATOB.Supplier() { Code = "" };
            ViewMatHang viewMatHang = new ViewMatHang() { Mathang = "" };
            ViewDmHttc viewDmHttc = new ViewDmHttc() { DienGiai = "" };

            var viewDmHttcs = _kVCTPTCService.GetAll_DmHttc_View().ToList();
            viewDmHttcs.Insert(0, viewDmHttc);
            TT621VM.DmHttcs = viewDmHttcs;

            Get_TkNo_TkCo();

            TT621VM.Quays = _kVCTPTCService.GetAll_Quay_View();
            var viewMatHangs = _kVCTPTCService.GetAll_MatHangs_View().ToList();
            viewMatHangs.Insert(0, viewMatHang);
            TT621VM.MatHangs = viewMatHangs;
            TT621VM.PhongBans = _kVCTPTCService.GetAll_PhongBans_View();

            TT621VM.LoaiHDGocs = _kVCTPTCService.LoaiHDGocs();
            return PartialView(TT621VM);
        }

        [HttpPost, ActionName("CapNhatCT_TT_Partial")]
        public async Task<IActionResult> CapNhatCT_TT_Partial_Post(/*long id, */decimal soTienNT_ChuaCapNhat) // soTienNT_ChuaCapNhat: soTienNT cũ
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                return View(TT621VM);
            }

            KVPTC kVPTC = await _kVPTCService.GetByGuidIdAsync(TT621VM.KVCTPTC.KVPTCId);

            //
            decimal soTienNT_CanKetChuyen = _tT621Service.Get_SoTienNT_CanKetChuyen(
                TT621VM.TT621.TamUngId, TT621VM.KVCTPTC.SoTienNT.Value, kVPTC.MFieu);
            // txtSoTienNT nhập vào không được vượt quá soTienNT_ChuaCapNhat(cũ) + soTienNT_CanKetChuyen (tt621 theo tamung, sotienNT theo phieu TC)
            if (TT621VM.TT621.SoTienNT > soTienNT_ChuaCapNhat + soTienNT_CanKetChuyen)
            {
                return Json(new
                {
                    status = false,
                    message = "<b>Số tiền NT</b> đã vượt quá số tiền cần kết chuyển."
                });
            }

            TT621VM.TT621.NguoiSua = user.Username;
            TT621VM.TT621.DienGiaiP = TT621VM.TT621.DienGiaiP.Trim().ToUpper();
            TT621VM.TT621.NgaySua = DateTime.Now;
            TT621VM.TT621.MaKhNo = string.IsNullOrEmpty(TT621VM.TT621.MaKhNo) ? "" : TT621VM.TT621.MaKhNo.ToUpper();
            TT621VM.TT621.MaKhCo = string.IsNullOrEmpty(TT621VM.TT621.MaKhCo) ? "" : TT621VM.TT621.MaKhCo.ToUpper();

            // ghi log

            #region log file

            //var t = _unitOfWork.tourRepository.GetById(id);
            string temp = "";
            TT621 t = _tT621Service.GetByIdAsNoTracking(TT621VM.TT621.Id);

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

            #endregion log file

            try
            {
                await _tT621Service.UpdateAsync(TT621VM.TT621);

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

        [HttpPost]
        public async Task<JsonResult> Delete(long tt621Id, long kVCTPTCId_PhieuTC)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (tt621Id == 0)
                return Json(new
                {
                    status = false,
                    message = "TT này không tồn tại."
                });

            TT621 tT621 = await _tT621Service.FindById_Include(tt621Id);

            if (tT621 == null)
                return Json(new
                {
                    status = false,
                    message = "TT này không tồn tại."
                });

            try
            {
                await _tT621Service.DeleteAsync(tT621);
                // nếu ketchuyen roi ==> ko xoá được

                // capnhat SoTU_DaTT vào kvctptc
                IEnumerable<TT621> tT621s = await _tT621Service.FindBySoCT(tT621.SoCT, user.Macn);
                if (tT621s.Count() == 0)
                {
                    KVCTPTC kVCTPTC = await _kVCTPTCService.GetById(kVCTPTCId_PhieuTC); // kVCTPCTId_PhieuTC
                    kVCTPTC.SoTT_DaTao = "";
                    await _kVCTPTCService.UpdateAsync(kVCTPTC);
                    return Json(new
                    {
                        status = true,
                        tT621sCount = ""
                    });
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

        public IActionResult GetKhachHangs_HDVATOB_By_Code_CapNhatCTTT(string code, int page = 1)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            code ??= "";

            //TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName(code, user.Macn);
            TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName_PagedList(code, user.Macn, page);
            TT621VM.MaKhText = code;
            return PartialView(TT621VM);
        }

        public IActionResult GetKhachHangs_HDVATOB_By_Code_ThemMoiCTTT_ContextMenu(string code, int page = 1)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            code ??= "";
            //TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName(code, user.Macn);
            TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName_PagedList(code, user.Macn, page);
            TT621VM.MaKhText = code;
            return PartialView(TT621VM);
        }

        public IActionResult GetKhachHangs_HDVATOB_By_Code_ThemMoiCTTT(string code, int page = 1)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            code ??= "";
            //TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName(code, user.Macn);
            TT621VM.KhachHangs_HDVATOB = _kVCTPTCService.GetSuppliersByCodeName_PagedList(code, user.Macn, page);
            TT621VM.MaKhText = code;
            return PartialView(TT621VM);
        }

        private void Get_TkNo_TkCo()
        {
            DmTk dmTk = new DmTk() { Tkhoan = "" };
            //var dmTks_TienMat = _kVCTPTCService.GetAll_DmTk_TienMat().ToList();
            var dmTks_TaiKhoan = _kVCTPTCService.GetAll_DmTk_TaiKhoan().ToList();
            //dmTks_TienMat.Insert(0, dmTk);
            dmTks_TaiKhoan.Insert(0, dmTk);

            //// do trong CTTT (tt621) có thể là của phiếu T or phiếu C
            TT621VM.DmTks_TkNo = dmTks_TaiKhoan;
            TT621VM.DmTks_TkCo = dmTks_TaiKhoan;
            //if (TT621VM.KVCTPCT.KVPCT.MFieu == "T")
            //{
            //    TT621VM.DmTks_TkNo = dmTks_TienMat;
            //    TT621VM.DmTks_TkCo = dmTks_TaiKhoan;
            //}
            //else
            //{
            //    TT621VM.DmTks_TkNo = dmTks_TaiKhoan;
            //    TT621VM.DmTks_TkCo = dmTks_TienMat;
            //}
        }

        private decimal soTienCanKetChuyen;

        public async Task<JsonResult> GetCommentText_By_TamUng(long tamUngId, decimal soTienNT, string loaiPhieu) // tamUngId == kvctpctId
        {
            var tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            decimal soTienNTTrongTT621_TheoTamUng = _tT621Service.GetSoTienNT_TrongTT621_TheoTamUng(tamUngId);
            string commentText;

            if (loaiPhieu == "C") // phieu C
            {
                soTienCanKetChuyen = tamUng.ConLaiNT.Value + soTienNT - soTienNTTrongTT621_TheoTamUng;
                if (tamUng.LoaiTien == "VND") // VND
                {
                    commentText = "Tạm ứng " + tamUng.SoCT + " còn nợ " + tamUng.ConLaiNT.Value.ToString("N0") + " số tiền cần kết chuyển 1411: "
                                                  + (tamUng.ConLaiNT.Value + soTienNT - soTienNTTrongTT621_TheoTamUng).ToString("N0");

                    //commentText = "Tạm ứng " + tamUng.SoCT + " còn nợ " + tamUng.SoTienNT.Value.ToString("N0") + " số tiền cần kết chuyển 1411: "
                    //                              + (tamUng.SoTienNT.Value + soTienNT - soTienNTTrongTT621_TheoTamUng).ToString("N0");
                }
                else // NgoaiTe
                {
                    commentText = "Tạm ứng " + tamUng.SoCT + " còn nợ " + tamUng.ConLaiNT.Value.ToString("N0")
                        + tamUng.LoaiTien
                        + " số tiền cần kết chuyển 1412: "
                                                  + (tamUng.ConLaiNT.Value + soTienNT - soTienNTTrongTT621_TheoTamUng).ToString("N0");

                    //commentText = "Tạm ứng " + tamUng.SoCT + " còn nợ " + tamUng.SoTienNT.Value.ToString("N0")
                    //    + tamUng.LoaiTien
                    //    + " số tiền cần kết chuyển 1412: "
                    //                              + (tamUng.SoTienNT.Value + soTienNT - soTienNTTrongTT621_TheoTamUng).ToString("N0");
                }
            }
            else // phieu T và chac chan' cho KhongTC
            {
                soTienCanKetChuyen = tamUng.ConLaiNT.Value - soTienNT - soTienNTTrongTT621_TheoTamUng;
                if (tamUng.LoaiTien == "VND") // VND
                {
                    commentText = "Tạm ứng " + tamUng.SoCT + " còn nợ " + tamUng.ConLaiNT.Value.ToString("N0") + " số tiền cần kết chuyển 1411: "
                                  + (tamUng.ConLaiNT.Value - soTienNT - soTienNTTrongTT621_TheoTamUng).ToString("N0");

                    //commentText = "Tạm ứng " + tamUng.SoCT + " còn nợ " + tamUng.SoTienNT.Value.ToString("N0") + " số tiền cần kết chuyển 1411: "
                    //              + (tamUng.SoTienNT.Value - soTienNT - soTienNTTrongTT621_TheoTamUng).ToString("N0");
                }
                else // NgoaiTe
                {
                    commentText = "Tạm ứng " + tamUng.SoCT + " còn nợ "
                        + tamUng.ConLaiNT.Value.ToString("N0") + tamUng.LoaiTien + " số tiền cần kết chuyển 1412: "
                                  + (tamUng.ConLaiNT.Value - soTienNT - soTienNTTrongTT621_TheoTamUng).ToString("N0");
                    //commentText = "Tạm ứng " + tamUng.SoCT + " còn nợ "
                    //    + tamUng.SoTienNT.Value.ToString("N0") + tamUng.LoaiTien + " số tiền cần kết chuyển 1412: "
                    //              + (tamUng.SoTienNT.Value - soTienNT - soTienNTTrongTT621_TheoTamUng).ToString("N0");
                }
            }

            return Json(commentText);
        }

        public async Task<JsonResult> GetTT621s_By_TamUng(long tamUngId)
        {
            var tT621s = await _tT621Service.GetTT621s_By_TamUng(tamUngId);
            //// get all && delete nhung TT theo TU truoc do ma chua KetChuyen
            //await _tT621Service.DeleteRangeAsync(tT621s);
            //// get all && delete nhung TT theo TU truoc do ma chua KetChuyen

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

        [HttpPost]
        public JsonResult Check_KetChuyenBtnStatus(long tamUngId, decimal soTienNT_Tren_TT621Create, string loaiPhieu)
        {
            decimal soTienNT_CanKetChuyen = _tT621Service.Get_SoTienNT_CanKetChuyen(
                tamUngId, soTienNT_Tren_TT621Create, loaiPhieu);
            if (soTienNT_CanKetChuyen == 0)
            {
                return Json(false); // btn on
            }
            return Json(true); // btn off
        }

        [HttpPost]
        public JsonResult Check_BtnThemMoiCTTT_Status(long tamUngId, decimal soTienNT_Tren_TT621Create, string loaiPhieu)
        {
            decimal soTienNT_CanKetChuyen = _tT621Service.Get_SoTienNT_CanKetChuyen(
                tamUngId, soTienNT_Tren_TT621Create, loaiPhieu);
            if (soTienNT_CanKetChuyen <= 0)
            {
                return Json(true); // btn on
            }
            return Json(false); // btn off
        }

        [HttpPost]
        public async Task<JsonResult> KetChuyen(long tamUngId, decimal soTienNT_PhieuTC, long kVCTPCTId_PhieuTC) // kVCTPCTId_PhieuTC: tren TT621CreateView
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            KVCTPTC kVCTPCT = new KVCTPTC();
            if (kVCTPCTId_PhieuTC == 0) // trường hợp TT 141 Khong TC: khong phieu TC
            {
                kVCTPCT.SoCT = "KHONG PTC";// m.phieutc='KHONG PTC'
            }
            else
            {
                kVCTPCT = await _kVCTPTCService.GetById(kVCTPCTId_PhieuTC);
            }

            TamUng tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            decimal soTienNTTrongTT621_TheoTamUng = _tT621Service.GetSoTienNT_TrongTT621_TheoTamUng(tamUngId);

            //if (tamUng.SoTienNT == soTienNTTrongTT621_TheoTamUng + soTienNT_PhieuTC)
            if (soTienCanKetChuyen == 0)
            {
                string log = "";
                log = System.Environment.NewLine;
                log += "=============";
                log += System.Environment.NewLine;
                log += " -User kết chuyển: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                tamUng.LogFile = tamUng.LogFile + log;

                tamUng.ConLaiNT = 0;
                tamUng.ConLai = 0;
                tamUng.PhieuTT = kVCTPCT.SoCT;
                await _tamUngService.UpdateAsync(tamUng);

                // capnhat SoTU_DaTT vào kvctptc
                kVCTPCT.SoTU_DaTT = tamUng.SoCT;
                await _kVCTPTCService.UpdateAsync(kVCTPCT);

                return Json(true);
            }
            return Json(false);
        }

        public JsonResult Gang_SoTienNT_CanKetChuyen(long tamUngId, decimal soTienNT_Tren_TT621Create, string loaiPhieu)
        {
            decimal soTienNT_CanKetChuyen = _tT621Service.Get_SoTienNT_CanKetChuyen(
                tamUngId, soTienNT_Tren_TT621Create, loaiPhieu);
            return Json(soTienNT_CanKetChuyen);
        }

        [HttpPost]
        public JsonResult Check_ThuHoanUngBtnStatus(long tamUngId)
        {
            var tT621s = _tT621Service.FindByTamUngId(tamUngId);
            if (tT621s.Count() == 0)
            {
                return Json(false); // btn on
            }
            return Json(true); // btn off
        }

        [HttpPost]
        public async Task<JsonResult> ThuHoanUng(long tamUngId, string soTienNT, string soCTThu, long kvctptcId) // chi VND
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            TamUng tamUng = await _tamUngService.GetByIdAsync(tamUngId);
            string conLaiNTOld = tamUng.ConLaiNT.ToString();
            string conLaiOld = tamUng.ConLai.ToString();

            if (tamUng == null)
                return Json(new
                {
                    status = false,
                    message = "Tạm ứng không tồn tại!"
                });
            else
            {
                //tamUng.SoTienNT = tamUng.SoTienNT - decimal.Parse(soTienNT);
                //tamUng.SoTien = tamUng.SoTien - decimal.Parse(soTienNT);
                tamUng.ConLaiNT -= decimal.Parse(soTienNT);
                tamUng.ConLai -= decimal.Parse(soTienNT);

                string temp = "";
                //temp += String.Format("- SoTienNT thay đổi: {0}->{1}", soTienNTOld, soTienNT);
                //temp += String.Format("- SoTien thay đổi: {0}->{1}", soTienOld, soTienNT);

                temp += String.Format("- ConLaiNT thay đổi: {0}->{1}", conLaiNTOld, tamUng.ConLaiNT);
                temp += String.Format("- ConLai thay đổi: {0}->{1}", conLaiOld, tamUng.ConLai);

                // kiem tra thay doi
                if (temp.Length > 0)
                {
                    string log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User thu hoàn ứng: " + user.Username + ", KVCTPTC thu: " + soCTThu + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    tamUng.LogFile = tamUng.LogFile + log;
                }

                await _tamUngService.UpdateAsync(tamUng);

                // cap nhat lai kvctptc
                KVCTPTC kVCTPTC = await _kVCTPTCService.GetById(kvctptcId); // tamUngId == kvctptcId
                kVCTPTC.HoanUngTU = tamUng.SoCT;
                kVCTPTC.LogFile += " -User thu hoàn ứng: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                await _kVCTPTCService.UpdateAsync(kVCTPTC);

                return Json(new
                {
                    status = true
                });
            }
        }
    }
}