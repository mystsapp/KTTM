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
using Data.Models_KTTM;

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

        [HttpPost]
        public IActionResult Create(string strUrl)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            if (!ModelState.IsValid)
            {
                HomeVM = new HomeViewModel()
                {
                    KVPCT = new KVPCT(),                    
                    Ngoaites = _kVPTCService.GetAllNgoaiTe(),
                    Phongbans = _kVPTCService.GetAllPhongBan(),
                    StrUrl = strUrl
                };

                return View(HomeVM);
            }

            HomeVM.KVPCT.Create = DateTime.Now;
            HomeVM.KVPCT.LapPhieu = user.Username;

            // next SoCT --> bat buoc phai co'
            if (TourVM.Tour.NgayKyHopDong != null)
            {
                DateTime dateTime;
                //dateTime = DateTime.Now;
                dateTime = TourVM.Tour.NgayKyHopDong.Value;

                var currentYear = dateTime.Year;
                var subfix = "/IB/" + currentYear.ToString();
                var tour = _unitOfWork.tourRepository.GetAllAsNoTracking().OrderByDescending(x => x.SoHopDong).ToList().FirstOrDefault();
                if (string.IsNullOrEmpty(tour.SoHopDong))
                {
                    TourVM.Tour.SoHopDong = GetNextId.NextID("", "") + subfix;
                }
                else
                {
                    var oldYear = tour.SoHopDong.Substring(10, 4);
                    // cung nam
                    if (oldYear == currentYear.ToString())
                    {
                        var oldSoHopdong = tour.SoHopDong.Substring(0, 6);
                        TourVM.Tour.SoHopDong = GetNextId.NextID(oldSoHopdong, "") + subfix;
                    }
                    else
                    {
                        // sang nam khac' chay lai tu dau
                        TourVM.Tour.SoHopDong = GetNextId.NextID("", "") + subfix;
                    }

                }
            }
            else
            {
                TourVM.Tour.SoHopDong = "";
            }

            // next SoHopdong

            //if (string.IsNullOrEmpty(TourVM.Tour.SoHopDong))
            //{
            //    TourVM.Tour.SoHopDong = "";
            //}
            TourVM.Tour.NguoiTao = user.Username;

            // create sgtcode
            var companies = await _unitOfWork.khachHangRepository.FindAsync(x => x.CompanyId == TourVM.Tour.MaKH); // find company by MaKH(companyId)
            var quocgias = await _unitOfWork.quocGiaRepository.FindAsync(x => x.Nation == companies.FirstOrDefault().Nation); // find by nation(vn)
            string sgtCode = "";
            //if (user.PhongBanId == "TF") // FRONT DESK
            if (quocgias.FirstOrDefault().Telcode == "000") // FRONT DESK
            {
                sgtCode = _tourService.newSgtcode(Convert.ToDateTime(TourVM.Tour.NgayDen), user.MaCN, "000"); // 000 --> macode cua front desk
            }

            // KDOB
            if (user.PhongBanId == "KDOB")
            {
                sgtCode = _tourService.newSgtcodeKDOB(Convert.ToDateTime(TourVM.Tour.NgayDen), user.MaCN, quocgias.FirstOrDefault().Telcode);
            }
            else // nhung thi truong khac' lay theo telcode cua quocgia
            {
                sgtCode = _tourService.newSgtcode(Convert.ToDateTime(TourVM.Tour.NgayDen), user.MaCN, quocgias.FirstOrDefault().Telcode);
            }

            TourVM.Tour.Sgtcode = sgtCode;
            // create sgtcode

            // ghi log
            TourVM.Tour.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username

            // insert tourinf
            Tourinf tourinf = new Tourinf();
            tourinf.Sgtcode = TourVM.Tour.Sgtcode;
            tourinf.Khachle = false;
            tourinf.CompanyId = TourVM.Tour.MaKH;
            tourinf.TourkindId = TourVM.Tour.LoaiTourId;
            tourinf.Arr = TourVM.Tour.NgayDen;
            tourinf.Dep = TourVM.Tour.NgayDi;
            tourinf.Pax = TourVM.Tour.SoKhachTT;
            tourinf.Childern = TourVM.Tour.SKTreEm;
            tourinf.Reference = TourVM.Tour.ChuDeTour;
            tourinf.Concernto = user.Username; // nguoi tao tour
            tourinf.Operators = ""; // nguoi dieu hanh
            tourinf.Departoperator = TourVM.Tour.PhongDH; //departoperator : qltour / phong dh
            tourinf.Departcreate = user.PhongBanId; // phong ban tao
            tourinf.Routing = "";
            //tourinf.Rate = TourVM.Tour.TyGia;
            tourinf.Rate = TourVM.Tour.TyGia;
            tourinf.Revenue = (TourVM.Tour.DoanhThuTT > 0) ? TourVM.Tour.DoanhThuTT : TourVM.Tour.DoanhThuDK;
            tourinf.PasstypeId = TourVM.Tour.LoaiKhach; // Inbound or tau bien
            //tourinf.Currency = TourVM.Tour.LoaiTien;
            tourinf.Currency = TourVM.Tour.LoaiTien;
            tourinf.Chinhanh = _unitOfWork.dmChiNhanhRepository.GetById(TourVM.Tour.ChiNhanhDHId).Macn; // chinhanh dieu hanh
            tourinf.Chinhanhtao = user.MaCN; // user login
            tourinf.Createtour = TourVM.Tour.NgayTao;
            tourinf.Logfile = TourVM.Tour.LogFile;
            // insert tourinf

            try
            {

                _unitOfWork.tourRepository.Create(TourVM.Tour);
                // insert tourinf
                _unitOfWork.tourInfRepository.Create(tourinf);

                // insert tourlewi
                if (quocgias.FirstOrDefault().Telcode == "000") // FRONT DESK
                {
                    // insert 
                    Data.Models_Tourlewi.Tour tourlewi = new Data.Models_Tourlewi.Tour();
                    tourlewi.Sgtcode = TourVM.Tour.Sgtcode;
                    tourlewi.Khachle = false;
                    tourlewi.Makh = TourVM.Tour.MaKH;
                    tourlewi.Loaitour = TourVM.Tour.LoaiTourId.ToString();
                    tourlewi.Batdau = TourVM.Tour.NgayDen;
                    tourlewi.Ketthuc = TourVM.Tour.NgayDi;
                    tourlewi.Socho = TourVM.Tour.SoKhachTT;
                    tourlewi.Chudetour = TourVM.Tour.ChuDeTour;
                    tourlewi.Tuyentq = TourVM.Tour.TuyenTQ;
                    tourlewi.Nguoitaotour = user.Username; // nguoi tao tour
                                                           //tourlewi.Operators = ""; // nguoi dieu hanh
                                                           //tourlewi.Departoperator = TourVM.Tour.PhongDH; //departoperator : qltour / phong dh
                                                           //tourlewi.Departcreate = user.PhongBanId; // phong ban tao
                                                           //tourlewi.Routing = "";
                                                           //tourlewi.Rate = TourVM.Tour.TyGia;
                                                           //tourlewi.Revenue = (TourVM.Tour.DoanhThuTT > 0) ? TourVM.Tour.DoanhThuTT : TourVM.Tour.DoanhThuDK;
                                                           //tourlewi.PasstypeId = TourVM.Tour.LoaiKhach; // Inbound or tau bien
                                                           //tourlewi.Currency = TourVM.Tour.LoaiTien;
                    tourlewi.Chinhanh = user.MaCN; // chinhanh tao
                                                   //tourlewi.Chinhanhtao = user.MaCN; // user login
                    tourlewi.Ngaytao = TourVM.Tour.NgayTao;
                    tourlewi.Logfile = TourVM.Tour.LogFile;
                    // insert tourlewi

                    _unitOfWork.tourWIRepository.Create(tourlewi);
                }
                //else
                //{
                //    //_unitOfWork.tourInfRepository.Create(tourinf);
                //    //// insert tourinf
                //    ////await _unitOfWork.Complete();

                //}
                // insert tourlewi
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");


                var fileCheck = Request.Form.Files;
                if (fileCheck.Count > 0)
                {

                    // upload excel
                    UploadExcelAsync(TourVM.Tour.Sgtcode);
                    // upload excel

                }

                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(TourVM);
            }

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
