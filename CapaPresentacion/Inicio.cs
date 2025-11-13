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
            ConfigurarControlesDeUsuarioEnMenu(); // <--- AÑADIR ESTA LÍNEA

        }


        //MOMENTO DONDE SE CARGA LA PAGINA 
        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> ListaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario); //creamos la lista para visualizar los permisos
            
            // Use OfType<IconMenuItem>() to filter only the items of that specific type.
            foreach (IconMenuItem iconMenu in menu.Items.OfType<IconMenuItem>())
            {
                bool encontrado = ListaPermisos.Any(m => m.NombreMenu == iconMenu.Name);

                if (encontrado == false)
                {
                    iconMenu.Visible = false;
                }
            }

            // lblusuario.Text = usuarioActual.Nombre;
        }



        //===========

        //creamos un metodo para traer o abrir el formulario que se cliquee en el menu
        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.SandyBrown;
            }
            menu.BackColor = Color.White;
            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            //imgCentral.Hide();
            contenedor.Controls.Clear();

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.BackColor = Color.Linen;

            // --- INICIO DE LA MODIFICACIÓN ---
            // 1. Forzar la creación del handle del subformulario.
            //    Esto obliga a que se aplique el escalado DPI y se calcule su tamaño final.
            if (!formulario.IsHandleCreated)
            {
                formulario.CreateControl();
            }
            Size subFormSize = formulario.Size;

            // 2. Calcular el tamaño de la "decoración" de la ventana principal (bordes y barra de título).
            int nonClientWidth = this.Width - this.ClientSize.Width;
            int nonClientHeight = this.Height - this.ClientSize.Height;

            // 3. Calcular el nuevo tamaño total requerido para la ventana 'Inicio'.
            //    Ancho: El ancho del subformulario + el ancho de los bordes.
            //    Alto: El alto del subformulario + el alto del menú + el alto de la barra de título/bordes.
            int newWidth = subFormSize.Width + nonClientWidth;
            int newHeight = subFormSize.Height + this.menu.Height + nonClientHeight;

            // 4. Establecer el tamaño total de la ventana 'Inicio'.
            this.Size = new Size(newWidth, newHeight);

            // 5. Agregar el subformulario al contenedor y acoplarlo.
            contenedor.Controls.Add(formulario);
            formulario.Dock = DockStyle.Fill;
            // --- FIN DE LA MODIFICACIÓN ---

            formulario.Show();
        }

        private void menuUsuario_Click(object sender, EventArgs e)
        {
            //insertamos el metodo para abrir el formulario aca abajo
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios());
        }

        //==========CATEGORIAS===
        //private void iconMenuItemToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    AbrirFormulario(menuMantenedor, new frmCategoria());
        //}

        //private void subMenuProducto_Click(object sender, EventArgs e)
        //{
        //    AbrirFormulario(menuMantenedor, new frmProducto());

        //}
        //private void subMenuNegocio_Click(object sender, EventArgs e)
        //{
        //    AbrirFormulario(menuMantenedor, new frmNegocio());
        //}

        //==========VENTAS===
        private void subMenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuVenta, new frmVentas(usuarioActual));
        }

        private void subMenuVerDetalleVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuVenta, new frmDetalleVenta());
        }

        //==========COMPRAS===

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
            AbrirFormulario((IconMenuItem)sender, new frmClientes(usuarioActual)); 
        }

        //==========PROVEEDOR===

        private void menuProveedor_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores(usuarioActual));
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


        //=========INVENTARIO===

        private void menuInventario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProducto());
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmCategoria());
        }

        //==========CONFIGURACIOKN/MANTENEDOR ===

        private void menuMantenedor_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmNegocio());
        }

        //==========ACERCA DE ===

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

        private void ConfigurarControlesDeUsuarioEnMenu()
        {
            // Crear el botón de salir
            var btnSalir = new FontAwesome.Sharp.IconButton
            {
                IconChar = FontAwesome.Sharp.IconChar.SignOutAlt,
                IconColor = System.Drawing.Color.Black,
                IconFont = FontAwesome.Sharp.IconFont.Auto,
                Size = new System.Drawing.Size(50, 45),
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                BackColor = System.Drawing.Color.Transparent,
            };
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            var toolStripBtnSalir = new System.Windows.Forms.ToolStripControlHost(btnSalir)
            {
                Alignment = System.Windows.Forms.ToolStripItemAlignment.Right,
                Margin = new System.Windows.Forms.Padding(0, 2, 0, 2)
            };

            // Crear la etiqueta para el nombre de usuario
            var lblUsuario = new System.Windows.Forms.Label
            {
                Text = usuarioActual.Nombre,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.Black,
                BackColor = System.Drawing.Color.Transparent,
                AutoSize = true
            };
            // --- INICIO DE LA MODIFICACIÓN DE ALINEACIÓN ---
            int topMarginUsuario = (btnSalir.Height - lblUsuario.Height) / 2;
            var toolStripLblUsuario = new System.Windows.Forms.ToolStripControlHost(lblUsuario)
            {
                Alignment = System.Windows.Forms.ToolStripItemAlignment.Right,
                Margin = new System.Windows.Forms.Padding(0, topMarginUsuario, 5, 0)
            };

            // Crear la etiqueta "Usuario:"
            var lblTextoUsuario = new System.Windows.Forms.Label
            {
                Text = "Usuario:",
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10F),
                ForeColor = System.Drawing.Color.Black,
                BackColor = System.Drawing.Color.Transparent,
                AutoSize = true
            };
            int topMarginTextoUsuario = (btnSalir.Height - lblTextoUsuario.Height) / 2;
            var toolStripLblTextoUsuario = new System.Windows.Forms.ToolStripControlHost(lblTextoUsuario)
            {
                Alignment = System.Windows.Forms.ToolStripItemAlignment.Right,
                Margin = new System.Windows.Forms.Padding(0, topMarginTextoUsuario, 0, 0)
            };
            // --- FIN DE LA MODIFICACIÓN DE ALINEACIÓN ---

            // Añadir los controles al MenuStrip en orden inverso (porque se alinean a la derecha)
            menu.Items.Add(toolStripBtnSalir);
            menu.Items.Add(toolStripLblUsuario);
            menu.Items.Add(toolStripLblTextoUsuario);
        }
    }
}
