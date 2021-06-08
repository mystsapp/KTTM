using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Data.Repository;
using Data.Services;
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

namespace KTTM.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKVPTCService _kVPTCService;

        [BindProperty]
        public HomeViewModel HomeVM { get; set; }

        public HomeController(IUnitOfWork unitOfWork, IKVPTCService kVPTCService)
        {
            _unitOfWork = unitOfWork;
            _kVPTCService = kVPTCService;

            HomeVM = new HomeViewModel()
            {
                KVPCT = new Data.Models_KTTM.KVPCT(),
            };
        }

        public IActionResult Index(string searchString, string searchFromDate, string searchToDate, int page = 1)
        {
            HomeVM.StrUrl = UriHelper.GetDisplayUrl(Request);

            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            HomeVM.KVPTCDtos = _kVPTCService.ListKVPTC(searchString, searchFromDate, searchToDate, page);
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
            HomeVM.KVPCT.NgayCT = DateTime.Now;
            HomeVM.KVPCT.Create = DateTime.Now;
            HomeVM.KVPCT.LapPhieu = user.Username;

            HomeVM.LoaiTiens = _kVPTCService.ListLoaiTien();
            HomeVM.LoaiPhieus = _kVPTCService.ListLoaiPhieu();
            HomeVM.Phongbans = _kVPTCService.GetAllPhongBan();

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
                    KVPCT = new KVPCT(),
                    LoaiTiens = _kVPTCService.ListLoaiTien(),
                    LoaiPhieus = _kVPTCService.ListLoaiPhieu(),
                    Phongbans = _kVPTCService.GetAllPhongBan(),
                    StrUrl = strUrl
                };

                return View(HomeVM);
            }

            HomeVM.KVPCT.Create = DateTime.Now;
            HomeVM.KVPCT.LapPhieu = user.Username;

            // next SoCT --> bat buoc phai co'
            switch (HomeVM.KVPCT.MFieu)
            {
                case "T": // thu
                    switch (HomeVM.KVPCT.NgoaiTe)
                    {
                        case "VN":
                            HomeVM.KVPCT.SoCT = _kVPTCService.GetSoCT("QT"); // thu VND
                            break;
                        default:
                            HomeVM.KVPCT.SoCT = _kVPTCService.GetSoCT("NT"); // thu NgoaiTe
                            break;
                    }
                    break;
                default: // chi
                    switch (HomeVM.KVPCT.NgoaiTe)
                    {
                        case "VN":
                            HomeVM.KVPCT.SoCT = _kVPTCService.GetSoCT("QC"); // chi VND
                            break;
                        default:
                            HomeVM.KVPCT.SoCT = _kVPTCService.GetSoCT("NC"); // chi NgoaiTe
                            break;
                    }
                    break;
            }            
            // next SoCT

            HomeVM.KVPCT.LapPhieu = user.Username;

            // May tinh
            var computerName = Environment.MachineName;
            var userName = Environment.UserName;
            var osVersion = Environment.OSVersion;
            var domainName = Environment.UserDomainName;

            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            // Get the IP  
            //var info = Dns.GetHostByName(hostName).AddressList;//.ToList();
            var info = Dns.GetHostEntry(hostName).AddressList;
            //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            string myIP = Dns.GetHostEntry(hostName).AddressList[3].ToString();

            HomeVM.KVPCT.MayTinh = computerName + "|" + userName + "|" + myIP + "|" + domainName;


            // ghi log
            HomeVM.KVPCT.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            try
            {
                await _kVPTCService.CreateAsync(HomeVM.KVPCT); // save

                SetAlert("Thêm mới thành công.", "success");

                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(HomeVM);
            }
        }

        public async Task<IActionResult> Edit(string soCT, string strUrl)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");
            
            HomeVM.StrUrl = strUrl;
            if (string.IsNullOrEmpty(soCT))
            {
                ViewBag.ErrorMessage = "Phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            HomeVM.KVPCT = await _kVPTCService.GetBySoCT(soCT);
            
            if (HomeVM.KVPCT == null)
            {
                ViewBag.ErrorMessage = "Phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            HomeVM.LoaiTiens = _kVPTCService.ListLoaiTien();
            HomeVM.LoaiPhieus = _kVPTCService.ListLoaiPhieu();
            HomeVM.Phongbans = _kVPTCService.GetAllPhongBan();

            return View(HomeVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string soCT, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            string temp = "", log = "";

            if (soCT != HomeVM.KVPCT.SoCT)
            {
                ViewBag.ErrorMessage = "Phiếu này không tồn tại.";
                return View("~/Views/Shared/NotFound.cshtml");
            }

            if (ModelState.IsValid)
            {
                HomeVM.KVPCT.NgaySua = DateTime.Now;
                HomeVM.KVPCT.NguoiSua = user.Username;

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _kVPTCService.GetBySoCTAsNoTracking(soCT);

                if (t.HoTen != HomeVM.KVPCT.HoTen)
                {
                    temp += String.Format("- Họ tên thay đổi: {0}->{1}", t.HoTen, HomeVM.KVPCT.HoTen);
                }

                if (t.Phong != HomeVM.KVPCT.Phong)
                {
                    temp += String.Format("- Phòng thay đổi: {0}->{1}", t.Phong, HomeVM.KVPCT.Phong);
                }
                
                if (t.DonVi != HomeVM.KVPCT.DonVi)
                {
                    temp += String.Format("- Đơn vị thay đổi: {0}->{1}", t.DonVi, HomeVM.KVPCT.DonVi);
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
                    HomeVM.KVPCT.LogFile = t.LogFile;
                }

                try
                {
                    await _kVPTCService.UpdateAsync(HomeVM.KVPCT);
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

        [HttpPost]
        public IActionResult KhongTC141()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}