namespace FrbUiEditor.Gui
{
    partial class Form1
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
            this.ControlsHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // ControlsHost
            // 
            this.ControlsHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlsHost.Location = new System.Drawing.Point(0, 0);
            this.ControlsHost.Name = "ControlsHost";
            this.ControlsHost.Size = new System.Drawing.Size(679, 397);
            this.ControlsHost.TabIndex = 0;
            this.ControlsHost.Child = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 397);
            this.Controls.Add(this.ControlsHost);
            this.Name = "Form1";
            this.Text = "FrbUi Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost ControlsHost;
    }
}

