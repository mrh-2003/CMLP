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
                this.Hide();
                (new Home()).ShowDialog();
            }
            else
                MessageBox.Show("Credenciales incompletas o incorrectas");
        }
    }
}
