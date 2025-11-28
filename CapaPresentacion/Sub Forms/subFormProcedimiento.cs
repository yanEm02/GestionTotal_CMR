using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;

namespace CapaPresentacion.Sub_Forms
{
    public partial class subFormProcedimiento : Form
    {
        public Procedimiento _procedimiento { get; set; }
        public subFormProcedimiento()
        {
            InitializeComponent();
        }

        private void subFormProcedimiento_Load(object sender, EventArgs e)
        {
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

            //mostrar todos los productos en el data grid view
            List<Procedimiento> lista = new CN_Procedimiento().Listar();

            foreach (Procedimiento item in lista)
            {
                if (item.Estado)
                    dgvData.Rows.Add(new object[] {
                    item.ID_Procedimiento,
                    item.Codigo,
                    item.Nombre,
                    item.oCategoria.Descripcion,
                    item.PrecioVenta,
                    item.PrecioVentaAsegurado
                });
            }
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
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

        private void btnLimpiarBuscador_Click_1(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColumn = e.ColumnIndex;

            if (iRow >= 0 && iColumn > 0)
            {
                _procedimiento = new Procedimiento()
                {
                    ID_Procedimiento = Convert.ToInt32(dgvData.Rows[iRow].Cells["id"].Value.ToString()),
                    Codigo = Convert.ToInt32(dgvData.Rows[iRow].Cells["Codigo"].Value.ToString()),
                    Nombre = dgvData.Rows[iRow].Cells["Nombre"].Value.ToString(),
                    oCategoria = new Categoria() { Descripcion = dgvData.Rows[iRow].Cells["Categoria"].Value.ToString() },
                    PrecioVenta = Convert.ToDecimal(dgvData.Rows[iRow].Cells["PrecioVenta"].Value.ToString()),
                    PrecioVentaAsegurado = Convert.ToDecimal(dgvData.Rows[iRow].Cells["PrecioVentaAsegurado"].Value.ToString()),
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
