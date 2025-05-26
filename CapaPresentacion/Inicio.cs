using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using CapaEntidad;
using CapaNegocio;
using System.Linq;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {

        private static Usuario usuarioActual;
        //variables para traer el menu 
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;

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
        }


        //MOMENTO DONDE SE CARGA LA PAGINA 
        private void Inicio_Load(object sender, EventArgs e)
        {

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

      

        //==========USUARIO===

        //creamos un metodo para traer o abrir el formulario usuario
        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;
            MenuActivo = menu;

            if (FormularioActivo != null)
            { 
                FormularioActivo.Close();
            }

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.SteelBlue;

            //agregamos el formulario ya hecho
            contenedor.Controls.Add(formulario);
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
        private void menuReporte_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmReportes());
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
