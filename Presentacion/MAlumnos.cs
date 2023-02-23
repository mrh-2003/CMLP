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
        DHistorial dHistorial = new DHistorial();
        int id = Login.id;
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
            txtId.Clear();
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
            txtAnio.Clear();
            txtDni.Focus();
        }
        private void mantenimiento(string opcion)
        {
            if (txtDni.Text != "" && txtApellidosNombres.Text != "" && cbxGrado.SelectedIndex != -1 && 
                cbxSeccion.SelectedIndex != -1 && txtEmail.Text != "" && txtCelular.Text != "" && 
                txtCelulcarAp.Text != "" && txtEmailAp.Text != "" && cbxDescuento.SelectedIndex != -1 && 
                dtpVencimiento.Value != null && txtAnio.Text != "")
            {
                EAlumno eAlumno = new EAlumno()
                {
                    Id = txtId.Text != "" ? Convert.ToInt32(txtId.Text) : 0,
                    Dni = txtDni.Text,
                    ApellidosNombres = txtApellidosNombres.Text,
                    Grado = Convert.ToInt32(cbxGrado.SelectedItem),
                    Seccion = Convert.ToChar(cbxSeccion.SelectedItem),
                    Email = txtEmail.Text,
                    EmailApoderado = txtEmailAp.Text,
                    Celular = Convert.ToInt32(txtCelular.Text),
                    CelularApoderado = Convert.ToInt32(txtCelulcarAp.Text),
                    Descuento = cbxDescuento.SelectedItem.ToString(),
                    FinDescuento = dtpVencimiento.Value,
                    AnioRegistro = Convert.ToInt32(txtAnio.Text)
                };
                string mensaje = dAlumno.Mantenimiento(eAlumno, opcion);
                MessageBox.Show(mensaje);
                //EHistorial historial = new EHistorial()
                //{
                //    Descripcion = mensaje,
                //    Usuario = (new DUsuario()).getUsuario(id).Usuario,
                //    Fecha = DateTime.Now
                //};
                //dHistorial.Insertar(historial);
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
                txtId.Text = dgvListar.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtDni.Text = dgvListar.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtApellidosNombres.Text = dgvListar.Rows[e.RowIndex].Cells[2].Value.ToString();
                cbxGrado.Text = dgvListar.Rows[e.RowIndex].Cells[3].Value.ToString();
                cbxSeccion.Text = dgvListar.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtEmail.Text = dgvListar.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtEmailAp.Text = dgvListar.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtCelular.Text = dgvListar.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtCelulcarAp.Text = dgvListar.Rows[e.RowIndex].Cells[8].Value.ToString();
                cbxDescuento.Text = dgvListar.Rows[e.RowIndex].Cells[9].Value.ToString();
                dtpVencimiento.Value = Convert.ToDateTime(dgvListar.Rows[e.RowIndex].Cells[10].Value);
                txtAnio.Text = dgvListar.Rows[e.RowIndex].Cells[11].Value.ToString();
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            txtAnio.Text = DateTime.Now.Year.ToString();
            EAlumno eAlumno = dAlumno.getAlumno(txtDni.Text);
            if (eAlumno == null)
                mantenimiento("insert");
            else
                MessageBox.Show("El alumno ya existe");
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                EAlumno eAlumno = dAlumno.getAlumnoById(Convert.ToInt32(txtId.Text));
                if (eAlumno != null && eAlumno.Dni == txtDni.Text)
                    mantenimiento("update");
                else
                    MessageBox.Show("El dni no se puede actualizar por seguridad");
            }
            else
                MessageBox.Show("Debe seleccionar un alumno");
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
                mantenimiento("delete");
            else
                MessageBox.Show("Debe seleccionar un alumno");
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
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvListar.DataSource = dAlumno.BuscarPorNombreODNI(txtBuscar.Text);
        }
    }
}
