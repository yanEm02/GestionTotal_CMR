using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;    

namespace CapaDatos
{
    public class CD_Venta_Procedimiento
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
                    query.AppendLine("select count(*) + 1 from VENTA_Procedimiento");

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

        public bool RegistrarVenta(VentaProcedimiento obj, DataTable detalleVentaProcedimiento, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_Registrar_Venta_Procedimiento", oconexion);
                        cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.IdUsuario);
                        cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                        cmd.Parameters.AddWithValue("numeroDocumento", obj.NumeroDocumento);
                        cmd.Parameters.AddWithValue("idCliente", obj.oCliente.IdCliente);
                        cmd.Parameters.AddWithValue("montoPago", obj.MontoPago);
                        cmd.Parameters.AddWithValue("montoCambio", obj.MontoCambio);
                        cmd.Parameters.AddWithValue("montoTotal", obj.MontoTotal);
                        cmd.Parameters.AddWithValue("detalleProcedimiento", detalleVentaProcedimiento);
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
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }
            return respuesta;
        }

        public VentaProcedimiento ObtenerVentaProcedimiento(string numero)
        {
            VentaProcedimiento obj = new VentaProcedimiento();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select v.ID_VENTA,u.NombreCompleto as NombreUsuario,c.NombreCompleto as NombreCliente,");
                    query.AppendLine("c.Documento,v.TipoDocumento,v.numeroDocumento,");
                    query.AppendLine("v.MONTOPAGO,v.Montocambio,v.Montototal,");
                    query.AppendLine("CONVERT(char(10),v.FECHAREGISTRO,103)[FechaRegistro]");
                    query.AppendLine("from VENTA_PROCEDIMIENTO v");
                    query.AppendLine("inner join USUARIO u on u.ID_usuario = v.USUARIO");
                    query.AppendLine("inner join CLIENTE c on c.ID_cliente = v.Cliente");
                    query.AppendLine("where v.NUMERODOCUMENTO = @numero");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            obj = new VentaProcedimiento()
                            {
                                Id_venta = int.Parse(dataReader["ID_VENTA"].ToString()),
                                oUsuario = new Usuario() { Nombre = dataReader["NombreUsuario"].ToString() },
                                TipoDocumento = dataReader["TipoDocumento"].ToString(),
                                NumeroDocumento = dataReader["numeroDocumento"].ToString(),
                                //oCliente = new Cliente() { Nombre = dataReader["NombreCliente"].ToString() },
                                oCliente = new Cliente()
                                {
                                    Documento = dataReader["Documento"].ToString(),
                                    Nombre = dataReader["NombreCliente"].ToString()
                                },
                                MontoPago = Convert.ToDecimal(dataReader["MONTOPAGO"].ToString()),
                                MontoCambio = Convert.ToDecimal(dataReader["Montocambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(dataReader["Montototal"].ToString()),
                                FechaRegistro = dataReader["FechaRegistro"].ToString(),
                            };
                        }

                    }
                }
                catch
                {
                    obj = new VentaProcedimiento();
                }
            }
            return obj;
        }

        //metodo que nos va a retornar los detalles de la venta del procedimiento
        public List<DetalleVentaProcedimiento> ObtenerDetalleVentaProcedimiento(int idVenta)
        {
            List<DetalleVentaProcedimiento> oLista = new List<DetalleVentaProcedimiento>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.Codigo,p.NOMBRE,c.Descripcion as Categoria,dv.PRECIOVENTA");
                    query.AppendLine("from DETALLE_VENTA_PROCEDIMIENTO dv");
                    query.AppendLine("inner join PROCEDIMIENTO p on p.ID_procedimiento =dv.Procedimiento");
                    query.AppendLine("inner join CATEGORIA c on c.ID_CATEGORIA = p.CATEGORIA");
                    query.AppendLine("where dv.VENTA = @idVenta");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            oLista.Add(new DetalleVentaProcedimiento()
                            {
                                oProcedimiento = new Procedimiento()
                                {
                                    Codigo = Convert.ToInt32(dataReader["Codigo"]),
                                    Nombre = dataReader["Nombre"].ToString(),
                                    oCategoria = new Categoria() { Descripcion = dataReader["Categoria"].ToString() }
                                },
                                PrecioVenta = Convert.ToDecimal(dataReader["PRECIOVENTA"].ToString()),
                                
                            });
                        }
                    }
                }
                catch
                {
                    oLista = new List<DetalleVentaProcedimiento>();
                }
            }

            return oLista;

        }


    }
}
