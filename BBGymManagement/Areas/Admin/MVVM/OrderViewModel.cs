using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.Areas.Admin.MVVM
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime FinishTime { get; set; }
        public bool IsActive { get; set; }
    }
}