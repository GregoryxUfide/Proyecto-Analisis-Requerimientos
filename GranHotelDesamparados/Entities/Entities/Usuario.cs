using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? CorreoUsuario { get; set; }

    public string? IdentificadorUsuario { get; set; }

    public string? ContrasenaUsuario { get; set; }

    public bool EstadoUsuario { get; set; }

    public virtual Empleado? Empleado { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();

    public virtual ICollection<Role> IdRols { get; set; } = new List<Role>();
}
