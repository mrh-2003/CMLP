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
    public partial class RPagosBancarios : Form
    {
        DPGCU dpgcu = new DPGCU();
        public RPagosBancarios()
        {
            InitializeComponent();
        }

        private void RPagosBancarios_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dpgcu.Listar();
        }
    }
}
