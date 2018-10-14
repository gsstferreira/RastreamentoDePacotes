using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common.Controllers
{
    class ErrorController : Controller 
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}
