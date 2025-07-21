using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotelproyecto.ViewModel
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100)]
        public string NombreProducto { get; set; }

        public string DescripcionProducto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una ubicación.")]
        public int IdUbicacionProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(0, int.MaxValue)]
        public int CantidadProducto { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CaducidadProducto { get; set; }

        public string MarcaProducto { get; set; }

        public bool EstadoProducto { get; set; }

        // Para cargar las ubicaciones en un dropdown
        public List<SelectListItem> Ubicaciones { get; set; } = new List<SelectListItem>();
    }
}
