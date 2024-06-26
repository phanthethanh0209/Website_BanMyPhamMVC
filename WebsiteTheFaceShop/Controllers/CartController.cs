using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;
using WebsiteTheFaceShop.Models;
using WebsiteTheFaceShop.ViewModels;
using WebsiteTheFaceShop.Filters;

namespace WebsiteTheFaceShop.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        DataModelDbContext db = new DataModelDbContext();
        private const string CartSession = "CartSession";

        //public ActionResult Index()
        //{
        //    return View();
        //}

        // xử lý Đặt hàng
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<GioHang>();
            if (cart != null)
            {
                list = (List<GioHang>)cart;
            }
            return View(list);
        }

        // xử lý xóa tất cả đơn hàng
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        // xử lý xóa 1 đơn hàng
        public JsonResult Delete(int id)
        {
            var sessionCart = (List<GioHang>)Session[CartSession];
            sessionCart.RemoveAll(x => x.SanPham.MaSP == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        // xử lý cập nhật đơn hàng
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<GioHang>>(cartModel);
            var sessionCart = (List<GioHang>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.SanPham.MaSP == item.SanPham.MaSP);
                if (jsonItem != null)
                {
                    item.SoLuong = jsonItem.SoLuong;
                }
            }

            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        // xử lý thêm sản phẩm
        public ActionResult AddItem(int productId, int quantity)
        {
            var product = db.SANPHAMs.FirstOrDefault(c => c.MaSP == productId);
            var cart = Session[CartSession];
            // ktra trong giỏ hàng có sản phẩm nào chưa
            if (cart != null)
            {
                var list = (List<GioHang>)cart;
                if (list.Exists(x => x.SanPham.MaSP == product.MaSP))
                {
                    foreach (var item in list)
                    {
                        if (item.SanPham.MaSP == product.MaSP)
                        {
                            item.SoLuong += quantity;
                        }
                    }
                }
                else
                {
                    // tạo mới đối tượng cart item
                    var item = new GioHang();
                    item.SanPham = product;
                    item.SoLuong = quantity;
                    list.Add(item);
                }

                // gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new GioHang();
                item.SanPham = product;
                item.SoLuong = quantity;
                var list = new List<GioHang>();
                list.Add(item);
                // gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        // Render giỏ hàng 
        [ChildActionOnly]
        public PartialViewResult HeaderCart() // render 1 phần của view
        {
            var cart = Session[CartSession];
            var list = new List<GioHang>();
            if (cart != null)
            {
                list = (List<GioHang>)cart;
            }

            return PartialView(list);
        }

        // xử lý Thanh toán
        [KiemTrangDangNhap]
        [HttpGet]
        public ActionResult Payment()
        {

            var cart = Session[CartSession];
            var list = new List<GioHang>();
            if (cart != null)
            {
                list = (List<GioHang>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string emailaddress)
        {
            var hd = new HOADON();
            hd.NgayLap = DateTime.Now;
            hd.DiaChi = address;
            hd.Email = emailaddress;
            hd.SDT = mobile;
            hd.TenKH = shipName;

            try
            {
                // Thêm Order gồm các thông tin khách hàng
                db.HOADONs.Add(hd);
                db.SaveChanges();
                var id = hd.MaHD;
                var cart = (List<GioHang>)Session[CartSession];

                decimal total = 0;
                foreach (var item in cart)
                {
                    var cthoadon = new CTHOADON();
                    cthoadon.MaHD = id;
                    cthoadon.MaSP = item.SanPham.MaSP;
                    cthoadon.DonGia = item.SanPham.DonGiaBan;
                    cthoadon.SoLuong = item.SoLuong;
                    db.CTHOADONs.Add(cthoadon);
                    db.SaveChanges();
                    total += (item.SanPham.DonGiaBan.GetValueOrDefault(0) * item.SoLuong);
                }

                //đọc file neworder để thay thế nội dung, vd: CustomerName thay thành shipName
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", emailaddress);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendEmail(emailaddress, "Đơn hàng mới từ TheFaceShop", content); // gửi về qtv
                new MailHelper().SendEmail(toEmail, "Đơn hàng mới từ TheFaceShop", content); // gửi đến khách hàng
                // Xóa hết giỏ hàng
                Session[CartSession] = null;
            }
            catch (Exception ex)
            {
                return Redirect("Failed");
            }
            return Redirect("Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed()
        {
            return View();
        }
    }
}