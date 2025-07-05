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
    /// <summary>
    /// Kategori işlemlerini yöneten controller - çoklu dil destekli
    /// </summary>
    public class CategoryController : Controller
    {
        private ProductService _productService = new ProductService();

        /// <summary>
        /// Spor salonu üyelik paketlerini listeler - çoklu dil destekli
        /// </summary>
        public ActionResult GymMembership()
        {
            ViewBag.CurrentLanguage = Session["Language"] as string ?? "en";
            var products = _productService.Get(x => x.CategoryId == Models.Entities.CategoryId.GymMembership);
            var model = new List<ProductViewModel>();
            
            foreach (var item in products)
            {
                model.Add(new ProductViewModel 
                { 
                    Id = item.Id, 
                    Name = item.Name,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl,
                    Description = item.Description,
                    Day = 30, // Varsayılan 30 gün
                    Duration = "Aylık", // Varsayılan süre
                    Difficulty = "Herkes", // Varsayılan zorluk
                    Category = "Gym Membership",
                    IsAvailable = true
                });
            }
            return View(model);  
        }

        /// <summary>
        /// Kişisel antrenör hizmetlerini listeler - çoklu dil destekli
        /// </summary>
        public ActionResult PersonalTrainer()
        {
            ViewBag.CurrentLanguage = Session["Language"] as string ?? "en";
            var products = _productService.Get(x => x.CategoryId == Models.Entities.CategoryId.PersonalTrainer);
            var model = new List<ProductViewModel>();
            foreach (var item in products)
            {
                model.Add(new ProductViewModel 
                { 
                    Id = item.Id, 
                    Name = item.Name, 
                    Price = item.Price, 
                    ImageUrl = item.ImageUrl,
                    Description = item.Description,
                    Day = 30, // Varsayılan 30 seans
                    Duration = "60 dakika", // Varsayılan süre
                    Difficulty = "Orta", // Varsayılan zorluk
                    Category = "Personal Trainer",
                    IsAvailable = true
                });
            }
            return View(model);
        }
    }
}