using CapaEntidad;
using CapaPresentacion.Modales;
using CapaPresentacion.Sub_Forms;
using CapaPresentacion.Utilidades;
using System;
using System.Windows.Forms;

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
            cmbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cmbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
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
    }
}
