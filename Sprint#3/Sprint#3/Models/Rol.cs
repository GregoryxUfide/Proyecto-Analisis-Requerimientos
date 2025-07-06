using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sprint_2.Models
{
    public class Rol
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del rol no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-Z\s\-������������]{1,50}$", ErrorMessage = "El nombre del rol solo puede contener letras, espacios y guiones.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripci�n es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripci�n no puede superar los 500 caracteres.")]
        public string Descripcion { get; set; }
        
        public bool Estado { get; set; }

        // Relaci�n con Usuarios
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
