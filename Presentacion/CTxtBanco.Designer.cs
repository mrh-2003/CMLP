namespace Presentacion
{
    partial class CTxtBanco
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
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.dgvListar = new System.Windows.Forms.DataGridView();
            this.btnCompletar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnNombre = new System.Windows.Forms.Button();
            this.btnDNI = new System.Windows.Forms.Button();
            this.btnImp = new System.Windows.Forms.Button();
            this.btnDesc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).BeginInit();
            this.SuspendLayout();
            // 
            // openFile
            // 
            this.openFile.FileName = "openFileDialog1";
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(39, 12);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(75, 23);
            this.btnAbrir.TabIndex = 0;
            this.btnAbrir.Text = "Abrir";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // dgvListar
            // 
            this.dgvListar.AllowUserToAddRows = false;
            this.dgvListar.AllowUserToResizeRows = false;
            this.dgvListar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnCompletar});
            this.dgvListar.Location = new System.Drawing.Point(27, 124);
            this.dgvListar.Name = "dgvListar";
            this.dgvListar.ReadOnly = true;
            this.dgvListar.RowHeadersVisible = false;
            this.dgvListar.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvListar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListar.Size = new System.Drawing.Size(733, 344);
            this.dgvListar.TabIndex = 10;
            this.dgvListar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListar_CellContentClick);
            this.dgvListar.SelectionChanged += new System.EventHandler(this.dgvListar_SelectionChanged);
            // 
            // btnCompletar
            // 
            this.btnCompletar.HeaderText = "Completar";
            this.btnCompletar.Name = "btnCompletar";
            this.btnCompletar.ReadOnly = true;
            this.btnCompletar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btnCompletar.Text = "Eliminar";
            this.btnCompletar.ToolTipText = "Eliminar";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(145, 12);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 11;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnNombre
            // 
            this.btnNombre.Location = new System.Drawing.Point(68, 38);
            this.btnNombre.Margin = new System.Windows.Forms.Padding(0);
            this.btnNombre.Name = "btnNombre";
            this.btnNombre.Size = new System.Drawing.Size(671, 21);
            this.btnNombre.TabIndex = 14;
            this.btnNombre.Text = "Nombre:";
            this.btnNombre.UseVisualStyleBackColor = true;
            this.btnNombre.Click += new System.EventHandler(this.btnNombre_Click);
            // 
            // btnDNI
            // 
            this.btnDNI.Location = new System.Drawing.Point(68, 59);
            this.btnDNI.Margin = new System.Windows.Forms.Padding(0);
            this.btnDNI.Name = "btnDNI";
            this.btnDNI.Size = new System.Drawing.Size(671, 21);
            this.btnDNI.TabIndex = 15;
            this.btnDNI.Text = "DNI:";
            this.btnDNI.UseVisualStyleBackColor = true;
            this.btnDNI.Click += new System.EventHandler(this.btnDNI_Click);
            // 
            // btnImp
            // 
            this.btnImp.Location = new System.Drawing.Point(68, 80);
            this.btnImp.Margin = new System.Windows.Forms.Padding(0);
            this.btnImp.Name = "btnImp";
            this.btnImp.Size = new System.Drawing.Size(671, 21);
            this.btnImp.TabIndex = 16;
            this.btnImp.Text = "Importe:";
            this.btnImp.UseVisualStyleBackColor = true;
            this.btnImp.Click += new System.EventHandler(this.btnImp_Click);
            // 
            // btnDesc
            // 
            this.btnDesc.Location = new System.Drawing.Point(68, 100);
            this.btnDesc.Margin = new System.Windows.Forms.Padding(0);
            this.btnDesc.Name = "btnDesc";
            this.btnDesc.Size = new System.Drawing.Size(671, 21);
            this.btnDesc.TabIndex = 17;
            this.btnDesc.Text = "Descripcion: ";
            this.btnDesc.UseVisualStyleBackColor = true;
            this.btnDesc.Click += new System.EventHandler(this.btnDesc_Click);
            // 
            // CTxtBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 546);
            this.Controls.Add(this.btnDesc);
            this.Controls.Add(this.btnImp);
            this.Controls.Add(this.btnDNI);
            this.Controls.Add(this.btnNombre);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dgvListar);
            this.Controls.Add(this.btnAbrir);
            this.Name = "CTxtBanco";
            this.Text = "CTxtBanco";
            this.Load += new System.EventHandler(this.CTxtBanco_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.DataGridView dgvListar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridViewButtonColumn btnCompletar;
        private System.Windows.Forms.Button btnNombre;
        private System.Windows.Forms.Button btnDNI;
        private System.Windows.Forms.Button btnImp;
        private System.Windows.Forms.Button btnDesc;
    }
}