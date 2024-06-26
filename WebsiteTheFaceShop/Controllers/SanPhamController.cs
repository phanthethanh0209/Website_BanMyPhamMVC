using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteTheFaceShop.Models;
using WebsiteTheFaceShop.Filters;
using PagedList;
using PagedList.Mvc;

namespace WebsiteTheFaceShop.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham 
        public ActionResult ListSanPham(int ? page)
        {
            //throw new Exception("Blalala");
            DataModelDbContext db = new DataModelDbContext();
            List<SANPHAM> listSanPham = db.SANPHAMs.ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pageListSP = listSanPham.ToPagedList(pageNumber, pageSize);
            return View(pageListSP);
        }

        public ActionResult SanPhamMoi(int ?page)
        {
            DataModelDbContext db = new DataModelDbContext();
            //Lấy sản phẩm mới nhất theo MaSP giảm dần
            List<SANPHAM> spMoi = db.SANPHAMs.OrderByDescending(sp => sp.MaSP).ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pageSPMoi = spMoi.ToPagedList(pageNumber, pageSize);
            return View(pageSPMoi);
        }

        public ActionResult SPTheoDanhMuc(int id,int ? page )
        {
            DataModelDbContext db = new DataModelDbContext();
            var listSPDanhMuc = db.SANPHAMs.Where(row => row.MaDanhMuc == id).ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pageSPDanhMuc = listSPDanhMuc.ToPagedList(pageNumber, pageSize);
            return View(pageSPDanhMuc);
        }

        public ActionResult KhuyenMai(int ? page)
        {
            DataModelDbContext db = new DataModelDbContext();

            // Lấy danh sách các sản phẩm có giảm giá hoặc giảm giá > 10,000
            List<SANPHAM> bigDealProducts = db.SANPHAMs
                .Where(sp => sp.GiamGia != null || sp.GiamGia > 10000)
                .ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            IPagedList<SANPHAM> pageSPSale = bigDealProducts.ToPagedList(pageNumber, pageSize);
            return View(pageSPSale);
        }

        public ActionResult ChiTietSP(int id)
        {
            DataModelDbContext db = new DataModelDbContext();
            SANPHAM sanpham = db.SANPHAMs.Where(row => row.MaSP == id).FirstOrDefault();
            if (sanpham != null)
            {
                DANHMUC danhMuc = db.DANHMUCs.Find(sanpham.MaDanhMuc); // Lấy thông tin danh mục
                ViewBag.TenDanhMuc = danhMuc != null ? danhMuc.TenDanhMuc : "Không có danh mục"; // Đảm bảo danh mục tồn tại
            }
            return View(sanpham);
        }

    }
}