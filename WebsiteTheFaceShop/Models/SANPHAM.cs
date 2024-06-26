using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteTheFaceShop.Models
{
    public class SANPHAM
    {
        //[MinLength(2, ErrorMessage="Mã sản phẩm phải chứa ít nhất 2 kí tự")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaSP { get; set; }

        public int MaDanhMuc { get; set; }

        //[Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string TenSP { get; set; }

        public string TenLoai { get; set; }

        public string TenThuongHieu { get; set; }

        //[Required(ErrorMessage = "Đơn giá mua không được để trống")]
        //[RegularExpression(@"^(0-9)*$", ErrorMessage = "Kí tự đã nhập không phải là số!")]
        public Nullable<decimal> DonGiaMua { get; set; }

        //[Required(ErrorMessage = "Đơn giá bán không được để trống")]
        //[RegularExpression(@"^(0-9)*$", ErrorMessage = "Kí tự đã nhập không phải là số!")]
        public Nullable<decimal> DonGiaBan { get; set; }

        //[Required(ErrorMessage = "Số lượng không được để trống")]
        //[RegularExpression(@"^(0-9)*$", ErrorMessage = "Kí tự đã nhập không phải là số!")]
        public Nullable<int> SoLuong { get; set; }

        public string MoTa { get; set; }
        public Nullable<decimal> GiamGia { get; set; }
        public string TenHinh { get; set; }

        public virtual DANHMUC DANHMUC { get; set; }



        
    }
}