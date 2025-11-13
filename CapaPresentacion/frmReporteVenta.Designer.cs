namespace CapaPresentacion
{
    partial class frmReporteVenta
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
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.btnBuscarPor = new FontAwesome.Sharp.IconButton();
            this.cmbFiltro = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDescargarExcel = new FontAwesome.Sharp.IconButton();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.FechaRegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo_documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Numero_Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsuarioRegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Razon_social = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio_Venta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscar.IconColor = System.Drawing.Color.Black;
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.IconSize = 19;
            this.btnBuscar.Location = new System.Drawing.Point(1679, 42);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(125, 43);
            this.btnBuscar.TabIndex = 17;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 78);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Fecha Inicio:";
            // 
            // txtFechaInicio
            // 
            this.txtFechaInicio.CustomFormat = "dd/mm/yyyy";
            this.txtFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFechaInicio.Location = new System.Drawing.Point(196, 74);
            this.txtFechaInicio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFechaInicio.Name = "txtFechaInicio";
            this.txtFechaInicio.Size = new System.Drawing.Size(136, 22);
            this.txtFechaInicio.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(372, 74);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Fecha Fin:";
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.CustomFormat = "dd/mm/yyyy";
            this.txtFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFechaFin.Location = new System.Drawing.Point(495, 74);
            this.txtFechaFin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.Size = new System.Drawing.Size(136, 22);
            this.txtFechaFin.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 29);
            this.label2.TabIndex = 12;
            this.label2.Text = "Reporte Ventas";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1814, 92);
            this.label1.TabIndex = 11;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusqueda.Location = new System.Drawing.Point(1521, 146);
            this.txtBusqueda.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(183, 27);
            this.txtBusqueda.TabIndex = 26;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.White;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiar.IconColor = System.Drawing.Color.Black;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 19;
            this.btnLimpiar.Location = new System.Drawing.Point(1763, 146);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(41, 28);
            this.btnLimpiar.TabIndex = 25;
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscarPor
            // 
            this.btnBuscarPor.BackColor = System.Drawing.Color.White;
            this.btnBuscarPor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarPor.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscarPor.IconColor = System.Drawing.Color.Black;
            this.btnBuscarPor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarPor.IconSize = 19;
            this.btnBuscarPor.Location = new System.Drawing.Point(1713, 146);
            this.btnBuscarPor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscarPor.Name = "btnBuscarPor";
            this.btnBuscarPor.Size = new System.Drawing.Size(41, 28);
            this.btnBuscarPor.TabIndex = 24;
            this.btnBuscarPor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscarPor.UseVisualStyleBackColor = false;
            this.btnBuscarPor.Click += new System.EventHandler(this.btnBuscarPor_Click);
            // 
            // cmbFiltro
            // 
            this.cmbFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFiltro.FormattingEnabled = true;
            this.cmbFiltro.Location = new System.Drawing.Point(1332, 146);
            this.cmbFiltro.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbFiltro.Name = "cmbFiltro";
            this.cmbFiltro.Size = new System.Drawing.Size(180, 28);
            this.cmbFiltro.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1153, 150);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "Buscar Por:";
            // 
            // btnDescargarExcel
            // 
            this.btnDescargarExcel.BackColor = System.Drawing.Color.White;
            this.btnDescargarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescargarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescargarExcel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDescargarExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnDescargarExcel.IconColor = System.Drawing.Color.Green;
            this.btnDescargarExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDescargarExcel.IconSize = 15;
            this.btnDescargarExcel.Location = new System.Drawing.Point(29, 142);
            this.btnDescargarExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDescargarExcel.Name = "btnDescargarExcel";
            this.btnDescargarExcel.Size = new System.Drawing.Size(217, 36);
            this.btnDescargarExcel.TabIndex = 21;
            this.btnDescargarExcel.Text = "Descargar Excel";
            this.btnDescargarExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDescargarExcel.UseVisualStyleBackColor = false;
            this.btnDescargarExcel.Click += new System.EventHandler(this.btnDescargarExcel_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FechaRegistro,
            this.Tipo_documento,
            this.Numero_Documento,
            this.Monto_Total,
            this.UsuarioRegistro,
            this.DocumentoProveedor,
            this.Razon_social,
            this.CodigoProducto,
            this.NombreProducto,
            this.Categoria,
            this.Precio_Venta,
            this.Cantidad,
            this.Subtotal});
            this.dgvData.Location = new System.Drawing.Point(29, 185);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.Size = new System.Drawing.Size(1779, 722);
            this.dgvData.TabIndex = 19;
            // 
            // FechaRegistro
            // 
            this.FechaRegistro.HeaderText = "Fecha Registro";
            this.FechaRegistro.MinimumWidth = 6;
            this.FechaRegistro.Name = "FechaRegistro";
            this.FechaRegistro.ReadOnly = true;
            this.FechaRegistro.Width = 125;
            // 
            // Tipo_documento
            // 
            this.Tipo_documento.HeaderText = "Forma de Pago";
            this.Tipo_documento.MinimumWidth = 6;
            this.Tipo_documento.Name = "Tipo_documento";
            this.Tipo_documento.ReadOnly = true;
            this.Tipo_documento.Width = 125;
            // 
            // Numero_Documento
            // 
            this.Numero_Documento.HeaderText = "Numero Documento";
            this.Numero_Documento.MinimumWidth = 6;
            this.Numero_Documento.Name = "Numero_Documento";
            this.Numero_Documento.ReadOnly = true;
            this.Numero_Documento.Width = 125;
            // 
            // Monto_Total
            // 
            this.Monto_Total.HeaderText = "Monto Total";
            this.Monto_Total.MinimumWidth = 6;
            this.Monto_Total.Name = "Monto_Total";
            this.Monto_Total.ReadOnly = true;
            this.Monto_Total.Width = 125;
            // 
            // UsuarioRegistro
            // 
            this.UsuarioRegistro.HeaderText = "Usuario Registro";
            this.UsuarioRegistro.MinimumWidth = 6;
            this.UsuarioRegistro.Name = "UsuarioRegistro";
            this.UsuarioRegistro.ReadOnly = true;
            this.UsuarioRegistro.Width = 125;
            // 
            // DocumentoProveedor
            // 
            this.DocumentoProveedor.HeaderText = "Documento Cliente";
            this.DocumentoProveedor.MinimumWidth = 6;
            this.DocumentoProveedor.Name = "DocumentoProveedor";
            this.DocumentoProveedor.ReadOnly = true;
            this.DocumentoProveedor.Width = 125;
            // 
            // Razon_social
            // 
            this.Razon_social.HeaderText = "Nombre Cliente";
            this.Razon_social.MinimumWidth = 6;
            this.Razon_social.Name = "Razon_social";
            this.Razon_social.ReadOnly = true;
            this.Razon_social.Width = 125;
            // 
            // CodigoProducto
            // 
            this.CodigoProducto.HeaderText = "Codigo Producto";
            this.CodigoProducto.MinimumWidth = 6;
            this.CodigoProducto.Name = "CodigoProducto";
            this.CodigoProducto.ReadOnly = true;
            this.CodigoProducto.Width = 125;
            // 
            // NombreProducto
            // 
            this.NombreProducto.HeaderText = "Nombre Producto";
            this.NombreProducto.MinimumWidth = 6;
            this.NombreProducto.Name = "NombreProducto";
            this.NombreProducto.ReadOnly = true;
            this.NombreProducto.Width = 125;
            // 
            // Categoria
            // 
            this.Categoria.HeaderText = "Categoria";
            this.Categoria.MinimumWidth = 6;
            this.Categoria.Name = "Categoria";
            this.Categoria.ReadOnly = true;
            this.Categoria.Width = 125;
            // 
            // Precio_Venta
            // 
            this.Precio_Venta.HeaderText = "Precio Venta";
            this.Precio_Venta.MinimumWidth = 6;
            this.Precio_Venta.Name = "Precio_Venta";
            this.Precio_Venta.ReadOnly = true;
            this.Precio_Venta.Width = 125;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.MinimumWidth = 6;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 125;
            // 
            // Subtotal
            // 
            this.Subtotal.HeaderText = "Subtotal";
            this.Subtotal.MinimumWidth = 6;
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            this.Subtotal.Width = 125;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(9, 129);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1814, 798);
            this.label6.TabIndex = 20;
            // 
            // frmReporteVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1826, 940);
            this.Controls.Add(this.txtBusqueda);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnBuscarPor);
            this.Controls.Add(this.cmbFiltro);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnDescargarExcel);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFechaInicio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFechaFin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmReporteVenta";
            this.Text = "frmReporteVenta";
            this.Load += new System.EventHandler(this.frmReporteVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker txtFechaInicio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker txtFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBusqueda;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton btnBuscarPor;
        private System.Windows.Forms.ComboBox cmbFiltro;
        private System.Windows.Forms.Label label7;
        private FontAwesome.Sharp.IconButton btnDescargarExcel;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaRegistro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo_documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero_Documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto_Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsuarioRegistro;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Razon_social;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio_Venta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
    }
}