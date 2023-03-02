using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class RTxtBanco : Form
    {
        DCalendario dCalendario = new DCalendario();
        DAlumno dAlumno = new DAlumno();
        string archivo = "";
        public RTxtBanco()
        {
            InitializeComponent();
        }

        private void RTxtBanco_Load(object sender, EventArgs e)
        {
            generaTxt();
            rtxtInfo.Text = archivo;
        }


        string getNombre(ECalendarioDTO calendario)
        {
            string nombre = dAlumno.getAlumno(calendario.Dni).ApellidosNombres;
            if (nombre.Length < 30) // Si es menor a 30, completar con espacios en blanco
                nombre = nombre.PadRight(30);
            else if (nombre.Length > 30) // Si es mayor a 30, recortar
                nombre = nombre.Substring(0, 30);
            return nombre;
        }
        string getMonto(ECalendarioDTO calendario)
        {
            decimal monto = calendario.MontoTotal - calendario.MontoPagado;
            string montoStr = monto.ToString().Replace(".", ""); // Eliminar el punto decimal
            montoStr = montoStr.PadLeft(8, '0'); // Agregar ceros a la izquierda si es necesario
            return montoStr;
        }
        string getConcepto(ECalendarioDTO calendario)
        {
            return calendario.ConceptoCodigo.ToString().PadLeft(4, '0');
        }
        void generaTxt()
        {
            archivo = "";
            List<ECalendarioDTO> deudores = dCalendario.PagosPendientes();
            foreach (ECalendarioDTO calendario in deudores)
            {
                int repitencia = deudores.FindAll(x => x.Dni == calendario.Dni).Count();
                archivo += "0083000000" + calendario.Dni + "0" + repitencia +
                    calendario.Vencimiento.ToString("yyyyMMdd") + getConcepto(calendario) + "SOL0000000" +
                    getMonto(calendario) + getNombre(calendario) + "000000000000000000000000000\n";
            }
            
        }

        private void btnEscribir_Click(object sender, EventArgs e)
        {
            if (Utilidades.escribirTxt("CPCO", archivo))
                MessageBox.Show("Se realizo correctamente");
            else
                MessageBox.Show("Ocurrio un error, intentelo de nuevo");
        }
    }
}
