using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteTheFaceShop.ViewModels
{
    public class DangKy
    {
        [Required(ErrorMessage = "Username không được để trống")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password không được để trống")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*()_+=[\]{};:<>?\\]).{8,}$", ErrorMessage = "Password phải dài ít nhất 8 ký tự chứa ít nhất một chữ cái in hoa, một chữ cái in thường, một số và ít nhất một ký tự đặc biệt. Ví dụ: Aa123456@")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không giống nhau.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Định dạng email không đúng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Định dạng số điện thoại không hợp lệ.")]
        public string Mobile { get; set; }

        public DateTime? NgaySinh { get; set; }

        public string DiaChi { get; set; }

    }
}