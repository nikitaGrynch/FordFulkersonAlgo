namespace FordFulkersonWinForms
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSourceVertex = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTargetVertex = new System.Windows.Forms.ComboBox();
            this.buttonSelectGraph = new System.Windows.Forms.Button();
            this.richTextBoxRoutes = new System.Windows.Forms.RichTextBox();
            this.labelStatusJson = new System.Windows.Forms.Label();
            this.buttonStartAlgo = new System.Windows.Forms.Button();
            this.openFileDialogSelectGraph = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source";
            // 
            // comboBoxSourceVertex
            // 
            this.comboBoxSourceVertex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSourceVertex.FormattingEnabled = true;
            this.comboBoxSourceVertex.Location = new System.Drawing.Point(12, 121);
            this.comboBoxSourceVertex.Name = "comboBoxSourceVertex";
            this.comboBoxSourceVertex.Size = new System.Drawing.Size(87, 40);
            this.comboBoxSourceVertex.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Target";
            // 
            // comboBoxTargetVertex
            // 
            this.comboBoxTargetVertex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetVertex.FormattingEnabled = true;
            this.comboBoxTargetVertex.Location = new System.Drawing.Point(12, 227);
            this.comboBoxTargetVertex.Name = "comboBoxTargetVertex";
            this.comboBoxTargetVertex.Size = new System.Drawing.Size(87, 40);
            this.comboBoxTargetVertex.TabIndex = 2;
            // 
            // buttonSelectGraph
            // 
            this.buttonSelectGraph.Location = new System.Drawing.Point(12, 12);
            this.buttonSelectGraph.Name = "buttonSelectGraph";
            this.buttonSelectGraph.Size = new System.Drawing.Size(178, 46);
            this.buttonSelectGraph.TabIndex = 3;
            this.buttonSelectGraph.Text = "Select Graph";
            this.buttonSelectGraph.UseVisualStyleBackColor = true;
            this.buttonSelectGraph.Click += new System.EventHandler(this.buttonSelectGraph_Click);
            // 
            // richTextBoxRoutes
            // 
            this.richTextBoxRoutes.Location = new System.Drawing.Point(144, 86);
            this.richTextBoxRoutes.Name = "richTextBoxRoutes";
            this.richTextBoxRoutes.ReadOnly = true;
            this.richTextBoxRoutes.Size = new System.Drawing.Size(488, 530);
            this.richTextBoxRoutes.TabIndex = 4;
            this.richTextBoxRoutes.Text = "";
            // 
            // labelStatusJson
            // 
            this.labelStatusJson.AutoSize = true;
            this.labelStatusJson.ForeColor = System.Drawing.Color.Red;
            this.labelStatusJson.Location = new System.Drawing.Point(210, 19);
            this.labelStatusJson.Name = "labelStatusJson";
            this.labelStatusJson.Size = new System.Drawing.Size(216, 32);
            this.labelStatusJson.TabIndex = 5;
            this.labelStatusJson.Text = "Graph not selected";
            // 
            // buttonStartAlgo
            // 
            this.buttonStartAlgo.Location = new System.Drawing.Point(12, 570);
            this.buttonStartAlgo.Name = "buttonStartAlgo";
            this.buttonStartAlgo.Size = new System.Drawing.Size(126, 46);
            this.buttonStartAlgo.TabIndex = 6;
            this.buttonStartAlgo.Text = "Start";
            this.buttonStartAlgo.UseVisualStyleBackColor = true;
            this.buttonStartAlgo.Click += new System.EventHandler(this.buttonStartAlgo_Click);
            // 
            // openFileDialogSelectGraph
            // 
            this.openFileDialogSelectGraph.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 658);
            this.Controls.Add(this.buttonStartAlgo);
            this.Controls.Add(this.labelStatusJson);
            this.Controls.Add(this.richTextBoxRoutes);
            this.Controls.Add(this.buttonSelectGraph);
            this.Controls.Add(this.comboBoxTargetVertex);
            this.Controls.Add(this.comboBoxSourceVertex);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "FordFulkerson";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxSourceVertex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxTargetVertex;
        private System.Windows.Forms.Button buttonSelectGraph;
        private System.Windows.Forms.RichTextBox richTextBoxRoutes;
        private System.Windows.Forms.Label labelStatusJson;
        private System.Windows.Forms.Button buttonStartAlgo;
        private System.Windows.Forms.OpenFileDialog openFileDialogSelectGraph;
    }
}
