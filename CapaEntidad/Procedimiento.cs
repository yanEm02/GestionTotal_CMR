using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Procedimiento
    {
        public int ID_Procedimiento { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public Categoria oCategoria { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioVentaAsegurado { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
