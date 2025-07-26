using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hotelproyecto.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotelproyecto.ViewModel
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_,.áéíóúÁÉÍÓÚñÑ]{1,100}$", ErrorMessage = "El nombre solo puede contener letras, números, espacios y ciertos caracteres como -_,.")]
        public string NombreProducto { get; set; }

        [StringLength(100, ErrorMessage = "La descripción no puede superar los 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_,.áéíóúÁÉÍÓÚñÑ]*$", ErrorMessage = "La descripción solo puede contener letras, números, espacios y ciertos caracteres como -_,.")]
        public string DescripcionProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0.")]
        public int CantidadProducto { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CaducidadProducto { get; set; }

        [StringLength(100, ErrorMessage = "La marca no puede superar los 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_,.áéíóúÁÉÍÓÚñÑ]*$", ErrorMessage = "La marca solo puede contener letras, números, espacios y ciertos caracteres como -_,.")]
        public string MarcaProducto { get; set; }

        public bool EstadoProducto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una ubicación.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una ubicación válida.")]
        public int IdUbicacionProducto { get; set; }

        public string? NombreUbicacion { get; set; }

        // Para el combo en la vista (crear/editar)
        public List<SelectListItem>? Ubicaciones { get; set; }
    }
}

