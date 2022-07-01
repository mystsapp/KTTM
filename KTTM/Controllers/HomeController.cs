using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_KTTM_1;
using Data.Models_QLTaiKhoan;
using Data.Utilities;
using KTTM.Models;
using KTTM.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NumToWords;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//using Data.Models_KTTM_1;

namespace KTTM.Controllers
{
    public class HomeController : BaseController
    {
        //private readonly KTTM_1Context _kTTM_1Context;
        private readonly IKVPTCService _kVPTCService;

        private readonly IKVCTPTCService _kVCTPTCService;
        private readonly ITamUngService _tamUngService;
        private readonly ITT621Service _tT621Service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITonQuyService _tonQuyService;
        private readonly KTTM_anhsonContext _kTTM_1Context;

        [BindProperty]
        public HomeViewModel HomeVM { get; set; }

        public HomeController(IKVPTCService kVPTCService, IKVCTPTCService kVCTPTCService,
            ITamUngService tamUngService, ITT621Service tT621Service, IWebHostEnvironment webHostEnvironment,
            ITonQuyService tonQuyService, KTTM_anhsonContext kTTM_1Context)
        {
            //_kTTM_1Context = kTTM_1Context;
            _kVPTCService = kVPTCService;
            _kVCTPTCService = kVCTPTCService;
            _tamUngService = tamUngService;
            _tT621Service = tT621Service;
            _webHostEnvironment = webHostEnvironment;
            _tonQuyService = tonQuyService;
            _kTTM_1Context = kTTM_1Context;
            HomeVM = new HomeViewModel()
            {
                KVPTC = new Data.Models_KTTM.KVPTC()
            };
        }

        //-----------LayDataCashierPartial------------

        public async Task<IActionResult> Index(string searchString, string searchFromDate, string searchToDate,
            string boolSgtcode, string boolTkNo1311, Guid id, int page = 1) // boolSgtcode: // search for chitiet in kvctptC
        {
            if (id == Guid.Empty)
            {
                ViewBag.id = "";
            }

            #region chuyen dulieu

            //List<Data.Models_KTTM_1.Tonquy> tonquiesAnhSon = _kTTM_1Context.Tonquies.ToList();
            //List<Data.Models_KTTM.TonQuy> tonQuies = new List<Data.Models_KTTM.TonQuy>();
            //foreach (var item in tonquiesAnhSon)
            //{
            //    tonQuies.Add(new Data.Models_KTTM.TonQuy()
            //    {
            //        LoaiTien = item.Loaitien.Trim(),
            //        LogFile = "==== chuyển từ data anh Sơn.",
            //        NgayCT = item.Ngayct,
            //        SoTien = item.Sotien.Value,
            //        SoTienNT = item.Sotiennt.Value,
            //        TyGia = item.Tygia.Value,
            //        MaCn = "STC"
            //    });
            //}
            //var abc = "";
            //await _tonQuyService.CreateRangeAsync(tonQuies);

            //List<Data.Models_KTTM.TamUng> tamUngs = _tamUngService.FindByMaCn("STN");
            ////List<Data.Models_KTTM.TamUng> tamUngs = _tamUngService.GetAll().ToList();
            //List<Tt621> tt621sAnhSon = _kTTM_1Context.Tt621s.ToList();
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
            //                DieuChinh = item.Dieuchinh.Value,
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
            //                VAT = item.Vat,
            //                MaCn = tamUng.MaCn
            //            });
            //        }
            //    }
            //}
            //var abc = "";
            //await _tT621Service.CreateRangeAsync(tT621s);

            //var list = _kTTM_1Context.Tamungs.ToList();
            //List<KVCTPTC> listKVCTPTC = _kVCTPTCService.FindByMaCN("STC");
            ////var listKVCTPTC = _kVCTPTCService.GetAll().ToList();
            //var listKVPTC = _kVPTCService.FindByMaCN("STC");
            ////var listKVPTC = _kVPTCService.GetAll().ToList();

            //List<Data.Models_KTTM.TamUng> tamUngs = new List<Data.Models_KTTM.TamUng>();
            //foreach (var tamUng in list)
            //{
            //    var kVCTPTC = listKVCTPTC.Where(x => x.SoCT == tamUng.Phieuchi)
            //               .Where(x => x.TKNo.Trim() == "1411" || x.TKNo.Trim() == "1412").FirstOrDefault();
            //    //list = list.Where(x => x.Phieuchi == kVPTC.SoCT).ToList();
            //    //.Where(x => x.Tkno.StartsWith("141")).ToList();
            //    if (list.Count > 0)
            //    {
            //        tamUngs.Add(new Data.Models_KTTM.TamUng()
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
            //            TyGia = tamUng.Tygia,
            //            MaCn = kVCTPTC.MaCn
            //        });
            //    }
            //}

            //var bac = "";
            //await _tamUngService.CreateRangeAsync(tamUngs);

            ////var kvptcs = _kVPTCService.GetAll().ToList();
            //List<KVPTC> kvptcs = _kVPTCService.FindByMaCN("STC");
            //var kvctptcs = _kTTM_1Context.Kvctpcts.ToList();
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
            //                DieuChinh = item.Dieuchinh.Value,
            //                DSKhongVAT = item.Dskhongvat.Value,
            //                HoaDonDT = item.Hoadondt.Trim(),
            //                HTTC = item.Httc.Trim(),
            //                HTTT = item.Httt.Trim(),
            //                KC141 = item.Kc141,
            //                KhoangMuc = item.Khoanmuc.Trim(),
            //                KVPTCId = kvptc.Id,
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
            //                SoCT = kvptc.SoCT.Trim(),
            //                STT = item.Stt.Trim(),
            //                TamUng = item.Tamung.Trim(),
            //                TenKH = item.Tenkh.Trim(),
            //                TKCo = item.Tkco.Trim(),
            //                TKNo = item.Tkno.Trim(),
            //                TyGia = item.Tygia,
            //                VAT = item.Vat,
            //                MaCn = kvptc.MaCn
            //            });
            //        }
            //    }
            //}

            //string abc = "";

            //await _kVCTPTCService.CreateRange(kVCTPTCs);

            //var kvptcAnhSon = _kTTM_1Context.Kvpcts.ToList();
            //List<KVPTC> kVPTCs = new List<KVPTC>();
            //foreach (var item in kvptcAnhSon)
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
            //        SoCT = item.Soct.Trim(),
            //        MaCn = "STC"
            //    });
            //}

            //var abc = "";
            //await _kVPTCService.CreateRangeAsync(kVPTCs);

            #endregion chuyen dulieu

            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            HomeVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            HomeVM.Page = page;

            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            ViewBag.boolSgtcode = boolSgtcode;
            ViewBag.boolTkNo1311 = boolTkNo1311;

            if (id != Guid.Empty) // for redirect with id
            {
                HomeVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(id);
                ViewBag.id = HomeVM.KVPTC.Id;
            }
            else
            {
                HomeVM.KVPTC = new KVPTC();
            }
            HomeVM.KVPTCDtos = await _kVPTCService.ListKVPTC(searchString, searchFromDate,
                searchToDate, boolSgtcode, boolTkNo1311, page, user.Macn);
            return View(HomeVM);
        }

        public IActionResult Create(string strUrl)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid || HomeVM.KVPTC.NgayCT == null)
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
                ModelState.AddModelError("", "NgayCT không được để trống.");
                return View(HomeVM);
            }

            HomeVM.KVPTC.Create = DateTime.Now;
            HomeVM.KVPTC.LapPhieu = user.Username;
            HomeVM.KVPTC.MaCn = user.Macn;

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
            HomeVM.KVPTC.HoTen = String.IsNullOrEmpty(HomeVM.KVPTC.HoTen) ? "" : HomeVM.KVPTC.HoTen.ToUpper();

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
                KVPTC kVPTC = await _kVPTCService.CreateAsync_ReturnEntity(HomeVM.KVPTC); // save

                SetAlert("Thêm mới thành công.", "success");

                //return Redirect(strUrl);
                return RedirectToAction(nameof(Index), new { id = kVPTC.Id });
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
            if (id == Guid.Empty)
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
            HomeVM.KVCTPTCs = await _kVCTPTCService.FinBy_KVPTCId(id);

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
                HomeVM.KVPTC.HoTen = HomeVM.KVPTC.HoTen.Trim().ToUpper();

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

                #endregion log file

                // kiem tra thay doi
                if (temp.Length > 0)
                {
                    log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật : " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    HomeVM.KVPTC.LogFile = t.LogFile;
                }

                try
                {
                    await _kVPTCService.UpdateAsync(HomeVM.KVPTC);
                    SetAlert("Cập nhật thành công", "success");

                    //return Redirect(strUrl);
                    return RedirectToAction(nameof(Index), new { id = id });
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

        [HttpPost] // 1) phải là phiếu C, TKNo là 1411 or 1412
        public async Task<JsonResult> CheckInPhieu(Guid id)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            ////var tamUngs = await _tamUngService.Find_TamUngs_By_PhieuChi_Include(soCT, user.Macn);
            //List<KVCTPTC> kVCTPTCs = await _kVCTPTCService.FinBy_SoCT(soCT, user.Macn);
            //List<TamUng> tamUngs = new List<TamUng>();
            //foreach (var item in kVCTPTCs)
            //{
            //    if (item.TKNo == "1411" || item.TKCo == "1411" ||
            //        item.TKNo == "1412" || item.TKCo == "1412") // la phieu TU
            //    {
            //        // lay het chi tiet ma co' maKhNo co tkNo = 1411 || 1412 va tamung.contai > 0 (chua thanh toan het)
            //        if (soCT.Contains("C"))
            //        {
            //            var tamUngs1 = await _tamUngService.Find_TamUngs_By_MaKh_Include(item.MaKh,
            //            user.Macn, item.TKNo); // MaKh == MaKhNo && theo TK (ung VND or NgoaiTe)
            //            tamUngs = tamUngs1.ToList();
            //        }
            //        else
            //        {
            //            var tamUngs1 = await _tamUngService.Find_TamUngs_By_MaKh_Include(item.MaKh,
            //            user.Macn, item.TKCo); // MaKh == MaKhNo && theo TK (ung VND or NgoaiTe)
            //            tamUngs = tamUngs1.ToList();
            //        }
            //    }
            //}

            var kVPTC = await _kVPTCService.GetByGuidIdAsync(id);

            //if (kVPTC.MFieu == "C")
            //{
            var kVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(id);
            kVCTPTCs = kVCTPTCs.Where(x => x.TKNo == "1411" || x.TKNo == "1412" || x.TKCo == "1411" || x.TKCo == "1412"); // dang tao phieu TU
            if (kVCTPTCs.Count() > 0) //
            {
                kVCTPTCs = kVCTPTCs.Where(x => string.IsNullOrEmpty(x.TamUng)); // nhung phieu chua TU
                if (kVCTPTCs.Count() > 0) // chua ketchuyen TU -> kt xem ketchuyen TT141 chua
                {
                    // kiem tra xem có ketchuyen TT141 chua
                    var tamUngs = await _tamUngService.Find_TamUngs_By_PhieuTT(kVPTC.SoCT, kVPTC.MaCn);
                    if (tamUngs.Count() > 0)
                    {
                        //var kVCTPTCs1 = kVCTPTCs.Where(x => string.IsNullOrEmpty(x.HoanUngTU));
                        //if (kVCTPTCs1.Count() > 0)
                        //{
                        //    return Json(true);
                        //}
                        //else
                        //{
                        //    return Json(false);
                        //}

                        return Json(true); // ko cho inphieu
                    }
                    return Json(false);
                }
            }
            return Json(true);
            //}

            //return Json(true); // ko cho inphieu
        }

        public async Task<JsonResult> CheckPhieuHoan(Guid id)
        {
            var jsonResult = await CheckInPhieu(id);
            var boolResult = jsonResult.Value.ToString(); // false : cho in
            if (bool.Parse(boolResult) == false) // ko cho in
            {
                var kVPTC = await _kVPTCService.GetByGuidIdAsync(id);
                var kVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(id);
                kVCTPTCs = kVCTPTCs.Where(x => x.TKNo == "1411" || x.TKNo == "1412" || x.TKCo == "1411" || x.TKCo == "1412"); // dang tao phieu TU

                var kVCTPTCs1 = kVCTPTCs.Where(x => string.IsNullOrEmpty(x.HoanUngTU));
                if (kVCTPTCs1.Count() > 0)
                {
                    return Json(false);
                }
                else
                {
                    return Json(true);
                }
            }

            return Json(false); // cho in
        }

        public async Task<JsonResult> CheckSoLuongDong(Guid id)
        {
            var kVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(id);
            if (kVCTPTCs.Count() == 0) return Json(false);
            else return Json(true);
        }

        public async Task<IActionResult> InPhieuView(Guid id, int page)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");
            HomeVM.ChiNhanh = await _kVPTCService.GetChiNhanhByMaCn(user.Macn);

            HomeVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(id);
            HomeVM.KVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(id);
            HomeVM.Page = page;

            switch (HomeVM.KVPTC.MFieu)
            {
                case "T": // thu
                    switch (HomeVM.KVPTC.NgoaiTe)
                    {
                        case "VN":
                            HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
                            break;

                        default:
                            HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
                            break;
                    }
                    break;

                default: // chi
                    switch (HomeVM.KVPTC.NgoaiTe)
                    {
                        case "VN":

                            HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
                            // chi VND
                            break;

                        default:

                            HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
                            // chi NgoaiTe
                            break;
                    }
                    break;
            }

            string tongTienNT = HomeVM.InPhieuView_Groupby_TkNo_TkCos.Sum(x => x.SoTienNT).ToString();
            string bangChu = "";
            string tongCong = "";
            // VN
            if (HomeVM.KVPTC.NgoaiTe == "VN")
            {
                ///// Currency to money
                string s = SoSangChu.DoiSoSangChu(tongTienNT.Split('.')[0]);
                string c = AmountToWords.changeCurrencyToWords(tongTienNT.ToLower());
                //string t = String.IsNullOrEmpty(loaitien) ? "" : " Exchange rate USD/VND";
                bangChu = char.ToUpper(s[0]) + s.Substring(1) + " đồng";// + " / " + char.ToUpper(c[0]) + c.Substring(1).ToLower() + "vnd";
                tongCong = decimal.Parse(tongTienNT).ToString("N0") + " VND";
            }
            else
            {
                // NT
                //string soTienNT_BangChu = "";
                //foreach (var item in HomeVM.InPhieuView_Groupby_TkNo_TkCos)
                //{
                //    string tien = SoSangChu.DoiSoSangChu(item.SoTienNT.ToString().Split('.')[0]);
                //    item.SoTienNT_BangChu = char.ToUpper(tien[0]) + tien.Substring(1) + " " + item.LoaiTien;
                //}

                //foreach (var item in HomeVM.InPhieuView_Groupby_TkNo_TkCos)
                //{
                //    int lastItem = HomeVM.InPhieuView_Groupby_TkNo_TkCos.ToList().IndexOf(item);
                //    if (lastItem != HomeVM.InPhieuView_Groupby_TkNo_TkCos.Count() - 1)
                //    {
                //        // this is the last item
                //        soTienNT_BangChu += item.SoTienNT_BangChu + " + ";
                //        tongCong += @item.SoTienNT.ToString("N0") + " " + @item.LoaiTien + " + ";
                //    }
                //    else
                //    {
                //        soTienNT_BangChu += item.SoTienNT_BangChu;
                //        tongCong += @item.SoTienNT.ToString("N0") + " " + @item.LoaiTien;
                //    }
                //}
                //bangChu = soTienNT_BangChu;
                ////ViewBag.SoTienNT_BangChu = soTienNT_BangChu;

                //
                var inPhieuViews = _kVPTCService.InPhieuView_Groupby_LoaiTiens(HomeVM.KVCTPTCs);
                string soTienNT_BangChu = "";
                foreach (var item in inPhieuViews)
                {
                    string tien = SoSangChu.DoiSoSangChu(item.SoTienNT.ToString().Split('.')[0]);
                    item.SoTienNT_BangChu = char.ToUpper(tien[0]) + tien.Substring(1) + " " + item.LoaiTien;
                }
                foreach (var item in inPhieuViews)
                {
                    int lastItem = inPhieuViews.ToList().IndexOf(item);
                    if (lastItem != inPhieuViews.Count() - 1)
                    {
                        // this is the last item
                        soTienNT_BangChu += item.SoTienNT_BangChu + " + ";
                        tongCong += @item.SoTienNT.ToString("N0") + " " + @item.LoaiTien + " + ";
                    }
                    else
                    {
                        soTienNT_BangChu += item.SoTienNT_BangChu;
                        tongCong += @item.SoTienNT.ToString("N0") + " " + @item.LoaiTien;
                    }
                }
                bangChu = soTienNT_BangChu;
            }
            ViewBag.tongCong = tongCong;
            ViewBag.bangChu = bangChu;

            // them dong rong
            var inPhieuViewCount = HomeVM.InPhieuView_Groupby_TkNo_TkCos.Count();
            if (inPhieuViewCount < 4)
            {
                for (int i = 1; i <= 4 - inPhieuViewCount; i++)
                {
                    HomeVM.InPhieuView_Groupby_TkNo_TkCos.Add(new InPhieuView_Groupby_TkNo_TkCo() { TkNo = "", TkCo = "", SoTien = 0, SoTienNT = 0 });
                }
            }
            return View(HomeVM);
        }

        public async Task<IActionResult> InPhieuPrint(Guid id, int page)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");
            HomeVM.ChiNhanh = await _kVPTCService.GetChiNhanhByMaCn(user.Macn);

            HomeVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(id);
            HomeVM.KVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(id);
            HomeVM.Page = page;

            switch (HomeVM.KVPTC.MFieu)
            {
                case "T": // thu
                    switch (HomeVM.KVPTC.NgoaiTe)
                    {
                        case "VN":
                            HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
                            break;

                        default:
                            HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
                            break;
                    }
                    break;

                default: // chi
                    switch (HomeVM.KVPTC.NgoaiTe)
                    {
                        case "VN":

                            HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
                            // chi VND
                            break;

                        default:

                            HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
                            // chi NgoaiTe
                            break;
                    }
                    break;
            }

            string tongTienNT = HomeVM.InPhieuView_Groupby_TkNo_TkCos.Sum(x => x.SoTienNT).ToString();
            string bangChu = "";
            string tongCong = "";
            // VN
            if (HomeVM.KVPTC.NgoaiTe == "VN")
            {
                ///// Currency to money
                string s = SoSangChu.DoiSoSangChu(tongTienNT.Split('.')[0]);
                string c = AmountToWords.changeCurrencyToWords(tongTienNT.ToLower());
                //string t = String.IsNullOrEmpty(loaitien) ? "" : " Exchange rate USD/VND";
                bangChu = char.ToUpper(s[0]) + s.Substring(1) + " đồng";// + " / " + char.ToUpper(c[0]) + c.Substring(1).ToLower() + "vnd";
                tongCong = decimal.Parse(tongTienNT).ToString("N0") + " VND";
            }
            else
            {
                // NT
                string soTienNT_BangChu = "";
                foreach (var item in HomeVM.InPhieuView_Groupby_TkNo_TkCos)
                {
                    string tien = SoSangChu.DoiSoSangChu(item.SoTienNT.ToString().Split('.')[0]);
                    item.SoTienNT_BangChu = char.ToUpper(tien[0]) + tien.Substring(1) + " " + item.LoaiTien;
                }

                foreach (var item in HomeVM.InPhieuView_Groupby_TkNo_TkCos)
                {
                    int lastItem = HomeVM.InPhieuView_Groupby_TkNo_TkCos.ToList().IndexOf(item);
                    if (lastItem != HomeVM.InPhieuView_Groupby_TkNo_TkCos.Count() - 1)
                    {
                        // this is the last item
                        soTienNT_BangChu += item.SoTienNT_BangChu + " + ";
                        tongCong += @item.SoTienNT.ToString("N0") + " " + @item.LoaiTien + " + ";
                    }
                    else
                    {
                        soTienNT_BangChu += item.SoTienNT_BangChu;
                        tongCong += @item.SoTienNT.ToString("N0") + " " + @item.LoaiTien;
                    }
                }
                bangChu = soTienNT_BangChu;
                //ViewBag.SoTienNT_BangChu = soTienNT_BangChu;
            }
            ViewBag.tongCong = tongCong;
            ViewBag.bangChu = bangChu;

            // them dong rong
            var inPhieuViewCount = HomeVM.InPhieuView_Groupby_TkNo_TkCos.Count();
            if (inPhieuViewCount < 4)
            {
                for (int i = 1; i <= 4 - inPhieuViewCount; i++)
                {
                    HomeVM.InPhieuView_Groupby_TkNo_TkCos.Add(new InPhieuView_Groupby_TkNo_TkCo() { TkNo = "", TkCo = "", SoTien = 0, SoTienNT = 0 });
                }
            }
            return View(HomeVM);

            //HomeVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(id);
            //HomeVM.KVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(id);
            //HomeVM.Page = page;

            //switch (HomeVM.KVPTC.MFieu)
            //{
            //    case "T": // thu
            //        switch (HomeVM.KVPTC.NgoaiTe)
            //        {
            //            case "VN":
            //                HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
            //                break;

            //            default:
            //                HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
            //                break;
            //        }
            //        break;

            //    default: // chi
            //        switch (HomeVM.KVPTC.NgoaiTe)
            //        {
            //            case "VN":

            //                HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
            //                // chi VND
            //                break;

            //            default:

            //                HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNo_TkCos(HomeVM.KVCTPTCs);
            //                // chi NgoaiTe
            //                break;
            //        }
            //        break;
            //}

            //string tongTienNT = HomeVM.InPhieuView_Groupby_TkNo_TkCos.Sum(x => x.SoTienNT).ToString();
            /////// Currency to money
            //string s = SoSangChu.DoiSoSangChu(tongTienNT.Split('.')[0]);
            //string c = AmountToWords.changeCurrencyToWords(tongTienNT.ToLower());
            ////string t = String.IsNullOrEmpty(loaitien) ? "" : " Exchange rate USD/VND";
            //ViewBag.bangChu = char.ToUpper(s[0]) + s.Substring(1) + " đồng";// + " / " + char.ToUpper(c[0]) + c.Substring(1).ToLower() + "vnd";

            //// NT
            //string soTienNT_BangChu = "";
            //foreach (var item in HomeVM.InPhieuView_Groupby_TkNo_TkCos)
            //{
            //    string tien = SoSangChu.DoiSoSangChu(item.SoTienNT.ToString().Split('.')[0]);
            //    item.SoTienNT_BangChu = char.ToUpper(tien[0]) + tien.Substring(1) + " " + item.LoaiTien;
            //}

            //foreach (var item in HomeVM.InPhieuView_Groupby_TkNo_TkCos)
            //{
            //    int lastItem = HomeVM.InPhieuView_Groupby_TkNo_TkCos.ToList().IndexOf(item);
            //    if (lastItem != HomeVM.InPhieuView_Groupby_TkNo_TkCos.Count() - 1)
            //    {
            //        // this is the last item
            //        soTienNT_BangChu += item.SoTienNT_BangChu + " + ";
            //    }
            //    else
            //    {
            //        soTienNT_BangChu += item.SoTienNT_BangChu;
            //    }
            //}
            //ViewBag.SoTienNT_BangChu = soTienNT_BangChu;

            //return View(HomeVM);
        }

        public FileResult DownloadExcel() // thao
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string folderPath = webRootPath + @"\Doc\";
            string newPath = Path.Combine(webRootPath, folderPath, "ExcelAttach.xlsx");

            //return File(newPath, "application/vnd.ms-excel", "Book3.xlsx");

            string filePath = newPath;

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", "File_mau.xlsx");
        }

        [HttpPost]
        public async Task<JsonResult> UploadExcelAttach(Guid kvptcId /*[FromForm] HomeViewModel homeVM*/) // thao
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            var fileCheck = Request.Form.Files;
            if (fileCheck.Count > 0)
            {
                KVPTC kVPTC = await _kVPTCService.GetByGuidIdAsync(kvptcId);
                //// tao phieuchi
                // tao roi
                //// tao phieuchi

                #region upload excel

                // var kVPTC = await _kVPTCService.GetByGuidIdAsync(kVPTCId);
                IFormFile file = Request.Form.Files[0];
                string folderName = "excelfolder";
                string webRootPath = _webHostEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);

                string folderPath = webRootPath + @"\excelfolder\";
                FileInfo fileInfo = new FileInfo(Path.Combine(folderPath, file.FileName));

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                    string fullPath = Path.Combine(newPath, file.FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                        //var list = workSheet.Cells.ToList();
                        //var table = workSheet.Tables.ToList();
                        int totalRows = workSheet.Dimension.Rows;

                        List<KVCTPTC> kVCTPTCs = new List<KVCTPTC>();

                        for (int i = 2; i <= totalRows; i++)
                        {
                            var kVCTPTC = new KVCTPTC();

                            if (workSheet.Cells[i, 1].Value != null)
                                kVCTPTC.DienGiaiP = workSheet.Cells[i, 1].Value.ToString().Trim();

                            if (workSheet.Cells[i, 2].Value != null)
                                kVCTPTC.Sgtcode = workSheet.Cells[i, 2].Value.ToString().Trim();

                            if (workSheet.Cells[i, 3].Value != null)
                                kVCTPTC.HTTC = workSheet.Cells[i, 3].Value.ToString().Trim();

                            if (workSheet.Cells[i, 4].Value != null)
                                kVCTPTC.SoTienNT = decimal.Parse(workSheet.Cells[i, 4].Value.ToString().Trim());
                            kVCTPTC.SoTien = kVCTPTC.SoTienNT;

                            if (workSheet.Cells[i, 5].Value != null)
                                kVCTPTC.TKNo = workSheet.Cells[i, 5].Value.ToString().Trim();
                            kVCTPTC.TKCo = "1111000000";

                            if (workSheet.Cells[i, 6].Value != null)
                                kVCTPTC.MaKhNo = workSheet.Cells[i, 6].Value.ToString().Trim();

                            kVCTPTC.MaKh = kVCTPTC.MaKhNo;

                            if (workSheet.Cells[i, 7].Value != null)
                                kVCTPTC.BoPhan = workSheet.Cells[i, 7].Value.ToString().Trim();

                            if (workSheet.Cells[i, 8].Value != null)
                                kVCTPTC.LoaiHDGoc = workSheet.Cells[i, 8].Value.ToString().Trim();

                            if (workSheet.Cells[i, 9].Value != null)
                                kVCTPTC.SoCTGoc = workSheet.Cells[i, 9].Value.ToString().Trim();

                            if (workSheet.Cells[i, 10].Value != null)
                                kVCTPTC.KyHieu = workSheet.Cells[i, 10].Value.ToString().Trim();

                            if (workSheet.Cells[i, 11].Value != null)
                                kVCTPTC.MauSoHD = workSheet.Cells[i, 11].Value.ToString().Trim();

                            if (workSheet.Cells[i, 12].Value != null)
                            {
                                DateTime ngayCT;
                                try
                                {
                                    ngayCT = DateTime.Parse(workSheet.Cells[i, 12].Value.ToString().Trim());
                                    kVCTPTC.NgayCTGoc = ngayCT;
                                }
                                catch (Exception ex)
                                {
                                    kVCTPTC.NgayCTGoc = null;
                                }
                            }

                            if (workSheet.Cells[i, 13].Value != null)
                                kVCTPTC.MatHang = workSheet.Cells[i, 13].Value.ToString().Trim();

                            // phan them
                            if (workSheet.Cells[i, 14].Value != null)
                                kVCTPTC.VAT = decimal.Parse(workSheet.Cells[i, 14].Value.ToString().Trim());

                            if (workSheet.Cells[i, 15].Value != null)
                                kVCTPTC.DSKhongVAT = decimal.Parse(workSheet.Cells[i, 15].Value.ToString().Trim());

                            if (workSheet.Cells[i, 16].Value != null)
                                kVCTPTC.LinkHDDT = workSheet.Cells[i, 16].Value.ToString().Trim();

                            if (workSheet.Cells[i, 17].Value != null)
                                kVCTPTC.MaTraCuu = workSheet.Cells[i, 17].Value.ToString().Trim();

                            if (workSheet.Cells[i, 18].Value != null)
                                kVCTPTC.NoQuay = workSheet.Cells[i, 18].Value.ToString().Trim();

                            if (workSheet.Cells[i, 19].Value != null)
                                kVCTPTC.CoQuay = workSheet.Cells[i, 19].Value.ToString().Trim();

                            if (workSheet.Cells[i, 20].Value != null)
                                kVCTPTC.BoPhan = workSheet.Cells[i, 20].Value.ToString().Trim();

                            if (workSheet.Cells[i, 21].Value != null)
                                kVCTPTC.Number = workSheet.Cells[i, 21].Value.ToString().Trim();

                            if (workSheet.Cells[i, 22].Value != null)
                                kVCTPTC.SoXe = workSheet.Cells[i, 22].Value.ToString().Trim();
                            
                            if (workSheet.Cells[i, 23].Value != null)
                                kVCTPTC.DienGiai = workSheet.Cells[i, 23].Value.ToString().Trim();

                            //if (workSheet.Cells[i, 20].Value != null)
                            kVCTPTC.KVPTCId = kvptcId;
                            //kVCTPTC.DSKhongVAT = 0;
                            kVCTPTC.NgayTao = DateTime.Now;
                            kVCTPTC.NguoiTao = user.Username;
                            //kVCTPTC.KVPTCId = kvptcId;
                            kVCTPTC.SoCT = kVPTC.SoCT;
                            kVCTPTC.LoaiTien = "VND";
                            kVCTPTC.TyGia = 1;
                            kVCTPTC.MaCn = user.Macn;
                            

                            if (string.IsNullOrEmpty(kVCTPTC.DienGiaiP) && string.IsNullOrEmpty(kVCTPTC.Sgtcode) &&
                                string.IsNullOrEmpty(kVCTPTC.HTTC) && string.IsNullOrEmpty(kVCTPTC.SoTienNT.ToString()) &&
                                string.IsNullOrEmpty(kVCTPTC.TKNo) && string.IsNullOrEmpty(kVCTPTC.MaKhNo) &&
                                string.IsNullOrEmpty(kVCTPTC.BoPhan) && string.IsNullOrEmpty(kVCTPTC.LoaiHDGoc) &&
                                string.IsNullOrEmpty(kVCTPTC.SoCTGoc) && string.IsNullOrEmpty(kVCTPTC.KyHieu) &&
                                string.IsNullOrEmpty(kVCTPTC.MauSoHD) && string.IsNullOrEmpty(kVCTPTC.NgayCTGoc.ToString()) &&
                                string.IsNullOrEmpty(kVCTPTC.MatHang) && string.IsNullOrEmpty(kVCTPTC.VAT.ToString()) &&
                                (kVCTPTC.DSKhongVAT == 0) && string.IsNullOrEmpty(kVCTPTC.LinkHDDT) &&
                                string.IsNullOrEmpty(kVCTPTC.MaTraCuu) && string.IsNullOrEmpty(kVCTPTC.NoQuay) &&
                                string.IsNullOrEmpty(kVCTPTC.CoQuay) && string.IsNullOrEmpty(kVCTPTC.BoPhan) &&
                                string.IsNullOrEmpty(kVCTPTC.Number) && string.IsNullOrEmpty(kVCTPTC.SoXe))
                            {
                            }
                            else
                            {
                                
                                // thong tin khach hang
                                var khachHang = _kVCTPTCService.GetSuppliersByCode(kVCTPTC.MaKh).FirstOrDefault();
                                //var supplier = _kVCTPTCService.GetSuppliersByCodeName(kVCTPTC.MaKh, user.Macn).FirstOrDefault();

                                if (khachHang != null && !string.IsNullOrEmpty(khachHang.Code))
                                {
                                    kVCTPTC.TenKH = khachHang.TenThuongMai;
                                    kVCTPTC.DiaChi = khachHang.DiaChi;
                                    //kVCTPTC.KyHieu = khachHang.KyHieuHd;  -> lay theo file excel
                                    kVCTPTC.MauSoHD = khachHang.MauSoHd;
                                    kVCTPTC.MsThue = khachHang.MaSoThue;
                                }
                                kVCTPTCs.Add(kVCTPTC);
                            }
                        }
                        try
                        {
                            kVCTPTCs = kVCTPTCs.ToList();
                            if (kVCTPTCs.Any(x => string.IsNullOrEmpty(x.TKNo)))
                            {
                                return Json(new
                                {
                                    status = false,
                                    message = "Tk nợ không được để trống."
                                });
                            }
                            await _kVCTPTCService.CreateRange(kVCTPTCs);

                            if (System.IO.File.Exists(fileInfo.ToString()))
                                System.IO.File.Delete(fileInfo.ToString());

                            // for redirect
                            ViewBag.id = kvptcId;

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
                }
                else
                {
                    return Json(new
                    {
                        status = false,
                        message = "Vui lòng chọn file!"
                    });
                }

                #endregion upload excel
            }
            return Json(new
            {
                status = false,
                message = "Vui lòng chọn file!"
            });
        }

        public IActionResult DetailsRedirect(string strUrl/*, string tabActive*/)
        {
            //if (!string.IsNullOrEmpty(tabActive))
            //{
            //    strUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab
            //}
            return Redirect(strUrl);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string strUrl, Guid id)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (id == Guid.Empty)
                return NotFound();
            var kVPTC = await _kVPTCService.GetByGuidIdAsync(id);
            if (kVPTC == null)
                return NotFound();
            try
            {
                
                    await _kVPTCService.DeleteAsync(kVPTC);

                return Redirect("/");
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

    }
}