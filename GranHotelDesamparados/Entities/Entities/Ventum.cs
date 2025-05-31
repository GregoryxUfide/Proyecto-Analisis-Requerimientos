using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public string? DescripcionVenta { get; set; }

    public DateTime? FechaVenta { get; set; }

    public int? IdUsuario { get; set; }

    public decimal? MontoVenta { get; set; }

    public decimal? MontoDescuento { get; set; }

    public string? MetodoPagoVenta { get; set; }

    public int? IdReserva { get; set; }

    public int? IdEmpleado { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Reserva? IdReservaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
