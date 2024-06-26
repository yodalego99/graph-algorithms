namespace Gráfelméleti_algoritmusok
{
    partial class RandomWeights
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
            this.minTextBox = new System.Windows.Forms.TextBox();
            this.maxTextBox = new System.Windows.Forms.TextBox();
            this.RandomizeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // minTextBox
            // 
            this.minTextBox.Location = new System.Drawing.Point(40, 33);
            this.minTextBox.Name = "minTextBox";
            this.minTextBox.Size = new System.Drawing.Size(100, 23);
            this.minTextBox.TabIndex = 0;
            // 
            // maxTextBox
            // 
            this.maxTextBox.Location = new System.Drawing.Point(40, 86);
            this.maxTextBox.Name = "maxTextBox";
            this.maxTextBox.Size = new System.Drawing.Size(100, 23);
            this.maxTextBox.TabIndex = 1;
            // 
            // RandomizeButton
            // 
            this.RandomizeButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RandomizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RandomizeButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RandomizeButton.Location = new System.Drawing.Point(193, 37);
            this.RandomizeButton.Name = "RandomizeButton";
            this.RandomizeButton.Size = new System.Drawing.Size(116, 55);
            this.RandomizeButton.TabIndex = 2;
            this.RandomizeButton.Text = "Randomize Weights";
            this.RandomizeButton.UseVisualStyleBackColor = false;
            this.RandomizeButton.Click += new System.EventHandler(this.RandomizeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(40, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Minimum:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(40, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Maximum:";
            // 
            // RandomWeights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 137);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RandomizeButton);
            this.Controls.Add(this.maxTextBox);
            this.Controls.Add(this.minTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RandomWeights";
            this.Text = "RandomWeights";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox minTextBox;
        private TextBox maxTextBox;
        private Button RandomizeButton;
        private Label label1;
        private Label label2;
    }
}