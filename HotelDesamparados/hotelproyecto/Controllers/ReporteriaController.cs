using Microsoft.AspNetCore.Mvc;
using hotelproyecto.Service;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Controllers
{
    public class ReporteriaController : Controller
    {
        private readonly ReporteriaService _service;

        public ReporteriaController(ReporteriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tablas = _service.ObtenerTablasDisponibles();
            return View(tablas);
        }

        [HttpPost]
        public async Task<IActionResult> DescargarPDF(string nombreTabla)
        {
            if (nombreTabla == "Producto")
            {
                var producto = await _service.ObtenerDatosProductosAsync();
                var pdfBytes = _service.GenerarPdfDeProductos(producto);
                return File(pdfBytes, "application/pdf", "Reporte_Productos.pdf");
            }

            if (nombreTabla == "UbicacionProducto")
            {
                var ubicaciones = await _service.ObtenerDatosUbicacionesAsync();
                var pdfBytes = _service.GenerarPdfDeUbicaciones(ubicaciones);
                return File(pdfBytes, "application/pdf", "Reporte_Ubicaciones.pdf");
            }

            if (nombreTabla == "Usuario")
            {
                var usuarios = await _service.ObtenerDatosUsuariosAsync();
                var pdfBytes = _service.GenerarPdfDeUsuarios(usuarios);
                return File(pdfBytes, "application/pdf", "Reporte_Usuarios.pdf");
            }
            
            if (nombreTabla == "Roles")
            {
                var roles = await _service.ObtenerDatosRolesAsync();
                var pdfBytes = _service.GenerarPdfDeRoles(roles);
                return File(pdfBytes, "application/pdf", "Reporte_Roles.pdf");
            }

            return RedirectToAction("Index");
        }
    }
}
