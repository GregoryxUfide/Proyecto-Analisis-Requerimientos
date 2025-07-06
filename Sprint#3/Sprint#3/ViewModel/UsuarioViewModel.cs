using System.ComponentModel.DataAnnotations;
using Sprint_2.Models;

namespace Sprint2.ViewModel;
public class UsuarioViewModel
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
    public string? Contrasena { get; set; }

    public bool Estado { get; set; }

    public int RolId { get; set; }

    public List<Rol> Roles { get; set; } = new List<Rol>();
}
