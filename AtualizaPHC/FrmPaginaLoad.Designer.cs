namespace AtualizaPorFTP
{
    partial class PaginaLoad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaginaLoad));
            this.Loading = new System.Windows.Forms.ProgressBar();
            this.Pic = new System.Windows.Forms.PictureBox();
            this.lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Pic)).BeginInit();
            this.SuspendLayout();
            // 
            // Loading
            // 
            this.Loading.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Loading.Location = new System.Drawing.Point(43, 370);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(728, 54);
            this.Loading.TabIndex = 1;
            this.Loading.UseWaitCursor = true;
            // 
            // Pic
            // 
            this.Pic.Image = global::AtualizaPorFTP.Properties.Resources.Nc_Logo;
            this.Pic.InitialImage = ((System.Drawing.Image)(resources.GetObject("Pic.InitialImage")));
            this.Pic.Location = new System.Drawing.Point(77, 3);
            this.Pic.Name = "Pic";
            this.Pic.Size = new System.Drawing.Size(638, 278);
            this.Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic.TabIndex = 0;
            this.Pic.TabStop = false;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(254, 341);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(269, 20);
            this.lbl.TabIndex = 2;
            this.lbl.Text = "ESTAMOS A ATUALIZAR O SEU PHC";
            // 
            // PaginaLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.Loading);
            this.Controls.Add(this.Pic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaginaLoad";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PaginaLoad";
            this.Activated += new System.EventHandler(this.PaginaLoad_Activated);
            this.Shown += new System.EventHandler(this.PaginaLoad_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Pic;
        private System.Windows.Forms.ProgressBar Loading;
        private System.Windows.Forms.Label lbl;
    }
}

