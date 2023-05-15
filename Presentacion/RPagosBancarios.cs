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
    public partial class RPagosBancarios : Form
    {
        DPGCU dpgcu = new DPGCU();
        string datos = "";
        int id = -1;
        public RPagosBancarios()
        {
            InitializeComponent();
        }

        private void RPagosBancarios_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dpgcu.ListarPorFecha(dtpInicio.Value, dtpFinal.Value);
            txtTotalXFecha.Text = dpgcu.TotalPorFecha(dtpInicio.Value, dtpFinal.Value).ToString();
            txtTotal.Text = dpgcu.Total().ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            dgvListar.DataSource = dpgcu.ListarPorFecha(dtpInicio.Value, dtpFinal.Value);
            txtTotalXFecha.Text = dpgcu.TotalPorFecha(dtpInicio.Value, dtpFinal.Value).ToString();
            txtTotal.Text = dpgcu.Total().ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(datos != "")
            {
                if (MessageBox.Show("¿Seguro que desea eliminar este pago? \nEsto puede generar errores de datos inconclusos en los reportes"  + datos, "Importante", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EPGCU epgcu = new EPGCU();
                    epgcu.id = id;
                    dpgcu.Mantenimiento(epgcu, "delete");
                }
            }
            else
            {
                MessageBox.Show("Primero debes seleccionar el pago a eliminar");
            }
        }

        private void dgvListar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                datos = "\n" + dgvListar.Rows[e.RowIndex].Cells[0].Value.ToString() + "\n" +
                    dgvListar.Rows[e.RowIndex].Cells[1].Value.ToString() + "\n" +
                    dgvListar.Rows[e.RowIndex].Cells[2].Value.ToString() + "\n" +
                    dgvListar.Rows[e.RowIndex].Cells[3].Value.ToString() + "\n" +
                    dgvListar.Rows[e.RowIndex].Cells[4].Value.ToString() + "\n" +
                    dgvListar.Rows[e.RowIndex].Cells[5].Value.ToString();
                id = Convert.ToInt32(dgvListar.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            txtTotalXFecha.Text = "";
            dgvListar.DataSource = dpgcu.BuscarPorNombreODNI(txtBuscar.Text);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int contador = 1;
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("dd-MM-yyyy"));

            string PaginaHTML_Texto = Properties.Resources.reportePagos.ToString();

            StringBuilder filas = new StringBuilder();

            foreach (DataGridViewRow row in dgvListar.Rows)
            {
                filas.Append("<tr>");
                filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", contador);
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
                    DateTime fecha = (DateTime)row.Cells["FECHA/PAGO"].Value;
                    filas.AppendFormat("<td style=\"text-align:left;\"><pre>{0}</pre></td>", fecha.ToString("d"));
                }
                else
                {
                    filas.Append("<td style=\"text-align:left;\"> </td>");
                }
                filas.Append("</tr>");
                contador++;
            }

            // Reemplazar la etiqueta @FILAS con todas las filas del DataGridView
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas.ToString());
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToShortDateString());
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECH1", dtpInicio.Value.ToString("d"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECH2", dtpFinal.Value.ToString("d"));
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTALFECHA", txtTotalXFecha.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", txtTotal.Text);

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
