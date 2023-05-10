using Datos;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class RPagosBancarios : Form
    {
        DPGCU dpgcu = new DPGCU();
        string datos = "";
        int id = -1;
        public RPagosBancarios()
        {
            InitializeComponent();
        }

        private void RPagosBancarios_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dpgcu.ListarPorFecha(dtpInicio.Value, dtpFinal.Value);
            txtTotalXFecha.Text = dpgcu.TotalPorFecha(dtpInicio.Value, dtpFinal.Value).ToString();
            txtTotal.Text = dpgcu.Total().ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            dgvListar.DataSource = dpgcu.ListarPorFecha(dtpInicio.Value, dtpFinal.Value);
            txtTotalXFecha.Text = dpgcu.TotalPorFecha(dtpInicio.Value, dtpFinal.Value).ToString();
            txtTotal.Text = dpgcu.Total().ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(datos != "")
            {
                if (MessageBox.Show("¿Seguro que desea eliminar este pago? \nEsto puede generar errores de datos inconclusos en los reportes"  + datos, "Importante", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EPGCU epgcu = new EPGCU();
                    epgcu.id = id;
                    dpgcu.Mantenimiento(epgcu, "delete");
                }
            }
            else
            {
                MessageBox.Show("Primero debes seleccionar el pago a eliminar");
            }
        }

        private void dgvListar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                datos = "\n" + dgvListar.Rows[e.RowIndex].Cells[0].Value.ToString() + "\n" +
                    dgvListar.Rows[e.RowIndex].Cells[1].Value.ToString() + "\n" +
                    dgvListar.Rows[e.RowIndex].Cells[2].Value.ToString() + "\n" +
                    dgvListar.Rows[e.RowIndex].Cells[3].Value.ToString() + "\n" +
                    dgvListar.Rows[e.RowIndex].Cells[4].Value.ToString() + "\n" +
                    dgvListar.Rows[e.RowIndex].Cells[5].Value.ToString();
                id = Convert.ToInt32(dgvListar.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            txtTotalXFecha.Text = "";
            dgvListar.DataSource = dpgcu.BuscarPorNombreODNI(txtBuscar.Text);
        }
    }
}
