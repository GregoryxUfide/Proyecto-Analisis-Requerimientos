using Sprint_2.Data;
using Sprint_2.Models;
using Sprint2.ViewModel;

namespace Sprint_2.Services
{
    public class LimpiezaHabitacionService
    {
        private readonly LimpiezaHabitacionData _limpiezaData;

        public LimpiezaHabitacionService(LimpiezaHabitacionData limpiezaData)
        {
            _limpiezaData = limpiezaData;
        }

        #region "Crear"
        public async Task CrearLimpiezaAsync(LimpiezaHabitacionViewModel vm)
        {
            var limpieza = new LimpiezaHabitacion
            {
                TareasCompletadas = vm.TareasCompletadas,
                NombreConserje = vm.NombreConserje,
                Foto = vm.Foto,
                FechaHora = DateTime.Now
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
                FechaHora = DateTime.Now
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
            return limpiezas.Select(l => new LimpiezaHabitacionViewModel
            {
                Id = l.Id,
                TareasCompletadas = l.TareasCompletadas,
                NombreConserje = l.NombreConserje,
                FechaHora = l.FechaHora
            }).ToList();
        }
        #endregion

        #region "Obtener por Id"
        public async Task<LimpiezaHabitacionViewModel?> ObtenerLimpiezaViewModelPorIdAsync(int id)
        {
            var limpieza = await _limpiezaData.ObtenerLimpiezaPorIdAsync(id);
            if (limpieza == null) return null;

            return new LimpiezaHabitacionViewModel
            {
                Id = limpieza.Id,
                TareasCompletadas = limpieza.TareasCompletadas,
                NombreConserje = limpieza.NombreConserje,
                Foto = limpieza.Foto,
                FechaHora = limpieza.FechaHora
            };
        }
        #endregion

        #region "Listar por conserje"
        public async Task<List<LimpiezaHabitacionViewModel>> ListarLimpiezasPorConserjeAsync(string nombreConserje)
        {
            var limpiezas = await _limpiezaData.ListarLimpiezasPorConserjeAsync(nombreConserje);
            return limpiezas.Select(l => new LimpiezaHabitacionViewModel
            {
                Id = l.Id,
                TareasCompletadas = l.TareasCompletadas,
                FechaHora = l.FechaHora
            }).ToList();
        }
        #endregion
    }
}