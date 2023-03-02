using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using Datos;
using Entidades;

namespace Presentacion
{
    public partial class MCalendarioPagos : Form
    {
        private const string TITULO_ALERTA = "Error de Entrada";
        DCalendario dCalendario = new DCalendario();
        DPago dPago = new DPago();
        DAlumno dAlumno = new DAlumno();
        DConcepto dConcepto = new DConcepto();
        EPago ePago = null;
        DHistorial dHistorial = new DHistorial();
        int id = Login.id;
        public MCalendarioPagos()
        {
            InitializeComponent();
        }
        private void MCalendarioPagos_Load(object sender, EventArgs e)
        {
            cbxDescripcion.DataSource = dPago.Listar();
            cbxConcepto.DataSource = dConcepto.Listar();
            mostrar();
        }
        void limpiar()
        {
            txtId.Clear();
            cbxDescripcion.Text = "";
            cbxConcepto.SelectedIndex = -1;
            txtMontoPagado.Text = "0";
            txtMontoTotal.Clear();
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
            if (txtDni.Text != "" && cbxDescripcion.Text != "" && cbxConcepto.Text != "" && txtMontoPagado.Text != "" && txtMontoTotal.Text != "" && dtpVencimiento.Value != null)
            {
                EAlumno eAlumno = dAlumno.getAlumno(txtDni.Text);
                if (eAlumno != null)
                {
                    ECalendario eCalendario = new ECalendario()
                    {
                        Id = txtId.Text != "" ? Convert.ToInt32(txtId.Text) : 0,
                        Descripcion = cbxDescripcion.Text,
                        MontoTotal = Convert.ToDecimal(txtMontoTotal.Text),
                        MontoPagado = Convert.ToDecimal(txtMontoPagado.Text),
                        Vencimiento = dtpVencimiento.Value,
                        AlumnoId = eAlumno.Id,
                        ConceptoCodigo = Convert.ToInt32(cbxConcepto.Text.Split('-')[0].Trim())
                    };
                    string mensaje = dCalendario.Mantenimiento(eCalendario, opcion);
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
                    MessageBox.Show("El alumno no existe");
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
                cbxDescripcion.Text = dgvListar.Rows[e.RowIndex].Cells[1].Value.ToString();
                EConcepto eConcepto = dConcepto.Listar().Find(x => x.Codigo == Convert.ToInt32(dgvListar.Rows[e.RowIndex].Cells[2].Value.ToString()));
                cbxConcepto.Text = eConcepto.Codigo + " - " + eConcepto.Concepto;
                txtDni.Text = dgvListar.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtMontoPagado.Text = dgvListar.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtMontoTotal.Text = dgvListar.Rows[e.RowIndex].Cells[5].Value.ToString();
                dtpVencimiento.Value = Convert.ToDateTime(dgvListar.Rows[e.RowIndex].Cells[6].Value);
            }
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
        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar!='.')
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.BuscarPorDniODescripcion(txtBuscar.Text);
        }

        private void cbxDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ePago = cbxDescripcion.SelectedItem as EPago;
            EConcepto eConcepto = dConcepto.getConcepto(ePago.ConceptoCodigo);
            if (ePago != null)
            {
                cbxConcepto.Text = eConcepto.Codigo + " - " + eConcepto.Concepto;
                txtMontoPagado.Text = ePago.Monto.ToString();
            }
        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            if (txtDni.Text.Length == 8 && dAlumno.getAlumno(txtDni.Text) != null)
                lbNombre.Text = "Nombre: " + dAlumno.getAlumno(txtDni.Text).ApellidosNombres;
            else
                lbNombre.Text = "Nombre: ";
        }
        private void txtMontoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
