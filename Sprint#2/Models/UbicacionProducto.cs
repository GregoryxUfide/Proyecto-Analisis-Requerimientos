using System.ComponentModel.DataAnnotations;

namespace Sprint_2.Models
{
    public class UbicacionProducto
    {
        public int IdUbicacionProducto { get; set; }

        [Required(ErrorMessage = "El nombre de la ubicaci�n es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string NombreUbicacionProducto { get; set; }

        [StringLength(100, ErrorMessage = "La descripci�n no puede superar los 100 caracteres.")]
        public string DescripcionUbicacionProducto { get; set; }        
    }
}
