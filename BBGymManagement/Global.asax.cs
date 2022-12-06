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
            EFDbContext context= new EFDbContext();
            if (!context.Customers.Any(x => x.RolId == context.Rols.FirstOrDefault(f => f.Name == "Admin").Id))
            {
                context.Customers.Add(new Models.Entities.Customer {Name= "Administrator",Surname= "Administrator",Email="admin@bbgym.com",Password="123321",VerPassword="123321",RolId=1,SecurityQuestion="Kaç cm ?",SecurityAnswer="5cm" });
                context.SaveChanges();
            }
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
