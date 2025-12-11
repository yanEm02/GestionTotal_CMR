using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<ReporteCompra> Compra(string fechaInicio, string fechaFin, int idProveedor)
        {
            List<ReporteCompra> lista = new List<ReporteCompra>();
            using(SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("SP_ReporteCompra", oconexion);
                    //cmd.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    //cmd.Parameters.AddWithValue("fechaFin", fechaFin);

                    cmd.Parameters.AddWithValue("fechaInicio", DateTime.Parse(fechaInicio).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("fechaFin", DateTime.Parse(fechaFin).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("idProveedor", idProveedor);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //mientras comando lea desde la cadena de base de datos entonces va a ir almacenando los datos 
                        while (dr.Read())
                        {
                            lista.Add(new ReporteCompra()
                            {
                                FechaRegistro =dr["FechaRegistro"].ToString(),
                                TipoDocumento =dr["TipoDocumento"].ToString(),
                                NumeroDocumento =dr["NumeroDocumento"].ToString(),
                                MontoTotal =dr["Montototal"].ToString(),
                                UsuarioRegistro =dr["UsuarioRegistro"].ToString(),
                                DocumentoProveedor =dr["DocumentoProveedor"].ToString(),
                                RazonSocial =dr["Nombre"].ToString(),
                                CodigoProducto =dr["CodigoProducto"].ToString(),
                                NombreProducto =dr["NombreProducto"].ToString(),
                                Categoria =dr["Categoria"].ToString(),
                                PrecioCompra =dr["PrecioCompra"].ToString(),
                                PrecioVenta =dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["MontoTotal"].ToString(),
                            });

                        }

                    }
                }
                catch (Exception ex) 
                {
                    lista = new List<ReporteCompra>();
                }
            }

            return lista;
        }

        public List<ReporteVenta> Venta(string fechaInicio, string fechaFin)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();    

            using(SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("SP_ReporteVenta", oconexion);
                    //cmd.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    //cmd.Parameters.AddWithValue("fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("fechaInicio", DateTime.Parse(fechaInicio).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("fechaFin", DateTime.Parse(fechaFin).ToString("yyyy-MM-dd"));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //mientras comando lea desde la cadena de base de datos entonces va a ir almacenando los datos 
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVenta()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["Montototal"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["CLIENTE"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString(),
                            });

                        }

                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ReporteVenta>();
                }
            }

            return lista;
        }

        public List<ReporteProcedimiento> VentaProcedimiento(string fechaInicio, string fechaFin)
        {
            List<ReporteProcedimiento> lista = new List<ReporteProcedimiento>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("SP_ReporteProcedimiento", oconexion);
                    cmd.Parameters.AddWithValue("fechaInicio", DateTime.Parse(fechaInicio).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("fechaFin", DateTime.Parse(fechaFin).ToString("yyyy-MM-dd"));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //mientras comando lea desde la cadena de base de datos entonces va a ir almacenando los datos 
                        while (dr.Read())
                        {
                            lista.Add(new ReporteProcedimiento()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["Montototal"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                NombreCliente = dr["NombreCompleto"].ToString(),
                                CodigoProcedimiento = dr["CodigoProducto"].ToString(),
                                NombreProcedimiento = dr["NombreProcedimiento"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                            });

                        }

                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ReporteProcedimiento>();
                }
            }

            return lista;
        }


    }
}
