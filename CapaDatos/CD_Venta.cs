using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Venta
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
                    query.AppendLine("select count(*) + 1 from VENTA");

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

        public bool RestarStock(int idProducto, int cantidad)  //metodo para restar el stock cada vez que se seleccione un producto para la venta
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                //capturador de errores en caso de algun problema con la base de datos
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRODUCTO set stock = stock - @cantidad where id_Producto = @idProducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.CommandType = CommandType.Text; //para declararr el tipo de comando ya que es una consulta con select

                    oconexion.Open();//abrir cadena de conexion

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool SumarStock(int idProducto, int cantidad)  //metodo para restar el stock cada vez que se seleccione un producto para la venta
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                //capturador de errores en caso de algun problema con la base de datos
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRODUCTO set stock = stock + @cantidad where id_Producto = @idProducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.CommandType = CommandType.Text; //para declararr el tipo de comando ya que es una consulta con select

                    oconexion.Open();//abrir cadena de conexion

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Registrar(Venta obj, DataTable detalleVenta, out string mensaje) //Aca registramos la compra 
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                //capturador de errores en caso de algun problema con la base de datos
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_VENTA", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("numeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("documentoCliente", obj.DocumentoCliente);
                    cmd.Parameters.AddWithValue("nombreCliente", obj.NombreCliente);
                    cmd.Parameters.AddWithValue("montoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("montoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("montoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("detalleVenta", detalleVenta);
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

        public Venta ObtenerVenta(string numero)
        {
            Venta obj = new Venta();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select v.ID_VENTA,u.NombreCompleto,");
                    query.AppendLine("v.Documento_Cliente,v.CLIENTE,");
                    query.AppendLine("v.Tipo_Documento,v.numero_Documento,");
                    query.AppendLine("v.MONTO_PAGO,v.Monto_cambio,v.Monto_total,");
                    query.AppendLine("CONVERT(char(10),v.FECHA_REGISTRO,103)[FechaRegistro]");
                    query.AppendLine("from VENTA v");
                    query.AppendLine("inner join USUARIO u on u.ID_usuario = v.USUARIO");
                    query.AppendLine("where v.NUMERO_DOCUMENTO = @numero");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            obj = new Venta()
                            {
                                IdVenta = int.Parse(dataReader["ID_VENTA"].ToString()),
                                oUsuario = new Usuario() { Nombre = dataReader["NombreCompleto"].ToString() },
                                DocumentoCliente = dataReader["Documento_Cliente"].ToString(),
                                NombreCliente = dataReader["CLIENTE"].ToString(),
                                TipoDocumento = dataReader["Tipo_Documento"].ToString(),
                                NumeroDocumento = dataReader["numero_Documento"].ToString(),
                                MontoPago = Convert.ToDecimal(dataReader["MONTO_PAGO"].ToString()),
                                MontoCambio = Convert.ToDecimal(dataReader["Monto_cambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(dataReader["Monto_total"].ToString()),
                                FechaRegistro = dataReader["FechaRegistro"].ToString(),


                            };
                        }

                    }
                }
                catch 
                {
                    obj = new Venta();
                }
            }



            return obj;
        }

        //metodo que nos va a retornar los detalles de la venta 
        public List<DetalleVenta> ObtenerDetalleVenta(int idVenta)
        {
            List<DetalleVenta> oLista = new List<DetalleVenta>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.NOMBRE,dv.PRECIO_VENTA,dv.CANTIDAD,dv.SUB_TOTAL");
                    query.AppendLine("from DETALLE_VENTA dv");  
                    query.AppendLine("inner join PRODUCTO p on p.ID_PRODUCTO =dv.PRODUCTO");
                    query.AppendLine("where dv.VENTA = @idVenta");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            oLista.Add(new DetalleVenta()
                            {
                                oProducto = new Producto() { Nombre = dataReader["Nombre"].ToString() },
                                PrecioVenta = Convert.ToDecimal(dataReader["PRECIO_VENTA"].ToString()),
                                Cantidad = Convert.ToInt32(dataReader["CANTIDAD"].ToString()),
                                SubTotal = Convert.ToDecimal(dataReader["SUB_TOTAL"].ToString()),

                            });
                        }
                    }
                }
                catch
                {
                    oLista = new List<DetalleVenta>();  
                }
            }

            return oLista;

        }


    }
}
