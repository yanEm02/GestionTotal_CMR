using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Procedimiento
    {
        private CD_Procedimiento objcd_Procedimiento = new CD_Procedimiento();
        public List<Procedimiento> Listar()
        {
            return objcd_Procedimiento.Listar();
        }

        public int Registrar(Procedimiento obj, out string Mensaje)
        {
            //regla de validacion 
            Mensaje = string.Empty;

            if (obj.Codigo == 0)
            {
                Mensaje += "Es necesario el codigo del procedimiento para poder registrar\n";
            }

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                Mensaje += "Es necesario el nombre del procedimiento para poder registrar\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Procedimiento.Registrar(obj, out Mensaje);

            }

        }

        public bool Editar(Procedimiento obj, out string Mensaje)
        {
            //regla de validacion 
            Mensaje = string.Empty;

             if (obj.Codigo == 0)
            {
                Mensaje += "Es necesario el codigo del procedimiento para poder registrar\n";
            }

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                Mensaje += "Es necesario el nombre del procedimiento para poder registrar\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Procedimiento.Editar(obj, out Mensaje);

            }
        }

        public bool Eliminar(Procedimiento obj, out string Mensaje)
        {
            return objcd_Procedimiento.Eliminar(obj, out Mensaje);
        }


    }
}
