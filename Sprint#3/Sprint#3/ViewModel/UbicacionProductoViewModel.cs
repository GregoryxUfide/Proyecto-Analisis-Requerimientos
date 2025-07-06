using System.ComponentModel.DataAnnotations;

namespace Sprint_2.ViewModel
{
    public class UbicacionProductoViewModel
    {
        public int IdUbicacionProducto { get; set; }

        [Required(ErrorMessage = "El nombre de la ubicación es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_,.áéíóúÁÉÍÓÚñÑ]{1,100}$", ErrorMessage = "El nombre solo puede contener letras, números, espacios y algunos caracteres como -_,.")]
        public string NombreUbicacionProducto { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(100, ErrorMessage = "La descripción no puede superar los 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_,.áéíóúÁÉÍÓÚñÑ]*$", ErrorMessage = "La descripción solo puede contener letras, números, espacios y algunos caracteres como -_,.")]
        public string DescripcionUbicacionProducto { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool Estado { get; set; }
    }
}
