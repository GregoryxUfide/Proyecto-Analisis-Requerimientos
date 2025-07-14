using System;
using System.ComponentModel.DataAnnotations;

namespace Sprint2.ViewModel
{
    public class LimpiezaHabitacionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Las tareas completadas son obligatorias.")]
        [StringLength(1000, ErrorMessage = "Las tareas no pueden superar los 1000 caracteres.")]
        [Display(Name = "Tareas Completadas")]
        public string TareasCompletadas { get; set; }

        [Required(ErrorMessage = "El nombre del conserje es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del conserje no puede superar los 100 caracteres.")]
        [Display(Name = "Nombre del Conserje")]
        public string NombreConserje { get; set; }

        [Display(Name = "Foto (opcional)")]
        public byte[]? Foto { get; set; }  // El signo ? indica que es nullable

        [Display(Name = "Subir foto")]
        [DataType(DataType.Upload)]
        public IFormFile? FotoArchivo { get; set; }

        [Display(Name = "Fecha y Hora de Registro")]
        public DateTime FechaHora { get; set; } = DateTime.Now;
    }
}
