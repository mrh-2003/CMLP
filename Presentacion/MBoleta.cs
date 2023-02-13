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
    public partial class MBoleta : Form
    {
        DBoleta dBoleta = new DBoleta();
        public MBoleta()
        {
            InitializeComponent();
        }

        private void MBoleta_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dBoleta.Listar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void dgvListar_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
