using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteTheFaceShop.Models;
using WebsiteTheFaceShop.ViewModels;
using PagedList;
using PagedList.Mvc;

namespace WebsiteTheFaceShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //TÍNH NĂNG XỬ LÍ LỖI PAGE NOT FOUND
        public ActionResult error404()
        {
            return View();
        }

        public ActionResult TrangChu()
        {
            DataModelDbContext db = new DataModelDbContext();
            HomeModel homeModel = new HomeModel();

            homeModel.ListSP = db.SANPHAMs.ToList();
            
            return View(homeModel);
        }

        public ActionResult DanhMuc()
        {
            DataModelDbContext db = new DataModelDbContext();
            var listDanhMuc = db.DANHMUCs.ToList();
            return PartialView(listDanhMuc); 
        }

        public ActionResult LienHe()
        {
            return View();
        }

        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult TinTuc()
        {
            return View();
        }
    }
}