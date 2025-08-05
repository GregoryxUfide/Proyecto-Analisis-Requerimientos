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
            ViewBag.Facebook = "https://facebook.com/tuperfil";
            ViewBag.Instagram = "https://www.instagram.com/granhotel_desamparados?utm_source=ig_web_button_share_sheet&igsh=ZDNlZDc0MzIxNw==";
            ViewBag.WhatsApp = "https://wa.me/tunumerowhatsapp";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Reserva()
        {
            ViewBag.Telefono = "+1 (234) 567-890";
            ViewBag.Correo = "reservas@granhoteldeseado.com";
            ViewBag.WhatsApp = "1234567890"; // Número sin símbolos ni espacios para enlace WhatsApp

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
