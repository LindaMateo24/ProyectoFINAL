namespace ProyectoPrestamo.Modales
{
    partial class mdCancelarPrestamo
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
            this.lbltexto = new System.Windows.Forms.Label();
            this.btncancelar = new FontAwesome.Sharp.IconButton();
            this.btnaceptar = new FontAwesome.Sharp.IconButton();
            this.txtclaveactual = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbltexto
            // 
            this.lbltexto.BackColor = System.Drawing.Color.LightCyan;
            this.lbltexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltexto.Location = new System.Drawing.Point(23, 60);
            this.lbltexto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbltexto.Name = "lbltexto";
            this.lbltexto.Size = new System.Drawing.Size(309, 106);
            this.lbltexto.TabIndex = 72;
            this.lbltexto.Text = "Esta ";
            this.lbltexto.UseCompatibleTextRendering = true;
            // 
            // btncancelar
            // 
            this.btncancelar.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btncancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncancelar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btncancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btncancelar.ForeColor = System.Drawing.Color.Black;
            this.btncancelar.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            this.btncancelar.IconColor = System.Drawing.Color.Black;
            this.btncancelar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btncancelar.IconSize = 20;
            this.btncancelar.Location = new System.Drawing.Point(207, 234);
            this.btncancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(125, 28);
            this.btncancelar.TabIndex = 71;
            this.btncancelar.Text = "Cancelar";
            this.btncancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btncancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btncancelar.UseVisualStyleBackColor = false;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // btnaceptar
            // 
            this.btnaceptar.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnaceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnaceptar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnaceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnaceptar.ForeColor = System.Drawing.Color.Black;
            this.btnaceptar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnaceptar.IconColor = System.Drawing.Color.Black;
            this.btnaceptar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnaceptar.IconSize = 20;
            this.btnaceptar.Location = new System.Drawing.Point(60, 234);
            this.btnaceptar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnaceptar.Name = "btnaceptar";
            this.btnaceptar.Size = new System.Drawing.Size(125, 28);
            this.btnaceptar.TabIndex = 70;
            this.btnaceptar.Text = "Aceptar";
            this.btnaceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnaceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnaceptar.UseVisualStyleBackColor = false;
            this.btnaceptar.Click += new System.EventHandler(this.btnaceptar_Click);
            // 
            // txtclaveactual
            // 
            this.txtclaveactual.Location = new System.Drawing.Point(27, 201);
            this.txtclaveactual.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtclaveactual.Name = "txtclaveactual";
            this.txtclaveactual.PasswordChar = '*';
            this.txtclaveactual.Size = new System.Drawing.Size(304, 22);
            this.txtclaveactual.TabIndex = 69;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.LightCyan;
            this.label10.Location = new System.Drawing.Point(23, 181);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 16);
            this.label10.TabIndex = 68;
            this.label10.Text = "Ingresar Contraseña:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.MediumTurquoise;
            this.label4.Font = new System.Drawing.Font("Lucida Sans", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(20, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(242, 29);
            this.label4.TabIndex = 66;
            this.label4.Text = "Cancelar Contrato";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightCyan;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(9, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(339, 269);
            this.label2.TabIndex = 67;
            // 
            // mdCancelarPrestamo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 284);
            this.Controls.Add(this.lbltexto);
            this.Controls.Add(this.btncancelar);
            this.Controls.Add(this.btnaceptar);
            this.Controls.Add(this.txtclaveactual);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "mdCancelarPrestamo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.mdCancelarPrestamo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbltexto;
        private FontAwesome.Sharp.IconButton btncancelar;
        private FontAwesome.Sharp.IconButton btnaceptar;
        private System.Windows.Forms.TextBox txtclaveactual;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}