using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotelproyecto.ViewModel
{
    public class EmpleadoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de empleado es obligatorio.")]
        [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Solo se permiten letras, números y guiones.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres permitidos.")]
        public string NumeroEmpleado { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio.")]
        [Range(0.01, 1000000.00, ErrorMessage = "El salario debe ser mayor que 0.")]
        public decimal SalarioEmpleado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un usuario.")]
        public int UsuarioId { get; set; }

        [Required]
        public bool Estado { get; set; }

        // Propiedades relacionadas para mostrar en vistas
        public string? UsuarioNombre { get; set; }
        public string? RolNombre { get; set; }

        // Para mostrar en el dropdown de usuarios
         public List<UsuarioViewModel> UsuariosLista{ get; set; } = new();
        public List<SelectListItem> UsuariosDisponibles { get; set; } = new();
    }
}
