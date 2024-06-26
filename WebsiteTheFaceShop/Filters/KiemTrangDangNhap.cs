using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace WebsiteTheFaceShop.Filters
{
    public class KiemTrangDangNhap : FilterAttribute, IAuthenticationFilter
    {
        //thực thi kiểm tra có là user hay không
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //nếu không đăng nhập thành công
            if (filterContext.HttpContext.User.Identity.IsAuthenticated == false)
            {
                //Sẽ redirect đến trang DangNhap được quy định ở Startup class
                filterContext.Result = new HttpUnauthorizedResult();
            }    
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}