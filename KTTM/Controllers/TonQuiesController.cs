﻿using KTTM.Models;
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
        private readonly IKVPCTService _kVPCTService;

        [BindProperty]
        public TonQuyViewModel TonQuyVM { get; set; }

        public TonQuiesController(ITonQuyService tonQuyService, IKVPCTService kVPCTService)
        {
            _tonQuyService = tonQuyService;
            _kVPCTService = kVPCTService;
            TonQuyVM = new TonQuyViewModel()
            {
                TonQuy = new Data.Models_KTTM.TonQuy()
            };
        }

        // TinhTonQuy
        [HttpPost]
        public async Task<IActionResult> TinhTonQuy(string searchFromDate, string searchToDate)
        {
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
            else
            {
                // dao ngay thang
                DateTime fromDate = DateTime.Parse(searchFromDate); // NgayCT
                DateTime toDate = DateTime.Parse(searchToDate); // NgayCT
                if (fromDate < DateTime.Parse("01/06/2021"))
                {
                    return Json(new
                    {
                        status = "nullDate",
                        message = "Không đồng ý tồn quỹ trước 01/06/2021"
                    });
                }

                if (fromDate > toDate) // dao nguoc lai
                {
                    string tmp = searchFromDate;
                    searchFromDate = searchToDate;
                    searchToDate = tmp;
                    ViewBag.searchFromDate = searchFromDate;
                    ViewBag.searchToDate = searchToDate;
                }

                BaoCaosController baoCaosController = new BaoCaosController();
                ExcelPackage ExcelApp = await baoCaosController.BaoCaoQuyTienVND(searchFromDate, searchToDate);

                byte[] fileContents;
                try
                {
                    fileContents = ExcelApp.GetAsByteArray();
                    return File(
                    fileContents: fileContents,
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileDownloadName: "QuyTienVND_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View();
        }

        // GetTonQuies_Partial
        public IActionResult GetTonQuies_Partial(string searchFromDate, string searchToDate)
        {
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

            TonQuyVM.TonQuies = _tonQuyService.FindTonQuy_By_Date(searchFromDate, searchToDate);

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