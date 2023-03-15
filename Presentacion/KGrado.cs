using Datos;
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
    public partial class KGrado : Form
    {
        DCalendario dCalendario = new DCalendario();
        public KGrado()
        {
            InitializeComponent();
        }

        private void fechahora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
        private void cmbSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnConsultar.PerformClick();
            }
        }

        private void KGrado_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.KardexGeneral();
            txbCancelado.Text = dCalendario.PagadoGeneral().ToString();
            txbPendiente.Text = dCalendario.DeudaGeneral().ToString();
            dgvListar.ClearSelection();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if(cbxGrado.Text != "" && cbxSeccion.Text != "")
            {
                dgvListar.DataSource = dCalendario.KardexXGrado(Convert.ToInt32(cbxGrado.Text), Convert.ToChar(cbxSeccion.Text));
                txbCancelado.Text = dCalendario.PagadoGradoSeccion(Convert.ToInt32(cbxGrado.Text), Convert.ToChar(cbxSeccion.Text)).ToString();
                txbPendiente.Text = dCalendario.DeudaGradoSeccion(Convert.ToInt32(cbxGrado.Text), Convert.ToChar(cbxSeccion.Text)).ToString();
                dgvListar.ClearSelection();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un grado y una sección", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
