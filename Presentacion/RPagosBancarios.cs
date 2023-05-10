using Datos;
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

namespace Presentacion
{
    public partial class RPagosBancarios : Form
    {
        DPGCU dpgcu = new DPGCU();
        public RPagosBancarios()
        {
            InitializeComponent();
        }

        private void RPagosBancarios_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dpgcu.Listar();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("dd-MM-yyyy" + "-" ));

            string PaginaHTML_Texto = Properties.Resources.reportePagos.ToString();

            StringBuilder filas = new StringBuilder();

            foreach (DataGridViewRow row in dgvListar.Rows)
            {
                filas.Append("<tr>");
                if (row.Cells["CODIGO"].Value != null)
                {
                    filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", row.Cells["CODIGO"].Value.ToString());
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
                if (row.Cells["IMPORTE"].Value != null)
                {
                    filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", row.Cells["IMPORTE"].Value.ToString());
                }
                else
                {
                    filas.Append("<td style=\"text-align:left;\"> </td>");
                }
                if (row.Cells["MORA"].Value != null)
                {
                    filas.AppendFormat("<td style=\"text-align:center;\"><pre>{0}</pre></td>", row.Cells["MORA"].Value.ToString());
                }
                else
                {
                    filas.Append("<td style=\"text-align:center;\"> </td>");
                }
                if (row.Cells["FECHA/PAGO"].Value != null && row.Cells["FECHA/PAGO"].Value is DateTime)
                {
                    DateTime fecha = (DateTime)row.Cells["EMISION"].Value;
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
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", dtpFecha.Text);
            //PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", txtTotal.Text);

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
    }
}
