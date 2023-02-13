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
            txtCelularMa.Clear();
            txtCelularPa.Clear();
            cbxDescuento.SelectedIndex = -1;
            dtpVencimiento.Value = DateTime.Now;
            txtDni.Focus();
        }

        private void mantenimiento(string opcion)
        {
            if (txtDni.Text != "" && txtApellidosNombres.Text != "" && cbxGrado.SelectedIndex != -1 && 
                cbxSeccion.SelectedIndex != -1 && txtEmail.Text != "" && txtCelular.Text != "" && 
                txtCelularMa.Text != "" && txtCelularPa.Text != "" && cbxDescuento.SelectedIndex != -1 && 
                dtpVencimiento.Value != null)
            {
                EAlumno eAlumno = new EAlumno()
                {
                    Dni = txtDni.Text,
                    ApellidosNombres = txtApellidosNombres.Text,
                    Grado = Convert.ToInt32(cbxGrado.SelectedItem),
                    Seccion = Convert.ToChar(cbxSeccion.SelectedItem),
                    Email = txtEmail.Text,
                    Celular = Convert.ToInt32(txtCelular.Text),
                    CelularMama = Convert.ToInt32(txtCelularMa.Text),
                    CelularPapa = Convert.ToInt32(txtCelularPa.Text),
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
                txtCelular.Text = dgvListar.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtCelularMa.Text = dgvListar.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtCelularPa.Text = dgvListar.Rows[e.RowIndex].Cells[7].Value.ToString();
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
    }
}
