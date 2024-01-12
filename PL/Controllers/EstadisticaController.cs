using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EstadisticaController : Controller
    {
        public IActionResult Estadistica()
        {
            return View();
        }
    }
}
