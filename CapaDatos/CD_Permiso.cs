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
    public class CD_Permiso
    {
        public List<Permiso> Listar(int idUsuario)
        {
            List<Permiso> lista = new List<Permiso>();

            //conexion a BD
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                //capturador de errores en caso de algun problema con la base de datos
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.ID_ROL,p.NombreMenu from PERMISO p");
                    query.AppendLine("inner join ROL r on r.ID_ROL = p.ID_ROL");
                    query.AppendLine("inner join USUARIO u on u.ID_ROL = r.ID_ROL");
                    query.AppendLine("where u.ID_USUARIO = @idUsuario");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario); //para reemplazar el ID usuario que le vamos a pasar 
                    cmd.CommandType = CommandType.Text; //para declararr el tipo de comando ya que es una consulta con select

                    oconexion.Open();//abrir cadena de conexion

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //mientras comando lea desde la cadena de base de datos entonces va a ir almacenando los datos 
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                oRol = new Rol() {IdRol = Convert.ToInt32(dr["ID_ROL"]) },
                                NombreMenu = dr["NombreMenu"].ToString(),

                            });

                        }

                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Permiso>();
                }
            }

            return lista;
        }
    }
}
