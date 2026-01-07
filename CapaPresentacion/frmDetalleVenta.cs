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
using PdfiumViewer;

namespace CapaPresentacion
{
    public partial class frmDetalleVenta : Form
    {
        private Venta oVenta;
        private static Usuario usuarioActualFor;
        public frmDetalleVenta(Usuario usuarioActual)
        {
            InitializeComponent();
            usuarioActualFor = usuarioActual;
        }

        private void frmDetalleVenta_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
            int rolUsuario = usuarioActualFor == null ? 1 : usuarioActualFor?.oRol?.IdRol ?? 1;
            if (rolUsuario == 2)
            {
                btnEliminarRegistro.Visible = false; // Oculta el botón Eliminar para usuarios estándar
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Venta oVenta = new CN_Venta().ObtenerVenta(txtBusqueda.Text);

            if(oVenta.IdVenta != 0) //confirmamos que se haya recibido una venta 
            {
                txtNumeroDocumento.Text = oVenta.NumeroDocumento;

                txtFecha.Text = oVenta.FechaRegistro;
                txtTipoDocumento.Text = oVenta.TipoDocumento;
                txtUsuario.Text = oVenta.oUsuario.Nombre;

                txtNumDocumento.Text = oVenta.DocumentoCliente;
                txtNombreCliente.Text = oVenta.NombreCliente;

                dgvData.Rows.Clear();
                foreach(DetalleVenta dv in oVenta.oDetalleVenta)
                {
                    dgvData.Rows.Add(new object[] { dv.oProducto.Nombre, dv.PrecioVenta.ToString("N2"), dv.Cantidad, dv.SubTotal.ToString("N2") });
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

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            if (txtNumeroDocumento.Text == "")
            {
                MessageBox.Show("No hay ningun registro para eliminar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (MessageBox.Show("Desea Eliminar el registro de venta?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool respuesta = new CN_Venta().EliminarVenta(txtNumeroDocumento.Text, out string mensaje);
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

        private void GenerarPdfVenta(string filePath)
        {
            string textoHtml = Properties.Resources.PlantillaVenta.ToString();
            Negocio oDatos = new CN_Negocio().ObtenerDatos();

            textoHtml = textoHtml.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            textoHtml = textoHtml.Replace("@docnegocio", oDatos.Rnc);
            textoHtml = textoHtml.Replace("@direcnegocio", oDatos.Direccion);
            textoHtml = textoHtml.Replace("@telefonoEmpresa", oDatos.Telefono);

            textoHtml = textoHtml.Replace("@tipodocumento", txtTipoDocumento.Text.ToUpper());
            textoHtml = textoHtml.Replace("@numerodocumento", txtNumeroDocumento.Text);

            textoHtml = textoHtml.Replace("@doccliente", txtNumDocumento.Text);
            textoHtml = textoHtml.Replace("@nombrecliente", txtNombreCliente.Text);
            textoHtml = textoHtml.Replace("@fecharegistro", txtFecha.Text);
            textoHtml = textoHtml.Replace("@usuarioregistro", txtUsuario.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["PrecioVenta"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
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
            if (txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Venta_{0}.pdf", txtNumeroDocumento.Text);
            saveFile.Filter = "Pdf Files|*.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GenerarPdfVenta(saveFile.FileName);
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
            if (txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados para imprimir", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string tempFilePath = Path.GetTempFileName() + ".pdf";

            try
            {
                GenerarPdfVenta(tempFilePath);

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
