using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.MVVM
{
    /// <summary>
    /// Ürün view modeli - frontend'de ürün bilgilerini taşır
    /// </summary>
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int Day { get; set; } // Seans gün sayısı için
        public string Duration { get; set; } // Süre bilgisi için
        public string Difficulty { get; set; } // Zorluk seviyesi için
    }
}