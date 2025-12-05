using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace CapaPresentacion
{
    public partial class frmDetalleVentaProcedimiento : Form
    {
        public frmDetalleVentaProcedimiento()
        {
            InitializeComponent();
        }

        private void frmDetalleVentaProcedimiento_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuario.Text = "";
            txtNumDocumento.Text = "";
            txtNombreCliente.Text = "";

            dgvData.Rows.Clear();
            txtMontoTotal.Text = "";
            txtMontoPago.Text = "";
            txtMontoCambio.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            VentaProcedimiento oVenta = new CN_Venta_Procedimiento().ObtenerVentaProcedimiento(txtBusqueda.Text);


            if (oVenta.Id_venta != 0) //confirmamos que se haya recibido una venta 
            {
                txtNumeroDocumento.Text = oVenta.NumeroDocumento;

                txtFecha.Text = oVenta.FechaRegistro;
                txtTipoDocumento.Text = oVenta.TipoDocumento;
                txtUsuario.Text = oVenta.oUsuario.Nombre;

                txtNumDocumento.Text = oVenta.oCliente.Documento;
                txtNombreCliente.Text = oVenta.oCliente.Nombre;

                dgvData.Rows.Clear();
                foreach (DetalleVentaProcedimiento dv in oVenta.oDetalleVentaProcedimiento)
                {
                    dgvData.Rows.Add(new object[] { dv.oProcedimiento.Codigo,dv.oProcedimiento.Nombre,dv.oProcedimiento.oCategoria.Descripcion, dv.PrecioVenta });
                }

                txtMontoTotal.Text = oVenta.MontoTotal.ToString("0.00");
                txtMontoPago.Text = oVenta.MontoPago.ToString("0.00");
                txtMontoCambio.Text = oVenta.MontoCambio.ToString("0.00");

            }
        }
    }
}
