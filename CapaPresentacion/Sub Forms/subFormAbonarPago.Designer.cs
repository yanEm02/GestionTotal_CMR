namespace CapaPresentacion.Sub_Forms
{
    partial class subFormAbonarPago
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label10 = new System.Windows.Forms.Label();
            this.txtMontoPendiente = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtFechaLimitePago = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMontoAbonar = new System.Windows.Forms.TextBox();
            this.btnAbonarMonto = new FontAwesome.Sharp.IconButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTipoPago = new System.Windows.Forms.ComboBox();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaRegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(17, 352);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 20);
            this.label10.TabIndex = 34;
            this.label10.Text = "Monto Pendiente:";
            // 
            // txtMontoPendiente
            // 
            this.txtMontoPendiente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMontoPendiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoPendiente.Location = new System.Drawing.Point(174, 348);
            this.txtMontoPendiente.Margin = new System.Windows.Forms.Padding(4);
            this.txtMontoPendiente.Name = "txtMontoPendiente";
            this.txtMontoPendiente.ReadOnly = true;
            this.txtMontoPendiente.Size = new System.Drawing.Size(166, 28);
            this.txtMontoPendiente.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(360, 352);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(149, 20);
            this.label12.TabIndex = 74;
            this.label12.Text = "Fecha Limite Pago";
            // 
            // txtFechaLimitePago
            // 
            this.txtFechaLimitePago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFechaLimitePago.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaLimitePago.Location = new System.Drawing.Point(517, 348);
            this.txtFechaLimitePago.Margin = new System.Windows.Forms.Padding(4);
            this.txtFechaLimitePago.Name = "txtFechaLimitePago";
            this.txtFechaLimitePago.ReadOnly = true;
            this.txtFechaLimitePago.Size = new System.Drawing.Size(166, 28);
            this.txtFechaLimitePago.TabIndex = 73;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cantidad,
            this.TipoPago,
            this.FechaRegistro});
            this.dgvData.Location = new System.Drawing.Point(13, 69);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.Size = new System.Drawing.Size(496, 262);
            this.dgvData.TabIndex = 75;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(541, 170);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 77;
            this.label1.Text = "Monto a Abonar";
            // 
            // txtMontoAbonar
            // 
            this.txtMontoAbonar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMontoAbonar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoAbonar.Location = new System.Drawing.Point(519, 205);
            this.txtMontoAbonar.Margin = new System.Windows.Forms.Padding(4);
            this.txtMontoAbonar.Name = "txtMontoAbonar";
            this.txtMontoAbonar.Size = new System.Drawing.Size(166, 28);
            this.txtMontoAbonar.TabIndex = 76;
            // 
            // btnAbonarMonto
            // 
            this.btnAbonarMonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbonarMonto.IconChar = FontAwesome.Sharp.IconChar.MoneyBill;
            this.btnAbonarMonto.IconColor = System.Drawing.Color.DarkGray;
            this.btnAbonarMonto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAbonarMonto.IconSize = 24;
            this.btnAbonarMonto.Location = new System.Drawing.Point(527, 256);
            this.btnAbonarMonto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAbonarMonto.Name = "btnAbonarMonto";
            this.btnAbonarMonto.Size = new System.Drawing.Size(144, 57);
            this.btnAbonarMonto.TabIndex = 78;
            this.btnAbonarMonto.Text = "Abonar Monto";
            this.btnAbonarMonto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAbonarMonto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbonarMonto.UseVisualStyleBackColor = true;
            this.btnAbonarMonto.Click += new System.EventHandler(this.btnAbonarMonto_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(17, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 29);
            this.label2.TabIndex = 79;
            this.label2.Text = "Historial de Pagos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(546, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 20);
            this.label3.TabIndex = 81;
            this.label3.Text = "Forma de Pago";
            // 
            // cmbTipoPago
            // 
            this.cmbTipoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoPago.FormattingEnabled = true;
            this.cmbTipoPago.Location = new System.Drawing.Point(527, 96);
            this.cmbTipoPago.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(160, 33);
            this.cmbTipoPago.TabIndex = 80;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.MinimumWidth = 6;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 125;
            // 
            // TipoPago
            // 
            this.TipoPago.HeaderText = "Tipo";
            this.TipoPago.MinimumWidth = 6;
            this.TipoPago.Name = "TipoPago";
            this.TipoPago.ReadOnly = true;
            this.TipoPago.Width = 125;
            // 
            // FechaRegistro
            // 
            this.FechaRegistro.HeaderText = "Fecha del Pago";
            this.FechaRegistro.MinimumWidth = 6;
            this.FechaRegistro.Name = "FechaRegistro";
            this.FechaRegistro.ReadOnly = true;
            this.FechaRegistro.Width = 125;
            // 
            // subFormAbonarPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(697, 389);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTipoPago);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAbonarMonto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMontoAbonar);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtFechaLimitePago);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMontoPendiente);
            this.Name = "subFormAbonarPago";
            this.Text = "subFormAbonarPago";
            this.Load += new System.EventHandler(this.subFormAbonarPago_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMontoPendiente;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtFechaLimitePago;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMontoAbonar;
        private FontAwesome.Sharp.IconButton btnAbonarMonto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTipoPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaRegistro;
    }
}