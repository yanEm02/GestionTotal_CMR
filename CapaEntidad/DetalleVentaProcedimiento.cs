using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVentaProcedimiento
    {
        public int IdDetalleVentaProcedimiento { get; set; }
        public int Venta { get; set; }
        public Procedimiento oProcedimiento { get; set; }
        public decimal PrecioVenta { get; set; }

    }
}
