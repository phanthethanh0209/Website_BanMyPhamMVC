using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteTheFaceShop.Identity;
using WebsiteTheFaceShop.Filters;

namespace WebsiteTheFaceShop.Areas.Admin.Controllers
{
    //Áp dụng kiểm tra quyền cho toàn bộ controller
    [KiemTrangDangNhap]
    [KiemTraQuyenTruyCap]
    public class QL_UserController : Controller
    {
        // GET: Admin/QL_User
        public ActionResult TrangChu()
        {
            AppDbContext db = new AppDbContext();
            List<AppUser> listUser = db.Users.ToList();
            return View(listUser);
        }
    }
}