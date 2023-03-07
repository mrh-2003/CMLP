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
    public partial class MUsuariosAdmin : Form
    {
        private const string TITULO_ALERTA = "Error de Entrada";
        DUsuario dUsuario = new DUsuario();
        string aux = "";
        int index = -1;
        DHistorial dHistorial = new DHistorial();
        int id = Login.id;
        public MUsuariosAdmin()
        {
            InitializeComponent();
        }
        private void MUsuarios_Load(object sender, EventArgs e)
        {
            mostrar();
        }
        void mostrar()
        {
            dgvListar.DataSource = dUsuario.Listar();
            dgvListar.ClearSelection();
            limpiar();
        }
        void limpiar()
        {
            txtId.Clear();
            txtUsuario.Clear();
            txtContrasenia.Clear();
            cbxRol.SelectedIndex = -1;
            txtUsuario.Focus();
        }
        void mantenimiento(string opcion)
        {
            if (txtId.Text!=""  && txtUsuario.Text != "" && txtContrasenia.Text != "" && cbxRol.Text != "")
            {
                EUsuario eUsuario = new EUsuario()
                {
                    Id = Convert.ToInt32(txtId.Text),
                    Usuario = txtUsuario.Text,
                    Contrasenia = txtContrasenia.Text,
                    Rol = cbxRol.Text
                };
                string mensaje = dUsuario.Mantenimiento(eUsuario, opcion);
                MessageBox.Show(mensaje);
                //EHistorial historial = new EHistorial()
                //{
                //    Descripcion = mensaje,
                //    Usuario = (new DUsuario()).getUsuario(id).Usuario,
                //    Fecha = DateTime.Now
                //};
                //dHistorial.Insertar(historial);
                mostrar();
            }
            else
            {
                MessageBox.Show("Todos los campos deben estar llenos");
            }
        }
        bool existeUsuario()
        {
            for (int i = 0; i < dgvListar.Rows.Count; i++)
                if(index != i && dgvListar.Rows[i].Cells[1].Value.ToString() == txtUsuario.Text)
                {
                     MessageBox.Show("No se puede registrar dos usuarios iguales");
                     return true;
                }
            return false;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvListar.Rows)
                if(txtUsuario.Text == item.Cells[1].Value.ToString())
                {
                    MessageBox.Show("No se puede registrar dos usuarios iguales");
                    return;
                }
            txtId.Text = "0";
            if(txtContrasenia.Text != "")
                txtContrasenia.Text = Utilidades.GetSHA256(txtContrasenia.Text);
            mantenimiento("insert");
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!existeUsuario())
            {
                if (txtContrasenia.Text == "")
                    txtContrasenia.Text = aux;
                else
                    txtContrasenia.Text = Utilidades.GetSHA256(txtContrasenia.Text);
                mantenimiento("update");
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            txtContrasenia.Text = aux;
            mantenimiento("delete");
        }
        private void dgvListar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            if (index != -1)
            {
                txtId.Text = dgvListar.Rows[index].Cells[0].Value.ToString();
                txtUsuario.Text = dgvListar.Rows[index].Cells[1].Value.ToString();
                aux = dgvListar.Rows[index].Cells[2].Value.ToString();
                cbxRol.Text = dgvListar.Rows[index].Cells[3].Value.ToString();
            }
        }
    }
}
