using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Entidades;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;

namespace Presentacion
{
    public partial class RBoletasXConcepto : Form
    {
        bool imprimir = false;
        DBoleta dBoleta = new DBoleta();
        DConcepto dConcepto = new DConcepto();
        public RBoletasXConcepto()
        {
            InitializeComponent();
        }
        private void RBoletasXConcepto_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dBoleta.Listar();
            cbxConcepto.DataSource = dConcepto.Listar();
            txtTotal.Text = "Total: " + dBoleta.Total();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            EConcepto eConcepto = cbxConcepto.SelectedItem as EConcepto;
            dgvListar.DataSource = dBoleta.ListarPorConceptos(dtpInicio.Value, dtpFinal.Value, eConcepto.Codigo);
            txtTotal.Text = "Total: " + dBoleta.TotalPorConceptos(dtpInicio.Value, dtpFinal.Value, eConcepto.Codigo);
            imprimir = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvListar.DataSource = dBoleta.Listar();
            txtTotal.Text = "Total: " + dBoleta.Total();
            dtpFinal.Value = dtpInicio.Value = DateTime.Now;
            imprimir = false;
        }

        private void btnImp_Click(object sender, EventArgs e)
        {
            if (imprimir)
            {
                try
                {
                    SaveFileDialog savefile = new SaveFileDialog();
                    savefile.FileName = string.Format("{0}.pdf", "Boleta concepto " + cbxConcepto.Text + " " +  dtpInicio.Value.ToString("dd-MM-yyyy") + " - " + dtpFinal.Value.ToString("dd-MM-yyyy"));
                    string PaginaHTML_Texto = Properties.Resources.DBoletaConcepto.ToString();
                    // Reemplazar los valores en la cadena HTML
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA_INICIO", dtpInicio.Value.ToString("dd/MM/yyyy"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA_FIN", dtpFinal.Value.ToString("dd/MM/yyyy"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CONCEPTO", cbxConcepto.Text);
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
                        if (row.Cells["APELLIDOS Y NOMBRES"].Value != null)
                        {
                            filas.AppendFormat("<td style=\"text-align:center;\">{0}</td>", row.Cells["APELLIDOS Y NOMBRES"].Value.ToString());
                        }
                        else
                        {
                            filas.Append("<td style=\"text-align:center;\"> </td>");
                        }
                        if (row.Cells["FECHA"].Value != null)
                        {
                            filas.AppendFormat("<td style=\"text-align:center;\">{0}</td>", Convert.ToDateTime(row.Cells["FECHA"].Value).ToString("dd/MM/yyyy"));
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
                        filas.Append("</tr>");
                    }
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas.ToString());
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", txtTotal.Text.Split(':')[1].Trim());
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
            else
                MessageBox.Show("Primero debe aplicar un filtro");
        } 
    }
}
