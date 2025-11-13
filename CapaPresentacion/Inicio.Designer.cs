namespace CapaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuVenta = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuRegistrarVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuVerDetalleVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCompra = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuRegistrarCompra = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuVerDetalleCompra = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCliente = new FontAwesome.Sharp.IconMenuItem();
            this.menuProveedor = new FontAwesome.Sharp.IconMenuItem();
            this.menuInventario = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuCategoria = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporte = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuReporteCompra = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuReporteVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUsuario = new FontAwesome.Sharp.IconMenuItem();
            this.menuMantenedor = new FontAwesome.Sharp.IconMenuItem();
            this.menuAcercaDe = new FontAwesome.Sharp.IconMenuItem();
            this.contenedor = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.SandyBrown;
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuVenta,
            this.menuCompra,
            this.menuCliente,
            this.menuProveedor,
            this.menuInventario,
            this.menuReporte,
            this.menuUsuario,
            this.menuMantenedor,
            this.menuAcercaDe});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menu.Size = new System.Drawing.Size(1129, 78);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // menuVenta
            // 
            this.menuVenta.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuRegistrarVenta,
            this.subMenuVerDetalleVenta});
            this.menuVenta.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.menuVenta.IconColor = System.Drawing.Color.Black;
            this.menuVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVenta.IconSize = 50;
            this.menuVenta.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuVenta.Name = "menuVenta";
            this.menuVenta.Size = new System.Drawing.Size(66, 74);
            this.menuVenta.Text = "Ventas";
            this.menuVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuRegistrarVenta
            // 
            this.subMenuRegistrarVenta.Name = "subMenuRegistrarVenta";
            this.subMenuRegistrarVenta.Size = new System.Drawing.Size(165, 26);
            this.subMenuRegistrarVenta.Text = "Registrar";
            this.subMenuRegistrarVenta.Click += new System.EventHandler(this.subMenuRegistrarVenta_Click);
            // 
            // subMenuVerDetalleVenta
            // 
            this.subMenuVerDetalleVenta.Name = "subMenuVerDetalleVenta";
            this.subMenuVerDetalleVenta.Size = new System.Drawing.Size(165, 26);
            this.subMenuVerDetalleVenta.Text = "Ver Detalle";
            this.subMenuVerDetalleVenta.Click += new System.EventHandler(this.subMenuVerDetalleVenta_Click);
            // 
            // menuCompra
            // 
            this.menuCompra.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuRegistrarCompra,
            this.subMenuVerDetalleCompra});
            this.menuCompra.IconChar = FontAwesome.Sharp.IconChar.DollyFlatbed;
            this.menuCompra.IconColor = System.Drawing.Color.Black;
            this.menuCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuCompra.IconSize = 50;
            this.menuCompra.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuCompra.Name = "menuCompra";
            this.menuCompra.Size = new System.Drawing.Size(82, 74);
            this.menuCompra.Text = "Compras";
            this.menuCompra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuRegistrarCompra
            // 
            this.subMenuRegistrarCompra.Name = "subMenuRegistrarCompra";
            this.subMenuRegistrarCompra.Size = new System.Drawing.Size(165, 26);
            this.subMenuRegistrarCompra.Text = "Registrar";
            this.subMenuRegistrarCompra.Click += new System.EventHandler(this.subMenuRegistrarCompra_Click);
            // 
            // subMenuVerDetalleCompra
            // 
            this.subMenuVerDetalleCompra.Name = "subMenuVerDetalleCompra";
            this.subMenuVerDetalleCompra.Size = new System.Drawing.Size(165, 26);
            this.subMenuVerDetalleCompra.Text = "Ver Detalle";
            this.subMenuVerDetalleCompra.Click += new System.EventHandler(this.subMenuVerDetalleCompra_Click);
            // 
            // menuCliente
            // 
            this.menuCliente.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.menuCliente.IconColor = System.Drawing.Color.Black;
            this.menuCliente.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuCliente.IconSize = 50;
            this.menuCliente.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuCliente.Name = "menuCliente";
            this.menuCliente.Size = new System.Drawing.Size(75, 74);
            this.menuCliente.Text = "Clientes";
            this.menuCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuCliente.Click += new System.EventHandler(this.menuCliente_Click);
            // 
            // menuProveedor
            // 
            this.menuProveedor.IconChar = FontAwesome.Sharp.IconChar.AddressCard;
            this.menuProveedor.IconColor = System.Drawing.Color.Black;
            this.menuProveedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuProveedor.IconSize = 50;
            this.menuProveedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuProveedor.Name = "menuProveedor";
            this.menuProveedor.Size = new System.Drawing.Size(105, 74);
            this.menuProveedor.Text = "Proveedores";
            this.menuProveedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuProveedor.Click += new System.EventHandler(this.menuProveedor_Click);
            // 
            // menuInventario
            // 
            this.menuInventario.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuCategoria});
            this.menuInventario.IconChar = FontAwesome.Sharp.IconChar.BoxesPacking;
            this.menuInventario.IconColor = System.Drawing.Color.Black;
            this.menuInventario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuInventario.IconSize = 50;
            this.menuInventario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuInventario.Name = "menuInventario";
            this.menuInventario.Size = new System.Drawing.Size(89, 74);
            this.menuInventario.Text = "Inventario";
            this.menuInventario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuInventario.Click += new System.EventHandler(this.menuInventario_Click);
            // 
            // subMenuCategoria
            // 
            this.subMenuCategoria.Name = "subMenuCategoria";
            this.subMenuCategoria.Size = new System.Drawing.Size(224, 26);
            this.subMenuCategoria.Text = "Categoria";
            this.subMenuCategoria.Click += new System.EventHandler(this.categoriaToolStripMenuItem_Click);
            // 
            // menuReporte
            // 
            this.menuReporte.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuReporteCompra,
            this.subMenuReporteVenta});
            this.menuReporte.IconChar = FontAwesome.Sharp.IconChar.BarChart;
            this.menuReporte.IconColor = System.Drawing.Color.Black;
            this.menuReporte.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuReporte.IconSize = 50;
            this.menuReporte.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuReporte.Name = "menuReporte";
            this.menuReporte.Size = new System.Drawing.Size(82, 74);
            this.menuReporte.Text = "Reportes";
            this.menuReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuReporteCompra
            // 
            this.subMenuReporteCompra.Name = "subMenuReporteCompra";
            this.subMenuReporteCompra.Size = new System.Drawing.Size(224, 26);
            this.subMenuReporteCompra.Text = "Reporte Compras";
            this.subMenuReporteCompra.Click += new System.EventHandler(this.subMenuReporteCompra_Click);
            // 
            // subMenuReporteVenta
            // 
            this.subMenuReporteVenta.Name = "subMenuReporteVenta";
            this.subMenuReporteVenta.Size = new System.Drawing.Size(224, 26);
            this.subMenuReporteVenta.Text = "Reporte Ventas";
            this.subMenuReporteVenta.Click += new System.EventHandler(this.subMenuReporteVenta_Click);
            // 
            // menuUsuario
            // 
            this.menuUsuario.IconChar = FontAwesome.Sharp.IconChar.UserGear;
            this.menuUsuario.IconColor = System.Drawing.Color.Black;
            this.menuUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuUsuario.IconSize = 50;
            this.menuUsuario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuUsuario.Name = "menuUsuario";
            this.menuUsuario.Size = new System.Drawing.Size(79, 74);
            this.menuUsuario.Text = "Usuarios";
            this.menuUsuario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuUsuario.Click += new System.EventHandler(this.menuUsuario_Click);
            // 
            // menuMantenedor
            // 
            this.menuMantenedor.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.menuMantenedor.IconColor = System.Drawing.Color.Black;
            this.menuMantenedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuMantenedor.IconSize = 50;
            this.menuMantenedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuMantenedor.Name = "menuMantenedor";
            this.menuMantenedor.Size = new System.Drawing.Size(116, 74);
            this.menuMantenedor.Text = "Configuracion";
            this.menuMantenedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuMantenedor.Click += new System.EventHandler(this.menuMantenedor_Click);
            // 
            // menuAcercaDe
            // 
            this.menuAcercaDe.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            this.menuAcercaDe.IconColor = System.Drawing.Color.Black;
            this.menuAcercaDe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuAcercaDe.IconSize = 50;
            this.menuAcercaDe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuAcercaDe.Name = "menuAcercaDe";
            this.menuAcercaDe.Size = new System.Drawing.Size(89, 74);
            this.menuAcercaDe.Text = "Acerca de";
            this.menuAcercaDe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuAcercaDe.Click += new System.EventHandler(this.menuAcercaDe_Click);
            // 
            // contenedor
            // 
            this.contenedor.BackColor = System.Drawing.Color.White;
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 78);
            this.contenedor.Margin = new System.Windows.Forms.Padding(4);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1129, 599);
            this.contenedor.TabIndex = 3;
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1129, 677);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ComerciaPlus";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private FontAwesome.Sharp.IconMenuItem menuAcercaDe;
        private FontAwesome.Sharp.IconMenuItem menuUsuario;
        private FontAwesome.Sharp.IconMenuItem menuMantenedor;
        private FontAwesome.Sharp.IconMenuItem menuVenta;
        private FontAwesome.Sharp.IconMenuItem menuCompra;
        private FontAwesome.Sharp.IconMenuItem menuCliente;
        private FontAwesome.Sharp.IconMenuItem menuProveedor;
        private FontAwesome.Sharp.IconMenuItem menuReporte;
        private System.Windows.Forms.Panel contenedor;
        private System.Windows.Forms.ToolStripMenuItem subMenuRegistrarVenta;
        private System.Windows.Forms.ToolStripMenuItem subMenuVerDetalleVenta;
        private System.Windows.Forms.ToolStripMenuItem subMenuRegistrarCompra;
        private System.Windows.Forms.ToolStripMenuItem subMenuVerDetalleCompra;
        private System.Windows.Forms.ToolStripMenuItem subMenuReporteCompra;
        private System.Windows.Forms.ToolStripMenuItem subMenuReporteVenta;
        private FontAwesome.Sharp.IconMenuItem menuInventario;
        private System.Windows.Forms.ToolStripMenuItem subMenuCategoria;
    }
}

