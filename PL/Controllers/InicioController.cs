using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Inicio()
        {
            return View();
        }
    }
}
