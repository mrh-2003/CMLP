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
    public partial class MConfiguracion : Form
    {
        DColegio dColegio = new DColegio();
        public MConfiguracion()
        {
            InitializeComponent();
        }

        private void MConfiguracion_Load(object sender, EventArgs e)
        {
            EColegio colegio = dColegio.getColegio();
            if (colegio != null)
            {
                txtEmail.Text = colegio.Email;
                txtContrasenia.Text = colegio.Contrasenia;
                txtCelular.Text = colegio.Numero;
                txtMora.Text = colegio.Mora.ToString();
                txtEmail.Focus();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text != "" && txtContrasenia.Text != "" && txtCelular.Text != "" && txtMora.Text != "")
            {
                EColegio colegio = new EColegio();
                colegio.Email = txtEmail.Text;
                colegio.Contrasenia = txtContrasenia.Text;
                colegio.Numero = txtCelular.Text;
                colegio.Mora = Convert.ToDecimal(txtMora.Text);
                string mensaje = dColegio.Mantenimineto(colegio);
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Debe completar todos los campos");
        }
        private void txtMora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnActualizar.PerformClick();
            }
        }
    }
}
