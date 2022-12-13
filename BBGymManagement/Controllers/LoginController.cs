using BBGymManagement.Models.Entities;
using BBGymManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        RolService _rolService = new RolService();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public ActionResult Index(string email, string password)
        {

            if (_customerService.IsAdmin(email, password))
            {
                var user = _customerService.Get(x => x.Email == email).FirstOrDefault();
                var role = _rolService.GetById(user.RolId).Name;
                var now = DateTime.UtcNow.ToLocalTime();
                string userData = email + ";" + role;
                var ticket = new FormsAuthenticationTicket(1, email, now, now.Add(FormsAuthentication.Timeout), true, userData, FormsAuthentication.FormsCookiePath);
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = true;
                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                cookie.Secure = FormsAuthentication.RequireSSL;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                if (FormsAuthentication.CookieDomain != null)
                {
                    cookie.Domain = FormsAuthentication.CookieDomain;
                }

                string encrypetedTicket = FormsAuthentication.Encrypt(ticket);
                FormsAuthentication.SetAuthCookie(encrypetedTicket, false);
                
                return Redirect("~/Admin/Home/Index");
            }
            else if (_customerService.IsCustomer(email, password))
            {
                var user = _customerService.Get(x => x.Email == email).FirstOrDefault();
                var role = _rolService.GetById(user.RolId).Name;
                string userData = email + ";" + role;
                var now = DateTime.UtcNow.ToLocalTime();
                var ticket = new FormsAuthenticationTicket(1, email, now, now.Add(FormsAuthentication.Timeout), true, userData, FormsAuthentication.FormsCookiePath);
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = true;
                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                cookie.Secure = FormsAuthentication.RequireSSL;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                if (FormsAuthentication.CookieDomain != null)
                {
                    cookie.Domain = FormsAuthentication.CookieDomain;
                }

                string encrypetedTicket = FormsAuthentication.Encrypt(ticket);
                FormsAuthentication.SetAuthCookie(encrypetedTicket, false);

                return Redirect("~/Home/Index");
            }
            else
            {
                TempData["LoginError"] = "Email or password is wrong";
                return View();
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}