using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Data.Utilities;
using KTTM.Models;
using KTTM.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTTM.Controllers
{
    public class TonQuiesController : BaseController
    {
        private readonly ITonQuyService _tonQuyService;
        private readonly IKVPTCService _kVPTCService;
        private readonly IKVCTPTCService _kVCTPTCService;

        [BindProperty]
        public TonQuyViewModel TonQuyVM { get; set; }

        public TonQuiesController(ITonQuyService tonQuyService, IKVPTCService kVPTCService,
            IKVCTPTCService kVCTPTCService)
        {
            _tonQuyService = tonQuyService;
            _kVPTCService = kVPTCService;
            _kVCTPTCService = kVCTPTCService;
            TonQuyVM = new TonQuyViewModel()
            {
                TonQuy = new Data.Models_KTTM.TonQuy()
            };
        }

        //// TinhTonQuy
        //[HttpPost]
        //public async Task<IActionResult> TinhTonQuy(string searchFromDate, string searchToDate)
        //{
        //    ViewBag.searchFromDate = searchFromDate;
        //    ViewBag.searchToDate = searchToDate;
        //    if (string.IsNullOrEmpty(searchFromDate) || string.IsNullOrEmpty(searchToDate)) //
        //    {
        //        return Json(new
        //        {
        //            status = "nullDate",
        //            message = "Ngày tháng không được để trống"
        //        });
        //    }
        //    else
        //    {
        //        // dao ngay thang
        //        DateTime fromDate = DateTime.Parse(searchFromDate); // NgayCT
        //        DateTime toDate = DateTime.Parse(searchToDate); // NgayCT
        //        if (fromDate < DateTime.Parse("01/06/2021"))
        //        {
        //            return Json(new
        //            {
        //                status = "nullDate",
        //                message = "Không đồng ý tồn quỹ trước 01/06/2021"
        //            });
        //        }

        //        if (fromDate > toDate) // dao nguoc lai
        //        {
        //            string tmp = searchFromDate;
        //            searchFromDate = searchToDate;
        //            searchToDate = tmp;
        //            ViewBag.searchFromDate = searchFromDate;
        //            ViewBag.searchToDate = searchToDate;
        //        }
        //        var tonQuies = _tonQuyService.FindTonQuy_By_Date("01/06/2021", searchFromDate);
        //        var tonQuy = tonQuies.OrderByDescending(x => x.NgayCT).FirstOrDefault();
        //        IEnumerable<KVCTPTC> kVCTPTCs = await _kVCTPTCService.FinByDate(searchFromDate, searchToDate);

        //        BaoCaosController baoCaosController = new BaoCaosController();
        //        ExcelPackage ExcelApp = await baoCaosController.BaoCaoQuyTienVND(searchFromDate, searchToDate, tonQuy, kVCTPTCs);

        //        byte[] fileContents;
        //        try
        //        {
        //            fileContents = ExcelApp.GetAsByteArray();
        //            return File(
        //            fileContents: fileContents,
        //            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //            fileDownloadName: "QuyTienVND_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    return View();
        //}

        // GetTonQuies_Partial
        public IActionResult GetTonQuiesNT_Partial(string searchFromDate, string searchToDate)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            if (string.IsNullOrEmpty(searchFromDate) || string.IsNullOrEmpty(searchToDate)) //
            {
                return Json(new
                {
                    status = "nullDate",
                    message = "Ngày tháng không được để trống"
                });
            }

            // dao ngay thang
            DateTime fromDate = DateTime.Parse(searchFromDate); // NgayCT
            DateTime toDate = DateTime.Parse(searchToDate); // NgayCT

            if (fromDate > toDate) // dao nguoc lai
            {
                string tmp = searchFromDate;
                searchFromDate = searchToDate;
                searchToDate = tmp;
                ViewBag.searchFromDate = searchFromDate;
                ViewBag.searchToDate = searchToDate;
            }

            List<TonQuy> tonQuies = new List<TonQuy>();
            foreach (var item in _tonQuyService.GetAllNgoaiTe())
            {
                if (item.MaNt != "VND")
                    tonQuies.AddRange(_tonQuyService.FindTonQuy_By_Date(searchFromDate, searchToDate, user.Macn, item.MaNt));
            }
            TonQuyVM.TonQuies = tonQuies;

            if (TonQuyVM.TonQuies == null)
            {
                return Json(new
                {
                    status = "null"
                });
            }

            return PartialView(TonQuyVM);
        }

        public IActionResult GetTonQuies_Partial(string searchFromDate, string searchToDate)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            if (string.IsNullOrEmpty(searchFromDate) || string.IsNullOrEmpty(searchToDate)) //
            {
                return Json(new
                {
                    status = "nullDate",
                    message = "Ngày tháng không được để trống"
                });
            }

            // dao ngay thang
            DateTime fromDate = DateTime.Parse(searchFromDate); // NgayCT
            DateTime toDate = DateTime.Parse(searchToDate); // NgayCT

            if (fromDate > toDate) // dao nguoc lai
            {
                string tmp = searchFromDate;
                searchFromDate = searchToDate;
                searchToDate = tmp;
                ViewBag.searchFromDate = searchFromDate;
                ViewBag.searchToDate = searchToDate;
            }

            TonQuyVM.TonQuies = _tonQuyService.FindTonQuy_By_Date(searchFromDate, searchToDate, user.Macn);

            if (TonQuyVM.TonQuies == null)
            {
                return Json(new
                {
                    status = "null"
                });
            }

            return PartialView(TonQuyVM);
        }
    }
}