using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Sub_Forms;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmProcedimientos : Form
    {

        private static Usuario usuarioActualFor; //almacenamos el usuario que se ha logeado
        public frmProcedimientos(Usuario usuarioActual)
        {
            usuarioActualFor = usuarioActual;
            InitializeComponent();
        }
        public frmProcedimientos()
        {
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
        }

        //BUSCAR PROCEDIMIENTO 
        private void btnBuscarProcedimiento_Click(object sender, EventArgs e)
        {
            using (var subForm = new subFormProcedimiento())
            {
                var result = subForm.ShowDialog();

                if (result == DialogResult.OK) {
                    txtIdProcedimiento.Text = subForm._procedimiento.ID_Procedimiento.ToString();
                    txtCodigo.Text = subForm._procedimiento.Codigo.ToString();
                    txtProcedimiento.Text = subForm._procedimiento.Nombre;
                    txtCategoria.Text = subForm._procedimiento.oCategoria.Descripcion;
                    if (chkBoxAsegurado.Checked)
                    {
                        txtPrecio.Text = subForm._procedimiento.PrecioVentaAsegurado.ToString("0.00");
                    }
                    else
                    {
                        txtPrecio.Text = subForm._procedimiento.PrecioVenta.ToString("0.00");
                    }
                }
                else
                {
                    txtCodigo.Select();
                }

            }
        }

        //BUSCAR PACIENTE 
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var subForm = new SubFormCliente())
            {
                var result = subForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtDocumentoCliente.Text = subForm._Cliente.Documento.ToString();
                    txtNombre.Text = subForm._Cliente.Nombre;
                }
                else
                {
                    txtDocumentoCliente.Select();
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
                    total += Convert.ToDecimal(row.Cells["Precio"].Value.ToString());
                }
            }
            txtTotalPagar.Text = total.ToString("0.00");
        }

        private void calcularCambio()
        {
            if (txtTotalPagar.Text.Trim().Length == 0)
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagaCon;
            decimal total = Convert.ToDecimal(txtTotalPagar.Text);

            if (txtPagaCon.Text.Trim() == "")
            {
                txtPagaCon.Text = "0";
            }
            if (decimal.TryParse(txtPagaCon.Text.Trim(), out pagaCon))
            {
                if (pagaCon < total)
                {
                    txtCambio.Text = "0.00";
                }
                else
                {
                    decimal cambio = pagaCon - total;
                    txtCambio.Text = cambio.ToString("0.00");
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
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio - Formato moneda Incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecio.Select();
                return;
            }
            foreach (DataGridViewRow fila in dgvData.Rows) //validamos si el producto ya existe
            {
                if (fila.Cells["ID_Procedimiento"].Value.ToString() == txtIdProcedimiento.Text)
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
                    precio.ToString("0.00"),
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
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
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


    }
}
