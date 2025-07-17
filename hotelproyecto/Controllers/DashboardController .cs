using Microsoft.AspNetCore.Mvc;

namespace hotelproyecto.Controllers
{    
    public class DashboardController : PermisoController
    {        
        public IActionResult Index()
        {
            if (!TienePermisoTrabajador())
                return RedirectToAction("Login", "Auth");
            // Puedes agregar l�gica para obtener datos del dashboard aqu�

            return View();
        }
    }
}
