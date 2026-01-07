using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
using PdfiumViewer;

namespace CapaPresentacion
{
    public partial class frmDetalleVentaProcedimiento : Form
    {
        private VentaProcedimiento oVenta;
        private static Usuario usuarioActualFor;

        public frmDetalleVentaProcedimiento(Usuario usuarioActual)
        {
            InitializeComponent();
            usuarioActualFor = usuarioActual;
        }

        private void frmDetalleVentaProcedimiento_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
            int rolUsuario = usuarioActualFor == null ? 1 : usuarioActualFor?.oRol?.IdRol ?? 1;
            if (rolUsuario == 2)
            {
                btnEliminarRegistro.Visible = false; // Oculta el botón Eliminar para usuarios estándar
            }
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
            txtBusqueda.Text = "";
            txtBusqueda.Select();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            oVenta = new CN_Venta_Procedimiento().ObtenerVentaProcedimiento(txtBusqueda.Text);

            if (oVenta != null && oVenta.Id_venta != 0) //confirmamos que se haya recibido una venta 
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
                    dgvData.Rows.Add(new object[] { dv.oProcedimiento.Codigo, dv.oProcedimiento.Nombre, dv.oProcedimiento.oCategoria.Descripcion, dv.PrecioVenta.ToString("N2") });
                }

                txtMontoTotal.Text = oVenta.MontoTotal.ToString("N2");
                txtMontoPago.Text = oVenta.MontoPago.ToString("N2");
                txtMontoCambio.Text = oVenta.MontoCambio.ToString("N2");
            }
            else
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBusqueda.Select();
            }
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            if (txtNumeroDocumento.Text == "")
            {
                MessageBox.Show("No hay ningun registro para eliminar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (MessageBox.Show("Desea Eliminar el registro de la venta de procedimiento?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool respuesta = new CN_Venta_Procedimiento().EliminarVentaProcedimiento(txtNumeroDocumento.Text, out string mensaje);
                    if (respuesta)
                    {
                        MessageBox.Show("Registro eliminado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBusqueda.Text = "";
                        btnLimpiarBuscador_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void GenerarPdfVentaProcedimiento(string filePath)
        {
            string textoHtml = Properties.Resources.PlantillaVentaProcedimiento.ToString();
            Negocio oDatos = new CN_Negocio().ObtenerDatos();

            textoHtml = textoHtml.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            textoHtml = textoHtml.Replace("@docnegocio", oDatos.Rnc);
            textoHtml = textoHtml.Replace("@direcnegocio", oDatos.Direccion);
            textoHtml = textoHtml.Replace("@telefonoEmpresa", oDatos.Telefono);

            textoHtml = textoHtml.Replace("@tipodocumento", txtTipoDocumento.Text.ToUpper());
            textoHtml = textoHtml.Replace("@numerodocumento", txtNumeroDocumento.Text);

            textoHtml = textoHtml.Replace("@nombreCliente", oVenta.oCliente.Nombre);
            textoHtml = textoHtml.Replace("@edadCliente", oVenta.oCliente.Edad.ToString());
            textoHtml = textoHtml.Replace("@sexoCliente", oVenta.oCliente.Sexo);
            textoHtml = textoHtml.Replace("@telefonoCliente", oVenta.oCliente.Telefono);
            textoHtml = textoHtml.Replace("@direccionCliente", oVenta.oCliente.Direccion);
            textoHtml = textoHtml.Replace("@fecharegistro", txtFecha.Text);
            textoHtml = textoHtml.Replace("@usuarioregistro", txtUsuario.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td class='left'>" + row.Cells["Procedimiento"].Value.ToString() + "</td>";
                filas += "<td class='right'>" + row.Cells["Precio"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            textoHtml = textoHtml.Replace("@filas", filas);
            textoHtml = textoHtml.Replace("@montototal", txtMontoTotal.Text);
            textoHtml = textoHtml.Replace("@pagocon", txtMontoPago.Text);
            textoHtml = textoHtml.Replace("@cambio", txtMontoCambio.Text);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                var anchoRecibo = 226.77f; // 80mm en puntos
                Document pdfDoc = new Document(new iTextSharp.text.Rectangle(0, 0, anchoRecibo, 842), 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                bool obtenido = true;
                byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);
                if (obtenido)
                {
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                    img.ScaleToFit(40, 40);
                    img.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(img);
                }

                using (StringReader sr = new StringReader(textoHtml))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                }
                pdfDoc.Close();
            }
        }

        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            if (oVenta == null || txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados para generar el PDF", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("VentaProcedimiento_{0}.pdf", txtNumeroDocumento.Text);
            saveFile.Filter = "Pdf Files|*.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GenerarPdfVentaProcedimiento(saveFile.FileName);
                    MessageBox.Show("Factura Generada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (oVenta == null || txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados para imprimir", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string tempFilePath = Path.GetTempFileName() + ".pdf";

            try
            {
                GenerarPdfVentaProcedimiento(tempFilePath);

                using (var document = PdfiumViewer.PdfDocument.Load(tempFilePath))
                {
                    using (var printDocument = document.CreatePrintDocument())
                    {
                        printDocument.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo imprimir el documento. Error: " + ex.Message, "Error de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    try
                    {
                        File.Delete(tempFilePath);
                    }
                    catch
                    {
                        // No es crítico si no se puede borrar
                    }
                }
            }
        }
    }
}