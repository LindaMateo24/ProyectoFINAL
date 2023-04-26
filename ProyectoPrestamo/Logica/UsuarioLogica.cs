using ProyectoPrestamo.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrestamo.Logica
{
    public class UsuarioLogica
    {
        private static UsuarioLogica _instancia = null;
        public UsuarioLogica()
        {

        }
        public static UsuarioLogica Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new UsuarioLogica();
                return _instancia;
            }
        }

        public Usuario Obtener(string usuario, string password)
        {
            Usuario obj = new Usuario();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = $"select top 1 * from USUARIO Where NombreUsuario = '{usuario}' and Clave = '{password}' ";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString()),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Tipo = dr["Tipo"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Ciudad = dr["Ciudad"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                NumeroTelefono = dr["NumeroTelefono"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj = new Usuario();
            }
            return obj;
        }


        public List<Usuario> Listar()
        {
            List<Usuario> oLista = new List<Usuario>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select * from USUARIO");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString()),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Tipo = dr["Tipo"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Ciudad = dr["Ciudad"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                NumeroTelefono = dr["NumeroTelefono"].ToString()

                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<Usuario>();
            }
            return oLista;
        }


        public int Guardar(Usuario objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {

                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("update USUARIO set NombreUsuario = @pnombre where IdUsuario = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@pnombre", objeto.NombreUsuario));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                        mensaje = "No se pudo actualizar el nombre de usuario";

                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }

        public int cambiarClave(string nuevaclave, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("update USUARIO set Clave = @pclave where IdUsuario = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@pclave", PasswordEncryption.EncryptPassword(nuevaclave)));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                        mensaje = "No se pudo cambiar la contraseña";
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }

        public int resetear()
        {
            int respuesta = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update USUARIO set NombreUsuario = 'Admin', Clave = '123' where IdUsuario = 1;");
                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
            }

            return respuesta;
        }


        public int Registrar(Usuario oCliente, out string mensaje, out string password)
        {

            mensaje = string.Empty;
            int respuesta = 0;
            SqlTransaction objTransaccion = null;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                password = GeneratePassword(8);
                try
                {
                    conexion.Open();
                    objTransaccion = conexion.BeginTransaction();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine(string.Format("INSERT INTO USUARIO(NombreCompleto,TipoDocumento,NumeroDocumento,Direccion,Ciudad,Correo,NumeroTelefono,NombreUsuario,Tipo,Clave) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');",
                        oCliente.NombreCompleto,
                        oCliente.TipoDocumento,
                        oCliente.NumeroDocumento,
                        oCliente.Direccion,
                        oCliente.Ciudad,
                        oCliente.Correo,
                        oCliente.NumeroTelefono,
                        oCliente.NombreUsuario,
                        oCliente.Tipo,
                        PasswordEncryption.EncryptPassword(password)));
                    query.AppendLine("select SCOPE_IDENTITY();");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Transaction = objTransaccion;
                    respuesta = Convert.ToInt32(cmd.ExecuteScalar());


                    if (respuesta < 1)
                    {
                        objTransaccion.Rollback();
                        mensaje = "No se pudo registrar el cliente";
                    }

                    objTransaccion.Commit();

                }
                catch (Exception ex)
                {
                    objTransaccion.Rollback();
                    respuesta = 0;
                    mensaje = ex.Message;
                }
            }


            return respuesta;
        }

        public int EditarUsuario(Usuario oCliente, out string mensaje)
        {

            mensaje = string.Empty;
            int respuesta = 0;
            SqlTransaction objTransaccion = null;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    objTransaccion = conexion.BeginTransaction();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine(string.Format("UPDATE USUARIO SET NombreCompleto = '{0}',TipoDocumento = '{1}',NumeroDocumento = '{2}',Direccion = '{3}',Ciudad = '{4}',Correo = '{5}'" +
                        ",NumeroTelefono = '{6}',NombreUsuario = '{7}',Tipo = '{8}' WHERE IdUsuario = {9};",
                        oCliente.NombreCompleto,
                        oCliente.TipoDocumento,
                        oCliente.NumeroDocumento,
                        oCliente.Direccion,
                        oCliente.Ciudad,
                        oCliente.Correo,
                        oCliente.NumeroTelefono,
                        oCliente.NombreUsuario,
                        oCliente.Tipo,
                        oCliente.IdUsuario
                        ));

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Transaction = objTransaccion;
                    respuesta = cmd.ExecuteNonQuery();


                    if (respuesta < 1)
                    {
                        objTransaccion.Rollback();
                        mensaje = "No se pudo editar";
                    }

                    objTransaccion.Commit();

                }
                catch (Exception ex)
                {
                    objTransaccion.Rollback();
                    respuesta = 0;
                    mensaje = ex.Message;
                }
            }


            return respuesta;
        }


        public int Eliminar(int id)
        {
            int respuesta = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("delete from CLIENTES where IdCliente= @id;");
                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    respuesta = 0;
                }

            }


            return respuesta;
        }

        string GeneratePassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789@!*-_";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
