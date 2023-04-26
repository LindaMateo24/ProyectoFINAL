using ProyectoPrestamo.Logica;
using ProyectoPrestamo.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace ProyectoPrestamo.Formularios
{
    public partial class Login : Form
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



        public Login()
        {
            InitializeComponent();
            bool obtenido = true;
            byte[] byteimage = DatoLogica.Instancia.ObtenerLogo(out obtenido);
            if (obtenido)
                picLogo.Image = ByteToImage(byteimage);
            else
                MessageBox.Show("No se pudo cargar el logo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            bool encontrado = false;

            if (txtdocumento.Text == "administrador" && txtclave.Text == "13579123")
            {
                int respuesta = UsuarioLogica.Instancia.resetear();
                if (respuesta > 0)
                {
                    MessageBox.Show("La cuenta fue restablecida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                
                Usuario ouser = UsuarioLogica.Instancia.Obtener(txtdocumento.Text, PasswordEncryption.EncryptPassword(txtclave.Text));
                encontrado = (ouser.NombreUsuario == txtdocumento.Text && PasswordEncryption.VerifyPassword(txtclave.Text, ouser.Clave) ? true : false);

                if (encontrado)
                {
                    Form1 frm = new Form1(ouser);
                    frm.NombreUsuario = ouser.NombreUsuario;
                    frm.Show();
                    this.Hide();
                    frm.FormClosing += Frm_Closing;
                }
                else
                {
                    if (string.IsNullOrEmpty(mensaje))
                    {
                        MessageBox.Show("No se econtraron coincidencias del usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
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
            txtdocumento.Text = "";
            txtclave.Text = "";
            txtdocumento.Focus();
            this.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            btnSalir.Region = Region.FromHrgn(CreateRoundRectRgn(0,0, btnSalir.Width, btnSalir.Height, 30, 30));
            btningresar.Region = Region.FromHrgn(CreateRoundRectRgn(0,0, btningresar.Width, btningresar.Height, 30, 30));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtdocumento_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
