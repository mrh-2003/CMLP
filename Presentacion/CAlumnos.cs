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
using SpreadsheetLight;

namespace Presentacion
{
    public partial class CAlumnos : Form
    {
        DAlumno dAlumno = new DAlumno();
        List<EAlumno> listaAlumnos = new List<EAlumno>();
        public CAlumnos()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            foreach (EAlumno item in listaAlumnos)
                dAlumno.Mantenimiento(item, "insert");
            MessageBox.Show("Tarea realizada exitosamente");
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
            string fallo = "";
            SLDocument sd = new SLDocument(directionFile);
            int irow = 1;
            while (!string.IsNullOrEmpty(sd.GetCellValueAsString(irow, 1)))
            {
                string dni = sd.GetCellValueAsString(irow, 1);
                string nombre = sd.GetCellValueAsString(irow, 2);
                string beneficio = sd.GetCellValueAsString(irow, 3);
                if (!listaAlumnos.Exists(x => x.Dni == dni))
                {
                    EAlumno eAlumno = dAlumno.getAlumno(dni);
                    if (eAlumno != null)
                    {
                        eAlumno.Descuento = beneficio;
                        eAlumno.FinDescuento = DateTime.Now.AddMonths(2);
                        listaAlumnos.Add(eAlumno);
                    }
                    else
                    {
                        fallo += dni + " " + nombre + " " + beneficio + "\n";
                    }
                }
                irow++;
            }
            if (fallo.Length > 0)
            {
                MessageBox.Show("Existen alumnos que no se pudieron registrar, informacion copiada al portapapeles");
                Clipboard.SetText(fallo);
            }
        }
    }
}
