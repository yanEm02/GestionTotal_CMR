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
                
                cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                
            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            // Carga la lista de clientes en el campo de la clase
            listaClientes = new CN_Cliente().Listar();

            // Itera sobre la lista para poblar el DataGridView
            foreach (Cliente item in listaClientes)
            {
                if(item.Estado)
                    dgvData.Rows.Add(new object[] {item.Documento, item.Nombre, item.Edad, item.Sexo, item.Telefono });
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;    

            if(iRow >= 0 && iColumn >= 0)
            {
                // Obtiene el número de documento de la fila seleccionada
                string documentoSeleccionado = dgvData.Rows[iRow].Cells["Documento"].Value.ToString();

                // Busca el cliente completo en la lista por su documento
                _Cliente = listaClientes.FirstOrDefault(c => c.Documento == documentoSeleccionado);

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
                Edad = Convert.ToInt32(txtEdad.Text),
                Sexo = sexoSeleccionado.Texto,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                Estado = true,
            };

            int idGenerado = new CN_Cliente().Registrar(obj, out mensaje);

            if (idGenerado != 0)
            {
                // Asignar el ID generado al objeto y agregarlo a la lista
                obj.IdCliente = idGenerado;
                listaClientes.Add(obj);

                //aqui agremos lo que este en el textbox del formulario para agregarse a la data grid view
                dgvData.Rows.Add(new object[] {txtDocumento.Text, txtNombreCompleto.Text, txtEdad.Text,
                    //((OpcionCombo)cmbSexo.SelectedItem).Valor.ToString(),
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
            // Permite solo números y teclas de control (como retroceso)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
