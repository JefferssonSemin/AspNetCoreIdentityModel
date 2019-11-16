using AspNetCoreIdentity.Extensions;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.LogTrace("usuario acessou a home");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClain()
        {
            return View("Secret");
        }

        [Authorize(Policy = "PodeGravar")]
        public IActionResult SecretClainGravar()
        {
            return View("Secret");
        }

        [CustomAuthorization.ClaimsAuthorizeAttribute("Produtos", "Ler")]
        public IActionResult ClaimsCustom()
        {
            return View("Secret");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();
            if (id == 500)
            {
                modelErro.Message = "Ocorreu um erro interno";
                modelErro.Titulo = "Ocorreu um erro";
                modelErro.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelErro.Message = "A pagina solicitada n foi encontrada.";
                modelErro.Titulo = "Pagina não encontrada";
                modelErro.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelErro.Message = "Você não tem autorização para acessar a pagina.";
                modelErro.Titulo = "Acesso negado";
                modelErro.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }
            return View("Error", modelErro);
        }
    }
}