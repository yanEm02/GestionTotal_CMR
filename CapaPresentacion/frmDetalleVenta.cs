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

        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            if (txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string textoHtml = Properties.Resources.PlantillaVenta.ToString();
            Negocio oDatos = new CN_Negocio().ObtenerDatos();

            //ACA REEMPLAZAMOS EL TEXTO DE LA PLANTILLA HTML CON INFO DEL FORMULARIO DE LA COMPRA
            textoHtml = textoHtml.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            textoHtml = textoHtml.Replace("@docnegocio", oDatos.Rnc);
            textoHtml = textoHtml.Replace("@direcnegocio", oDatos.Direccion);
            textoHtml = textoHtml.Replace("@telefonoEmpresa", oDatos.Telefono);

            textoHtml = textoHtml.Replace("@tipodocumento", txtTipoDocumento.Text.ToUpper());
            textoHtml = textoHtml.Replace("@numerodocumento", txtNumeroDocumento.Text);

            textoHtml = textoHtml.Replace("@doccliente", txtNumDocumento.Text);
            textoHtml = textoHtml.Replace("@nombrecliente", txtNombreCliente.Text);
            //textoHtml = textoHtml.Replace("@telefonoCliente", oVenta.oCliente.Telefono);
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

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Venta_{0}.pdf", txtNumeroDocumento.Text);
            saveFile.Filter = "Pdf Files|*.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFile.FileName; // Guardamos la ruta del archivo
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    // Definimos solo el ancho del recibo y márgenes pequeños.
                    var anchoRecibo = Utilities.MillimetersToPoints(80);
                    Document pdfDoc = new Document(new iTextSharp.text.Rectangle(0, 0, anchoRecibo, 842), 10, 10, 10, 10);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream); //creamos el pdf con el pdfwriter
                    pdfDoc.Open();

                    // --- INICIO: Cargar y registrar la fuente OCR-B ---
                    string tempFontFile = null;
                    try
                    {
                        // La ruta del recurso es "NombreDelProyecto.NombreDeLaCarpeta.NombreDelArchivo"
                        string fontResourcePath = "CapaPresentacion.Resources.OCRB_Regular.ttf";
                        using (Stream fontStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(fontResourcePath))
                        {
                            if (fontStream != null)
                            {
                                byte[] fontBytes = new byte[fontStream.Length];
                                fontStream.Read(fontBytes, 0, (int)fontStream.Length);

                                // Crear un archivo temporal para la fuente
                                tempFontFile = Path.GetTempFileName();
                                File.WriteAllBytes(tempFontFile, fontBytes);

                                // Registrar la fuente usando la ruta del archivo temporal
                                FontFactory.Register(tempFontFile, "OCRB");
                            }
                            else
                            {
                                // Si no se encuentra la fuente, se usará una por defecto. Opcional: mostrar un mensaje.
                                MessageBox.Show("Advertencia: No se pudo cargar la fuente OCR-B. Se usará una fuente por defecto.", "Fuente no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar la fuente: " + ex.Message, "Error de Fuente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // --- FIN: Cargar y registrar la fuente OCR-B ---

                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);

                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(40, 40); // Ajustar tamaño del logo
                        img.Alignment = Element.ALIGN_CENTER; // Centrar el logo
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(textoHtml))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }
                    pdfDoc.Close();
                    stream.Close(); //cerramos pdf y archivo de memoria
                    MessageBox.Show("Factura Generada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar el archivo temporal de la fuente después de usarlo
                    if (tempFontFile != null && File.Exists(tempFontFile))
                    {
                        try
                        {
                            File.Delete(tempFontFile);
                        }
                        catch
                        {
                            // No hacer nada si no se puede borrar, es un archivo temporal.
                        }
                    }
                }

                // Iniciar el proceso de impresión directo con PdfiumViewer
                try
                {
                    using (var document = PdfiumViewer.PdfDocument.Load(filePath))
                    {
                        using (var printDocument = document.CreatePrintDocument())
                        {
                            // Opcional: puedes especificar una impresora por su nombre
                            // printDocument.PrinterSettings.PrinterName = "NombreDeTuImpresora";

                            // Imprime en la impresora predeterminada del sistema
                            printDocument.Print();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo imprimir el documento. Error: " + ex.Message, "Error de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }  
}
