using CapaEntidad.EntidadBD;
using CapaEntidad.EntidadForm;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestiónTareas.Pages
{
    public class IndexModel : PageModel
    {
        private CN_GestionTareas CapaNegocios = new CN_GestionTareas();
        public List<Usuario> ListaUsuarios = new List<Usuario>();
        public List<Tarea> ListaTareas = new List<Tarea>();
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public Tarea DatosTarea { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                ListaUsuarios = CapaNegocios.ListarUsuarioProyecto(2);

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public IActionResult OnPostGuardarTarea()
        {
            ERespuesta respuesta = new ERespuesta();

            try
            {
                if (!ModelState.IsValid)
                {
                    respuesta = new ERespuesta()
                    {
                        Respuesta = false,
                        Mensaje = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                    };
                } 
                else
                {
                    if (DatosTarea.IdTarea == 0)
                    {
                        respuesta = CapaNegocios.RegistrarTarea(DatosTarea);
                    }
                    else
                    {
                        respuesta = CapaNegocios.ActualizarTarea(DatosTarea);
                    }
                }

            } 
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString());
                respuesta = new ERespuesta()
                {
                    Respuesta = false,
                    Mensaje = "Error al " + (DatosTarea?.IdTarea == 0 ? " registrar " : " actualizar ") + "tarea: " + ex.Message.ToString()
                };
            }

            return new JsonResult(respuesta);
        }

        public IActionResult OnGetObtenerListaTareas()
        {
            var ListaTareas = new List<DetalleTarea>();
            try
            {
                ListaTareas = CapaNegocios.ListarTareas(2);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return new JsonResult(ListaTareas);
        }
    }
}
