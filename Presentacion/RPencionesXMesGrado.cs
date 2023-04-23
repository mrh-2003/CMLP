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
    public partial class RPencionesXMesGrado : Form
    {
        DCalendario dCalendario = new DCalendario();
        public RPencionesXMesGrado()
        {
            InitializeComponent();
        }
        private void RPencionesXMesGrado_Load(object sender, EventArgs e)
        {
            mostrar();

        }
        void mostrar()
        {
            dgvListar.DataSource = dCalendario.ListarPencionesXMesXGrado(cbxMes.Text == "" ? 1 : Convert.ToInt32(cbxMes.Text), cbxGrado.Text == "" ? 3 : Convert.ToInt32(cbxGrado.Text));
            lb3ro.Text = "Tercero: " + dCalendario.TotalPorGrado(cbxMes.Text == "" ? 1 : Convert.ToInt32(cbxMes.Text), 3).ToString();
            lb4to.Text = "Cuarto: " + dCalendario.TotalPorGrado(cbxMes.Text == "" ? 1 : Convert.ToInt32(cbxMes.Text), 4).ToString();
            lb5to.Text = "Quinto: " + dCalendario.TotalPorGrado(cbxMes.Text == "" ? 1 : Convert.ToInt32(cbxMes.Text), 5).ToString();
        }
        private void cbxMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrar();
        }

        private void cbxGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrar();
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
           if(cbxMes.Text!="" && cbxGrado.Text!="")
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("dd-MM-yyyy" + "-" + cbxMes.Text + cbxGrado.Text));

                string PaginaHTML_Texto = Properties.Resources.RApensionesmesgrado.ToString();

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
                    if (row.Cells["EMAIL"].Value != null)
                    {
                        filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", row.Cells["EMAIL"].Value.ToString());
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
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MES", cbxMes.Text);
                PaginaHTML_Texto = PaginaHTML_Texto.Replace("@GRADO", cbxGrado.Text);

                if (cbxGrado.Text == "3")
                {
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MONTO", lb3ro.Text);
                }
                else if (cbxGrado.Text == "4")
                {
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MONTO", lb4to.Text);
                }
                else
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MONTO", lb5to.Text);

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

