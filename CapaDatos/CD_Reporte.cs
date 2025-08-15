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
                                TipoDocumento =dr["Tipo_Documento"].ToString(),
                                NumeroDocumento =dr["Numero_Documento"].ToString(),
                                MontoTotal =dr["Monto_total"].ToString(),
                                UsuarioRegistro =dr["UsuarioRegistro"].ToString(),
                                DocumentoProveedor =dr["DocumentoProveedor"].ToString(),
                                RazonSocial =dr["Razon_Social"].ToString(),
                                CodigoProducto =dr["CodigoProducto"].ToString(),
                                NombreProducto =dr["NombreProducto"].ToString(),
                                Categoria =dr["Categoria"].ToString(),
                                PrecioCompra =dr["Precio_Compra"].ToString(),
                                PrecioVenta =dr["Precio_Venta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString(),
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
                                TipoDocumento = dr["Tipo_Documento"].ToString(),
                                NumeroDocumento = dr["Numero_Documento"].ToString(),
                                MontoTotal = dr["Monto_total"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoCliente = dr["Documento_Cliente"].ToString(),
                                NombreCliente = dr["CLIENTE"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                PrecioVenta = dr["Precio_Venta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["Sub_Total"].ToString(),
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


    }
}
