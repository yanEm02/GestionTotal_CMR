using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteProcedimiento
    {
        public string FechaRegistro { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string MontoTotal { get; set; }
        public string UsuarioRegistro { get; set; }
        public string NombreCliente { get; set; }
        public string CodigoProcedimiento { get; set; }
        public string NombreProcedimiento { get; set; }
        public string Categoria { get; set; }
        public string PrecioVenta { get; set; }
    }
}
