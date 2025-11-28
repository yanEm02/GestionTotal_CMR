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







    }
}
