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
using Datos;
using Entidades;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;

namespace Presentacion
{
    public partial class RAlumnosXGrado : Form
    {
        DAlumno dAlumno = new DAlumno();
        public RAlumnosXGrado()
        {
            InitializeComponent();
        }
        private void RAlumnosXGrado_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dAlumno.Listar();
        }
        private void cbxGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            int grado = cbxGrado.Text == "" ? 0 : Convert.ToInt32(cbxGrado.Text);
            int seccion = cbxSeccion.Text == "" ? 0 : Convert.ToInt32(cbxSeccion.Text);
            dgvListar.DataSource = dAlumno.FiltrarGradoSeccion(grado, seccion);
        }
        private void cbxSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int grado = cbxGrado.Text == "" ? 0 : Convert.ToInt32(cbxGrado.Text);
            int seccion = cbxSeccion.Text == "" ? 0 : Convert.ToInt32(cbxSeccion.Text);
            dgvListar.DataSource = dAlumno.FiltrarGradoSeccion(grado, seccion);
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            cbxGrado.SelectedIndex = -1;
            cbxSeccion.SelectedIndex = -1;
            dgvListar.DataSource = dAlumno.Listar();
        }
        private void cbxSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnClear.PerformClick();
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (cbxGrado.Text != "" && cbxSeccion.Text != "")
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("dd-MM-yyyy" + "-" + cbxGrado.Text + cbxSeccion.Text));

                string PaginaHTML_Texto = Properties.Resources.RAgrado.ToString();

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
                    if (row.Cells["GRADO"].Value != null)
                    {
                        filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", row.Cells["GRADO"].Value.ToString());
                    }
                    else
                    {
                        filas.Append("<td style=\"text-align:left;\"> </td>");
                    }
                    if (row.Cells["SECCION"].Value != null)
                    {
                        filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", row.Cells["SECCION"].Value.ToString());
                    }
                    else
                    {
                        filas.Append("<td style=\"text-align:left;\"> </td>");
                    }
                    if (row.Cells["EMAIL-APODERADO"].Value != null)
                    {
                        filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", row.Cells["EMAIL-APODERADO"].Value.ToString());
                    }
                    else
                    {
                        filas.Append("<td style=\"text-align:center;\"> </td>");
                    }

                    if (row.Cells["CELULAR-APODERADO"].Value != null)
                    {
                        filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", row.Cells["CELULAR-APODERADO"].Value.ToString());
                    }
                    else
                    {
                        filas.Append("<td style=\"text-align:center;\"> </td>");
                    }
                    if (row.Cells["DESCUENTO"].Value != null)
                    {
                        filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", row.Cells["DESCUENTO"].Value.ToString());
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

                    filas.Append("</tr>");
                }


                // Reemplazar la etiqueta @FILAS con todas las filas del DataGridView
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas.ToString());
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToShortDateString());
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@SECCION", cbxSeccion.Text);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@GRADO", cbxGrado.Text);

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
                MessageBox.Show("Se debe ingresar datos en todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
