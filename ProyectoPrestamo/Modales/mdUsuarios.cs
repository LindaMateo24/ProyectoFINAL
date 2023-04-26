using ProyectoPrestamo.Modelo;
using ProyectoPrestamo.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.IdentityModel.Tokens;

namespace ProyectoPrestamo.Modales
{
    public partial class mdUsuarios : Form
    {
        public Usuario oCliente { get; set; }
        public bool Editar { get; set; }
        public string nombre { get; set; } = "Usuario";
        public mdUsuarios()
        {
            InitializeComponent(this.nombre);
        }

        private void mdCliente_Load(object sender, EventArgs e)
        {
            if (oCliente != null) {
                txtclientenombre.Text = oCliente.NombreCompleto;
                txtclientetipodocumento.Text = oCliente.TipoDocumento;
                txtclientedocumento.Text = oCliente.NumeroDocumento;
                txtclientedireccion.Text = oCliente.Direccion;
                txtclienteciudad.Text = oCliente.Ciudad;
                txtclientecorreo.Text = oCliente.Correo;
                txtclientetelefono.Text = oCliente.NumeroTelefono;
                usuario.Text = oCliente.NombreUsuario;

                if (oCliente.Tipo.Replace(" ", "") == "admin") { radioButton1.Checked = true; } else { radioButton2.Checked = true; }
            }else { radioButton1.Checked = true; }
        }

        private void btnaceptar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            string password = string.Empty;
            if (txtclientenombre.Text.Trim() == "" || txtclientetipodocumento.Text.Trim() == "" || txtclientedocumento.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar los campos obligatorios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Editar)
            {
                int _idtemp = oCliente.IdUsuario;
                int nroperacion = UsuarioLogica.Instancia.EditarUsuario(new Usuario()
                {
                    IdUsuario = _idtemp,
                    NombreCompleto = txtclientenombre.Text,
                    TipoDocumento = txtclientetipodocumento.Text,
                    NumeroDocumento = txtclientedocumento.Text,
                    Direccion = txtclientedireccion.Text,
                    Ciudad = txtclienteciudad.Text,
                    Correo = txtclientecorreo.Text,
                    NumeroTelefono = txtclientetelefono.Text,
                    NombreUsuario = usuario.Text,
                    Tipo = radioButton1.Checked ? "admin": "cajero"
                }, out mensaje);
                if (nroperacion < 1)
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    oCliente = new Usuario()
                    {
                        IdUsuario = _idtemp,
                        NombreCompleto = txtclientenombre.Text,
                        TipoDocumento = txtclientetipodocumento.Text,
                        NumeroDocumento = txtclientedocumento.Text,
                        Direccion = txtclientedireccion.Text,
                        Ciudad = txtclienteciudad.Text,
                        Correo = txtclientecorreo.Text,
                        NumeroTelefono = txtclientetelefono.Text,
                        NombreUsuario= usuario.Text,
                        Tipo  = radioButton1.Checked ? "admin" : "cajero"
                    };
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }


            }
            else {
                int idgenerado = UsuarioLogica.Instancia.Registrar(new Usuario() {
                    NombreCompleto = txtclientenombre.Text,
                    TipoDocumento = txtclientetipodocumento.Text,
                    NumeroDocumento = txtclientedocumento.Text,
                    Direccion = txtclientedireccion.Text,
                    Ciudad = txtclienteciudad.Text,
                    Correo = txtclientecorreo.Text,
                    NumeroTelefono = txtclientetelefono.Text,
                    NombreUsuario = usuario.Text,
                    Tipo = radioButton1.Checked ? "admin" : "cajero"
                }, out mensaje,out password);

                if (idgenerado < 1)
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else {
                    oCliente = new Usuario()
                    {
                        IdUsuario = idgenerado,
                        NombreCompleto = txtclientenombre.Text,
                        TipoDocumento = txtclientetipodocumento.Text,
                        NumeroDocumento = txtclientedocumento.Text,
                        Direccion = txtclientedireccion.Text,
                        Ciudad = txtclienteciudad.Text,
                        Correo = txtclientecorreo.Text,
                        NumeroTelefono = txtclientetelefono.Text,
                        NombreUsuario = usuario.Text,
                        Tipo = radioButton1.Checked ? "admin" : "cajero"
                    };
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    MessageBox.Show(password, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
