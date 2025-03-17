using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //me cierra el formulario de incio de sesion al presionar cancelar 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //configuracion del boton de ingresar para que oculte el formulario de i.s y muestre el principal
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            List<Usuario> TEST = new CN_Usuario().Listar();

            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == txtBoxDoc.Text && u.Clave == txtBoxClave.Text).FirstOrDefault();

            if (ousuario != null)
            {



                Inicio form = new Inicio();

                form.Show();
                this.Hide();

                form.FormClosing += FormClosing;
            }
            else
            {
                MessageBox.Show("Nos se encontro el usuario","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }



        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            //me muestre los campos de clave y documentos vacios al volver al inicio de sesion
            txtBoxDoc.Text = "";
            txtBoxClave.Text = "";

            this.Show();
        }
    }
}
