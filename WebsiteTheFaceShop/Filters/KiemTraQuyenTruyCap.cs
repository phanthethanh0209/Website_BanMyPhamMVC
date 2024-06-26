using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
namespace WebsiteTheFaceShop.Filters
{
    public class KiemTraQuyenTruyCap : FilterAttribute, IAuthorizationFilter
    {
        //Kiểm tra role
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //Nếu không phải là admin và manager thì không được phép truy cập vào trang admin
            if (filterContext.HttpContext.User.IsInRole("Admin") == false && filterContext.HttpContext.User.IsInRole("Manager") == false)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }    
        }
    }
}