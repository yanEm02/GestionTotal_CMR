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
using Color = System.Drawing.Color;

namespace CapaPresentacion
{
    public partial class frmCompras : Form
    {
        //para almacenar el usuario que esta logggeado
        private Usuario _usuario;
        public frmCompras(Usuario oUsuario = null)
        {
            _usuario = oUsuario;
            InitializeComponent();
        }

        private void frmCompras_Load(object sender, System.EventArgs e)
        {
            cmbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Efectivo", Texto = "Efectivo" });
            cmbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Tarjeta", Texto = "Tarjeta" });
            cmbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Transferencia", Texto = "Transferencia" });
            cmbTipoDocumento.DisplayMember = "Texto";
            cmbTipoDocumento.ValueMember = "Valor";
            cmbTipoDocumento.SelectedIndex = 0;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtIdProveedor.Text = "0";
            txtIdProducto.Text = "0";
        }

        //aca agarramos el provvedor una vez seleccionado a traves del sub formularioo 
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var subForm = new subFrmProveedor())
            {
                var result = subForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdProveedor.Text = subForm._proveedor.IdProveedor.ToString();
                    txtDocumentoProveedor.Text = subForm._proveedor.Documento;
                    txtNombre.Text = subForm._proveedor.RazonSocial;
                }
                else
                {
                    txtDocumentoProveedor.Select();
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
                    txtPrecioCompra.Select(); 
                }
                else
                {
                    txtCodProducto.Select();
                }
            }
        }

        //funcion para que busque el producto una vez presionado enter luego de haber introducido el codigo 
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
                    txtPrecioCompra.Select();
                }
                else
                {
                    txtCodProducto.BackColor = Color.MistyRose;
                    txtIdProducto.Text = "0";
                    txtProducto.Text = "";
                }

            }

        }

        //agregando los productos o registrando la compra
        private void iconButton1_Click(object sender, EventArgs e)
        {
            decimal precioCompra = 0;
            decimal precioVenta = 0;
            bool productoExiste = false;

            int idProducto;
            if (!int.TryParse(txtIdProducto.Text, out idProducto) || idProducto == 0) //verificamos que haya un prod seleccionado antes de agregar
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!decimal.TryParse(txtPrecioCompra.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out precioCompra))
            {
                MessageBox.Show("Precio Compra - Formato moneda Incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecioCompra.Select();
                return; // <-- And this
            }
            if (!decimal.TryParse(txtPrecioVenta.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out precioVenta))
            {
                MessageBox.Show("Precio Venta - Formato moneda Incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecioVenta.Select();
                return; // <-- And this
            }
            if (precioVenta <= precioCompra)
            {
                MessageBox.Show("El precio de venta debe ser mayor al precio de compra.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    precioCompra.ToString("N2"),
                    precioVenta.ToString("N2"),
                    txtCantidad.Value,
                    (txtCantidad.Value * precioCompra).ToString("N2")

                });
            }
            CalcularTotal();
            LimpiarProducto();
            txtCodProducto.Select();

        }

        private void LimpiarProducto()
        {
            txtIdProducto.Text = "0";
            txtCodProducto.Text = string.Empty;
            txtCodProducto.BackColor = Color.White;
            txtProducto.Text = string.Empty;
            txtPrecioCompra.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            txtCantidad.Value = 1;
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

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 6)
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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e) //configuramos el boton elimar de las filas 
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    dgvData.Rows.RemoveAt(indice);
                    CalcularTotal();

                }
            }
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e) //ajustamos para poder controlar lo que introducimos en el campo de los precios 
        {
            char separadorDecimal = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            char separadorMiles = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator);

            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == separadorDecimal && txtPrecioCompra.Text.IndexOf(separadorDecimal) == -1)
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

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            char separadorDecimal = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            char separadorMiles = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator);

            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == separadorDecimal && txtPrecioVenta.Text.IndexOf(separadorDecimal) == -1)
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


        //configuramos el boton de registrar 
        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdProveedor.Text) == 0) //primero haccemos las validaciones de que proveedor y que haya compras en registro
            {
                MessageBox.Show("Debe Seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar los productos en la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            decimal montoTotal;
            if (string.IsNullOrWhiteSpace(txtTotalPagar.Text) || !decimal.TryParse(txtTotalPagar.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out montoTotal))
            {
                MessageBox.Show("El monto total debe ser un número válido.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            DataTable detalle_compra = new DataTable(); //creamos el data table

            detalle_compra.Columns.Add("IdProducto", typeof(int));
            detalle_compra.Columns.Add("PrecioCompra", typeof(decimal));
            detalle_compra.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_compra.Columns.Add("Cantidad", typeof(int));
            detalle_compra.Columns.Add("SubTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            { //agregamos los valores dentro del datatable
                detalle_compra.Rows.Add(
                new object[]
                {
                    Convert.ToInt32(row.Cells["IdProducto"].Value),
                    Convert.ToDecimal(row.Cells["PrecioCompra"].Value, CultureInfo.CurrentCulture),
                    Convert.ToDecimal(row.Cells["PrecioVenta"].Value, CultureInfo.CurrentCulture),
                    Convert.ToInt32(row.Cells["Cantidad"].Value),
                    Convert.ToDecimal(row.Cells["SubTotal"].Value, CultureInfo.CurrentCulture),

                });

            }

            int idCorrelativo = new CN_Compra().ObtenerCorrelativo(); //generamos el numero de compra aleatorio
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

            Compra oCompra = new Compra()
            {
                oUsuario = new Usuario() { IdUsuario = _usuario.IdUsuario },
                oProveedor = new Proveedor() { IdProveedor = Convert.ToInt32(txtIdProveedor.Text) },
                TipoDocumento = ((OpcionCombo)cmbTipoDocumento.SelectedItem).Texto,
                NumeroDocumento = numeroDocumento,
                MontoTotal = montoTotal,
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Compra().Registrar(oCompra, detalle_compra, out mensaje); //hacemos el registro en la BD

            if (respuesta)
            {
                var result = MessageBox.Show("Numero de compra generada:\n" + numeroDocumento + "\n\nDesea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(numeroDocumento);
                }

                //GenerarEImprimirRecibo(oCompra, detalle_compra);

                txtIdProveedor.Text = "0";
                txtDocumentoProveedor.Text = "";
                txtNombre.Text = "";
                dgvData.Rows.Clear();
                CalcularTotal();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void GenerarEImprimirRecibo(Compra oCompra, DataTable detalleCompra)
        {
            try
            {
                string textoHtml = Properties.Resources.PlantillaCompra.ToString();
                Negocio oDatos = new CN_Negocio().ObtenerDatos();

                // Rellenar la plantilla con los datos del negocio y de la compra
                textoHtml = textoHtml.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
                textoHtml = textoHtml.Replace("@docnegocio", oDatos.Rnc);
                textoHtml = textoHtml.Replace("@direcnegocio", oDatos.Direccion);
                textoHtml = textoHtml.Replace("@telefonoEmpresa", oDatos.Telefono);

                textoHtml = textoHtml.Replace("@tipodocumento", oCompra.TipoDocumento.ToUpper());
                textoHtml = textoHtml.Replace("@numerodocumento", oCompra.NumeroDocumento);

                // Usamos los datos del proveedor desde los campos de texto del formulario
                textoHtml = textoHtml.Replace("@docproveedor", txtDocumentoProveedor.Text);
                textoHtml = textoHtml.Replace("@nombreproveedor", txtNombre.Text);
                textoHtml = textoHtml.Replace("@telefonoProveedor", oCompra.oProveedor.Telefono);
                textoHtml = textoHtml.Replace("@fecharegistro", DateTime.Now.ToString("dd/MM/yyyy"));
                textoHtml = textoHtml.Replace("@usuarioregistro", _usuario.Nombre);

                string filas = string.Empty;
                foreach (DataRow row in detalleCompra.Rows)
                {
                    // Buscamos el nombre del producto usando el IdProducto
                    string nombreProducto = dgvData.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.Cells["IdProducto"].Value.ToString() == row["IdProducto"].ToString())
                        ?.Cells["Producto"].Value.ToString() ?? "N/A";

                    filas += "<tr>";
                    filas += "<td>" + nombreProducto + "</td>";
                    filas += "<td>" + Convert.ToDecimal(row["PrecioCompra"]).ToString("N2") + "</td>";
                    filas += "<td>" + row["Cantidad"].ToString() + "</td>";
                    filas += "<td>" + Convert.ToDecimal(row["SubTotal"]).ToString("N2") + "</td>";
                    filas += "</tr>";
                }
                textoHtml = textoHtml.Replace("@filas", filas);
                textoHtml = textoHtml.Replace("@montototal", oCompra.MontoTotal.ToString("N2"));

                // Define aquí la ruta fija donde quieres guardar los recibos de compra.
                string carpetaRecibos = @"C:\Users\yan_e\Downloads"; // <-- ¡CAMBIA ESTA RUTA POR LA QUE NECESITES!

                //string carpetaFacturas = @"C:\CarpetaFacturas"; 

                if (!Directory.Exists(carpetaRecibos))
                {
                    Directory.CreateDirectory(carpetaRecibos);
                }
                string nombreArchivo = string.Format("Compra_{0}.pdf", oCompra.NumeroDocumento);
                string rutaCompleta = Path.Combine(carpetaRecibos, nombreArchivo);

                using (FileStream stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    var anchoRecibo = iTextSharp.text.Utilities.MillimetersToPoints(80);
                    Document pdfDoc = new Document(new iTextSharp.text.Rectangle(0, 0, anchoRecibo, 842), 10, 10, 10, 10);

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
                MessageBox.Show("La compra fue registrada, pero ocurrió un error al generar o imprimir el recibo:\n" + ex.Message, "Error de Recibo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
