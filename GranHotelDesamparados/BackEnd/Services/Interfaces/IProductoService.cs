using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IProductoService
    {
        List<ProductoDTO> GetProductos();
        ProductoDTO Add(ProductoDTO producto);
        ProductoDTO Update(ProductoDTO producto);
        void Delete(int id);
        ProductoDTO GetById(int id);
    }
}
