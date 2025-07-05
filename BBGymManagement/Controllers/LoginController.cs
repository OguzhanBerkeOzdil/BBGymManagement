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
    /// <summary>
    /// Kullanıcı giriş işlemlerini yöneten controller - çoklu dil destekli
    /// </summary>
    public class LoginController : Controller
    {
        private CustomerService _customerService = new CustomerService();
        private RolService _rolService = new RolService();

        /// <summary>
        /// Giriş sayfasını gösterir
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.CurrentLanguage = Session["Language"] as string ?? "en";
            return View();
        }

        /// <summary>
        /// Kullanıcı giriş işlemini gerçekleştirir
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string email, string password)
        {
            var currentLang = Session["Language"] as string ?? "en";
            
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    TempData["LoginError"] = currentLang == "tr" 
                        ? "E-posta ve şifre alanları zorunludur." 
                        : "Email and password fields are required.";
                    return View();
                }

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

                // Başarılı giriş mesajı
                TempData["SuccessMessage"] = currentLang == "tr" 
                    ? "Başarıyla giriş yaptınız!" 
                    : "Successfully logged in!";

                return Redirect("~/Home/Index");
            }
            else
            {
                TempData["LoginError"] = currentLang == "tr" 
                    ? "E-posta veya şifre hatalı." 
                    : "Email or password is incorrect.";
                return View();
            }
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcı dostu mesaj göster
                TempData["LoginError"] = currentLang == "tr" 
                    ? "Giriş yapılırken bir hata oluştu. Lütfen tekrar deneyiniz." 
                    : "An error occurred during login. Please try again.";
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