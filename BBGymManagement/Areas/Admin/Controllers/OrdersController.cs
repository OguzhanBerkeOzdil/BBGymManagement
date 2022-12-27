﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BBGymManagement.Areas.Admin.MVVM;
using BBGymManagement.Models.Context;
using BBGymManagement.Models.Entities;
using BBGymManagement.Services;

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
                orders.Add(new OrderViewModel { Id = item.Id, CustomerName = customer.Name + " " + customer.Surname, FinishTime = item.FinishTime, TotalPrice = item.TotalPrice, ProductName = product.Name });
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
            return View(order);
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
    }
}
