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
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Utilidades.VerificarConexionInternet())
            {
                string html = @"
                    <html>
                    <head>
                        <title>Confirmación de pago de boleta electrónica</title>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                font-size: 16px;
                                line-height: 1.5;
                                color: #333;
                                background-color: #f5f5f5;
                                margin: 0;
                                padding: 0;
                            }
                            header {
                                text-align: center;
                                color: black;
                                padding: 15px;
                            }
                            main {
                                padding: 20px;
                                background-color: #fff;
                            }
                            table {
                                border-collapse: collapse;
                                margin-bottom: 20px;
                                width: 100%;
                            }

                            table td, table th {
                                border: 1px solid #ccc;
                                padding: 10px;
                                text-align: left;
                            }

                            table th {
                                background-color: #f7f7f7;
                                font-weight: bold;
                            }
                            footer {
                                font-weight: bold;
                                color: #000;
                                padding: 20px;
                                display: flex;
                                align-items: center;
            
                            }
                            .footer-text {
                                margin: 0;
                                margin: auto;
                                font-size: 20px;
                            }
                            .footer-text span {
                                font-size: 40px;
                                color: #007bff;
                            }
        
                        </style>
                    </head>
                    <body>
                        <header>
                            <h1>Confirmación de pago de boleta electrónica</h1>
                        </header>
                        <main>
                            <p>Estimado/a [Nombre del cliente],</p>
                            <p>Le informamos que su pago de la boleta electrónica número [Número de la boleta] ha sido procesado con éxito. A continuación, encontrará los detalles del pago:</p>
                            <table>
                                <thead>
                                    <tr>
                                        <th>Concepto</th>
                                        <th>Monto</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>[Concepto del pago]</td>
                                        <td>[Monto del pago]</td>
                                    </tr>
           
                                </tbody>
                            </table>
                            <p>Para cualquier consulta, por favor no dude en contactarnos a través de nuestro correo electrónico [Correo electrónico de la empresa] o en nuestro número de atención al cliente [Número de atención al cliente].</p>
                            <p>Atentamente,</p>
                            <p>El area de administración del Colegio Militar Leoncio Prado</p>
                        </main>
                        <footer>
                            <p class=""footer-text"">¡Felicitaciones! Continúa realizando tus pagos de boletas. <span>😊👍</span></p>
                        </footer>
                    </body>
                    </html>    
                    ";
                mantenimiento("insert");
                string resultado = await Utilidades.EnviarCorreo("huberjuanillo@gmail.com", "bobyyfoptgcwojbx", "74143981@pronabec.edu.pe", "Prueba", html);
                MessageBox.Show(resultado);
            }
            else 
                MessageBox.Show("No hay conexion a internet");
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (Utilidades.VerificarConexionInternet())
            {
                EAlumno eAlumno = dAlumno.getAlumno(txtDni.Text);
                EBoleta eBoleta = dBoleta.getBoleta(txtCodigo.Text);
                if (eBoleta != null && eAlumno != null && eBoleta.AlumnoId == eAlumno.Id)
                {
                    mantenimiento("update");
                    string resultado = await Utilidades.EnviarCorreo("huberjuanillo@gmail.com", "bobyyfoptgcwojbx", "74143981@pronabec.edu.pe", "Prueba", "Prueba");
                    MessageBox.Show(resultado);
                }
                else
                    MessageBox.Show("No se puede modificar esos datos");
            }
            else
                MessageBox.Show("No hay conexion a internet");
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Utilidades.VerificarConexionInternet())
            {
                mantenimiento("delete");
                string resultado = await Utilidades.EnviarCorreo("huberjuanillo@gmail.com", "bobyyfoptgcwojbx", "74143981@pronabec.edu.pe", "Prueba", "Prueba");
                MessageBox.Show(resultado);
            }
            else
                MessageBox.Show("No hay conexion a internet");
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
