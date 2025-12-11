using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;


namespace CapaDatos
{
    public class CD_Procedimiento
    {
        public List<Procedimiento> Listar()
        {
            List<Procedimiento> lista = new List<Procedimiento>();
            //conexion a BD
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                //capturador de errores en caso de algun problema con la base de datos
                try
                {
                    StringBuilder query = new StringBuilder();
                    // Add the SELECT keyword and correct the column aliasing
                    query.AppendLine("SELECT ID_procedimiento, CODIGO, NOMBRE, c.ID_CATEGORIA, c.DESCRIPCION AS DescripcionCategoria, Precio_Venta, PrecioVenta_Asegurado, p.ESTADO");
                    query.AppendLine("FROM PROCEDIMIENTO p");
                    query.AppendLine("INNER JOIN CATEGORIA c ON c.ID_CATEGORIA = p.CATEGORIA");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text; //para declararr el tipo de comando ya que es una consulta con select
                    oconexion.Open();//abrir cadena de conexion
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //mientras comando lea desde la cadena de base de datos entonces va a ir almacenando los datos 
                        while (dr.Read())
                        {
                            lista.Add(new Procedimiento()
                            {
                                ID_Procedimiento = Convert.ToInt32(dr["ID_PROCEDIMIENTO"]),
                                Codigo = Convert.ToInt32(dr["Codigo"]),
                                Nombre = dr["NOMBRE"].ToString(),
                                oCategoria = new Categoria()
                                {
                                    IdCategoria = Convert.ToInt32(dr["ID_CATEGORIA"]),
                                    Descripcion = dr["DescripcionCategoria"].ToString()
                                },
                                PrecioVenta = Convert.ToDecimal(dr["PRECIO_VENTA"].ToString()),
                                PrecioVentaAsegurado = Convert.ToDecimal(dr["PRECIOVENTA_ASEGURADO"].ToString()),
                                Estado = dr["Estado"] != DBNull.Value ? Convert.ToBoolean(dr["Estado"]) : false,
                                //FechaRegistro = dr["FechaRegistro"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Procedimiento>();
                }
            }
            return lista;
        }

        public int Registrar(Procedimiento obj, out string Mensaje)
        {
            int idProductoGenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_PROCEDIMIENTO", oconexion);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("PrecioVenta", obj.PrecioVenta);
                    cmd.Parameters.AddWithValue("PrecioVenta_asegurado", obj.PrecioVentaAsegurado);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure; //para declararr el tipo de comando ya que es un proc almacenado

                    oconexion.Open();//abrir cadena de conexion

                    cmd.ExecuteNonQuery();

                    idProductoGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception e)
            {
                idProductoGenerado = 0;
                Mensaje = e.Message;
            }
            return idProductoGenerado;
        }

        public bool Editar(Procedimiento obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITAR_PROCEDIMIENTO", oconexion);
                    cmd.Parameters.AddWithValue("ID_procedimiento", obj.ID_Procedimiento);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("PrecioVenta", obj.PrecioVenta);
                    cmd.Parameters.AddWithValue("PrecioVenta_asegurado", obj.PrecioVentaAsegurado);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure; //para declararr el tipo de comando ya que es un proc almacenado

                    oconexion.Open();//abrir cadena de conexion

                    cmd.ExecuteNonQuery(); //ejecutamos el prco

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception e)
            {
                respuesta = false;
                Mensaje = e.Message;
            }
            return respuesta;
        }

        public bool Eliminar(Procedimiento obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_PROCEDIMIENTO", oconexion);
                    cmd.Parameters.AddWithValue("ID_procedimiento", obj.ID_Procedimiento);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure; //para declararr el tipo de comando ya que es un proc almacenado

                    oconexion.Open();//abrir cadena de conexion

                    cmd.ExecuteNonQuery(); //ejecutamos el prco

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }

            }
            catch (Exception e)
            {
                respuesta = false;
                Mensaje = e.Message;
            }


            return respuesta;
        }






    }
}
