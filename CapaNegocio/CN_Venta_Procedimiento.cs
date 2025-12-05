using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Venta_Procedimiento
    {
        private CD_Venta_Procedimiento objcd_Venta_Procedimiento = new CD_Venta_Procedimiento();
        public int ObtenerCorrelativo()
        {
            return objcd_Venta_Procedimiento.ObtenerCorrelativo();
        }

        public bool Registrar(VentaProcedimiento obj, DataTable detalleVentaProcedimiento, out string mensaje)
        {
            return objcd_Venta_Procedimiento.RegistrarVenta(obj, detalleVentaProcedimiento, out mensaje);
        }

        public VentaProcedimiento ObtenerVentaProcedimiento(string numero)
        {
            VentaProcedimiento oVenta = objcd_Venta_Procedimiento.ObtenerVentaProcedimiento(numero);

            if (oVenta.Id_venta != 0)
            {
                List<DetalleVentaProcedimiento> oDetalleVentaProcedimiento = objcd_Venta_Procedimiento.ObtenerDetalleVentaProcedimiento(oVenta.Id_venta);
                oVenta.oDetalleVentaProcedimiento = oDetalleVentaProcedimiento;
            }

            return oVenta;
        }

    }
}
