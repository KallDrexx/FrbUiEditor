﻿namespace FrbUiEditor.Gui
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
            this.mainEditorControl1 = new FrbUiEditor.Controls.MainEditorControl();
            this.SuspendLayout();
            // 
            // mainEditorControl1
            // 
            this.mainEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.mainEditorControl1.Name = "mainEditorControl1";
            this.mainEditorControl1.Size = new System.Drawing.Size(679, 397);
            this.mainEditorControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 397);
            this.Controls.Add(this.mainEditorControl1);
            this.Name = "Form1";
            this.Text = "FrbUi Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MainEditorControl mainEditorControl1;
    }
}

