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
            try
            {
                // Debug: Model validation kontrolü
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    TempData["ErrorMessage"] = "Validation errors: " + string.Join(", ", errors);
                    return View(product);
                }

                if (IsValidProduct(product))
                {
                    // Dosya yükleme işlemi
                    if (file != null && file.ContentLength > 0)
                    {
                        string picture = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine("/Content/Images/", picture);
                        string serverPath = Server.MapPath("~/Content/Images/");
                        
                        // Klasör yoksa oluştur
                        if (!System.IO.Directory.Exists(serverPath))
                        {
                            System.IO.Directory.CreateDirectory(serverPath);
                        }

                        file.SaveAs(System.IO.Path.Combine(serverPath, picture));
                        product.ImageUrl = path;
                    }
                    else
                    {
                        // Varsayılan resim
                        product.ImageUrl = "/Content/Images/default-product.jpg";
                    }

                    // Ürünü ekle
                    _productService.Add(product);
                    TempData["SuccessMessage"] = "Ürün başarıyla eklendi!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Lütfen tüm alanları doğru şekilde doldurun! Name: " + (string.IsNullOrEmpty(product.Name?.Trim()) ? "Empty" : "OK") + 
                        ", Description: " + (string.IsNullOrEmpty(product.Description?.Trim()) ? "Empty" : "OK") + 
                        ", Price: " + product.Price + ", Day: " + product.Day + ", Category: " + product.CategoryId;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ürün eklenirken bir hata oluştu: " + ex.Message + " - " + ex.StackTrace;
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
            try
            {
                if (IsValidProduct(product))
                {
                    // Mevcut ürünü al
                    var existingProduct = _productService.GetById(product.Id);
                    if (existingProduct == null)
                    {
                        TempData["ErrorMessage"] = "Ürün bulunamadı!";
                        return RedirectToAction("Index");
                    }

                    // Dosya yükleme işlemi
                    if (file != null && file.ContentLength > 0)
                    {
                        string picture = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine("/Content/Images/", picture);
                        string serverPath = Server.MapPath("~/Content/Images/");
                        
                        // Klasör yoksa oluştur
                        if (!System.IO.Directory.Exists(serverPath))
                        {
                            System.IO.Directory.CreateDirectory(serverPath);
                        }

                        file.SaveAs(System.IO.Path.Combine(serverPath, picture));
                        product.ImageUrl = path;
                    }
                    else
                    {
                        // Mevcut resmi koru
                        product.ImageUrl = existingProduct.ImageUrl;
                    }

                    _productService.Update(product, product.Id);
                    TempData["SuccessMessage"] = "Ürün başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Lütfen tüm alanları doğru şekilde doldurun!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ürün güncellenirken bir hata oluştu: " + ex.Message;
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
            try
            {
                _productService.Remove(id);
                TempData["SuccessMessage"] = "Ürün başarıyla silindi!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ürün silinirken bir hata oluştu: " + ex.Message;
            }
            
            return RedirectToAction("Index");
        }
    }
}
