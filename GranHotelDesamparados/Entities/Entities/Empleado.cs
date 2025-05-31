using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string? PuestoEmpleado { get; set; }

    public int? TelefonoEmpleado { get; set; }

    public decimal? SalarioEmpleado { get; set; }

    public int? IdRol { get; set; }

    public virtual Usuario IdEmpleadoNavigation { get; set; } = null!;

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<LimpiezaHabitacione> LimpiezaHabitaciones { get; set; } = new List<LimpiezaHabitacione>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
