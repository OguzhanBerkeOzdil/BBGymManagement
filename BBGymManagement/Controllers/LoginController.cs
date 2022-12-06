using BBGymManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BBGymManagement.Controllers
{
    public class LoginController : Controller
    {
        CustomerService _customerService = new CustomerService();
        RolService _rolService=new RolService();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email,string password)
        {
            if (_customerService.IsAdmin(email, password))
            {
                var user = _customerService.Get(x=>x.Email==email).FirstOrDefault();
                var role = _rolService.GetById(user.RolId).Name;
                FormsAuthentication.SetAuthCookie(user.Name,true);
                var identity = new System.Security.Principal.GenericIdentity(user.Name);
                var principal = new GenericPrincipal(identity,new string[]{role});
                HttpContext.User = principal;
                Thread.CurrentPrincipal = principal;

                return RedirectToAction("Index","Home");
            }
            else if (_customerService.IsCustomer(email, password))
            {
                var user = _customerService.Get(x => x.Email == email).FirstOrDefault();
                var role = _rolService.GetById(user.RolId).Name;
                FormsAuthentication.SetAuthCookie(user.Name, true);
                var identity = new System.Security.Principal.GenericIdentity(user.Name);
                var principal = new GenericPrincipal(identity, new string[] { role });
                HttpContext.User = principal;
                Thread.CurrentPrincipal = principal;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["LoginError"] = "Username or password is wrong";
                return View();
            }
           
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}