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
    public partial class RPencionesXMesGrado : Form
    {
        DCalendario dCalendario = new DCalendario();
        public RPencionesXMesGrado()
        {
            InitializeComponent();
        }

        private void RPencionesXMesGrado_Load(object sender, EventArgs e)
        {
            mostrar();

        }

        void mostrar()
        {
            dgvListar.DataSource = dCalendario.ListarPencionesXMesXGrado(cbxMes.Text == "" ? 1 : Convert.ToInt32(cbxMes.Text), cbxGrado.Text == "" ? 3 : Convert.ToInt32(cbxGrado.Text));
            lb3ro.Text = "Tercero: " + dCalendario.TotalPorGrado(cbxMes.Text == "" ? 1 : Convert.ToInt32(cbxMes.Text), 3).ToString();
            lb4to.Text = "Cuarto: " + dCalendario.TotalPorGrado(cbxMes.Text == "" ? 1 : Convert.ToInt32(cbxMes.Text), 4).ToString();
            lb5to.Text = "Quinto: " + dCalendario.TotalPorGrado(cbxMes.Text == "" ? 1 : Convert.ToInt32(cbxMes.Text), 5).ToString();
        }

        private void cbxMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrar();
        }

        private void cbxGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrar();
        }
    }

    }
}
