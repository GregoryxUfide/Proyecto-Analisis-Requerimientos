using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;


namespace hotelproyecto.Services
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

            var (codigo, mensaje) = await _reservaData.CrearReservaAsync(reserva);

            if (codigo == -1)
                throw new Exception("La habitación especificada no existe.");

            if (codigo == -2)
                throw new Exception("Ya existe una reserva para esa habitación en las fechas indicadas.");

            if (codigo != 1)
                throw new Exception($"Error al crear la reserva: {mensaje}");
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarReservaAsync(ReservaViewModel vm)
        {
            // Opcional: Puedes hacer validación local de conflictos si quieres
            var reservasExistentes = await _reservaData.ListarReservasAsync();

            bool existeConflicto = reservasExistentes.Any(r =>
                r.IdReserva != vm.IdReserva &&
                r.NumHabitacion == vm.NumHabitacion &&
                (
                    (vm.FechaInicio >= r.FechaInicio && vm.FechaInicio < r.FechaFinal) ||
                    (vm.FechaFinal > r.FechaInicio && vm.FechaFinal <= r.FechaFinal) ||
                    (vm.FechaInicio <= r.FechaInicio && vm.FechaFinal >= r.FechaFinal)
                )
            );

            if (existeConflicto)
                throw new Exception("Ya existe una reserva para esa habitación en las fechas indicadas.");

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
