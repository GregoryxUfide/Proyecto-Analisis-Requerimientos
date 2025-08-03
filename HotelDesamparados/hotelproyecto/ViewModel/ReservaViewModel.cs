using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace hotelproyecto.ViewModel
{
    public class ReservaViewModel
    {
        public int IdReserva { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "Debe ingresar una fecha válida.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha final es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "Debe ingresar una fecha válida.")]
        public DateTime FechaFinal { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÑa-záéíóúñ\s]+$", ErrorMessage = "El nombre solo debe contener letras y espacios.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El teléfono debe contener exactamente 8 dígitos.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder los 100 caracteres.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una habitación.")]
        public int HabitacionId { get; set; }

        [Required(ErrorMessage = "El estado de la reserva es obligatorio.")]
        public bool Estado { get; set; } // true = Activa, false = Finalizada

        [BindNever]
        public IEnumerable<SelectListItem>? Habitaciones { get; set; } = Enumerable.Empty<SelectListItem>();

        [BindNever]
        public string? HabitacionNombre { get; set; }

        [BindNever]
        public string? EstadoTexto => Estado ? "Reserva Activa" : "Finalizó Reserva";
    }
}
