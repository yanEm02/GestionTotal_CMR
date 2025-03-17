using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;


namespace CapaDatos
{
    public class Conexion
    {
    /*
     * esta clase se encarga de pasar la conexion, primero anadimos la referencia a la clase system.configurartion y luego agregamos la conexion usando una variable que toma el valor de la cadena configurada en app.config 
     
     */
        public static string cadena = ConfigurationManager.ConnectionStrings["cadenaConexion"].ToString();
    }
}
