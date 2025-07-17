using System.ComponentModel.DataAnnotations;
using hotelproyecto.Models;

namespace hotelproyecto.ViewModel;
public class UsuarioFiltroViewModel
{
    public int? RolSeleccionadoId { get; set; }
    public bool? EstadoSeleccionado { get; set; }
    public string Busqueda { get; set; }
    public List<Rol> Roles { get; set; } = new();
    public List<UsuarioViewModel> Usuarios { get; set; } = new();
}

