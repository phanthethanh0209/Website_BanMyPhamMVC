using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebsiteTheFaceShop.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace WebsiteTheFaceShop.Models
{
    public class HOADON
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHD { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        [Required]
        public string TenKH { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Định dạng số điện thoại không hợp lệ.")]
        public string SDT { get; set; }
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Định dạng email không đúng")]
        public string Email { get; set; }

    }
}