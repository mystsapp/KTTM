using Data.Models_DanhMucKT;
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
using NumToWords;

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
        private readonly ITonQuyService _tonQuyService;

        [BindProperty]
        public HomeViewModel HomeVM { get; set; }

        public HomeController(IKVPTCService kVPTCService, IKVCTPTCService kVCTPTCService,
            ITamUngService tamUngService, ITT621Service tT621Service,
            ITonQuyService tonQuyService)
        {
            //_kTTM_1Context = kTTM_1Context;
            _kVPTCService = kVPTCService;
            _kVCTPTCService = kVCTPTCService;
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
            if (id == Guid.Empty)
            {
                ViewBag.id = "";
            }

            //List<Data.Models_KTTM_1.TonQuy> tonquiesAnhSon = _kTTM_1Context.TonQuies.ToList();
            //List<Data.Models_KTTM.TonQuy> tonQuies = new List<Data.Models_KTTM.TonQuy>();
            //foreach (var item in tonquiesAnhSon)
            //{
            //    tonQuies.Add(new Data.Models_KTTM.TonQuy()
            //    {
            //        LoaiTien = item.LoaiTien.Trim(),
            //        LogFile = "==== chuyển từ data anh Sơn.",
            //        NgayCT = item.NgayCt,
            //        SoTien = item.SoTien,
            //        SoTienNT = item.SoTienNt,
            //        TyGia = item.TyGia,
            //        MaCn = "STS"
            //    });
            //}
            //var abc = "";
            //await _tonQuyService.CreateRangeAsync(tonQuies);

            //List<Data.Models_KTTM.TamUng> tamUngs = _tamUngService.GetAll().ToList();
            //List<Tt621> tt621sAnhSon = _kTTM_1Context.Tt621s.ToList();
            //List<TT621> tT621s = new List<TT621>();

            //foreach (var tamUng in tamUngs)
            //{
            //    IEnumerable<Tt621> tt621sAnhSon1 = tt621sAnhSon.Where(x => x.PhieuTu.Trim() == tamUng.SoCT.Trim());
            //    if (tt621sAnhSon1.Count() > 0)
            //    {
            //        foreach (var item in tt621sAnhSon1)
            //        {
            //            tT621s.Add(new TT621()
            //            {
            //                BoPhan = item.BoPhan.Trim(),
            //                CoQuay = item.CoQuay.Trim(),
            //                DiaChi = item.DiaChi.Trim(),
            //                DienGiai = item.DienGiai.Trim(),
            //                DienGiaiP = item.DienGiaiP.Trim(),
            //                DieuChinh = item.DieuChinh,
            //                DSKhongVAT = item.DskhongVat,
            //                GhiSo = item.GhiSo.Trim(),
            //                HoaDonDT = item.HoaDonDt.Trim(),
            //                HTTC = item.Httc.Trim(),
            //                KyHieu = item.KyHieu.Trim(),
            //                KyHieuHD = item.KyHieuHd.Trim(),
            //                LapPhieu = item.LapPhieu.Trim(),
            //                LoaiHDGoc = item.LoaiHdgoc.Trim(),
            //                LoaiTien = item.LoaiTien.Trim(),
            //                LogFile = "==== chuyển từ data anh Sơn",
            //                MaKhCo = item.MaKhCo.Trim(),
            //                MaKhNo = item.MaKhNo.Trim(),
            //                MatHang = item.MatHang.Trim(),
            //                MauSoHD = item.MauSoHd.Trim(),
            //                MsThue = item.MsThue.Trim(),
            //                NgayCT = item.NgayCt,
            //                NgayCTGoc = item.NgayCtgoc,
            //                NoQuay = item.NoQuay.Trim(),
            //                PhieuTC = item.PhieuTc.Trim(),
            //                PhieuTU = item.PhieuTu.Trim(),
            //                Sgtcode = item.Sgtcode.Trim(),
            //                SoCT = item.SoCt.Trim(),
            //                SoCTGoc = item.SoCtgoc.Trim(),
            //                SoTien = item.SoTien,
            //                SoTienNT = item.SoTienNt,
            //                SoXe = item.SoXe.Trim(),
            //                TamUngId = tamUng.Id,
            //                TenKH = item.TenKh.Trim(),
            //                TKCo = item.Tkco.Trim(),
            //                TKNo = item.Tkno.Trim(),
            //                TyGia = item.TyGia,
            //                VAT = item.Vat,
            //                MaCn = tamUng.MaCn

            //            });
            //        }
            //    }

            //}
            //var abc = "";
            //await _tT621Service.CreateRangeAsync(tT621s);

            //var list = _kTTM_1Context.TamUngs.ToList();
            //var listKVCTPTC = _kVCTPTCService.GetAll().ToList();
            //var listKVPTC = _kVPTCService.GetAll().ToList();

            //List<Data.Models_KTTM.TamUng> tamUngs = new List<Data.Models_KTTM.TamUng>();
            //foreach (var tamUng in list)
            //{
            //    var kVCTPTC = listKVCTPTC.Where(x => x.SoCT == tamUng.PhieuChi)
            //               .Where(x => x.TKNo.Trim() == "1411" || x.TKNo.Trim() == "1412").FirstOrDefault();
            //    //list = list.Where(x => x.Phieuchi == kVPTC.SoCT).ToList();
            //    //.Where(x => x.Tkno.StartsWith("141")).ToList();
            //    if (list.Count > 0)
            //    {
            //        tamUngs.Add(new Data.Models_KTTM.TamUng()
            //        {
            //            ConLai = tamUng.ConLai,
            //            ConLaiNT = tamUng.ConLaiNt,
            //            DienGiai = tamUng.DienGiai.Trim(),
            //            Id = kVCTPTC.Id,
            //            LoaiTien = tamUng.LoaiTien.Trim(),
            //            LogFile = "==== chuyển từ data anh Sơn.",
            //            MaKhNo = tamUng.MaKhNo.Trim(),
            //            NgayCT = tamUng.NgayCt,
            //            PhieuChi = tamUng.PhieuChi.Trim(),
            //            PhieuTT = tamUng.PhieuTt.Trim(),
            //            Phong = tamUng.Phong.Trim(),
            //            SoCT = tamUng.SoCt.Trim(),
            //            SoTien = tamUng.SoTien,
            //            SoTienNT = tamUng.SoTienNt,
            //            TKCo = tamUng.Tkco.Trim(),
            //            TKNo = tamUng.Tkno.Trim(),
            //            TTTP = tamUng.Tttp,
            //            TyGia = tamUng.TyGia,
            //            MaCn = kVCTPTC.MaCn

            //        });

            //    }

            //}

            //var bac = "";
            //await _tamUngService.CreateRangeAsync(tamUngs);

            //var kvptcs = _kVPTCService.GetAll().ToList();
            //var kvctptcs = _kTTM_1Context.Kvctptcs.ToList();
            //List<KVCTPTC> kVCTPTCs = new List<KVCTPTC>();

            //foreach (var kvptc in kvptcs)
            //{
            //    var kvctptcs1 = kvctptcs.Where(x => x.Kvptcid == kvptc.SoCT).ToList();
            //    if (kvctptcs1.Count > 0)
            //    {
            //        foreach (var item in kvctptcs1)
            //        {
            //            kVCTPTCs.Add(new KVCTPTC()
            //            {
            //                BoPhan = item.BoPhan.Trim(),
            //                CardNumber = item.CardNumber.Trim(),
            //                CoQuay = item.CoQuay.Trim(),
            //                DiaChi = item.DiaChi.Trim(),
            //                DienGiai = item.DienGiai.Trim(),
            //                DienGiaiP = item.DienGiaiP.Trim(),
            //                DieuChinh = item.DieuChinh.Value,
            //                DSKhongVAT = item.DskhongVat,
            //                HoaDonDT = item.HoaDonDt.Trim(),
            //                HTTC = item.Httc.Trim(),
            //                HTTT = item.Httt.Trim(),
            //                KC141 = item.Kc141,
            //                KhoangMuc = item.KhoangMuc.Trim(),
            //                KVPTCId = kvptc.Id,
            //                KyHieu = item.KyHieu.Trim(),
            //                LoaiHDGoc = item.LoaiHdgoc.Trim(),
            //                LoaiTien = item.LoaiTien.Trim(),
            //                LogFile = "==== chuyển từ data anh Sơn.",
            //                MaKh = item.MaKh.Trim(),
            //                MaKhCo = item.MaKhCo.Trim(),
            //                MaKhNo = item.MaKhNo.Trim(),
            //                MatHang = item.MatHang.Trim(),
            //                MauSoHD = item.MauSoHd.Trim(),
            //                MsThue = item.MsThue.Trim(),
            //                NgayCTGoc = item.NgayCtgoc,
            //                NoQuay = item.NoQuay.Trim(),
            //                SalesSlip = item.SalesSlip.Trim(),
            //                Sgtcode = item.Sgtcode.Trim(),
            //                SoCTGoc = item.SoCtgoc.Trim(),
            //                SoTien = item.SoTien,
            //                SoTienNT = item.SoTienNt,
            //                SoXe = item.SoXe.Trim(),
            //                SoCT = kvptc.SoCT.Trim(),
            //                STT = item.Stt.Trim(),
            //                TamUng = item.TamUng.Trim(),
            //                TenKH = item.TenKh.Trim(),
            //                TKCo = item.Tkco.Trim(),
            //                TKNo = item.Tkno.Trim(),
            //                TyGia = item.TyGia,
            //                VAT = item.Vat,
            //                MaCn = kvptc.MaCn
            //            });
            //        }

            //    }
            //}

            //string abc = "";

            //await _kVCTPTCService.CreateRange(kVCTPTCs);

            //var kvptcAnhSon = _kTTM_1Context.Kvptcs.ToList();
            //List<KVPTC> kVPTCs = new List<KVPTC>();
            //foreach (var item in kvptcAnhSon)
            //{
            //    kVPTCs.Add(new KVPTC()
            //    {
            //        Create = item.Create,
            //        DonVi = item.DonVi.Trim(),
            //        HoTen = item.HoTen.Trim(),
            //        LapPhieu = item.LapPhieu.Trim(),
            //        Lock = item.Lock,
            //        Locker = item.Locker.Trim(),
            //        LogFile = "==== chuyển từ data anh Sơn.",
            //        MayTinh = item.MayTinh.Trim(),
            //        MFieu = item.Mfieu.Trim(),
            //        NgayCT = item.NgayCt,
            //        NgoaiTe = item.NgoaiTe.Trim(),
            //        Phong = item.Phong.Trim(),
            //        SoCT = item.SoCt.Trim(),
            //        MaCn = "STS"
            //    });
            //}

            //var abc = "";
            //await _kVPTCService.CreateRangeAsync(kVPTCs);

            HomeVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            HomeVM.Page = page;

            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            ViewBag.boolSgtcode = boolSgtcode;

            if (id != Guid.Empty) // for redirect with id
            {
                HomeVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(id);
                ViewBag.id = HomeVM.KVPTC.Id;
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

                #endregion log file

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

        public async Task<IActionResult> InPhieuView(Guid id, int page)
        {
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
            ///// Currency to money
            string s = SoSangChu.DoiSoSangChu(tongTienNT.Split('.')[0]);
            string c = AmountToWords.changeCurrencyToWords(tongTienNT.ToLower());
            //string t = String.IsNullOrEmpty(loaitien) ? "" : " Exchange rate USD/VND";
            ViewBag.bangChu = char.ToUpper(s[0]) + s.Substring(1) + " đồng";// + " / " + char.ToUpper(c[0]) + c.Substring(1).ToLower() + "vnd";

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
                }
                else
                {
                    soTienNT_BangChu += item.SoTienNT_BangChu;
                }
            }
            ViewBag.SoTienNT_BangChu = soTienNT_BangChu;

            return View(HomeVM);
        }

        public async Task<IActionResult> InPhieuPrint(Guid id, int page)
        {
            HomeVM.KVPTC = await _kVPTCService.GetByGuidIdAsync(id);
            HomeVM.KVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(id);
            HomeVM.Page = page;
            HomeVM.InPhieuView_Groupby_TkNo_TkCos = _kVPTCService.InPhieuView_Groupby_TkNos(HomeVM.KVCTPTCs);
            string tongTienNT = HomeVM.InPhieuView_Groupby_TkNo_TkCos.Sum(x => x.SoTienNT).ToString();
            ///// Currency to money VNC
            string s = SoSangChu.DoiSoSangChu(tongTienNT.Split('.')[0]);
            string c = AmountToWords.changeCurrencyToWords(tongTienNT.ToLower());
            //string t = String.IsNullOrEmpty(loaitien) ? "" : " Exchange rate USD/VND";
            ViewBag.bangChu = char.ToUpper(s[0]) + s.Substring(1) + " đồng";// + " / " + char.ToUpper(c[0]) + c.Substring(1).ToLower() + "vnd";

            // NT
            string soTienNT_BangChu = "";
            foreach (var item in HomeVM.InPhieuView_Groupby_TkNo_TkCos)
            {
                string tien = SoSangChu.DoiSoSangChu(item.SoTienNT.ToString().Split('.')[0]);
                item.SoTienNT_BangChu = char.ToUpper(tien[0]) + s.Substring(1) + " " + item.LoaiTien;
            }

            foreach (var item in HomeVM.InPhieuView_Groupby_TkNo_TkCos)
            {
                int lastItem = HomeVM.InPhieuView_Groupby_TkNo_TkCos.ToList().IndexOf(item);
                if (lastItem != HomeVM.InPhieuView_Groupby_TkNo_TkCos.Count() - 1)
                {
                    // this is the last item
                    soTienNT_BangChu += @item.SoTienNT.ToString("N0") + " " + @item.LoaiTien + " + ";
                }
                else
                {
                    soTienNT_BangChu += @item.SoTienNT.ToString("N0") + " " + @item.LoaiTien;
                }
            }
            ViewBag.SoTienNT_BangChu = soTienNT_BangChu;

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