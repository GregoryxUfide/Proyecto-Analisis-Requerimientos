using System;
using System.ComponentModel.DataAnnotations;

namespace Sprint_2.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string NombreProducto { get; set; }

        [StringLength(100, ErrorMessage = "La descripción no puede superar los 100 caracteres.")]
        public string DescripcionProducto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una ubicación.")]
        public int IdUbicacionProducto { get; set; }

        public UbicacionProducto UbicacionProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0.")]
        public int CantidadProducto { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de caducidad")]
        public DateTime? CaducidadProducto { get; set; }

        [StringLength(100, ErrorMessage = "La marca no puede superar los 100 caracteres.")]
        public string MarcaProducto { get; set; }

        public bool EstadoProducto { get; set; }
    }
}
