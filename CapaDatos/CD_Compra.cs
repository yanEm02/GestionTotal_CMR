using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Xml.Linq;
using System.Linq.Expressions;

namespace CapaDatos
{
    public class CD_Compra
    {
        public int ObtenerCorrelativo()  //obtenemos el id de la compra generada para generar el numero de compra o combrobante 
        {
            int idCorrelativo = 0;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                //capturador de errores en caso de algun problema con la base de datos
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from COMPRA\r\n");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text; //para declararr el tipo de comando ya que es una consulta con select

                    oconexion.Open();//abrir cadena de conexion

                    idCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    idCorrelativo = 0;
                }
            }
            return idCorrelativo;
        }

        public bool Registrar(Compra obj, DataTable detalleCompra, out string mensaje) //Aca registramos la compra 
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                //capturador de errores en caso de algun problema con la base de datos
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_COMPRA", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.oProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("numeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("montoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("montoPendiente", obj.MontoPendiente);

                    // Manejo explícito para el parámetro nullable de fecha
                    if (string.IsNullOrEmpty(obj.FechaLimite))
                    {
                        cmd.Parameters.AddWithValue("fechaLimite", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("fechaLimite", obj.FechaLimite);
                    }

                    cmd.Parameters.AddWithValue("detalleCompra", detalleCompra);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure; //para declararr el tipo de comando ya que es una consulta con select

                    oconexion.Open();//abrir cadena de conexion
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    respuesta = false;
                    mensaje = ex.Message;
                }
            }
            return respuesta;
        }

        public Compra ObtenerCompra(string numero)
        {
            Compra obj = new Compra();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                //capturador de errores en caso de algun problema con la base de datos
                try
                {
                    oconexion.Open();//abrir cadena de conexion
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select c.ID_COMPRA, u.NombreCompleto, p.DOCUMENTO, p.Nombre, p.Telefono,");
                    query.AppendLine("c.TIPODOCUMENTO, c.NUMERODOCUMENTO, c.MONTOTOTAL, c.MontoPendiente, c.FechaLimite,");
                    query.AppendLine("CONVERT(char(10), c.FECHAREGISTRO, 103)[FechaRegistro]");
                    query.AppendLine("from COMPRA c");
                    query.AppendLine("inner join USUARIO u on u.ID_usuario = c.USUARIO");
                    query.AppendLine("inner join PROVEEDOR p on p.ID_PROVEEDOR = c.PROVEEDOR");
                    query.AppendLine("where c.NumeroDocumento = @numero");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Compra()
                            {
                                IdCompra = Convert.ToInt32(dr["ID_COMPRA"]),
                                oUsuario = new Usuario() { Nombre = dr["NombreCompleto"].ToString() },
                                oProveedor = new Proveedor() { Documento = dr["DOCUMENTO"].ToString(), RazonSocial = dr["Nombre"].ToString(), Telefono = dr["Telefono"].ToString() },
                                TipoDocumento = dr["TIPODOCUMENTO"].ToString(),
                                NumeroDocumento = dr["NUMERODOCUMENTO"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MONTOTOTAL"]),
                                MontoPendiente = Convert.ToDecimal(dr["MontoPendiente"]),
                                FechaLimite = dr["FechaLimite"] != DBNull.Value ? Convert.ToDateTime(dr["FechaLimite"]).ToString("dd/MM/yyyy") : "",
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Compra();
                }
            }

            if (obj.IdCompra != 0)
            {
                obj.oDetalleCompra = new CD_Compra().ObtenerDetalleCompra(obj.IdCompra);
            }

            return obj;
        }

        public List<DetalleCompra> ObtenerDetalleCompra(int idCompra) //para mostrar los productos de la compra
        {
            List<DetalleCompra> oLista = new List<DetalleCompra>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select p.NOMBRE, dc.PRECIOCOMPRA, dc.CANTIDAD, dc.MONTOTOTAL");
                    query.AppendLine("from DETALLE_COMPRA dc");
                    query.AppendLine("inner join PRODUCTO p on p.ID_PRODUCTO = dc.PRODUCTO");
                    query.AppendLine("where dc.COMPRA = @idCompra");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idCompra", idCompra);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new DetalleCompra()
                            {
                                oProducto = new Producto() { Nombre = dr["Nombre"].ToString() },
                                PrecioCompra = Convert.ToDecimal(dr["PRECIOCOMPRA"].ToString()),
                                Cantidad = Convert.ToInt32(dr["cantidad"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MONTOTOTAL"].ToString()),
                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                oLista = new List<DetalleCompra>();
            }
            return oLista;
        }

        public bool EliminarCompra(string numeroDocumento, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                //capturador de errores en caso de algun problema con la base de datos
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_COMPRA", oconexion);
                    cmd.Parameters.AddWithValue("numeroDocumento", numeroDocumento);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure; //para declararr el tipo de comando ya que es una consulta con select
                    oconexion.Open();//abrir cadena de conexion
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    mensaje = ex.Message;
                }
            }
            return respuesta;
        }

        public bool RegistrarPago(HistorialPagoCompra obj, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_PAGO_COMPRA", oconexion);
                    cmd.Parameters.AddWithValue("IdCompra", obj.oCompra.IdCompra);
                    cmd.Parameters.AddWithValue("MontoAbonado", obj.Cantidad);
                    cmd.Parameters.AddWithValue("TipoPago", obj.TipoPago);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    mensaje = ex.Message;
                }
            }
            return respuesta;
        }

        public List<HistorialPagoCompra> ObtenerHistorialPagos(int idCompra)
        {
            List<HistorialPagoCompra> lista = new List<HistorialPagoCompra>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "SELECT Cantidad, TipoPago, CONVERT(char(10), FechaRegistro, 103) as FechaRegistro FROM HISTORIAL_PAGO_COMPRA WHERE Compra = @IdCompra";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@IdCompra", idCompra);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new HistorialPagoCompra()
                            {
                                Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                TipoPago = dr["TipoPago"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<HistorialPagoCompra>();
                }
            }
            return lista;
        }
    }
}