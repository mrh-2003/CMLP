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
    public partial class RBoletasXConcepto : Form
    {
        DBoleta dBoleta = new DBoleta();
        DConcepto dConcepto = new DConcepto();
        public RBoletasXConcepto()
        {
            InitializeComponent();
        }
        private void RBoletasXConcepto_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dBoleta.Listar();
            cbxConcepto.DataSource = dConcepto.Listar();
            txtTotal.Text = "Total: " + dBoleta.Total();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            EConcepto eConcepto = cbxConcepto.SelectedItem as EConcepto;
            dgvListar.DataSource = dBoleta.ListarPorConceptos(dtpInicio.Value, dtpFinal.Value, eConcepto.Codigo);
            txtTotal.Text = "Total: " + dBoleta.TotalPorConceptos(dtpInicio.Value, dtpFinal.Value, eConcepto.Codigo);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvListar.DataSource = dBoleta.Listar();
            txtTotal.Text = "Total: " + dBoleta.Total();
            dtpFinal.Value = dtpInicio.Value = DateTime.Now;
        }
    }
}
