using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BBGymManagement.Models.Context;
using BBGymManagement.Models.Entities;
using BBGymManagement.Services;

namespace BBGymManagement.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class ProductsController : Controller
    {
        private ProductService _productService = new ProductService();


        #region HelperMethod

        private bool IsValidProduct(Product product)
        {
            bool validation = true;
            if (string.IsNullOrEmpty(product.Name?.Trim()))
            {
                validation = false;
            }
            if (string.IsNullOrEmpty(product.Description?.Trim()))
            {
                validation = false;
            }
            if (product.Price <= 0)
            {
                validation = false;
            }
            if (product.Day <= 0)
            {
                validation = false;
            }
            if (product.CategoryId == 0)
            {
                validation = false;
            }

            return validation;
        }
        #endregion


        public ActionResult Index()
        {
            return View(_productService.GetAll());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productService.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, Product product)
        {

            if (IsValidProduct(product) && file != null)
            {
                string picture = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine("/Content/Images/", picture);

                file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Content/Images/"), picture));

                product.ImageUrl = path;

                _productService.Add(product);

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productService.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            if (IsValidProduct(product))
            {
                if (file != null)
                {
                    string picture = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine("/Content/Images/", picture);

                    file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Content/Images/"), picture));

                    product.ImageUrl = path;
                }
                _productService.Update(product, product.Id);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productService.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _productService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
