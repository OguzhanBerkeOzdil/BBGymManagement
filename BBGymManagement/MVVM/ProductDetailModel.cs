using BBGymManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.MVVM
{
    public class ProductDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Day { get; set; }
        public string ImageUrl { get; set; }
        public CategoryId CategoryId { get; set; }
    }
}