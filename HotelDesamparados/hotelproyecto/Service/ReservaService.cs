using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotelproyecto.Services
{
    public class ReservaService
    {
        private readonly ReservaData _reservaData;
        private readonly HabitacionService _habitacionService;

        public ReservaService(ReservaData reservaData, HabitacionService habitacionService)
        {
            _reservaData = reservaData;
            _habitacionService = habitacionService;
        }

        #region "Listar"
        public async Task<List<ReservaViewModel>> ListarReservasViewModelAsync()
        {
            var reservas = await _reservaData.ListarReservasAsync();
            var habitaciones = await _habitacionService.ListarHabitacionesAsync();

            return reservas.Select(r => new ReservaViewModel
            {
                IdReserva = r.IdReserva,
                FechaInicio = r.FechaInicio,
                FechaFinal = r.FechaFinal,
                Nombre = r.Nombre,
                Telefono = r.Telefono,
                Correo = r.Correo,
                HabitacionId = r.HabitacionId,
                Estado = r.Estado,
                HabitacionNombre = habitaciones.FirstOrDefault(h => h.Id == r.HabitacionId)?.NumHabitacion.ToString()
            }).ToList();
        }
        #endregion

        #region "ObtenerPorId"
        public async Task<ReservaViewModel?> ObtenerReservaViewModelPorIdAsync(int id)
        {
            var reserva = await _reservaData.ObtenerReservaPorIdAsync(id);
            if (reserva == null) return null;

            var habitaciones = await _habitacionService.ListarHabitacionesAsync();

            return new ReservaViewModel
            {
                IdReserva = reserva.IdReserva,
                FechaInicio = reserva.FechaInicio,
                FechaFinal = reserva.FechaFinal,
                Nombre = reserva.Nombre,
                Telefono = reserva.Telefono,
                Correo = reserva.Correo,
                HabitacionId = reserva.HabitacionId,
                Estado = reserva.Estado,
                Habitaciones = habitaciones.Select(h => new SelectListItem
                {
                    Value = h.Id.ToString(),
                    Text = h.NumHabitacion.ToString()
                }).ToList(),
                HabitacionNombre = habitaciones.FirstOrDefault(h => h.Id == reserva.HabitacionId)?.NumHabitacion.ToString()
            };
        }
        #endregion

        #region "Crear"
        public async Task CrearReservaAsync(ReservaViewModel vm)
        {
            var reserva = new Reserva
            {
                FechaInicio = vm.FechaInicio,
                FechaFinal = vm.FechaFinal,
                Nombre = vm.Nombre,
                Telefono = vm.Telefono,
                Correo = vm.Correo,
                HabitacionId = vm.HabitacionId,
                Estado = vm.Estado
            };

            await _reservaData.CrearReservaAsync(reserva);
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarReservaAsync(ReservaViewModel vm)
        {
            var reserva = new Reserva
            {
                IdReserva = vm.IdReserva,
                FechaInicio = vm.FechaInicio,
                FechaFinal = vm.FechaFinal,
                Nombre = vm.Nombre,
                Telefono = vm.Telefono,
                Correo = vm.Correo,
                HabitacionId = vm.HabitacionId,
                Estado = vm.Estado
            };

            await _reservaData.ActualizarReservaAsync(reserva);
        }
        #endregion

        #region "Dropdown Habitaciones"
        public async Task<List<SelectListItem>> ObtenerHabitacionesSelectListAsync()
        {
            var habitaciones = await _habitacionService.ListarHabitacionesLimpiasAsync();

            return habitaciones.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = h.NumHabitacion.ToString()
            }).ToList();
        }
        #endregion

        #region "Listar Reservas Activas"
        public async Task<List<ReservaViewModel>> ListarReservasActivasViewModelAsync()
        {
            var reservas = await _reservaData.ListarReservasAsync();
            var habitaciones = await _habitacionService.ListarHabitacionesAsync();

            // Filtrar solo las reservas activas
            var reservasActivas = reservas.Where(r => r.Estado).ToList();

            return reservasActivas.Select(r => new ReservaViewModel
            {
                IdReserva = r.IdReserva,
                FechaInicio = r.FechaInicio,
                FechaFinal = r.FechaFinal,
                Nombre = r.Nombre,
                Telefono = r.Telefono,
                Correo = r.Correo,
                HabitacionId = r.HabitacionId,
                Estado = r.Estado,
                HabitacionNombre = habitaciones.FirstOrDefault(h => h.Id == r.HabitacionId)?.NumHabitacion.ToString()
            }).ToList();
        }
        #endregion

        #region "Finalizar Reservas Automáticamente"
        public async Task ActualizarReservasFinalizadasAsync()
        {
            var reservas = await _reservaData.ListarReservasAsync();
            var ahora = DateTime.Now;

            var reservasFinalizadas = reservas
                .Where(r => r.Estado && r.FechaFinal < ahora)
                .ToList();

            foreach (var reserva in reservasFinalizadas)
            {
                await _reservaData.CambiarEstadoReservaAsync(reserva.IdReserva, false);
            }
        }
        #endregion
    }
}
