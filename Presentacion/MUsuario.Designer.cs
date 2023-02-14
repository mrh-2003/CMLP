
namespace Presentacion
{
    partial class MUsuario
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
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtContraAnt = new System.Windows.Forms.TextBox();
            this.txtContraNuev = new System.Windows.Forms.TextBox();
            this.txtConfContNuev = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(380, 112);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(100, 20);
            this.txtUsuario.TabIndex = 0;
            // 
            // txtContraAnt
            // 
            this.txtContraAnt.Location = new System.Drawing.Point(380, 152);
            this.txtContraAnt.Name = "txtContraAnt";
            this.txtContraAnt.PasswordChar = '*';
            this.txtContraAnt.Size = new System.Drawing.Size(100, 20);
            this.txtContraAnt.TabIndex = 1;
            // 
            // txtContraNuev
            // 
            this.txtContraNuev.Location = new System.Drawing.Point(380, 192);
            this.txtContraNuev.Name = "txtContraNuev";
            this.txtContraNuev.PasswordChar = '*';
            this.txtContraNuev.Size = new System.Drawing.Size(100, 20);
            this.txtContraNuev.TabIndex = 2;
            // 
            // txtConfContNuev
            // 
            this.txtConfContNuev.Location = new System.Drawing.Point(380, 230);
            this.txtConfContNuev.Name = "txtConfContNuev";
            this.txtConfContNuev.PasswordChar = '*';
            this.txtConfContNuev.Size = new System.Drawing.Size(100, 20);
            this.txtConfContNuev.TabIndex = 3;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(401, 298);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // MUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtConfContNuev);
            this.Controls.Add(this.txtContraNuev);
            this.Controls.Add(this.txtContraAnt);
            this.Controls.Add(this.txtUsuario);
            this.Name = "MUsuario";
            this.Text = "MUsuario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtContraAnt;
        private System.Windows.Forms.TextBox txtContraNuev;
        private System.Windows.Forms.TextBox txtConfContNuev;
        private System.Windows.Forms.Button btnAceptar;
    }
}