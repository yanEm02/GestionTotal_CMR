using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Sub_Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using PdfiumViewer;
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

namespace CapaPresentacion
{
    public partial class frmDetalleCompra : Form
    {
        private static Usuario usuarioActualFor;
        private Compra compraActual; // Campo para almacenar la compra buscada

        public frmDetalleCompra(Usuario usuarioActual)
        {
            InitializeComponent();
            usuarioActualFor = usuarioActual;

        }

        private void frmDetalleCompra_Load(object sender, EventArgs e)
        {
            txtBusqueda.Focus();

            int rolUsuario = usuarioActualFor == null ? 1 : usuarioActualFor?.oRol?.IdRol ?? 1;
            if (rolUsuario == 2)
            {
                btnEliminarRegistro.Visible = false; // Oculta el botón Eliminar para usuarios estándar
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                MessageBox.Show("Ingrese un numero de compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            compraActual = new CN_Compra().ObtenerCompra(txtBusqueda.Text);

            if (compraActual.IdCompra != 0)
            {
                txtNumeroDocumento.Text = compraActual.NumeroDocumento;
                txtFecha.Text = compraActual.FechaRegistro;
                txtTipoDocumento.Text = compraActual.TipoDocumento;
                txtUsuario.Text = compraActual.oUsuario.Nombre;
                txtDocProveedor.Text = compraActual.oProveedor.Documento;
                txtRazonSocial.Text = compraActual.oProveedor.RazonSocial;

                dgvData.Rows.Clear();
                foreach (DetalleCompra dc in compraActual.oDetalleCompra)
                {
                    dgvData.Rows.Add(new object[] { dc.oProducto.Nombre, dc.PrecioCompra.ToString("N2"), dc.Cantidad, dc.MontoTotal.ToString("N2") });
                }

                txtMontoTotal.Text = compraActual.MontoTotal.ToString("N2");
                txtMontoPendiente.Text = compraActual.MontoPendiente.ToString("N2");
                txtFechaLimitePago.Text = compraActual.FechaLimite;

                if (compraActual.MontoPendiente == 0)
                {
                    txtEstado.Text = "Pagada";
                    txtEstado.BackColor = Color.LightGreen;
                    //btnAbonarMonto.Visible = false;
                }
                else
                {
                    txtEstado.Text = "Pendiente de Pago";
                    txtEstado.BackColor = Color.LightCoral;
                    btnAbonarMonto.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnLimpiarBuscador_Click(sender, e);
                txtBusqueda.Focus();
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocProveedor.Text = "";
            txtRazonSocial.Text = "";
            txtMontoPendiente.Text = "";
            txtFechaLimitePago.Text = "";
            txtEstado.Text = "";
            txtEstado.BackColor = SystemColors.Control;
            btnAbonarMonto.Visible = false;
            compraActual = null;


            dgvData.Rows.Clear();
            txtMontoTotal.Text = "0.00";
            txtBusqueda.Text = "";
            txtBusqueda.Focus();
        }
        private void GenerarPdf(string filePath)
        {
            string textoHtml = Properties.Resources.PlantillaCompraHojaNormal.ToString();
            Negocio oDatos = new CN_Negocio().ObtenerDatos();
            Compra oCompra = new CN_Compra().ObtenerCompra(txtBusqueda.Text);

            textoHtml = textoHtml.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            textoHtml = textoHtml.Replace("@docnegocio", oDatos.Rnc);
            textoHtml = textoHtml.Replace("@direcnegocio", oDatos.Direccion);

            textoHtml = textoHtml.Replace("@tipodocumento", txtTipoDocumento.Text.ToUpper());
            textoHtml = textoHtml.Replace("@numerodocumento", txtNumeroDocumento.Text);

            textoHtml = textoHtml.Replace("@docproveedor", txtDocProveedor.Text);
            textoHtml = textoHtml.Replace("@nombreproveedor", txtRazonSocial.Text);
            textoHtml = textoHtml.Replace("@fecharegistro", txtFecha.Text);
            textoHtml = textoHtml.Replace("@usuarioregistro", txtUsuario.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }

            textoHtml = textoHtml.Replace("@filas", filas);
            textoHtml = textoHtml.Replace("@montototal", txtMontoTotal.Text);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                bool obtenido = true;
                byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);
                if (obtenido)
                {
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                    img.ScaleToFit(60, 60);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;
                    img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.Top - 60);
                    pdfDoc.Add(img);
                }

                using (StringReader sr = new StringReader(textoHtml))
                {
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
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
            saveFile.FileName = string.Format("Compra_{0}.pdf", txtNumeroDocumento.Text);
            saveFile.Filter = "Pdf Files|*.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GenerarPdf(saveFile.FileName);
                    MessageBox.Show("Factura Generada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                // 1. Generar el PDF en la ruta temporal
                GenerarPdf(tempFilePath);

                // 2. Imprimir el PDF usando PdfiumViewer
                using (var document = PdfiumViewer.PdfDocument.Load(tempFilePath))
                {
                    using (var printDocument = document.CreatePrintDocument())
                    {
                        // Imprime en la impresora predeterminada del sistema
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
                // 3. Limpiar el archivo temporal
                if (File.Exists(tempFilePath))
                {
                    try
                    {
                        File.Delete(tempFilePath);
                    }
                    catch
                    {
                        // No es crítico si no se puede borrar, el sistema operativo lo limpiará eventualmente.
                    }
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

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            if(txtNumeroDocumento.Text == "")
            {
                MessageBox.Show("No hay ningun registro para eliminar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (MessageBox.Show("Desea Eliminar el registro de compra?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool respuesta = new CN_Compra().EliminarCompra(txtNumeroDocumento.Text, out string mensaje);
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

        private void btnAbonarMonto_Click(object sender, EventArgs e)
        {
            if (compraActual == null || compraActual.IdCompra == 0)
            {
                MessageBox.Show("Debe buscar una compra primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var subForm = new subFormAbonarPago(compraActual))
            {
                if (subForm.ShowDialog() == DialogResult.OK)
                {
                    // Creamos el objeto para registrar el pago
                    HistorialPagoCompra nuevoPago = new HistorialPagoCompra()
                    {
                        oCompra = new Compra() { IdCompra = compraActual.IdCompra },
                        Cantidad = subForm.MontoAbonado,
                        TipoPago = subForm.TipoPagoSeleccionado
                    };

                    // Llamamos a la capa de negocio para registrar el pago
                    bool exito = new CN_Compra().RegistrarPago(nuevoPago, out string mensaje);

                    if (exito)
                    {
                        MessageBox.Show("Abono registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Volvemos a buscar la compra para refrescar los datos en pantalla
                        btnBuscar_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar el abono: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
