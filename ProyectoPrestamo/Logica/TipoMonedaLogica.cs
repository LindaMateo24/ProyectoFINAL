﻿using ProyectoPrestamo.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrestamo.Logica
{
    public class TipoMonedaLogica
    {

        private static TipoMonedaLogica _instancia = null;

        public TipoMonedaLogica()
        {

        }

        public static TipoMonedaLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new TipoMonedaLogica();
                return _instancia;
            }
        }


        public List<TipoMoneda> Listar(out string mensaje)
        {
            mensaje = string.Empty;
            List<TipoMoneda> oLista = new List<TipoMoneda>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    string query = "select IdTipoMoneda,Divisa,Abreviatura from TIPO_MONEDA;";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new TipoMoneda()
                            {
                                IdTipoMoneda = int.Parse(dr["IdTipoMoneda"].ToString()),
                                Divisa = dr["Divisa"].ToString(),
                                Abreviatura = dr["Abreviatura"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<TipoMoneda>();
                mensaje = ex.Message;
            }


            return oLista;
        }


        public int Existe(string Divisa, int defaultid, out string mensaje)
        {

            mensaje = string.Empty;
            int respuesta = 0;
            try
            {

                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select count(*)[resultado] from TIPO_MONEDA where upper(Divisa) = upper(@pdiv) and IdTipoMoneda != @defaultid");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@pdiv", Divisa));
                    cmd.Parameters.Add(new SqlParameter("@defaultid", defaultid));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta > 0)
                        mensaje = "Ya existe el tipo de moneda";

                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }

        public int Guardar(TipoMoneda objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {

                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("insert into TIPO_MONEDA(Divisa,Abreviatura) values (@pdiv,@pabre);");
                    query.AppendLine("select SCOPE_IDENTITY();");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@pdiv", objeto.Divisa));
                    cmd.Parameters.Add(new SqlParameter("@pabre", objeto.Abreviatura));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta < 1)
                        mensaje = "No se pudo registrar el tipo de moneda";

                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }


        public int Editar(TipoMoneda objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update TIPO_MONEDA set Divisa = @pdiv, Abreviatura = @pabre where IdTipoMoneda= @id;");
                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@id", objeto.IdTipoMoneda));
                    cmd.Parameters.Add(new SqlParameter("@pdiv", objeto.Divisa));
                    cmd.Parameters.Add(new SqlParameter("@pabre", objeto.Abreviatura));
                    cmd.CommandType = System.Data.CommandType.Text;
                    respuesta = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }


        public int Validar(int id, out string mensaje)
        {

            mensaje = string.Empty;
            int respuesta = 0;
            try
            {

                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) from PRESTAMO a");
                    query.AppendLine("inner join TIPO_MONEDA tm on tm.IdTipoMoneda = a.IdTipoMoneda");
                    query.AppendLine("where tm.IdTipoMoneda = @id");
                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta > 0)
                        mensaje = "No se puede eliminar el tipo de moneda.\nEl tipo de moneda ya se encuentra asignado a un PRESTAMO.";

                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }


        public int Eliminar(int id)
        {
            int respuesta = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("delete from TIPO_MONEDA where IdTipoMoneda= @id;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@id", id));
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

    }
}
