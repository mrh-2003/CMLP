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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbxGrado = new System.Windows.Forms.ComboBox();
            this.dgvListar = new System.Windows.Forms.DataGridView();
            this.cbxMes = new System.Windows.Forms.ComboBox();
            this.lb3ro = new System.Windows.Forms.Label();
            this.lb4to = new System.Windows.Forms.Label();
            this.lb5to = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.cbxGrado.Location = new System.Drawing.Point(616, 71);
            this.cbxGrado.Margin = new System.Windows.Forms.Padding(4);
            this.cbxGrado.Name = "cbxGrado";
            this.cbxGrado.Size = new System.Drawing.Size(168, 24);
            this.cbxGrado.TabIndex = 11;
            this.cbxGrado.SelectedIndexChanged += new System.EventHandler(this.cbxGrado_SelectedIndexChanged);
            // 
            // dgvListar
            // 
            this.dgvListar.AllowUserToAddRows = false;
            this.dgvListar.AllowUserToResizeRows = false;
            this.dgvListar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvListar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListar.Location = new System.Drawing.Point(13, 197);
            this.dgvListar.Margin = new System.Windows.Forms.Padding(4);
            this.dgvListar.Name = "dgvListar";
            this.dgvListar.ReadOnly = true;
            this.dgvListar.RowHeadersVisible = false;
            this.dgvListar.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvListar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListar.Size = new System.Drawing.Size(966, 471);
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
            this.cbxMes.Location = new System.Drawing.Point(196, 71);
            this.cbxMes.Margin = new System.Windows.Forms.Padding(4);
            this.cbxMes.Name = "cbxMes";
            this.cbxMes.Size = new System.Drawing.Size(226, 24);
            this.cbxMes.TabIndex = 13;
            this.cbxMes.SelectedIndexChanged += new System.EventHandler(this.cbxMes_SelectedIndexChanged);
            // 
            // lb3ro
            // 
            this.lb3ro.AutoSize = true;
            this.lb3ro.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb3ro.ForeColor = System.Drawing.Color.White;
            this.lb3ro.Location = new System.Drawing.Point(194, 160);
            this.lb3ro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb3ro.Name = "lb3ro";
            this.lb3ro.Size = new System.Drawing.Size(70, 17);
            this.lb3ro.TabIndex = 14;
            this.lb3ro.Text = "Tercero: ";
            // 
            // lb4to
            // 
            this.lb4to.AutoSize = true;
            this.lb4to.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb4to.ForeColor = System.Drawing.Color.White;
            this.lb4to.Location = new System.Drawing.Point(445, 160);
            this.lb4to.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb4to.Name = "lb4to";
            this.lb4to.Size = new System.Drawing.Size(62, 17);
            this.lb4to.TabIndex = 15;
            this.lb4to.Text = "Cuarto:";
            // 
            // lb5to
            // 
            this.lb5to.AutoSize = true;
            this.lb5to.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb5to.ForeColor = System.Drawing.Color.White;
            this.lb5to.Location = new System.Drawing.Point(705, 160);
            this.lb5to.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb5to.Name = "lb5to";
            this.lb5to.Size = new System.Drawing.Size(66, 17);
            this.lb5to.TabIndex = 16;
            this.lb5to.Text = "Quinto: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(247, 124);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(460, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Total de penciones por cada grado en el mes seleccionado";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(241, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(446, 25);
            this.label11.TabIndex = 101;
            this.label11.Text = "Reporte de Pensiones por Mes y Grado";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(152, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Mes:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(556, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Grado:";
            // 
            // RPencionesXMesGrado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(65)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(992, 681);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb5to);
            this.Controls.Add(this.lb4to);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb3ro);
            this.Controls.Add(this.cbxMes);
            this.Controls.Add(this.dgvListar);
            this.Controls.Add(this.cbxGrado);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}