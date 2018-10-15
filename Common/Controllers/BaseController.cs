using Common.DBServices;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Common.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Usuario AuthUser { get; set; }

        protected static JavaScriptSerializer serializer = new JavaScriptSerializer();

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool hasAllowAnonymous = filterContext.ActionDescriptor
            .GetCustomAttributes(typeof(AllowAnonymousAttribute), false)
            .Any();

            if(hasAllowAnonymous)
            {
                return;
            }
            else if(filterContext.HttpContext.Request.Headers.Get("Authorization") == null)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
