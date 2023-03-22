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
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.IO;
using iTextSharp.tool.xml.html;

namespace Presentacion
{
    public partial class MBoleta : Form
    {
        private const string TITULO_ALERTA = "Error de Entrada";
        DBoleta dBoleta = new DBoleta();
        DConcepto dConcepto = new DConcepto();
        DCalendario dCalendario = new DCalendario();
        DAlumno dAlumno = new DAlumno();
        DHistorial dHistorial = new DHistorial();
        EColegio eColegio = (new DColegio()).getColegio();
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
            txtTotal.Text = dBoleta.Total().ToString();
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
                    if (opcion == "insert")
                        enviarCorreoAgregar();
                    else if (opcion == "update")
                        enviarCorreoActualizar();
                    else
                        enviarCorreoEliminar();

                    mostrar();
                }
                else
                    MessageBox.Show("El alumno no existe");
            }
            else
                MessageBox.Show("Todos los campos deben estar llenos");
        }
        string getKardex()
        {
            string PaginaHTML_Texto = CMLP.Properties.Resources.KDeterminadoAlumno.ToString();

            StringBuilder filas = new StringBuilder();

            dgvDatos.DataSource = dCalendario.KardexXAlumno(txtDni.Text);
            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                filas.Append("<tr>");
                if (row.Cells["EMISION"].Value != null && row.Cells["EMISION"].Value is DateTime)
                {
                    DateTime fecha = (DateTime)row.Cells["EMISION"].Value;
                    filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", fecha.ToString("d"));
                }
                else
                {
                    filas.Append("<td style=\"text-align:center;\"> </td>");
                }
                if (row.Cells["VENCE EL"].Value != null && row.Cells["VENCE EL"].Value is DateTime)
                {
                    DateTime fecha = (DateTime)row.Cells["VENCE EL"].Value;
                    filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", fecha.ToString("d"));
                }
                else
                {
                    filas.Append("<td style=\"text-align:center;\"> </td>");
                }
                if (row.Cells["CP"].Value != null)
                {
                    filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", row.Cells["CP"].Value.ToString());
                }
                else
                {
                    filas.Append("<td style=\"text-align:center;\"> </td>");
                }
                if (row.Cells["CANCELADO"].Value != null && row.Cells["EMISION"].Value is DateTime)
                {
                    DateTime fecha = (DateTime)row.Cells["CANCELADO"].Value;
                    filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", fecha.ToString("d"));
                }
                else
                {
                    filas.Append("<td style=\"text-align:center;\"> </td>");
                }
                if (row.Cells["MOTIVO DE PAGO"].Value != null)
                {
                    filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", row.Cells["MOTIVO DE PAGO"].Value.ToString());
                }
                else
                {
                    filas.Append("<td style=\"text-align:center;\"> </td>");
                }
                if (row.Cells["IMPORTE"].Value != null)
                {
                    filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", row.Cells["IMPORTE"].Value.ToString());
                }
                else
                {
                    filas.Append("<td style=\"text-align:center;\"> </td>");
                }
                if (row.Cells["IMP/MORA"].Value != null)
                {
                    filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", row.Cells["IMP/MORA"].Value.ToString());
                }
                else
                {
                    filas.Append("<td style=\"text-align:center;\"> </td>");
                }
                filas.Append("</tr>");
            }

            // Reemplazar la etiqueta @FILAS con todas las filas del DataGridView
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas.ToString());

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToShortDateString());

            EAlumno eAlumno = (new DAlumno()).getAlumno(txtDni.Text);
            if (eAlumno != null)
            {
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@APELLIDOS_NOMBRES", eAlumno.ApellidosNombres);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DNI", eAlumno.Dni);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANCELADO", dCalendario.PagadoPorAlumno(txtDni.Text).ToString());
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PENDIENTE", dCalendario.DeudaPorAlumno(txtDni.Text).ToString());
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@GRADO", eAlumno.Grado.ToString());
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@SECCION", eAlumno.Seccion.ToString());
            }
            return PaginaHTML_Texto;
        }
        async void enviarCorreoAgregar()
        {
            string html = @"
                        [HEAD]
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
                            [FOOT]
                        <footer>
                            <p class=""footer-text"">¡Felicitaciones! Continúa realizando tus pagos de boletas. <span>😊👍</span></p>
                        </footer>
                    </body>
                    </html>
                    <br>
                    <hr>
                    <br>
                      [KARDEX]
                    ";
            EAlumno eAlumno = dAlumno.getAlumno(txtDni.Text);
            html = html.Replace("[HEAD]", Utilidades.getHead());
            html = html.Replace("[FOOT]", Utilidades.getFoot());
            html = html.Replace("[Nombre del cliente]", eAlumno.ApellidosNombres);
            html = html.Replace("[Número de la boleta]", txtCodigo.Text);
            html = html.Replace("[Concepto del pago]", cbxConcepto.Text);
            html = html.Replace("[Monto del pago]", txtMonto.Text);
            html = html.Replace("[Correo]", eColegio.Email);
            html = html.Replace("[Número de atención al cliente]", eColegio.Numero);
            html = html.Replace("[KARDEX]", getKardex());
            string resultado = await Utilidades.EnviarCorreo(eColegio.Email, eColegio.Contrasenia, eAlumno.EmailApoderado, "Confirmación de pago de boleta electrónica", html);
            MessageBox.Show(resultado);
        }
        async void enviarCorreoActualizar() 
        {
            string html = @"
                        [HEAD]
                        <header>
                            <h1>Actualización de boleta electrónica</h1>
                        </header>
                        <main>
                            <p>Estimado/a [Nombre del cliente],</p>
                            <p>Le informamos que se cometio un error en la boleta electrónica número [Número de la boleta], por ello, se procedio a actulizar los datos. A continuación, encontrará los detalles de la nueva boleta:</p>
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
                            [FOOT]
                    </body>
                    </html>    
                    ";
            EAlumno eAlumno = dAlumno.getAlumno(txtDni.Text);
            html = html.Replace("[HEAD]", Utilidades.getHead());
            html = html.Replace("[FOOT]", Utilidades.getFoot());
            html = html.Replace("[Nombre del cliente]", eAlumno.ApellidosNombres);
            html = html.Replace("[Número de la boleta]", txtCodigo.Text);
            html = html.Replace("[Concepto del pago]", cbxConcepto.Text);
            html = html.Replace("[Monto del pago]", txtMonto.Text);
            html = html.Replace("[Correo]", eColegio.Email);
            html = html.Replace("[Número de atención al cliente]", eColegio.Numero);
            string resultado = await Utilidades.EnviarCorreo(eColegio.Email, eColegio.Contrasenia, eAlumno.EmailApoderado, "Confirmación de pago de boleta electrónica", html);
            MessageBox.Show(resultado);
        }
        async void enviarCorreoEliminar()
        {
            string html = @"
                        [HEAD]
                        <header>
                            <h1>Boleta electrónica eliminada</h1>
                        </header>
                        <main>
                            <p>Estimado/a [Nombre del cliente],</p>
                            <p>Le informamos que la boleta electrónica número [Número de la boleta] ha sido eliminada. A continuación, encontrará los detalles de la boleta eliminada:</p>
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
                            [FOOT]
                    </body>
                    </html>    
                    ";
            EAlumno eAlumno = dAlumno.getAlumno(txtDni.Text);
            html = html.Replace("[HEAD]", Utilidades.getHead());
            html = html.Replace("[FOOT]", Utilidades.getFoot());
            html = html.Replace("[Nombre del cliente]", eAlumno.ApellidosNombres);
            html = html.Replace("[Número de la boleta]", txtCodigo.Text);
            html = html.Replace("[Concepto del pago]", cbxConcepto.Text);
            html = html.Replace("[Monto del pago]", txtMonto.Text);
            html = html.Replace("[Correo]", eColegio.Email);
            html = html.Replace("[Número de atención al cliente]", eColegio.Numero);
            string resultado = await Utilidades.EnviarCorreo(eColegio.Email, eColegio.Contrasenia, eAlumno.EmailApoderado, "Boleta electrónica eliminada", html);
            MessageBox.Show(resultado);
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Utilidades.VerificarConexionInternet())
                mantenimiento("insert");
            else 
                MessageBox.Show("No hay conexion a internet");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (Utilidades.VerificarConexionInternet())
            {
                EAlumno eAlumno = dAlumno.getAlumno(txtDni.Text);
                EBoleta eBoleta = dBoleta.getBoleta(txtCodigo.Text);
                if (eBoleta != null && eAlumno != null && eBoleta.AlumnoId == eAlumno.Id)
                    mantenimiento("update");
                else
                    MessageBox.Show("No se puede modificar esos datos");
            }
            else
                MessageBox.Show("No hay conexion a internet");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Utilidades.VerificarConexionInternet())
                mantenimiento("delete");
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
            txtTotal.Text = dBoleta.TotalBuscarPorCodigoOdni(txtBuscar.Text).ToString();
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvListar.SelectedRows[0];

                // Crear una lista para almacenar los valores de las celdas de la fila seleccionada
                List<string> valoresCeldasFila = new List<string>();

                // Recorrer todas las celdas de la fila seleccionada y agregar sus valores a la lista
                foreach (DataGridViewCell celda in filaSeleccionada.Cells)
                {
                    DateTime fecha;
                    if (DateTime.TryParse(celda.Value.ToString(), out fecha))
                    {
                        valoresCeldasFila.Add(fecha.ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        valoresCeldasFila.Add(celda.Value.ToString());
                    }
                }
                string dni = valoresCeldasFila[1];

                string nombre = dAlumno.getAlumno(dni).ApellidosNombres;

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("{0}.pdf", valoresCeldasFila[0]+" - "+nombre);

                string PaginaHTML_Texto = CMLP.Properties.Resources.DBoleta.ToString();
              
               
                // Reemplazar los valores en la cadena HTML
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODIGO", valoresCeldasFila[0]);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DNI", dni);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@APELLIDOS_NOMBRES", nombre);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MONTO", valoresCeldasFila[2]);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", valoresCeldasFila[3]);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CONCEPTO_CODIGO", dConcepto.getConcepto(Convert.ToInt32(valoresCeldasFila[4])).Concepto);

                StringBuilder filas = new StringBuilder();
                foreach (DataGridViewRow row in dgvListar.Rows)
                {
                    filas.Append("<tr>");
                    if (row.Cells["CODIGO"].Value != null)
                    {
                        filas.AppendFormat("<td style=\"text-align:center;\">{0}</td>", row.Cells["CODIGO"].Value.ToString());
                    }
                    else
                    {
                        filas.Append("<td style=\"text-align:center;\"> </td>");
                    }
                    if (row.Cells["DNI"].Value != null)
                    {
                        filas.AppendFormat("<td style=\"text-align:center;\">{0}</td>", row.Cells["DNI"].Value.ToString());
                    }
                    else
                    {
                        filas.Append("<td style=\"text-align:center;\"> </td>");
                    }
                    if (row.Cells["MONTO"].Value != null)
                    {
                        filas.AppendFormat("<td style=\"text-align:center;\">{0}</td>", row.Cells["MONTO"].Value.ToString());
                    }
                    else
                    {
                        filas.Append("<td style=\"text-align:center;\"> </td>");
                    }
                    if (row.Cells["CP"].Value != null)
                    {
                        filas.AppendFormat("<td style=\"text-align:center;\">{0}</td>", row.Cells["CP"].Value.ToString());
                    }
                    else
                    {
                        filas.Append("<td style=\"text-align:center;\"> </td>");
                    }
                    filas.Append("</tr>");
                }

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                    {
                        //Creamos un nuevo documento y lo definimos como PDF
                        Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        pdfDoc.Add(new Phrase(""));

                        using (StringReader sr = new StringReader(PaginaHTML_Texto))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        }
                        pdfDoc.Close();
                        stream.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
