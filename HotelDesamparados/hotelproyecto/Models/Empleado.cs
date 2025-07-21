using System.ComponentModel.DataAnnotations;
using hotelproyecto.Models;

namespace hotelproyecto.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de empleado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de empleado no puede superar los 50 caracteres.")]
        public string NumeroEmpleado { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio.")]
        [Range(0.01, 1000000, ErrorMessage = "El salario debe ser un número positivo.")]
        public decimal SalarioEmpleado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un usuario.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public int RolId { get; set; }

        public bool Estado { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Rol Rol { get; set; }
    }
}
