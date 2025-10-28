using CapaEntidad;
using CapaNegocio;
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

namespace CapaPresentacion.Sub_Forms
{
    public partial class subFormClientesInactivos : Form
    {
        public Cliente _Cliente { get; set; }
        public subFormClientesInactivos()
        {
            InitializeComponent();
        }

        private void subFormClientesInactivos_Load(object sender, EventArgs e)
        {
            //COMBOBOX DE ESTADO
            //agregamos los items del combobox para desplegarlos, usando la clase dentro de utilidades, usamos clases y objetos 
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;


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

            List<Cliente> lista = new CN_Cliente().Listar();

            foreach (Cliente item in lista)
            {
                if (item.Estado == false)
                        dgvData.Rows.Add(new object[] { "", item.IdCliente, item.Documento, item.Nombre, item.Correo, item.Telefono,
                      item.Estado == true ? 1 : 0,
                      item.Estado == true ? "Activo" : "No Activo",
                });
            }


        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;

            if (iRow >= 0 && iColumn >= 0)
            {
                _Cliente = new Cliente()
                {
                    Documento = dgvData.Rows[iRow].Cells["Documento"].Value.ToString(),
                    Nombre = dgvData.Rows[iRow].Cells["NombreCompleto"].Value.ToString()
                };
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
        }
    }
}
