using Microsoft.AspNetCore.Mvc;

namespace hotelproyecto.Controllers
{    
    public class PermisoController : Controller
    {
        protected bool TienePermisoTrabajador()
        {
            var rol = HttpContext.Session.GetString("Rol");
            return !string.IsNullOrEmpty(rol) &&
                   (rol == "Admin" || rol == "Vendedor" || rol == "Conserje");
        }

        protected bool TieneRol(string rolRequerido)
        {
            var rol = HttpContext.Session.GetString("Rol");
            return rol != null && rol.Equals(rolRequerido, StringComparison.OrdinalIgnoreCase);
        }

        protected bool EstaLogueado()
        {
            return HttpContext.Session.GetInt32("UsuarioID") != null;
        }
    }
}
