using hotelproyecto.Data;
using hotelproyecto.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Services
{
    public class PuntoVentaService
    {
        private readonly PuntoVentaData _puntoVentaData;
        private readonly ReservaService _reservaService;
        private readonly EmpleadoService _empleadoService;
        private readonly UsuarioData _usuarioData;
        private readonly RolService _rolService;

        public PuntoVentaService(PuntoVentaData puntoVentaData, ReservaService reservaService, EmpleadoService empleadoService, UsuarioData usuarioData, RolService rolService)
        {
            _puntoVentaData = puntoVentaData;
            _reservaService = reservaService;
            _empleadoService = empleadoService;
            _usuarioData = usuarioData;
            _rolService = rolService;
        }

        #region "Listar"
        public async Task<List<PuntoVentaViewModel>> ListarPuntosVentaViewModelAsync()
        {
            var puntosVenta = await _puntoVentaData.ListarPuntosVentaAsync();

            return puntosVenta.Select(pv => new PuntoVentaViewModel
            {
                Id = pv.Id,
                DescripcionVenta = pv.DescripcionVenta,
                Metodo_Pago = pv.Metodo_Pago,
                Descuento = pv.Descuento,
                ReservaId = pv.ReservaId,
                EmpleadoId = pv.EmpleadoId
            }).ToList();
        }
        #endregion

        #region "Obtener por Id"
        public async Task<PuntoVentaViewModel?> ObtenerPuntoVentaViewModelPorIdAsync(int id)
        {
            var puntoVenta = await _puntoVentaData.ObtenerPuntoVentaPorIdAsync(id);
            if (puntoVenta == null) return null;

            var reservas = await _reservaService.ListarReservasViewModelAsync();
            var empleados = await _empleadoService.ListarEmpleadosViewModelAsync();
            
            var empleadoNombre = empleados.FirstOrDefault(e => e.UsuarioId == puntoVenta.EmpleadoId)?.UsuarioNombre ?? "";
            
            var reservaDetalle = reservas
                .Where(r => r.IdReserva == puntoVenta.ReservaId)
                .Select(r => $"{r.IdReserva} - {r.Correo}")
                .FirstOrDefault() ?? "";

            return new PuntoVentaViewModel
            {
                Id = puntoVenta.Id,
                DescripcionVenta = puntoVenta.DescripcionVenta,
                Metodo_Pago = puntoVenta.Metodo_Pago,
                Descuento = puntoVenta.Descuento,
                ReservaId = puntoVenta.ReservaId,
                EmpleadoId = puntoVenta.EmpleadoId,
                Reservas = reservas.Select(r => new SelectListItem
                {
                    Value = r.IdReserva.ToString(),
                    Text = $"{r.IdReserva} - {r.Correo}"
                }).ToList(),
                Empleados = empleados.Select(e => new SelectListItem
                {
                    Value = e.UsuarioId.ToString(),
                    Text = e.UsuarioNombre
                }).ToList(),

                // Aquí agregamos las propiedades para mostrar en detalles
                EmpleadoNombre = empleadoNombre,
                ReservaDetalle = reservaDetalle
            };
        }
        #endregion

        #region "Crear"
        public async Task CrearPuntoVentaAsync(PuntoVentaViewModel vm)
        {
            var puntoVenta = new PuntoVenta
            {
                DescripcionVenta = vm.DescripcionVenta,
                Metodo_Pago = vm.Metodo_Pago,
                Descuento = vm.Descuento,
                ReservaId = vm.ReservaId,
                EmpleadoId = vm.EmpleadoId
            };

            await _puntoVentaData.CrearPuntoVentaAsync(puntoVenta);
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarPuntoVentaAsync(PuntoVentaViewModel vm)
        {
            var puntoVenta = new PuntoVenta
            {
                Id = vm.Id,
                DescripcionVenta = vm.DescripcionVenta,
                Metodo_Pago = vm.Metodo_Pago,
                Descuento = vm.Descuento,
                ReservaId = vm.ReservaId,
                EmpleadoId = vm.EmpleadoId
            };

            await _puntoVentaData.ActualizarPuntoVentaAsync(puntoVenta);
        }
        #endregion

        #region "Dropdowns"
        public async Task<List<SelectListItem>> ObtenerReservasSelectListAsync()
        {
            var reservas = await _reservaService.ListarReservasActivasViewModelAsync();

            return reservas.Select(r => new SelectListItem
            {
                Value = r.IdReserva.ToString(),
                Text = $"{r.IdReserva} - {r.Correo}"
            }).ToList();
        }

        public async Task<List<SelectListItem>> ObtenerEmpleadosSelectListAsync()
        {
            
            var roles = await _rolService.ListarRolesAsync();
            var idAdmin = roles.FirstOrDefault(r => r.Nombre.ToLower() == "admin")?.Id;
            var idVendedor = roles.FirstOrDefault(r => r.Nombre.ToLower() == "vendedor")?.Id;
           
            if (idAdmin == null && idVendedor == null)
                return new List<SelectListItem>();
            
            var empleados = await _empleadoService.ListarEmpleadosViewModelAsync(); 
            var idsUsuarios = empleados.Select(e => e.UsuarioId).Distinct().ToList();
            
            var usuariosFiltrados = await _usuarioData.ListarUsuariosPorFiltroAsync(null, true);

            var resultado = usuariosFiltrados
                .Where(u => idsUsuarios.Contains(u.Id) &&
                           (u.RolId == idAdmin || u.RolId == idVendedor))
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Nombre} {u.Apellidos} ({u.Rol?.Nombre})"
                }).ToList();

            return resultado;
        }
        #endregion
    }
}
