using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class ConexionBD
    {
        protected readonly string? _cadenaConexion;

        public ConexionBD()
        {
            // Carga manual del appsettings.json desde la raíz del proyecto principal
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // carpeta base del ejecutable
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _cadenaConexion = configuration.GetConnectionString("BD_Connection");
        }

    }
}
