namespace Presentacion
{
    partial class RTxtBanco
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
            this.btnEscribir = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.rtxtInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnEscribir
            // 
            this.btnEscribir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(19)))));
            this.btnEscribir.FlatAppearance.BorderSize = 0;
            this.btnEscribir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscribir.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscribir.ForeColor = System.Drawing.Color.White;
            this.btnEscribir.Location = new System.Drawing.Point(400, 629);
            this.btnEscribir.Name = "btnEscribir";
            this.btnEscribir.Size = new System.Drawing.Size(202, 40);
            this.btnEscribir.TabIndex = 1;
            this.btnEscribir.Text = "Escribir";
            this.btnEscribir.UseVisualStyleBackColor = false;
            this.btnEscribir.Click += new System.EventHandler(this.btnEscribir_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(376, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(241, 25);
            this.label11.TabIndex = 89;
            this.label11.Text = "Generar txt al Banco";
            // 
            // rtxtInfo
            // 
            this.rtxtInfo.Location = new System.Drawing.Point(32, 57);
            this.rtxtInfo.Name = "rtxtInfo";
            this.rtxtInfo.Size = new System.Drawing.Size(924, 556);
            this.rtxtInfo.TabIndex = 0;
            this.rtxtInfo.Text = "";
            // 
            // RTxtBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(65)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(992, 681);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnEscribir);
            this.Controls.Add(this.rtxtInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RTxtBanco";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTxtBanco";
            this.Load += new System.EventHandler(this.RTxtBanco_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnEscribir;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox rtxtInfo;
    }
}