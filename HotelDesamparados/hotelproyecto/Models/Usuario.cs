using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace hotelproyecto.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "El nombre solo puede contener letras y espacios, entre 2 y 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "Los apellidos solo pueden contener letras y espacios, entre 2 y 50 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
        public string Gmail { get; set; }

        [Required(ErrorMessage = "El username es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9]{4,20}$", ErrorMessage = "El username debe contener solo letras o números y tener entre 4 y 20 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [RegularExpression(@"^(?=.*[!@#$%^&*(),.?""{}|<>])[A-Za-z\d!@#$%^&*(),.?""{}|<>]{6,}$", ErrorMessage = "La contraseña debe tener al menos 6 caracteres y al menos un carácter especial.")]
        public string Contrasena { get; set; }

        public bool Estado { get; set; }

        public int RolId { get; set; }
        [BindNever]
        public Rol Rol { get; set; }

        [NotMapped] 
        public string RolNombre { get; set; } 
    }
}
