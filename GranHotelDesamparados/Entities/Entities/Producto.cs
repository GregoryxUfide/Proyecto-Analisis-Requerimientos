using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public string? DescripcionProducto { get; set; }

    public int? IdUbicacionProducto { get; set; }

    public int? CantidadProducto { get; set; }

    public DateOnly? CaducidadProducto { get; set; }

    public string? MarcaProducto { get; set; }

    public bool EstadoProducto { get; set; }

    public virtual ICollection<Habitacione> Habitaciones { get; set; } = new List<Habitacione>();

    public virtual UbicacionProducto? IdUbicacionProductoNavigation { get; set; }
}
