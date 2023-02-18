using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Entidades;

namespace Presentacion
{  
    public partial class MConceptos : Form
    {
        private const string TITULO_ALERTA = "Error de Entrada";
        DConcepto dConcepto = new DConcepto();
        public MConceptos()
        {
            InitializeComponent();
        }

        private void MConceptos_Load(object sender, EventArgs e)
        {
            mostrar();
        }
        void mostrar()
        {
            dgvListar.DataSource = dConcepto.Listar();
            dgvListar.ClearSelection();
            //dgvListar.EnableHeadersVisualStyles = false;
            //dgvListar.ColumnHeadersDefaultCellStyle.BackColor = Color.Cyan;
            //dgvListar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dgvListar.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Gray;
            limpiar();

        }

        void limpiar()
        {
            txtCodigo.Clear();
            txtConcepto.Clear();
            txtImporte.Clear();
            txtCodigo.Focus();
        }

        void mantenimiento(string opcion)
        {
            if (txtCodigo.Text != "" && txtConcepto.Text != "" && txtImporte.Text != "")
            {
                EConcepto eConcepto = new EConcepto()
                {
                    Codigo = Convert.ToInt32(txtCodigo.Text),
                    Concepto = txtConcepto.Text,
                    Importe = Convert.ToDecimal(txtImporte.Text)
                };
                MessageBox.Show(dConcepto.Mantenimiento(eConcepto, opcion));
                mostrar();
            } else
            {
                MessageBox.Show("Todos los campos deben estar llenos");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            mantenimiento("insert");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            mantenimiento("update");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            mantenimiento("delete");
        }

        private void dgvListar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtCodigo.Text = dgvListar.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtConcepto.Text = dgvListar.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtImporte.Text = dgvListar.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void txtConcepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta letras. Introduce un nombre válido",TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
