using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Sub_Forms;
using CapaPresentacion.Utilidades;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmVentasProcedimientos : Form
    {

        private Usuario usuarioActualFor; //almacenamos el usuario que se ha logeado
        private Cliente clienteSeleccionado; // para almacenar el objeto del cliente completo
        private List<Cliente> listaClientes; // <-- Añadimos una lista para mantener los clientes en memoria

        public frmVentasProcedimientos(Usuario oUsuario)
        {
            usuarioActualFor = oUsuario;
            InitializeComponent();
        }

        private void frmProcedimientos_Load(object sender, EventArgs e)
        {
            cmbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Efectivo", Texto = "Efectivo" });
            cmbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Tarjeta", Texto = "Tarjeta" });
            cmbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Transferencia", Texto = "Transferencia" });
            cmbTipoDocumento.DisplayMember = "Texto";
            cmbTipoDocumento.ValueMember = "Valor";
            cmbTipoDocumento.SelectedIndex = 0;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtPagaCon.Text = "";
            txtCambio.Text = "";
            txtIdProcedimiento.Text = "0";

            CargarNombresProcedimientos(); // <-- Añade esta línea
            CargarNombresClientes();
        }

        //BUSCAR PROCEDIMIENTO 
        private void btnBuscarProcedimiento_Click(object sender, EventArgs e)
        {
            using (var subForm = new subFormProcedimiento())
            {
                var result = subForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdProcedimiento.Text = subForm._procedimiento.ID_Procedimiento.ToString();
                    txtCodigo.Text = subForm._procedimiento.Codigo.ToString();
                    txtProcedimiento.Text = subForm._procedimiento.Nombre;
                    txtCategoria.Text = subForm._procedimiento.oCategoria.Descripcion;
                    if (chkBoxAsegurado.Checked)
                    {
                        txtPrecio.Text = subForm._procedimiento.PrecioVentaAsegurado.ToString("N2");
                    }
                    else
                    {
                        txtPrecio.Text = subForm._procedimiento.PrecioVenta.ToString("N2");
                    }
                }
                else
                {
                    txtCodigo.Select();
                }

            }
        }

        private void CargarNombresProcedimientos()
        {
            var listaProcedimientos = new CN_Procedimiento().Listar().Where(p => p.Estado).ToList();
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            foreach (var procedimiento in listaProcedimientos)
            {
                source.Add(procedimiento.Nombre);
            }

            txtCodigo.AutoCompleteCustomSource = source;
            txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void CargarNombresClientes()
        {
            // Obtenemos los clientes y los guardamos en la lista de la clase
            listaClientes = new CN_Cliente().Listar().Where(c => c.Estado).ToList();
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            foreach (var cliente in listaClientes)
            {
                source.Add(cliente.Nombre);
            }

            txtDocumentoCliente.AutoCompleteCustomSource = source;
            txtDocumentoCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtDocumentoCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        //BUSCAR PACIENTE 
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var subForm = new SubFormCliente())
            {
                var result = subForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    //almacenamos el objeto Cliente completo
                    clienteSeleccionado = subForm._Cliente;

                    // Actualiza los campos de texto como antes
                    txtDocumentoCliente.Text = clienteSeleccionado.Documento.ToString();
                    txtNombre.Text = clienteSeleccionado.Nombre;
                }
                else
                {
                    txtDocumentoCliente.Select();
                }

                // Volvemos a cargar los nombres de los clientes para refrescar la lista de autocompletar
                CargarNombresClientes();
            }
        }

        private void CalcularTotal()
        {
            decimal total = 0;
            if (dgvData.Rows.Count > 0) //validamos que hayan registros 
            {
                foreach (DataGridViewRow row in dgvData.Rows)//recorremos los rows para sumar lossubtotal
                {
                    total += Convert.ToDecimal(row.Cells["Precio"].Value, CultureInfo.CurrentCulture);
                }
            }
            txtTotalPagar.Text = total.ToString("N2");
        }

        private void calcularCambio()
        {
            if (string.IsNullOrWhiteSpace(txtTotalPagar.Text))
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagaCon;
            decimal total = Convert.ToDecimal(txtTotalPagar.Text, CultureInfo.CurrentCulture);

            if (string.IsNullOrWhiteSpace(txtPagaCon.Text))
            {
                txtPagaCon.Text = "0";
            }

            if (decimal.TryParse(txtPagaCon.Text.Trim(), NumberStyles.Currency, CultureInfo.CurrentCulture, out pagaCon))
            {
                if (pagaCon < total)
                {
                    txtCambio.Text = "0.00";
                }
                else
                {
                    decimal cambio = pagaCon - total;
                    txtCambio.Text = cambio.ToString("N2");
                }
            }
        }

        private void LimpiarProducto()
        {
            txtIdProcedimiento.Text = "0";
            txtCodigo.Text = "";
            txtCodigo.BackColor = Color.White;
            txtProcedimiento.Text = "";
            txtPrecio.Text = "";
            txtCategoria.Text = "";
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool procedimientoExiste = false;

            if (int.Parse(txtIdProcedimiento.Text) == 0) //verificamos que haya un prod sleccionado anets de agreagr
            {
                MessageBox.Show("Debe seleccionar un procedimiento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!decimal.TryParse(txtPrecio.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out precio))
            {
                MessageBox.Show("Precio - Formato moneda Incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecio.Select();
                return;
            }
            foreach (DataGridViewRow fila in dgvData.Rows) //validamos si el producto ya existe
            {
                if (fila.Cells["IdProcedimiento"].Value.ToString() == txtIdProcedimiento.Text)
                {
                    procedimientoExiste = true;
                    break;
                }
            }

            if (!procedimientoExiste) //agregamos el procedimiento a la caja de texto
            {
                dgvData.Rows.Add(new object[]{
                        txtIdProcedimiento.Text,
                        txtCodigo.Text,
                        txtProcedimiento.Text,
                        txtCategoria.Text,
                        precio.ToString("N2"),
                    });

                CalcularTotal();
                LimpiarProducto();
                txtCodigo.Select();

            }
        }

        private void txtPagaCon_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calcularCambio();
                e.SuppressKeyPress = true;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            char separadorDecimal = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            char separadorMiles = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator);

            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == separadorDecimal && txtPrecio.Text.IndexOf(separadorDecimal) == -1)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == separadorMiles)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }


        private void dgvData_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 5)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.delete_24dp_E3E3E3_FILL0_wght400_GRAD0_opsz24.Width;
                var h = Properties.Resources.delete_24dp_E3E3E3_FILL0_wght400_GRAD0_opsz24.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete_24dp_E3E3E3_FILL0_wght400_GRAD0_opsz24, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    dgvData.Rows.RemoveAt(indice);
                    CalcularTotal();
                    txtCambio.Text = "";

                }
            }
        }

        private void btnCrearVenta_Click(object sender, EventArgs e)
        {
            // Volvemos a validar el cliente seleccionado contra el texto del formulario
            if (clienteSeleccionado == null || clienteSeleccionado.Nombre != txtNombre.Text)
            {
                // Si no coinciden, intentamos encontrarlo de nuevo.
                clienteSeleccionado = listaClientes.FirstOrDefault(c => c.Nombre == txtNombre.Text && c.Documento == txtDocumentoCliente.Text);
            }

            // Valida usando el objeto en lugar del texto
            if (clienteSeleccionado == null || clienteSeleccionado.IdCliente == 0)
            {
                MessageBox.Show("Debe Seleccionar un Cliente válido. Verifique que el nombre y documento sean correctos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar un procedimiento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal pagaCon = 0;
            decimal.TryParse(txtPagaCon.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out pagaCon);

            if (pagaCon < Convert.ToDecimal(txtTotalPagar.Text, CultureInfo.CurrentCulture))
            {
                MessageBox.Show("El monto con el que paga el cliente no puede ser menor al total a pagar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable detalleVentaProcedimiento = new DataTable();

            detalleVentaProcedimiento.Columns.Add("IdProcedimiento", typeof(int));
            detalleVentaProcedimiento.Columns.Add("PrecioVenta", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            { //agregamos los valores dentro del datatable
                detalleVentaProcedimiento.Rows.Add(new object[]{
                        Convert.ToInt32(row.Cells["IdProcedimiento"].Value.ToString()),
                        Convert.ToDecimal(row.Cells["Precio"].Value, CultureInfo.CurrentCulture),
                    });
            }

            int idCorrelativo = new CN_Venta_Procedimiento().ObtenerCorrelativo(); //generamos el numero de compra aleatorio
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

            calcularCambio();

            VentaProcedimiento objVenta = new VentaProcedimiento()
            {
                oUsuario = new Usuario() { IdUsuario = usuarioActualFor.IdUsuario },
                TipoDocumento = ((OpcionCombo)cmbTipoDocumento.SelectedItem).Valor.ToString(),
                NumeroDocumento = numeroDocumento,
                // Asigna un nuevo objeto Cliente solo con el ID, que es lo que necesita la FK
                oCliente = new Cliente() { IdCliente = this.clienteSeleccionado.IdCliente },
                MontoPago = Convert.ToDecimal(txtPagaCon.Text, CultureInfo.CurrentCulture),
                MontoCambio = Convert.ToDecimal(txtCambio.Text, CultureInfo.CurrentCulture),
                MontoTotal = Convert.ToDecimal(txtTotalPagar.Text, CultureInfo.CurrentCulture),
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Venta_Procedimiento().Registrar(objVenta, detalleVentaProcedimiento, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Numero de venta generada:\n" + numeroDocumento, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Clipboard.SetText(numeroDocumento);

                // Para generar la factura, pasamos el objeto cliente completo que ya teníamos
                GenerarEImprimirFactura(objVenta, detalleVentaProcedimiento, this.clienteSeleccionado);

                LimpiarProducto();
                dgvData.Rows.Clear();
                txtDocumentoCliente.Text = "";
                txtNombre.Text = "";
                txtPagaCon.Text = "";
                txtCambio.Text = "";
                txtTotalPagar.Text = "";
                clienteSeleccionado = null; // <-- Limpia el cliente seleccionado
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void GenerarEImprimirFactura(VentaProcedimiento oVenta, DataTable detalleVenta, Cliente oClienteFactura)
        {
            try
            {
                string textoHtml = Properties.Resources.PlantillaVentaProcedimiento.ToString();
                Negocio oDatos = new CN_Negocio().ObtenerDatos();

                // Rellenar la plantilla con los datos del negocio y de la venta
                textoHtml = textoHtml.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
                textoHtml = textoHtml.Replace("@docnegocio", oDatos.Rnc);
                textoHtml = textoHtml.Replace("@direcnegocio", oDatos.Direccion);
                textoHtml = textoHtml.Replace("@telefonoEmpresa", oDatos.Telefono);

                textoHtml = textoHtml.Replace("@tipodocumento", oVenta.TipoDocumento.ToUpper());
                textoHtml = textoHtml.Replace("@numerodocumento", oVenta.NumeroDocumento);

                // Rellenar datos del cliente desde el objeto completo
                textoHtml = textoHtml.Replace("@nombreCliente", oClienteFactura.Nombre);
                textoHtml = textoHtml.Replace("@edadCliente", oClienteFactura.Edad.ToString());
                textoHtml = textoHtml.Replace("@sexoCliente", oClienteFactura.Sexo);
                textoHtml = textoHtml.Replace("@telefonoCliente", oClienteFactura.Telefono);
                textoHtml = textoHtml.Replace("@direccionCliente", oClienteFactura.Direccion);
                textoHtml = textoHtml.Replace("@fecharegistro", DateTime.Now.ToString("dd/MM/yyyy"));
                textoHtml = textoHtml.Replace("@usuarioregistro", usuarioActualFor.Nombre);

                // Asignar valor a @asegurado según el estado del CheckBox
                string tipoPaciente = chkBoxAsegurado.Checked ? "Asegurado" : "Privado";
                textoHtml = textoHtml.Replace("@asegurado", tipoPaciente);

                string filas = string.Empty;
                foreach (DataRow row in detalleVenta.Rows)
                {
                    // Buscamos el nombre del procedimiento usando el IdProcedimiento
                    string nombreProcedimiento = dgvData.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.Cells["IdProcedimiento"].Value.ToString() == row["IdProcedimiento"].ToString())
                        ?.Cells["Nombre"].Value.ToString() ?? "N/A";

                    filas += "<tr>";
                    filas += "<td class='left'>" + nombreProcedimiento + "</td>";
                    filas += "<td class='right'>" + Convert.ToDecimal(row["PrecioVenta"]).ToString("N2") + "</td>";
                    filas += "</tr>";
                }
                textoHtml = textoHtml.Replace("@filas", filas);
                textoHtml = textoHtml.Replace("@montototal", oVenta.MontoTotal.ToString("N2"));
                textoHtml = textoHtml.Replace("@pagocon", oVenta.MontoPago.ToString("N2"));
                textoHtml = textoHtml.Replace("@cambio", oVenta.MontoCambio.ToString("N2"));

                // Define aquí la ruta fija donde quieres guardar las facturas de procedimientos.
                string carpetaFacturas = @"C:\FacturasVentaProcedimientos"; // <-- ¡CAMBIA ESTA RUTA POR LA QUE NECESITES!
                                                                            //string carpetaFacturas = @"C:\CarpetaFacturas"; 

                if (!Directory.Exists(carpetaFacturas))
                {
                    Directory.CreateDirectory(carpetaFacturas);
                }
                string nombreArchivo = string.Format("VentaProcedimiento_{0}.pdf", oVenta.NumeroDocumento);
                string rutaCompleta = Path.Combine(carpetaFacturas, nombreArchivo);

                using (FileStream stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    var anchoRecibo = iTextSharp.text.Utilities.MillimetersToPoints(80);
                    iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(0, 0, anchoRecibo, 842), 10, 10, 10, 10);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);
                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(40, 40);
                        img.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(textoHtml))
                    {
                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }
                    pdfDoc.Close();
                }

                // Imprimir el PDF recién creado
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
                MessageBox.Show("La venta fue registrada, pero ocurrió un error al generar o imprimir la factura:\n" + ex.Message, "Error de Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodigo_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Busca el procedimiento por NOMBRE en la lista obtenida de la base de datos
                Procedimiento oProcedimiento = new CN_Procedimiento().Listar()
                    .FirstOrDefault(p => p.Nombre.Equals(txtCodigo.Text, StringComparison.OrdinalIgnoreCase) && p.Estado == true);

                if (oProcedimiento != null)
                {
                    // Si se encuentra, rellena los campos
                    txtIdProcedimiento.Text = oProcedimiento.ID_Procedimiento.ToString();
                    txtCodigo.Text = oProcedimiento.Codigo.ToString(); // Opcional: mostrar el código después de seleccionar
                    txtProcedimiento.Text = oProcedimiento.Nombre;
                    txtCategoria.Text = oProcedimiento.oCategoria.Descripcion;

                    // Aplica la lógica del precio según si es asegurado o no
                    if (chkBoxAsegurado.Checked)
                    {
                        txtPrecio.Text = oProcedimiento.PrecioVentaAsegurado.ToString("N2");
                    }
                    else
                    {
                        txtPrecio.Text = oProcedimiento.PrecioVenta.ToString("N2");
                    }
                    // Mueve el foco al botón de agregar para agilizar el siguiente paso
                    iconButton1.Select();
                }
                else
                {
                    // Si no se encuentra, informa al usuario y limpia los campos
                    MessageBox.Show("Procedimiento no encontrado o inactivo.", "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIdProcedimiento.Text = "0";
                    txtProcedimiento.Text = "";
                    txtPrecio.Text = "";
                    txtCategoria.Text = "";
                    txtCodigo.SelectAll();
                }
                // Evita que el sonido de "ding" de Windows suene al presionar Enter
                e.SuppressKeyPress = true;
            }
        }

        private void txtDocumentoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Busca todos los clientes que coincidan con el nombre y estén activos, usando la lista en memoria.
                var clientesEncontrados = listaClientes
                    .Where(c => c.Nombre.Equals(txtDocumentoCliente.Text, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (clientesEncontrados.Count == 1)
                {
                    // Si solo hay un resultado, lo seleccionamos automáticamente.
                    clienteSeleccionado = clientesEncontrados[0];
                    txtDocumentoCliente.Text = clienteSeleccionado.Documento;
                    txtNombre.Text = clienteSeleccionado.Nombre;
                    txtCodigo.Select(); // Mueve el foco al siguiente campo.
                }
                else if (clientesEncontrados.Count > 1)
                {
                    // Si hay múltiples resultados, abrimos el formulario de búsqueda para que el usuario elija.
                    MessageBox.Show("Se encontraron varios clientes con el mismo nombre. Por favor, seleccione el correcto de la lista.", "Múltiples coincidencias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBuscar_Click(sender, e); // Reutilizamos la lógica del botón de búsqueda.
                }
                else
                {
                    // Si no se encuentra ningún cliente.
                    MessageBox.Show("Cliente no encontrado o inactivo.", "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    clienteSeleccionado = null;
                    txtNombre.Text = "";
                    txtDocumentoCliente.SelectAll();
                }
                e.SuppressKeyPress = true; // Evita el sonido de "ding".
            }
        }

        private void chkBoxAsegurado_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtPagaCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            char separadorDecimal = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            char separadorMiles = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator);

            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == separadorDecimal && txtPagaCon.Text.IndexOf(separadorDecimal) == -1)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == separadorMiles)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
