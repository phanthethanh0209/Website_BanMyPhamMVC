using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTheFaceShop.Models;

namespace WebsiteTheFaceShop.ViewModels
{
    [Serializable]
    public class GioHang
    {
        public SANPHAM SanPham { get; set; }
        public int SoLuong { get; set; }
    }
}