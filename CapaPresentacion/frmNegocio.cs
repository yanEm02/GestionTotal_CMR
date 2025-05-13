using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmNegocio : Form
    {
        public frmNegocio()
        {
            InitializeComponent();
        }

        //metodo para convertir el array de bites en imagen
        public Image ByteToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(); //para guardar imagenes en memoria
            ms.Write(imageBytes, 0, imageBytes.Length);

            Image imagen = new Bitmap(ms);//hacemos la conversion 

            return imagen;
        }


        private void frmNegocio_Load(object sender, EventArgs e)
        {
            //cuando cargamos el formulario, cargamos el logo
            bool obtenido = true;
            byte[] byteImagen = new CN_Negocio().ObtenerLogo(out obtenido);

            if (obtenido) 
            {
                picLogo.Image = ByteToImage(byteImagen);
            }

            //cargamos los datos cuando se abre el formulario
            Negocio datos = new CN_Negocio().ObtenerDatos();

            txtNombre.Text = datos.Nombre;
            txtRNC.Text = datos.Rnc;
            txtDireccion.Text = datos.Direccion;
        }

        //configuramos el metodo de subir la imagen
        private void btnSubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.FileName = "Files|*.jpg;*.jpeg;*.png"; //filtramos para que solo podamos subir imagenes

            if (openFileDialog.ShowDialog() == DialogResult.OK) //si el archivo que subimos esta bien, entonces mostramos
            {
                byte[] byteImage = File.ReadAllBytes(openFileDialog.FileName);
                bool respuesta = new CN_Negocio().ActualizarLogo(byteImage,out mensaje);

                if (respuesta) 
                {
                    picLogo.Image = ByteToImage(byteImage);
                }
                else
                {
                    MessageBox.Show(mensaje,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty ;

            Negocio obj = new Negocio()
            {
                Nombre = txtNombre.Text,
                Rnc = txtRNC.Text, 
                Direccion = txtDireccion.Text,
            };

            bool respuesta = new CN_Negocio().GuardarDatos(obj, out mensaje);

            if (respuesta)
            {
                MessageBox.Show(mensaje, "Los cambios fueron guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show(mensaje, "No se pudo guardar los cambios", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
