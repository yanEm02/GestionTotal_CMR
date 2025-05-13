using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Negocio
    {
        public Negocio ObtenerDatos()
        {
            Negocio obj = new Negocio();

            try 
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select IdNegocio,Nombre,RNC,Direccion from NEGOCIO";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Negocio()
                            {
                                IdNegocio = int.Parse(dr["IdNegocio"].ToString()),
                                Nombre = dr["Nombre"].ToString(),
                                Rnc = dr["RNC"].ToString(),
                                Direccion = dr["Direccion"].ToString()
                            };
                        }
                    }
                }
            }
            catch
            {
                obj = new Negocio();
            }

            return obj;
        }

        public bool GuardarDatos( Negocio objeto, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE NEGOCIO SET NOMBRE = @nombre,");
                    query.AppendLine("RNC = @RNC,");
                    query.AppendLine("DIRECCION = @DIRECCION");
                    query.AppendLine("WHERE IDNEGOCIO = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@nombre",objeto.Nombre);
                    cmd.Parameters.AddWithValue("@rnc",objeto.Rnc);
                    cmd.Parameters.AddWithValue("@direccion",objeto.Direccion);
                    cmd.CommandType = CommandType.Text;

                    if(cmd.ExecuteNonQuery() <  1) 
                    {
                        mensaje = "No se pudo guardar los datos";
                        respuesta = false;
                    }
                }
            }
            catch(Exception ex) 
            {
                mensaje = ex.Message;
                respuesta=false;
            }

            return respuesta;

        }

        //metodo para obtener el logo del negocio, usando un array de bytes 
        public byte[] ObtenerLogo( out bool obtenido) //variable obtenido apra ver si fue un exito la obtencion del logo
        {
            obtenido = true;
            byte[] LogoBytes = new byte[0];

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select Logo from NEGOCIO where IdNegocio = 1";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text ;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LogoBytes = (byte[])dr["Logo"]; 

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obtenido = false;
                LogoBytes=new byte[0]; 
            }

            return LogoBytes;   
        }

        //metodo para actualizar el logo
        public bool ActualizarLogo(byte[] image, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE NEGOCIO SET Logo = @imagen");
                    query.AppendLine("WHERE IDNEGOCIO = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@imagen", image);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo";
                        respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }

            return respuesta;
        }
    }
}
