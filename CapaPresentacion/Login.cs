using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

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
            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == txtBoxDoc.Text && u.Clave == txtBoxClave.Text).FirstOrDefault();

            if (ousuario != null)
            {



                Inicio form = new Inicio(ousuario);

                form.Show();
                this.Hide();

                form.FormClosing += FormClosing;
            }
            else
            {
                MessageBox.Show("No se encuentra el usuario", "Usuario o Clave Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            //me muestre los campos de clave y documentos vacios al volver al inicio de sesion
            txtBoxDoc.Text = "";
            txtBoxClave.Text = "";

            this.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = AutoScaleMode.Dpi; //para que se adapte a la resolucion de la pantalla
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
