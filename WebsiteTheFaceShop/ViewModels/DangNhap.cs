﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteTheFaceShop.ViewModels
{
    public class DangNhap
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}