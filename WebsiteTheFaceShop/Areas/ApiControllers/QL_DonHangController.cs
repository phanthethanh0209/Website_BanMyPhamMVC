using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebsiteTheFaceShop.Models;

namespace WebsiteTheFaceShop.Areas.ApiControllers
{
    public class QL_DonHangController : ApiController
    {
        public List<HOADON> GetHD()
        {
            DataModelDbContext db = new DataModelDbContext();
            List<HOADON> hd = db.HOADONs.ToList();
            return hd;
        }

        public List<CTHOADON> GetCTHD(int id)
        {
            DataModelDbContext db = new DataModelDbContext();
            List<CTHOADON> cthd = db.CTHOADONs.Where(row => row.MaHD == id).ToList();

            return cthd;
        }
    }
}
