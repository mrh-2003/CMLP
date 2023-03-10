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
        private const string TITULO_ALERTA = "Error de Entrada";
        DBoleta dBoleta = new DBoleta();
        DConcepto dConcepto = new DConcepto();
        DAlumno dAlumno = new DAlumno();
        DHistorial dHistorial = new DHistorial();
        int id = Login.id;
        public MBoleta()
        {
            InitializeComponent();
        }
        private void MBoleta_Load(object sender, EventArgs e)
        {
            cbxConcepto.DataSource = dConcepto.Listar();
            mostrar();
        }
        void mostrar()
        {
            dgvListar.DataSource = dBoleta.Listar();
            dgvListar.ClearSelection();
            limpiar();
        }

        private void limpiar()
        {
            txtCodigo.Clear();
            txtDni.Clear();
            txtMonto.Clear();
            dtpFecha.Value = DateTime.Now;
            cbxConcepto.SelectedIndex = -1;
            lbNombre.Text = "Nombre: ";
            txtCodigo.Focus();
        }
        private void mantenimiento(string opcion)
        {
            if (txtCodigo.Text != "" && txtDni.Text != "" && txtMonto.Text != "" && dtpFecha.Value != null && cbxConcepto.SelectedIndex != -1)
            {
                EConcepto eConcepto = cbxConcepto.SelectedItem as EConcepto;
                EAlumno eAlumno = dAlumno.getAlumno(txtDni.Text);
                if (eAlumno != null)
                {
                    EBoleta eBoleta = new EBoleta()
                    {
                        Codigo = txtCodigo.Text,
                        AlumnoId = eAlumno.Id,
                        Monto = Convert.ToDecimal(txtMonto.Text),
                        Fecha = dtpFecha.Value,
                        ConceptoCodigo = eConcepto.Codigo
                    };
                    string mensaje = dBoleta.Mantenimiento(eBoleta, opcion);
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
                    MessageBox.Show("El alumno no existe");
            }
            else
                MessageBox.Show("Todos los campos deben estar llenos");
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            mantenimiento("insert");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            EAlumno eAlumno = dAlumno.getAlumno(txtDni.Text);
            EBoleta eBoleta = dBoleta.getBoleta(txtCodigo.Text);
            if (eBoleta != null && eAlumno != null && eBoleta.AlumnoId == eAlumno.Id)
                mantenimiento("update");
            else
                MessageBox.Show("No se puede modificar esos datos");
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
                txtDni.Text = dgvListar.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtMonto.Text = dgvListar.Rows[e.RowIndex].Cells[2].Value.ToString();
                dtpFecha.Value = Convert.ToDateTime(dgvListar.Rows[e.RowIndex].Cells[3].Value);
                EConcepto eConcepto = dConcepto.Listar().Find(x=> x.Codigo == Convert.ToInt32(dgvListar.Rows[e.RowIndex].Cells[4].Value.ToString()));
                cbxConcepto.Text = eConcepto.Codigo + " - " + eConcepto.Concepto;
            }
        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            if (txtDni.Text.Length == 8 && dAlumno.getAlumno(txtDni.Text) != null)
                lbNombre.Text = "Nombre: " + dAlumno.getAlumno(txtDni.Text).ApellidosNombres;
            else
                lbNombre.Text = "Nombre: ";
        }
        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvListar.DataSource = dBoleta.BuscarPorCodigoOdni(txtBuscar.Text);
        }
    }
}
