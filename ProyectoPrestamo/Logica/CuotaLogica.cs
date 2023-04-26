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
    public class CuotaLogica
    {
        private static CuotaLogica _instancia = null;

        public CuotaLogica()
        {

        }

        public static CuotaLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new CuotaLogica();
                return _instancia;
            }
        }

        public List<Cuota> Listar(int idprestamo)
        {
            List<Cuota> oLista = new List<Cuota>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCuota, IdPrestamo, NumeroCuota, FechaPagoCuota [FechaPagoCuota],");
                    query.AppendLine("MontoCuota,EstadoCuota,iif(FechaPago = '','',FechaPago)FechaPago, ProximoPago");
                    query.AppendLine("from CUOTA");
                    query.AppendLine("where IdPrestamo = @pid");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SqlParameter("@pid", idprestamo));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Cuota()
                            {
                                IdCuota = Convert.ToInt32(dr["IdCuota"].ToString()),
                                oPrestamo = new Prestamo() { IdPrestamo = Convert.ToInt32(dr["IdPrestamo"].ToString()) },
                                NumeroCuota = Convert.ToInt32(dr["NumeroCuota"].ToString()),
                                FechaPagoCuota = dr["FechaPagoCuota"].ToString(),
                                MontoCuota = Convert.ToDecimal(dr["MontoCuota"].ToString()),
                                EstadoCuota = dr["EstadoCuota"].ToString(),
                                FechaPago = dr["FechaPago"].ToString(),
                                ProximoPago = Convert.ToInt32(dr["ProximoPago"].ToString()),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<Cuota>();
            }
            return oLista;
        }

    }
}
