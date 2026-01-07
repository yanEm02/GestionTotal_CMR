using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
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
using iTextSharpTextRectangle = iTextSharp.text.Rectangle;

namespace CapaPresentacion
{
    public partial class frmReporteProcedimiento : Form
    {
        public frmReporteProcedimiento()
        {
            InitializeComponent();
        }

        private void frmReporteProcedimiento_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                cmbFiltro.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cmbFiltro.DisplayMember = "Texto";
            cmbFiltro.ValueMember = "Value";
            cmbFiltro.SelectedIndex = 0;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            List<ReporteProcedimiento> lista = new List<ReporteProcedimiento>();

            lista = new CN_Reporte().Procedimiento(txtFechaInicio.Value.ToString(), txtFechaFin.Value.ToString());

            dgvData.Rows.Clear();
            foreach (ReporteProcedimiento item in lista)
            {
                dgvData.Rows.Add(new object[] {
                    item.FechaRegistro,
                    item.TipoDocumento,
                    item.NumeroDocumento,
                    item.UsuarioRegistro,
                    item.NombreCliente,
                    item.CodigoProcedimiento,
                    item.NombreProcedimiento,
                    item.Categoria,
                    item.PrecioVenta, // Se pasa el valor decimal directamente
                    item.MontoTotal,   // Se pasa el valor decimal directamente
                });
            }

            // Formatear las celdas de moneda después de agregar los datos
            dgvData.Columns["Precio_Venta"].DefaultCellStyle.Format = "N2";
            dgvData.Columns["MontoTotal"].DefaultCellStyle.Format = "N2";

            CalcularYMostrarTotales();
        }

        private void CalcularYMostrarTotales()
        {
            var ventasUnicas = new HashSet<string>();
            decimal montoTotalGeneral = 0;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.Visible)
                {
                    string numeroDocumento = row.Cells["NumeroDocumento"].Value.ToString();

                    if (ventasUnicas.Add(numeroDocumento))
                    {
                        montoTotalGeneral += Convert.ToDecimal(row.Cells["MontoTotal"].Value);
                    }
                }
            }

            int totalVentas = ventasUnicas.Count;

            // Asumiendo que los TextBox se llaman txtTotalVentaProcedimiento y txtMontoTotalGeneral
            txtTotalVentaProcedimiento.Text = totalVentas.ToString();
            txtMontoTotalGeneral.Text = montoTotalGeneral.ToString("N2");
        }

        private void btnBuscarPor_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbFiltro.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    //hacemos el filtro con un foreach, limpiando los espacios y conviertiendo a mayus
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
            CalcularYMostrarTotales(); // Recalcular totales después de filtrar
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
            CalcularYMostrarTotales(); // Recalcular totales al limpiar filtro
        }

        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvData.Columns) //agregamos las columnas
                {
                    dt.Columns.Add(columna.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow fila in dgvData.Rows)//agregamos las filas
                {
                    if (fila.Visible)
                    {
                        dt.Rows.Add(new object[]
                        {
                            fila.Cells[0].Value.ToString(),
                            fila.Cells[1].Value.ToString(),
                            fila.Cells[2].Value.ToString(),
                            fila.Cells[3].Value.ToString(),
                            fila.Cells[4].Value.ToString(),
                            fila.Cells[5].Value.ToString(),
                            fila.Cells[6].Value.ToString(),
                            fila.Cells[7].Value.ToString(),
                            Convert.ToDecimal(fila.Cells[8].Value).ToString("N2"),
                            Convert.ToDecimal(fila.Cells[9].Value).ToString("N2"),
                        });
                    }
                }

                //instanciamos el savefiledialog
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = string.Format("ReporteProcedimientos_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                sfd.Filter = "Excel Files | *.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.Columns().AdjustToContents(); //ajustamos el ancho de las columnas
                        wb.SaveAs(sfd.FileName); //guardamos el archivo en la ruta seleccionada
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("No se ha seleccionado un archivo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }

                }

            }
        }

        private void btnImprimirResumen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTotalVentaProcedimiento.Text) || txtTotalVentaProcedimiento.Text == "0")
            {
                MessageBox.Show("No hay datos para generar un resumen.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string textoHtml = Properties.Resources.PlantillaResumenProcedimiento.ToString();
                Negocio oDatos = new CN_Negocio().ObtenerDatos();

                // Rellenar la plantilla con los datos del negocio y del resumen
                textoHtml = textoHtml.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
                textoHtml = textoHtml.Replace("@docnegocio", oDatos.Rnc);
                textoHtml = textoHtml.Replace("@direcnegocio", oDatos.Direccion);

                textoHtml = textoHtml.Replace("@fechaInicio", txtFechaInicio.Value.ToString("dd/MM/yyyy"));
                textoHtml = textoHtml.Replace("@fechaFin", txtFechaFin.Value.ToString("dd/MM/yyyy"));
                textoHtml = textoHtml.Replace("@totalVentas", txtTotalVentaProcedimiento.Text);
                textoHtml = textoHtml.Replace("@montoTotal", txtMontoTotalGeneral.Text);

                // Guardar el PDF en una ruta temporal
                string rutaCompleta = Path.Combine(Path.GetTempPath(), string.Format("Resumen_{0}.pdf", DateTime.Now.ToString("yyyyMMddHHmmss")));

                using (FileStream stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    // Usamos un tamaño de papel de recibo estándar (80mm de ancho)
                    var anchoRecibo = iTextSharp.text.Utilities.MillimetersToPoints(80);
                    Document pdfDoc = new Document(new iTextSharpTextRectangle(0, 0, anchoRecibo, 842), 10, 10, 10, 10);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    using (StringReader sr = new StringReader(textoHtml))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }
                    pdfDoc.Close();
                }

                // Imprimir el PDF recién creado sin diálogo
                using (var document = PdfiumViewer.PdfDocument.Load(rutaCompleta))
                {
                    using (var printDocument = document.CreatePrintDocument())
                    {
                        printDocument.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar o imprimir el resumen:\n" + ex.Message, "Error de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
