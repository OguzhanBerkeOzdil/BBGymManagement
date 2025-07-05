using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BBGymManagement.Areas.Admin.MVVM;
using BBGymManagement.Models.Context;
using BBGymManagement.Models.Entities;
using BBGymManagement.Services;
using QRCoder;

namespace BBGymManagement.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class OrdersController : Controller
    {
        public OrderService _orderService = new OrderService();
        public CustomerService _customerService = new CustomerService();
        public ProductService _productService = new ProductService();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();
            foreach (var item in _orderService.GetAll())
            {
                var customer = _customerService.GetById(item.UserId);
                var product = _productService.GetById(item.ProductId);
                
                string customerName = customer != null ? customer.Name + " " + customer.Surname : "Bilinmeyen Müşteri";
                string productName = product != null ? product.Name : "Bilinmeyen Ürün";
                
                orders.Add(new OrderViewModel 
                { 
                    Id = item.Id, 
                    CustomerName = customerName, 
                    FinishTime = item.FinishTime, 
                    TotalPrice = item.TotalPrice, 
                    ProductName = productName, 
                    IsActive = item.IsActive 
                });
            }
            return View(orders);
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderService.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            OrderDetailViewModel model = new OrderDetailViewModel
            {
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                ProductId = order.ProductId,
                FinishTime = order.FinishTime,
                Id = order.Id,
                IsActive = order.IsActive
            };

            var product = _productService.GetById(order.ProductId);

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
                        model.QRImageLink = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());

                    }
                }
            }
            return View(model);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderService.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            _orderService.Update(order, order.Id);
            return RedirectToAction("Index");
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderService.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _orderService.Remove(id);
            return RedirectToAction("Index");
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.Customers = _customerService.GetAll().Select(c => new SelectListItem 
            { 
                Value = c.Id.ToString(), 
                Text = c.Name + " " + c.Surname 
            }).ToList();

            ViewBag.Products = _productService.GetAll().Select(p => new SelectListItem 
            { 
                Value = p.Id.ToString(), 
                Text = p.Name + " - " + p.Price.ToString("C", new System.Globalization.CultureInfo("tr-TR")) 
            }).ToList();

            return View();
        }

        // POST: Admin/Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Ürün bilgilerini al
                    var product = _productService.GetById(order.ProductId);
                    if (product != null)
                    {
                        order.TotalPrice = product.Price;
                        order.FinishTime = DateTime.Now.AddDays(product.Day);
                        order.IsActive = true;
                        
                        _orderService.Add(order);
                        TempData["SuccessMessage"] = "Sipariş başarıyla oluşturuldu!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Seçilen ürün bulunamadı!";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Lütfen tüm alanları doğru şekilde doldurun!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Sipariş oluşturulurken bir hata oluştu: " + ex.Message;
            }

            // Hata durumunda dropdown'ları tekrar doldur
            ViewBag.Customers = _customerService.GetAll().Select(c => new SelectListItem 
            { 
                Value = c.Id.ToString(), 
                Text = c.Name + " " + c.Surname 
            }).ToList();

            ViewBag.Products = _productService.GetAll().Select(p => new SelectListItem 
            { 
                Value = p.Id.ToString(), 
                Text = p.Name + " - " + p.Price.ToString("C", new System.Globalization.CultureInfo("tr-TR")) 
            }).ToList();

            return View(order);
        }
    }
}
