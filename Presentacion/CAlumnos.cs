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
        DCalendario dCalendario = new DCalendario();
        DPago dPago = new DPago();
        List<EAlumno> listaAlumnos = new List<EAlumno>();
        public CAlumnos()
        {
            InitializeComponent();
        }

        void mostrar()
        {
            dgvListar.DataSource = dAlumno.listObjectToDataTable(listaAlumnos); ;
            dgvListar.ClearSelection();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            string fallo = "";
            List<EPago> pagos = dPago.Listar();
            foreach (EAlumno item in listaAlumnos)
            {
                if (dAlumno.getAlumno(item.Dni) == null)
                {
                    dAlumno.Mantenimiento(item, "insert");
                    EAlumno alumno = dAlumno.getAlumno(item.Dni);
                    foreach (EPago pago in pagos)
                    {
                        ECalendario eCalendario;
                        if (pago.Descripcion == "Alimentación" && alumno.Descuento != "Ninguna")
                        {
                            if(alumno.Descuento == "Beca")
                            {
                                eCalendario = new ECalendario()
                                {
                                    Descripcion = pago.Descripcion,
                                    MontoPagado = 0,
                                    MontoTotal = 0,
                                    Vencimiento = pago.Vencimiento,
                                    AlumnoId = alumno.Id,
                                    ConceptoCodigo = pago.ConceptoCodigo,
                                    Mora = 0,
                                    Cancelacion = DateTime.Now,
                                    Emision = DateTime.Now
                                };
                            }
                            else
                            {
                                eCalendario = new ECalendario()
                                {
                                    Descripcion = pago.Descripcion,
                                    MontoPagado = 0,
                                    MontoTotal = pago.Monto / 2,
                                    Vencimiento = pago.Vencimiento,
                                    AlumnoId = alumno.Id,
                                    ConceptoCodigo = pago.ConceptoCodigo,
                                    Mora = 0,
                                    Cancelacion = DateTime.Now,
                                    Emision = DateTime.Now
                                };
                            }
                        }
                        else
                        {
                            eCalendario = new ECalendario()
                            {
                                Descripcion = pago.Descripcion,
                                MontoPagado = 0,
                                MontoTotal = pago.Monto,
                                Vencimiento = pago.Vencimiento,
                                AlumnoId = alumno.Id,
                                ConceptoCodigo = pago.ConceptoCodigo,
                                Mora = 0,
                                Cancelacion = DateTime.Now,
                                Emision = DateTime.Now
                            };
                        }
                        dCalendario.Mantenimiento(eCalendario, "insert");
                    }
                }
                else
                    fallo += item.Dni + " - " + item.ApellidosNombres + "\n";
            }
            if (fallo.Length > 0)
            {
                MessageBox.Show("Estos alumnos ya fueron registrados antes :\n" + fallo);
                Clipboard.SetText(fallo);
                MessageBox.Show("Los alumnos existentes fueron copiados al portapapeles");
            }
            MessageBox.Show("Tarea realizada exitosamente");
            dgvListar.DataSource = null;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            openFile.InitialDirectory = downloadsFolder;
            openFile.Filter = "xlsx (*.xlsx)|*.xlsx";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                cargarBoletas();
            }

        }
        private void cargarBoletas()
        {
            try
            {
                string directionFile = openFile.FileName;
                string fallo = "";
                SLDocument sd = new SLDocument(directionFile);
                int irow = 1;
                while (!string.IsNullOrEmpty(sd.GetCellValueAsString(irow, 1)))
                {
                    string dni = sd.GetCellValueAsString(irow, 1);
                    if (!listaAlumnos.Exists(x => x.Dni == dni))
                    {
                        EAlumno eAlumno = new EAlumno()
                        {
                            Dni = dni,
                            ApellidosNombres = sd.GetCellValueAsString(irow, 2),
                            Grado = sd.GetCellValueAsInt32(irow, 3),
                            Seccion = Convert.ToChar(sd.GetCellValueAsString(irow, 4)),
                            Email = sd.GetCellValueAsString(irow, 5),
                            EmailApoderado = sd.GetCellValueAsString(irow, 6),
                            Celular = sd.GetCellValueAsInt32(irow, 7),
                            CelularApoderado = sd.GetCellValueAsInt32(irow, 8),
                            Descuento = sd.GetCellValueAsString(irow, 9),
                            FinDescuento = DateTime.Now.AddMonths(2),
                            AnioRegistro = DateTime.Now.Year //Esto tambien se podria leer
                        };
                        listaAlumnos.Add(eAlumno);
                    }
                    else
                        fallo += dni + "\n";
                    irow++;
                }
                mostrar();
                if (fallo.Length > 0)
                {
                    MessageBox.Show("Los DNIs de alumnos repetidos fueron copiados al portapapeles");
                    Clipboard.SetText(fallo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al cargar las boletas: {ex.Message}");
            }
        }

    }
}
