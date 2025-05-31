using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class UbicacionProducto
{
    public int IdUbicacionProducto { get; set; }

    public string? NombreUbicacionProducto { get; set; }

    public string? DescripcionUbicacionProducto { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
