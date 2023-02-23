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
    public partial class KAlumno : Form
    {
        private const string TITULO_ALERTA = "Error de Entrada";
        public KAlumno()
        {
            InitializeComponent();
        }
        private void fechahora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
        private void txbDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnConsultar.PerformClick();
            }
        }
        private void txbDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar)&& !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta letras. Introduce un nombre válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void tbxAlumno_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (char.IsNumber(e.KeyChar))
                {
                    e.Handled = true;
                    MessageBox.Show("Este campo solo acepta letras. Introduce un nombre válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
        }
        private void txbTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta letras. Introduce un nombre válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } 
        }
    }
}
