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

namespace CapaPresentacion
{
    public partial class frmProcedimiento : Form
    {
        public frmProcedimiento()
        {
            InitializeComponent();
        }

        private void frmProcedimiento_Load(object sender, EventArgs e)
        {
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            List<Categoria> listaCategoria = new CN_Categoria().Listar();

            foreach (Categoria item in listaCategoria)
            {
                cboCategoria.Items.Add(new OpcionCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });
            }
            cboCategoria.DisplayMember = "Texto";
            cboCategoria.ValueMember = "Valor";
            if (cboCategoria.Items.Count > 0)
            {
                cboCategoria.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No hay categorias disponibles para mostrar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


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

            List<Procedimiento> lista = new CN_Procedimiento().Listar();

            foreach (Procedimiento item in lista)
            {
                dgvData.Rows.Add(new object[] { "",
                    item.ID_Procedimiento,
                    item.Codigo,
                    item.Nombre,
                    item.oCategoria.IdCategoria,
                    item.oCategoria.Descripcion,
                    item.PrecioVenta,
                    item.PrecioVentaAsegurado,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "No Activo",
                });
            }


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Procedimiento obj = new Procedimiento()
            {
                ID_Procedimiento = Convert.ToInt32(txtid.Text),
                Codigo = Convert.ToInt32(txtCodigo.Text),
                Nombre = txtNombre.Text,
                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor) },
                PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text),
                PrecioVentaAsegurado = Convert.ToDecimal(txtPrecioVentaAsegurado.Text),
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
            };

            //hacemos una condicional para decidir el comportamiento del boton guardar, si es para editar si hay un usuario selccionado o guardar si no hay usuario seleccionado 
            if (obj.ID_Procedimiento == 0)
            {
                int idGenerado = new CN_Procedimiento().Registrar(obj, out mensaje);

                if (idGenerado != 0)
                {
                    //aqui agremos lo que este en el textbox del formulario para agregarse a la data grid view
                    dgvData.Rows.Add(new object[] { "",
                        idGenerado,
                        txtCodigo.Text,
                        txtNombre.Text,
                        ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString(), //para obtener el estado de la base de datos
                        ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString(),
                        txtPrecioVenta.Text,
                        txtPrecioVentaAsegurado.Text,
                        ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(), //para obtener el estado de la base de datos
                        ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString(),
                    });

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else //aqui ejecuta el metodo editar si el indice seleccionado es el de un usuario existente 
            {
                bool resultado = new CN_Procedimiento().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Codigo"].Value = txtCodigo.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Id_Categoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString();
                    row.Cells["Categoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString();
                    row.Cells["PrecioVenta"].Value = txtPrecioVenta.Text;
                    row.Cells["PrecioVentaAsegurado"].Value = txtPrecioVentaAsegurado.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("Desea Eliminar el Producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    //sacamos el id usuario de nuestra clase de usuario 
                    string mensaje = string.Empty;
                    Procedimiento obj = new Procedimiento()
                    {
                        ID_Procedimiento = Convert.ToInt32(txtid.Text),
                    };
                    bool respuesta = new CN_Procedimiento().Eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

            }
            Limpiar();
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtid.Text = "0";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            cboCategoria.SelectedIndex = 0;
            txtPrecioVenta.Text = "0";
            txtPrecioVentaAsegurado.Text = "0";
            cboEstado.SelectedIndex = 0;

            txtCodigo.Select();
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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            txtIndice.Text = "-1";
            txtid.Text = "0";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            cboCategoria.SelectedIndex = 0;
            txtPrecioVenta.Text = "0";
            txtPrecioVentaAsegurado.Text = "0";
            cboEstado.SelectedIndex = 0;

            txtCodigo.Select();
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
                    txtCodigo.Text = dgvData.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtPrecioVenta.Text = dgvData.Rows[indice].Cells["PrecioVenta"].Value.ToString();
                    txtPrecioVentaAsegurado.Text = dgvData.Rows[indice].Cells["PrecioVentaAsegurado"].Value.ToString();

                    //agreagamos los combobox
                    foreach (OpcionCombo oc in cboCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["Id_Categoria"].Value))
                        {
                            int indice_combo = cboCategoria.Items.IndexOf(oc);
                            cboCategoria.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(Convert.ToDecimal(dgvData.Rows[indice].Cells["EstadoValor"].Value)))
                        {
                            int indice_combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }

        
    }
}
