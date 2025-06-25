using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IUbicacionProductoHelper
    {
        List<UbicacionProductoViewModel> GetAll();
        UbicacionProductoViewModel GetById(int id);
        UbicacionProductoViewModel AddUbicacionProducto(UbicacionProductoViewModel UbicacionProductoViewModel);
        UbicacionProductoViewModel EditUbicacionProducto(UbicacionProductoViewModel UbicacionProductoViewModel);
        void DeleteUbicacionProducto(int id);
    }
}
