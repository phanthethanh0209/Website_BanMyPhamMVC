using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteTheFaceShop.Filters;
using WebsiteTheFaceShop.Models;
using WebsiteTheFaceShop.Identity;

namespace WebsiteTheFaceShop.Areas.Admin.Controllers
{
    //Áp dụng kiểm tra quyền cho toàn bộ controller
    [KiemTraQuyenTruyCap]
    public class QL_DonHangController : Controller
    {
        // GET: Admin/QL_DonHang
        public ActionResult TrangChu()
        {
            ViewBag.TongDon = TongDonHang();
            ViewBag.TongDoanhThu = TongDoanhThu();
            ViewBag.TongAccount = TongAccount();
            ViewBag.TongSanPham = TongSP();
            return View();
        }

        public ActionResult XemChiTiet(int id)
        {
            DataModelDbContext db = new DataModelDbContext();
            List<CTHOADON> cthd = db.CTHOADONs.Where(row => row.MaHD == id).ToList();
            return View(cthd);
        }

        public int TongDonHang()
        {
            DataModelDbContext db = new DataModelDbContext();
            int tongDDH = db.HOADONs.Count();
            return tongDDH;
        }

        public int TongAccount()
        {
            AppDbContext db = new AppDbContext();
            int tongUser = db.Users.Count();
            return tongUser;
        }

        public decimal TongDoanhThu()
        {
            DataModelDbContext db = new DataModelDbContext();

            decimal tongDT = 0; // Khởi tạo tổng doanh thu ban đầu là 0

            foreach (var hd in db.CTHOADONs)
            {
                if (hd.DonGia.HasValue && hd.SoLuong.HasValue)
                {
                    tongDT += hd.DonGia.Value * hd.SoLuong.Value;
                }
            }

            return tongDT;
        }

        public int TongSP()
        {
            DataModelDbContext db = new DataModelDbContext();
            int tongSP = db.SANPHAMs.Count();
            return tongSP;
        }
    }
}