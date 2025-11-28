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


    }
}
