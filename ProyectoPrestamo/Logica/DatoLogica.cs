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
    public class DatoLogica
    {
        private static DatoLogica _instancia = null;

        public DatoLogica()
        {

        }

        public static DatoLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new DatoLogica();
                return _instancia;
            }
        }

        public Datos Obtener()
        {
            Datos obj = new Datos();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select top 1 IdDato, RazonSocial, RUC, Representante, Correo, Telefono, Ciudad from DATOS ";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Datos()
                            {
                                IdDato = int.Parse(dr["IdDato"].ToString()),
                                RazonSocial = dr["RazonSocial"].ToString(),
                                RUC = dr["RUC"].ToString(),
                                Representante = dr["Representante"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Ciudad = dr["Ciudad"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj = new Datos();
            }
            return obj;
        }

        public int Guardar(Datos objeto, out string mensaje)
        {
            mensaje = string.Empty;
            SqlTransaction objTransaccion = null;
            int respuesta = 0;
            try
            {

                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {

                    conexion.Open(); objTransaccion = 
                    conexion.BeginTransaction();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("update DATOS set RazonSocial = @prazonsocial,");
                    query.AppendLine("RUC = @pruc,");
                    query.AppendLine("Representante = @prepresentante,");
                    query.AppendLine("Correo = @pcorreo,");
                    query.AppendLine("Telefono = @ptelefono,");
                    query.AppendLine("Ciudad = @pciudad");
                    query.AppendLine("where IdDato != null ;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@prazonsocial", objeto.RazonSocial));
                    cmd.Parameters.Add(new SqlParameter("@pruc", objeto.RUC));
                    cmd.Parameters.Add(new SqlParameter("@prepresentante", objeto.Representante));
                    cmd.Parameters.Add(new SqlParameter("@pcorreo", objeto.Correo));
                    cmd.Parameters.Add(new SqlParameter("@ptelefono", objeto.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@pciudad", objeto.Ciudad));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                        mensaje = "No se pudo actualizar los datos";

                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }

        public int ActualizarLogo(byte[] imagen, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {

                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("update DATOS set Logo = @pimagen");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    SqlParameter parameter = new SqlParameter("@pimagen", System.Data.DbType.Binary);
                    parameter.Value = imagen;
                    cmd.Parameters.Add(parameter);
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                        mensaje = "No se pudo actualizar el logo";

                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }

        public byte[] ObtenerLogo(out bool obtenido)
        {
            obtenido = true;
            byte[] obj = new byte[0];
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select top 1 Logo from DATOS";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = (byte[])dr["Logo"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obtenido = false;
                obj = new byte[0];
            }
            return obj;
        }





    }
}
