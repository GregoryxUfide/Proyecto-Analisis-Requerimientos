using System;
using System.ComponentModel.DataAnnotations;
using hotelproyecto.Validations;

namespace hotelproyecto.Models
{
    public class Reserva
    {
        [Required(ErrorMessage = "El IdReserva es obligatorio.")]
        public int IdReserva { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha final es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Final")]
        [DateGreaterThan("FechaInicio", ErrorMessage = "La fecha final debe ser mayor o igual a la fecha de inicio.")]
        public DateTime FechaFinal { get; set; }

        [Required(ErrorMessage = "El nombre del reservante es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del reservante no puede superar los 50 caracteres.")]
        [Display(Name = "Nombre Reservante")]
        public string NombreReservante { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Range(10000000, 999999999, ErrorMessage = "El teléfono debe ser un número válido.")]
        public int Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [StringLength(50, ErrorMessage = "El correo no puede superar los 50 caracteres.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El número de habitación es obligatorio.")]
        [Range(1, 1000, ErrorMessage = "El número de habitación debe estar entre 1 y 1000.")]
        [Display(Name = "Número de Habitación")]
        public int NumHabitacion { get; set; }
    }
}
