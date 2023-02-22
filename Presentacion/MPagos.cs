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
    public partial class MPagos : Form
    {
        DPago dPago = new DPago();
        DHistorial dHistorial = new DHistorial();
        int id = Login.id;
        public MPagos()
        {
            InitializeComponent();
        }

        private void MPagos_Load(object sender, EventArgs e)
        {
            mostrar();
        }
        void limpiar()
        {
            txtId.Clear();
            txtDescripcion.Clear();
            txtMonto.Clear();
            dtpVencimiento.Value = DateTime.Now;
            txtId.Focus();
        }
        void mostrar()
        {
            dgvListar.DataSource = dPago.Listar();
            dgvListar.ClearSelection();
            limpiar();
        }
        private void mantenimiento(string opcion)
        {
            if (txtDescripcion.Text != "" && txtMonto.Text != "" && dtpVencimiento.Value != null)
            {
                EPago ePago = new EPago()
                {
                    Id = txtId.Text != "" ? Convert.ToInt32(txtId.Text) : 0,
                    Descripcion = txtDescripcion.Text,
                    Monto = Convert.ToDecimal(txtMonto.Text),
                    Vencimiento = dtpVencimiento.Value
                };
                string mensaje = dPago.Mantenimiento(ePago, opcion);
                MessageBox.Show(mensaje);
                EHistorial historial = new EHistorial()
                {
                    Descripcion = mensaje,
                    Usuario = (new DUsuario()).getUsuario(id).Usuario,
                    Fecha = DateTime.Now
                };
                dHistorial.Insertar(historial);
                mostrar();
            }
            else
            {
                MessageBox.Show("Todos los campos deben estar llenos");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            mantenimiento("insert");
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
                mantenimiento("update");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
                mantenimiento("delete");
        }

        private void dgvListar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtId.Text = dgvListar.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtDescripcion.Text = dgvListar.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtMonto.Text = dgvListar.Rows[e.RowIndex].Cells[2].Value.ToString();
                dtpVencimiento.Value = Convert.ToDateTime(dgvListar.Rows[e.RowIndex].Cells[3].Value);
            }
        }
    }
}
