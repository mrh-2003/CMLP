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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        private void abrirFormHija(object frmHija)
        {
            if (this.panelEscritorio.Controls.Count > 0)
                this.panelEscritorio.Controls.RemoveAt(0);
            Form fh = frmHija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelEscritorio.Controls.Add(fh);
            this.panelEscritorio.Tag = fh;
            fh.Show();
        }
        private void btnMantAlumnos_Click(object sender, EventArgs e)
        {
            abrirFormHija(new MAlumnos());
        }
    }
}
