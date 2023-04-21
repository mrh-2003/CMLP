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

namespace Presentacion
{
    public partial class RAlumnosXGrado : Form
    {
        DAlumno dAlumno = new DAlumno();
        public RAlumnosXGrado()
        {
            InitializeComponent();
        }

        private void RAlumnosXGrado_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dAlumno.Listar();
        }

        private void cbxGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            int grado = cbxGrado.Text == "" ? 0 : Convert.ToInt32(cbxGrado.Text);
            int seccion = cbxSeccion.Text == "" ? 0 : Convert.ToInt32(cbxSeccion.Text);
            dgvListar.DataSource = dAlumno.FiltrarGradoSeccion(grado, seccion);
        }

        private void cbxSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int grado = cbxGrado.Text == "" ? 0 : Convert.ToInt32(cbxGrado.Text);
            int seccion = cbxSeccion.Text == "" ? 0 : Convert.ToInt32(cbxSeccion.Text);
            dgvListar.DataSource = dAlumno.FiltrarGradoSeccion(grado, seccion);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbxGrado.SelectedIndex = -1;
            cbxSeccion.SelectedIndex = -1;
            dgvListar.DataSource = dAlumno.Listar();
        }

        private void cbxSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnClear.PerformClick();
            }
        }
    }
}
