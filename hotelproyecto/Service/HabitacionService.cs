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
                Comentarios = vm.Comentarios
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
                Comentarios = vm.Comentarios
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
                Comentarios = h.Comentarios
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
                Comentarios = habitacion.Comentarios
            };
        }

       


        #endregion
    }
}
