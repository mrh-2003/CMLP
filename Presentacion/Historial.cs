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
    public partial class Historial : Form
    {
        DHistorial dHistorial = new DHistorial();
        public Historial()
        {
            InitializeComponent();
        }
        private void Historial_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dHistorial.Listar();
        }
        private void txtBuscarPorUsuarioOdescripcion_TextChanged(object sender, EventArgs e)
        {
            dgvListar.DataSource = dHistorial.BuscarPorUsuarioOdescripcion(txtBuscarPorUsuarioOdescripcion.Text);
        }
    }
}
