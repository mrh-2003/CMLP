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
    public partial class KGrado : Form
    {
        DCalendario dCalendario = new DCalendario();
        bool imp = false;
        public KGrado()
        {
            InitializeComponent();
        }

        private void fechahora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
        private void cmbSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnConsultar.PerformClick();
            }
        }

        private void KGrado_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dCalendario.KardexGeneral();
            txbCancelado.Text = dCalendario.PagadoGeneral().ToString();
            txbPendiente.Text = dCalendario.DeudaGeneral().ToString();
            dgvListar.ClearSelection();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (cbxGrado.Text != "" && cbxSeccion.Text != "")
            {
                dgvListar.DataSource = dCalendario.KardexXGrado(Convert.ToInt32(cbxGrado.Text), Convert.ToChar(cbxSeccion.Text));
                txbCancelado.Text = dCalendario.PagadoGradoSeccion(Convert.ToInt32(cbxGrado.Text), Convert.ToChar(cbxSeccion.Text)).ToString();
                txbPendiente.Text = dCalendario.DeudaGradoSeccion(Convert.ToInt32(cbxGrado.Text), Convert.ToChar(cbxSeccion.Text)).ToString();
                dgvListar.ClearSelection();
                imp = true;
            } else
                MessageBox.Show("Se debe seleccionar un grado y una seccion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (imp)
            {
                imp = false;
                if (cbxGrado.Text != "" && cbxSeccion.Text != "")
                {
                    SaveFileDialog savefile = new SaveFileDialog();
                    savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("dd-MM-yyyy" + " - " + cbxGrado.Text + " - " + cbxSeccion.Text));

                    string PaginaHTML_Texto = Properties.Resources.KDeterminadoGrado.ToString();

                    StringBuilder filas = new StringBuilder();

                    foreach (DataGridViewRow row in dgvListar.Rows)
                    {
                        filas.Append("<tr>");
                        if (row.Cells["DNI"].Value != null)
                        {
                            filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", row.Cells["DNI"].Value.ToString());
                        }
                        else
                        {
                            filas.Append("<td style=\"text-align:left;\"> </td>");
                        }
                        if (row.Cells["APELLIDOS Y NOMBRES"].Value != null)
                        {
                            filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", row.Cells["APELLIDOS Y NOMBRES"].Value.ToString());
                        }
                        else
                        {
                            filas.Append("<td style=\"text-align:left;\"> </td>");
                        }
                        if (row.Cells["EMISION"].Value != null && row.Cells["EMISION"].Value is DateTime)
                        {
                            DateTime fecha = (DateTime)row.Cells["EMISION"].Value;
                            filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", fecha.ToString("d"));
                        }
                        else
                        {
                            filas.Append("<td style=\"text-align:left;\"> </td>");
                        }
                        if (row.Cells["VENCE EL"].Value != null && row.Cells["VENCE EL"].Value is DateTime)
                        {
                            DateTime fecha = (DateTime)row.Cells["VENCE EL"].Value;
                            filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", fecha.ToString("d"));
                        }
                        else
                        {
                            filas.Append("<td style=\"text-align:left;\"> </td>");
                        }
                        if (row.Cells["CP"].Value != null)
                        {
                            filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", row.Cells["CP"].Value.ToString());
                        }
                        else
                        {
                            filas.Append("<td style=\"text-align:center;\"> </td>");
                        }
                        if (row.Cells["CANCELADO"].Value != null && row.Cells["VENCE EL"].Value is DateTime)
                        {
                            DateTime fecha = (DateTime)row.Cells["CANCELADO"].Value;
                            filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", fecha.ToString("d"));
                        }
                        else
                        {
                            filas.Append("<td style=\"text-align:left;\"> </td>");
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
                            filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", row.Cells["IMPORTE"].Value.ToString());
                        }
                        else
                        {
                            filas.Append("<td style=\"text-align:left;\"> </td>");
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
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@GRADO", cbxGrado.Text);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@SECCION", cbxSeccion.Text);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANCELADO", txbCancelado.Text);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PENDIENTE", txbPendiente.Text);

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
                    MessageBox.Show("Se debe seleccionar un grado y una seccion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Primero se debe hacer una consulta");
        }

    }
}
