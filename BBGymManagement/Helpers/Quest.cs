using BBGymManagement.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BBGymManagement.Helpers
{
    /// <summary>
    /// Quartz Job olarak çalışan spor salonu abonelik kontrolü sınıfı
    /// </summary>
    public class Quest : IJob
    {
        private OrderService _orderService = new OrderService();
        
        /// <summary>
        /// Aktif abonelikleri kontrol eder ve süreleri dolanları deaktif eder
        /// </summary>
        /// <param name="context">Job execution context</param>
        /// <returns>Task</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                // Aktif abonelikleri getir
                var orders = _orderService.Get(x => x.IsActive == true);

                // Her abonelik için kontrol yap
                foreach (var order in orders)
                {
                    // Abonelik süresi dolmuş mu kontrol et
                    if (order.FinishTime < DateTime.Now)
                    {
                        order.IsActive = false;
                        _orderService.Update(order, order.Id);
                    }
                }
                
                // Async işlem tamamlandı - await uyarısını gidermek için Task.CompletedTask döndür
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama yapılabilir
                System.Diagnostics.Debug.WriteLine($"Quest job hatası: {ex.Message}");
                await Task.CompletedTask;
            }
        }
    }
}