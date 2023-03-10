﻿using System;
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

        private void subMenukardexAlumno_Click(object sender, EventArgs e)
        {
            abrirFormHija(new KAlumno());
        }

        private void subMenukardexGrado_Click(object sender, EventArgs e)
        {
            abrirFormHija(new KGrado());
        }

        private void subMenukardexResumenDeGrado_Click(object sender, EventArgs e)
        {
            abrirFormHija(new KGeneral());
        }

        private void btnCargarAlum_Click(object sender, EventArgs e)
        {
            menuCAlumnos.Show(btnCargarAlum, btnCargarAlum.Width, 4);
        }

        private void btnKardex_Click(object sender, EventArgs e)
        {
            menuKardex.Show(btnKardex, btnKardex.Width, 4);
        }

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            abrirFormHija(new MCalendarioPagos());
        }

        private void btnRAlumnos_Click(object sender, EventArgs e)
        {
            menuRAlum.Show(btnRAlumnos, btnRAlumnos.Width, 4);
        }

        private void btnBoleta_Click(object sender, EventArgs e)
        {
            abrirFormHija(new MBoleta());
        }

        private void btnCargarBol_Click(object sender, EventArgs e)
        {
            abrirFormHija(new CBoletas());
        }

        private void btnConceptos_Click(object sender, EventArgs e)
        {
            abrirFormHija(new MConceptos());
        }

        private void btnRBoletas_Click(object sender, EventArgs e)
        {
            menuRBoleta.Show(btnRBoletas, btnRBoletas.Width, 4);
        }

        private void submenuCAlum_Click(object sender, EventArgs e)
        {
            abrirFormHija(new CAlumnos());
        }

        private void submenuCBecarios_Click(object sender, EventArgs e)
        {
            abrirFormHija(new CBecarios());
        }

        private void submenuRBConcepto_Click(object sender, EventArgs e)
        {
            abrirFormHija(new RBoletasXConcepto());
        }

        private void submenuRBFecha_Click(object sender, EventArgs e)
        {
            abrirFormHija(new RBoletasXFechas());
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            abrirFormHija(new Historial());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            abrirFormHija(new MUsuariosAdmin());
        }

        private void submenuPenGradoMes_Click(object sender, EventArgs e)
        {
            abrirFormHija(new RPencionesXMesGrado());
        }

        private void submenuDeuFecha_Click(object sender, EventArgs e)
        {
            abrirFormHija(new RDeudoresXFecha());
        }

        private void submenuDeuGrado_Click(object sender, EventArgs e)
        {
            abrirFormHija(new RDeudoresXFechaXGrado());
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
