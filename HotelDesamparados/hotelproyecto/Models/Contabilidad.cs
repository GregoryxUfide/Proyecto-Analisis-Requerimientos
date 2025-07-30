using System.ComponentModel.DataAnnotations;

namespace hotelproyecto.Models
{
    public class Contabilidad
    {
        public int IdContabilidad { get; set; }



        [DataType(DataType.Date)]
        [Display(Name = "Fecha de tramite")]
        public DateTime? Fecha { get; set; }



        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser un valor positivo.")]
        public decimal Monto { get; set; }



        [Required(ErrorMessage = "El detalle es obligatorio.")]
        [StringLength(int.MaxValue)]
        public string Detalle { get; set; }



        [Required(ErrorMessage = "El comentario es obligatorio.")]
        [StringLength(int.MaxValue)]
        public string Comentario { get; set; }

    }
}
