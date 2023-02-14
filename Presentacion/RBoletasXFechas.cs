﻿using System;
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
    public partial class RBoletasXFechas : Form
    {
        DBoleta dBoleta = new DBoleta();
        public RBoletasXFechas()
        {
            InitializeComponent();
        }

        private void RBoletasXFechas_Load(object sender, EventArgs e)
        {
            dgvListar.DataSource = dBoleta.Listar();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            dgvListar.DataSource = dBoleta.ListarPorFecha(dtpInicio.Value, dtpFinal.Value);
        }
    }
}