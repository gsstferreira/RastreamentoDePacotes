using Common.DBServices;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common.Controllers
{
    public class BaseController : Controller
    {
        protected Usuario AuthUser { get; set; }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            string auth = filterContext.RequestContext.HttpContext.Request.Headers.Get("Authorization");
            if (auth == null)
            {
                RequestResponse(filterContext, 401, "The request does not have an Authorization header.");
            }
            else
            {
                try
                {
                    AuthUser = UsuarioService.ObterPorId(new Guid(auth));

                    if (AuthUser == null)
                    {
                        RequestResponse(filterContext, 403, "The Authorization header does not correspond to a valid user.");
                    }
                }
                catch (Exception)
                {
                    RequestResponse(filterContext, 403, "The Authorization header is not in the correct format.");
                }
            }

        }

        private void RequestResponse(AuthorizationContext Context, int HttpStatusCode, string message)
        {
            var resp = Context.RequestContext.HttpContext.Response;

            resp.StatusCode = HttpStatusCode;
            resp.StatusDescription = message;
            resp.SuppressContent = true;

            Context.Result = new ErrorController().Index();
            Context.RequestContext.HttpContext.ApplicationInstance.CompleteRequest();
        }
    }
}
