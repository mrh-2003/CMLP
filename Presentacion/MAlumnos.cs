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
    public partial class MAlumnos : Form
    {
        private const string TITULO_ALERTA = "Error de Entrada";
        DAlumno dAlumno = new DAlumno();
        public MAlumnos()
        {
            InitializeComponent();
        }

        private void MAlumnos_Load(object sender, EventArgs e)
        {
            mostrar();
        }

        void mostrar()
        {
            dgvListar.DataSource = dAlumno.Listar();
            dgvListar.ClearSelection();
            limpiar();
        }

        void limpiar()
        {
            txtDni.Clear();
            txtApellidosNombres.Clear();
            cbxGrado.SelectedIndex = -1;
            cbxSeccion.SelectedIndex = -1;
            txtEmail.Clear();
            txtCelular.Clear();
            txtCelulcarAp.Clear();
            txtEmailAp.Clear();
            cbxDescuento.SelectedIndex = -1;
            dtpVencimiento.Value = DateTime.Now;
            txtDni.Focus();
        }

        private void mantenimiento(string opcion)
        {
            if (txtDni.Text != "" && txtApellidosNombres.Text != "" && cbxGrado.SelectedIndex != -1 && 
                cbxSeccion.SelectedIndex != -1 && txtEmail.Text != "" && txtCelular.Text != "" && 
                txtCelulcarAp.Text != "" && txtEmailAp.Text != "" && cbxDescuento.SelectedIndex != -1 && 
                dtpVencimiento.Value != null)
            {
                EAlumno eAlumno = new EAlumno()
                {
                    Dni = txtDni.Text,
                    ApellidosNombres = txtApellidosNombres.Text,
                    Grado = Convert.ToInt32(cbxGrado.SelectedItem),
                    Seccion = Convert.ToChar(cbxSeccion.SelectedItem),
                    Email = txtEmail.Text,
                    EmailApoderado = txtEmailAp.Text,
                    Celular = Convert.ToInt32(txtCelular.Text),
                    CelularApoderado = Convert.ToInt32(txtCelulcarAp.Text),
                    Descuento = cbxDescuento.SelectedItem.ToString(),
                    FinDescuento = dtpVencimiento.Value
                };
                MessageBox.Show(dAlumno.Mantenimiento(eAlumno, opcion));
                mostrar();
            }
            else
            {
                MessageBox.Show("Todos los campos deben estar llenos");
            }
        }


        private void dgvListar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtDni.Text = dgvListar.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtApellidosNombres.Text = dgvListar.Rows[e.RowIndex].Cells[1].Value.ToString();
                cbxGrado.Text = dgvListar.Rows[e.RowIndex].Cells[2].Value.ToString();
                cbxSeccion.Text = dgvListar.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtEmail.Text = dgvListar.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtEmailAp.Text = dgvListar.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtCelular.Text = dgvListar.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtCelulcarAp.Text = dgvListar.Rows[e.RowIndex].Cells[7].Value.ToString();
                cbxDescuento.Text = dgvListar.Rows[e.RowIndex].Cells[8].Value.ToString();
                dtpVencimiento.Value = Convert.ToDateTime(dgvListar.Rows[e.RowIndex].Cells[9].Value);
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

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtApellidosNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta letras. Introduce un nombre válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void txtCelulcarAp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
