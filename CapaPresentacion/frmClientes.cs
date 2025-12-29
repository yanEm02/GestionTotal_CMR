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
    public partial class frmClientes : Form
    {

        private static Usuario usuarioActualFor; //almacenamos el usuario que se ha logeado
        public frmClientes(Usuario usuarioActual)
        {
            usuarioActualFor = usuarioActual;
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            //COMBOBOX DE ESTADO
            //agregamos los items del combobox para desplegarlos, usando la clase dentro de utilidades, usamos clases y objetos 
            cmbSexo.Items.Add(new OpcionCombo() {Texto = "Masculino" });
            cmbSexo.Items.Add(new OpcionCombo() {Texto = "Femenino" });
            cmbSexo.DisplayMember = "Texto";
            cmbSexo.SelectedIndex = 0;
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            //hacemos una lista para traer los roles de la base de datos y listarlos con un foreach
            List<Rol> listaRol = new CN_Rol().Listar();

            //[para realizar el filtro por columna 
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            CargarDatos();

            int rolUsuario = usuarioActualFor?.oRol?.IdRol ?? 1;

            if (rolUsuario == 2)
            {
                btnEliminar.Visible = false; // Oculta el botón Eliminar para usuarios estándar
                cboEstado.Enabled = false; // Deshabilita el ComboBox de estado para usuarios estándar
            }
        }

        private void CargarDatos()
        {
            dgvData.Rows.Clear();
            List<Cliente> lista = new CN_Cliente().Listar();
            int rolUsuario = usuarioActualFor?.oRol?.IdRol ?? 1;

            foreach (Cliente item in lista)
            {
                if (rolUsuario == 1 || rolUsuario == 0)
                {
                    dgvData.Rows.Add(new object[] { "", item.IdCliente, item.Documento, item.Nombre, item.Edad,
                        item.Sexo, item.Direccion, item.Telefono,
                        item.Estado ? 1 : 0,
                        item.Estado ? "Activo" : "No Activo"
                    });
                }
                else if (rolUsuario == 2 && item.Estado)
                {
                    dgvData.Rows.Add(new object[] { "", item.IdCliente, item.Documento, item.Nombre, item.Edad,
                        item.Sexo, item.Direccion, item.Telefono,
                        1, "Activo"
                    });
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpcionCombo sexoSeleccionado = (OpcionCombo)cmbSexo.SelectedItem;

            int edad;
            int.TryParse(txtEdad.Text, out edad); // Si falla, edad será 0

            Cliente obj = new Cliente()
            {
                IdCliente = Convert.ToInt32(txtid.Text),
                Documento = txtDocumento.Text,
                Nombre = txtNombreCompleto.Text,
                Edad = edad,
                Sexo = sexoSeleccionado.Texto,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
            };

            if (obj.IdCliente == 0)
            {
                int idGenerado = new CN_Cliente().Registrar(obj, out mensaje);
                if (idGenerado != 0)
                {
                    CargarDatos();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Cliente().Editar(obj, out mensaje);
                if (resultado)
                {
                    CargarDatos();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        //limpiamos los datos introducidos despues de guardar 
        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtid.Text = "0";
            txtDocumento.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtEdad.Text = "";
            txtNombreCompleto.Text = "";
            cboEstado.SelectedIndex = 0;
            cmbSexo.SelectedIndex = 0;

            txtDocumento.Select();
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check_24dp_E3E3E3_FILL0_wght400_GRAD0_opsz24.Width;
                var h = Properties.Resources.check_24dp_E3E3E3_FILL0_wght400_GRAD0_opsz24.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check_24dp_E3E3E3_FILL0_wght400_GRAD0_opsz24, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtid.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtEdad.Text = dgvData.Rows[indice].Cells["Edad"].Value.ToString();
                    cmbSexo.SelectedIndex = cmbSexo.FindStringExact(dgvData.Rows[indice].Cells["Sexo"].Value.ToString());
                    txtDireccion.Text = dgvData.Rows[indice].Cells["Direccion"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["telefono"].Value.ToString();

                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("Desea Eliminar el Cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Cliente objCliente = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtid.Text),
                    };
                    bool respuesta = new CN_Cliente().Eliminar(objCliente, out mensaje);

                    if (respuesta)
                    {
                        CargarDatos();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

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
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private bool EsCorreoValido(string correo) //para confirmar si el correo es valido
        {
            // Expresión regular básica para validar correo electrónico
            return System.Text.RegularExpressions.Regex.IsMatch(
                correo,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
            );
        }

        private void btnClientesInactivos_Click(object sender, EventArgs e)
        {
            new subFormClientesInactivos().ShowDialog();
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if ((sender as TextBox).Text.Length >= 10)
            {
                e.Handled = true;
            }
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
