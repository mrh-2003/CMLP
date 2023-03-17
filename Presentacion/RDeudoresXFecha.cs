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
using Entidades;


namespace Presentacion
{
    public partial class RDeudoresXFecha : Form
    {
        DCalendario dCalendario = new DCalendario();
        public RDeudoresXFecha()
        {
            InitializeComponent();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.ListarDeudoresXFecha(dtpFecha.Value);
            txtTotal.Text = dCalendario.DeudaPorFecha(dtpFecha.Value).ToString();

        }

        private void RDeudoresXFecha_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.ListarDeudoresXFecha(dtpFecha.Value);
            txtTotal.Text = dCalendario.DeudaPorFecha(dtpFecha.Value).ToString();
        }
    }
}
