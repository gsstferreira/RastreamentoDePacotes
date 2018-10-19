using Common.DBServices;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using static Common.Services.RequestReaderService;

namespace Common.Controllers
{
    public class BaseApiController : Controller
    {
        protected Usuario _user = null;
        protected JsonObject _requestBody = null;

        protected readonly EmpresaService _empresaService;
        protected readonly EnderecoService _enderecoService;
        protected readonly EstacaoService _estacaoService;
        protected readonly LocalizacaoService _localizacaoService;
        protected readonly PacoteService _pacoteService;
        protected readonly RotaService _rotaService;
        protected readonly UsuarioService _usuarioService;
        protected readonly VeiculoService _veiculoService;

        public BaseApiController()
        {

            _empresaService = new EmpresaService();
            _enderecoService = new EnderecoService();
            _estacaoService = new EstacaoService();
            _localizacaoService = new LocalizacaoService();
            _pacoteService = new PacoteService();
            _rotaService = new RotaService();
            _veiculoService = new VeiculoService();

            _usuarioService = new UsuarioService().OpenSession();
        }

        protected static string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

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
            else
            {
                try
                {
                    Guid id = new Guid(filterContext.HttpContext.Request.Headers.Get("Authorization"));

                    _user = _usuarioService.ObterPorId(id);
                }
                catch (Exception)
                {
                    _user = null;
                }

                if(_user == null)
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.HttpMethod.Equals("POST"))
            {
                _requestBody = ReadRequestBody(Request);
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _empresaService.Dispose();
            _enderecoService.Dispose();
            _estacaoService.Dispose();
            _localizacaoService.Dispose();
            _pacoteService.Dispose();
            _rotaService.Dispose();
            _usuarioService.Dispose();
            _veiculoService.Dispose();

            _requestBody = null;
            _user = null;
        }
    }
}
