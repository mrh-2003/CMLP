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
    public partial class MUsuarios : Form
    {
        DUsuario dUsuario = new DUsuario();
        public MUsuarios()
        {
            InitializeComponent();
        }

        private void MUsuarios_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dUsuario.Listar();
        }
    }
}
