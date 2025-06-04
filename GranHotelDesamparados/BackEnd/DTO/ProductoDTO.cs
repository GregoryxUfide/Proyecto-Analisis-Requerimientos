namespace BackEnd.DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        public string? NombreProducto { get; set; }

        public string? DescripcionProducto { get; set; }

        public int? IdUbicacionProducto { get; set; }

        public int? CantidadProducto { get; set; }

        public DateOnly? CaducidadProducto { get; set; }

        public string? MarcaProducto { get; set; }

        public bool EstadoProducto { get; set; }
    }
}
