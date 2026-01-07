using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Sub_Forms
{
    public partial class SubFormCliente : Form
    {
        public Cliente _Cliente {  get; set; }
        private List<Cliente> listaClientes;

        public SubFormCliente()
        {
            InitializeComponent();
        }

        private void SubFormCliente_Load(object sender, EventArgs e)
        {
            cmbSexo.Items.Add(new OpcionCombo() { Texto = "Masculino" });
            cmbSexo.Items.Add(new OpcionCombo() { Texto = "Femenino" });
            cmbSexo.DisplayMember = "Texto";
            cmbSexo.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                // Evitar que la columna oculta aparezca en el ComboBox de búsqueda
                if (columna.Visible)
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            // Carga la lista de clientes y la ordena por IdCliente descendente para mostrar los más nuevos primero
            listaClientes = new CN_Cliente().Listar().OrderByDescending(c => c.IdCliente).ToList();

            // Itera sobre la lista para poblar el DataGridView
            foreach (Cliente item in listaClientes)
            {
                if(item.Estado)
                    // Añadimos el IdCliente en la primera columna (oculta)
                    dgvData.Rows.Add(new object[] {item.IdCliente, item.Documento, item.Nombre, item.Edad, item.Sexo, item.Telefono });
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;    

            if(iRow >= 0 && iColumn >= 0)
            {
                // Obtiene el ID del cliente desde la columna oculta (índice 0).
                int idClienteSeleccionado = Convert.ToInt32(dgvData.Rows[iRow].Cells["IdCliente"].Value);

                // Busca el cliente completo en la lista por su ID único.
                _Cliente = listaClientes.FirstOrDefault(c => c.IdCliente == idClienteSeleccionado);

                // Cierra el formulario si se encontró el cliente
                if (_Cliente != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
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

        //CREAMOS PROCEDIMIENTO PARA AGREGAR CLIENTE NUEVO SI EL USUARIO DESEA 

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpcionCombo sexoSeleccionado = (OpcionCombo)cmbSexo.SelectedItem;

            Cliente obj = new Cliente()
            {
                IdCliente = Convert.ToInt32(txtid.Text),
                Documento = txtDocumento.Text,
                Nombre = txtNombreCompleto.Text,
                Edad = txtEdad.Text,
                Sexo = sexoSeleccionado.Texto,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                Estado = true,
            };

            int idGenerado = new CN_Cliente().Registrar(obj, out mensaje);

            if (idGenerado != 0)
            {
                // Asignar el ID generado al objeto y agregarlo al inicio de la lista
                obj.IdCliente = idGenerado;
                listaClientes.Insert(0, obj);

                // Insertamos la nueva fila al principio del DataGridView (índice 0)
                dgvData.Rows.Insert(0, new object[] {
                    idGenerado, // Añadimos el nuevo ID a la columna oculta
                    txtDocumento.Text,
                    txtNombreCompleto.Text,
                    txtEdad.Text,
                    ((OpcionCombo)cmbSexo.SelectedItem).Texto.ToString(),
                    txtTelefono.Text,
                    });

                Limpiar();
            }
            else
            {
                MessageBox.Show(mensaje);
            }
        }

        private void Limpiar()
        {
            
            txtid.Text = "0";
            txtDocumento.Text = "";
            txtTelefono.Text = "";
            txtNombreCompleto.Text = "";
            txtEdad.Text = "";
            txtDireccion.Text = "";

            txtDocumento.Select();
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite solo números y teclas de control (como retroceso)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).Text.Length >= 15 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Cancela la acción para no exceder el límite
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si la tecla presionada no es una tecla de control (como retroceso) Y no es un dígito
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Cancela la acción
            }
            // Si la longitud del texto es 14 o más Y la tecla presionada no es de control
            else if ((sender as TextBox).Text.Length >= 14 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Cancela la acción para no exceder el límite
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar.PerformClick();
                // Evita que el sonido de "ding" de Windows suene al presionar Enter
                e.Handled = true;
            }

        }
    }
}
