﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KTTM.Models;
using Data.Repository;
using Data.Services;

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
            HomeVM.KVPTCDtos = _kVPTCService.ListKVPTC(searchString, searchFromDate, searchToDate, page);
            return View(HomeVM);
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
