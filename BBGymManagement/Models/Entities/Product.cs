using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.Models.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Day { get; set; }
        public string ImageUrl { get; set; }
        public CategoryId CategoryId { get; set; }

    }

    public enum CategoryId
    {
        GymMembership=10,
        
        PersonalTrainer=20
    }
}