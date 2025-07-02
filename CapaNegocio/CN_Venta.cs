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
    public class CN_Venta
    {
        private CD_Venta objcd_Venta = new CD_Venta();

        public bool RestarStock(int idProducto, int cantidad)
        {
            return objcd_Venta.RestarStock(idProducto, cantidad);
        }

        public bool SumarStock(int idProducto, int cantidad)
        {
            return objcd_Venta.SumarStock(idProducto, cantidad);
        }

        public int ObtenerCorrelativo()
        {
            return objcd_Venta.ObtenerCorrelativo();
        }

        public bool Registrar(Venta obj, DataTable detalleVenta, out string Mensaje)
        {
            return objcd_Venta.Registrar(obj, detalleVenta, out Mensaje);

        }

        public Venta ObtenerVenta(string numero)
        {
            Venta oVenta = objcd_Venta.ObtenerVenta(numero);

            if(oVenta.IdVenta != 0)
            {
                List<DetalleVenta> oDetalleVenta = objcd_Venta.ObtenerDetalleVenta(oVenta.IdVenta);
                oVenta.oDetalleVenta = oDetalleVenta;
            }

            return oVenta;
        }

    }
}
