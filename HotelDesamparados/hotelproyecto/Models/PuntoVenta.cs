using System.ComponentModel.DataAnnotations;

namespace hotelproyecto.Models
{
    public class PuntoVenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción de la venta es obligatoria.")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres.")]
        public string DescripcionVenta { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [RegularExpression(@"^(Efectivo|Tarjeta|Transferencia|Sinpe Movil)$",
            ErrorMessage = "El método de pago debe ser: Efectivo, Tarjeta, Transferencia o Cheque.")]
        public string Metodo_Pago { get; set; }

        [Required(ErrorMessage = "El descuento es obligatorio.")]
        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public decimal Descuento { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una reserva.")]
        public int ReservaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un empleado.")]
        public int EmpleadoId { get; set; }
    }
}
