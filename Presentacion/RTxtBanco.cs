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
        DColegio dColegio = new DColegio();
        EColegio eColegio = null;
        string archivo = "";
        public RTxtBanco()
        {
            InitializeComponent();
        }

        private void RTxtBanco_Load(object sender, EventArgs e)
        {
            eColegio = dColegio.getColegio();
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
            string montoStr = Math.Round(monto, 2).ToString().Replace(".", ""); // Eliminar el punto decimal
            montoStr = montoStr.PadLeft(15, '0'); // Agregar ceros a la izquierda si es necesario
            return montoStr;
        }
        string getConcepto(ECalendarioDTO calendario)
        {
            return calendario.ConceptoCodigo.ToString().PadLeft(4, '0');
        }
        string getCodigoBanco()
        {
            return "0083";
        }
        string getDNI(ECalendarioDTO calendario)
        {
            return calendario.Dni.PadLeft(12, '0');
        }
        string getMora()
        {
            decimal monto = eColegio.Mora;
            string decimales = monto.ToString().Split('.')[1].PadRight(7, '0');
            string entero = monto.ToString().Split('.')[0].PadLeft(8, '0');
            return entero + decimales;
        }
        string getInteresComp()
        {
            return "".PadLeft(15, '0');
        }

        string getRegistroControl(int cupones, decimal total)
        {
            string montoStr = Math.Round(total, 2).ToString().Replace(".", ""); // Eliminar el punto decimal
            montoStr = montoStr.PadLeft(15, '0');
            string nombre = "REGISTRO DE CONTROL".PadRight(30, ' ');
            return ("9999" + cupones.ToString().PadLeft(12, '0') + "0000" +
                DateTime.Now.ToString("yyyyMMdd") + "   " + montoStr + nombre).PadRight(128, '0');
        }

        void generaTxt()
        {
            archivo = "";
            List<ECalendarioDTO> deudores = dCalendario.PagosPendientes();
            int totalCupones = deudores.Count;
            decimal montoTotal = 0;
            foreach (ECalendarioDTO calendario in deudores)
            {
                montoTotal += calendario.MontoTotal - calendario.MontoPagado;
                archivo += (getCodigoBanco() + getDNI(calendario) + getConcepto(calendario) + 
                    calendario.Vencimiento.ToString("yyyyMMdd") + "SOL" +
                    getMonto(calendario) + getNombre(calendario) + "0" + getMora() + getInteresComp()).PadRight(128, '0') + "\n";
            }
            archivo += getRegistroControl(totalCupones, montoTotal);            
        }

        private void btnEscribir_Click(object sender, EventArgs e)
        {
            string nombreArchivo = "PGCU_083" + DateTime.Now.ToString("yyyyMMdd");
            if (Utilidades.escribirTxt(nombreArchivo, archivo))
            {
                MessageBox.Show(dCalendario.ActualizarCalendario());
            }
            else
                MessageBox.Show("Ocurrio un error, intentelo de nuevo");
        }
    }
}
