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
using DocumentFormat.OpenXml.Wordprocessing;
using Entidades;

namespace Presentacion
{
    public partial class RDeudoresXFechaXGrado : Form
    {
        DCalendario dCalendario = new DCalendario();
        public RDeudoresXFechaXGrado()
        {
            InitializeComponent();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.ListarDeudoresXFechaXGrado(dtpFecha.Value, cbxGrado.Text != "" ? Convert.ToInt32(cbxGrado.Text) : 0);
        }

        private void cbxGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.ListarDeudoresXFechaXGrado(dtpFecha.Value, cbxGrado.Text != "" ? Convert.ToInt32(cbxGrado.Text) : 0);
        }

        private void RDeudoresXFechaXGrado_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.ListarDeudoresXFechaXGrado(dtpFecha.Value, cbxGrado.Text != "" ? Convert.ToInt32(cbxGrado.Text) : 0);
        }
    }
}
