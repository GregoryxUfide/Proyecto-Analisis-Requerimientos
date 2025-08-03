using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Services
{
    public class HabitacionService
    {
        private readonly HabitacionData _habitacionData;

        public HabitacionService(HabitacionData habitacionData)
        {
            _habitacionData = habitacionData;
        }

        #region "Crear"
        public async Task CrearHabitacionAsync(HabitacionViewModel vm)
        {
            var habitacion = new Habitacion
            {
                Capacidad = vm.Capacidad,
                Precio = vm.Precio,
                NumHabitacion = vm.NumHabitacion,
                NumCamas = vm.NumCamas,
                Extras = vm.Extras,
                Comentarios = vm.Comentarios,
                Estado = true // Al crear, se asume activa
            };

            await _habitacionData.CrearHabitacionAsync(habitacion);
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarHabitacionAsync(HabitacionViewModel vm)
        {
            var habitacion = new Habitacion
            {
                Id = vm.Id,
                Capacidad = vm.Capacidad,
                Precio = vm.Precio,
                NumHabitacion = vm.NumHabitacion,
                NumCamas = vm.NumCamas,
                Extras = vm.Extras,
                Comentarios = vm.Comentarios,
                Estado = vm.Estado // Aunque no se envía al SP, lo mantenemos por consistencia
            };

            await _habitacionData.ActualizarHabitacionAsync(habitacion);
        }
        #endregion

        #region "Listar"
        public async Task<List<HabitacionViewModel>> ListarHabitacionesAsync()
        {
            var habitaciones = await _habitacionData.ListarHabitacionesAsync();
            return habitaciones.Select(h => new HabitacionViewModel
            {
                Id = h.Id,
                Capacidad = h.Capacidad,
                Precio = h.Precio,
                NumHabitacion = h.NumHabitacion,
                NumCamas = h.NumCamas,
                Extras = h.Extras,
                Comentarios = h.Comentarios,
                Estado = h.Estado
            }).ToList();
        }
        #endregion

        #region "Listar Habitaciones Limpias"
        public async Task<List<HabitacionViewModel>> ListarHabitacionesLimpiasAsync()
        {
            var habitaciones = await _habitacionData.ListarHabitacionesAsync();
            var limpias = habitaciones.Where(h => h.Estado == true).ToList();  // Solo habitaciones limpias

            return limpias.Select(h => new HabitacionViewModel
            {
                Id = h.Id,
                Capacidad = h.Capacidad,
                Precio = h.Precio,
                NumHabitacion = h.NumHabitacion,
                NumCamas = h.NumCamas,
                Extras = h.Extras,
                Comentarios = h.Comentarios,
                Estado = h.Estado
            }).ToList();
        }
        #endregion

        #region "Listar Habitaciones Sucias"
        public async Task<List<HabitacionViewModel>> ListarHabitacionesSuciasAsync()
        {
            var habitaciones = await _habitacionData.ListarHabitacionesAsync();
            var sucias = habitaciones.Where(h => h.Estado == false).ToList();  // Solo habitaciones sucias

            return sucias.Select(h => new HabitacionViewModel
            {
                Id = h.Id,
                Capacidad = h.Capacidad,
                Precio = h.Precio,
                NumHabitacion = h.NumHabitacion,
                NumCamas = h.NumCamas,
                Extras = h.Extras,
                Comentarios = h.Comentarios,
                Estado = h.Estado
            }).ToList();
        }
        #endregion

        #region "Obtener por Id"
        public async Task<HabitacionViewModel?> ObtenerHabitacionViewModelPorIdAsync(int id)
        {
            var habitacion = await _habitacionData.ObtenerHabitacionPorIdAsync(id);
            if (habitacion == null) return null;

            return new HabitacionViewModel
            {
                Id = habitacion.Id,
                Capacidad = habitacion.Capacidad,
                Precio = habitacion.Precio,
                NumHabitacion = habitacion.NumHabitacion,
                NumCamas = habitacion.NumCamas,
                Extras = habitacion.Extras,
                Comentarios = habitacion.Comentarios,
                Estado = habitacion.Estado
            };
        }
        #endregion

        #region "Cambiar estado"
        public async Task CambiarEstadoHabitacionAsync(int id, bool estado)
        {
            await _habitacionData.CambiarEstadoHabitacionAsync(id, estado);
        }
        #endregion
    }
}
