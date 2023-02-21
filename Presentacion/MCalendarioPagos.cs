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
    public partial class MCalendarioPagos : Form
    {
        DCalendario dCalendario = new DCalendario();
        public MCalendarioPagos()
        {
            InitializeComponent();
        }

        private void MCalendarioPagos_Load(object sender, EventArgs e)
        {
            mostrar();
        }
        void limpiar()
        {
            txtId.Clear();
            cbxDescripcion.SelectedIndex = -1;
            txtMonto.Clear();
            dtpVencimiento.Value = DateTime.Now;
            txtDni.Clear();
            lbNombre.Text = "Nombre: ";
            txtId.Focus();
        }
        void mostrar()
        {
            dgvListar.DataSource = dCalendario.Listar();
            dgvListar.ClearSelection();
            limpiar();
        }
        private void mantenimiento(string opcion)
        {
            if (txtDni.Text != "" && cbxDescripcion.SelectedIndex != -1 && txtMonto.Text != "" && dtpVencimiento.Value != null)
            {
                ECalendario eCalendario = new ECalendario()
                {
                    Id = txtId.Text != "" ? Convert.ToInt32(txtId.Text) : 0,
                    Descripcion = cbxDescripcion.Text,
                    Monto = Convert.ToDecimal(txtMonto.Text),
                    Vencimiento = dtpVencimiento.Value,
                    AlumnoDNI = txtDni.Text
                };
                MessageBox.Show(dCalendario.Mantenimiento(eCalendario, opcion));
                mostrar();
            }
            else
            {
                MessageBox.Show("Todos los campos deben estar llenos");
            }
        }


        private void dgvListar_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            mantenimiento("insert");
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(txtId.Text != "")
                mantenimiento("update");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
                mantenimiento("delete");
        }
    }
}
