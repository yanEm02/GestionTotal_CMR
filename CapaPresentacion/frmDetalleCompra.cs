using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
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

namespace CapaPresentacion
{
    public partial class frmDetalleCompra : Form
    {
        public frmDetalleCompra()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Compra oCompra = new CN_Compra().ObtenerCompra(txtBusqueda.Text);

            if (oCompra.IdCompra != 0)
            {
                txtNumeroDocumento.Text = oCompra.NumeroDocumento;
                txtFecha.Text = oCompra.FechaRegistro;
                txtTipoDocumento.Text = oCompra.TipoDocumento;
                txtUsuario.Text = oCompra.oUsuario.Nombre;
                txtDocProveedor.Text = oCompra.oProveedor.Documento;
                txtRazonSocial.Text = oCompra.oProveedor.RazonSocial;

            }

            dgvData.Rows.Clear();
            foreach (DetalleCompra dc in oCompra.oDetalleCompra)
            {
                dgvData.Rows.Add(new object[] {dc.oProducto.Nombre,dc.PrecioCompra,dc.Cantidad,dc.MontoTotal});
            }

            txtMontoTotal.Text = oCompra.MontoTotal.ToString("0.00");
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocProveedor.Text = "";
            txtRazonSocial.Text = "";

            dgvData.Rows.Clear();
            txtMontoTotal.Text = "0.00";
        }

        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            if(txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resuultados","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            string textoHtml = Properties.Resources.PlantillaCompra.ToString();
            Negocio oDatos = new CN_Negocio().ObtenerDatos(); 

            //ACA REEMPLAZAMOS EL TEXTO DE LA PLANTILLA HTML CON INFO DEL FORMULARIO DE LA COMPRA
            textoHtml = textoHtml.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            textoHtml = textoHtml.Replace("@docnegocio", oDatos.Rnc);
            textoHtml = textoHtml.Replace("@direcnegocio", oDatos.Direccion);

            textoHtml = textoHtml.Replace("@tipodocumento", txtTipoDocumento.Text.ToUpper());
            textoHtml = textoHtml.Replace("@numerodocumento", txtNumeroDocumento.Text);

            textoHtml = textoHtml.Replace("@docproveedor", txtDocProveedor.Text);
            textoHtml = textoHtml.Replace("@nombreproveedor", txtRazonSocial.Text);
            textoHtml = textoHtml.Replace("@fecharegistro", txtFecha.Text);
            textoHtml = textoHtml.Replace("@usuarioregistro", txtUsuario.Text);

            string filas = string.Empty;
            foreach(DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                    filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";


            }
            textoHtml = textoHtml.Replace("@filas", filas);
            textoHtml = textoHtml.Replace("@montototal", txtMontoTotal.Text);

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Compra_{0}.pdf", txtNumeroDocumento.Text);
            saveFile.Filter = "Pdf Files|*.pdf";

            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4,25,25,25,25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc,stream); //creamos el pdf con el pdfwriter
                    pdfDoc.Open();

                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);

                    if (obtenido) 
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(textoHtml))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }
                    pdfDoc.Close();
                    stream.Close(); //cerramos pdf y archivo de memoria
                    MessageBox.Show("Factura Generada","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }

        }
    }
}
