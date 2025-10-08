using System.ComponentModel.DataAnnotations;

namespace CapaEntidad.EntidadBD
{
    public class EquipoUsuario
    {
        [Key]
        public int Id { get; set; }
        public int IdEquipo { get; set; }
        public int IdUsuario { get; set; }
    }
}
