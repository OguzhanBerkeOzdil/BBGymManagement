using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.MVVM
{
    public class CheckoutModel
    {
        public ProductViewModel Product { get; set; }
        public int CustomerId { get; set; }
        public string CardName { get; set; }
        public ulong CardNumber { get; set; }
        public int CVV { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}