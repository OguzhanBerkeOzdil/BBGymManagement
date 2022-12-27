using BBGymManagement.Models.Entities;
using BBGymManagement.MVVM;
using BBGymManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBGymManagement.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private ProductService _productService=new ProductService();

        public ActionResult GymMembership()
        {
            var products = _productService.Get(x => x.CategoryId == Models.Entities.CategoryId.GymMembership);
            var model = new List<ProductViewModel>();
            foreach (var item in products)
            {
                model.Add(new ProductViewModel { Id=item.Id, Name=item.Name,Price=item.Price,ImageUrl=item.ImageUrl});
            }
            return View(model);  
        }

        public ActionResult PersonalTrainer()
        {
            var products = _productService.Get(x => x.CategoryId == Models.Entities.CategoryId.PersonalTrainer);
            var model = new List<ProductViewModel>();
            foreach (var item in products)
            {
                model.Add(new ProductViewModel {Id=item.Id, Name = item.Name, Price = item.Price, ImageUrl = item.ImageUrl });
            }
            return View(model);
        }
    }
}