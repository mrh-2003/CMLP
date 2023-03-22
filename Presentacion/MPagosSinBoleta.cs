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
    public partial class MPagosSinBoleta : Form
    {
        DBancoDTO dBancoDTO = new DBancoDTO();
        public MPagosSinBoleta()
        {
            InitializeComponent();
        }

        private void MPagosSinBoleta_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dBancoDTO.ListarConCalendario();
            dgvListar.ClearSelection();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            List<EBancoDTO> pagos = dgvListar.Rows.Cast<DataGridViewRow>()
           .Where(row => Convert.ToBoolean(row.Cells["cbxEliminar"].Value))
           .Select(row => row.DataBoundItem as EBancoDTO)
           .ToList();
            if(pagos.Count > 0)
            {
                foreach (EBancoDTO banco in pagos)
                    dBancoDTO.Mantenimiento(banco, "delete");
                dgvListar.DataSource = dBancoDTO.ListarConCalendario();
                dgvListar.ClearSelection();
                MessageBox.Show("Pagos eliminados");
            } else
                MessageBox.Show("Debe seleccionar al menos un pago");
        }
    }
}
