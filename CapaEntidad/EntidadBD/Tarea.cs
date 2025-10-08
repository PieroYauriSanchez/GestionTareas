using System.ComponentModel.DataAnnotations;

namespace CapaEntidad.EntidadBD
{
    public class Tarea
    {
        [Key]
        public int IdTarea { get; set; }
        [Required(ErrorMessage = "Por favor, ingrese el nombre de la tarea.")]
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        [Required(ErrorMessage = "Por favor, seleccione el usuario asignado a la tarea.")]
        public int? IdUsuarioAsignado { get; set; }
        public int Estado { get; set; }
    }
}
