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
    public partial class MBoleta : Form
    {
        private const string TITULO_ALERTA = "Error de Entrada";
        DBoleta dBoleta = new DBoleta();
        DConcepto dConcepto = new DConcepto();
        DAlumno dAlumno = new DAlumno();
        public MBoleta()
        {
            InitializeComponent();
        }

        private void MBoleta_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dBoleta.Listar();
            cbxConcepto.DataSource = dConcepto.Listar();
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void dgvListar_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            if(txtDni.Text.Length == 8 && dAlumno.getAlumno(txtDni.Text) != null)
                lbNombre.Text =  "Nombre: " + dAlumno.getAlumno(txtDni.Text).ApellidosNombres;
        }
        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Este campo solo acepta numeros. Introduce un valor válido", TITULO_ALERTA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
