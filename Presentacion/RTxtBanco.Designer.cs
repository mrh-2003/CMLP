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
            this.rtxtInfo = new System.Windows.Forms.RichTextBox();
            this.btnEscribir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtxtInfo
            // 
            this.rtxtInfo.Location = new System.Drawing.Point(12, 30);
            this.rtxtInfo.Name = "rtxtInfo";
            this.rtxtInfo.Size = new System.Drawing.Size(776, 341);
            this.rtxtInfo.TabIndex = 0;
            this.rtxtInfo.Text = "";
            // 
            // btnEscribir
            // 
            this.btnEscribir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(19)))));
            this.btnEscribir.FlatAppearance.BorderSize = 0;
            this.btnEscribir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscribir.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscribir.ForeColor = System.Drawing.Color.White;
            this.btnEscribir.Location = new System.Drawing.Point(289, 390);
            this.btnEscribir.Name = "btnEscribir";
            this.btnEscribir.Size = new System.Drawing.Size(121, 35);
            this.btnEscribir.TabIndex = 1;
            this.btnEscribir.Text = "Escribir";
            this.btnEscribir.UseVisualStyleBackColor = false;
            this.btnEscribir.Click += new System.EventHandler(this.btnEscribir_Click);
            // 
            // RTxtBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnEscribir);
            this.Controls.Add(this.rtxtInfo);
            this.Name = "RTxtBanco";
            this.Text = "RTxtBanco";
            this.Load += new System.EventHandler(this.RTxtBanco_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtInfo;
        private System.Windows.Forms.Button btnEscribir;
    }
}