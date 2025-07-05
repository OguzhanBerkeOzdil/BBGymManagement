using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BBGymManagement.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        // GET: Admin/Home - Dashboard sayfası
        public ActionResult Index()
        {
            // Dashboard için gerekli istatistikleri buraya ekleyebiliriz
            return View();
        }

        // Admin çıkış işlemi
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Home/Index");
        }
    }
}