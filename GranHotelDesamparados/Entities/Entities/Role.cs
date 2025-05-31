using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Role
{
    public int IdRol { get; set; }

    public string? NombreRol { get; set; }

    public string? DescripcionRol { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Usuario> IdUsuarios { get; set; } = new List<Usuario>();
}
