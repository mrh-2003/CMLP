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

namespace Presentacion
{
    public partial class CTxtBanco : Form
    {
        public List<EBancoDTO> lista = new List<EBancoDTO>();
        public CTxtBanco()
        {
            InitializeComponent();
        }
        void cargarDatos()
        {
            using (StreamReader archivo = new StreamReader("archivo.txt"))
            {
                List<string> lineas = new List<string>();
                string linea;
                while ((linea = archivo.ReadLine()) != null)
                {
                    lineas.Add(linea);
                }
                archivo.Close();
                foreach (string l in lineas)
                {
                    Console.WriteLine(l);
                }
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            openFile.InitialDirectory = downloadsFolder;
            openFile.Filter = "txt (*.txt)|*.txt";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                cargarDatos();
            }
        }
    }
}
