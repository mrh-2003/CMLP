using Datos;
using Entidades;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Presentacion
{
    public partial class KAlumno : Form
    {
        private const string TITULO_ALERTA = "Error de Entrada";
        DCalendario dCalendario = new DCalendario();
        DAlumno dAlumno = new DAlumno();
        public KAlumno()
        {
            InitializeComponent();
        }
        private void fechahora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
        private void txbDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta letras. Introduce un nombre válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txbDni_TextChanged(object sender, EventArgs e)
        {
            if (txbDni.Text.Length == 8)
            {
                dgvListar.DataSource = dCalendario.KardexXAlumno(txbDni.Text);
                dgvListar.ClearSelection();
                EAlumno eAlumno = (new DAlumno()).getAlumno(txbDni.Text);
                if(eAlumno!=null)
                {
                    lbNombre.Text = eAlumno.ApellidosNombres;
                    lbTelefono.Text = eAlumno.Celular.ToString();
                    lbEmail.Text = eAlumno.Email.ToString();
                    lblAnio.Text = eAlumno.Grado.ToString();
                    lblSeccion.Text = eAlumno.Seccion.ToString();
                    txbCancelado.Text = dCalendario.PagadoPorAlumno(txbDni.Text).ToString();
                    txbPendiente.Text = dCalendario.DeudaPorAlumno(txbDni.Text).ToString();
                }              
            } else
            {
                lbNombre.Text = "";
                lbTelefono.Text = "";
                lbEmail.Text = "";
                lblAnio.Text = "";
                lblSeccion.Text = "";
                dgvListar.DataSource = dCalendario.KardexGeneral();
                txbCancelado.Text = dCalendario.PagadoGeneral().ToString();
                txbPendiente.Text = dCalendario.DeudaGeneral().ToString();
                dgvListar.ClearSelection();
            }
        }

        private void KAlumno_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.KardexGeneral();
            txbCancelado.Text = dCalendario.PagadoGeneral().ToString();
            txbPendiente.Text = dCalendario.DeudaGeneral().ToString();
            dgvListar.ClearSelection();
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            EAlumno eAlumno = (new DAlumno()).getAlumno(txbDni.Text);
            if (eAlumno != null)
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("dd-MM-yyyy" + " - " + txbDni.Text + " - " + lblAnio.Text + " - " + lblSeccion.Text));

                string PaginaHTML_Texto =   CMLP.Properties.Resources.KDeterminadoAlumno.ToString();

                StringBuilder filas = new StringBuilder();
                foreach (DataGridViewRow row in dgvListar.Rows)
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


                if (eAlumno != null)
                {
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@APELLIDOS_NOMBRES", lbNombre.Text = eAlumno.ApellidosNombres);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DNI", lbNombre.Text = eAlumno.Dni);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANCELADO", txbCancelado.Text = dCalendario.PagadoPorAlumno(txbDni.Text).ToString());
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PENDIENTE", txbPendiente.Text = dCalendario.DeudaPorAlumno(txbDni.Text).ToString());
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@GRADO", lblAnio.Text);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@SECCION", lblSeccion.Text);
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
            else 
                MessageBox.Show("No se encontro el alumno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
