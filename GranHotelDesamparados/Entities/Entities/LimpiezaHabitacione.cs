using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class LimpiezaHabitacione
{
    public int IdLimpiezaHabitacion { get; set; }

    public int? IdHabitacion { get; set; }

    public DateTime? FechaLimpiezaHabitacion { get; set; }

    public bool LimpiezaBano { get; set; }

    public bool LimpiezaMuebles { get; set; }

    public bool LimpiezaCuarto { get; set; }

    public int? IdEmpleado { get; set; }

    public string? ComentariosLimpiezaHabitacion { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Habitacione? IdHabitacionNavigation { get; set; }
}
