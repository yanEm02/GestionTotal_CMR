using System.Windows.Forms;
using System.Collections.Generic;

using CapaPresentacion.Utilidades;
using CapaEntidad;
using CapaNegocio;
using System.Drawing;
using System;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }


        //cargamos le formulario
        private void frmUsuarios_Load(object sender, System.EventArgs e)
        {
            //COMBOBOX DE ESTADO
            //agregamos los items del combobox para desplegarlos, usando la clase dentro de utilidades, usamos clases y objetos 
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1 , Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0 , Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            //hacemos una lista para traer los roles de la base de datos y listarlos con un foreach
            List<Rol> listaRol = new CN_Rol().Listar();

            foreach (Rol item in listaRol) {
                cboRol.Items.Add(new OpcionCombo() {Valor = item.IdRol, Texto = item.Descripcion });
            }
            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;


            //[para realizar el filtro por columna 
            foreach(DataGridViewColumn columna in dgvData.Columns)
            {
                if(columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            //mostrar todos los usuarios en el data grid view
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] { "", item.IdUsuario, item.Documento, item.Nombre, item.Correo, item.Clave, item.oRol.IdRol, 
                  item.oRol.Descripcion,
                  item.Estado == true ? 1 : 0,
                  item.Estado == true ? "Activo" : "No Activo",
            });
            }

        }

        //EJECUCION DEL BOTON GUARDAR
        private void btnGuardar_Click(object sender, System.EventArgs e)
        {
            string mensaje = string.Empty;

            Usuario objusuario = new Usuario() 
            {
                IdUsuario = Convert.ToInt32(txtid.Text),
                Documento = txtDocumento.Text,
                Nombre = txtNombreCompleto.Text,
                Correo = txtCorreo.Text,
                Clave = txtClave.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
            };

            //hacemos una condicional para decidir el comportamiento del boton guardar, si es para editar si hay un usuario selccionado o guardar si no hay usuario seleccionado 
            if (objusuario.IdUsuario == 0)
            {
                int idUsuarioGenerado = new CN_Usuario().Registrar(objusuario, out mensaje);

                if (idUsuarioGenerado != 0)
                {
                    //aqui agremos lo que este en el textbox del formulario para agregarse a la data grid view
                    dgvData.Rows.Add(new object[] { "", idUsuarioGenerado, txtDocumento.Text, txtNombreCompleto.Text, txtCorreo.Text, txtClave.Text, ((OpcionCombo)cboRol.SelectedItem).Valor.ToString(), //para obtener el rol de la base de datos
                    ((OpcionCombo)cboRol.SelectedItem).Texto.ToString(),
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
                bool resultado = new CN_Usuario().Editar(objusuario, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Documento"].Value = txtDocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtNombreCompleto.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text;
                    row.Cells["Clave"].Value = txtClave.Text;
                    row.Cells["Id_Rol"].Value = ((OpcionCombo)cboRol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)cboRol.SelectedItem).Texto.ToString();
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

        //EJECUCION DEL BOTON LIMPIAR 
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        //EJECUCION DEL BOTON ELIMINAR 
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("Desea Eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    //sacamos el id usuario de nuestra clase de usuario 
                    string mensaje = string.Empty;
                    Usuario objusuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text),
                    };
                    bool respuesta = new CN_Usuario().Eliminar(objusuario, out mensaje);

                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            
            }

        }

        //boton de FILTRO
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

        //LIMPIAR EL FILTRO
        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true; 
            }
        }

        //limpiamos los datos introducidos despues de guardar 
        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtid.Text = "0";
            txtDocumento.Text = "";
            txtCorreo.Text = "";
            txtConfirmarClave.Text = "";
            txtClave.Text = "";
            txtNombreCompleto.Text = "";
            cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;

            txtDocumento.Select();
        }

        //para agregar el icono del check mark a las columnaas 
        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }
            if(e.ColumnIndex == 0)
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

        //pintamos los datos del datagrid view en el panel izq una vez seleccionado el usuario
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
                    txtCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();
                    txtConfirmarClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();

                    //agreagamos los combobox
                    foreach(OpcionCombo oc in cboRol.Items)
                    {
                        if(Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["Id_Rol"].Value))
                        {
                            int indice_combo = cboRol.Items.IndexOf(oc);
                            cboRol.SelectedIndex = indice_combo;
                            break;
                        }
                    }

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

    }
}
