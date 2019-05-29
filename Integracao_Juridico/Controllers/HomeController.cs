using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Integracao_Juridico.Models;

namespace Integracao_Juridico.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        } 

        public string DadosProcesso()
        {
            return new ConsultaTestePjePush().Consultar();
        }

        public string ObterDetalhesProcesso(string id)
        {
            return new ConsultaTestePjePush().Consultar(id);
        }
    }
}
