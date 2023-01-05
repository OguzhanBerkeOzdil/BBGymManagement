using BBGymManagement.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BBGymManagement.Models.Entities;
using BBGymManagement.Services;
using BBGymManagement.MVVM;
using System.Collections;
using System.Web.Security;

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

            var model = new CheckoutModel();
            var product = _productService.GetById(id);
            model.Product = new ProductViewModel { Id = product.Id, Name = product.Name, Price = product.Price, ImageUrl = product.ImageUrl };
            var formsIdentity = (FormsIdentity)HttpContext.User.Identity;
            var ticket = FormsAuthentication.Decrypt(formsIdentity.Ticket.Name);
            var customerId = _customerService.Get(x => x.Email == ticket.Name).FirstOrDefault().Id;
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
                    }
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

    }
}