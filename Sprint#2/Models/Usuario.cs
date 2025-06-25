using System.ComponentModel.DataAnnotations;

namespace Sprint_2.Models;  
public class Usuario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Los apellidos son obligatorios.")]
    public string Apellidos { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
    public string Gmail { get; set; }

    [Required(ErrorMessage = "El username es obligatorio.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Contrasena { get; set; }

    public bool Estado { get; set; }

    public int RolId { get; set; }
    public Rol Rol { get; set; }
}
