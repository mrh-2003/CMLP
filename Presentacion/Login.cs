using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Datos;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace Presentacion
{
    public partial class Login : Form
    {
        DUsuario dUsuario = new DUsuario();
        public static int id;
        public Login()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //EUsuario user = new EUsuario()
            //{
            //    Usuario = txtUsuario.Text,
            //    Contrasenia = Utilidades.GetSHA256(txtContrasenia.Text),
            //    Rol = "Administrador"
            //};
            //MessageBox.Show(dUsuario.Mantenimiento(user, "insert"));
            id = dUsuario.Login(txtUsuario.Text, txtContrasenia.Text);
            if (id != 0)
            {
                Utilidades.anio = cbxAnio.Text;
                this.Hide();
                (new Home()).ShowDialog();
            }
            else
                MessageBox.Show("Credenciales incompletas o incorrectas");
        }
        private void txtContrasenia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnLogin.PerformClick();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year - 3; i < DateTime.Now.Year + 3; i++)
                cbxAnio.Items.Add(i);
            cbxAnio.Items.Add("TODOS");
            cbxAnio.SelectedIndex = 3;
        }
    }
}
