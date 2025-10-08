using CapaDatos;
using CapaEntidad.EntidadBD;
using CapaEntidad.EntidadForm;

namespace CapaNegocio
{
    public class CN_GestionTareas
    {
        CD_GestionTareas CapaDatos = new CD_GestionTareas();

        public List<Usuario> ListarUsuarioProyecto(int idProyecto)
        {
            try
            {
                return CapaDatos.ListarUsuarioProyecto(idProyecto);

            } catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString());
                return new List<Usuario>();
            }
        }

        public ERespuesta RegistrarTarea(Tarea datosTarea)
        {
            try
            {
                return CapaDatos.RegistrarTarea(datosTarea);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString());
                return new ERespuesta()
                {
                    Respuesta = false, Mensaje = "Error: " + ex.Message.ToString()
                };
            }
        }

        public ERespuesta ActualizarTarea(Tarea datosTarea)
        {
            try
            {
                return CapaDatos.ActualizarTarea(datosTarea);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString());
                return new ERespuesta()
                {
                    Respuesta = false,
                    Mensaje = "Error: " + ex.Message.ToString()
                };
            }
        }

        public List<DetalleTarea> ListarTareas(int idProyecto)
        {
            try
            {
                return CapaDatos.ListarTareas(idProyecto);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString());
                return new List<DetalleTarea>();
            }
        }
    }
}
