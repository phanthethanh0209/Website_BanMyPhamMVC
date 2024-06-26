using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebsiteTheFaceShop.Models
{
    public class DataModelDbContext:DbContext
    {
        public DataModelDbContext() : base("WebsiteTheFaceShop") { }
        public DbSet<SANPHAM> SANPHAMs { get; set; }
        public DbSet<HOADON> HOADONs { get; set; }
        public DbSet<CTHOADON> CTHOADONs { get; set; }
        public DbSet<DANHMUC> DANHMUCs { get; set; }
    }
}