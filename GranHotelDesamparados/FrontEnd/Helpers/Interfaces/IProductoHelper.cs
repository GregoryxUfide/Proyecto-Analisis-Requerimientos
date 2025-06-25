using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IProductoHelper
    {
        List<ProductoViewModel> GetAll();
        ProductoViewModel GetById(int id);
        ProductoViewModel AddProducto(ProductoViewModel ProductoViewModel);
        ProductoViewModel EditProducto(ProductoViewModel ProductoViewModel);
        void DeleteProducto(int id);
    }
}
