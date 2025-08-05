using Microsoft.AspNetCore.Mvc;

namespace hotelproyecto.Controllers
{    
    public class DashboardController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }
    }
}
