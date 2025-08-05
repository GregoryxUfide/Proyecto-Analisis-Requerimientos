using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace hotelproyecto.Filters
{
    public class PermisoAttribute : ActionFilterAttribute
    {
        private static readonly Dictionary<string, Dictionary<string, List<string>>> _permisosPorRol = new()
        {
            ["Admin"] = new Dictionary<string, List<string>>
            {
                ["Dashboard"] = new() { "*" },
                ["Rol"] = new() { "*" },
                ["Usuario"] = new() { "*" },
                ["Empleado"] = new() { "*" },
                ["Contabilidad"] = new() { "*" },
                ["Habitacion"] = new() { "*" },
                ["Reserva"] = new() { "*" },
                ["PuntoVenta"] = new() { "*" },
                ["LimpiezaHabitacion"] = new() { "*" },
                ["UbicacionProducto"] = new() { "*" },
                ["Producto"] = new() { "*" },
                ["Reporteria"] = new() { "*" },
            },
            ["Vendedor"] = new Dictionary<string, List<string>>
            {
                ["Dashboard"] = new() { "*" },
                ["Habitacion"] = new() { "Index", "Detalles" },
                ["Reserva"] = new() { "*" },
                ["PuntoVenta"] = new() { "*" },
                ["Reporteria"] = new() { "*" },
            },
            ["Conserje"] = new Dictionary<string, List<string>>
            {
                ["Dashboard"] = new() { "*" },
                ["Habitacion"] = new() { "Index", "Detalles", "CambiarEstado" },
                ["LimpiezaHabitacion"] = new() { "*" },
            },
            ["Usuario"] = new Dictionary<string, List<string>>
            {
                ["Usuario"] = new() {"EditarMiPerfil", "CambiarMiContrasena"}
            }
        };
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controlador = context.RouteData.Values["controller"]?.ToString();
            var accion = context.RouteData.Values["action"]?.ToString();
            
            var rutasPublicas = new List<(string Controller, string Action)>
                {
                    ("Home", "Index"),
                    ("Home", "Privacy"),
                    ("Home", "Reserva"),
                    ("Home", "Acceso Denegado"),
                    ("Home", "Error"),
                    ("Auth", "Login"),
                    ("Auth", "Register"),
                    ("Auth", "SolicitarToken"),
                    ("Auth", "IngresarToken"),
                    ("Auth", "NuevaContrasena"),
                    ("Auth", "Logout"),
                };

            if (rutasPublicas.Any(r =>
                string.Equals(r.Controller, controlador, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(r.Action, accion, StringComparison.OrdinalIgnoreCase)))
            {
                base.OnActionExecuting(context);
                return;
            }

            var rol = context.HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rol) || !_permisosPorRol.ContainsKey(rol))
            {
                context.Result = new RedirectToActionResult("AccesoDenegado", "Home", null);
                return;
            }

            var controladores = _permisosPorRol[rol];

            if (!controladores.ContainsKey(controlador))
            {
                context.Result = new RedirectToActionResult("AccesoDenegado", "Home", null);
                return;
            }

            var acciones = controladores[controlador];

            if (!acciones.Contains("*") && !acciones.Contains(accion))
            {
                context.Result = new RedirectToActionResult("AccesoDenegado", "Home", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}