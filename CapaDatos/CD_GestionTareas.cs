using CapaEntidad.EntidadBD;
using CapaEntidad.EntidadForm;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_GestionTareas : ConexionBD
    {
        
        public List<Usuario> ListarUsuarioProyecto(int idProyecto)
        {
            try
            {
                var lista = new List<Usuario>();
                using (SqlConnection cn = new SqlConnection(_cadenaConexion))
                {
                    string sql = @"SELECT u.IdUsuario, u.Nombres, u.Apellidos
                                     FROM Usuarios u 
                                     JOIN EquiposUsuarios e ON e.IdUsuario = u.IdUsuario
                                    WHERE e.IdEquipo = @IdEquipo
                                      AND u.Estado   = 1";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@IdEquipo", idProyecto);
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = dr.GetInt32(0),
                            Nombres = dr.GetString(1),
                            Apellidos = dr.GetString(2)
                        });
                    }
                }
                return lista;
            } 
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Usuarios: " + ex.Message.ToString());
                return new List<Usuario>();
            }   
        }

        public ERespuesta RegistrarTarea(Tarea datosTarea)
        {
            ERespuesta respuesta = new ERespuesta();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaConexion))
                {
                    string sql = @"INSERT INTO Tareas 
                                    (Titulo, Descripcion, IdUsuarioAsignado, Estado, FechaVencimiento)
                                   VALUES (@Titulo, @Descripcion, @IdUsuarioAsignado, 1, @FechaVencimiento)";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@Titulo", datosTarea.Titulo);
                    cmd.Parameters.AddWithValue("@Descripcion", datosTarea.Descripcion ?? "");
                    cmd.Parameters.AddWithValue("@IdUsuarioAsignado", datosTarea.IdUsuarioAsignado == null ? DBNull.Value : datosTarea.IdUsuarioAsignado);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", datosTarea.FechaVencimiento == null ? DBNull.Value : datosTarea.FechaVencimiento);
                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    respuesta = new ERespuesta()
                    {
                        Respuesta = (filasAfectadas > 0),
                        Mensaje = (filasAfectadas > 0) ? "Tarea registrada correctamente." : "No se pudo registrar la tarea. Por favor, contacte a TI."
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar tarea: " + ex.Message.ToString());
                respuesta = new ERespuesta()
                {
                    Respuesta = false,
                    Mensaje = "Ocurrió un error al registrar la tarea: " + ex.Message.ToString()
                };
            }
            return respuesta;
        }

        public ERespuesta ActualizarTarea(Tarea datosTarea)
        {
            ERespuesta respuesta = new ERespuesta();
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaConexion))
                {
                    string sql = @"UPDATE Tareas 
                                      SET Titulo = @Titulo
                                         ,Descripcion = @Descripcion
                                         ,IdUsuarioAsignado = @IdUsuarioAsignado
                                         ,FechaVencimiento = @FechaVencimiento
                                         ,Estado = @Estado
                                    WHERE IdTarea = @IdTarea";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@Titulo", datosTarea.Titulo);
                    cmd.Parameters.AddWithValue("@Descripcion", datosTarea.Descripcion??"");
                    cmd.Parameters.AddWithValue("@IdUsuarioAsignado", datosTarea.IdUsuarioAsignado == null ? DBNull.Value : datosTarea.IdUsuarioAsignado);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", datosTarea.FechaVencimiento == null ? DBNull.Value : datosTarea.FechaVencimiento);
                    cmd.Parameters.AddWithValue("@Estado", datosTarea.Estado);
                    cmd.Parameters.AddWithValue("@IdTarea", datosTarea.IdTarea);
                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    respuesta = new ERespuesta()
                    {
                        Respuesta = (filasAfectadas > 0),
                        Mensaje = (filasAfectadas > 0) ? "Tarea actualizada correctamente." : "No se pudo actualizar la tarea. Por favor, contacte a TI."
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar tarea: " + ex.Message.ToString());
                respuesta = new ERespuesta()
                {
                    Respuesta = false,
                    Mensaje = "Ocurrió un error al actualizar la tarea: " + ex.Message.ToString()
                };
            }
            return respuesta;
        }

        public List<DetalleTarea> ListarTareas(int idProyecto)
        {
            try
            {
                var lista = new List<DetalleTarea>();
                using (SqlConnection cn = new SqlConnection(_cadenaConexion))
                {
                    string sql = @"SELECT t.IdTarea, t.Titulo, t.Descripcion, t.FechaVencimiento, 
                                          t.Estado, t.IdUsuarioAsignado, 
                                          CONCAT (u.Nombres, ' ', u.Apellidos) as NombreUsuarioAsignado
                                     FROM Tareas t
                                     JOIN Usuarios u ON u.IdUsuario = t.IdUsuarioAsignado
                                     JOIN EquiposUsuarios e ON e.IdUsuario = u.IdUsuario
                                    WHERE e.IdEquipo = @IdEquipo
                                      AND t.Estado  != 0";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@IdEquipo", idProyecto);
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lista.Add(new DetalleTarea
                        {
                            IdTarea = dr.GetInt32(0),
                            Titulo = dr.GetString(1),
                            Descripcion = dr.IsDBNull(2) ? "" : dr.GetString(2),
                            FechaVencimiento = dr.IsDBNull(3) ? DateTime.MinValue : dr.GetDateTime(3),
                            Estado = dr.GetInt32(4),
                            IdUsuarioAsignado = dr.GetInt32(5),
                            NombreUsuarioAsignado = dr.GetString(6)
                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar Usuarios: " + ex.Message.ToString());
                return new List<DetalleTarea>();
            }
        }

    }
}
