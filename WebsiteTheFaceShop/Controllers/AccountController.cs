using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteTheFaceShop.Models;
using WebsiteTheFaceShop.ViewModels;
using WebsiteTheFaceShop.Identity;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace WebsiteTheFaceShop.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(DangKy dky)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var passwdHash = Crypto.HashPassword(dky.Password);

                var user = new AppUser()
                {
                    Email = dky.Email,
                    UserName = dky.Username,
                    PasswordHash = passwdHash,
                    DiaChi = dky.DiaChi,
                    NgaySinh = dky.NgaySinh,
                    PhoneNumber = dky.Mobile
                };

                //tạo user mới
                IdentityResult identityResult = userManager.Create(user);
                if (identityResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                }
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                ModelState.AddModelError("New Errorr", "Lỗi không load được data");
            }
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(DangNhap dnhap)
        {
            var appDbContext = new AppDbContext();
            var userStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(userStore);
            var user = userManager.Find(dnhap.Username, dnhap.Password);

            if (user != null)
            {
                var authenManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenManager.SignIn(new AuthenticationProperties(), userIdentity);


                //return RedirectToAction("TrangChu", "Home");
                //Check role nếu là admin thì sẽ chuyển đến trang admin

                if (userManager.IsInRole(user.Id, "Admin") || userManager.IsInRole(user.Id, "Manager"))
                {
                    return RedirectToAction("TrangChu", "QL_SanPham", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("TrangChu", "Home");
                }

            }
            else
            {
                ModelState.AddModelError("Errorr", "Username và passoword không đúng!");
                return View();
            }
        }

        public ActionResult DangXuat()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("TrangChu", "Home");
        }
    }
}
