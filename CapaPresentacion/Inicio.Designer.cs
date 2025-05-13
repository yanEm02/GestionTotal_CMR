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
            this.menuUsuario = new FontAwesome.Sharp.IconMenuItem();
            this.menuMantenedor = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuCategoria = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuProducto = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVenta = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuRegistrarVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuVerDetalleVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCompra = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuRegistrarCompra = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuVerDetalleCompra = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCliente = new FontAwesome.Sharp.IconMenuItem();
            this.menuProveedor = new FontAwesome.Sharp.IconMenuItem();
            this.menuReporte = new FontAwesome.Sharp.IconMenuItem();
            this.menuAcercaDe = new FontAwesome.Sharp.IconMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.contenedor = new System.Windows.Forms.Panel();
            this.lblusuario = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuTitulo = new System.Windows.Forms.MenuStrip();
            this.subMenuNegocio = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUsuario,
            this.menuMantenedor,
            this.menuVenta,
            this.menuCompra,
            this.menuCliente,
            this.menuProveedor,
            this.menuReporte,
            this.menuAcercaDe});
            this.menu.Location = new System.Drawing.Point(0, 48);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menu.Size = new System.Drawing.Size(1163, 73);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // menuUsuario
            // 
            this.menuUsuario.IconChar = FontAwesome.Sharp.IconChar.UserGear;
            this.menuUsuario.IconColor = System.Drawing.Color.Black;
            this.menuUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuUsuario.IconSize = 50;
            this.menuUsuario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuUsuario.Name = "menuUsuario";
            this.menuUsuario.Size = new System.Drawing.Size(64, 69);
            this.menuUsuario.Text = "Usuarios";
            this.menuUsuario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuUsuario.Click += new System.EventHandler(this.menuUsuario_Click);
            // 
            // menuMantenedor
            // 
            this.menuMantenedor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuCategoria,
            this.subMenuProducto,
            this.subMenuNegocio});
            this.menuMantenedor.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.menuMantenedor.IconColor = System.Drawing.Color.Black;
            this.menuMantenedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuMantenedor.IconSize = 50;
            this.menuMantenedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuMantenedor.Name = "menuMantenedor";
            this.menuMantenedor.Size = new System.Drawing.Size(84, 69);
            this.menuMantenedor.Text = "Mantenedor";
            this.menuMantenedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuCategoria
            // 
            this.subMenuCategoria.Name = "subMenuCategoria";
            this.subMenuCategoria.Size = new System.Drawing.Size(180, 22);
            this.subMenuCategoria.Text = "Categoria";
            this.subMenuCategoria.Click += new System.EventHandler(this.iconMenuItemToolStripMenuItem_Click);
            // 
            // subMenuProducto
            // 
            this.subMenuProducto.Name = "subMenuProducto";
            this.subMenuProducto.Size = new System.Drawing.Size(180, 22);
            this.subMenuProducto.Text = "Producto";
            this.subMenuProducto.Click += new System.EventHandler(this.subMenuProducto_Click);
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
            this.menuVenta.Size = new System.Drawing.Size(62, 69);
            this.menuVenta.Text = "Ventas";
            this.menuVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuRegistrarVenta
            // 
            this.subMenuRegistrarVenta.Name = "subMenuRegistrarVenta";
            this.subMenuRegistrarVenta.Size = new System.Drawing.Size(129, 22);
            this.subMenuRegistrarVenta.Text = "Registrar";
            this.subMenuRegistrarVenta.Click += new System.EventHandler(this.subMenuRegistrarVenta_Click);
            // 
            // subMenuVerDetalleVenta
            // 
            this.subMenuVerDetalleVenta.Name = "subMenuVerDetalleVenta";
            this.subMenuVerDetalleVenta.Size = new System.Drawing.Size(129, 22);
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
            this.menuCompra.Size = new System.Drawing.Size(67, 69);
            this.menuCompra.Text = "Compras";
            this.menuCompra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // subMenuRegistrarCompra
            // 
            this.subMenuRegistrarCompra.Name = "subMenuRegistrarCompra";
            this.subMenuRegistrarCompra.Size = new System.Drawing.Size(129, 22);
            this.subMenuRegistrarCompra.Text = "Registrar";
            this.subMenuRegistrarCompra.Click += new System.EventHandler(this.subMenuRegistrarCompra_Click);
            // 
            // subMenuVerDetalleCompra
            // 
            this.subMenuVerDetalleCompra.Name = "subMenuVerDetalleCompra";
            this.subMenuVerDetalleCompra.Size = new System.Drawing.Size(129, 22);
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
            this.menuCliente.Size = new System.Drawing.Size(62, 69);
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
            this.menuProveedor.Size = new System.Drawing.Size(84, 69);
            this.menuProveedor.Text = "Proveedores";
            this.menuProveedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuProveedor.Click += new System.EventHandler(this.menuProveedor_Click);
            // 
            // menuReporte
            // 
            this.menuReporte.IconChar = FontAwesome.Sharp.IconChar.BarChart;
            this.menuReporte.IconColor = System.Drawing.Color.Black;
            this.menuReporte.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuReporte.IconSize = 50;
            this.menuReporte.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuReporte.Name = "menuReporte";
            this.menuReporte.Size = new System.Drawing.Size(65, 69);
            this.menuReporte.Text = "Reportes";
            this.menuReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuReporte.Click += new System.EventHandler(this.menuReporte_Click);
            // 
            // menuAcercaDe
            // 
            this.menuAcercaDe.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            this.menuAcercaDe.IconColor = System.Drawing.Color.Black;
            this.menuAcercaDe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuAcercaDe.IconSize = 50;
            this.menuAcercaDe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuAcercaDe.Name = "menuAcercaDe";
            this.menuAcercaDe.Size = new System.Drawing.Size(71, 69);
            this.menuAcercaDe.Text = "Acerca de";
            this.menuAcercaDe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "GestionTotal";
            // 
            // contenedor
            // 
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 121);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1163, 561);
            this.contenedor.TabIndex = 3;
            // 
            // lblusuario
            // 
            this.lblusuario.AutoSize = true;
            this.lblusuario.BackColor = System.Drawing.Color.SteelBlue;
            this.lblusuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusuario.ForeColor = System.Drawing.Color.White;
            this.lblusuario.Location = new System.Drawing.Point(1024, 19);
            this.lblusuario.Name = "lblusuario";
            this.lblusuario.Size = new System.Drawing.Size(61, 15);
            this.lblusuario.TabIndex = 4;
            this.lblusuario.Text = "lblusuario";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.SteelBlue;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(970, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Usuario: ";
            // 
            // menuTitulo
            // 
            this.menuTitulo.AutoSize = false;
            this.menuTitulo.BackColor = System.Drawing.Color.SteelBlue;
            this.menuTitulo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuTitulo.Location = new System.Drawing.Point(0, 0);
            this.menuTitulo.Name = "menuTitulo";
            this.menuTitulo.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuTitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuTitulo.Size = new System.Drawing.Size(1163, 48);
            this.menuTitulo.TabIndex = 1;
            this.menuTitulo.Text = "menuStrip2";
            // 
            // subMenuNegocio
            // 
            this.subMenuNegocio.Name = "subMenuNegocio";
            this.subMenuNegocio.Size = new System.Drawing.Size(180, 22);
            this.subMenuNegocio.Text = "Negocio";
            this.subMenuNegocio.Click += new System.EventHandler(this.subMenuNegocio_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 682);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblusuario);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.menuTitulo);
            this.MainMenuStrip = this.menu;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private FontAwesome.Sharp.IconMenuItem menuAcercaDe;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconMenuItem menuUsuario;
        private FontAwesome.Sharp.IconMenuItem menuMantenedor;
        private FontAwesome.Sharp.IconMenuItem menuVenta;
        private FontAwesome.Sharp.IconMenuItem menuCompra;
        private FontAwesome.Sharp.IconMenuItem menuCliente;
        private FontAwesome.Sharp.IconMenuItem menuProveedor;
        private FontAwesome.Sharp.IconMenuItem menuReporte;
        private System.Windows.Forms.Panel contenedor;
        private System.Windows.Forms.Label lblusuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem subMenuCategoria;
        private System.Windows.Forms.ToolStripMenuItem subMenuProducto;
        private System.Windows.Forms.ToolStripMenuItem subMenuRegistrarVenta;
        private System.Windows.Forms.ToolStripMenuItem subMenuVerDetalleVenta;
        private System.Windows.Forms.ToolStripMenuItem subMenuRegistrarCompra;
        private System.Windows.Forms.ToolStripMenuItem subMenuVerDetalleCompra;
        private System.Windows.Forms.MenuStrip menuTitulo;
        private System.Windows.Forms.ToolStripMenuItem subMenuNegocio;
    }
}

