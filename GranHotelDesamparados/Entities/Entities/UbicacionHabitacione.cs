using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class UbicacionHabitacione
{
    public int IdUbicacionHabitacion { get; set; }

    public string? NombreUbicacionHabitacion { get; set; }

    public string? DescripcionUbicacionHabitacion { get; set; }

    public virtual ICollection<Habitacione> Habitaciones { get; set; } = new List<Habitacione>();
}
