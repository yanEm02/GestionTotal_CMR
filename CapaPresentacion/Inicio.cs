using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using CapaEntidad;
using CapaNegocio;
using System.Linq;
using CapaPresentacion.Sub_Forms;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {

        private static Usuario usuarioActual;
        //variables para traer el menu 
        private static IconMenuItem MenuActivo = null;  //almacenamos el menu que esta activo
        private static Form FormularioActivo = null; //almacena el formulario activo que se muestra en el panel

        //almacenamos el usuario que se ha logeado 
        public Inicio(Usuario objusuario = null)
        {
            if (objusuario == null)
            {
                usuarioActual = new Usuario() { Nombre = "ADMIN PREDEFINIDO", IdUsuario = 1 };
            }
            else
            {
                usuarioActual = objusuario;
            }

            InitializeComponent();

            // Asegura que el contenedor ocupe todo el espacio disponible
            contenedor.Dock = DockStyle.Fill;

            // Suscríbete al evento Resize
            this.Resize += Inicio_Resize;
        }


        //MOMENTO DONDE SE CARGA LA PAGINA 
        private void Inicio_Load(object sender, EventArgs e)
        {
            //this.AutoScaleMode = AutoScaleMode.Dpi; //para que se adapte a la resolucion de la pantalla

            List<Permiso> ListaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario); //creamos la lista para visualizar los permisos
            foreach (IconMenuItem iconMenu in menu.Items)
            {
                bool encontrado = ListaPermisos.Any(m => m.NombreMenu == iconMenu.Name);

                if(encontrado == false)
                {
                    iconMenu.Visible = false;
                }
            }


            lblusuario.Text = usuarioActual.Nombre;
        }



        //===========

        //creamos un metodo para traer o abrir el formulario que se cliquee en el menu
        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.PeachPuff;
            }
            menu.BackColor = Color.White;
            MenuActivo = menu;

            if (FormularioActivo != null)
            { 
                FormularioActivo.Close();
            }
            

            imgCentral.Hide();
            // Limpia el contenedor antes de agregar el nuevo formulario
            contenedor.Controls.Clear();

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.Linen;
            //formulario.AutoScaleMode = AutoScaleMode.Dpi; //para que se adapte a la resolucion de la pantalla

            //agregamos el formulario ya hecho
            contenedor.Controls.Add(formulario);
            // Ajusta el tamaño del formulario principal al del subformulario
            //this.Size = formulario.Size;

            formulario.Show();
        }
        private void menuUsuario_Click(object sender, EventArgs e)
        {
            //insertamos el metodo para abrir el formulario aca abajo
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios());
        }

        //==========MANTENEDOR===
        private void iconMenuItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmCategoria());
        }

        private void subMenuProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmProducto());

        }
        private void subMenuNegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmNegocio());
        }

        //==========VENTAS===
        private void subMenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuVenta, new frmVentas(usuarioActual));
        }

        private void subMenuVerDetalleVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuVenta, new frmDetalleVenta());
        }

        //==========VENTAS===

        private void subMenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCompra, new frmCompras(usuarioActual));
        }

        private void subMenuVerDetalleCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCompra, new frmDetalleCompra());
        }

        //==========CLIENTES===

        private void menuCliente_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes()); 
        }

        //==========PROVEEDOR===

        private void menuProveedor_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores());
        }

        //==========REPORTE===

        private void subMenuReporteCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuReporte, new frmReporteCompra());
        }

        private void subMenuReporteVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuReporte, new frmReporteVenta());
        }

        private void menuAcercaDe_Click(object sender, EventArgs e)
        {
            subFormAcercaDe subForm = new subFormAcercaDe();
            subForm.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Desea salir de la aplicacion?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void Inicio_Resize(object sender, EventArgs e)
        {
            contenedor.Size = this.ClientSize;
        }




        //==========================TESTING LA OBTENCION DE DATOS 
        //private void CargarPermisos()
        //{
        //    try
        //    {
        //        // Llama directamente a la clase de datos, o a través de la capa de negocio si la tienes
        //        List<Permiso> lista = new CN_Permiso().Listar(usuarioActual.IdUsuario);

        //        // 💬 Aquí agregas la línea de prueba
        //        MessageBox.Show("Total permisos: " + lista.Count);

        //        // Proyección simple para mostrar los datos en el DataGridView
        //        var datos = lista.Select(p => new
        //        {
        //            IdRol = p.oRol.IdRol,
        //            NombreMenu = p.NombreMenu
        //        }).ToList();

        //        dataGridViewPermisos.DataSource = datos;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al cargar permisos: " + ex.Message);
        //    }
        //}


    }
}
