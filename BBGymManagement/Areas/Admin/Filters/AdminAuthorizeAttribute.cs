using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BBGymManagement.Areas.Admin.Controllers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext == null ||
                filterContext.HttpContext.Request == null ||
                !filterContext.HttpContext.Request.IsAuthenticated ||
                !(filterContext.HttpContext.User.Identity is FormsIdentity))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

            if (filterContext.HttpContext.User != null && filterContext.HttpContext.User.Identity != null && filterContext.HttpContext.User.Identity is FormsIdentity)
            {
                var formsIdentity = (FormsIdentity)filterContext.HttpContext.User.Identity;

                if (formsIdentity.Ticket == null)
                    throw new ArgumentNullException("ticket");

                var ticket = FormsAuthentication.Decrypt(formsIdentity.Ticket.Name);
                var userdata = ticket.UserData;

                if (String.IsNullOrWhiteSpace(userdata))
                    filterContext.Result = new HttpUnauthorizedResult();

                var roleName = userdata.Split(';')[1];

                if (roleName != "Admin")
                    filterContext.Result = new HttpUnauthorizedResult();

            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}