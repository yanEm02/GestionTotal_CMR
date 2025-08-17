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
using ClosedXML.Excel;

namespace CapaPresentacion
{
    public partial class frmReporteCompra : Form
    {
        public frmReporteCompra()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmReporteCompra_Load(object sender, EventArgs e)
        {
            List<Proveedor> lista = new CN_Proveedor().Listar();

            //seleccion del proveedor, para mostrar los proveedores
            cmbProveedor.Items.Add(new OpcionCombo() { Valor = 0, Texto = "TODOS" }); //agregamos opcion que pase una a la consulta sql y traiga todos los proveedores
            foreach (Proveedor item in lista)
            {
                cmbProveedor.Items.Add(new OpcionCombo() { Valor = item.IdProveedor, Texto = item.RazonSocial });
            }
            cmbProveedor.DisplayMember = "Texto";
            cmbProveedor.ValueMember = "Value";
            cmbProveedor.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                cmbFiltro.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cmbFiltro.DisplayMember = "Texto";
            cmbFiltro.ValueMember = "Value";
            cmbFiltro.SelectedIndex = 0;


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int idProveedor = Convert.ToInt32(((OpcionCombo)cmbProveedor.SelectedItem).Valor.ToString());

            List<ReporteCompra> lista = new List<ReporteCompra>();
            lista = new CN_Reporte().Compra(
                txtFechaInicio.Value.ToString(),
                txtFechaFin.Value.ToString(),
                idProveedor
            );

            dgvData.Rows.Clear(); 
            
            foreach(ReporteCompra rc in lista)
            {
                dgvData.Rows.Add(new object[]
                {
                    rc.FechaRegistro,
                    rc.TipoDocumento,
                    rc.NumeroDocumento,
                    rc.MontoTotal,
                    rc.UsuarioRegistro,
                    rc.DocumentoProveedor,
                    rc.RazonSocial,
                    rc.CodigoProducto,
                    rc.NombreProducto,
                    rc.Categoria,
                    rc.PrecioCompra,
                    rc.PrecioVenta,
                    rc.Cantidad,
                    rc.SubTotal
                });
            }

        }

        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            if(dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvData.Columns) //agregamos las columnas
                {
                    dt.Columns.Add(columna.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow fila in dgvData.Rows)//agregamos las filas
                {
                    if (fila.Visible)
                    {
                        dt.Rows.Add(new object[]
                        {

                            fila.Cells[0].Value.ToString(),
                            fila.Cells[1].Value.ToString(),
                            fila.Cells[2].Value.ToString(),
                            fila.Cells[3].Value.ToString(),
                            fila.Cells[4].Value.ToString(),
                            fila.Cells[5].Value.ToString(),
                            fila.Cells[6].Value.ToString(),
                            fila.Cells[8].Value.ToString(),
                            fila.Cells[9].Value.ToString(),
                            fila.Cells[10].Value.ToString(),
                            fila.Cells[11].Value.ToString(),
                            fila.Cells[12].Value.ToString(),
                            fila.Cells[13].Value.ToString(),
                        });
                    }
                }

                //instanciamos el savefiledialog
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = string.Format("ReporteCompra_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                sfd.Filter = "Excel Files | *.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.Columns().AdjustToContents(); //ajustamos el ancho de las columnas
                        wb.SaveAs(sfd.FileName); //guardamos el archivo en la ruta seleccionada
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("No se ha seleccionado un archivo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    
                }
                
            }
        }

        private void btnBuscarPor_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbFiltro.SelectedItem).Valor.ToString();

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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
