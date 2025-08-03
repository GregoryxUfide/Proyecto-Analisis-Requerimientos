using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Services
{
    public class LimpiezaHabitacionService
    {
        private readonly LimpiezaHabitacionData _limpiezaData;
        private readonly HabitacionService  _habitacionService;

       public LimpiezaHabitacionService(LimpiezaHabitacionData limpiezaData, HabitacionService habitacionService)
        {
            _limpiezaData = limpiezaData;
            _habitacionService= habitacionService;
        }

        #region "Crear"
        public async Task CrearLimpiezaAsync(LimpiezaHabitacionViewModel vm)
        {
            var limpieza = new LimpiezaHabitacion
            {
                TareasCompletadas = vm.TareasCompletadas,
                NombreConserje = vm.NombreConserje,
                Foto = vm.FotoArchivo != null ? await ConvertFileToByteArray(vm.FotoArchivo) : null,
                FechaHora = DateTime.Now,
                HabitacionId = vm.HabitacionId
            };

            await _limpiezaData.CrearLimpiezaAsync(limpieza);
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarLimpiezaAsync(LimpiezaHabitacionViewModel vm)
        {
            var limpiezaExistente = await _limpiezaData.ObtenerLimpiezaPorIdAsync(vm.Id);

            var limpieza = new LimpiezaHabitacion
            {
                Id = vm.Id,
                TareasCompletadas = vm.TareasCompletadas,
                NombreConserje = vm.NombreConserje,
                Foto = vm.FotoArchivo != null ? await ConvertFileToByteArray(vm.FotoArchivo) : limpiezaExistente?.Foto,
                FechaHora = DateTime.Now,
                HabitacionId = limpiezaExistente != null ? limpiezaExistente.HabitacionId : vm.HabitacionId
            };

            await _limpiezaData.ActualizarLimpiezaAsync(limpieza);
        }

        private async Task<byte[]> ConvertFileToByteArray(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
        #endregion

        #region "Listar"
        public async Task<List<LimpiezaHabitacionViewModel>> ListarLimpiezasAsync()
        {
            var limpiezas = await _limpiezaData.ListarLimpiezasAsync();
            var habitaciones = await _habitacionService.ListarHabitacionesAsync();

            return limpiezas.Select(l =>
            {
                var habitacion = habitaciones.FirstOrDefault(h => h.Id == l.HabitacionId);
                return new LimpiezaHabitacionViewModel
                {
                    Id = l.Id,
                    TareasCompletadas = l.TareasCompletadas,
                    NombreConserje = l.NombreConserje,
                    Foto = l.Foto,
                    FechaHora = l.FechaHora,
                    HabitacionId = l.HabitacionId,
                    NumHabitacion = habitacion?.NumHabitacion ?? 0
                };
            }).ToList();
        }
        #endregion

        #region "Obtener por Id"
        public async Task<LimpiezaHabitacionViewModel?> ObtenerLimpiezaViewModelPorIdAsync(int id)
        {
            var limpieza = await _limpiezaData.ObtenerLimpiezaPorIdAsync(id);
            if (limpieza == null) return null;

            var habitaciones = await _habitacionService.ListarHabitacionesSuciasAsync();
            var habitacionesVm = habitaciones.Select(h => new HabitacionViewModel
            {
                Id = h.Id,
                NumHabitacion = h.NumHabitacion
            }).ToList();

            var habitacionActual = habitacionesVm.FirstOrDefault(h => h.Id == limpieza.HabitacionId);

            return new LimpiezaHabitacionViewModel
            {
                Id = limpieza.Id,
                TareasCompletadas = limpieza.TareasCompletadas,
                NombreConserje = limpieza.NombreConserje,
                Foto = limpieza.Foto,
                FechaHora = limpieza.FechaHora,
                HabitacionId = limpieza.HabitacionId,
                NumHabitacion = habitacionActual?.NumHabitacion ?? 0,
                Habitaciones = habitacionesVm // lista completa para dropdown
            };
        }

        #endregion

        #region "Listar por conserje"
        public async Task<List<LimpiezaHabitacionViewModel>> ListarLimpiezasPorConserjeAsync(string nombreConserje)
        {
            var limpiezas = await _limpiezaData.ListarLimpiezasPorConserjeAsync(nombreConserje);
            var habitaciones = await _habitacionService.ListarHabitacionesAsync();

            return limpiezas.Select(l =>
            {
                var habitacion = habitaciones.FirstOrDefault(h => h.Id == l.HabitacionId);
                return new LimpiezaHabitacionViewModel
                {
                    Id = l.Id,
                    TareasCompletadas = l.TareasCompletadas,
                    FechaHora = l.FechaHora,
                    HabitacionId = l.HabitacionId,
                    NumHabitacion = habitacion?.NumHabitacion ?? 0
                };
            }).ToList();
        }
        #endregion

        #region "Listar habitaciones"
        public async Task<List<HabitacionViewModel>> ObtenerTodasLasHabitacionesAsync()
        {
            return await _habitacionService.ListarHabitacionesAsync();
        }
        #endregion
    }
}
