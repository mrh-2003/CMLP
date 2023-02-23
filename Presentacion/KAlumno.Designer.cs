
namespace Presentacion
{
    partial class KAlumno
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txbPendiente = new System.Windows.Forms.TextBox();
            this.txbCancelado = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txbTelefono = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSeccion = new System.Windows.Forms.Label();
            this.lblAnio = new System.Windows.Forms.Label();
            this.txbDireccion = new System.Windows.Forms.TextBox();
            this.tbxAlumno = new System.Windows.Forms.TextBox();
            this.txbCorreo = new System.Windows.Forms.TextBox();
            this.txbDni = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fechahora = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConsultar
            // 
            this.btnConsultar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(19)))));
            this.btnConsultar.FlatAppearance.BorderSize = 0;
            this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.ForeColor = System.Drawing.Color.White;
            this.btnConsultar.Location = new System.Drawing.Point(769, 271);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(129, 44);
            this.btnConsultar.TabIndex = 6;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 338);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(944, 267);
            this.dataGridView1.TabIndex = 7;
            // 
            // txbPendiente
            // 
            this.txbPendiente.Font = new System.Drawing.Font("Verdana", 11F);
            this.txbPendiente.Location = new System.Drawing.Point(517, 651);
            this.txbPendiente.Name = "txbPendiente";
            this.txbPendiente.Size = new System.Drawing.Size(152, 25);
            this.txbPendiente.TabIndex = 56;
            // 
            // txbCancelado
            // 
            this.txbCancelado.Font = new System.Drawing.Font("Verdana", 11F);
            this.txbCancelado.Location = new System.Drawing.Point(291, 651);
            this.txbCancelado.Name = "txbCancelado";
            this.txbCancelado.Size = new System.Drawing.Size(152, 25);
            this.txbCancelado.TabIndex = 55;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 11F);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(552, 628);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 18);
            this.label14.TabIndex = 53;
            this.label14.Text = "Pendiente";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 11F);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(325, 628);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 18);
            this.label13.TabIndex = 54;
            this.label13.Text = "Cancelado";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 11F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(376, 610);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(207, 18);
            this.label11.TabIndex = 52;
            this.label11.Text = "** Resumen de Totales **";
            // 
            // txbTelefono
            // 
            this.txbTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbTelefono.Location = new System.Drawing.Point(131, 269);
            this.txbTelefono.MaxLength = 9;
            this.txbTelefono.Name = "txbTelefono";
            this.txbTelefono.Size = new System.Drawing.Size(180, 23);
            this.txbTelefono.TabIndex = 4;
            this.txbTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbTelefono_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 11F);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(43, 271);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 18);
            this.label17.TabIndex = 49;
            this.label17.Text = "Telefono:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 11F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(133, 353);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 18);
            this.label6.TabIndex = 50;
            // 
            // lblSeccion
            // 
            this.lblSeccion.AutoSize = true;
            this.lblSeccion.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccion.ForeColor = System.Drawing.Color.White;
            this.lblSeccion.Location = new System.Drawing.Point(749, 189);
            this.lblSeccion.Name = "lblSeccion";
            this.lblSeccion.Size = new System.Drawing.Size(23, 18);
            this.lblSeccion.TabIndex = 93;
            this.lblSeccion.Text = "...";
            // 
            // lblAnio
            // 
            this.lblAnio.AutoSize = true;
            this.lblAnio.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnio.ForeColor = System.Drawing.Color.White;
            this.lblAnio.Location = new System.Drawing.Point(609, 190);
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.Size = new System.Drawing.Size(23, 18);
            this.lblAnio.TabIndex = 92;
            this.lblAnio.Text = "...";
            // 
            // txbDireccion
            // 
            this.txbDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbDireccion.Location = new System.Drawing.Point(129, 309);
            this.txbDireccion.Name = "txbDireccion";
            this.txbDireccion.Size = new System.Drawing.Size(435, 23);
            this.txbDireccion.TabIndex = 5;
            this.txbDireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbDireccion_KeyPress);
            // 
            // tbxAlumno
            // 
            this.tbxAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAlumno.Location = new System.Drawing.Point(129, 190);
            this.tbxAlumno.Name = "tbxAlumno";
            this.tbxAlumno.Size = new System.Drawing.Size(404, 23);
            this.tbxAlumno.TabIndex = 2;
            this.tbxAlumno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxAlumno_KeyPress);
            // 
            // txbCorreo
            // 
            this.txbCorreo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbCorreo.Location = new System.Drawing.Point(129, 229);
            this.txbCorreo.Name = "txbCorreo";
            this.txbCorreo.Size = new System.Drawing.Size(264, 23);
            this.txbCorreo.TabIndex = 3;
            // 
            // txbDni
            // 
            this.txbDni.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbDni.Location = new System.Drawing.Point(129, 153);
            this.txbDni.MaxLength = 8;
            this.txbDni.Name = "txbDni";
            this.txbDni.Size = new System.Drawing.Size(121, 23);
            this.txbDni.TabIndex = 1;
            this.txbDni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbDni_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 11F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(682, 190);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 18);
            this.label10.TabIndex = 87;
            this.label10.Text = "Sección:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 11F);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(43, 310);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 18);
            this.label16.TabIndex = 85;
            this.label16.Text = "Direccion:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 11F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(571, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 18);
            this.label9.TabIndex = 86;
            this.label9.Text = "Año:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 11F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(43, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 18);
            this.label7.TabIndex = 84;
            this.label7.Text = "Alumno:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 11F);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(43, 232);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 18);
            this.label15.TabIndex = 83;
            this.label15.Text = "Correo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 11F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(43, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 18);
            this.label4.TabIndex = 82;
            this.label4.Text = "Dni:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 11F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(53, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 18);
            this.label3.TabIndex = 81;
            this.label3.Text = "** KARDEX DE PAGOS **";
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("Verdana", 11F);
            this.lblHora.ForeColor = System.Drawing.Color.White;
            this.lblHora.Location = new System.Drawing.Point(800, 116);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(42, 18);
            this.lblHora.TabIndex = 80;
            this.lblHora.Text = "hora";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Verdana", 11F);
            this.lblFecha.ForeColor = System.Drawing.Color.White;
            this.lblFecha.Location = new System.Drawing.Point(800, 73);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(48, 18);
            this.lblFecha.TabIndex = 78;
            this.lblFecha.Text = "fecha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 11F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(53, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(285, 18);
            this.label2.TabIndex = 79;
            this.label2.Text = "COLEGIO MILITAR \"LEONCIO PRADO\"";
            // 
            // fechahora
            // 
            this.fechahora.Enabled = true;
            this.fechahora.Tick += new System.EventHandler(this.fechahora_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(319, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 25);
            this.label1.TabIndex = 79;
            this.label1.Text = "Karde´x de un Determinado Alumno";
            // 
            // KAlumno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(65)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(992, 681);
            this.Controls.Add(this.lblSeccion);
            this.Controls.Add(this.lblAnio);
            this.Controls.Add(this.txbDireccion);
            this.Controls.Add(this.tbxAlumno);
            this.Controls.Add(this.txbCorreo);
            this.Controls.Add(this.txbDni);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txbPendiente);
            this.Controls.Add(this.txbCancelado);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txbTelefono);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "KAlumno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KardexAlumno";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txbPendiente;
        private System.Windows.Forms.TextBox txbCancelado;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txbTelefono;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSeccion;
        private System.Windows.Forms.Label lblAnio;
        private System.Windows.Forms.TextBox txbDireccion;
        private System.Windows.Forms.TextBox tbxAlumno;
        private System.Windows.Forms.TextBox txbCorreo;
        private System.Windows.Forms.TextBox txbDni;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer fechahora;
        private System.Windows.Forms.Label label1;
    }
}