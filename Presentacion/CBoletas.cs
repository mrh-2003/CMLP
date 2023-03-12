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
        private const string TITULO_ALERTA = "Error de Entrada";
        private List<EBoleta> listaBoletas = new List<EBoleta>();
        DAlumno dAlumno = new DAlumno();
        DBoleta dBoleta = new DBoleta();
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
            return boleta.ConceptoCodigo;
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

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            if (Utilidades.VerificarConexionInternet())
            {
                foreach (EBoleta boleta in listaBoletas)
                {
                    dBoleta.Mantenimiento(boleta, "insert");
                    await Utilidades.EnviarCorreo("huberjuanillo@gmail.com", "bobyyfoptgcwojbx", "74143981@pronabec.edu.pe", "Prueba", "Prueba");
                }
            }
            else
                MessageBox.Show("Necesita tener conexion a internet");
        }
    }
}
