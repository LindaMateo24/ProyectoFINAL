namespace ProyectoPrestamo.Formularios
{
    partial class frmTipoMoneda
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTipoMoneda));
            this.dgvdata = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Divisa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Abreviatura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btneditar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btneliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnregistrar = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdata)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvdata
            // 
            this.dgvdata.AllowUserToAddRows = false;
            this.dgvdata.BackgroundColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Divisa,
            this.Abreviatura,
            this.btneditar,
            this.btneliminar});
            this.dgvdata.Location = new System.Drawing.Point(18, 103);
            this.dgvdata.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvdata.MultiSelect = false;
            this.dgvdata.Name = "dgvdata";
            this.dgvdata.ReadOnly = true;
            this.dgvdata.RowHeadersWidth = 62;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvdata.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvdata.RowTemplate.Height = 28;
            this.dgvdata.Size = new System.Drawing.Size(812, 388);
            this.dgvdata.TabIndex = 96;
            this.dgvdata.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvdata_CellContentClick);
            this.dgvdata.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvdata_CellPainting);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 8;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 150;
            // 
            // Divisa
            // 
            this.Divisa.HeaderText = "Divisa";
            this.Divisa.MinimumWidth = 8;
            this.Divisa.Name = "Divisa";
            this.Divisa.ReadOnly = true;
            this.Divisa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Divisa.Width = 110;
            // 
            // Abreviatura
            // 
            this.Abreviatura.HeaderText = "Abreviatura";
            this.Abreviatura.MinimumWidth = 8;
            this.Abreviatura.Name = "Abreviatura";
            this.Abreviatura.ReadOnly = true;
            this.Abreviatura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Abreviatura.Width = 150;
            // 
            // btneditar
            // 
            this.btneditar.HeaderText = "";
            this.btneditar.MinimumWidth = 8;
            this.btneditar.Name = "btneditar";
            this.btneditar.ReadOnly = true;
            this.btneditar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btneditar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btneditar.Width = 35;
            // 
            // btneliminar
            // 
            this.btneliminar.HeaderText = "";
            this.btneliminar.MinimumWidth = 8;
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.ReadOnly = true;
            this.btneliminar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btneliminar.Width = 35;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(18, 74);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(813, 15);
            this.groupBox4.TabIndex = 95;
            this.groupBox4.TabStop = false;
            // 
            // btnregistrar
            // 
            this.btnregistrar.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnregistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnregistrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnregistrar.ForeColor = System.Drawing.Color.Black;
            this.btnregistrar.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnregistrar.IconColor = System.Drawing.Color.Black;
            this.btnregistrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnregistrar.IconSize = 19;
            this.btnregistrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnregistrar.Location = new System.Drawing.Point(18, 25);
            this.btnregistrar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnregistrar.Name = "btnregistrar";
            this.btnregistrar.Size = new System.Drawing.Size(192, 40);
            this.btnregistrar.TabIndex = 94;
            this.btnregistrar.Text = "Registrar Nuevo";
            this.btnregistrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnregistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnregistrar.UseVisualStyleBackColor = false;
            this.btnregistrar.Click += new System.EventHandler(this.btnregistrar_Click);
            // 
            // frmTipoMoneda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.dgvdata);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnregistrar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmTipoMoneda";
            this.Tag = "Tipo de Moneda";
            this.Text = "frmTipoMoneda";
            this.Load += new System.EventHandler(this.frmTipoMoneda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvdata;
        private System.Windows.Forms.GroupBox groupBox4;
        private FontAwesome.Sharp.IconButton btnregistrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Divisa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Abreviatura;
        private System.Windows.Forms.DataGridViewButtonColumn btneditar;
        private System.Windows.Forms.DataGridViewButtonColumn btneliminar;
    }
}