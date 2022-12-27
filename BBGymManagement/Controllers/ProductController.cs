using BBGymManagement.MVVM;
using BBGymManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBGymManagement.Controllers
{
    public class ProductController : Controller
    {
        ProductService _productService = new ProductService();
        // GET: Product
        public ActionResult Detail(int id)
        {
            var product = _productService.GetById(id);
            var model = new ProductDetailModel { Id=product.Id, CategoryId = product.CategoryId, Day = product.Day, Description = product.Description, ImageUrl = product.ImageUrl, Name = product.Name, Price = product.Price };
            return View(model);
        }
    }
}