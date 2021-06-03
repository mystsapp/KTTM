using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Data.Utilities;
using KTTM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KTTM.Controllers
{
    public class LoginsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //var result = _unitOfWork.userRepository.login(model.Username, "015");

                // login
                var applications = _unitOfWork.applicationQLTaiKhoanRepository.GetAll();
                var applicationUsers = _unitOfWork.applicationUserQLTaiKhoanRepository.GetAll();
                var users = _unitOfWork.userQLTaiKhoanRepository.GetAll();
                
                var result = (from a in applications
                              join au in applicationUsers on a.Mact equals au.Mact
                              join u in users on au.Username equals u.Username
                              where au.Mact == "018" && u.Username.ToLower() == model.Username.ToLower()
                              select new LoginModel()
                              {
                                  Username = u.Username,
                                  Mact = a.Mact,
                                  Password = u.Password,
                                  Hoten = u.Hoten,
                                  Dienthoai = u.Dienthoai,
                                  Email = u.Email,
                                  Macn = u.Macn,
                                  RoleId = u.RoleId,
                                  Trangthai = u.Trangthai,
                                  Doimk = u.Doimk,
                                  Ngaydoimk = u.Ngaydoimk
                              }).FirstOrDefault();
                // login

                if (result == null)
                {
                    ModelState.AddModelError("", "Tài khoản này không tồn tại");
                }
                else
                {
                    if (!result.Trangthai)
                    {
                        ModelState.AddModelError("", "Tài khoản này đã bị khóa");
                        return View();
                    }
                    string modelPass = MaHoaSHA1.EncodeSHA1(model.Password);
                    if (result.Password != modelPass)
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng");

                    }
                    if (result.Password == modelPass)
                    {
                        //var user = _userRepository.GetById(model.Username);
                        var user = await _unitOfWork.userQLTaiKhoanRepository.GetByIdAsync(model.Username);
                        HttpContext.Session.Set("loginUser", user);

                        //HttpContext.Session.SetString("username", model.Username);
                        HttpContext.Session.SetString("password", model.Password);
                        //HttpContext.Session.SetString("hoten", result.Hoten);
                        //HttpContext.Session.SetString("phong", result.Maphong);
                        HttpContext.Session.SetString("chinhanh", user.Macn);
                        HttpContext.Session.SetString("userId", user.Username);
                        
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }


    }
}