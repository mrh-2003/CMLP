using SpreadsheetLight;
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
    public partial class CBoletas : Form
    {
        private List<EBoleta> listaBoletas = new List<EBoleta>();
        DAlumno dAlumno = new DAlumno();
        DBoleta dBoleta = new DBoleta();
        DBancoDTO dBancoDTO = new DBancoDTO();
        DCalendario dCalendario = new DCalendario();
        EColegio eColegio = (new DColegio()).getColegio();
        public CBoletas()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (Utilidades.VerificarConexionInternet())
            {
                string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                openFile.InitialDirectory = downloadsFolder;
                openFile.Filter = "xlsx (*.xlsx)|*.xlsx";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    cargarBoletas();
                }
            }
            else
                MessageBox.Show("No hay conexion a internet");

        }
        int obtenerConcepto(EBoleta boleta)
        {
            List<EBancoDTO> lista = dBancoDTO.ListarConCalendario();
            EAlumno alumno = dAlumno.getAlumnoById(boleta.AlumnoId);
            EBancoDTO eBancoDTO = lista.Find(x => x.NCredito == alumno.Dni && x.SPagado == boleta.Monto);
            if (eBancoDTO != null)
            {
                int concepto = eBancoDTO.NCuota;
                dBancoDTO.Mantenimiento(eBancoDTO, "delete");
                return concepto;
            }
            return -1;
        }
        private void cargarBoletas()
        {
            string fallo = "";
            string directionFile = openFile.FileName;
            SLDocument sd = new SLDocument(directionFile);
            int irow = 1;
            while (!string.IsNullOrEmpty(sd.GetCellValueAsString(irow, 1)))
            {
                EBoleta eBoleta = new EBoleta();
                string dni = sd.GetCellValueAsString(irow, 2).Split('-')[0].Trim();
                EAlumno eAlumno = dAlumno.getAlumno(dni);
                if (eAlumno != null)
                {
                    eBoleta.Codigo = sd.GetCellValueAsString(irow, 1);
                    eBoleta.AlumnoId = eAlumno.Id;
                    eBoleta.Monto = sd.GetCellValueAsDecimal(irow, 3);
                    eBoleta.Fecha = sd.GetCellValueAsDateTime(irow, 4);
                    eBoleta.ConceptoCodigo = obtenerConcepto(eBoleta);
                    if(eBoleta.ConceptoCodigo == -1)
                        fallo += "Fila " + irow + " CONCEPTO no encontrado : " + sd.GetCellValueAsString(irow, 2) + "\n";
                    else 
                        listaBoletas.Add(eBoleta);
                }
                else
                    fallo += "Fila " + irow + " DNI no encontrado : " + sd.GetCellValueAsString(irow, 2) + "\n";
                irow++;
            }
            dgvListar.DataSource = listaBoletas;
            if(fallo.Length > 0)
            {
                MessageBox.Show("Los errores fueron copiados al portapapeles");
                Clipboard.SetText(fallo);
            }
        }
        string getKardex(string dni)
        {
            string PaginaHTML_Texto = Properties.Resources.KDeterminadoAlumno.ToString();

            StringBuilder filas = new StringBuilder();

            dgvDatos.DataSource = dCalendario.KardexXAlumno(dni);
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

            EAlumno eAlumno = (new DAlumno()).getAlumno(dni);
            if (eAlumno != null)
            {
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@APELLIDOS_NOMBRES", eAlumno.ApellidosNombres);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DNI", eAlumno.Dni);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANCELADO", dCalendario.PagadoPorAlumno(dni).ToString());
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PENDIENTE", dCalendario.DeudaPorAlumno(dni).ToString());
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@GRADO", eAlumno.Grado.ToString());
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@SECCION", eAlumno.Seccion.ToString());
            }
            return PaginaHTML_Texto;
        }
        async void enviarCorreoAgregar(EBoleta boleta)
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
            EAlumno eAlumno = dAlumno.getAlumnoById(boleta.AlumnoId);
            html = html.Replace("[HEAD]", Utilidades.getHead());
            html = html.Replace("[FOOT]", Utilidades.getFoot());
            html = html.Replace("[Nombre del cliente]", eAlumno.ApellidosNombres);
            html = html.Replace("[Número de la boleta]", boleta.Codigo);
            EConcepto eConcepto = (new DConcepto()).getConcepto(boleta.ConceptoCodigo);
            html = html.Replace("[Concepto del pago]", eConcepto.Codigo + " - " + eConcepto.Concepto);
            html = html.Replace("[Monto del pago]", boleta.Monto.ToString());
            html = html.Replace("[Correo]", eColegio.Email);
            html = html.Replace("[Número de atención al cliente]", eColegio.Numero);
            html = html.Replace("[KARDEX]", getKardex(eAlumno.Dni));
            await Utilidades.EnviarCorreo(eColegio.Email, eColegio.Contrasenia, eAlumno.EmailApoderado, "Confirmación de pago de boleta electrónica", html);
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            string fallo = "";
            int count = 0;
            if (Utilidades.VerificarConexionInternet())
            {
                foreach (EBoleta boleta in listaBoletas)
                {
                    if (dBoleta.getBoleta(boleta.Codigo) == null)
                    {
                        dBoleta.Mantenimiento(boleta, "insert");
                        enviarCorreoAgregar(boleta);
                        count++;
                    } 
                    else 
                        fallo += "La boleta " + boleta.Codigo + " ya existe\n";
                }
                MessageBox.Show("Se cargaron " + count + " boletas");
                dgvListar.DataSource = null;
                if(fallo.Length > 0)
                {
                    MessageBox.Show("Los errores fueron copiados al portapapeles\n" + fallo);
                    Clipboard.SetText(fallo);
                }
            }
            else
                MessageBox.Show("Necesita tener conexion a internet");
        }
    }
}
