using BBGymManagement.Models.Entities;
using BBGymManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using BBGymManagement.Helpers;
namespace BBGymManagement.Controllers
{
    /// <summary>
    /// Üye kayıt işlemlerini yöneten controller - çoklu dil destekli
    /// </summary>
    public class RegisterController : Controller
    {
        CustomerService _customerService = new CustomerService();
        RolService _rolService = new RolService();



        /// <summary>
        /// Model validasyonu kontrol eder - debugging için log eklenmiş
        /// </summary>
        public bool IsValidation(Customer model)
        {
            var validation = true;
            var errors = new List<string>();

            if (string.IsNullOrEmpty(model.Name?.Trim()))
            {
                validation = false;
                errors.Add("Name is empty");
            }
            if (string.IsNullOrEmpty(model.Surname?.Trim()))
            {
                validation = false;
                errors.Add("Surname is empty");
            }
            if (string.IsNullOrEmpty(model.Email?.Trim()))
            {
                validation = false;
                errors.Add("Email is empty");
            }
            if (string.IsNullOrEmpty(model.Password?.Trim()))
            {
                validation = false;
                errors.Add("Password is empty");
            }

            // Debug için hata listesini ViewBag'e ekle
            ViewBag.ValidationErrors = errors;

            return validation;
        }
        /// <summary>
        /// Kayıt sayfasını gösterir - çoklu dil destekli
        /// </summary>
        // GET: Register
        public ActionResult Index()
        {
            ViewBag.CurrentLanguage = Session["Language"] as string ?? "en";
            return View();
        }

        /// <summary>
        /// Kayıt işlemini gerçekleştirir - çoklu dil destekli
        /// </summary>
        /// <summary>
        /// Kayıt işlemini gerçekleştirir - çoklu dil destekli ve debugging eklenmiş
        /// </summary>
        [HttpPost]
        public ActionResult Index(Customer model)
        {
            ViewBag.CurrentLanguage = Session["Language"] as string ?? "en";
            var currentLang = ViewBag.CurrentLanguage;

            // Debug için gelen modeli kontrol et
            ViewBag.DebugInfo = $"Name: '{model?.Name}', Surname: '{model?.Surname}', Email: '{model?.Email}', Password: '{model?.Password}'";

            if (!IsValidation(model))
            {
                TempData["error"] = currentLang == "tr" ? "Lütfen boş alanları doldurun." : "Please fill the empty places.";
                return View(model);
            }
            if (_customerService.CheckEmail(model.Email))
            {
                TempData["error"] = currentLang == "tr" ? "Bu e-posta adresi zaten kullanılıyor." : "This e-mail is already being used.";
                return View(model);
            }

            try
            {
                model.RolId = _rolService.Get(x => x.Name == "Customer").First().Id;

                // Şifreyi şifrele
                model.Password = MD5EncryptionCustom.MD5Encryption(model.Password);
                
                // Varsayılan değerler
                model.IsActive = true;
                
                _customerService.Add(model);

                TempData["success"] = currentLang == "tr" ? "Kayıt başarılı! Giriş yapabilirsiniz." : "Registration successful! You can login now.";
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                TempData["error"] = currentLang == "tr" ? $"Kayıt sırasında hata oluştu: {ex.Message}" : $"Error during registration: {ex.Message}";
                return View(model);
            }
        }
    }
}