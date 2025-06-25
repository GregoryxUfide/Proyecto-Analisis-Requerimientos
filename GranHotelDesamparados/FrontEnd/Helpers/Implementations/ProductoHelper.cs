using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class ProductoHelper : IProductoHelper
    {
        IServiceRepository _serviceRepository;


        public ProductoHelper(IServiceRepository serviceRepository)
        {
            this._serviceRepository = serviceRepository;

        }

        ProductoViewModel Convertir(ProductoAPI producto)
        {
            return new ProductoViewModel
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                DescripcionProducto = producto.DescripcionProducto,
                IdUbicacionProducto = (int) producto.IdUbicacionProducto,
                CantidadProducto = producto.CantidadProducto,
                CaducidadProducto = producto.CaducidadProducto,
                MarcaProducto = producto.MarcaProducto,
                EstadoProducto = producto.EstadoProducto

            };
        }

        ProductoAPI Convertir(ProductoViewModel producto)
        {
            return new ProductoAPI
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

        public ProductoViewModel AddProducto(ProductoViewModel ProductoViewModel)
        {
            HttpResponseMessage response = _serviceRepository.PostResponse("api/Producto", Convertir(ProductoViewModel));
            if (response != null)
            {
                var content = response.Content;
            }


            return ProductoViewModel;
        }

        public void DeleteProducto(int id)
        {
            HttpResponseMessage response = _serviceRepository.DeleteResponse("api/Producto/" + id.ToString());
            if (response != null)
            {
                var content = response.Content;
            }
        }


        public ProductoViewModel EditProducto(ProductoViewModel ProductoViewModel)
        {
            HttpResponseMessage response = _serviceRepository.PutResponse("api/Producto", Convertir(ProductoViewModel));
            if (response != null)
            {
                var content = response.Content;
            }
            return ProductoViewModel;
        }

        public List<ProductoViewModel> GetAll()
        {
            List<ProductoAPI> Productos = new List<ProductoAPI>();
            HttpResponseMessage responseMessage = _serviceRepository.GetResponse("api/Producto");

            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                Productos = JsonConvert.DeserializeObject<List<ProductoAPI>>(content);
            }
            List<ProductoViewModel> lista = new List<ProductoViewModel>();
            foreach (var item in Productos)
            {
                lista.Add(Convertir(item));
            }
            return lista;
        }

        public ProductoViewModel GetById(int id)
        {
            ProductoAPI Producto = new ProductoAPI();
            HttpResponseMessage responseMessage = _serviceRepository.GetResponse("api/Producto/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                Producto = JsonConvert.DeserializeObject<ProductoAPI>(content);
            }
            return Convertir(Producto);
        }
    }
}
