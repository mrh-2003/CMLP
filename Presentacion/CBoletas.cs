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
        public CBoletas()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openFile.InitialDirectory = "C:\\Users\\Hub CJ Technology\\Downloads";
            openFile.Filter = "xlsx (*.xlsx)|*.xlsx";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                cargarBoletas();
            }
            
        }
        private void cargarBoletas()
        {
            string directionFile = openFile.FileName;
            SLDocument sd = new SLDocument(directionFile);
            int irow = 1;
            while (!string.IsNullOrEmpty(sd.GetCellValueAsString(irow, 1)))
            {
                EBoleta eBoleta = new EBoleta()
                {
                    Codigo = sd.GetCellValueAsString(irow, 1),
                    AlumnoDNI = sd.GetCellValueAsString(irow, 2).Split('-')[0].Trim(),
                    Monto = sd.GetCellValueAsDecimal(irow, 3),
                    Fecha = sd.GetCellValueAsDateTime(irow, 4),
                    ConceptoCodigo = sd.GetCellValueAsInt32(irow, 5)
                };
                listaBoletas.Add(eBoleta);
                irow++;
            }
            dgvListar.DataSource = listaBoletas;
        }
    }
}
