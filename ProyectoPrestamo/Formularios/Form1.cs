using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoPrestamo.Formularios;
using ProyectoPrestamo.Logica;
using ProyectoPrestamo.Modales;
using ProyectoPrestamo.Modelo;

namespace ProyectoPrestamo
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
           int nLeftRect,
           int nTopRect,
           int nRightRect,
           int nBottomRect,
           int nWidthE11ipse,
           int nHeightEIIipse
        );
        private Usuario usuario1 = new Usuario();
        public string NombreUsuario { get; set; }
        public Form1( Usuario usuario)
        {
            this.usuario1 = usuario;
            InitializeComponent();

            if (this.usuario1.Tipo.Replace(" ","") == "cajero")
            {
                btnUsuarios.Visible = false;
                btnregistrarprestamo.Visible = false;
                btnclientes.Visible = false;
                btnresumengeneral.Visible = false;
                btnconfiguracion.Visible = false;
            }
            else
            {
                btnUsuarios.Visible = true;
                btnregistrarprestamo.Visible = true;
                btnclientes.Visible = true;
                btnresumengeneral.Visible = true;
                btnconfiguracion.Visible = true;
            }


            bool obtenido = true;
            byte[] byteimage = DatoLogica.Instancia.ObtenerLogo(out obtenido);
          
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //lblusuario.Text = string.Format("Usuario: {0}", NombreUsuario);
        }
        public Image ByteToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = new Bitmap(ms);
            return image;
        }

        private void Frm_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnregistrarprestamo_Click(object sender, EventArgs e)
        {
            frmRegistrarPrestamo frm = new frmRegistrarPrestamo(this.usuario1);
            frm.Show();
            this.Hide();
            frm.FormClosing += Frm_Closing;
        }

        private void btnregistrarcobro_Click(object sender, EventArgs e)
        {
            frmRegistrarCobro frm = new frmRegistrarCobro(this.usuario1);
            frm.Show();
            this.Hide();
            frm.FormClosing += Frm_Closing;
        }

        private void btnhistorialprestamo_Click(object sender, EventArgs e)
        {
            frmHistorialPrestamo frm = new frmHistorialPrestamo(this.usuario1);
            frm.Show();
            this.Hide();
            frm.FormClosing += Frm_Closing;
        }

        private void btnclientes_Click(object sender, EventArgs e)
        {
            frmClientes frm = new frmClientes(this.usuario1);
            frm.Show();
            this.Hide();
            frm.FormClosing += Frm_Closing;
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuarios frm = new frmUsuarios(this.usuario1);
            frm.Show();
            this.Hide();
            frm.FormClosing += Frm_Closing;
        }

        private void btnconfiguracion_Click(object sender, EventArgs e)
        {
            frmConfiguracion frm = new frmConfiguracion(this, this.usuario1);
            frm.Show();
            this.Hide();
            frm.FormClosing += Frm_Closing;
        }

        private void btnresumengeneral_Click(object sender, EventArgs e)
        {
            frmResumenGeneral frm = new frmResumenGeneral(this.usuario1);
            frm.Show();
            this.Hide();
            frm.FormClosing += Frm_Closing;
        }
    }
}
