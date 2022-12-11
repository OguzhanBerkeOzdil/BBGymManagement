using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BBGymManagement.Areas.Admin.Controllers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly HttpContext _httpContext;
        public AdminAuthorizeAttribute()
        {
            this._httpContext = HttpContext.Current;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

            if (_httpContext.User != null && _httpContext.User.Identity != null && _httpContext.User.Identity is FormsIdentity)
            {
                var formsIdentity = (FormsIdentity)_httpContext.User.Identity;

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