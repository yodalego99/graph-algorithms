namespace Gráfelméleti_algoritmusok
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.AddNodeButton = new System.Windows.Forms.Button();
            this.AddEdgeButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExplainAlgoButton = new System.Windows.Forms.Button();
            this.HelpButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.PrevButton = new System.Windows.Forms.Button();
            this.SelectStartButton = new System.Windows.Forms.Button();
            this.selectFolder = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.AddWeightButton = new System.Windows.Forms.Button();
            this.AddDirectedEdgeButton = new System.Windows.Forms.Button();
            this.SelectEndPointButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.RandomizeWeightsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(10, 17);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 23);
            this.comboBox1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(24, 158);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(335, 516);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ResetButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ResetButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ResetButton.Location = new System.Drawing.Point(385, 9);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(134, 29);
            this.ResetButton.TabIndex = 8;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // AddNodeButton
            // 
            this.AddNodeButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.AddNodeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddNodeButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddNodeButton.Location = new System.Drawing.Point(525, 9);
            this.AddNodeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddNodeButton.Name = "AddNodeButton";
            this.AddNodeButton.Size = new System.Drawing.Size(134, 29);
            this.AddNodeButton.TabIndex = 9;
            this.AddNodeButton.Text = "Add Node";
            this.AddNodeButton.UseVisualStyleBackColor = false;
            this.AddNodeButton.Click += new System.EventHandler(this.AddNodeButton_Click);
            // 
            // AddEdgeButton
            // 
            this.AddEdgeButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.AddEdgeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddEdgeButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddEdgeButton.Location = new System.Drawing.Point(665, 9);
            this.AddEdgeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddEdgeButton.Name = "AddEdgeButton";
            this.AddEdgeButton.Size = new System.Drawing.Size(134, 29);
            this.AddEdgeButton.TabIndex = 11;
            this.AddEdgeButton.Text = "Add Edge";
            this.AddEdgeButton.UseVisualStyleBackColor = false;
            this.AddEdgeButton.Click += new System.EventHandler(this.AddEdgeButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StartButton.Location = new System.Drawing.Point(84, 58);
            this.StartButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(82, 22);
            this.StartButton.TabIndex = 12;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SaveButton.Enabled = false;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SaveButton.Location = new System.Drawing.Point(237, 84);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(122, 41);
            this.SaveButton.TabIndex = 13;
            this.SaveButton.Text = "Save Current Graph";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ExplainAlgoButton
            // 
            this.ExplainAlgoButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ExplainAlgoButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExplainAlgoButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ExplainAlgoButton.Location = new System.Drawing.Point(215, 17);
            this.ExplainAlgoButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExplainAlgoButton.Name = "ExplainAlgoButton";
            this.ExplainAlgoButton.Size = new System.Drawing.Size(24, 21);
            this.ExplainAlgoButton.TabIndex = 14;
            this.ExplainAlgoButton.Text = "?";
            this.ExplainAlgoButton.UseVisualStyleBackColor = false;
            this.ExplainAlgoButton.Click += new System.EventHandler(this.ExplainAlgoButton_Click);
            // 
            // HelpButton
            // 
            this.HelpButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.HelpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.HelpButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.HelpButton.Location = new System.Drawing.Point(24, 683);
            this.HelpButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(120, 50);
            this.HelpButton.TabIndex = 15;
            this.HelpButton.Text = "Help";
            this.HelpButton.UseVisualStyleBackColor = false;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(385, 43);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(1285, 691);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1300, 691);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // LoadButton
            // 
            this.LoadButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LoadButton.Enabled = false;
            this.LoadButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoadButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LoadButton.Location = new System.Drawing.Point(237, 127);
            this.LoadButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(122, 26);
            this.LoadButton.TabIndex = 17;
            this.LoadButton.Text = "Load Graph";
            this.LoadButton.UseVisualStyleBackColor = false;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.NextButton.Enabled = false;
            this.NextButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NextButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.NextButton.Location = new System.Drawing.Point(130, 127);
            this.NextButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(89, 26);
            this.NextButton.TabIndex = 18;
            this.NextButton.Text = "Forward";
            this.NextButton.UseVisualStyleBackColor = false;
            // 
            // PrevButton
            // 
            this.PrevButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.PrevButton.Enabled = false;
            this.PrevButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PrevButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PrevButton.Location = new System.Drawing.Point(32, 127);
            this.PrevButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(89, 26);
            this.PrevButton.TabIndex = 19;
            this.PrevButton.Text = "Backward";
            this.PrevButton.UseVisualStyleBackColor = false;
            // 
            // SelectStartButton
            // 
            this.SelectStartButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SelectStartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectStartButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SelectStartButton.Location = new System.Drawing.Point(955, 9);
            this.SelectStartButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectStartButton.Name = "SelectStartButton";
            this.SelectStartButton.Size = new System.Drawing.Size(164, 29);
            this.SelectStartButton.TabIndex = 20;
            this.SelectStartButton.Text = "Select Starting Point";
            this.SelectStartButton.UseVisualStyleBackColor = false;
            this.SelectStartButton.Click += new System.EventHandler(this.SelectStartButton_Click);
            // 
            // selectFolder
            // 
            this.selectFolder.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.selectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.selectFolder.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.selectFolder.Location = new System.Drawing.Point(237, 43);
            this.selectFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectFolder.Name = "selectFolder";
            this.selectFolder.Size = new System.Drawing.Size(122, 38);
            this.selectFolder.TabIndex = 21;
            this.selectFolder.Text = "Select Folder";
            this.selectFolder.UseVisualStyleBackColor = false;
            this.selectFolder.Click += new System.EventHandler(this.selectFolder_Click);
            // 
            // StopButton
            // 
            this.StopButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.StopButton.Enabled = false;
            this.StopButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.StopButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StopButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StopButton.Location = new System.Drawing.Point(84, 94);
            this.StopButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(82, 22);
            this.StopButton.TabIndex = 22;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // AddWeightButton
            // 
            this.AddWeightButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.AddWeightButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddWeightButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddWeightButton.Location = new System.Drawing.Point(1265, 9);
            this.AddWeightButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddWeightButton.Name = "AddWeightButton";
            this.AddWeightButton.Size = new System.Drawing.Size(134, 29);
            this.AddWeightButton.TabIndex = 23;
            this.AddWeightButton.Text = "Add Weight";
            this.AddWeightButton.UseVisualStyleBackColor = false;
            this.AddWeightButton.Click += new System.EventHandler(this.AddWeightButton_Click);
            // 
            // AddDirectedEdgeButton
            // 
            this.AddDirectedEdgeButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.AddDirectedEdgeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddDirectedEdgeButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddDirectedEdgeButton.Location = new System.Drawing.Point(805, 9);
            this.AddDirectedEdgeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddDirectedEdgeButton.Name = "AddDirectedEdgeButton";
            this.AddDirectedEdgeButton.Size = new System.Drawing.Size(144, 29);
            this.AddDirectedEdgeButton.TabIndex = 24;
            this.AddDirectedEdgeButton.Text = "Add Directed Edge";
            this.AddDirectedEdgeButton.UseVisualStyleBackColor = false;
            this.AddDirectedEdgeButton.Click += new System.EventHandler(this.AddDirectedEdgeButton_Click);
            // 
            // SelectEndPointButton
            // 
            this.SelectEndPointButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SelectEndPointButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectEndPointButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SelectEndPointButton.Location = new System.Drawing.Point(1125, 9);
            this.SelectEndPointButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectEndPointButton.Name = "SelectEndPointButton";
            this.SelectEndPointButton.Size = new System.Drawing.Size(134, 29);
            this.SelectEndPointButton.TabIndex = 25;
            this.SelectEndPointButton.Text = "Select End Point";
            this.SelectEndPointButton.UseVisualStyleBackColor = false;
            this.SelectEndPointButton.Click += new System.EventHandler(this.SelectEndPointButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DeleteButton.Location = new System.Drawing.Point(1405, 9);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(144, 29);
            this.DeleteButton.TabIndex = 26;
            this.DeleteButton.Text = "Delete Node/Edge";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // RandomizeWeightsButton
            // 
            this.RandomizeWeightsButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RandomizeWeightsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RandomizeWeightsButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RandomizeWeightsButton.Location = new System.Drawing.Point(1555, 9);
            this.RandomizeWeightsButton.Name = "RandomizeWeightsButton";
            this.RandomizeWeightsButton.Size = new System.Drawing.Size(128, 29);
            this.RandomizeWeightsButton.TabIndex = 27;
            this.RandomizeWeightsButton.Text = "Random Weights";
            this.RandomizeWeightsButton.UseVisualStyleBackColor = false;
            this.RandomizeWeightsButton.Click += new System.EventHandler(this.RandomizeWeightsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1695, 758);
            this.Controls.Add(this.RandomizeWeightsButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SelectEndPointButton);
            this.Controls.Add(this.AddDirectedEdgeButton);
            this.Controls.Add(this.AddWeightButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.selectFolder);
            this.Controls.Add(this.SelectStartButton);
            this.Controls.Add(this.PrevButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.ExplainAlgoButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.AddEdgeButton);
            this.Controls.Add(this.AddNodeButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.comboBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1012, 439);
            this.Name = "Form1";
            this.Text = "Gráfelméleti algoritmusok szemléltetése";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox comboBox1;
        private RichTextBox richTextBox1;
        private Button ResetButton;
        private Button AddNodeButton;
        private Button AddEdgeButton;
        private Button StartButton;
        private Button SaveButton;
        private Button ExplainAlgoButton;
        private Button HelpButton;
        private PictureBox pictureBox1;
        private Button LoadButton;
        private Button NextButton;
        private Button PrevButton;
        private Button SelectStartButton;
        private Button selectFolder;
        private Button StopButton;
        private Button AddWeightButton;
        private Button AddDirectedEdgeButton;
        private Button SelectEndPointButton;
        private Button DeleteButton;
        private Button RandomizeWeightsButton;
    }
}