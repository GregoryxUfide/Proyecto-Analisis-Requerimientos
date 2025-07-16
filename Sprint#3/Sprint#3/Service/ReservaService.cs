using Sprint_2.Data;
using Sprint_2.Models;
using Sprint_2.ViewModel;
using Sprint2.ViewModel;

namespace Sprint_2.Services
{
    public class ReservaService
    {
        private readonly ReservaData _reservaData;

        public ReservaService(ReservaData reservaData)
        {
            _reservaData = reservaData;
        }

        #region "Crear"
        public async Task CrearReservaAsync(ReservaViewModel vm)
        {
            var reserva = new Reserva
            {
                IdReserva = vm.IdReserva,
                FechaInicio = vm.FechaInicio,
                FechaFinal = vm.FechaFinal,
                NombreReservante = vm.NombreReservante,
                Telefono = vm.Telefono,
                Correo = vm.Correo,
                NumHabitacion = vm.NumHabitacion
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
                NombreReservante = vm.NombreReservante,
                Telefono = vm.Telefono,
                Correo = vm.Correo,
                NumHabitacion = vm.NumHabitacion
            };

            await _reservaData.ActualizarReservaAsync(reserva);
        }
        #endregion

        #region "Eliminar"
        public async Task EliminarReservaAsync(int id)
        {
            await _reservaData.EliminarReservaAsync(id);
        }
        #endregion

        #region "Listar"
        public async Task<List<ReservaViewModel>> ListarReservasAsync()
        {
            var reservas = await _reservaData.ListarReservasAsync();
            return reservas.Select(r => new ReservaViewModel
            {
                IdReserva = r.IdReserva,
                FechaInicio = r.FechaInicio,
                FechaFinal = r.FechaFinal,
                NombreReservante = r.NombreReservante,
                Telefono = r.Telefono,
                Correo = r.Correo,
                NumHabitacion = r.NumHabitacion
            }).ToList();
        }
        #endregion

        #region "Listar por Fecha"
        public async Task<List<ReservaViewModel>> ListarReservasPorFechaInicioAsync(DateTime fechaInicio)
        {
            var reservas = await _reservaData.ListarReservasPorFechaInicioAsync(fechaInicio);
            return reservas.Select(r => new ReservaViewModel
            {
                IdReserva = r.IdReserva,
                FechaInicio = r.FechaInicio,
                FechaFinal = r.FechaFinal,
                NombreReservante = r.NombreReservante,
                Telefono = r.Telefono,
                Correo = r.Correo,
                NumHabitacion = r.NumHabitacion
            }).ToList();
        }
        #endregion

        #region "Obtener por Id"
        public async Task<ReservaViewModel?> ObtenerReservaViewModelPorIdAsync(int id)
        {
            var reserva = await _reservaData.ObtenerReservaPorIdAsync(id);
            if (reserva == null) return null;

            return new ReservaViewModel
            {
                IdReserva = reserva.IdReserva,
                FechaInicio = reserva.FechaInicio,
                FechaFinal = reserva.FechaFinal,
                NombreReservante = reserva.NombreReservante,
                Telefono = reserva.Telefono,
                Correo = reserva.Correo,
                NumHabitacion = reserva.NumHabitacion
            };
        }
        #endregion
    }
}
