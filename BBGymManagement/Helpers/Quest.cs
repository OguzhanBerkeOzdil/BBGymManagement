using BBGymManagement.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BBGymManagement.Helpers
{
    public class Quest : IJob
    {
        private OrderService _orderService = new OrderService();
        public async Task Execute(IJobExecutionContext context)
        {
            var orders = _orderService.Get(x => x.IsActive == true);

            foreach (var order in orders)
            {
                if (order.FinishTime < DateTime.Now)
                {
                    order.IsActive = false;
                    _orderService.Update(order, order.Id);
                }
            }
        }
    }
}