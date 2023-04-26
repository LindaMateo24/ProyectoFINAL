﻿using Microsoft.IdentityModel.Tokens;
using ProyectoPrestamo.Herramientas;
using ProyectoPrestamo.Logica;
using ProyectoPrestamo.Modales;
using ProyectoPrestamo.Modelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoPrestamo.Formularios
{
    public partial class frmUsuarios : Form
    {
        private string Nombre = "Lista de Usuarios";
        Usuario user = new Usuario();
       
        public frmUsuarios(Usuario user)
        {
            this.user = user;
            InitializeComponent(this.Nombre);

          
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {

            List<Usuario> lista = UsuarioLogica.Instancia.Listar();
               
            foreach (Usuario cl in lista)
            {
                dgvdata.Rows.Add(new object[] {
                    cl.IdUsuario,
                    cl.NombreCompleto,
                    cl.TipoDocumento,
                    cl.NumeroDocumento,
                    cl.Direccion,
                    cl.Ciudad,
                    cl.Correo,
                    cl.NumeroTelefono,
                    cl.Tipo,
                    cl.NombreUsuario,
                    "",
                    ""
                });
            }
                
            foreach (DataGridViewColumn cl in dgvdata.Columns)
            {
                if (cl.Visible == true && cl.Name != "btneditar" && cl.Name != "btneliminar")
                {
                    cbobuscar.Items.Add(new OpcionCombo() { Valor = cl.Name, Texto = cl.HeaderText });
                }
            }
            
            cbobuscar.DisplayMember = "Texto";
            cbobuscar.ValueMember = "Valor";
            cbobuscar.SelectedIndex = 0;

        }

        private void btnregistrarnuevo_Click(object sender, EventArgs e)
        {
            using (var form = new mdUsuarios())
            {
                form.Editar = false;
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dgvdata.Rows.Add(new object[] {
                        form.oCliente.IdUsuario,
                        form.oCliente.NombreCompleto,
                        form.oCliente.TipoDocumento,
                        form.oCliente.NumeroDocumento,
                        form.oCliente.Direccion,
                        form.oCliente.Ciudad,
                        form.oCliente.Correo,
                        form.oCliente.NumeroTelefono,
                        "",
                        ""
                    });
                }
            }
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 10)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.edit16.Width;
                var h = Properties.Resources.edit16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.edit16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }

            if (e.ColumnIndex == 11)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.delete16.Width;
                var h = Properties.Resources.delete16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index >= 0)
            {
                if (dgvdata.Columns[e.ColumnIndex].Name == "btneditar")
                {
                    using (var form = new mdUsuarios())
                    {
                        form.Editar = true;

                        form.oCliente = new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dgvdata.Rows[index].Cells["Id"].Value.ToString()),
                            NombreUsuario = dgvdata.Rows[index].Cells["Usuario"].Value.ToString(),
                            Clave = "******",
                            Tipo = dgvdata.Rows[index].Cells["Tipo"].Value.ToString(),
                            NombreCompleto = dgvdata.Rows[index].Cells["NombreCompleto"].Value.ToString(),
                            TipoDocumento = dgvdata.Rows[index].Cells["TipoDocumento"].Value.ToString(),
                            NumeroDocumento = dgvdata.Rows[index].Cells["NumeroDocumento"].Value.ToString(),
                            Direccion = dgvdata.Rows[index].Cells["Direccion"].Value.ToString(),
                            Ciudad = dgvdata.Rows[index].Cells["Ciudad"].Value.ToString(),
                            Correo = dgvdata.Rows[index].Cells["Correo"].Value.ToString(),
                            NumeroTelefono = dgvdata.Rows[index].Cells["NumeroTelefono"].Value.ToString()
                        };

                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            dgvdata.Rows[index].Cells["NombreCompleto"].Value = form.oCliente.NombreCompleto;
                            dgvdata.Rows[index].Cells["TipoDocumento"].Value = form.oCliente.TipoDocumento;
                            dgvdata.Rows[index].Cells["NumeroDocumento"].Value = form.oCliente.NumeroDocumento;
                            dgvdata.Rows[index].Cells["Direccion"].Value = form.oCliente.Direccion;
                            dgvdata.Rows[index].Cells["Ciudad"].Value = form.oCliente.Ciudad;
                            dgvdata.Rows[index].Cells["Correo"].Value = form.oCliente.Correo;
                            dgvdata.Rows[index].Cells["NumeroTelefono"].Value = form.oCliente.NumeroTelefono;
                            dgvdata.Rows[index].Cells["Tipo"].Value = form.oCliente.NombreUsuario;
                            dgvdata.Rows[index].Cells["Usuario"].Value = form.oCliente.Tipo;

                        }
                    }

                }
                else if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
                {

                    if (MessageBox.Show("¿Desea eliminar el cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int _id = int.Parse(dgvdata.Rows[index].Cells["Id"].Value.ToString());
                        string mensaje = string.Empty;

                        int respuesta = ClienteLogica.Instancia.Eliminar(_id, this.Nombre.Contains("Usuarios") ? "Usuarios" : "");

                        if (respuesta > 0)
                        {
                            dgvdata.Rows.RemoveAt(index);
                        }
                        else
                            MessageBox.Show("No se pudo eliminar el cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobuscar.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbuscar.Text.ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}


