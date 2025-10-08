using System.ComponentModel.DataAnnotations;

namespace CapaEntidad.EntidadBD
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Estado { get; set; }

    }
}
