using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteTheFaceShop.Models;
using WebsiteTheFaceShop.Filters;
using System.Data;
using System.IO;

namespace WebsiteTheFaceShop.Areas.Admin.Controllers
{
    //Áp dụng kiểm tra quyền cho toàn bộ controller
    [KiemTraQuyenTruyCap]
    public class QL_SanPhamController : Controller
    {
        // GET: Admin/QL_SanPham
        public ActionResult TrangChu(string search = "", string SortColumn = "MaSP", string IconClass = "fa-sort-asc", int page = 1)
        {
            DataModelDbContext db = new DataModelDbContext();
            List<SANPHAM> dsSanPham = db.SANPHAMs.Where(row => row.TenSP.Contains(search)).ToList();

            //TÍNH NĂNG TÌM KIẾM SP
            if (dsSanPham.Count == 0)
            {
                ViewBag.Search = search;
                ViewBag.Message = "Sản phẩm không tồn tại.";
            }
            else
            {
                ViewBag.Search = search;
                ViewBag.Message = "";
            }

            //TÍNH NĂNG SẮP XẾP SP
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (SortColumn == "MaSP")
            {
                if (IconClass == "fa-sort-asc")
                    dsSanPham = dsSanPham.OrderBy(row => row.MaSP).ToList();
                else
                    dsSanPham = dsSanPham.OrderByDescending(row => row.MaSP).ToList();
            }
            else if (SortColumn == "TenSP")
            {
                if (IconClass == "fa-sort-asc")
                    dsSanPham = dsSanPham.OrderBy(row => row.TenSP).ToList();
                else
                    dsSanPham = dsSanPham.OrderByDescending(row => row.TenSP).ToList();
            }
            else //sx theo loại   
            {
                if (IconClass == "fa-sort-asc")
                    dsSanPham = dsSanPham.OrderBy(row => row.TenLoai).ToList();
                else
                    dsSanPham = dsSanPham.OrderByDescending(row => row.TenLoai).ToList();
            }

            //TÍNH NĂNG PHÂN TRANG
            int NoOfRecordPerPage = 8;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dsSanPham.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            dsSanPham = dsSanPham.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(dsSanPham);
        }

        //TÍNH NĂNG THÊM MỚI SP VÀO DATABASE
        public ActionResult ThemSP()
        {
            DataModelDbContext db = new DataModelDbContext();
            ViewBag.TenLoai = db.SANPHAMs.ToList();
            ViewBag.TenThuongHieu = db.SANPHAMs.ToList();
            ViewBag.DanhMuc = db.DANHMUCs.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult ThemSP(SANPHAM sp,HttpPostedFileBase uploadHinh)
        {
            DataModelDbContext db = new DataModelDbContext();

            // Kiểm tra xem thư mục tồn tại chưa, nếu không thì tạo mới
            string uploadFolder = Server.MapPath("~/Content/images");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            if (uploadHinh == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn hình ảnh";
                return View();
            }

            if (ModelState.IsValid)
            {
                //Lưu tên file
                var fileName = Path.GetFileName(uploadHinh.FileName);
                //Lưu đường dẫn của file
                var path = Path.Combine(uploadFolder, fileName);
                //Kiểm tra hình bị trùng
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    //lưu hình ảnh vào đường dẫn
                    uploadHinh.SaveAs(path);
                }
                sp.TenHinh = fileName; //Update tên hình vào database
                db.SANPHAMs.Add(sp);
                db.SaveChanges();
            }
            return RedirectToAction("TrangChu");
        }

        //TÍNH NĂNG XEM CHI TIẾT SP
        public ActionResult XemChiTietSP(int id)
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

        //TÍNH NĂNG CHỈNH SỬA SP
        public ActionResult ChinhSuaSP(int id)
        {
            DataModelDbContext db = new DataModelDbContext();
            SANPHAM sanpham = db.SANPHAMs.Where(row => row.MaSP == id).FirstOrDefault();
            var distinctLoai = db.SANPHAMs.Select(sp => sp.TenLoai).Distinct().ToList();
            ViewBag.TenLoai = new SelectList(distinctLoai);
            var distinctThuongHieu = db.SANPHAMs.Select(sp => sp.TenThuongHieu).Distinct().ToList();
            ViewBag.TenThuongHieu = new SelectList(distinctThuongHieu);
            ViewBag.DanhMuc = db.DANHMUCs.ToList();
            return View(sanpham);
        }

        [HttpPost]
        public ActionResult ChinhSuaSP(SANPHAM sp, HttpPostedFileBase uploadHinh)
        {
            DataModelDbContext db = new DataModelDbContext();
            SANPHAM sanpham = db.SANPHAMs.Where(row => row.MaSP == sp.MaSP).FirstOrDefault();

            //Cập nhật sản phẩm
            sanpham.TenSP = sp.TenSP;
            sanpham.TenLoai = sp.TenLoai;
            sanpham.TenThuongHieu = sp.TenThuongHieu;
            sanpham.MaDanhMuc = sp.MaDanhMuc;
            sanpham.DonGiaMua = sp.DonGiaMua;
            sanpham.DonGiaBan = sp.DonGiaBan;
            sanpham.SoLuong = sp.SoLuong;
            sanpham.GiamGia = sp.GiamGia;
            sanpham.TenHinh = sp.TenHinh;
            sanpham.MoTa = sp.MoTa;

            string uploadFolder = Server.MapPath("~/Content/images");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            if (uploadHinh == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn hình ảnh";
                return View();
            }

            if (uploadHinh != null && uploadHinh.ContentLength > 0)
            {
     
                //Lưu tên file
                var fileName = Path.GetFileName(uploadHinh.FileName);
                //Lưu đường dẫn của file
                var path = Path.Combine(uploadFolder, fileName);
                //Kiểm tra hình bị trùng
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    //lưu hình ảnh vào đường dẫn
                    uploadHinh.SaveAs(path);
                }
                sanpham.TenHinh = fileName;
            }

            db.SaveChanges();

            return RedirectToAction("TrangChu");
        }

        //TÍNH NĂNG XOÁ SP
        public ActionResult XoaSP(int id)
        {
            DataModelDbContext db = new DataModelDbContext();
            SANPHAM sanpham = db.SANPHAMs.Where(row => row.MaSP == id).FirstOrDefault();
            return View(sanpham);
        }

        [HttpPost]
        public ActionResult XoaSP(int id, SANPHAM sp)
        {
            DataModelDbContext db = new DataModelDbContext();
            SANPHAM sanpham = db.SANPHAMs.Where(row => row.MaSP == id).FirstOrDefault();
            db.SANPHAMs.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("TrangChu");
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}