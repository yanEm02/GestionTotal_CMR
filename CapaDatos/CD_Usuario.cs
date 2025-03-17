using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            //conexion a BD
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                //capturador de errores en caso de algun problema con la base de datos
                try
                {
                    string query = "select Id_Usuario, Documento, Nombre, Correo, Clave, Estado from usuario";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text; //para declararr el tipo de comando ya que es una consulta con select

                    oconexion.Open();//abrir cadena de conexion

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //mientras comando lea desde la cadena de base de datos entonces va a ir almacenando los datos 
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["Id_Usuario"]),
                                Documento = dr["Documento"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])

                            });

                        }

                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Usuario>();
                }
            }

            return lista;
        }
    }
}

