using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaSalida { get; set; }

    public int? IdUsuario { get; set; }

    public bool EstadoReserva { get; set; }

    public int? IdHabitacion { get; set; }

    public virtual Habitacione? IdHabitacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
