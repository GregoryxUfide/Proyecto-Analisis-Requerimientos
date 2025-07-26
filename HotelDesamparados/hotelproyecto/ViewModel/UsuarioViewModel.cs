using System.ComponentModel.DataAnnotations;
using hotelproyecto.Models;

namespace hotelproyecto.ViewModel;
public class UsuarioViewModel
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
    
    public string? Contrasena { get; set; }

    public bool Estado { get; set; }

    public int RolId { get; set; }

    public List<RolViewModel> Roles { get; set; } = new List<RolViewModel>();
    
    public bool EsEdicionPropiaComoAdmin { get; set; }

}
