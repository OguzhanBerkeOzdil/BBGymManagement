using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.Models.Entities
{
    public class Order:BaseEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime FinishTime { get; set; }
    }
}