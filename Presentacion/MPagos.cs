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
        DConcepto dConcepto = new DConcepto();
        DAlumno dAlumno = new DAlumno();
        DCalendario dCalendario = new DCalendario();
        int id = Login.id;
        public MPagos()
        {
            InitializeComponent();
        }

        private void MPagos_Load(object sender, EventArgs e)
        {
            if(Utilidades.anio == "TODOS")
            {
                btnAgregar.Enabled = false;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            cbxConcepto.DataSource = dConcepto.Listar();
            mostrar();
        }
        void limpiar()
        {
            txtId.Clear();
            txtDescripcion.Clear();
            txtMonto.Clear();
            dtpVencimiento.Value = DateTime.Now;
            cbxConcepto.SelectedIndex = -1;
            cbxGrado.SelectedIndex = 0;
            cbxSeccion.SelectedIndex = 0;
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
            if (txtDescripcion.Text != "" && txtMonto.Text != "" && dtpVencimiento.Value != null
                && cbxConcepto.SelectedIndex != -1)
            {
                EPago ePago = new EPago()
                {
                    Id = txtId.Text != "" ? Convert.ToInt32(txtId.Text) : 0,
                    Descripcion = txtDescripcion.Text,
                    Monto = Convert.ToDecimal(txtMonto.Text),
                    Vencimiento = dtpVencimiento.Value,
                    ConceptoCodigo = Convert.ToInt32(cbxConcepto.Text.Split('-')[0].Trim())
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
            else
                MessageBox.Show("Seleccione un registro");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
                mantenimiento("delete");
            else
                MessageBox.Show("Seleccione un registro");
        }

        private void cbxConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            EConcepto eConcepto = cbxConcepto.SelectedItem as EConcepto;
            if (eConcepto != null)
                txtMonto.Text = eConcepto.Importe.ToString();
        }
        void comprobarBeca(EAlumno alumno)
        {
            if(alumno.FinDescuento <= DateTime.Now)
            {
                alumno.Descuento = "Ninguna";
                alumno.FinDescuento = new DateTime(1900, 1, 1);
                dAlumno.Mantenimiento(alumno, "update");
            }
        }
        private void btnAsignar_Click(object sender, EventArgs e)
        {
            List<EPago> pagos = dgvListar.Rows.Cast<DataGridViewRow>()
            .Where(row => Convert.ToBoolean(row.Cells["cbxAgregar"].Value))
            .Select(row => row.DataBoundItem as EPago)
            .ToList();
            if (pagos.Count > 0)
            {
                List<EAlumno> listaAlumnos = null;
                if (cbxSeccion.Text == "TODOS" && cbxGrado.Text == "TODOS")
                    listaAlumnos = dAlumno.ListarLista();
                else
                    if (cbxSeccion.Text != "TODOS" && cbxGrado.Text != "TODOS")
                    listaAlumnos = dAlumno.ListarGradoSeccion("AND", Convert.ToInt32(cbxGrado.Text), Convert.ToChar(cbxSeccion.Text));
                else
                    listaAlumnos = dAlumno.ListarGradoSeccion("OR", Convert.ToInt32(cbxGrado.Text == "TODOS" ? "0" : cbxGrado.Text), Convert.ToChar(cbxSeccion.Text == "TODOS" ? "0" : cbxSeccion.Text));

                foreach (EAlumno item in listaAlumnos)
                {
                    comprobarBeca(item);
                    EAlumno alumno = dAlumno.getAlumno(item.Dni);
                    foreach (EPago pago in pagos)
                    {
                        ECalendario eCalendario;
                        if (pago.ConceptoCodigo == 2 && alumno.Descuento != "Ninguna")
                        {
                            if (alumno.Descuento == "Beca")
                            {
                                eCalendario = new ECalendario()
                                {
                                    Descripcion = pago.Descripcion,
                                    MontoPagado = 0,
                                    MontoTotal = 0,
                                    Vencimiento = pago.Vencimiento,
                                    AlumnoId = alumno.Id,
                                    ConceptoCodigo = pago.ConceptoCodigo,
                                    Mora = 0,
                                    Cancelacion = DateTime.Now,
                                    Emision = DateTime.Now
                                };
                            }
                            else
                            {
                                eCalendario = new ECalendario()
                                {
                                    Descripcion = pago.Descripcion,
                                    MontoPagado = 0,
                                    MontoTotal = pago.Monto / 2,
                                    Vencimiento = pago.Vencimiento,
                                    AlumnoId = alumno.Id,
                                    ConceptoCodigo = pago.ConceptoCodigo,
                                    Mora = 0,
                                    Cancelacion = new DateTime(1900, 1, 1),
                                    Emision = DateTime.Now
                                };
                            }
                        }
                        else
                        {
                            eCalendario = new ECalendario()
                            {
                                Descripcion = pago.Descripcion,
                                MontoPagado = 0,
                                MontoTotal = pago.Monto,
                                Vencimiento = pago.Vencimiento,
                                AlumnoId = alumno.Id,
                                ConceptoCodigo = pago.ConceptoCodigo,
                                Mora = 0,
                                Cancelacion = new DateTime(1900, 1, 1),
                                Emision = DateTime.Now
                            };
                        }
                        dCalendario.Mantenimiento(eCalendario, "insert");
                    }
                }
                limpiar();
                MessageBox.Show("Tarea cumplida exitosamente");
            }
            else
                MessageBox.Show("Debe marcar almenos un pago");
        }

        private void dgvListar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtId.Text = dgvListar.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDescripcion.Text = dgvListar.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtMonto.Text = dgvListar.Rows[e.RowIndex].Cells[3].Value.ToString();
                dtpVencimiento.Value = Convert.ToDateTime(dgvListar.Rows[e.RowIndex].Cells[4].Value);
                EConcepto eConcepto = dConcepto.Listar().Find(x => x.Codigo == Convert.ToInt32(dgvListar.Rows[e.RowIndex].Cells[5].Value.ToString()));
                cbxConcepto.Text = eConcepto.Codigo + " - " + eConcepto.Concepto;
            }
        }
    }
}
