using hotelproyecto;
using System.ComponentModel.DataAnnotations;

namespace hotelproyecto.ViewModel
{
    public class EmpleadoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de empleado es obligatorio.")]
        public string NumeroEmpleado { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser un valor positivo.")]
        public decimal SalarioEmpleado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un usuario.")]
        public int UsuarioId { get; set; }

        public string? UsuarioUsername { get; set; } // Solo para vista Index

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public int RolId { get; set; }

        public string? RolNombre { get; set; } // Solo para vista Index

        public bool Estado { get; set; }

        // Inicializar para evitar nulls
        public List<RolViewModel> RolesDisponibles { get; set; } = new List<RolViewModel>();
        public List<UsuarioViewModel> UsuariosDisponibles { get; set; } = new List<UsuarioViewModel>();
    }
}
