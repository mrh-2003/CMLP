using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;

namespace Presentacion
{
    public partial class KGeneral : Form
    {
        DCalendario dCalendario = new DCalendario();
        public KGeneral()
        {
            InitializeComponent();
        }

        private void KGeneral_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.KardexGeneral();
            txbCancelado.Text = dCalendario.PagadoGeneral().ToString(); 
            txbPendiente.Text = dCalendario.DeudaGeneral().ToString();
            dgvListar.ClearSelection();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.BuscarKardexGeneral(txtBuscar.Text);
            txbCancelado.Text = dCalendario.PagadoBusquedaGeneral(txtBuscar.Text).ToString();
            txbPendiente.Text = dCalendario.DeudaBusquedaGeneral(txtBuscar.Text).ToString();
            dgvListar.ClearSelection();
        }
    }
}
