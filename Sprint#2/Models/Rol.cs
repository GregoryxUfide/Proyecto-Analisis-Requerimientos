using System.ComponentModel.DataAnnotations;

namespace Sprint_2.Models
{
    public class Rol
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre del rol no puede superar los 50 caracteres")]
        public string Nombre { get; set; }

        // Relación con Usuarios
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
