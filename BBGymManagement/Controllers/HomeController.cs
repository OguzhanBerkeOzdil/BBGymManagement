using BBGymManagement.Models.Entities;
using BBGymManagement.MVVM;
using BBGymManagement.Services;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Windows.Documents;
using WebGrease.Extensions;
using Microsoft.Ajax.Utilities;
using BBGymManagement.Helpers;

namespace BBGymManagement.Controllers
{
    public class HomeController : Controller
    {
        public ProductService _productService = new ProductService();
        public CustomerService _customerService = new CustomerService();
        public OrderService _orderService = new OrderService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult BodyFatCalculator()
        {
            ViewBag.BodyFat = 9999;
            return View();
        }



        [HttpPost]
        public ActionResult BodyFatCalculator(BodyFatCalculatorModel model)
        {
            if (model.Sex.ToString() == "Male")
            {
                var result = 495 / (1.0324 - 0.19077 * Math.Log10(model.WaistCircumference - model.NeckCircumference) + 0.15456 * Math.Log10(model.Height)) - 450;
                ViewBag.BodyFat = Math.Round(result, 1);
            }
            else if (model.Sex.ToString() == "Female")
            {
                var result = 495 / (1.29579 - 0.35004 * Math.Log10(model.WaistCircumference + model.HipCircumference - model.NeckCircumference) + 0.22100 * Math.Log10(model.Height)) - 450;
                ViewBag.BodyFat = Math.Round(result, 1);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Checkout(int id)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Login");

            var formsIdentity = (FormsIdentity)HttpContext.User.Identity;
            var ticket = FormsAuthentication.Decrypt(formsIdentity.Ticket.Name);
            var customerId = _customerService.Get(x => x.Email == ticket.Name).FirstOrDefault().Id;
            var order = _orderService.Get(x => x.UserId == customerId && x.IsActive == true && x.ProductId == id);
            if (order.Count > 0)
            {
                TempData["Product"] = "The product that you are trying to buy is already active. Check your \"My Orders\" section.";
                return RedirectToAction("Index", "Home");
            }

            var model = new CheckoutModel();
            var product = _productService.GetById(id);
            model.Product = new ProductViewModel { Id = product.Id, Name = product.Name, Price = product.Price, ImageUrl = product.ImageUrl };

            model.CustomerId = customerId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutModel model)
        {
            var order = new Order();
            order.ProductId = model.Product.Id;
            order.UserId = model.CustomerId;
            var product = _productService.GetById(model.Product.Id);
            order.TotalPrice = product.Price;
            order.FinishTime = DateTime.Now.AddDays(product.Day);
            order.IsActive = true;
            if (Enum.GetName(typeof(CategoryId), product.CategoryId) == "GymMembership")
            {
                Random r = new Random();
                int number = r.Next(1000, 10000);
                order.QR = number;
                TempData["CheckoutCompleted"] = "You can see the your QR code in the my orders section of my account page";
            }

            _orderService.Add(order);

            return RedirectToAction("CheckoutCompleted", "Home");
        }

        public ActionResult CheckoutCompleted()
        {
            return View();
        }

        public ActionResult MyOrders()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Login");

            var formsIdentity = (FormsIdentity)HttpContext.User.Identity;
            var ticket = FormsAuthentication.Decrypt(formsIdentity.Ticket.Name);
            var customerId = _customerService.Get(x => x.Email == ticket.Name).FirstOrDefault().Id;

            var orders = _orderService.Get(x => x.UserId == customerId);
            List<CustomerOrdersModel> model = new List<CustomerOrdersModel>();

            foreach (var order in orders)
            {
                var product = _productService.GetById(order.ProductId);
                var qrImageLink = "";
                if (Enum.GetName(typeof(CategoryId), product.CategoryId) == "GymMembership")
                {
                    QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                    QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(order.QR.ToString(), QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qRCodeData);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (Bitmap bitmap = qrCode.GetGraphic(20))
                        {
                            bitmap.Save(ms, ImageFormat.Png);
                            qrImageLink = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());

                        }
                    }
                }
                model.Add(new CustomerOrdersModel
                {
                    FinishTime = order.FinishTime,
                    TotalPrice = order.TotalPrice,
                    Product = new ProductDetailModel
                    {
                        Name = product.Name,
                        Description = product.Description,
                        CategoryId = product.CategoryId,
                        Day = product.Day,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        Id = product.Id
                    },
                    QRImageLink = qrImageLink
                });
            }
            return View(model);
        }


        public ActionResult MyAccount()
        {
            var formsIdentity = (FormsIdentity)HttpContext.User.Identity;
            var ticket = FormsAuthentication.Decrypt(formsIdentity.Ticket.Name);
            var customer = _customerService.Get(x => x.Email == ticket.Name).FirstOrDefault();
            var model = new CustomerDetailModel { Name = customer.Name, Surname = customer.Surname, Email = customer.Email };
            return View(model);
        }



        public ActionResult ChangePassword()
        {
            var formsIdentity = (FormsIdentity)HttpContext.User.Identity;
            var ticket = FormsAuthentication.Decrypt(formsIdentity.Ticket.Name);
            Customer customer = _customerService.Get(x => x.Email == ticket.Name).FirstOrDefault();

            PasswordViewModel model = new PasswordViewModel { SecurityQuestion = customer.SecurityQuestion };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(PasswordViewModel model)
        {
            var formsIdentity = (FormsIdentity)HttpContext.User.Identity;
            var ticket = FormsAuthentication.Decrypt(formsIdentity.Ticket.Name);
            Customer customer = _customerService.Get(x => x.Email == ticket.Name).FirstOrDefault();

            if (model.SecurityAnswer == customer.SecurityAnswer)
            {
                if (MD5EncryptionCustom.MD5Encryption(model.OldPassword) == customer.Password)
                {
                    if (model.NewPassword == model.NewPasswordConfirmation)
                    {
                        customer.Password = MD5EncryptionCustom.MD5Encryption(model.NewPassword);
                        customer.VerPassword = MD5EncryptionCustom.MD5Encryption(model.NewPassword);
                        _customerService.Update(customer, customer.Id);
                        FormsAuthentication.SignOut();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Error"] = "Your new password and password confirmation does not match";
                        return View(model);
                    }
                }
                else
                {
                    TempData["Error"] = "Your old password is not correct";
                    return View(model);
                }
            }
            else
            {
                TempData["Error"] = "Your security answer is wrong";

                return View(model);
            }
        }
    }
}