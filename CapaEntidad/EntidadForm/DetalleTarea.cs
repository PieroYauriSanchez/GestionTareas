
namespace CapaEntidad.EntidadForm
{
    public class DetalleTarea
    {
        public int IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Estado { get; set; }
        public int IdUsuarioAsignado { get; set; }
        public string NombreUsuarioAsignado { get; set; }
    }
}
