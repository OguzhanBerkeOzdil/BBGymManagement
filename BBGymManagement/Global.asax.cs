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
            EFDbContext context = new EFDbContext();
            if (!context.Rols.Any())
            {
                context.Rols.Add(new Models.Entities.Rol { Name = "Admin" });
                context.Rols.Add(new Models.Entities.Rol { Name = "Customer" });
                context.Rols.Add(new Models.Entities.Rol { Name = "PersonalTrainer" });
                context.SaveChanges();
            }
            if (!context.Customers.Any(x => x.RolId == context.Rols.FirstOrDefault(f => f.Name == "Admin").Id))
            {
                var md5password = MD5EncryptionCustom.MD5Encryption("123321");
                context.Customers.Add(new Models.Entities.Customer { Name = "Administrator", Surname = "Administrator", Email = "admin@bbgym.com", Password = md5password, VerPassword = md5password, RolId = 1, SecurityQuestion = "Kaç cm ?", SecurityAnswer = "5cm" });
                context.SaveChanges();
            }

            Trigger trigger = new Trigger();
            trigger.QuestTrigger();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
