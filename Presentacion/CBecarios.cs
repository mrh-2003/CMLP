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
    public partial class CBecarios : Form
    {
        private const string TITULO_ALERTA = "Error de Entrada";
        DAlumno dAlumno = new DAlumno();
        List<EAlumno> listaAlumnos = new List<EAlumno>();
        public CBecarios()
        {
            InitializeComponent();
        }
        private void CBecarios_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = listaAlumnos;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtDni.Text != "" && cbxBeca.Text != "")
                if (!listaAlumnos.Exists(x => x.Dni == txtDni.Text))
                {
                    EAlumno eAlumno = dAlumno.getAlumno(txtDni.Text);
                    eAlumno.Descuento = cbxBeca.Text;
                    eAlumno.FinDescuento = DateTime.Now.AddMonths(2);
                    listaAlumnos.Add(eAlumno);
                    MessageBox.Show("Becario registrado con exito");
                    mostrar();
                }
                else
                {
                    MessageBox.Show("El usuario ya esta registrado");
                    limpiar();
                }
            else
                MessageBox.Show("Debe llenar todos los datos");
        }
        void mostrar()
        {
            //dgvListar.DataSource = null;
            //dgvListar.DataSource = listaAlumnos;
            dgvListar.DataSource = dAlumno.listObjectToDataTable(listaAlumnos); ;
            dgvListar.ClearSelection();
            limpiar();
        }
        void limpiar()
        {
            txtDni.Clear();
            cbxBeca.SelectedIndex = -1;
            lbNombre.Text = "Nombre: ";
            txtDni.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtDni.Text != "" && cbxBeca.Text != "")
            {
                EAlumno eAlumno = listaAlumnos.Find(x=> x.Dni == txtDni.Text);
                if (eAlumno != null)
                {
                    eAlumno.Descuento = cbxBeca.Text;
                    eAlumno.FinDescuento = DateTime.Now.AddMonths(2);
                    MessageBox.Show("Becario modificado con exito");
                    mostrar();
                }
            }
            else 
                MessageBox.Show("Debe llenar todos los datos");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtDni.Text != "")
            {
                int count = listaAlumnos.RemoveAll(x => x.Dni == txtDni.Text);  
                if(count > 0)
                    MessageBox.Show("Becario eliminado con exito");
                mostrar();
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            openFile.InitialDirectory = downloadsFolder;
            openFile.Filter = "xlsx (*.xlsx)|*.xlsx";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                cargarBoletas();
                MessageBox.Show("Tarea realizada exitosamente");
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
                    } else
                    {
                        fallo += dni + " " + nombre + " " + beneficio + "\n";
                    }
                }
                irow++;
            }
            mostrar();
            if(fallo.Length > 0)
            {
                MessageBox.Show("Existen alumnos que aun no se registraron, informacion copiada al portapapeles");
                Clipboard.SetText(fallo);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            foreach (EAlumno item in listaAlumnos)
                dAlumno.Mantenimiento(item, "update");
            MessageBox.Show("Tarea realizada exitosamente");
            listaAlumnos.Clear();
            mostrar();
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            if (txtDni.Text.Length == 8 && dAlumno.getAlumno(txtDni.Text) != null)
                lbNombre.Text = "Nombre: " + dAlumno.getAlumno(txtDni.Text).ApellidosNombres;
        }

        private void dgvListar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtDni.Text = dgvListar.Rows[e.RowIndex].Cells[1].Value.ToString();
                cbxBeca.Text = dgvListar.Rows[e.RowIndex].Cells[9].Value.ToString();
            }
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
    }
}
