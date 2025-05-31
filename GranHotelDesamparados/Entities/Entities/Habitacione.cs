using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Habitacione
{
    public int IdHabitacion { get; set; }

    public int? NumeroHabitacion { get; set; }

    public string? NombreHabitacion { get; set; }

    public string? DescripcionHabitacion { get; set; }

    public int? CantidadPersonasHabitacion { get; set; }

    public decimal? PrecioHabitacion { get; set; }

    public int? IdUbicacionHabitacion { get; set; }

    public int? CantidadCamasHabitacion { get; set; }

    public int? IdProducto { get; set; }

    public bool EstadoHabitacion { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual UbicacionHabitacione? IdUbicacionHabitacionNavigation { get; set; }

    public virtual ICollection<LimpiezaHabitacione> LimpiezaHabitaciones { get; set; } = new List<LimpiezaHabitacione>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
