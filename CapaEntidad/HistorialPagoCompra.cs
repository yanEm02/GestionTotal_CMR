using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class HistorialPagoCompra
    {
        public int IdHisotiralPago { get; set; }
        public Compra oCompra { get; set; }
        public decimal Cantidad { get; set; }
        public string TipoPago { get; set; }
        public string FechaRegistro { get; set; }
    }
}
