namespace CapaPresentacion.Sub_Forms
{
    partial class subFormAcercaDe
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imgCentral = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgCentral)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 132);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sistema de Información para la Gestión de los Procesos de Compra y Venta de las M" +
    "IPYMES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 465);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Developed By: YanSoriano";
            // 
            // imgCentral
            // 
            this.imgCentral.Image = global::CapaPresentacion.Properties.Resources.ComerciaPlus_Logo;
            this.imgCentral.Location = new System.Drawing.Point(53, 214);
            this.imgCentral.Margin = new System.Windows.Forms.Padding(4);
            this.imgCentral.Name = "imgCentral";
            this.imgCentral.Size = new System.Drawing.Size(266, 143);
            this.imgCentral.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgCentral.TabIndex = 2;
            this.imgCentral.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(104, 386);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = "Version: 1.0.0";
            // 
            // subFormAcercaDe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(368, 551);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.imgCentral);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "subFormAcercaDe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "subFormAcercaDe";
            ((System.ComponentModel.ISupportInitialize)(this.imgCentral)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox imgCentral;
        private System.Windows.Forms.Label label3;
    }
}