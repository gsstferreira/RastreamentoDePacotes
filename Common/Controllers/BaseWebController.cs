using Common.DBServices;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using static Common.Services.RequestReaderService;

namespace Common.Controllers
{
    public class BaseWebController : Controller
    {

        protected readonly EmpresaService _empresaService;
        protected readonly EnderecoService _enderecoService;
        protected readonly EstacaoService _estacaoService;
        protected readonly LocalizacaoService _localizacaoService;
        protected readonly PacoteService _pacoteService;
        protected readonly RotaService _rotaService;
        protected readonly VeiculoService _veiculoService;
        protected readonly OcorrenciaService _ocorrenciaService;

        public BaseWebController()
        {
            _empresaService = new EmpresaService();
            _enderecoService = new EnderecoService();
            _estacaoService = new EstacaoService();
            _localizacaoService = new LocalizacaoService();
            _pacoteService = new PacoteService();
            _rotaService = new RotaService();
            _veiculoService = new VeiculoService();
            _ocorrenciaService = new OcorrenciaService();
        }

        protected static string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        protected override void OnAuthorization(AuthorizationContext filterContext){ return; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext){ return; }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _empresaService.Dispose();
            _enderecoService.Dispose();
            _estacaoService.Dispose();
            _localizacaoService.Dispose();
            _pacoteService.Dispose();
            _rotaService.Dispose();
            _veiculoService.Dispose();
            _ocorrenciaService.Dispose();
        }
    }
}
