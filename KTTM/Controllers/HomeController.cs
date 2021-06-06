using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KTTM.Models;
using Data.Repository;
using Data.Services;
using Data.Utilities;
using Data.Models_QLTaiKhoan;
using Microsoft.AspNetCore.Http.Extensions;

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
            HomeVM.KVPCT.LapPhieu = user.Username;

            HomeVM.Ngoaites = _kVPTCService.GetAllNgoaiTe();
            HomeVM.Phongbans = _kVPTCService.GetAllPhongBan();
            
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
