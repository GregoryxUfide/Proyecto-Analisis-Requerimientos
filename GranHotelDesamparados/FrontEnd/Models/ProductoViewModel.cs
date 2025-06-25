using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }

        public string? NombreProducto { get; set; }

        public string? DescripcionProducto { get; set; }

        public int IdUbicacionProducto { get; set; }

        public IEnumerable<UbicacionProductoViewModel> UbicacionProductos { get; set; }
        [Display(Name = "Nombre de Ubicacion")]
        public string NombreUbicacionProducto { get; set; }

        public int? CantidadProducto { get; set; }

        public DateOnly? CaducidadProducto { get; set; }

        public string? MarcaProducto { get; set; }

        public bool EstadoProducto { get; set; }

        //Agrego UbicacionProducto para que se pueda mostrar en la vista
    }
}
