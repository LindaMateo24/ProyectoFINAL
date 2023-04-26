using ProyectoPrestamo.Modelo;
using ProyectoPrestamo.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Drawing;

namespace ProyectoPrestamo.Logica
{
    public class PrestamoLogica
    {

        private static PrestamoLogica _instancia = null;
        Cuota oCuota;
        int IdPrestamo;
        int TotalCuotas;
        Prestamo prestamo;

        public PrestamoLogica()
        {

        }

        public static PrestamoLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new PrestamoLogica();
                return _instancia;
            }
        }

        public int ObtenerCorrelativo(out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from PRESTAMO");
                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                mensaje = ex.Message;
            }

            return respuesta;
        }


        public int Registrar(Prestamo oPrestamo, List<Cuota> oListaCuota, out string mensaje)
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

                    query.AppendLine("CREATE TABLE  TABLE_TEMP (id INTEGER);");

                    query.AppendLine(string.Format("Insert into PRESTAMO(NumeroOperacion,FechaRegistro,IdTipoPago,IdTipoMoneda,FechaInicio,FechaFin,MontoPrestamo,Interes,NumeroCuotas,MontoCuota,TotalIntereses,MontoTotal,NombreCliente,TipoDocumento,NumeroDocumento,Direccion,Ciudad,Correo,NumeroTelefono,Estado,Clausula) values('{0}','{1}',{2},{3},'{4}','{5}','{6}',{7},{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}');",
                        oPrestamo.NumeroOperacion,
                        oPrestamo.FechaRegistro,
                        oPrestamo.oTipoPago.IdTipoPago,
                        oPrestamo.oTipoMoneda.IdTipoMoneda,
                        oPrestamo.FechaInicio,
                        oPrestamo.FechaFin,
                        oPrestamo.MontoPrestamo,
                        oPrestamo.Interes,
                        oPrestamo.NumeroCuotas,
                        oPrestamo.MontoCuota,
                        oPrestamo.TotalIntereses,
                        oPrestamo.MontoTotal,
                        oPrestamo.NombreCliente,
                        oPrestamo.TipoDocumento,
                        oPrestamo.NumeroDocumento,
                        oPrestamo.Direccion,
                        oPrestamo.Ciudad,
                        oPrestamo.Correo,
                        oPrestamo.NumeroTelefono,
                        oPrestamo.Estado,
                        oPrestamo.Clausula
                        ));
                    
                    query.AppendLine("INSERT INTO TABLE_TEMP (id) VALUES (SCOPE_IDENTITY());");

                    foreach (Cuota c in oListaCuota)
                    {
                        query.AppendLine(string.Format("insert into CUOTA(IdPrestamo,NumeroCuota,FechaPagoCuota,MontoCuota,EstadoCuota,ProximoPago) values({0},{1},'{2}','{3}','{4}',{5});",
                            "(select id from TABLE_TEMP)",
                            c.NumeroCuota,
                            c.FechaPagoCuota,
                            c.MontoCuota.ToString("0.00"),
                            c.EstadoCuota,
                            c.ProximoPago
                            ));
                    }

                    query.AppendLine(string.Format("INSERT INTO CLIENTES(NombreCompleto,TipoDocumento,NumeroDocumento,Direccion,Ciudad,Correo,NumeroTelefono) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}');",
                        oPrestamo.NombreCliente,
                        oPrestamo.TipoDocumento,
                        oPrestamo.NumeroDocumento,
                        oPrestamo.Direccion,
                        oPrestamo.Ciudad,
                        oPrestamo.Correo,
                        oPrestamo.NumeroTelefono));

                    query.AppendLine("DROP TABLE IF EXISTS TABLE_TEMP;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Transaction = objTransaccion;
                    respuesta = cmd.ExecuteNonQuery();


                    if (respuesta < 1)
                    {
                        objTransaccion.Rollback();
                        mensaje = "No se pudo registrar el alquiler";
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


        public Prestamo Obtener(string numerooperacion, string numerodocumento)
        {
            Prestamo objeto = new Prestamo();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select top 1 p.IdPrestamo,p.NumeroOperacion,p.FechaRegistro [FechaRegistro],");
                    query.AppendLine("tp.IdTipoPago,tp.Descripcion[DescripcionTP],tm.IdTipoMoneda,tm.Abreviatura,");
                    query.AppendLine("p.FechaInicio [FechaInicio],p.FechaFin [FechaFin],");
                    query.AppendLine("p.MontoPrestamo,p.Interes,p.NumeroCuotas,p.MontoCuota,p.TotalIntereses,p.MontoTotal,p.NombreCliente,");
                    query.AppendLine("p.TipoDocumento,p.NumeroDocumento,p.Direccion,p.Ciudad,p.Correo,p.NumeroTelefono,p.Estado,p.Clausula");
                    query.AppendLine("from PRESTAMO p");
                    query.AppendLine("INNER JOIN TIPO_PAGO tp on tp.IdTipoPago = p.IdTipoPago");
                    query.AppendLine("INNER JOIN TIPO_MONEDA tm on tm.IdTipoMoneda = p.IdTipoMoneda");
                    query.AppendLine("where p.NumeroOperacion = iif(upper(@pnumerooperacion) = '', p.NumeroOperacion, upper(@pnumerooperacion))");
                    query.AppendLine("and p.NumeroDocumento = iif(upper(@pnumerodocumento) = '', p.NumeroDocumento, upper(@pnumerodocumento))");
                    query.AppendLine("ORDER BY p.IdPrestamo DESC;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@pnumerooperacion", numerooperacion));
                    cmd.Parameters.Add(new SqlParameter("@pnumerodocumento", numerodocumento));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new Prestamo()
                            {
                                IdPrestamo = Convert.ToInt32(dr["IdPrestamo"].ToString()),
                                NumeroOperacion = dr["NumeroOperacion"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                oTipoPago = new TipoPago() { IdTipoPago = Convert.ToInt32(dr["IdTipoPago"].ToString()),
                                    Descripcion = dr["DescripcionTP"].ToString(),
                                },
                                oTipoMoneda = new TipoMoneda() { IdTipoMoneda = Convert.ToInt32(dr["IdTipoMoneda"].ToString()),
                                    Abreviatura = dr["Abreviatura"].ToString(),
                                },
                                FechaInicio = dr["FechaInicio"].ToString(),
                                FechaFin = dr["FechaFin"].ToString(),
                                MontoPrestamo = dr["MontoPrestamo"].ToString(),
                                Interes = dr["Interes"].ToString(),
                                NumeroCuotas = Convert.ToInt32(dr["NumeroCuotas"].ToString()),
                                MontoCuota = dr["MontoCuota"].ToString(),
                                TotalIntereses = dr["TotalIntereses"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Ciudad = dr["Ciudad"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                NumeroTelefono = dr["NumeroTelefono"].ToString(),
                                Estado = dr["Estado"].ToString(),
                                Clausula = dr["Clausula"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objeto = new Prestamo();
            }
            return objeto;
        }


        public int Actualizar(Prestamo objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            try
            {

                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {

                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("UPDATE PRESTAMO set NombreCliente = @pnombre,");
                    query.AppendLine("TipoDocumento = @ptipodoc,");
                    query.AppendLine("NumeroDocumento = @pdoc,");
                    query.AppendLine("Direccion = @pdirec,");
                    query.AppendLine("Ciudad = @pciudad,");
                    query.AppendLine("Correo = @pcorreo,");
                    query.AppendLine("NumeroTelefono = @ptelefono,");
                    query.AppendLine("Clausula = @pclausula");
                    query.AppendLine("where IdPrestamo = @pid;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@pnombre", objeto.NombreCliente));
                    cmd.Parameters.Add(new SqlParameter("@ptipodoc", objeto.TipoDocumento));
                    cmd.Parameters.Add(new SqlParameter("@pdoc", objeto.NumeroDocumento));
                    cmd.Parameters.Add(new SqlParameter("@pdirec", objeto.Direccion));
                    cmd.Parameters.Add(new SqlParameter("@pciudad", objeto.Ciudad));
                    cmd.Parameters.Add(new SqlParameter("@pcorreo", objeto.Correo));
                    cmd.Parameters.Add(new SqlParameter("@ptelefono", objeto.NumeroTelefono));
                    cmd.Parameters.Add(new SqlParameter("@pclausula", objeto.Clausula));
                    cmd.Parameters.Add(new SqlParameter("@pid", objeto.IdPrestamo));
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


        public int Cancelar(int IdPrestamo, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            SqlTransaction objTransaccion = null;
            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                   
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(string.Format("UPDATE PRESTAMO SET Estado = '{0}' WHERE IdPrestamo = {1};", "PAGADO", IdPrestamo));
                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    
                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                    {
                        objTransaccion.Rollback();
                        mensaje = "No se pudo cancelar el prestamo";
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


        public int Pagar(Cuota oCuota, int IdPrestamo,int TotalCuotas, Prestamo prestamo, out string mensaje)
        {

            this.oCuota = oCuota;
            this.IdPrestamo = IdPrestamo;
            this.TotalCuotas = TotalCuotas;
            this.prestamo = prestamo;

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

                    query.AppendLine(string.Format("UPDATE CUOTA set EstadoCuota = 'PAGADO' , FechaPago = '{0}', ProximoPago = 0 WHERE IdCuota = {1} and IdPrestamo = {2} and NumeroCuota = {3};", oCuota.FechaPago, oCuota.IdCuota, IdPrestamo, oCuota.NumeroCuota));

                    //VALIDAMOS SI ES EL ULTIMO PERIODO A PAGAR
                    if (TotalCuotas > oCuota.NumeroCuota)
                    {
                        oCuota.NumeroCuota += 1;
                        query.AppendLine(string.Format("UPDATE CUOTA set ProximoPago = 1 where IdPrestamo = {0} and NumeroCuota = {1};", IdPrestamo, oCuota.NumeroCuota));
                    }
                    else
                    {
                        query.AppendLine(string.Format("UPDATE PRESTAMO SET Estado = 'CERRADO' WHERE IdPrestamo = {0};",IdPrestamo));
                    }

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Transaction = objTransaccion;
                    respuesta = cmd.ExecuteNonQuery();


                    if (respuesta < 1)
                    {
                        objTransaccion.Rollback();
                        mensaje = "No se pudo registrar el pago";
                    }

                    objTransaccion.Commit();

                    

                    // Create a new instance of the PrintDocument class
                    PrintDocument pd = new PrintDocument();

                    // Set the PrintPage event handler method
                    pd.PrintPage += new PrintPageEventHandler(PrintPage);

                    // Call the Print method to send the document to the printer
                    pd.Print();

                    // Define the PrintPage event handler method
                   

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


        public List<VistaReporte> Resumen(string fechainicio = "", string fechafin = "")
        {
            List<VistaReporte> oLista = new List<VistaReporte>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select p.NumeroOperacion,p.FechaRegistro [FechaRegistro],tp.Descripcion[DescripcionTP], tm.Abreviatura,");
                    query.AppendLine("p.FechaInicio[FechaInicio],p.MontoPrestamo,p.Interes,p.NumeroCuotas,p.MontoCuota,");
                    query.AppendLine("p.TotalIntereses,p.MontoTotal,p.NombreCliente,p.TipoDocumento,p.NumeroDocumento,p.Direccion,p.Ciudad,p.Correo,p.NumeroTelefono,p.Estado");
                    query.AppendLine("from PRESTAMO p");
                    query.AppendLine("inner join TIPO_PAGO tp on tp.IdTipoPago = p.IdTipoPago");
                    query.AppendLine("inner join TIPO_MONEDA tm on tm.IdTipoMoneda = p.IdTipoMoneda");
                    query.AppendLine("where CAST(p.FechaRegistro as date) BETWEEN CAST(@pfechainicio as date) AND CAST(@pfechafin as date)");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@pfechainicio", fechainicio));
                    cmd.Parameters.Add(new SqlParameter("@pfechafin", fechafin));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new VistaReporte()
                            {
                                NroOperacion = dr["NumeroOperacion"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                FormaPago = dr["DescripcionTP"].ToString(),
                                TipoMoneda = dr["Abreviatura"].ToString(),
                                FechaInicio = dr["FechaInicio"].ToString(),
                                MontoPrestamo = dr["MontoPrestamo"].ToString(),
                                Interes = dr["Interes"].ToString(),
                                NroCuotas = dr["NumeroCuotas"].ToString(),
                                MontoporCuota = dr["MontoCuota"].ToString(),
                                TotalInteres = dr["TotalIntereses"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NroDocumento = dr["NumeroDocumento"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Ciudad = dr["Ciudad"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                NroTelefonoCelular = dr["NumeroTelefono"].ToString(),
                                Estado = dr["Estado"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<VistaReporte>();
            }
            return oLista;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {

            // Print the image using the Graphics object
            Image image = Image.FromFile("C:\\Users\\linda\\Desktop\\WhatsApp Image 2023-04-23 at 20.11.57.jpg");
            ev.Graphics.DrawImage(image, new Rectangle(50, 50, 250, 150));

            // Define the font and brush to use for drawing text
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font textFont = new Font("Arial", 8);
            Brush brush = Brushes.Black;
           
            // Define the position and spacing of the text
            int x = 50;
            int y = 150;
            int lineHeight = 20;

            // Draw the header
            ev.Graphics.DrawString(" ", textFont, brush, x, y);
            y += lineHeight * 4;

            // Draw the date and time
            ev.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), textFont, brush, x, y);
            y += lineHeight * 2;

            // Draw the payment details
            ev.Graphics.DrawString($"Cliente:                {this.prestamo.NombreCliente}", textFont, brush, x, y);
            y += lineHeight;
            ev.Graphics.DrawString($"Prestamo:               {this.prestamo.NumeroOperacion}", textFont, brush, x, y);
            y += lineHeight * 2;

            // Draw the payment details
            ev.Graphics.DrawString($"Monto pagado:          { decimal.Parse(this.prestamo.MontoCuota).ToString("C")}", textFont, brush, x, y);
            y += lineHeight;
            ev.Graphics.DrawString($"Numero de cuota:       {this.oCuota.NumeroCuota}", textFont, brush, x, y);
            y += lineHeight * 2;

            // Draw the footer
            ev.Graphics.DrawString("Gracias por su pago!", headerFont, brush, x, y);
            y += lineHeight;
            ev.Graphics.DrawString("Los Mugiwara", headerFont, brush, x, y);
        }


    }
}
