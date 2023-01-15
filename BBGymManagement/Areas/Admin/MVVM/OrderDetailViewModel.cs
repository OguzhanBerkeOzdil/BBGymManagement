using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.Areas.Admin.MVVM
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime FinishTime { get; set; }
        public string QRImageLink { get; set; }
        public bool IsActive { get; set; }
    }
}