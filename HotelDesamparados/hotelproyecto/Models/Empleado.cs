using System.ComponentModel.DataAnnotations;

namespace hotelproyecto.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de empleado es obligatorio.")]
        [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "El número de empleado solo puede contener letras, números y guiones.")]
        [StringLength(50, ErrorMessage = "El número de empleado no puede tener más de 50 caracteres.")]
        public string NumeroEmpleado { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio.")]
        [Range(0.01, 1000000.00, ErrorMessage = "El salario debe ser mayor que 0.")]
        public decimal SalarioEmpleado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un usuario.")]
        public int UsuarioId { get; set; }

        [Required]
        public bool Estado { get; set; }

        // Relaciones
        public Usuario? Usuario { get; set; }
    }
}
