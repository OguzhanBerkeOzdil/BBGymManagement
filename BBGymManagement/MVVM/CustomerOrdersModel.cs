using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.MVVM
{
    public class CustomerOrdersModel
    {
        public ProductDetailModel Product { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime FinishTime { get; set; }
        public string QRImageLink { get; set; }
    }
}