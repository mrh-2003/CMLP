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
        public List<EPGCU> listaPGCU = new List<EPGCU>();
        DPGCU dpgcu = new DPGCU();
        DBancoDTO dBanco = new DBancoDTO();
        DCalendario dCalendario = new DCalendario();
        public CTxtBanco()
        {
            InitializeComponent();
        }
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            lista.Clear();
            string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            openFile.InitialDirectory = downloadsFolder;
            openFile.Filter = "PAGOS (*.PAGOS)|*.PAGOS";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (dBanco.InsertarArchivos(openFile.FileName))
                {
                    cargarDatos();
                    cargarBD();
                    mostrar();
                } else
                {
                    if (MessageBox.Show("¿Desea continuar?\nEl archivo ya fue cargado antes", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        cargarDatos();
                        cargarBD();
                        mostrar();
                    }
                }
            }
        }
        void cargarDatos()
        {
            try
            {
                string directionFile = openFile.FileName;
                using (StreamReader archivo = new StreamReader(directionFile))
                {
                    string linea;
                    while ((linea = archivo.ReadLine()) != null)
                    {
                        if (linea.Length >= 194 && linea.Substring(0, 4) != "9999")
                        {
                            EBancoDTO banco = new EBancoDTO();
                            EPGCU pgcu = new EPGCU();
                            banco.NCredito = linea.Substring(8, 8);
                            banco.NCuota = Convert.ToInt32(linea.Substring(16, 4));
                            string fechaString = linea.Substring(20, 8);
                            DateTime fecha = DateTime.ParseExact(fechaString, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                            banco.FVncmto = fecha;
                            fechaString = linea.Substring(28, 8);
                            fecha = DateTime.ParseExact(fechaString, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                            banco.FPago = fecha;
                            decimal numero = Convert.ToDecimal(Convert.ToInt32(linea.Substring(59, 13)) + (Convert.ToInt32(linea.Substring(72, 2)) / 100.0));
                            banco.SImporte = numero;
                            banco.ACliente = linea.Substring(74, 30);
                            numero = Convert.ToDecimal(Convert.ToInt32(linea.Substring(135, 13)) + (Convert.ToInt32(linea.Substring(148, 2)) / 100.0));
                            banco.SInterMora = numero;
                            numero = Convert.ToDecimal(Convert.ToInt32(linea.Substring(180, 13)) + (Convert.ToInt32(linea.Substring(193, 2)) / 100.0));
                            banco.SPagado = numero;
                            pgcu.codigo = banco.NCredito;
                            pgcu.nombres = banco.ACliente;
                            pgcu.importe = banco.SImporte;
                            pgcu.mora = banco.SInterMora;
                            pgcu.fecha = banco.FPago;
                            listaPGCU.Add(pgcu);
                            lista.Add(banco);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }
        void cargarBD()
        {
            foreach (EBancoDTO banco in lista)
                dBanco.Mantenimiento(banco, "insert");
            foreach (EPGCU epgcu in listaPGCU)
                dpgcu.Mantenimiento(epgcu, "insert");
        }

        void mostrar()
        {
            dgvListar.DataSource = dBanco.ListarSinCalendario();
            if(dgvListar.RowCount  > 0 )
                dgvListar.Rows[0].Selected = true;
        }

        private void CTxtBanco_Load(object sender, EventArgs e)
        {
            mostrar();
        }

        private void dgvListar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvListar.Columns["btnCompletar"].Index && e.RowIndex >= 0)
            {
                EBancoDTO eBanco = dgvListar.Rows[e.RowIndex].DataBoundItem as EBancoDTO;
                if (completarCalendario(eBanco))
                {
                    eBanco.Calendario = true;
                    dBanco.Mantenimiento(eBanco, "update");
                }
                else
                    MessageBox.Show("Este registro no coincide con ningun dato del calendario por lo cual no se puede eliminar, revise el cobro y el calendario de pagos y modifique manualmente el pago realizado por el alumno, despues de ello puede precionar el boton de eliminar todo para que se limpie los pagos pendientes de boleta");
                mostrar();
            }
        }

        bool completarCalendario(EBancoDTO banco)
        {
            List<ECalendarioDTO> calendarios = dCalendario.ListarListaDTO();
            ECalendarioDTO eCalendarioDTO = calendarios.Find(x =>
            x.Dni == banco.NCredito && x.Vencimiento == banco.FVncmto &&
            x.ConceptoCodigo == banco.NCuota && banco.SImporte + x.MontoPagado <= x.MontoTotal);
            if (eCalendarioDTO != null)
            {
                ECalendario calendario = dCalendario.getCalendario(eCalendarioDTO.Id);
                if (calendario != null)
                {
                    calendario.Cancelacion = banco.FPago;
                    calendario.Mora += banco.SInterMora;
                    calendario.MontoPagado += banco.SImporte;
                    dCalendario.Mantenimiento(calendario, "update");
                    return true;
                }
            }
            return false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (dgvListar.Rows.Count > 0)
            {
                if (MessageBox.Show("¿Está seguro de que desea continuar?\nEsta accion eliminara todos los registros", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dBanco.ActualizarCalendario();
                    mostrar();
                }
            }
            else
                MessageBox.Show("No hay registros en la tabla");
        }

        private void dgvListar_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvListar.SelectedRows.Count > 0) // asegurarse de que haya una fila seleccionada
                {
                    EBancoDTO eBanco = dgvListar.SelectedRows[0].DataBoundItem as EBancoDTO;
                    btnNombre.Text = "Nombre: " + eBanco.ACliente;
                    btnDNI.Text = "DNI: " + eBanco.NCredito;
                    btnImp.Text = "Importe: " + eBanco.SPagado;
                    btnDesc.Text = "Descripcion: " + (new DConcepto()).getConcepto(eBanco.NCuota).Concepto;
        }
            }
            catch (Exception)
            {
                btnNombre.Text = "Nombre: ";
                btnDNI.Text = "DNI: " ;
                btnImp.Text = "Importe: ";
                btnDesc.Text = "Descripcion: ";
            }
        }

        private void btnNombre_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(btnNombre.Text.Split(':')[1].Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("No existe dato para copiar");
            }
                
        }

        private void btnDNI_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(btnDNI.Text.Split(':')[1].Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("No existe dato para copiar");
            }
               
        }

        private void btnImp_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(btnImp.Text.Split(':')[1].Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("No existe dato para copiar");
            }
            
        }

        private void btnDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(btnDesc.Text.Split(':')[1].Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("No existe dato para copiar");
            }
        }
    }
}
