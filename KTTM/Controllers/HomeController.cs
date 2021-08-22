﻿using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Data.Repository;
using KTTM.Services;
using Data.Utilities;
using KTTM.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Data.Models_KTTM_Anhson;

namespace KTTM.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IKVPTCService _kVPTCService;
        private readonly IKVCTPTCService _kVCTPTCService;
        private readonly kttm_anhSonContext _kttm_AnhSonContext;
        private readonly ITamUngService _tamUngService;
        private readonly ITT621Service _tT621Service;
        private readonly ITonQuyService _tonQuyService;

        [BindProperty]
        public HomeViewModel HomeVM { get; set; }

        public HomeController(IKVPTCService kVPTCService, IKVCTPTCService kVCTPTCService, kttm_anhSonContext kttm_AnhSonContext, ITamUngService tamUngService, ITT621Service tT621Service, ITonQuyService tonQuyService)
        {
            _kVPTCService = kVPTCService;
            _kVCTPTCService = kVCTPTCService;
            _kttm_AnhSonContext = kttm_AnhSonContext;
            _tamUngService = tamUngService;
            _tT621Service = tT621Service;
            _tonQuyService = tonQuyService;
            HomeVM = new HomeViewModel()
            {
                KVPTC = new Data.Models_KTTM.KVPTC()
            };
        }

        //-----------LayDataCashierPartial------------

        public async Task<IActionResult> Index(string searchString, string searchFromDate, string searchToDate, string boolSgtcode, Guid id, int page = 1)
        {
            //List<Tonquy> tonquiesAnhSon = _kttm_AnhSonContext.Tonquies.ToList();
            //List<TonQuy> tonQuies = new List<TonQuy>();
            //foreach(var item in tonquiesAnhSon)
            //{
            //    tonQuies.Add(new TonQuy()
            //    {
            //        LoaiTien = item.Loaitien.Trim(),
            //        LogFile = "==== chuyển từ data anh Sơn.",
            //        NgayCT = item.Ngayct,
            //        SoTien = item.Sotien.Value,
            //        SoTienNT = item.Sotiennt.Value,
            //        TyGia = item.Tygia.Value
            //    });
            //}
            //var abc = "";
            //await _tonQuyService.CreateRangeAsync(tonQuies);


            //List<TamUng> tamUngs = _tamUngService.GetAll().ToList();
            //List<Tt621> tt621sAnhSon = _kttm_AnhSonContext.Tt621s.ToList();
            //List<TT621> tT621s = new List<TT621>();

            //foreach (var tamUng in tamUngs)
            //{
            //    IEnumerable<Tt621> tt621sAnhSon1 = tt621sAnhSon.Where(x => x.Phieutu.Trim() == tamUng.SoCT.Trim());
            //    if (tt621sAnhSon1.Count() > 0)
            //    {
            //        foreach (var item in tt621sAnhSon1)
            //        {
            //            tT621s.Add(new TT621()
            //            {
            //                BoPhan = item.Bophan.Trim(),
            //                CoQuay = item.Coquay.Trim(),
            //                DiaChi = item.Diachi.Trim(),
            //                DienGiai = item.Diengiai.Trim(),
            //                DienGiaiP = item.Diengiaip.Trim(),
            //                DieuChinh = item.Dieuchinh,
            //                DSKhongVAT = item.Dskhongvat,
            //                GhiSo = item.Ghiso.Trim(),
            //                HoaDonDT = item.Hoadondt.Trim(),
            //                HTTC = item.Httc.Trim(),
            //                KyHieu = item.Kyhieu.Trim(),
            //                KyHieuHD = item.Kyhieuhd.Trim(),
            //                LapPhieu = item.Lapphieu.Trim(),
            //                LoaiHDGoc = item.Loaihdgoc.Trim(),
            //                LoaiTien = item.Loaitien.Trim(),
            //                LogFile = "==== chuyển từ data anh Sơn",
            //                MaKhCo = item.Makhco.Trim(),
            //                MaKhNo = item.Makhno.Trim(),
            //                MatHang = item.Mathang.Trim(),
            //                MauSoHD = item.Mausohd.Trim(),
            //                MsThue = item.Msthue.Trim(),
            //                NgayCT = item.Ngayct,
            //                NgayCTGoc = item.Ngayctgoc,
            //                NoQuay = item.Noquay.Trim(),
            //                PhieuTC = item.Phieutc.Trim(),
            //                PhieuTU = item.Phieutu.Trim(),
            //                Sgtcode = item.Sgtcode.Trim(),
            //                SoCT = item.Soct.Trim(),
            //                SoCTGoc = item.Soctgoc.Trim(),
            //                SoTien = item.Sotien,
            //                SoTienNT = item.Sotiennt,
            //                SoXe = item.Soxe.Trim(),
            //                TamUngId = tamUng.Id,
            //                TenKH = item.Tenkh.Trim(),
            //                TKCo = item.Tkco.Trim(),
            //                TKNo = item.Tkno.Trim(),
            //                TyGia = item.Tygia,
            //                VAT = item.Vat

            //            });
            //        }
            //    }


            //}
            //var abc = "";
            //await _tT621Service.CreateRangeAsync(tT621s);


            //var list = _kttm_AnhSonContext.Tamungs.ToList();
            //var listKVCTPTC = _kVCTPTCService.GetAll().ToList();
            //var listKVPTC = _kVPTCService.GetAll().ToList();

            //List<TamUng> tamUngs = new List<TamUng>();
            //foreach (var tamUng in list)
            //{
            //    var kVCTPTC = listKVCTPTC.Where(x => x.KVPTCId == tamUng.Phieuchi)
            //               .Where(x => x.TKNo.Trim() == "1411" || x.TKNo.Trim() == "1412").FirstOrDefault();
            //    //list = list.Where(x => x.Phieuchi == kVPTC.SoCT).ToList();
            //    //.Where(x => x.Tkno.StartsWith("141")).ToList();
            //    if (list.Count > 0)
            //    {

            //        tamUngs.Add(new TamUng()
            //        {
            //            ConLai = tamUng.Conlai,
            //            ConLaiNT = tamUng.Conlaint,
            //            DienGiai = tamUng.Diengiai.Trim(),
            //            Id = kVCTPTC.Id,
            //            LoaiTien = tamUng.Loaitien.Trim(),
            //            LogFile = "==== chuyển từ data anh Sơn.",
            //            MaKhNo = tamUng.Makhno.Trim(),
            //            NgayCT = tamUng.Ngayct,
            //            PhieuChi = tamUng.Phieuchi.Trim(),
            //            PhieuTT = tamUng.Phieutt.Trim(),
            //            Phong = tamUng.Phong.Trim(),
            //            SoCT = tamUng.Soct.Trim(),
            //            SoTien = tamUng.Sotien,
            //            SoTienNT = tamUng.Sotiennt,
            //            TKCo = tamUng.Tkco.Trim(),
            //            TKNo = tamUng.Tkno.Trim(),
            //            TTTP = tamUng.Tttp,
            //            TyGia = tamUng.Tygia

            //        });

            //    }

            //}

            //var bac = "";
            //await _tamUngService.CreateRangeAsync(tamUngs);

            //var kvptcs = _kVPTCService.GetAll();
            //var kvctptcs = _kttm_AnhSonContext.Kvctptcs.ToList();

            //List<KVCTPTC> kVCTPTCs = new List<KVCTPTC>();

            //foreach (var kvptc in kvptcs)
            //{
            //    var kvctptcs1 = kvctptcs.Where(x => x.Soct == kvptc.SoCT).ToList();
            //    if (kvctptcs1.Count > 0)
            //    {
            //        foreach (var item in kvctptcs1)
            //        {
            //            kVCTPTCs.Add(new KVCTPTC()
            //            {
            //                BoPhan = item.Bophan.Trim(),
            //                CardNumber = item.Cardnumber.Trim(),
            //                CoQuay = item.Coquay.Trim(),
            //                DiaChi = item.Diachi.Trim(),
            //                DienGiai = item.Diengiai.Trim(),
            //                DienGiaiP = item.Diengiaip.Trim(),
            //                DieuChinh = item.Dieuchinh,
            //                DSKhongVAT = item.Dskhongvat,
            //                HoaDonDT = item.Hoadondt.Trim(),
            //                HTTC = item.Httc.Trim(),
            //                HTTT = item.Httt.Trim(),
            //                KC141 = item.Kc141,
            //                KhoangMuc = item.Khoanmuc.Trim(),
            //                KVPTCId = item.Soct.Trim(),
            //                KyHieu = item.Kyhieu.Trim(),
            //                LoaiHDGoc = item.Loaihdgoc.Trim(),
            //                LoaiTien = item.Loaitien.Trim(),
            //                LogFile = "==== chuyển từ data anh Sơn.",
            //                MaKh = item.Makh.Trim(),
            //                MaKhCo = item.Makhco.Trim(),
            //                MaKhNo = item.Makhno.Trim(),
            //                MatHang = item.Mathang.Trim(),
            //                MauSoHD = item.Mausohd.Trim(),
            //                MsThue = item.Msthue.Trim(),
            //                NgayCTGoc = item.Ngayctgoc,
            //                NoQuay = item.Noquay.Trim(),
            //                SalesSlip = item.Salesslip.Trim(),
            //                Sgtcode = item.Sgtcode.Trim(),
            //                SoCTGoc = item.Soctgoc.Trim(),
            //                SoTien = item.Sotien,
            //                SoTienNT = item.Sotiennt,
            //                SoXe = item.Soxe.Trim(),
            //                STT = item.Stt.Trim(),
            //                TamUng = item.Tamung.Trim(),
            //                TenKH = item.Tenkh.Trim(),
            //                TKCo = item.Tkco.Trim(),
            //                TKNo = item.Tkno.Trim(),
            //                TyGia = item.Tygia,
            //                VAT = item.Vat
            //            });
            //        }

            //    }
            //}

            //string abc = "";

            //await _kVCTPTCService.CreateRange(kVCTPTCs);

            //var kvptcAnhSon = _kttm_AnhSonContext.Kvptcs.ToList();
            //List<KVPTC> kVPTCs = new List<KVPTC>();
            //foreach(var item in kvptcAnhSon)
            //{
            //    kVPTCs.Add(new KVPTC()
            //    {
            //        Create = item.Create,
            //        DonVi = item.Donvi.Trim(),
            //        HoTen = item.Hoten.Trim(),
            //        LapPhieu = item.Lapphieu.Trim(),
            //        Lock = item.Lock,
            //        Locker = item.Locker.Trim(),
            //        LogFile = "==== chuyển từ data anh Sơn.",
            //        MayTinh = item.Maytinh.Trim(),
            //        MFieu = item.Mfieu.Trim(),
            //        NgayCT = item.Ngayct,
            //        NgoaiTe = item.Ngoaite.Trim(),
            //        Phong = item.Phong.Trim(),
            //        SoCT = item.Soct.Trim()
            //    });
            //}

            //await _kVPTCService.CreateRangeAsync(kVPTCs);

            HomeVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            HomeVM.Page = page;

            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            ViewBag.boolSgtcode = boolSgtcode;

            if (id == null) // for redirect with id
            {
                HomeVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(id);
            }
            else
            {
                HomeVM.KVPTC = new KVPTC();
            }
            HomeVM.KVPTCDtos = await _kVPTCService.ListKVPTC(searchString, searchFromDate, searchToDate, boolSgtcode, page);
            return View(HomeVM);
        }

        public IActionResult Create(string strUrl)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            //if (user.Role.RoleName == "KeToans")
            //{
            //    return View("~/Views/Shared/AccessDenied.cshtml");
            //}

            HomeVM.StrUrl = strUrl;
            HomeVM.KVPTC.NgayCT = DateTime.Now;
            HomeVM.KVPTC.DonVi = "CÔNG TY TNHH MỘT THÀNH VIÊN DỊCH VỤ LỮ HÀNH SAIGONTOURIST";
            HomeVM.KVPTC.Create = DateTime.Now;
            HomeVM.KVPTC.LapPhieu = user.Username;

            HomeVM.LoaiTiens = _kVPTCService.ListLoaiTien();
            HomeVM.LoaiPhieus = _kVPTCService.ListLoaiPhieu();
            //HomeVM.Phongbans = _kVPTCService.GetAllPhongBan();
            HomeVM.Phongbans = _kVCTPTCService.GetAll_PhongBans();

            return View(HomeVM);
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                HomeVM = new HomeViewModel()
                {
                    KVPTC = new KVPTC(),
                    LoaiTiens = _kVPTCService.ListLoaiTien(),
                    LoaiPhieus = _kVPTCService.ListLoaiPhieu(),
                    //Phongbans = _kVPTCService.GetAllPhongBan(),
                    Phongbans = _kVCTPTCService.GetAll_PhongBans(),
                    StrUrl = strUrl
                };

                return View(HomeVM);
            }

            HomeVM.KVPTC.Create = DateTime.Now;
            HomeVM.KVPTC.LapPhieu = user.Username;

            // next SoCT --> bat buoc phai co'
            switch (HomeVM.KVPTC.MFieu)
            {
                case "T": // thu
                    switch (HomeVM.KVPTC.NgoaiTe)
                    {
                        case "VN":
                            HomeVM.KVPTC.SoCT = _kVPTCService.GetSoCT("QT", user.Macn); // thu VND
                            break;
                        default:
                            HomeVM.KVPTC.SoCT = _kVPTCService.GetSoCT("NT", user.Macn); // thu NgoaiTe
                            break;
                    }
                    break;
                default: // chi
                    switch (HomeVM.KVPTC.NgoaiTe)
                    {
                        case "VN":
                            HomeVM.KVPTC.SoCT = _kVPTCService.GetSoCT("QC", user.Macn); // chi VND
                            break;
                        default:
                            HomeVM.KVPTC.SoCT = _kVPTCService.GetSoCT("NC", user.Macn); // chi NgoaiTe
                            break;
                    }
                    break;
            }
            // next SoCT

            HomeVM.KVPTC.LapPhieu = user.Username;

            //// May tinh
            //var computerName = Environment.MachineName;
            //var userName = Environment.UserName;
            //var osVersion = Environment.OSVersion;
            //var domainName = Environment.UserDomainName;

            //string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            //// Get the IP  
            ////var info = Dns.GetHostByName(hostName).AddressList;//.ToList();
            //var info = Dns.GetHostEntry(hostName).AddressList;
            ////string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            //string myIP = Dns.GetHostEntry(hostName).AddressList[3].ToString();

            //HomeVM.KVPCT.MayTinh = computerName + "|" + userName + "|" + myIP + "|" + domainName;


            // ghi log
            HomeVM.KVPTC.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _kVPTCService.CreateAsync(HomeVM.KVPTC); // save

                SetAlert("Thêm mới thành công.", "success");

                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(HomeVM);
            }
        }

        public async Task<IActionResult> Edit(Guid id, string strUrl)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            HomeVM.StrUrl = strUrl;
            if (id == null)
            {
                ViewBag.ErrorMessage = "Phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            HomeVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(id);

            if (HomeVM.KVPTC == null)
            {
                ViewBag.ErrorMessage = "Phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            HomeVM.LoaiTiens = _kVPTCService.ListLoaiTien();
            HomeVM.LoaiPhieus = _kVPTCService.ListLoaiPhieu();
            //HomeVM.Phongbans = _kVPTCService.GetAllPhongBan();
            HomeVM.Phongbans = _kVCTPTCService.GetAll_PhongBans();

            return View(HomeVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            string temp = "", log = "";

            if (id != HomeVM.KVPTC.Id)
            {
                ViewBag.ErrorMessage = "Phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            if (ModelState.IsValid)
            {
                HomeVM.KVPTC.NgaySua = DateTime.Now;
                HomeVM.KVPTC.NguoiSua = user.Username;

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _kVPTCService.GetByIdAsNoTracking(id);

                if (t.HoTen != HomeVM.KVPTC.HoTen)
                {
                    temp += String.Format("- Họ tên thay đổi: {0}->{1}", t.HoTen, HomeVM.KVPTC.HoTen);
                }

                if (t.Phong != HomeVM.KVPTC.Phong)
                {
                    temp += String.Format("- Phòng thay đổi: {0}->{1}", t.Phong, HomeVM.KVPTC.Phong);
                }

                if (t.DonVi != HomeVM.KVPTC.DonVi)
                {
                    temp += String.Format("- Đơn vị thay đổi: {0}->{1}", t.DonVi, HomeVM.KVPTC.DonVi);
                }

                #endregion
                // kiem tra thay doi
                if (temp.Length > 0)
                {

                    log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật tour: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    HomeVM.KVPTC.LogFile = t.LogFile;
                }

                try
                {
                    await _kVPTCService.UpdateAsync(HomeVM.KVPTC);
                    SetAlert("Cập nhật thành công", "success");

                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");

                    return View(HomeVM);
                }
            }
            // not valid

            return View(HomeVM);
        }

        public IActionResult DetailsRedirect(string strUrl/*, string tabActive*/)
        {
            //if (!string.IsNullOrEmpty(tabActive))
            //{
            //    strUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab
            //}
            return Redirect(strUrl);
        }

    }
}