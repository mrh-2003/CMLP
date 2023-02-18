using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class CAlumnos : Form
    {
        private const string TITULO_ALERTA = "Error de Entrada";
        public CAlumnos()
        {
            InitializeComponent();
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtApellidosNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta letras. Introduce un nombre válido válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void txtCelulcarAp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
