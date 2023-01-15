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
    public class RegisterController : Controller
    {
        CustomerService _customerService = new CustomerService();
        RolService _rolService = new RolService();



        public bool IsValidatiıon(Customer model)
        {
            var validation = true;

            if (string.IsNullOrEmpty(model.Name?.Trim()))
            {
                validation = false;
            }
            if (string.IsNullOrEmpty(model.Surname?.Trim()))
            {
                validation = false;
            }
            if (string.IsNullOrEmpty(model.Email?.Trim()))
            {
                validation = false;
            }
            if (string.IsNullOrEmpty(model.SecurityQuestion?.Trim()))
            {
                validation = false;
            }
            if (string.IsNullOrEmpty(model.SecurityAnswer?.Trim()))
            {
                validation = false;
            }
            if (string.IsNullOrEmpty(model.Password?.Trim()))
            {
                validation = false;
            }
            if (string.IsNullOrEmpty(model.VerPassword?.Trim()))
            {
                validation = false;
            }

            return validation;

        }
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Customer model)
        {

            if (!IsValidatiıon(model))
            {
                TempData["error"] += "Please fill the empty places. ";
                return View(model);
            }
            if (_customerService.CheckEmail(model.Email))
            {
                TempData["error"] += "This e-mail is already being used ";
                return View(model);
            }
            if (model.Password != model.VerPassword)
            {
                TempData["error"] += "Passwords do not match. ";
                return View(model);
            }

            model.RolId = _rolService.Get(x => x.Name == "Customer").First().Id;

            model.Password = MD5EncryptionCustom.MD5Encryption(model.Password);
            model.VerPassword = MD5EncryptionCustom.MD5Encryption(model.Password);
            _customerService.Add(model);

            return RedirectToAction("Index", "Login");
        }
    }
}