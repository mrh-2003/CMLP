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
    public partial class MUsuario : Form
    {
        int id = Login.id;
        DUsuario dUsuario = new DUsuario();
        public MUsuario()
        {
            InitializeComponent();
        }
        void limpiar()
        {
            txtUsuario.Clear();
            txtContraAnt.Clear();
            txtContraNuev.Clear();
            txtConfContNuev.Clear();
            txtUsuario.Focus();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "" && txtContraAnt.Text != "" && txtContraNuev.Text != "" && txtConfContNuev.Text != "")
            {
                EUsuario eUsuario = dUsuario.getUsuario(id);
                if(dUsuario.Login(eUsuario.Usuario, txtContraAnt.Text) != 0 && txtContraNuev.Text == txtConfContNuev.Text && eUsuario.Usuario == txtUsuario.Text)
                {
                    eUsuario.Contrasenia = Utilidades.GetSHA256(txtContraNuev.Text);
                    MessageBox.Show(dUsuario.Mantenimiento(eUsuario, "update"));
                } else
                    MessageBox.Show("Datos incorrectos");
            }
            else
            {
                MessageBox.Show("Todos los campos deben estar llenos");
            }
        }
        private void txtConfContNuev_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnAceptar.PerformClick();
            }
        }
    }
}
