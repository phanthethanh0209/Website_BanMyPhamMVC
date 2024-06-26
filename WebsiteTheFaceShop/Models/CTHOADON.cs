using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTheFaceShop.Models
{
    public class CTHOADON
    {
        [Key, Column(Order = 0)]
        public int MaHD { get; set; }
        [Key, Column(Order = 1)]
        public int MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<int> TinhTrang { get; set; }

        public virtual HOADON HOADON { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}