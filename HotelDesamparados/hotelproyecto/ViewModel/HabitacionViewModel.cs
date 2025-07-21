using System.ComponentModel.DataAnnotations;

namespace hotelproyecto.ViewModel
{
    public class HabitacionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La capacidad es obligatoria.")]
        [Range(1, 10, ErrorMessage = "La capacidad debe estar entre 1 y 10 personas.")]
        public int Capacidad { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El número de habitación es obligatorio.")]
        [Range(1, 1000, ErrorMessage = "El número debe estar entre 1 y 1000.")]
        [Display(Name = "Número de Habitación")]
        public int NumHabitacion { get; set; }

        [Required(ErrorMessage = "El número de camas es obligatorio.")]
        [Range(1, 10, ErrorMessage = "El número de camas debe estar entre 1 y 10.")]
        [Display(Name = "Número de Camas")]
        public int NumCamas { get; set; }

        [StringLength(500, ErrorMessage = "Los extras no pueden superar los 500 caracteres.")]
        public string Extras { get; set; }

        [StringLength(500, ErrorMessage = "Los comentarios no pueden superar los 500 caracteres.")]
        public string Comentarios { get; set; }


        }
}
