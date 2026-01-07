using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Sub_Forms;
using CapaPresentacion.Utilidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmVentas : Form
    {
        private Usuario _usuario;
        private Cliente clienteSeleccionado; // para almacenar el objeto del cliente completo

        public frmVentas(Usuario oUsuario = null)
        {
            _usuario = oUsuario;
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, System.EventArgs e)
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
            txtIdProducto.Text = "0";

            CargarNombresClientes();
        }

        private void CargarNombresClientes()
        {
            var listaClientes = new CN_Cliente().Listar().Where(c => c.Estado).ToList();
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            foreach (var cliente in listaClientes)
            {
                source.Add(cliente.Nombre);
            }

            txtDocumentoCliente.AutoCompleteCustomSource = source;
            txtDocumentoCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtDocumentoCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var subForm = new SubFormCliente())
            {
                var result = subForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtDocumentoCliente.Text = subForm._Cliente.Documento.ToString();
                    txtNombre.Text = subForm._Cliente.Nombre;
                    txtCodProducto.Select();
                }
                else
                {
                    txtDocumentoCliente.Select();
                }
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            using (var subForm = new subFrmProducto())
            {
                var result = subForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = subForm._producto.IdProducto.ToString();
                    txtCodProducto.Text = subForm._producto.Codigo;
                    txtProducto.Text = subForm._producto.Nombre;
                    txtPrecio.Text = subForm._producto.PrecioVenta.ToString("N2");
                    txtStock.Text = subForm._producto.Stock.ToString();
                    txtCantidad.Select();
                }
                else
                {
                    txtCodProducto.Select();
                }
            }
        }

        private void txtCodProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {//consultamos de la lista usando una expresion lambda para obtener el producto del codigo que hayamos introduc
                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCodProducto.Text && p.Estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtCodProducto.BackColor = Color.Honeydew;
                    txtIdProducto.Text = oProducto.IdProducto.ToString();
                    txtProducto.Text = oProducto.Nombre;
                    txtPrecio.Text = oProducto.PrecioVenta.ToString("N2");
                    txtStock.Text = oProducto.Stock.ToString();
                    txtCantidad.Select();
                }
                else
                {
                    txtCodProducto.BackColor = Color.MistyRose;
                    txtIdProducto.Text = "0";
                    txtProducto.Text = "";
                    txtPrecio.Text = "";
                    txtStock.Text = "";
                    txtCantidad.Value = 1;
                }

            }
        }
        private void CalcularTotal()
        {
            decimal total = 0;
            if (dgvData.Rows.Count > 0) //validamos que hayan registros 
            {
                foreach (DataGridViewRow row in dgvData.Rows)//recorremos los rows para sumar lossubtotal
                {
                    total += Convert.ToDecimal(row.Cells["subTotal"].Value, CultureInfo.CurrentCulture);
                }
            }
            txtTotalPagar.Text = total.ToString("N2");
        }

        private void LimpiarProducto()
        {
            txtIdProducto.Text = "0";
            txtCodProducto.Text = "";
            txtCodProducto.BackColor = Color.White;
            txtProducto.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtCantidad.Value = 1;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool productoExiste = false;

            if (int.Parse(txtIdProducto.Text) == 0) //verificamos que haya un prod sleccionado anets de agreagr
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!decimal.TryParse(txtPrecio.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out precio))
            {
                MessageBox.Show("Precio - Formato moneda Incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecio.Select();
                return;
            }
            if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(txtCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor al stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow fila in dgvData.Rows) //validamos si el producto ya existe
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtIdProducto.Text)
                {
                    productoExiste = true;
                    break;
                }
            }

            if (!productoExiste) //agregamos el producto a la caja de texto
            {
                dgvData.Rows.Add(new object[]
                {
                txtIdProducto.Text,
                txtProducto.Text,
                precio.ToString("N2"),
                txtCantidad.Value.ToString(),
                (txtCantidad.Value * precio).ToString("N2")

                });

                CalcularTotal();
                LimpiarProducto();
                txtCodProducto.Select();
                
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

                e.Graphics.DrawImage(Properties.Resources.delete_24dp_E3E3E3_FILL0_wght400_GRAD0_opsz24, new System.Drawing.Rectangle(x, y, w, h));
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
                    // SE ELIMINAN LAS LLAMADAS A SumarStock
                    dgvData.Rows.RemoveAt(indice);
                    CalcularTotal();
                }
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            char separadorDecimal = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            char separadorMiles = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator);

            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
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

        private void txtPagaCon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calcularCambio();
                e.SuppressKeyPress = true;
            }
        }

        private void btnCrearVenta_Click(object sender, EventArgs e)
        {
            if (txtDocumentoCliente.Text == "") //primero haccemos las validaciones de que proveedor y que haya compras en registro
            {
                MessageBox.Show("Debe Seleccionar un Cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtNombre.Text == "") //primero haccemos las validaciones de que proveedor y que haya compras en registro
            {
                MessageBox.Show("Debe Seleccionar un Cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar los productos en la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            decimal pagaCon = Convert.ToDecimal(txtPagaCon.Text, CultureInfo.CurrentCulture);
            decimal montoTotal = Convert.ToDecimal(txtTotalPagar.Text, CultureInfo.CurrentCulture);

            if (pagaCon < montoTotal)
            {
                MessageBox.Show("El monto con el que paga el cliente no puede ser menor al total a pagar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable detalle_venta = new DataTable(); //creamos el data table

            detalle_venta.Columns.Add("IdProducto", typeof(int));
            detalle_venta.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("SubTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            { //agregamos los valores dentro del datatable
                detalle_venta.Rows.Add(new object[]{
                    Convert.ToInt32(row.Cells["IdProducto"].Value),
                    Convert.ToDecimal(row.Cells["Precio"].Value, CultureInfo.CurrentCulture),
                    Convert.ToInt32(row.Cells["Cantidad"].Value),
                    Convert.ToDecimal(row.Cells["SubTotal"].Value, CultureInfo.CurrentCulture),

                });

            }

            int idCorrelativo = new CN_Venta().ObtenerCorrelativo(); //generamos el numero de compra aleatorio
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

            calcularCambio(); //calculamos el cambio, para que actualize antes de r ealizar la venta

            Venta oVenta = new Venta() //insertamos en la tabla venta
            {
                oUsuario = new Usuario() { IdUsuario = _usuario.IdUsuario },
                TipoDocumento = ((OpcionCombo)cmbTipoDocumento.SelectedItem).Texto,
                NumeroDocumento = numeroDocumento,
                DocumentoCliente = txtDocumentoCliente.Text,
                NombreCliente = txtNombre.Text,
                MontoPago = Convert.ToDecimal(txtPagaCon.Text, CultureInfo.CurrentCulture),
                MontoCambio = Convert.ToDecimal(txtCambio.Text, CultureInfo.CurrentCulture),
                MontoTotal = Convert.ToDecimal(txtTotalPagar.Text, CultureInfo.CurrentCulture),
            };

            //creamos la variable para guardar el mesanje del metodo almacenado
            string mensaje = string.Empty;
            bool respuesta = new CN_Venta().Registrar(oVenta,detalle_venta, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Numero de venta generada:\n" + numeroDocumento, "Mensaje", MessageBoxButtons.OK);

                // ***** INICIO: NUEVA LLAMADA *****
                // Llamamos a la función para generar e imprimir la factura
                GenerarEImprimirFactura(oVenta, detalle_venta);
                // ***** FIN: NUEVA LLAMADA *****

                txtDocumentoCliente.Text = "0";
                txtNombre.Text = "";
                dgvData.Rows.Clear();
                txtPagaCon.Text = "";
                txtCambio.Text = "";
                CalcularTotal();


            }
            else {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void GenerarEImprimirFactura(Venta oVenta, DataTable detalleVenta)
        {
            try
            {
                string textoHtml = Properties.Resources.PlantillaVenta.ToString();
                Negocio oDatos = new CN_Negocio().ObtenerDatos();

                // Rellenar la plantilla con los datos del negocio y de la venta
                textoHtml = textoHtml.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
                textoHtml = textoHtml.Replace("@docnegocio", oDatos.Rnc);
                textoHtml = textoHtml.Replace("@direcnegocio", oDatos.Direccion);
                textoHtml = textoHtml.Replace("@telefonoEmpresa", oDatos.Telefono);

                textoHtml = textoHtml.Replace("@tipodocumento", oVenta.TipoDocumento.ToUpper());
                textoHtml = textoHtml.Replace("@numerodocumento", oVenta.NumeroDocumento);

                textoHtml = textoHtml.Replace("@doccliente", oVenta.DocumentoCliente);
                textoHtml = textoHtml.Replace("@nombrecliente", oVenta.NombreCliente);
                textoHtml = textoHtml.Replace("@fecharegistro", DateTime.Now.ToString("dd/MM/yyyy"));
                textoHtml = textoHtml.Replace("@usuarioregistro", _usuario.Nombre);

                string filas = string.Empty;
                foreach (DataRow row in detalleVenta.Rows)
                {
                    // Buscamos el nombre del producto usando el IdProducto
                    string nombreProducto = dgvData.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.Cells["IdProducto"].Value.ToString() == row["IdProducto"].ToString())
                        ?.Cells["Producto"].Value.ToString() ?? "N/A";

                    filas += "<tr>";
                    filas += "<td>" + nombreProducto + "</td>";
                    filas += "<td>" + Convert.ToDecimal(row["PrecioVenta"]).ToString("N2") + "</td>";
                    filas += "<td>" + row["Cantidad"].ToString() + "</td>";
                    filas += "<td>" + Convert.ToDecimal(row["SubTotal"]).ToString("N2") + "</td>";
                    filas += "</tr>";
                }
                textoHtml = textoHtml.Replace("@filas", filas);
                textoHtml = textoHtml.Replace("@montototal", oVenta.MontoTotal.ToString("N2"));
                textoHtml = textoHtml.Replace("@pagocon", oVenta.MontoPago.ToString("N2"));
                textoHtml = textoHtml.Replace("@cambio", oVenta.MontoCambio.ToString("N2"));

                // Definir la ruta de guardado automático
                string carpetaFacturas = @"C:\FacturasVentas"; // <-- ¡CAMBIA ESTA RUTA POR LA QUE NECESITES!

                //string carpetaFacturas = @"C:\CarpetaFacturas"; 

                if (!Directory.Exists(carpetaFacturas))
                {
                    Directory.CreateDirectory(carpetaFacturas);
                }
                string nombreArchivo = string.Format("Venta_{0}.pdf", oVenta.NumeroDocumento);
                string rutaCompleta = Path.Combine(carpetaFacturas, nombreArchivo);

                using (FileStream stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    var anchoRecibo = iTextSharp.text.Utilities.MillimetersToPoints(80);
                    Document pdfDoc = new Document(new iTextSharp.text.Rectangle(0, 0, anchoRecibo, 842), 10, 10, 10, 10);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Lógica para el logo (asumiendo que ya la tienes en CN_Negocio)
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
                // Es importante notificar si algo falla en la impresión/generación del PDF
                MessageBox.Show("La venta fue registrada, pero ocurrió un error al generar o imprimir la factura:\n" + ex.Message, "Error de Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDocumentoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            // Busca el cliente por NOMBRE en la lista obtenida de la base de datos
            Cliente oCliente = new CN_Cliente().Listar()
                .FirstOrDefault(c => c.Nombre.Equals(txtDocumentoCliente.Text, StringComparison.OrdinalIgnoreCase) && c.Estado == true);

            if (oCliente != null)
            {
                // Si se encuentra, almacena el objeto y rellena los campos
                clienteSeleccionado = oCliente;
                txtDocumentoCliente.Text = oCliente.Documento;
                txtNombre.Text = oCliente.Nombre;
                txtProducto.Select(); // Mueve el foco al siguiente campo
            }
            else
            {
                // Si no se encuentra, informa al usuario
                MessageBox.Show("Cliente no encontrado o inactivo.", "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clienteSeleccionado = null;
                txtNombre.Text = "";
                txtDocumentoCliente.SelectAll();
            }
            e.SuppressKeyPress = true; // Evita el sonido de "ding"
        }
    }
}
