using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Service
{
    public class ContabilidadService
    {
        private readonly ContabilidadData _contabilidadData;

        public ContabilidadService(ContabilidadData contabilidadData)
        {
            _contabilidadData = contabilidadData;
        }


        #region "Crear"
        public async Task CrearContabilidadAsync(ContabilidadViewModel vm)
        {
            var contabilidad = new Contabilidad
            {
                Fecha = vm.Fecha,
                Monto = vm.Monto,
                Detalle = vm.Detalle,
                Comentario = vm.Comentario
            };

            await _contabilidadData.CrearContabilidadAsync(contabilidad);
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarContabilidadAsync(ContabilidadViewModel vm)
        {
            var contabilidad = new Contabilidad
            {
                IdContabilidad = vm.IdContabilidad,
                Fecha = vm.Fecha,
                Monto = vm.Monto,
                Detalle = vm.Detalle,
                Comentario = vm.Comentario
            };

            await _contabilidadData.ActualizarContabilidadAsync(contabilidad);
        }
        #endregion

        #region "Listar"
        public async Task<List<ContabilidadViewModel>> ListarContabilidadAsync()
        {
            var lista = await _contabilidadData.ListarContabilidadAsync();

            return lista.Select(c => new ContabilidadViewModel
            {
                IdContabilidad = c.IdContabilidad,
                Fecha = c.Fecha,
                Monto = c.Monto,
                Detalle = c.Detalle,
                Comentario = c.Comentario
            }).ToList();
        }
        #endregion

        #region "Obtener por Id"
        public async Task<ContabilidadViewModel?> ObtenerContabilidadPorIdAsync(int id)
        {
            var c = await _contabilidadData.ObtenerContabilidadPorIdAsync(id);
            if (c == null) return null;

            return new ContabilidadViewModel
            {
                IdContabilidad = c.IdContabilidad,
                Fecha = c.Fecha,
                Monto = c.Monto,
                Detalle = c.Detalle,
                Comentario = c.Comentario
            };
        }
        #endregion

        #region "Filtro"
        public async Task<List<ContabilidadViewModel>> FiltrarPorFechaAsync(int? mes, int? anio)
        {
            var lista = await _contabilidadData.FiltrarPorFechaAsync(mes, anio);

            return lista.Select(c => new ContabilidadViewModel
            {
                IdContabilidad = c.IdContabilidad,
                Fecha = c.Fecha,
                Monto = c.Monto,
                Detalle = c.Detalle,
                Comentario = c.Comentario
            }).ToList();
        }

        #endregion
    }
}
