using Datos;
using Entidades;
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
        DCalendario dCalendario = new DCalendario();
        public KAlumno()
        {
            InitializeComponent();
        }
        private void fechahora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
        private void txbDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar)&& !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta letras. Introduce un nombre válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txbDni_TextChanged(object sender, EventArgs e)
        {
            if(txbDni.Text.Length == 8)
            {
                dgvListar.DataSource = dCalendario.KardexXAlumno(txbDni.Text);
                dgvListar.ClearSelection();
                EAlumno eAlumno = (new DAlumno()).getAlumno(txbDni.Text);
                lbNombre.Text = eAlumno.ApellidosNombres;
                lbTelefono.Text = eAlumno.Celular.ToString();
                lbEmail.Text = eAlumno.Email.ToString();
                lblAnio.Text = eAlumno.Grado.ToString();
                lblSeccion.Text = eAlumno.Seccion.ToString();
                txbCancelado.Text = dCalendario.PagadoPorAlumno(txbDni.Text).ToString();
                txbPendiente.Text = dCalendario.DeudaPorAlumno(txbDni.Text).ToString();
            }
        }

        private void KAlumno_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.KardexGeneral();
            txbCancelado.Text = dCalendario.PagadoGeneral().ToString();
            txbPendiente.Text = dCalendario.DeudaGeneral().ToString();
            dgvListar.ClearSelection();
        }
    }
}
