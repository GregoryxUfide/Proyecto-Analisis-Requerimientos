using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class UbicacionProductoHelper : IUbicacionProductoHelper
    {
        IServiceRepository _serviceRepository;


        public UbicacionProductoHelper(IServiceRepository serviceRepository)
        {
            this._serviceRepository = serviceRepository;

        }

        UbicacionProductoViewModel Convertir(UbicacionProductoAPI ubicacionProducto)
        {
            return new UbicacionProductoViewModel
            {
                IdUbicacionProducto = ubicacionProducto.IdUbicacionProducto,
                NombreUbicacionProducto = ubicacionProducto.NombreUbicacionProducto,
                DescripcionUbicacionProducto = ubicacionProducto.DescripcionUbicacionProducto

            };
        }

        UbicacionProductoAPI Convertir(UbicacionProductoViewModel ubicacionProducto)
        {
            return new UbicacionProductoAPI
            {
                IdUbicacionProducto = ubicacionProducto.IdUbicacionProducto,
                NombreUbicacionProducto = ubicacionProducto.NombreUbicacionProducto,
                DescripcionUbicacionProducto = ubicacionProducto.DescripcionUbicacionProducto
            };
        }

        public UbicacionProductoViewModel AddUbicacionProducto(UbicacionProductoViewModel UbicacionProductoViewModel)
        {
            HttpResponseMessage response = _serviceRepository.PostResponse("api/UbicacionProducto", Convertir(UbicacionProductoViewModel));
            if (response != null)
            {
                var content = response.Content;
            }


            return UbicacionProductoViewModel;
        }

        public void DeleteUbicacionProducto(int id)
        {
            HttpResponseMessage response = _serviceRepository.DeleteResponse("api/UbicacionProducto/" + id.ToString());
            if (response != null)
            {
                var content = response.Content;
            }
        }


        public UbicacionProductoViewModel EditUbicacionProducto(UbicacionProductoViewModel UbicacionProductoViewModel)
        {
            HttpResponseMessage response = _serviceRepository.PutResponse("api/UbicacionProducto", Convertir(UbicacionProductoViewModel));
            if (response != null)
            {
                var content = response.Content;
            }
            return UbicacionProductoViewModel;
        }

        public List<UbicacionProductoViewModel> GetAll()
        {
            List<UbicacionProductoAPI> UbicacionProductos = new List<UbicacionProductoAPI>();
            HttpResponseMessage responseMessage = _serviceRepository.GetResponse("api/UbicacionProducto");

            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                UbicacionProductos = JsonConvert.DeserializeObject<List<UbicacionProductoAPI>>(content);
            }
            List<UbicacionProductoViewModel> lista = new List<UbicacionProductoViewModel>();
            foreach (var item in UbicacionProductos)
            {
                lista.Add(Convertir(item));
            }
            return lista;
        }

        public UbicacionProductoViewModel GetById(int id)
        {
            UbicacionProductoAPI UbicacionProducto = new UbicacionProductoAPI();
            HttpResponseMessage responseMessage = _serviceRepository.GetResponse("api/UbicacionProducto/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                UbicacionProducto = JsonConvert.DeserializeObject<UbicacionProductoAPI>(content);
            }
            return Convertir(UbicacionProducto);
        }
    }
}
