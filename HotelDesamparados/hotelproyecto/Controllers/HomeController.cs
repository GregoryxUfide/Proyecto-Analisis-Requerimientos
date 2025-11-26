using Microsoft.AspNetCore.Mvc;
using hotelproyecto.ViewModel;
using System.Diagnostics;

namespace hotelproyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Reserva()
        {
            ViewBag.Telefono = "+(506)8646-7728";
            ViewBag.Correo = "recuperacionhotelproy@gmail.com";
            ViewBag.WhatsApp = "https://wa.me/50686467728";

            return View();
        }
        public IActionResult AccesoDenegado()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
