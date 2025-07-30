using System.ComponentModel.DataAnnotations;

namespace hotelproyecto.ViewModel
{
    public class ContabilidadViewModel
    {
        public int IdContabilidad { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de trámite")]
        public DateTime? Fecha { get; set; }



        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser un valor positivo.")]
        public decimal Monto { get; set; }



        [Required(ErrorMessage = "El detalle es obligatorio.")]
        public string Detalle { get; set; }



        [Required(ErrorMessage = "El comentario es obligatorio.")]
        public string Comentario { get; set; }
    }
}
