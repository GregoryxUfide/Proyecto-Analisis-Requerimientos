using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IUbicacionProductoService
    {
        List<UbicacionProductoDTO> GetUbicacionProductos();
        UbicacionProductoDTO Add(UbicacionProductoDTO ubicacionProducto);
        UbicacionProductoDTO Update(UbicacionProductoDTO ubicacionProducto);
        void Delete(int id);
        UbicacionProductoDTO GetById(int id);
    }
}
