using System.ComponentModel.DataAnnotations;

namespace hotelproyecto.ViewModel
{
    public class RolViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del rol no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-Z\s\-áéíóúÁÉÍÓÚñÑ]{1,50}$", ErrorMessage = "El nombre del rol solo puede contener letras, espacios y guiones.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        public string Descripcion { get; set; }

        public bool Estado { get; set; } = true;  
    }
}
