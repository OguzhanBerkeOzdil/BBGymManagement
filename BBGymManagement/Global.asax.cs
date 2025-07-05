using BBGymManagement.Helpers;
using BBGymManagement.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BBGymManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                using (EFDbContext context = new EFDbContext())
                {
                    // Veritabanını başlat
                    context.Database.Initialize(true);

                    // Rol verilerini kontrol et ve ekle
                    if (!context.Rols.Any())
                    {
                        context.Rols.Add(new Models.Entities.Rol { Name = "Admin" });
                        context.Rols.Add(new Models.Entities.Rol { Name = "Customer" });
                        context.Rols.Add(new Models.Entities.Rol { Name = "PersonalTrainer" });
                        context.SaveChanges();
                    }

                    // Admin kullanıcısını kontrol et ve ekle
                    var adminRole = context.Rols.FirstOrDefault(f => f.Name == "Admin");
                    if (adminRole != null && !context.Customers.Any(x => x.RolId == adminRole.Id))
                    {
                        var md5password = MD5EncryptionCustom.MD5Encryption("123321");
                        context.Customers.Add(new Models.Entities.Customer 
                        { 
                            Name = "Administrator", 
                            Surname = "Administrator", 
                            Email = "admin@bbgym.com", 
                            Password = md5password, 
                            VerPassword = md5password, 
                            RolId = adminRole.Id, 
                            SecurityQuestion = "Kaç cm ?", 
                            SecurityAnswer = "5cm" 
                        });
                        context.SaveChanges();
                    }
                }

                // Trigger başlat
                try
                {
                    Trigger trigger = new Trigger();
                    trigger.QuestTrigger();
                }
                catch (Exception triggerEx)
                {
                    // Trigger hatası durumunda log tut
                    System.Diagnostics.Debug.WriteLine("Trigger Error: " + triggerEx.Message);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda log tut (geliştirme aşamasında konsola yaz)
                System.Diagnostics.Debug.WriteLine("Application_Start Error: " + ex.Message);
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.HeaderEncoding = System.Text.Encoding.UTF8;
        }
    }
}
