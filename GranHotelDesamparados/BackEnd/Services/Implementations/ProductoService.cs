using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class ProductoService : IProductoService
    {
        IUnidadDeTrabajo _Unidad;

        public ProductoService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _Unidad = unidadDeTrabajo;
        }

        ProductoDTO Convertir(Producto producto)
        {
            return new ProductoDTO
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                DescripcionProducto = producto.DescripcionProducto,
                IdUbicacionProducto = producto.IdUbicacionProducto,
                CantidadProducto = producto.CantidadProducto,
                CaducidadProducto = producto.CaducidadProducto,
                MarcaProducto = producto.MarcaProducto,
                EstadoProducto = producto.EstadoProducto
            };
        }

        Producto Convertir(ProductoDTO producto)
        {
            return new Producto
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                DescripcionProducto = producto.DescripcionProducto,
                IdUbicacionProducto = producto.IdUbicacionProducto,
                CantidadProducto = producto.CantidadProducto,
                CaducidadProducto = producto.CaducidadProducto,
                MarcaProducto = producto.MarcaProducto,
                EstadoProducto = producto.EstadoProducto
            };
        }



        public ProductoDTO Add(ProductoDTO producto)
        {
            try
            {
                _Unidad.ProductoDAL.Add(Convertir(producto));
                _Unidad.Complete();
                return producto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            Producto producto = new Producto { IdProducto = id };
            _Unidad.ProductoDAL.Remove(producto);
            _Unidad.Complete();
        }

        public ProductoDTO GetById(int id)
        {
            var producto = _Unidad.ProductoDAL.Get(id);
            return Convertir(producto);
        }


        public List<ProductoDTO> GetProductos()
        {
            var productos = _Unidad.ProductoDAL.GetAll();
            List<ProductoDTO> productoList = new List<ProductoDTO>();
            foreach (var producto in productos)
            {
                productoList.Add(Convertir(producto));
            }
            return productoList;
        }

        public ProductoDTO Update(ProductoDTO producto)
        {
            try
            {
                _Unidad.ProductoDAL.Update(Convertir(producto));
                _Unidad.Complete();
                return producto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
