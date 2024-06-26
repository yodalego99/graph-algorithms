namespace Gráfelméleti_algoritmusok
{
    partial class LoadMenu
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
            this.graphThumbnails = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // graphThumbnails
            // 
            this.graphThumbnails.AutoScroll = true;
            this.graphThumbnails.BackColor = System.Drawing.SystemColors.ControlLight;
            this.graphThumbnails.Location = new System.Drawing.Point(12, 12);
            this.graphThumbnails.Name = "graphThumbnails";
            this.graphThumbnails.Size = new System.Drawing.Size(781, 426);
            this.graphThumbnails.TabIndex = 0;
            // 
            // LoadMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 450);
            this.Controls.Add(this.graphThumbnails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoadMenu";
            this.Text = "LoadMenu";
            this.ResumeLayout(false);

        }

        #endregion

        public FlowLayoutPanel graphThumbnails;
    }
}