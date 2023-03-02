namespace Presentacion
{
    partial class RPencionesXMesGrado
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
            this.cbxGrado = new System.Windows.Forms.ComboBox();
            this.dgvListar = new System.Windows.Forms.DataGridView();
            this.cbxMes = new System.Windows.Forms.ComboBox();
            this.lb3ro = new System.Windows.Forms.Label();
            this.lb4to = new System.Windows.Forms.Label();
            this.lb5to = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxGrado
            // 
            this.cbxGrado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGrado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGrado.FormattingEnabled = true;
            this.cbxGrado.Items.AddRange(new object[] {
            "3",
            "4",
            "5"});
            this.cbxGrado.Location = new System.Drawing.Point(563, 22);
            this.cbxGrado.Name = "cbxGrado";
            this.cbxGrado.Size = new System.Drawing.Size(145, 24);
            this.cbxGrado.TabIndex = 11;
            this.cbxGrado.SelectedIndexChanged += new System.EventHandler(this.cbxGrado_SelectedIndexChanged);
            // 
            // dgvListar
            // 
            this.dgvListar.AllowUserToAddRows = false;
            this.dgvListar.AllowUserToResizeRows = false;
            this.dgvListar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListar.Location = new System.Drawing.Point(12, 52);
            this.dgvListar.Name = "dgvListar";
            this.dgvListar.ReadOnly = true;
            this.dgvListar.RowHeadersVisible = false;
            this.dgvListar.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvListar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListar.Size = new System.Drawing.Size(776, 280);
            this.dgvListar.TabIndex = 12;
            // 
            // cbxMes
            // 
            this.cbxMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMes.FormattingEnabled = true;
            this.cbxMes.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cbxMes.Location = new System.Drawing.Point(139, 12);
            this.cbxMes.Name = "cbxMes";
            this.cbxMes.Size = new System.Drawing.Size(145, 24);
            this.cbxMes.TabIndex = 13;
            this.cbxMes.SelectedIndexChanged += new System.EventHandler(this.cbxMes_SelectedIndexChanged);
            // 
            // lb3ro
            // 
            this.lb3ro.AutoSize = true;
            this.lb3ro.Location = new System.Drawing.Point(174, 391);
            this.lb3ro.Name = "lb3ro";
            this.lb3ro.Size = new System.Drawing.Size(50, 13);
            this.lb3ro.TabIndex = 14;
            this.lb3ro.Text = "Tercero: ";
            // 
            // lb4to
            // 
            this.lb4to.AutoSize = true;
            this.lb4to.Location = new System.Drawing.Point(341, 391);
            this.lb4to.Name = "lb4to";
            this.lb4to.Size = new System.Drawing.Size(41, 13);
            this.lb4to.TabIndex = 15;
            this.lb4to.Text = "Cuarto:";
            // 
            // lb5to
            // 
            this.lb5to.AutoSize = true;
            this.lb5to.Location = new System.Drawing.Point(551, 391);
            this.lb5to.Name = "lb5to";
            this.lb5to.Size = new System.Drawing.Size(44, 13);
            this.lb5to.TabIndex = 16;
            this.lb5to.Text = "Quinto: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(255, 350);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(287, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Total de penciones por cada grado en el mes seleccionado";
            // 
            // RPencionesXMesGrado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb5to);
            this.Controls.Add(this.lb4to);
            this.Controls.Add(this.lb3ro);
            this.Controls.Add(this.cbxMes);
            this.Controls.Add(this.dgvListar);
            this.Controls.Add(this.cbxGrado);
            this.Name = "RPencionesXMesGrado";
            this.Text = "RPencionesXMesGrado";
            this.Load += new System.EventHandler(this.RPencionesXMesGrado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxGrado;
        private System.Windows.Forms.DataGridView dgvListar;
        private System.Windows.Forms.ComboBox cbxMes;
        private System.Windows.Forms.Label lb3ro;
        private System.Windows.Forms.Label lb4to;
        private System.Windows.Forms.Label lb5to;
        private System.Windows.Forms.Label label4;
    }
}