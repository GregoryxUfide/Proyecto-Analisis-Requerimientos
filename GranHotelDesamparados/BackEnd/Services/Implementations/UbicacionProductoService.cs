using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class UbicacionProductoService : IUbicacionProductoService
    {
        IUnidadDeTrabajo _Unidad;

        public UbicacionProductoService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _Unidad = unidadDeTrabajo;
        }

        UbicacionProductoDTO Convertir(UbicacionProducto ubicacionProducto)
        {
            return new UbicacionProductoDTO
            {
                IdUbicacionProducto = ubicacionProducto.IdUbicacionProducto,
                NombreUbicacionProducto = ubicacionProducto.NombreUbicacionProducto,
                DescripcionUbicacionProducto = ubicacionProducto.DescripcionUbicacionProducto
            };
        }

        UbicacionProducto Convertir(UbicacionProductoDTO ubicacionProducto)
        {
            return new UbicacionProducto
            {
                IdUbicacionProducto = ubicacionProducto.IdUbicacionProducto,
                NombreUbicacionProducto = ubicacionProducto.NombreUbicacionProducto,
                DescripcionUbicacionProducto = ubicacionProducto.DescripcionUbicacionProducto
            };
        }



        public UbicacionProductoDTO Add(UbicacionProductoDTO ubicacionProducto)
        {
            try
            {
                _Unidad.UbicacionProductoDAL.Add(Convertir(ubicacionProducto));
                _Unidad.Complete();
                return ubicacionProducto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            UbicacionProducto ubicacionProducto = new UbicacionProducto { IdUbicacionProducto = id };
            _Unidad.UbicacionProductoDAL.Remove(ubicacionProducto);
            _Unidad.Complete();
        }

        public UbicacionProductoDTO GetById(int id)
        {
            var ubicacionProducto = _Unidad.UbicacionProductoDAL.Get(id);
            return Convertir(ubicacionProducto);
        }


        public List<UbicacionProductoDTO> GetUbicacionProductos()
        {
            var ubicacionProductos = _Unidad.UbicacionProductoDAL.GetAll();
            List<UbicacionProductoDTO> UbicacionProductoList = new List<UbicacionProductoDTO>();
            foreach (var ubicacionProducto in ubicacionProductos)
            {
                UbicacionProductoList.Add(Convertir(ubicacionProducto));
            }
            return UbicacionProductoList;
        }

        public UbicacionProductoDTO Update(UbicacionProductoDTO ubicacionProducto)
        {
            try
            {
                _Unidad.UbicacionProductoDAL.Update(Convertir(ubicacionProducto));
                _Unidad.Complete();
                return ubicacionProducto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
