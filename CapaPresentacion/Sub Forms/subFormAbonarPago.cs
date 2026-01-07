using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Sub_Forms
{
    public partial class subFormAbonarPago : Form
    {
        private Compra _compraActual;
        public decimal MontoAbonado { get; private set; }
        public string TipoPagoSeleccionado { get; private set; }

        public subFormAbonarPago(Compra oCompra)
        {
            _compraActual = oCompra;
            InitializeComponent();
        }

        private void subFormAbonarPago_Load(object sender, EventArgs e)
        {
            cmbTipoPago.Items.Add(new OpcionCombo() { Valor = "Efectivo", Texto = "Efectivo" });
            cmbTipoPago.Items.Add(new OpcionCombo() { Valor = "Tarjeta", Texto = "Tarjeta" });
            cmbTipoPago.Items.Add(new OpcionCombo() { Valor = "Transferencia", Texto = "Transferencia" });
            cmbTipoPago.DisplayMember = "Texto";
            cmbTipoPago.ValueMember = "Valor";
            cmbTipoPago.SelectedIndex = 0;

            // Mostramos el monto pendiente y la fecha límite en el formulario
            txtMontoPendiente.Text = _compraActual.MontoPendiente.ToString("C2");
            txtFechaLimitePago.Text = _compraActual.FechaLimite;

            // Cargar historial de pagos
            List<HistorialPagoCompra> historial = new CN_Compra().ObtenerHistorialPagos(_compraActual.IdCompra);

            dgvData.Rows.Clear();
            foreach (var pago in historial)
            {
                dgvData.Rows.Add(new object[] {
                    pago.Cantidad.ToString("C2"),
                    pago.TipoPago,
                    pago.FechaRegistro
                });
            }
        }

        private void btnAbonarMonto_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMontoAbonar.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal montoAbonar))
            {
                MessageBox.Show("Por favor, ingrese un monto válido.", "Monto Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (montoAbonar <= 0)
            {
                MessageBox.Show("El monto a abonar debe ser mayor que cero.", "Monto Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (montoAbonar > _compraActual.MontoPendiente)
            {
                MessageBox.Show("El monto a abonar no puede ser mayor que el monto pendiente.", "Monto Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si la validación es correcta, asignamos los valores.
            this.MontoAbonado = montoAbonar;
            this.TipoPagoSeleccionado = ((OpcionCombo)cmbTipoPago.SelectedItem).Texto;
            this.DialogResult = DialogResult.OK;
        }
    }
}
